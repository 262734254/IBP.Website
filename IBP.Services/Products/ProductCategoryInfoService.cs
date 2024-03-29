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

namespace IBP.Services
{
	/// <summary>
	/// ProductCategoryInfo业务逻辑类
	/// </summary>
	public partial class ProductCategoryInfoService : BaseService
	{
		// 在此添加你的代码...

        public ProductCategoryInfoModel GetProductCategoryInfoById(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                return null;
            }

            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            return (dict != null && dict.ContainsKey(categoryId)) ? dict[categoryId] : null;
        }

        public ProductCategoryInfoModel GetProductCategoryInfoBySortOrder(int sortOrder)
        {
            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict != null)
            {
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    if (item.SortOrder == sortOrder)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取产品分组信息列表。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public List<string> GetProductCategoryGroupList(bool clear)
        {
            string cacheKey = CacheKey.PRODUCT_CATEGORYGROUP_LIST;
            List<string> list = CacheUtil.Get<List<string>>(cacheKey);

            if (list == null || clear)
            {
                string sql = "SELECT group_name FROM product_category_info GROUP BY group_name ORDER BY group_name";

                DataTable dt = ExecuteDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<string>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(dt.Rows[i][0].ToString());
                    }

                    CacheUtil.Set(cacheKey, list);
                }
            }

