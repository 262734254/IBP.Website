/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;
using IBP.Models;
using System.Text;

namespace IBP.Services
{
	/// <summary>
	/// ProductInfo业务逻辑类
	/// </summary>
	public partial class ProductInfoService : BaseService
	{
		// 在此添加你的代码...

        public List<string> GetProductList(string productCategoryId, string productName, string statusName, int pageIndex, int pageSize, string orderField, string orderDirection, out int total, out string message)
        {
            List<string> result = new List<string>();
            message = "操作失败，请与管理员联系";
            total = 0;

            ParameterCollection pc = new ParameterCollection();


            string sql = "FROM product_info WHERE 1 = 1 ";
            
            if(!string.IsNullOrEmpty(productCategoryId))
            {
                sql += " AND category_id = $categoryId$ ";
                pc.Add("categoryId", productCategoryId);
            }

            if (!string.IsNullOrEmpty(statusName))
            {
                sql += " AND sales_status = $saleStatus$ ";
                pc.Add("saleStatus", statusName);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                sql += " AND product_name LIKE $productName$ ";
                pc.Add("productName", "%" + statusName + "%");
            }
            
            

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sql, pc));
            DataTable dt = ExecuteDataTable("SELECT product_id " + sql, pc, pageIndex, pageSize, OrderByCollection.Create(orderField, orderDirection));
            result = ModelConvertFrom(dt);


