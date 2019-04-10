using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IBP.Services;
using IBP.Models;
using Framework.Utilities;

namespace IBP.Controllers
{
    public class ProductCenterController : BaseController
    {
        [AuthorizeFlag]
        public ActionResult ProductSaleStatusMgr()
        {
            InitPagerForm();
            ViewBag.ProductCategoryId = GetFormData("productCategoryId");
            ViewBag.StatusName = GetFormData("salesStatus");
            ViewBag.ProductName = GetFormData("productName");

            int total = 0;
            string message = "操作失败，请管理员联系";

            ViewBag.ProductIdList = ProductInfoService.Instance.GetProductList(ViewBag.ProductCategoryId, ViewBag.ProductName, ViewBag.StatusName, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total, out message);
            ViewBag.ProductTotal = total;

            return View();
        }

        #region 产品入库管理

        [AuthorizeFlag]
        public ActionResult ProductStockMgr()
        {
            InitPagerForm();

            ViewBag.ProductCategoryId = (string.IsNullOrEmpty(GetFormData("productCategory"))) ? ProductCategoryInfoService.Instance.GetProductCategoryInfoBySortOrder(1).ProductCategoryId : GetFormData("productCategory");
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            ViewBag.ProductAttributeList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(ViewBag.ProductCategoryId, false) as Dictionary<string, ProductCategoryAttributesModel>;
            ViewBag.SaleStatusList = ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(ViewBag.ProductCategoryId, false);
            
            if (ViewBag.ProductCategoryId != null)
            {
                QueryItemDomainModel queryItem = null;

                foreach (ProductCategoryAttributesModel item in ViewBag.ProductAttributeList.Values)
                {
                    if (!string.IsNullOrEmpty(GetFormData(item.CategoryAttributeId)) && GetFormData("chk_" + item.CategoryAttributeId) == "0")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = "datetime";
                        queryItem.Operation = GetFormData("opddl_" + item.CategoryAttributeId);

                        if (item.FieldType == "datetime")
                        {
                            queryItem.BeginTime = Convert.ToDateTime(GetFormData(item.CategoryAttributeId));
                            queryItem.EndTime = Convert.ToDateTime(GetFormData("date_end_" + item.CategoryAttributeId));
                        }
                        else
                        {
                            queryItem.SearchValue = GetFormData(item.CategoryAttributeId);
                        }

                        queryCollection.Add(item.CategoryAttributeId, queryItem);
                    }
                }
            }

            int total = 0;
            ViewBag.ProductIdList = ProductInfoService.Instance.GetProductList(ViewBag.ProductCategoryId, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.ProductTotal = total;

            return View();
        }