            return list;
        }

        /// <summary>
        /// 创建产品型信息。
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CreateProductCategory(ProductCategoryInfoModel categoryInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (categoryInfo == null || string.IsNullOrEmpty(categoryInfo.CategoryName))
            {
                message = "缺少参数，请与管理员联系";
                return false;
            }

            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict != null)
            {
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    if (item.CategoryName == categoryInfo.CategoryName)
                    {
                        message = "操作失败，当前存在相同的产品类型名称";
                        return false;
                    }
                }
            }

            CustomDataDomainModel SaleCity = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);
            string saleCityString = null;
            foreach (CustomDataValueDomainModel item in SaleCity.ValueList.Values)
            {
                saleCityString += item.DataValue + " ";
            }

            categoryInfo.ProductCategoryId = GetGuid();
            categoryInfo.SortOrder = (dict == null) ? 1 : dict.Count + 1;
            categoryInfo.TableName = "product_info_" + CharacterUtil.ConvertToPinyin(categoryInfo.CategoryName);

            #region 创建表字段 

            List<FieldInfo> fieldList = new List<FieldInfo>();
            FieldInfo field = new FieldInfo();
            field.FieldName = "product_id";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = true;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "product_category_id";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "category_group_id";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "item_price";
            field.FieldType = "decimal";
            field.MinLength = 18;
            field.MaxLength = 2;
            field.DefaultValue = "0";
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "created_on";
            field.FieldType = "datetime";
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "created_by";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = false;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "modified_on";
            field.FieldType = "datetime";
            field.IsNull = true;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "modified_by";
            field.FieldType = "varchar";
            field.MinLength = 50;
            field.MaxLength = 50;
            field.IsNull = true;
            field.IsPrimaryKey = false;
            fieldList.Add(field);

            field = new FieldInfo();
            field.FieldName = "status_code";
            field.FieldType = "int";
            field.IsNull = true;
            fieldList.Add(field);

            string createTableSql = DTableUtil.GetCreateTableSQL(categoryInfo.TableName, fieldList);

            #endregion

            try
            {
                BeginTransaction();

                ExecuteNonQuery(createTableSql);

                if (Create(categoryInfo) == 1)
                {
                    GetProductCategoryList(true);

                    #region 创建默认字段 

                    ProductCategoryAttributesModel att = new ProductCategoryAttributesModel();
                    att.AttributeName = "产品代码";
                    att.CategoryAttributeId = GetGuid();
                    att.FieldMinLength = 50;
                    att.FieldType = "string";
                    att.IsDisplay = 0;
                    att.IsRequest = 0;
                    att.NodeId = 0;
                    att.ProductCategoryId = categoryInfo.ProductCategoryId;
                    att.SortOrder = 1;
                    att.Status = 0;
                    if (!ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(att, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);

                    att = new ProductCategoryAttributesModel();
                    att.AttributeName = "产品名称";
                    att.CategoryAttributeId = GetGuid();
                    att.FieldMinLength = 50;
                    att.FieldType = "string";
                    att.IsDisplay = 0;
                    att.IsRequest = 0;
                    att.NodeId = 0;
                    att.ProductCategoryId = categoryInfo.ProductCategoryId;
                    att.SortOrder = 2;
                    att.Status = 0;
                    if (!ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(att, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);

                    att = new ProductCategoryAttributesModel();
                    att.AttributeName = "销售状态";
                    att.CategoryAttributeId = GetGuid();
                    att.FieldMinLength = 50;
                    att.FieldType = "custom";
                    att.CustomValue = "已建档";
                    att.IsDisplay = 0;
                    att.IsRequest = 0;
                    att.NodeId = 0;
                    att.ProductCategoryId = categoryInfo.ProductCategoryId;
                    att.SortOrder = 3;
                    att.Status = 0;
                    if (!ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(att, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);

                    att = new ProductCategoryAttributesModel();
                    att.AttributeName = "销售城市";
                    att.CategoryAttributeId = GetGuid();
                    att.FieldMinLength = 50;
                    att.FieldType = "custom";
                    att.CustomValue = "所有";
                    att.IsDisplay = 0;
                    att.IsRequest = 0;
                    att.NodeId = 0;
                    att.ProductCategoryId = categoryInfo.ProductCategoryId;
                    att.SortOrder = 3;
                    att.Status = 0;
                    if (!ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(att, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);


                    att = new ProductCategoryAttributesModel();
                    att.AttributeName = "销售价格";
                    att.CategoryAttributeId = GetGuid();
                    att.FieldMinLength = 18;
                    att.FieldMaxLength = 2;
                    att.FieldType = "decimal";
                    att.CustomValue = "0";
                    att.IsDisplay = 0;
                    att.IsRequest = 0;
                    att.NodeId = 0;
                    att.ProductCategoryId = categoryInfo.ProductCategoryId;
                    att.SortOrder = 4;
                    att.Status = 0;
                    if (!ProductCategoryAttributesService.Instance.CreateProductCategoryAttribute(att, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);
      
                    #endregion

                    #region 创建默认的销售状态

                    ProductCategorySalesStatusModel saleStatus = new ProductCategorySalesStatusModel();
                    saleStatus.ProductCategoryId = categoryInfo.ProductCategoryId;
                    saleStatus.SalesStatusId = GetGuid();
                    saleStatus.SalestatusName = "已建档";
                    saleStatus.SortOrder = 1;
                    saleStatus.Status = 0;

                    if (!ProductCategorySalesStatusService.Instance.CreateProductCategorySaleStatus(saleStatus, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(categoryInfo.ProductCategoryId, true);

                    #endregion

                    #region 创建默认的销售城市

                    ProductCategorySalesStatusModel saleCityList = new ProductCategorySalesStatusModel();
                    saleCityList.ProductCategoryId = categoryInfo.ProductCategoryId;
                    saleCityList.SalesStatusId = GetGuid();
                    saleCityList.SalestatusName = "所有";
                    saleCityList.SortOrder = 1;
                    saleCityList.Status = 0;

                    if (!ProductCategorySalesStatusService.Instance.CreateProductCategorySaleStatus(saleCityList, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                    ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(categoryInfo.ProductCategoryId, true);

                    #endregion


                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryInfo.ProductCategoryId, true);
                    GetProductCategoryList(true);
                    message = "成功创建产品类型信息";
                    result = true;

                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    message = "创建产品类型信息失败";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建产品类型异常", ex);
                throw ex;
            }

            return result;
        }


        /// <summary>
        /// 更新产品信息。
        /// </summary>
        /// <param name="categoryInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateProductCategory(ProductCategoryInfoModel categoryInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (categoryInfo == null || string.IsNullOrEmpty(categoryInfo.CategoryName))
            {
                message = "缺少参数，请与管理员联系";
                return false;
            }

            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict.ContainsKey(categoryInfo.ProductCategoryId) == false)
            {
                message = "操作失败，产品类型ID不存在";
                return false;
            }

            foreach (ProductCategoryInfoModel item in dict.Values)
            {
                if (item.CategoryName == categoryInfo.CategoryName && item.ProductCategoryId != categoryInfo.ProductCategoryId)
                {
                    message = "操作失败，当前存在相同的产品类型名称";
                    return false;
                }
            }

            ProductCategoryInfoModel oldCatInfo = dict[categoryInfo.ProductCategoryId];

            try
            {
                BeginTransaction();

                if (oldCatInfo.CategoryName != categoryInfo.CategoryName)
                {
                    categoryInfo.TableName = "product_info_" + CharacterUtil.ConvertToPinyin(categoryInfo.CategoryName);
                    string renSQL = DTableUtil.GetRenameTableSQL(oldCatInfo.TableName, categoryInfo.TableName);
                    ExecuteNonQuery(renSQL);
                }

                if (Update(categoryInfo) == 1)
                {
                    CommitTransaction();
                    GetProductCategoryList(true);
                    message = "成功更新产品类型信息";
                    result = true;
                }
                else
                {
                    RollbackTransaction();
                    message = "更新产品类型信息失败";
                    result = true;
                }
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新产品类型信息异常", ex);
                throw ex;
            } 

            return result;
        }

        /// <summary>
        /// 删除产品类型信息。
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteProductCategory(string categoryId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (string.IsNullOrEmpty(categoryId))
            {
                message = "缺少参数，请与管理员联系";
                return false;
            }

            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict.ContainsKey(categoryId) == false)
            {
                message = "操作失败，产品类型ID不存在";
                return false;
            }

            ProductCategoryInfoModel oldCatInfo = dict[categoryId];

            try
            {
                BeginTransaction();

                string deleteCategoryAttributeSQL = "DELETE FROM product_category_attributes WHERE product_category_id = $categoryId$;";
                string deleteCategorySaleStatusSQL = "DELETE FROM product_category_sales_status WHERE product_category_id = $categoryId$;";
                string deleteCategoryAttributeValueSQL = "DELETE FROM product_attributes_value WHERE product_category_id = $categoryId$;";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("categoryId", categoryId);

                if (Delete(categoryId) == 1)
                {
                    ExecuteNonQuery(deleteCategoryAttributeSQL, pc);
                    ExecuteNonQuery(deleteCategorySaleStatusSQL, pc);
                    ExecuteNonQuery(deleteCategoryAttributeValueSQL, pc);

                    string dropTableSQL = DTableUtil.GetDropTableSQL(oldCatInfo.TableName);
                    ExecuteNonQuery(dropTableSQL);

                    foreach (ProductCategoryInfoModel item in dict.Values)
                    {
                        if (item.SortOrder > oldCatInfo.SortOrder)
                        {
                            item.SortOrder -= 1;
                            if (Update(item) != 1)
                            {
                                RollbackTransaction();
                                message = "重构产品类型信息排序索引失败";
                                result = false;
                            }
                        }
                    }

                    CommitTransaction();
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeList(categoryId, true);
                    ProductCategoryAttributesService.Instance.GetProductCategoryAttributeGroupList(categoryId, true);
                    ProductCategorySalesStatusService.Instance.GetProductCategorySalesStatusList(categoryId, true);
                    GetProductCategoryList(true);

                    message = "成功删除产品类型信息";
                    result = true;
                }
                else
                {
                    RollbackTransaction();
                    message = "删除产品类型信息失败";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除产品类型信息异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 从缓存中获取产品类别字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, ProductCategoryInfoModel> GetProductCategoryList(bool clear)
        {
            Dictionary<string, ProductCategoryInfoModel> dict = null;
            string cacheKey = CacheKey.PRODUCT_CATEGORY_DICT;

            dict = CacheUtil.Get<Dictionary<string, ProductCategoryInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetProductCategoryListFromDatabase();
                if (dict != null)
                {
                    CacheUtil.Set(cacheKey, dict);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }

            return dict;
        }

        /// <summary>
        /// 根据产品类型名称获取产品类型实体。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ProductCategoryInfoModel GetProductCategoryInfoByName(string name)
        {
            ProductCategoryInfoModel result = null;
            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict != null)
            {
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    if (item.CategoryName == name)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;            
        }

        public ProductCategoryInfoModel GetProductCategoryInfoByCategoryCode(string code)
        {
            ProductCategoryInfoModel result = null;
            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict != null)
            {
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    if (item.CategoryCode == code)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        public Dictionary<string, ProductCategoryInfoModel> GetProductCategoryListByGroupId(string categoryGroupId)
        {
            Dictionary<string, ProductCategoryInfoModel> result = null;
            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            if (dict != null)
            {
                result = new Dictionary<string, ProductCategoryInfoModel>();
                foreach (ProductCategoryInfoModel item in dict.Values)
                {
                    if (item.GroupName == categoryGroupId)
                    {
                        result[item.ProductCategoryId] = item;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从数据库获取产品类别字典。
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ProductCategoryInfoModel> GetProductCategoryListFromDatabase()
        {
            Dictionary<string, ProductCategoryInfoModel> dict = null;
            List<ProductCategoryInfoModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("sort_order", "asc"));
            if (list != null && list.Count > 0)
            {
                dict = new Dictionary<string, ProductCategoryInfoModel>();
                foreach (ProductCategoryInfoModel item in list)
                {
                    dict.Add(item.ProductCategoryId, item);
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取产品类别总数。
        /// </summary>
        /// <returns></returns>
        public int GetProductCategoryTotal()
        {
            Dictionary<string, ProductCategoryInfoModel> dict = GetProductCategoryList(false);

            return (dict == null) ? 0 : dict.Count;
        }


        /// <summary>
        /// 分页获取产品类别信息。
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public List<ProductCategoryInfoModel> GetProductCategoryListFromDatabase(string categoryName, string groupName, string saleCity, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            string sql = "";
            DataTable dt = null;
            List<ProductCategoryInfoModel> result = null;
            ParameterCollection pc = new ParameterCollection();
            OrderByCollection obc = OrderByCollection.Create(orderField, orderDirection);
            total = 0;

            sql = string.Format("FROM product_category_info WHERE 1 = 1 {0} {1} {2} ",
                string.IsNullOrEmpty(categoryName) ? "" : " AND category_name like $categoryName$ ",
                string.IsNullOrEmpty(groupName) ? "" : "AND group_name = $groupName$ ",
                string.IsNullOrEmpty(saleCity) ? "" : "AND (sale_city = $saleCity$ OR sale_city IS NULL  OR sale_city = '') ");

            if (!string.IsNullOrEmpty(categoryName))
            {
                pc.Add("categoryName","%" + categoryName + "%");
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                pc.Add("groupName", groupName);
            }

            if (!string.IsNullOrEmpty(saleCity))
            {
                pc.Add("saleCity", saleCity);
            }

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sql, pc));
            dt = ExecuteDataTable("SELECT * " + sql, pc, pageIndex, pageSize, obc);
            result = ModelConvertFrom<ProductCategoryInfoModel>(dt);

            return result;
        }

	}
}