            return result;
        }

        public List<string> GetProductList(string productCategoryId, Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            List<string> result = new List<string>();
            total = 0;
            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(productCategoryId);
            if (catInfo == null)
            {
                Exception ex = new Exception("获取产品列表方法异常， 不存在的产品类型ID");
                LogUtil.Error("获取产品列表方法异常， 不存在的产品类型ID", ex);
                throw ex;
            }

            Dictionary<string, ProductCategoryAttributesModel> attList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(productCategoryId, false);
            if (attList == null)
            {
                Exception ex = new Exception("获取产品列表方法异常， 不存在的产品类型ID");
                LogUtil.Error("获取产品列表方法异常， 不存在的产品类型ID", ex);
                throw ex;
            }

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat(@"FROM [{0}] WHERE 1=1 ", catInfo.TableName);

            ParameterCollection pc = new ParameterCollection();

            int count = 0;

            foreach (KeyValuePair<string, QueryItemDomainModel> item in queryCollection)
            {
                switch (item.Value.Operation)
                {
                    case "equal":
                        sqlBuilder.AppendFormat(@" AND [{0}] = $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "notequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <> $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "contain":
                        sqlBuilder.AppendFormat(@" AND [{0}] LIKE $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), "%" + item.Value.SearchValue + "%");
                        break;

                    case "greater":
                        sqlBuilder.AppendFormat(@" AND [{0}] > $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "greaterequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] >= $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "less":
                        sqlBuilder.AppendFormat(@" AND [{0}] < $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "lessequal":
                        sqlBuilder.AppendFormat(@" AND [{0}] <= $value{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("value" + count.ToString(), item.Value.SearchValue);
                        break;

                    case "between":
                        sqlBuilder.AppendFormat(@" AND [{0}] BETWEEN $begin{1}$ AND $end{1}$", attList[item.Key].AttributeName, count);
                        pc.Add("begin" + count.ToString(), item.Value.BeginTime);
                        pc.Add("end" + count.ToString(), item.Value.EndTime);
                        break;

                    case "today":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(DAY,[{0}],GETDATE()) = 0", attList[item.Key].AttributeName);
                        break;

                    case "week":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(WEEK,[{0}],GETDATE()) = 0", attList[item.Key].AttributeName);
                        break;

                    case "month":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(MONTH,[{0}],GETDATE()) = 0", attList[item.Key].AttributeName);
                        break;

                    case "quarter":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(QUARTER,[{0}],GETDATE()) = 0", attList[item.Key].AttributeName);
                        break;

                    case "year":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(YEAR,[{0}],GETDATE()) = 0", attList[item.Key].AttributeName);
                        break;

                    default:
                        break;
                }

                
                count++;
            }

            string totalSql = string.Format(@"SELECT COUNT(1) {0} ", sqlBuilder.ToString());
            total = Convert.ToInt32(ExecuteScalar(totalSql, pc));

            DataTable dt = ExecuteDataTable("SELECT product_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create(orderField, orderDirection));
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i][0].ToString());
                }
            }

            return result;
        }


        public bool ChangeProductSaleStatus(string productCategoryId, string productId, string statusName)
        {
            bool result = false;

            ProductCategoryDomainModel category = GetProductCategoryDomainModelById(productCategoryId);
            if (category == null)
            {
                return false;
            }

            ProductInfoDomainModel product = GetProductDomainInfoByProductId(productId, false);
            if (product == null)
            {
                return false;
            }

            if (product.BasicInfo.SalesStatus == statusName)
            {
                return true;
            }

            product.BasicInfo.SalesStatus = statusName;
            //product.AttributeList[""] = statusName;

            try
            {
                BeginTransaction();

                if (ProductInfoService.Instance.Update(product.BasicInfo) != 1)
                {
                    RollbackTransaction();
                    return false;
                }

                string sql = string.Format(@"
UPDATE 
    [{0}]
SET 
    [销售状态] = $statusName$,
    [modified_on] = GETDATE(),
    [modified_by] = $modifiedBy$
WHERE 
    product_id = $productId$", category.BasicInfo.TableName);

                ParameterCollection pc = new ParameterCollection();
                pc.Add("statusName", statusName);
                pc.Add("modifiedBy", SessionUtil.Current.UserId);
                pc.Add("productId", product.BasicInfo.ProductId);

                if (DbUtil.IBPDBManager.IData.ExecuteNonQuery(sql, pc) == 0)
                {
                    RollbackTransaction();
                    return false;
                }

                CommitTransaction();
                GetProductDomainInfoByProductId(product.BasicInfo.ProductId, true);
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error(ex.Message, ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取产品信息领域模型。
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public ProductInfoDomainModel GetProductDomainInfoByProductId(string productId, bool clear)
        {
            if (string.IsNullOrEmpty(productId))
                return null;

            string cacheKey = CacheKey.PRODUCT_DOMAIN_MODEL.GetKeyDefine(productId);
            ProductInfoDomainModel info = CacheUtil.Get<ProductInfoDomainModel>(cacheKey);

            if (info == null || clear)
            {
                info = GetProductDomainInfoByProductIdFromDatabase(productId);
                if (info != null)
                {
                    CacheUtil.Set(cacheKey, info);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }

            return info;
        }

        public bool CreateProductInfo(ProductInfoDomainModel productInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            if (productInfo == null || productInfo.BasicInfo == null || productInfo.AttributeList == null)
            {
                message = "操作失败，产品参数不完整";
                return false;
            }

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(productInfo.BasicInfo.CategoryId);

            Dictionary<string, ProductCategoryAttributesModel> catAttList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(productInfo.BasicInfo.CategoryId, false);
            if (catAttList == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            string InsProductSQL = GetCreateProductSQL(catInfo, productInfo, catAttList);

            try
            {
                BeginTransaction();

                if (Create(productInfo.BasicInfo) == 1)
                {
                    ProductAttributesValueModel valueInfo = null;
                    foreach (KeyValuePair<string, string> item in productInfo.AttributeList)
                    {
                        valueInfo = new ProductAttributesValueModel();
                        valueInfo.ValueId = GetGuid();
                        valueInfo.AttributeId = item.Key;
                        valueInfo.AttributeValue = item.Value;
                        valueInfo.ProductCategoryId = productInfo.BasicInfo.CategoryId;
                        valueInfo.ProductId = productInfo.BasicInfo.ProductId;

                        if (ProductAttributesValueService.Instance.Create(valueInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，保存产品属性值失败";
                            return false;
                        }
                    }

                    if (ExecuteNonQuery(InsProductSQL) != 1)
                    {
                        RollbackTransaction();
                        message = "操作失败，插入产品信息表失败";
                        return false;
                    }

                    CommitTransaction();
                    message = "成功创建产品信息";
                    GetProductDomainInfoByProductId(productInfo.BasicInfo.ProductId, true);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建产品信息异常", ex);
                throw ex;
            }

            return result;
        }

        private string GetCreateProductSQL(ProductCategoryInfoModel catInfo, ProductInfoDomainModel productInfo, Dictionary<string, ProductCategoryAttributesModel> catAttList)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"INSERT INTO [{0}] ([product_id],[created_on],[created_by],[modified_on],[modified_by],[status_code]", catInfo.TableName);

            foreach (ProductCategoryAttributesModel item in catAttList.Values)
            {
                sql.AppendFormat(",[{0}]", item.AttributeName);
            }

            sql.AppendFormat(@") VALUES ('{0}',GETDATE(),'{1}',NULL,NULL, 0", productInfo.BasicInfo.ProductId, SessionUtil.Current.UserId);

            foreach (ProductCategoryAttributesModel item in catAttList.Values)
            {
                if (item.FieldType == "int" || item.FieldType == "decimal")
                {
                    sql.AppendFormat(", {0}", productInfo.AttributeList[item.CategoryAttributeId]);
                }
                else
                {
                    sql.AppendFormat(", '{0}'", productInfo.AttributeList[item.CategoryAttributeId]);
                }
            }

            sql.Append(");");

            return sql.ToString();
        }

        private ProductInfoDomainModel GetProductDomainInfoByProductIdFromDatabase(string productId)
        {
            ProductInfoDomainModel domainInfo = null;
            ProductInfoModel basicInfo = Retrieve(productId);
            if (basicInfo != null)
            {
                domainInfo = new ProductInfoDomainModel();
                domainInfo.BasicInfo = basicInfo;

                ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(basicInfo.CategoryId);
                if (catInfo == null)
                {
                    LogUtil.Debug(string.Format("产品表中存在未知类型ID的记录,CategoryId: {0}, ProductId: {1}",basicInfo.CategoryId, productId));
                    return null;
                }

                Dictionary<string, ProductCategoryAttributesModel> catAttList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(catInfo.ProductCategoryId, false);
                if (catAttList == null)
                {
                    LogUtil.Debug(string.Format("产品表中类型ID为{0}的记录无扩展属性", basicInfo.CategoryId, productId));
                    return null;
                }

                string getDetailSQL = string.Format("SELECT * FROM [{0}] WHERE product_id = $product_id$", catInfo.TableName);

                ParameterCollection pc = new ParameterCollection();
                pc.Add("product_id", basicInfo.ProductId);

                DataTable dt = ExecuteDataTable(getDetailSQL, pc);
                if (dt != null && dt.Rows.Count > 0)
                {
                    domainInfo.AttributeList = new Dictionary<string, string>();
                    foreach (ProductCategoryAttributesModel item in catAttList.Values)
                    {
                        domainInfo.AttributeList.Add(item.AttributeName, dt.Rows[0][item.AttributeName].ToString());
                    }

                    if (dt.Rows.Count > 1)
                    {
                        LogUtil.Debug(string.Format("产品表中存在相同ProductId的记录,CategoryId: {0}, ProductId: {1}", basicInfo.CategoryId, productId));
                        return null;
                    }
                }
            }

            return domainInfo;
        }

        public ProductCategoryDomainModel GetProductCategoryDomainModelByName(string categoryName)
        {
            Dictionary<string, ProductCategoryInfoModel> dict = ProductCategoryInfoService.Instance.GetProductCategoryList(false);
            foreach (ProductCategoryInfoModel catInfo in dict.Values)
            {
                if (catInfo.CategoryName == categoryName)
                {
                    return GetProductCategoryDomainModelById(catInfo.ProductCategoryId);
                }
            }

            return null;
        }

        public ProductCategoryDomainModel GetProductCategoryDomainModelById(string productCategoryId)
        {
            ProductCategoryDomainModel result = null;

            ProductCategoryInfoModel basicInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(productCategoryId);
             ProductCategoryGroupInfoModel groupInfo = null;

             if (basicInfo != null)
             {
                 groupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupById(basicInfo.GroupName);
             }

            Dictionary<string, ProductCategoryAttributesModel> AttributeList = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(productCategoryId, true);

            Dictionary<string, ProductCategorySalesStatusModel> SalestatusList = ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(productCategoryId, true);

            if (basicInfo == null || groupInfo == null || AttributeList == null || SalestatusList == null)
            {
                return null;
            }

            result = new ProductCategoryDomainModel();
            result.BasicInfo = basicInfo;
            result.GroupInfo = groupInfo;
            result.AttributeList = AttributeList;
            result.SalestatusList = SalestatusList;

            return result;
        }
	}
}

