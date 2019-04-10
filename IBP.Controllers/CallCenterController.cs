using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Framework.Utilities;
using IBP.Models;
using IBP.Services;
namespace IBP.Controllers
{
    public class CallCenterController : BaseController
    {
        #region 创建客户信息
        /// <summary>
        /// 新建分组信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewCustomerAttributeGroupInfo()
        {
            return View();
        }
        /// <summary>
        /// 编辑分组信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DoUpdateCustomerAttributeGrouInfo()
        {
            CustomerAttributeGroupInfoModel attInfo = new CustomerAttributeGroupInfoModel();
            string message = "操作失败，请与管理员联系";

            try
            {
                attInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
                attInfo.GroupName =GetFormData("groupName");
                attInfo.GroupId = GetFormData("groupid");
       
            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (CustomerAttributeGroupInfoService.Instance.UpdateGroupInfo(attInfo, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerAttributeGroupMgr", "CallCenter_CustomerAttributeGroupMgr", "closeCurrent", "/CallCenter/CustomerAttributeGroupMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }


        /// <summary>
        /// 删除编辑分组信息。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteCustomerAttributeGroup()
        {
            string groupid = GetQueryString("id");
            string resultMessage = "";
            if (CustomerAttributeGroupInfoService.Instance.DeleteGroupInfoById(groupid, out resultMessage))
            {
                return SuccessedJson(resultMessage, "CallCenter_CustomerAttributeGroupMgr", "CallCenter_CustomerAttributeGroupMgr", "", "");
            }
            else
            {
                return FailedJson(resultMessage);
            }
        }

        /// <summary>
        /// 编辑分组信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditCustomerAttributeGroup()
        {
            return View();
        }

        /// <summary>
        /// 新建分组信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DoAddCustomerAttributeGrouInfo()
        {
            CustomerAttributeGroupInfoModel model = new CustomerAttributeGroupInfoModel();
            string message = "操作失败，请与管理员联系";

            try
            {
                model.GroupName = GetFormData("groupName");
                model.Status = (GetFormData("Status") == "0") ? 0 : 1;

            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (CustomerAttributeGroupInfoService.Instance.CreateGroupInfo(model, out message))
            {

                return SuccessedJson(message, "CallCenter_CustomerAttributeGroupMgr", "CallCenter_CustomerAttributeGroupMgr", "closeCurrent", "/CallCenter/CustomerAttributeGroupMgr");

            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 新建客户信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddCustomerInfo()
        {
            return View();
        }


        [AuthorizeFlag]
        public ActionResult CustomerAttributeGroupMgr()
        {
            InitPagerForm();
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            queryItem = new QueryItemDomainModel();



            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.GroupInfo = CustomerAttributeGroupInfoService.Instance.GetGroupInfoList(queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.GroupInfoTotal = total;

            return View();
        }

          /// <summary>
        /// Excel导入创建客户视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelImportCustomerInfo()
        {          
            return View();      
        }

         //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Import(System.Web.HttpPostedFileBase FileData, string folder)
        {
            string result = "";
            string importLogs = null, message = "操作失败，请与管理员联系";
            if (null != FileData)
            {
                try
                {
                    result = Path.GetFileName(FileData.FileName);//获得文件名
                    string ext = Path.GetExtension(FileData.FileName);//获得文件扩展名
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ext;//实际保存文件名
                    saveFile(FileData, folder, saveName);//保存文件
                    string phyPath = Request.MapPath("~" + folder + "/" + saveName);
                    DataSet ds = CustomerBasicInfoService.Instance.ImportCustomerBasicInfoFromExcel(phyPath);
                    if (CustomerBasicInfoService.Instance.ImportCustomerBasicInfo(ds.Tables[0], out importLogs, out message))
                    {
                        return SuccessedJson(message, "CallCenter_CustomerMgr", "CallCenter_CustomerMgr", "closeCurrent", "/CallCenter/CustomerMgr");
                    }
                    else
                    {
                        return FailedJson(message);
                    }
                }
                catch
                {
                    return FailedJson(message);
                }
            }
            else
            {
                return FailedJson(message);
            }
           
        }
        [NonAction]
        private void saveFile(System.Web.HttpPostedFileBase postedFile, string filepath, string saveName)
        {
            string phyPath = Request.MapPath("~" + filepath + "/");
            if (!Directory.Exists(phyPath))
            {
                Directory.CreateDirectory(phyPath);
            }
            try
            {
                postedFile.SaveAs(phyPath + saveName);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);

            }
        }
        /// <summary>
        /// 批量创建客户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult BatchAddCustomer()
        {
         
            return View();
        }


        /// <summary>
        /// 批量新建客户信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoBatchAddCustomerInfo()
        {
            string message = "";
            CustomerBasicInfoModel model = new CustomerBasicInfoModel();
       
            model.CustomerName = "未知";
            model.Sex =2;
            model.SalesFrom = GetFormData("salesFrom");
            if (string.IsNullOrEmpty(model.SalesFrom))
            {
                return FailedJson("请选择客户来源");
            }
            model.Level = GetFormData("level");
            if (string.IsNullOrEmpty(model.Level))
            {
                return FailedJson("请选择客户等级");
            }    
            model.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            model.UsingPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            model.CommunicationConsumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            model.PreferredPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            model.UsingSmartphone = 3;
            model.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            string phoneNumebr = GetFormData("phone_number").Replace("\r\n","\n");
            List<string> PhoneNumebrList = phoneNumebr.Split('\n').ToList();
            if (PhoneNumebrList == null && PhoneNumebrList.Count == 0)
            {
                message = "请输入电话号码";
                return FailedJson(message);
            }
            if (CustomerBasicInfoService.Instance.BatchCreateCustomeBasicInfo(model, out message, phoneNumebr))
            {
                return SuccessedJson(message, "CallCenter_CustomerMgr", "CallCenter_CustomerMgr", "closeCurrent", "/CallCenter/CustomerMgr");
            }
            else
            {

                return FailedJson(message);
            }
        }

        #endregion

      
        #region 客户扩展属性信息表
        /// <summary>
        /// 客户扩展属性信息表
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult CustomerAttributeMgr()
        {
            InitPagerForm();
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            queryItem = new QueryItemDomainModel();



            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.Attribute = CustomerExtAttributesInfoService.Instance.GetCustomerAttributesList(queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.AttributeTotal = total;

            return View();

        }

        #endregion


        /// <summary>
        /// 删除客户属性信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]   
        public JsonResult DeleteCustomerAttribute()
        {
            string extattributeid = GetQueryString("id");
            string resultMessage = "";
            if (CustomerExtAttributesInfoService.Instance.DeleteCustomerAttributeById(extattributeid, out resultMessage))
            {
                return SuccessedJson(resultMessage, "CallCenter_CustomerAttributeMgr", "CallCenter_CustomerAttributeMgr", "", "");
            }
            else
            {
                return FailedJson(resultMessage);
            }
        }

      /// <summary>
        /// 新建客户属性视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewCustomerAttributes()
        {
            return View();
        }

        /// <summary>
        /// 编辑客户属性视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditCustomerAttributes()
        {
            return View();
        }


        /// <summary>
        /// 新建属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddcustomerAttribute()
        {
            CustomerExtAttributesModel attInfo = new CustomerExtAttributesModel();
            string message = "操作失败，请与管理员联系";

            try
            {
                attInfo.AttributeName = GetFormData("attributeName");
                attInfo.CustomValue = GetFormData("customValue");
                attInfo.FieldMaxLength = (GetFormData("maxLength") == null || GetFormData("maxLength") == "") ? -1 : Convert.ToInt32(GetFormData("maxLength"));
                attInfo.FieldMinLength = (GetFormData("minLength") == null || GetFormData("minLength") == "") ? -1 : Convert.ToInt32(GetFormData("minLength"));
                attInfo.FieldType = GetFormData("fieldType");

                attInfo.DefaultValue = (attInfo.FieldType == "string" || attInfo.FieldType == "decimal" || attInfo.FieldType == "text") ? GetFormData("defaultValue1") : GetFormData("defaultValue2");
                attInfo.GroupId = GetFormData("groupName");
                attInfo.IsDisplay = (GetFormData("isdisplay") == "0") ? 0 : 1;
                attInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
                attInfo.Description = GetFormData("attDesc");
            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (CustomerExtAttributesInfoService.Instance.CreateCustomerAttribute(attInfo, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerAttributeMgr", "CallCenter_CustomerAttributeMgr", "closeCurrent", "/CallCenter/CustomerAttributeMgr");

            }
            else
            {
                return FailedJson(message);
            }
        }

          /// <summary>
        /// 更新客户属性操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdatecustomerAttribute()
        {
            CustomerExtAttributesModel attInfo = new CustomerExtAttributesModel();
            string oldName = "";
            string message = "操作失败，请与管理员联系";

            try
            {
                attInfo.ExtAttributeId = GetFormData("extAttributeId");
                attInfo.AttributeName = GetFormData("attributeName");
                attInfo.CustomValue = GetFormData("customValue");
                attInfo.FieldMaxLength = (GetFormData("maxLength") == null || GetFormData("maxLength") == "") ? -1 : Convert.ToInt32(GetFormData("maxLength"));
                attInfo.FieldMinLength = (GetFormData("minLength") == null || GetFormData("minLength") == "") ? -1 : Convert.ToInt32(GetFormData("minLength"));
                attInfo.FieldType = GetFormData("fieldType");
                attInfo.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));

                attInfo.DefaultValue = (attInfo.FieldType == "string" || attInfo.FieldType == "decimal" || attInfo.FieldType == "text") ? GetFormData("defaultValue1") : GetFormData("defaultValue2");
                attInfo.GroupId = GetFormData("groupName");
                attInfo.IsDisplay = (GetFormData("isdisplay") == "0") ? 0 : 1;
                attInfo.Status = (GetFormData("Status") == "0") ? 0 : 1;
                attInfo.Description = GetFormData("attDesc");
                oldName = GetFormData("tableName");
            }
            catch (Exception ex)
            {
                return FailedJson("操作异常，请检查输入项，" + ex.Message);
            }


            if (CustomerExtAttributesInfoService.Instance.UpdateCustomerAttribute(attInfo, out message, oldName))
            {
                return SuccessedJson(message, "CallCenter_CustomerAttributeMgr", "CallCenter_CustomerAttributeMgr", "closeCurrent", "/CallCenter/CustomerAttributeMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }


        

        /// <summary>
        /// 新建客户备注视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewCustomerMemo()
        {
            return View();
        }

        /// <summary>
        /// 新建客户联系记录视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewCustomerContact()
        {
            return View();
        }

        ///// <summary>
        ///// 新增客户档案视图。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult NewCustomer()
        //{
        //    return View();
        //}

        /// <summary>
        /// 新建客户信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
      
        public JsonResult DoAddCustomerInfo()
        {

            CustomerBasicInfoModel model = new CustomerBasicInfoModel();
            model.CustomerCode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            model.CustomerName = GetFormData("customer_name");
            model.Sex = Convert.ToInt32(GetFormData("sex"));
            model.SalesFrom = GetFormData("salesFrom");
            if (string.IsNullOrEmpty(model.SalesFrom))
            {
                return FailedJson("请选择客户来源");
            }
            model.Level = GetFormData("level");
            if (string.IsNullOrEmpty(model.Level))
            {
                return FailedJson("请选择客户等级");
            }
            model.ComeFrom = GetFormData("comefrom");
            model.MobilePhone = GetFormData("mobile_phone");
            model.HomePhone = GetFormData("home_phone");
            model.OtherPhone = GetFormData("other_phone");
            if (string.IsNullOrEmpty(model.MobilePhone) && string.IsNullOrEmpty(model.HomePhone) && string.IsNullOrEmpty(model.OtherPhone))
            {
                return FailedJson("客户联系号码必须填写至少一项");
            }

            model.Carriers = GetFormData("carriers");
            if (string.IsNullOrEmpty(model.Carriers))
            {
                model.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            }

            model.UsingPhoneBrand = GetFormData("using_phone_brand");
            if (string.IsNullOrEmpty(model.UsingPhoneBrand))
            {
                model.UsingPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            }

            model.UsingPhoneType = GetFormData("using_phone_type");
            model.CommunicationConsumer = GetFormData("communication_consumer");
            if (string.IsNullOrEmpty(model.CommunicationConsumer))
            {
                model.CommunicationConsumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            }

            model.PreferredPhoneBrand = GetFormData("preferred_phone_brand");
            if (string.IsNullOrEmpty(model.PreferredPhoneBrand))
            {
                model.PreferredPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            }

            model.UsingSmartphone = Convert.ToInt32(GetFormData("using_smartphone"));
            if (model.UsingSmartphone == null)
            {
                model.UsingSmartphone = 3;
            }
            model.MobilePhonePrice = GetFormData("mobile_phone_price");
            if (string.IsNullOrEmpty(model.MobilePhonePrice))
            {
                model.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            }

            if (!string.IsNullOrEmpty(GetFormData("birthday")))
            {
                model.Birthday = Convert.ToDateTime(GetFormData("birthday"));
            }
            model.CreatedOn = DateTime.Now;
            model.CreatedBy = SessionUtil.Current.CnName;
            if (!string.IsNullOrEmpty(GetFormData("chinaId")))
            {
                model.ChinaId = Convert.ToInt32(GetFormData("chinaId"));
            }

            string message = "";


            if (CustomerBasicInfoService.Instance.CreateCustomeBasicInfo(model, out message))
            {
                switch (GetQueryString("page"))
                {
                    case "customermgr77":
                        return SuccessedJson(message, "CallCenter_CustomerMgrForGroup77", "CallCenter_CustomerMgrForGroup77", "closeCurrent", "/CallCenter/CustomerMgrForGroup77");

                    case "customermgr":
                        return SuccessedJson(message, "CallCenter_CustomerMgr", "CallCenter_CustomerMgr", "closeCurrent", "/CallCenter/CustomerMgr");                
                }
                return SuccessedJson(message, "CallCenter_CustomerMgr", "CallCenter_CustomerMgr", "closeCurrent", "/CallCenter/CustomerMgr");
            }

            return FailedJson(message);
        }

        #region 我的营销

        /// <summary>
        /// 客户基本信息视图。
        /// </summary>
        /// <returns></returns>
        
        public ActionResult CustomerInfo()
        {
            InitCustomerBasicInfo();
            return View();
        }


        ///// <summary>
        ///// 我的营销视图。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult MySales()
        //{
        //    //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\产品信息导入表.xls");

        //    //string importLogs = null, message = null;
        //    //ProductInfoService.Instance.BatchDeleteAllProductCategories();
        //    //ProductInfoService.Instance.ImportProductCategories(ds.Tables["产品类型信息表"], out importLogs, out message);
        //    //ProductInfoService.Instance.ImportProductCategoryAttributes(ds.Tables["产品类型分组属性信息表"], out importLogs, out message);
        //    //ProductInfoService.Instance.ImportProductInfoList(ds, out importLogs, out message);

        //    return View();
        //}

        /// <summary>
        /// 首次来电记录客户信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult FirstInComeCall()
        {
            InitCustomerBasicInfo();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult IncomeCallForGroup77()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult CustomerMgrForGroup77()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string salesFromId = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户来源", false).GetCustomDataValueDomainByDataValue("40077项目").ValueId; // "934dfe2a-dd2e-46b3-aabc-41606210787d"; GetFormData("salesFrom");
            //if (salesFromId != "All" && salesFromId != null)
            //{
            queryItem = new QueryItemDomainModel();
            queryItem.FieldType = "sales_from";
            queryItem.SearchValue = salesFromId;
            queryItem.Operation = "equal";

            queryCollection[queryItem.FieldType] = queryItem;
            //}

            string customer_name = GetFormData("customer_name_out");
            if (string.IsNullOrEmpty(customer_name) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_name";
                queryItem.SearchValue = customer_name;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string phoneNumber = GetFormData("phone_number");
            if (string.IsNullOrEmpty(phoneNumber) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "phone_number";
                queryItem.SearchValue = phoneNumber;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            if (GetFormData("sel_customer_name") == "0")
            {
                queryCollection.Remove("customer_name");
            }
            if (GetFormData("sel_mobile_phone") == "0")
            {
                queryCollection.Remove("mobile_phone");
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.CustomerIdList = CustomerInfoService.Instance.GetCustomerIdList(queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.CustomerTotal = total;

            return View();
        }


        /// <summary>
        /// 处理首次来电。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoCreateFirstIncomeCall()
        {
            string incomeCallNumber = GetQueryString("income");
            string calledNumber = GetQueryString("call");
            string newCustomerId = "";
            bool isFirstIncomeCall = false;
            CustomerInfoService.Instance.ProcessInComeCall(incomeCallNumber, calledNumber, out newCustomerId, out isFirstIncomeCall);

            if (UserInfoService.Instance.CheckInProjectGroup77())
            {
                return SuccessedJson("成功处理来电号码", "", "", "closeCurrent", "", new { ProjectGroupName = "40077项目组", CustomerId = newCustomerId, DataTag= string.Format("{0}_{1}",newCustomerId, DateTime.Now.ToString("yyyyMMddHHmmssfff")),  InComeNumber = incomeCallNumber, IsFirstIncomeCall = isFirstIncomeCall });
            }

            if (isFirstIncomeCall == true)
            {
                return SuccessedJson("成功处理来电号码", "", "", "closeCurrent", "", new { CustomerId = newCustomerId, InComeNumber = incomeCallNumber, IsFirstIncomeCall = isFirstIncomeCall });
            }
            else
            {
                return SuccessedJson("成功处理来电号码", newCustomerId, newCustomerId, "forward", "/CallCenter/CustomerInfo?cid=" + newCustomerId, new { InComeNumber = incomeCallNumber, IsFirstIncomeCall = isFirstIncomeCall, Title = string.Format("新进客户来电【{0}】", incomeCallNumber), CustomerId = newCustomerId, Fresh = true });
            }
        }

        /// <summary>
        /// 处理外呼操作。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ProcessOutCallInfo()
        {
            string calledNumber = GetQueryString("call");
            string newCustomerId = GetQueryString("cid");

            if (UserInfoService.Instance.CheckInProjectGroup77())
            {
                ViewBag.CustomerId = newCustomerId; // +"_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                return View("OutCallProcessForGroup77");
            }

            return View("NewCustomerContact");
        }

        [AuthorizeFlag]
        public ActionResult OutCallProcessForGroup77()
        {
            return View();
        }

        /// <summary>
        /// 处理首次来电操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateFirstInComeInfo()
        {
            string customerId = GetFormData("customerId");
            string customerMemo = GetFormData("inCallDesc");
            

            CustomerDomainModel customerInfo = CustomerInfoService.Instance.GetCustomerDomainModelById(customerId, false);
            if (customerInfo == null)
            {
                return FailedJson("操作失败，客户信息不存在");
            }

            UpdateCustomerBasicInfo(customerInfo.BasicInfo);

            string message = "操作失败，请与管理员联系";

            if (CustomerInfoService.Instance.UpdateFirstInComeCall(customerInfo.BasicInfo, customerMemo, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerInfo", "CallCenter_CustomerInfo", "forward", "/CallCenter/CustomerInfo?cid=" + customerId, new { Title = string.Format("客户【{0}】", customerInfo.BasicInfo.CustomerName), CustomerId = customerId, Fresh = true });
            }
            else
            {
                return FailedJson(message);
            }
        }


        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewCustomerWorkOrder()
        {
            string message = "操作失败，请与管理员联系";
                        
            WorkorderInfoModel workOrderInfo = new WorkorderInfoModel();
            string customerId = GetFormData("customerId");
            workOrderInfo.WorkorderType = GetFormData("workordertype");
            if (string.IsNullOrEmpty(workOrderInfo.WorkorderType) || workOrderInfo.WorkorderType == "All")
            {
                return FailedJson("参数错误，请选择当前工单的类型(子级分类)");
            }

            workOrderInfo.Description = GetFormData("workOrderDesc");
            if (string.IsNullOrEmpty(GetFormData("proimary")))
            {
                return FailedJson("参数错误，请选择紧急程度");
            }
            workOrderInfo.Level = GetFormData("proimary");
            workOrderInfo.NowStatusId = GetFormData("workTypeStatus");
            workOrderInfo.NowResultId = GetFormData("workTypeResult");
            workOrderInfo.RelCustomerId = customerId;
            workOrderInfo.RelOrderId = GetFormData("relorderDesc");
            if (Request.Form["userGroupList"] != null)
            {
                workOrderInfo.RelUsergroupId = Request.Form.GetValues("userGroupList")[0];

                if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
                {
                    workOrderInfo.NowProcessUserid = Request.Form.GetValues("groupUserList")[0];
                }
            }
            else
            {
                workOrderInfo.RelUsergroupId = SessionUtil.Current.UserGroup[0];
                workOrderInfo.NowProcessUserid = SessionUtil.Current.UserId;
            }
            if (!string.IsNullOrEmpty(GetFormData("advanceTime")))
            {
                workOrderInfo.AdvanceTime = Convert.ToDateTime(GetFormData("advanceTime"));
            }

            if (!string.IsNullOrEmpty(GetFormData("expiredTime")))
            {
                workOrderInfo.ExpiredTime = Convert.ToDateTime(GetFormData("expiredTime"));
            }
            
            CustomerContactInfoModel contactInfo = null;
            if (GetFormData("hasContactRecords") == "on")
            {
                contactInfo = new CustomerContactInfoModel();
                contactInfo.CustomerId = customerId;
                contactInfo.CustomerPhone = (string.IsNullOrEmpty(GetFormData("otherNumber"))) ? GetFormData("contactPhone") : GetFormData("otherNumber");
                if (contactInfo.CustomerPhone == "")
                {
                    return FailedJson("参数错误，请填写联系记录的联系号码");
                }
                contactInfo.Description = workOrderInfo.Description;
                contactInfo.Directions = (GetFormData("calldirection") == "1") ? 1 : 2;
                contactInfo.Purpose = GetFormData("purpose");
                contactInfo.Results = GetFormData("result");
            }

            if (WorkorderInfoService.Instance.CreateNewWorkOrder(workOrderInfo,contactInfo, out message))
            {
                return SuccessedJson(message, customerId, customerId, "closeCurrent", "/callcenter/customerinfo?cid=" + customerId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 创建客户备注信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewCustomerMemoInfo()
        {
            string message = "操作失败，请与管理员联系";
            CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
            memoInfo.CustomerId = GetFormData("customerId");
            memoInfo.Memo = GetFormData("memoDesc");
            if (string.IsNullOrEmpty(memoInfo.Memo))
            {
                return FailedJson("请填写客户备注信息内容。");
            }

            if (CustomerMemoInfoService.Instance.CreateMemoInfo(memoInfo, out message))
            {
                return SuccessedJson(message, memoInfo.CustomerId, memoInfo.CustomerId, "closeCurrent", "/callcenter/customerinfo?cid=" + memoInfo.CustomerId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 创建客户联系记录信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewCustomerContactInfo()
        {
            string message = "操作失败，请与管理员联系";
            CustomerContactInfoModel contactInfo = new CustomerContactInfoModel();
            contactInfo.Purpose = GetFormData("purpose");
            contactInfo.Results = GetFormData("results");
            contactInfo.CustomerId = GetFormData("customerId");
            
            contactInfo.CustomerPhone = GetFormData("otherNumber");
            if (string.IsNullOrEmpty(contactInfo.CustomerPhone))
            {
                contactInfo.CustomerPhone = GetFormData("contactPhone");
            }
            contactInfo.Description = GetFormData("contactDesc");
            contactInfo.Directions = Convert.ToInt32(GetFormData("direction"));
            contactInfo.Status = 0;

            if (string.IsNullOrEmpty(contactInfo.Description))
            {
                return FailedJson("请填写联系记录备注信息。");
            }
            
            if (CustomerContactInfoService.Instance.CreateContactInfo(contactInfo, out message))
            {
                return SuccessedJson(message, contactInfo.CustomerId, contactInfo.CustomerId, "closeCurrent", "/callcenter/customerinfo?cid=" + contactInfo.CustomerId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        

        protected void UpdateCustomerBasicInfo(CustomerBasicInfoModel basicInfo)
        {
            if (basicInfo != null)
            {
                basicInfo.CustomerName = GetFormData("customerName");
                basicInfo.Sex = Convert.ToInt32(GetFormData("sex"));
                basicInfo.SalesFrom = GetFormData("salesFrom");
                basicInfo.Level = GetFormData("level");
                basicInfo.ComeFrom = GetFormData("comefrom");
                basicInfo.MobilePhone = GetFormData("mobilePhone");
                basicInfo.HomePhone = GetFormData("homePhone");
                basicInfo.OtherPhone = GetFormData("otherPhone");
                basicInfo.Carriers = GetFormData("carriers");
                basicInfo.UsingPhoneBrand = GetFormData("phoneBrand");
                basicInfo.UsingPhoneType = GetFormData("phoneType");
                basicInfo.CommunicationConsumer = GetFormData("consumer");
                basicInfo.PreferredPhoneBrand = GetFormData("prePhoneBrand");
                basicInfo.UsingSmartphone = Convert.ToInt32(GetFormData("isSmartphone"));
                basicInfo.MobilePhonePrice = GetFormData("mobilePhonePrice");

                if(!string.IsNullOrEmpty(GetFormData("chinaId")))
                {
                    basicInfo.ChinaId = Convert.ToInt32(GetFormData("chinaId"));
                }

                if(!string.IsNullOrEmpty(GetFormData("callStatus")))
                {
                    basicInfo.CallStatus = (GetFormData("callStatus") == "0") ? 0 : 1;
                }
            }
        }

        #endregion

        #region 客户信息管理

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoChangeCustomerCallBack()
        {
            string customerId = GetQueryString("cid");
            string op = GetQueryString("op");

            string message = "操作失败，请与管理员联系";

            if (CustomerInfoService.Instance.ChangeCustomerCallBackInfo(customerId, op, out message))
            {
                return SuccessedJson(message, customerId, customerId, "forward", "/CallCenter/CustomerInfo?cid=" + customerId);
            }

            return FailedJson(message);
        }

        /// <summary>
        /// 获取用户组所有用户JSON对象。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public JsonResult GetCustomerSecurityInfoJson()
        {
            string securityCode = GetQueryString("cid");
            CustomerSecurityInfoDomainModel model = CustomerInfoService.Instance.GetCustomerSecurityInfo(securityCode,true);

            return Json(model);
        }

        [AuthorizeFlag]
        public ActionResult CustomerMgr()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string callBack = GetFormData("callBack");
            if (!string.IsNullOrEmpty(callBack))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "call_status";
                queryItem.SearchValue = callBack;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string salesFromId = GetFormData("salesFrom");
            if (salesFromId != "All" && salesFromId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "sales_from";
                queryItem.SearchValue = salesFromId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string createdBeginTime = GetFormData("createdBeginTime");
            string createdEndTime = GetFormData("createdEndTime");

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.created_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.created_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.created_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string customer_name = GetFormData("customer_name_out");
            if (string.IsNullOrEmpty(customer_name) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.customer_name";
                queryItem.SearchValue = customer_name;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string phoneNumber = GetFormData("phone_number");
            if (string.IsNullOrEmpty(phoneNumber) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.phone_number";
                queryItem.SearchValue = phoneNumber;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 订单创建人
            string createdUserName = GetFormData("createdUserName");
            if (!string.IsNullOrEmpty(createdUserName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = " create_userinfo.work_id";
                queryItem.SearchValue = "WORKID_" + createdUserName;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_","");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_","op_"));
                        
                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            if (GetFormData("sel_customer_name") == "0")
            {
                queryCollection.Remove("customer_name");
            }
            if (GetFormData("sel_mobile_phone") == "0")
            {
                queryCollection.Remove("mobile_phone");
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;
            ViewBag.CustomerIdList = CustomerInfoService.Instance.GetCustomerIdList(queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.CustomerTotal = total;

            return View();
        }



        /// <summary>
        /// 执行更新客户基本信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]

        public JsonResult DoUpdateCustomerBasicInfo()
        {
            string customerId = GetFormData("customerId");

            CustomerBasicInfoModel customerInfo = new CustomerBasicInfoModel();
            customerInfo.CustomerId = customerId;
            UpdateCustomerBasicInfo(customerInfo);

            string message = "操作失败，请与管理员联系";

            if (CustomerInfoService.Instance.UpdateCustomerBasicInfo(customerInfo, out message))
            {
                return SuccessedJson(message, customerId, customerId, "forward", "/CallCenter/CustomerInfo?cid=" + customerId, new { Title = string.Format("客户【{0}】", customerInfo.CustomerName), CustomerId = customerId, Fresh = true });
            }
            else
            {
                return FailedJson(message);
            }
        }

        #endregion

        #region 客户信息修改审批

        /// <summary>
        /// 客户信息审核视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult CustomerInfoApproval()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult CustomerApprovalMgr()
        {
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();


            QueryItemDomainModel queryItem = null;
            string approvalStatus = GetFormData("approvalStatus");
            if (string.IsNullOrEmpty(approvalStatus))
            {
                approvalStatus = "0";
            }

            string createdBeginTime = Request["createdBeginTime"];
            string createdEndTime = Request["createdEndTime"];

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "modified_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "modified_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "modified_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }


            ViewBag.ApprovalStatus = approvalStatus;

            InitPagerForm();

            int total = 0;
            ViewBag.ApprovalList = CustomerInfoApprovalService.Instance.GetApprovalList(queryCollection,approvalStatus, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.ApprovalTotal = total;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;

            return View();
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAcceptCustomerApproval()
        {
            List<string> ids = Request.Form.GetValues("ids").ToList();
            string message = "操作失败，请与管理员联系";
            string description = GetFormData("approvalDesc");

            if(CustomerInfoApprovalService.Instance.UpdateCustomerApprovalStatus(ids,description, 2, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerApprovalMgr", "CallCenter_CustomerApprovalMgr", "closeCurrent", "/CallCenter/CustomerApprovalMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoRefuseCustomerApproval()
        {
            List<string> ids = Request.Form.GetValues("ids").ToList();
            string message = "操作失败，请与管理员联系";
            string description = GetFormData("approvalDesc");

            if (CustomerInfoApprovalService.Instance.UpdateCustomerApprovalStatus(ids, description,1, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerApprovalMgr", "CallCenter_CustomerApprovalMgr", "forward", "/CallCenter/CustomerApprovalMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoChangeCustomerApproval()
        {
            List<string> ids = Request.Form.GetValues("ids").ToList();
            string message = "操作失败，请与管理员联系";
            string description = GetFormData("approvalDesc");
            int status = (GetFormData("approvalAction") == "accept") ? 2 : 1;

            if (CustomerInfoApprovalService.Instance.UpdateCustomerApprovalStatus(ids, description, status, out message))
            {
                return SuccessedJson(message, "CallCenter_CustomerApprovalMgr", "CallCenter_CustomerApprovalMgr", "closeCurrent", "/CallCenter/CustomerApprovalMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteCustomerApproval()
        {
            return FailedJson("本功能暂不实现");
        }

        #endregion

        #region 客户持卡信息

     
        /// <summary>
        /// 新增客户持卡信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewCreditCardInfo()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult EditCreditCardInfo()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateCreditCardInfo()
        {
            string message = "操作失败，请与管理员联系";

            CustomerCreditcardInfoModel creditInfo = new CustomerCreditcardInfoModel();
            creditInfo.CreditcardId = GetFormData("creditCardId");
            creditInfo.CustomerId = GetFormData("customerId");
            creditInfo.Bank = GetFormData("openbank");
            creditInfo.BillAddress = GetFormData("billAddress");
            if (string.IsNullOrEmpty(GetFormData("chinaId")) == false)
            {
                creditInfo.BillChinaId = Convert.ToInt32(GetFormData("chinaId"));
            }

            creditInfo.BillZipcode = GetFormData("postCode");
            creditInfo.CardUsername = GetFormData("cardUsername");
            creditInfo.IdcardType = GetFormData("idcardType");
            creditInfo.OpeningAddress = GetFormData("openAddress");

            creditInfo.IvrDataId = GetFormData("timeTag");

            if (CustomerCreditcardInfoService.Instance.UpdateCreditcardInfo(creditInfo, out message))
            {
                return SuccessedJson(message, creditInfo.CustomerId, creditInfo.CustomerId, "closeCurrent", "/CallCenter/customerinfo?cid=" + creditInfo.CustomerId);
            }

            return FailedJson(message);
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewCreditCardInfo()
        {
            string message = "操作失败，请与管理员联系";

            CustomerCreditcardInfoModel creditInfo = new CustomerCreditcardInfoModel();
            creditInfo.CustomerId = GetFormData("customerId");
            creditInfo.Bank = GetFormData("openbank");
            creditInfo.BillAddress = GetFormData("billAddress");
            creditInfo.InfoType = GetFormData("infoType");
            if (string.IsNullOrEmpty(GetFormData("chinaId")) == false)
            {
                creditInfo.BillChinaId = Convert.ToInt32(GetFormData("chinaId"));
            }

            creditInfo.BillZipcode = GetFormData("postCode");
            creditInfo.CardUsername = GetFormData("cardUsername");
            creditInfo.IdcardType = GetFormData("idcardType");
            creditInfo.OpeningAddress = GetFormData("openAddress");

            string securityCode = GetFormData("timeTag");

            if (CustomerCreditcardInfoService.Instance.CreateCreditcardInfo(creditInfo, securityCode, out message))
            {
                return SuccessedJson(message, creditInfo.CustomerId, creditInfo.CustomerId, "closeCurrent", "/CallCenter/customerinfo?cid=" + creditInfo.CustomerId, new { CustomerId = creditInfo.CustomerId, CreditCardId = creditInfo.CreditcardId });
            }

            return FailedJson(message);
        }

     

        #endregion

        #region 客户配送信息

        /// <summary>
        /// 新增客户配送信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewDeliveryInfo()
        {
            return View();
        }

      
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewDeliveryInfo()
        {
            string message = "操作失败，请与管理员联系";

            CustomerDeliveryInfoModel deliveryInfo = new CustomerDeliveryInfoModel();
            deliveryInfo.BillTitle = GetFormData("billTitle");
            deliveryInfo.Consignee = GetFormData("consignee");
            deliveryInfo.ConsigneePhone = (GetFormData("contactPhone") == "OtherNumber") ? GetFormData("otherNumber") : GetFormData("contactPhone");
            deliveryInfo.CustomerId = GetFormData("customerId");
            deliveryInfo.DeliveryAddress = GetFormData("deliveryAddress");
            deliveryInfo.DeliveryRegionId = Convert.ToInt32(GetFormData("chinaId"));
            deliveryInfo.DeliveryType = Convert.ToInt32(GetFormData("deliveryType"));
            deliveryInfo.NeedBills = (GetFormData("needBill") == null) ? 0 : 1;
            deliveryInfo.PostCode = GetFormData("postCode");
            deliveryInfo.Status = 0;

            if (CustomerDeliveryInfoService.Instance.CreateCustomerDeliveryInfo(deliveryInfo, out message))
            {
                return SuccessedJson(message, deliveryInfo.CustomerId, deliveryInfo.CustomerId, "closeCurrent", "/CallCenter/customerinfo?cid=" + deliveryInfo.CustomerId, new { DeliveryId = deliveryInfo.DeliveryId, CustomerId = deliveryInfo.CustomerId });
            }

            return FailedJson(message);
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateDeliveryInfo()
        {
            string message = "操作失败，请与管理员联系";

            return FailedJson(message);
        }

        #endregion

          
        ///// <summary>
        ///// 客户分配管理视图。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Distribution()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 客户级别管理视图。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Level()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// 客户CallBack任务列表。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult CallBackList()
        //{
        //    return View();
        //}

        /// <summary>
        /// 号码查询视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberQuery()
        {
            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\产品信息导入表.xls");
            //string importLogs = null, message = null;
            //ProductInfoService.Instance.BatchDeleteAllProductCategories();
            //ProductInfoService.Instance.ImportProductCategories(ds.Tables["产品类型信息表"], out importLogs, out message);
            //ProductInfoService.Instance.ImportProductCategoryAttributes(ds.Tables["产品类型分组属性信息表"], out importLogs, out message);

            return View();
        }


        /// <summary>
        /// 订单查询视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderQuery()
        {
            return View();
        }

        
        /// <summary>
        /// 工单记录列表视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewWorkOrder()
        {
            return View();
        }

        /// <summary>
        /// 新建手机销售订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewPhoneSaleOrder()
        {
            return View();
        }

        ///// <summary>
        ///// 新建客户联系记录视图。
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult AddContactRecord()
        //{
        //    return View();
        //}


        #region 自动外呼管理

        [AuthorizeFlag]
        public ActionResult AutoOutDialerNumberMgr()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult NewAutoDialerTask()
        {
            return View();
        }

        /// <summary>
        /// 创建自动外呼任务操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewAutoDialerTask()
        {
            AutoDialerTaskInfoModel taskInfo = new AutoDialerTaskInfoModel();
            taskInfo.AutoDialerTaskName = GetFormData("taskName");
            taskInfo.BeginTime = Convert.ToDateTime(GetFormData("beginTime"));
            taskInfo.EndTime = Convert.ToDateTime(GetFormData("endTime"));

            taskInfo.StartTime1 = (string.IsNullOrEmpty(GetFormData("startTime1"))) ? "NONE" : GetFormData("startTime1");
            taskInfo.StartTime2 = (string.IsNullOrEmpty(GetFormData("startTime2"))) ? "NONE" : GetFormData("startTime2");
            taskInfo.StartTime3 = (string.IsNullOrEmpty(GetFormData("startTime3"))) ? "NONE" : GetFormData("startTime3");
            taskInfo.StartTime4 = (string.IsNullOrEmpty(GetFormData("startTime4"))) ? "NONE" : GetFormData("startTime4");
            taskInfo.StopTime1 = (string.IsNullOrEmpty(GetFormData("stopTime1"))) ? "NONE" : GetFormData("stopTime1");
            taskInfo.StopTime2 = (string.IsNullOrEmpty(GetFormData("stopTime2"))) ? "NONE" : GetFormData("stopTime2");
            taskInfo.StopTime3 = (string.IsNullOrEmpty(GetFormData("stopTime3"))) ? "NONE" : GetFormData("stopTime3");
            taskInfo.StopTime4 = (string.IsNullOrEmpty(GetFormData("stopTime4"))) ? "NONE" : GetFormData("stopTime4");

            taskInfo.Interval = Convert.ToInt32(GetFormData("Interval"));
            taskInfo.RetryCount = Convert.ToInt32(GetFormData("tryCount"));
            taskInfo.Priority = Convert.ToInt32(GetFormData("priority"));
            taskInfo.Description = GetFormData("taskDesc");
            string message = "操作失败，请与管理员联系";

            if (AutoDialerTaskInfoService.Instance.CreateNewAutoDialerTask(taskInfo, out message))
            {
                return SuccessedJson(message, "autoDialerTaskBox", "", "closeCurrent", "/CallCenter/AutoDialerTaskMgr?=" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            }
            else
            {
                return FailedJson(message);
            }
        }

        [AuthorizeFlag]
        public JsonResult DoUpdateAutoDialerTask()
        {
            string message = "操作失败，请与管理员联系";
            AutoDialerTaskInfoModel taskInfo = new AutoDialerTaskInfoModel();
            taskInfo.AutoDialerTaskId = GetFormData("taskId");
            taskInfo.IvrDialerProjectId = Convert.ToInt32(GetFormData("ivrProjectId"));
            taskInfo.AutoDialerTaskName = GetFormData("taskName");
            taskInfo.BeginTime = Convert.ToDateTime(GetFormData("beginTime"));
            taskInfo.EndTime = Convert.ToDateTime(GetFormData("endTime"));

            taskInfo.StartTime1 = (string.IsNullOrEmpty(GetFormData("startTime1"))) ? "NONE" : GetFormData("startTime1");
            taskInfo.StartTime2 = (string.IsNullOrEmpty(GetFormData("startTime2"))) ? "NONE" : GetFormData("startTime2");
            taskInfo.StartTime3 = (string.IsNullOrEmpty(GetFormData("startTime3"))) ? "NONE" : GetFormData("startTime3");
            taskInfo.StartTime4 = (string.IsNullOrEmpty(GetFormData("startTime4"))) ? "NONE" : GetFormData("startTime4");
            taskInfo.StopTime1 = (string.IsNullOrEmpty(GetFormData("stopTime1"))) ? "NONE" : GetFormData("stopTime1");
            taskInfo.StopTime2 = (string.IsNullOrEmpty(GetFormData("stopTime2"))) ? "NONE" : GetFormData("stopTime2");
            taskInfo.StopTime3 = (string.IsNullOrEmpty(GetFormData("stopTime3"))) ? "NONE" : GetFormData("stopTime3");
            taskInfo.StopTime4 = (string.IsNullOrEmpty(GetFormData("stopTime4"))) ? "NONE" : GetFormData("stopTime4");

            taskInfo.Interval = Convert.ToInt32(GetFormData("Interval"));
            taskInfo.RetryCount = Convert.ToInt32(GetFormData("tryCount"));
            taskInfo.Priority = Convert.ToInt32(GetFormData("priority"));
            taskInfo.Description = GetFormData("taskDesc");
            
            // List<string> numberList = GetFormData("numberList").Split('|').ToList();

            if (AutoDialerTaskInfoService.Instance.UpdateAutoDialerTask(taskInfo, out message))
            {
                return SuccessedJson(message, "autoDialerTaskBox", "", "closeCurrent", "/CallCenter/AutoDialerTaskMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 添加自动外呼任务外呼号码操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddAutoDialerTaskNumbers()
        {
            string message = "操作失败，请与管理员联系";
            List<string> AddNumbers = Request.Form.GetValues("AddNumbers[]").ToList();
            string taskId = GetFormData("TaskId");
            int nowTotal = 0;
            if (AutoDialerTaskInfoService.Instance.AddAutoDialerTaskNumbers(taskId, AddNumbers, out nowTotal, out message))
            {
                return Json(new { code = "ok", msg = message, total = nowTotal }); // SuccessedJson(message, "autoDialerTaskBox", "", "forward", "/CallCenter/AutoDialerTaskMgr");
            }
            else
            {
                return Json(new { code = "failed", msg = message });
            }
        }

        /// <summary>
        /// 删除自动外呼任务外呼号码操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteAutoDialerTaskNumbers()
        {
            string message = "操作失败，请与管理员联系";
            List<string> DelNumbers = Request.Form.GetValues("DelNumbers[]").ToList();
            string taskId = GetFormData("TaskId");
            int nowTotal = 0;
            if (AutoDialerTaskInfoService.Instance.DeleteAutoDialerTaskNumbers(taskId, DelNumbers, out nowTotal, out message))
            {
                return Json(new { code = "ok", msg = message, total = nowTotal }); // SuccessedJson(message, "autoDialerTaskBox", "", "forward", "/CallCenter/AutoDialerTaskMgr");
            }
            else
            {
                return Json(new { code = "failed", msg = message });
            }
        }

        /// <summary>
        /// 删除自动外呼任务操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteAutoDialerTask()
        {
            string message = "操作失败，请与管理员联系";
            string taskId = GetQueryString("aid");

            if (AutoDialerTaskInfoService.Instance.DeleteAutoDialerTask(taskId, out message))
            {
                return SuccessedJson(message, "autoDialerTaskBox", "", "forward", "/CallCenter/AutoDialerTaskMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新自动外呼任务状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateAutoDialerTaskStatus()
        {
            string message = "操作失败，请与管理员联系";
            string taskId = GetQueryString("aid");
            int taskStatus = Convert.ToInt32(GetQueryString("status"));

            if (AutoDialerTaskInfoService.Instance.UpdateAutoDialerTaskStatus(taskId, taskStatus, out message))
            {
                return SuccessedJson(message, "autoDialerTaskBox", "", "forward", "/CallCenter/AutoDialerTaskMgr");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 启动指定自动外呼任务操作。
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult DoRunAutoDialerTask()
        //{
        //    string message = "操作失败，请与管理员联系";
        //    string taskId = GetQueryString("aid");

        //    if (AutoDialerTaskInfoService.Instance.RunAutoDialerTask(taskId, out message))
        //    {
        //        return SuccessedJson(message, "autoDialerTaskBox", "", "forward", "/CallCenter/AutoDialerTaskMgr");
        //    }
        //    else
        //    {
        //        return FailedJson(message);
        //    }
        //}

        /// <summary>
        /// 更新自动外呼策略信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateAutoDialerCampaign()
        {
            string message = "操作失败，请与管理员联系";
            DialerInfoCampaignModel model = new DialerInfoCampaignModel();
            model.Times = Convert.ToInt32(GetFormData("timers"));
            model.Interval = Convert.ToInt32(GetFormData("interval"));
            model.Maxports = Convert.ToInt32(GetFormData("maxports"));

            string taskId = GetQueryString("aid");

            if (AutoDialerTaskInfoService.Instance.UpdateAutoDialerCampaign(model))
            {
                return SuccessedJson("成功更新自动外呼策略信息", "autoDialerCampaignBox", "", "forward", "/CallCenter/AutoOutDialerConfig");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [AuthorizeFlag]
        public ActionResult EditAutoDialerTask()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult AutoDialerTaskMgr()
        {
            GetAutoDialerInfo();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult AutoOutDialerResult()
        {
            GetAutoDialerInfo();
            return View();
        }

        [AuthorizeFlag]
        public ActionResult AutoOutDialerConfig()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult AutoDialerTaskResultDetails()
        {
            return View();
        }

        protected void GetAutoDialerInfo()
        {
            ViewBag.TaskStatus = GetFormData("taskStatus");
            ViewBag.TaskCode = GetFormData("taskcode");

            InitPagerForm();

            ViewBag.TaskTotal = AutoDialerTaskInfoService.Instance.GetGetAutoDialerTaskTotal();
            ViewBag.TaskList = AutoDialerTaskInfoService.Instance.GetAutoDialerTaskList(ViewBag.TaskCode, ViewBag.TaskStatus, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection);
        }

        #endregion
    }
}