        [AuthorizeFlag]
        public ActionResult AddProductInfo()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult ProductExtAttributeList()
        {
            return View();
        }


        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddProductInfo()
        {
            string message = "操作失败，请与管理员联系";

            string productCategoryId = GetFormData("productCategory");
            Dictionary<string, ProductCategoryAttributesModel> catAttList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(productCategoryId, false);
            if (catAttList == null)
            {
                return FailedJson("操作失败，不存在的产品类型ID");
            }

            ProductInfoDomainModel productInfo = new ProductInfoDomainModel();
            productInfo.BasicInfo = new ProductInfoModel();
            productInfo.BasicInfo.CategoryId = productCategoryId;
            productInfo.BasicInfo.ProductId = Guid.NewGuid().ToString();
            productInfo.BasicInfo.ProductCode = (GetFormData("productCode") == "AUTO" || GetFormData("productCode") == null) ? productInfo.BasicInfo.ProductId : GetFormData("productCode");
            productInfo.BasicInfo.ProductName = GetFormData("productName");
            productInfo.BasicInfo.SalesStatus = GetFormData("productSaleStatus");

            productInfo.AttributeList = new Dictionary<string, string>();

            string attValue = "";
            foreach (ProductCategoryAttributesModel item in catAttList.Values)
            {
                if (item.AttributeName == "产品代码")
                {
                    productInfo.AttributeList.Add(item.CategoryAttributeId, productInfo.BasicInfo.ProductCode);
                }
                else if (item.AttributeName == "产品名称")
                {
                    productInfo.AttributeList.Add(item.CategoryAttributeId, productInfo.BasicInfo.ProductName);
                }
                else if (item.AttributeName == "销售状态")
                {
                    productInfo.AttributeList.Add(item.CategoryAttributeId, productInfo.BasicInfo.SalesStatus);
                }  
                else
                {
                    attValue = GetFormData(item.AttributeName);
                    if (item.IsRequest == 0 && string.IsNullOrEmpty(attValue))
                    {
                        return FailedJson("操作失败，请检查所有必填项是否全部输入");
                    }

                    switch (item.FieldType)
                    {
                        case "string":
                            if (attValue.Length < item.FieldMinLength || attValue.Length > item.FieldMaxLength)
                            {
                                return FailedJson(string.Format("操作失败，【{0}】属性长度不符合要求，请检查输入", item.AttributeName));
                            }
                            break;

                        case "decimal":
                            if (RegexUtil.IsNumeric(attValue) == false)
                            {
                                return FailedJson(string.Format("操作失败，【{0}】属性为数值类型，请检查输入", item.AttributeName));
                            }
                            break;

                        case "datetime":
                            if (RegexUtil.IsDateTime(attValue) == false)
                            {
                                return FailedJson(string.Format("操作失败，【{0}】属性为日期时间类型，请检查输入", item.AttributeName));
                            }
                            break;

                        case "custom":
                            if (item.CustomValue.Split('\n').ToList().Contains(attValue) == false)
                            {
                                return FailedJson(string.Format("操作失败，【{0}】属性值输入不在枚举范围内，请检查输入", item.AttributeName));
                            }
                            break;

                        default:
                            break;
                    }
                    productInfo.AttributeList.Add(item.CategoryAttributeId, attValue);
                }
            }

            if (ProductInfoService.Instance.CreateProductInfo(productInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductStockMgr", "ProductCenter_ProductStockMgr", "forward", "/productcenter/ProductStockMgr?catid=" + productInfo.BasicInfo.CategoryId + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        #endregion

        #region 产品分组管理

        [AuthorizeFlag]
        public ActionResult ProductCategoryGroupMgr()
        {
            ViewBag.CategoryGroupName = GetFormData("categoryGroupName");
            InitPagerForm();
            int total = 0;
            ViewBag.PageSize = 2000;

            ViewBag.ProductCategoryGroupList = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupFromDatabase(ViewBag.CategoryGroupName, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.ProductCategoryGroupTotal = total;

            return View();
        }

        [AuthorizeFlag]
        public ActionResult NewProductCategoryGroup()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult EditProductCategoryGroup()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddProductCategoryGroup()
        {
            ProductCategoryGroupInfoModel groupInfo = new ProductCategoryGroupInfoModel();
            groupInfo.GroupName = GetFormData("groupName");
            groupInfo.IsItemPrice = (GetFormData("isItemPrice") != null) ? ((GetFormData("isItemPrice") == "0") ? 0 : 1) : 1;
            groupInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));
            groupInfo.Status = Convert.ToInt32(GetFormData("Status"));
            groupInfo.Description = GetFormData("categoryGroupDesc");
            groupInfo.ProductCategoryGroupId = GetGuid();

            string message = "操作失败，请与管理员联系";

            if (ProductCategoryGroupInfoService.Instance.CreateProductCategoryGroupInfo(groupInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryGroupMgr", "ProductCenter_ProductCategoryGroupMgr", "closeCurrent", "/productcenter/productcategorygroupmgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateProductCategoryGroup()
        {
            ProductCategoryGroupInfoModel groupInfo = new ProductCategoryGroupInfoModel();
            groupInfo.ProductCategoryGroupId = GetFormData("categoryGroupId");

            groupInfo.GroupName = GetFormData("groupName");
            groupInfo.IsItemPrice = (GetFormData("isItemPrice") != null) ? ((GetFormData("isItemPrice") == "0") ? 0 : 1) : 1;
            groupInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));
            groupInfo.Status = Convert.ToInt32(GetFormData("Status"));
            groupInfo.Description = GetFormData("categoryGroupDesc");
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryGroupInfoService.Instance.UpdateProductCategoryGroupInfo(groupInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryGroupMgr", "ProductCenter_ProductCategoryGroupMgr", "closeCurrent", "/productcenter/productcategorygroupmgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        #endregion

        #region 产品类型

        [AuthorizeFlag]
        public ActionResult ProductCategoryMgr()
        {
           GetProductCategoryInfoFromDatabase();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult NewProductCategory()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult EditProductCategory()
        {
            return View();
        }
        
        /// <summary>
        /// 创建产品类型信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AuthorizeFlag]
        public JsonResult DoAddProductCategory()
        {
            this.ValidateRequest = false;
            ProductCategoryInfoModel categoryInfo = new ProductCategoryInfoModel();
            categoryInfo.CategoryName = GetFormData("categoryName");
            categoryInfo.SaleCity = GetFormData("saleCity");
            categoryInfo.CategoryCode = GetFormData("categoryCode");
            categoryInfo.Remark = GetFormData("remark");
            categoryInfo.Description = Request.Form["description"]; // GetFormDataNotValidate("description");
            categoryInfo.SalesGuide = Request.Form["salesGuide"]; // GetFormDataNotValidate("salesGuide");

            categoryInfo.GroupName = GetFormData("groupName");
            categoryInfo.ItemPrice = Convert.ToDecimal(GetFormData("itemPrice"));
            categoryInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
            string message = "操作失败，请与管理员联系";
            this.ValidateRequest = true;

            if (ProductCategoryInfoService.Instance.CreateProductCategory(categoryInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryMgr", "ProductCenter_ProductCategoryMgr", "closeCurrent", "/productcenter/productcategorymgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新产品类型信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AuthorizeFlag]
        public JsonResult DoUpdateProductCategory()
        {
            this.ValidateRequest = false;
            ProductCategoryInfoModel categoryInfo = new ProductCategoryInfoModel();
            categoryInfo.ProductCategoryId = GetFormData("categoryId");
            categoryInfo.SaleCity = GetFormData("saleCity");
            categoryInfo.CategoryCode = GetFormData("categoryCode");
            categoryInfo.CategoryName = GetFormData("categoryName");
            categoryInfo.GroupName = GetFormData("groupName");
            categoryInfo.Description = Request.Form["description"]; // GetFormDataNotValidate("description");
            categoryInfo.SalesGuide = Request.Form["salesGuide"]; // GetFormDataNotValidate("salesGuide");
            categoryInfo.TableName = GetFormData("tableName");
            categoryInfo.ItemPrice = Convert.ToDecimal(GetFormData("itemPrice"));
            categoryInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
            categoryInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));
            this.ValidateRequest = true;
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryInfoService.Instance.UpdateProductCategory(categoryInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryMgr", "ProductCenter_ProductCategoryMgr", "closeCurrent", "/productcenter/productcategorymgr");
            }
            else
            {
                return FailedJson(message);
            }
        }


        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDelProductCategory()
        {
            string categoryId = GetQueryString("catid");
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryInfoService.Instance.DeleteProductCategory(categoryId, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryMgr", "ProductCenter_ProductCategoryMgr", "forward", "/productcenter/productcategorymgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        #endregion

        #region 产品类型属性

        [AuthorizeFlag]
        public ActionResult ProductCategoryAttribute()
        {
            GetProductCategoryInfoFromDatabase();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult ProductCategoryAttributeList()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult NewProductCategoryAttribute()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult EditProductCategoryAttribute()
        {
            return View();
        }

        /// <summary>
        /// 上移产品类型属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveUpProductCategoryAttribute()
        {
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryAttributesService.Instance.MoveUpProductCategoryAttribute(Request.QueryString["id"].Split('|')[1], Request.QueryString["id"].Split('|')[0], out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryAttribute", "ProductCenter_ProductCategoryAttribute", "forward", "/productcenter/ProductCategoryAttribute?catid=" + Request.QueryString["id"].Split('|')[1] + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 下移产品类型属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveDownProductCategoryAttribute()
        {
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryAttributesService.Instance.MoveDownProductCategoryAttribute(Request.QueryString["id"].Split('|')[1], Request.QueryString["id"].Split('|')[0], out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryAttribute", "ProductCenter_ProductCategoryAttribute", "forward", "/productcenter/ProductCategoryAttribute?catid=" + Request.QueryString["id"].Split('|')[1] + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新产品类型属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateProductCategoryAttribute()
        {
            ProductCategoryAttributesModel attInfo = new ProductCategoryAttributesModel();
            string message = "操作失败，请与管理员联系";

            try
            {
                attInfo.CategoryAttributeId = GetFormData("attributeId");
                attInfo.ProductCategoryId = GetFormData("categoryId");
                attInfo.AttributeName = GetFormData("attributeName");
                attInfo.CustomValue = GetFormData("customValue");
                attInfo.FieldMaxLength = (GetFormData("maxLength") == null || GetFormData("maxLength") == "") ? -1 : Convert.ToInt32(GetFormData("maxLength"));
                attInfo.FieldMinLength = (GetFormData("minLength") == null || GetFormData("minLength") == "") ? -1 : Convert.ToInt32(GetFormData("minLength"));
                attInfo.FieldType = GetFormData("fieldType");
                attInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));

                attInfo.DefaultValue = (attInfo.FieldType == "string" || attInfo.FieldType == "decimal" || attInfo.FieldType == "text") ? GetFormData("defaultValue1") : GetFormData("defaultValue2");
                attInfo.GroupName = GetFormData("groupName");
                attInfo.IsRequest = (GetFormData("isRequest") == "0") ? 0 : 1;
                attInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
                attInfo.Description = GetFormData("attDesc");
            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (ProductCategoryAttributesService.Instance.UpdateProductCategoryAttribute(attInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryAttribute", "ProductCenter_ProductCategoryAttribute", "forward", "/productcenter/ProductCategoryAttribute?catid=" + attInfo.ProductCategoryId.ToString() + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }


        /// <summary>
        /// 新建产品类型属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddProductCategoryAttribute()
        {
            ProductCategoryAttributesModel attInfo = new ProductCategoryAttributesModel();
            string message = "操作失败，请与管理员联系";

           try
            {
                attInfo.ProductCategoryId = GetFormData("categoryId");
                attInfo.AttributeName = GetFormData("attributeName");
                attInfo.CustomValue = GetFormData("customValue");
                attInfo.FieldMaxLength = (GetFormData("maxLength") == null || GetFormData("maxLength") == "") ? -1 : Convert.ToInt32(GetFormData("maxLength"));
                attInfo.FieldMinLength = (GetFormData("minLength") == null || GetFormData("minLength") == "") ? -1 : Convert.ToInt32(GetFormData("minLength"));
                attInfo.FieldType = GetFormData("fieldType");

                attInfo.DefaultValue = (attInfo.FieldType == "string" || attInfo.FieldType == "decimal" || attInfo.FieldType == "text") ? GetFormData("defaultValue1") : GetFormData("defaultValue2");
                attInfo.GroupName = GetFormData("groupName");
                attInfo.IsRequest = (GetFormData("isRequest") == "0") ? 0 : 1;
                attInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
                attInfo.Description = GetFormData("attDesc");
            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(attInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryAttribute", "ProductCenter_ProductCategoryAttribute", "forward", "/productcenter/ProductCategoryAttribute?catid=" + attInfo.ProductCategoryId.ToString() + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }


        [HttpPost]
        [AuthorizeFlag]
        public JsonResult GetProductCategoryAttributeListByCategoryId()
        {
            string productCategoryId = GetFormData("ProductCategoryId");
            Dictionary<string, ProductCategoryAttributesModel> dict = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(productCategoryId, false);

            List<ProductCategoryAttributesModel> list = new List<ProductCategoryAttributesModel>();
            if (dict != null)
            {
                foreach (ProductCategoryAttributesModel item in dict.Values)
                {
                    list.Add(item);
                }
            }

            return Json(list);
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult GetProductCategoryListByGroupId()
        {
            string productCategoryGroupId = GetFormData("ProductCategoryGroupId");
            Dictionary<string, ProductCategoryInfoModel> dict = ProductCategoryInfoService.Instance.GetProductCategoryListByGroupId(productCategoryGroupId);

            List<ProductCategoryInfoModel> list = new List<ProductCategoryInfoModel>();
            if (dict != null)
            {
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    list.Add(item);
                }
            }

            return Json(list);
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteProductCategoryAttribute()
        {
            string message = "操作失败，请与管理员联系";

            if (ProductCategoryAttributesService.Instance.DeleteProductCategoryAttribute(Request.QueryString["id"].Split('|')[1], Request.QueryString["id"].Split('|')[0], out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategoryAttribute", "ProductCenter_ProductCategoryAttribute", "forward", "/productcenter/ProductCategoryAttribute?catid=" + Request.QueryString["id"].Split('|')[1] + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        [AuthorizeFlag]
        public ActionResult ProductSearchPanel()
        {
            return View();
        }

        #endregion

        #region 产品类型销售状态

        [AuthorizeFlag]
        public ActionResult ProductCategorySaleStatus()
        {
            string categoryGroupName = GetFormData("categoryGroupName");
            GetProductCategoryInfoFromDatabase();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult ProductCategorySaleStatusList()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult NewProductCategorySaleStatus()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult EditProductCategorySaleStatus()
        {
            return View();
        }
        
        /// <summary>
        /// 创建产品类型销售状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddProductCategorySaleStatus()
        {
            ProductCategorySalesStatusModel statusInfo = new ProductCategorySalesStatusModel();
            statusInfo.ProductCategoryId = GetFormData("categoryId");
            statusInfo.SalestatusName = GetFormData("statusName");
            statusInfo.Description = GetFormData("statusDesc");
            statusInfo.Status = Convert.ToInt32(GetFormData("Status"));

            string message = "操作失败，请与管理员联系";

            if (ProductCategorySalesStatusService.Instance.CreateProductCategorySaleStatus(statusInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategorySaleStatus", "ProductCenter_ProductCategorySaleStatus", "forward", "/productcenter/ProductCategorySaleStatus?catid=" + statusInfo.ProductCategoryId.ToString() + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 上移产品类型销售状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult MoveUpProductCategorySaleStatus()
        {
            string message = "操作失败，请与管理员联系";

            if (ProductCategorySalesStatusService.Instance.MoveUpProductCategorySaleStatus(Request.QueryString["id"].Split('|')[1], Request.QueryString["id"].Split('|')[0], out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategorySaleStatus", "ProductCenter_ProductCategorySaleStatus", "forward", "/productcenter/ProductCategorySaleStatus?=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 下移产品类型销售状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult MoveDownProductCategorySaleStatus()
        {
            string message = "操作失败，请与管理员联系";

            if (ProductCategorySalesStatusService.Instance.MoveDownProductCategorySaleStatus(Request.QueryString["id"].Split('|')[1], Request.QueryString["id"].Split('|')[0], out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategorySaleStatus", "ProductCenter_ProductCategorySaleStatus", "forward", "/productcenter/ProductCategorySaleStatus?=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新产品类型销售状态信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateProductCategorySaleStatus()
        {
            ProductCategorySalesStatusModel statusInfo = new ProductCategorySalesStatusModel();
            statusInfo.SalesStatusId = GetFormData("statusId");
            statusInfo.ProductCategoryId = GetFormData("categoryId");
            statusInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));
            statusInfo.SalestatusName = GetFormData("statusName");
            statusInfo.Description = GetFormData("statusDesc");
            statusInfo.Status = Convert.ToInt32(GetFormData("Status"));

            string message = "操作失败，请与管理员联系";

            if (ProductCategorySalesStatusService.Instance.UpdateProductCategorySaleStatus(statusInfo, out message))
            {
                return SuccessedJson(message, "ProductCenter_ProductCategorySaleStatus", "ProductCenter_ProductCategorySaleStatus", "forward", "/productcenter/ProductCategorySaleStatus?catid=" + statusInfo.ProductCategoryId.ToString() + "&=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult GetProductSaleStatusListByCategoryId()
        {
            string productCategoryId = GetFormData("ProductCategoryId");
            Dictionary<string, ProductCategorySalesStatusModel> dict = ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(productCategoryId, false);
            
            List<ProductCategorySalesStatusModel> list = new List<ProductCategorySalesStatusModel>();
            if (dict != null)
            {
                foreach(ProductCategorySalesStatusModel item in dict.Values)
                {
                    list.Add(item);
                }
            }

            return Json(list);
        }

        #endregion

        #region 私有方法

        protected void GetProductCategoryInfoFromDatabase()
        {
            ViewBag.CategoryName = GetFormData("categoryName");
            ViewBag.ProductGroupName = GetFormData("categoryGroupName");
            ViewBag.SaleCity = GetFormData("saleCity");
            InitPagerForm();
            int total = 0;
            ViewBag.ProductCategoryList = ProductCategoryInfoService.Instance.GetProductCategoryListFromDatabase(ViewBag.CategoryName, ViewBag.ProductGroupName,ViewBag.SaleCity, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.ProductCategoryTotal = total;
        }

        #endregion
    }
}
