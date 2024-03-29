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
	/// ProductCategoryAttributes业务逻辑类
	/// </summary>
	public partial class ProductCategoryAttributesService : BaseService
	{
		// 在此添加你的代码...

        public bool MoveUpProductCategoryAttribute(string productCategoryId, string productAttributeId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategoryAttributesModel> dict =  GetProductCategoryAttributeList(productCategoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategoryAttributesModel currInfo = GetProductCategoryAttributeById(productCategoryId, productAttributeId);
            if (currInfo == null)
            {
                message = "操作失败，不存在的产品类型属性ID";
                return false;
            }

            if (currInfo.SortOrder == 1)
            {
                message = "操作失败，当前产品类型属性已经是最上序列";
                return false;
            }

            ProductCategoryAttributesModel upInfo = null;
            foreach (ProductCategoryAttributesModel item in dict.Values)
            {
                if (item.SortOrder == currInfo.SortOrder - 1)
                {
                    upInfo = item;
                    break;
                }
            }

            currInfo.SortOrder = currInfo.SortOrder - 1;
            upInfo.SortOrder = upInfo.SortOrder + 1;

            try
            {
                BeginTransaction();

                if (Update(currInfo) == 1 && Update(upInfo) == 1)
                {
                    CommitTransaction();
                    GetProductCategoryAttributeList(productCategoryId, true);
                    result = true;
                    message = "成功上移选中产品类型属性";
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "操作失败，上移选中产品类型属性失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("上移选中产品类型属性异常", ex);
                result = false;
                message = "操作失败，上移选中产品类型属性异常";
            }

            return result;
        }

        public bool MoveDownProductCategoryAttribute(string productCategoryId, string productAttributeId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(productCategoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategoryAttributesModel currInfo = GetProductCategoryAttributeById(productCategoryId, productAttributeId);
            if (currInfo == null)
            {
                message = "操作失败，不存在的产品类型属性ID";
                return false;
            }

            if (currInfo.SortOrder == dict.Count)
            {
                message = "操作失败，当前产品类型属性已经是最下序列";
                return false;
            }

            ProductCategoryAttributesModel downInfo = null;
            foreach (ProductCategoryAttributesModel item in dict.Values)
            {
                if (item.SortOrder == currInfo.SortOrder + 1)
                {
                    downInfo = item;
                    break;
                }
            }

            currInfo.SortOrder = currInfo.SortOrder + 1;
            downInfo.SortOrder = downInfo.SortOrder - 1;

            try
            {
                BeginTransaction();

                if (Update(currInfo) == 1 && Update(downInfo) == 1)
                {
                    CommitTransaction();
                    GetProductCategoryAttributeList(productCategoryId, true);
                    result = true;
                    message = "成功下移选中产品类型属性";
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "操作失败，下移选中产品类型属性失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("下移选中产品类型属性异常", ex);
                result = false;
                message = "操作失败，下移选中产品类型属性异常";
            }

            return result;
        }

        public bool DeleteProductCategoryAttribute(string productCategoryId, string productAttributeId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(productCategoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategoryAttributesModel currInfo = GetProductCategoryAttributeById(productCategoryId, productAttributeId);
            if (currInfo == null)
            {
                message = "操作失败，不存在的产品类型属性ID";
                return false;
            }

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(currInfo.ProductCategoryId);
            if(catInfo == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            try
            {
                BeginTransaction();

                foreach (ProductCategoryAttributesModel item in dict.Values)
                {
                    if (item.SortOrder > currInfo.SortOrder)
                    {
                        item.SortOrder = item.SortOrder + 1;
                        if (Update(item) != 1)
                        {
                            message = "更新产品属性排序索引失败，请与管理员联系";
                            RollbackTransaction();
                            return false;
                        }
                    }
                }

                string deleteFieldSQL = DTableUtil.GetDeleteFieldSQL(catInfo.TableName, currInfo.AttributeName);

                ExecuteNonQuery(deleteFieldSQL);

                if (Delete(currInfo.CategoryAttributeId) == 1)
                {
                    CommitTransaction();
                    message = "成功删除选中产品属性";
                    GetProductCategoryAttributeList(currInfo.ProductCategoryId, true);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除选中产品类型属性异常", ex);
                result = false;
                message = "操作失败，删除选中产品类型属性异常";
            }

            return result;
        }


        public bool UpdateProductCategoryAttribute(ProductCategoryAttributesModel attInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(attInfo.ProductCategoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(attInfo.ProductCategoryId);
            if (catInfo == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            foreach (ProductCategoryAttributesModel item in dict.Values)
            {
                if (item.AttributeName == attInfo.AttributeName && item.CategoryAttributeId != attInfo.CategoryAttributeId)
                {
                    message = "操作失败，本产品类型存在相同名称属性";
                    return false;
                }
            }

            ProductCategoryAttributesModel oldAttInfo = ProductCategoryAttributesService.Instance.GetProductCategoryAttributeById(attInfo.ProductCategoryId, attInfo.CategoryAttributeId);
            if (attInfo.FieldType != oldAttInfo.FieldType || attInfo.AttributeName != oldAttInfo.AttributeName)
            {
                message = "更改产品属性将直接影响现有本类型产品信息，系统暂不提供该功能，请与管理员联系使用其他方式实现";
                return false;
            }

            try
            {
                BeginTransaction();

                //if (attInfo.AttributeName != oldAttInfo.AttributeName)
                //{
                //    string renameFieldSQL = DTableUtil.GetRenameFieldSQL(catInfo.TableName, oldAttInfo.AttributeName, attInfo.AttributeName);
                //    ExecuteNonQuery(renameFieldSQL);
                //}

                if (Update(attInfo) == 1)
                {
                    GetProductCategoryAttributeList(attInfo.ProductCategoryId, true);
                    message = "成功更新产品类型属性";
                    result = true;

                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    message = "更新产品类型属性失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新产品类型属性异常", ex);
                throw ex;
            }

            return result;
        }

        public bool CreateProductCategoryAttribute(ProductCategoryAttributesModel attInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (attInfo.FieldType.ToLower() == "string")
            {
                if (attInfo.FieldMinLength <= 1 || attInfo.FieldMaxLength <= 1 || attInfo.FieldMaxLength <= attInfo.FieldMinLength)
                {
                    message = "属性字段长度有误，请检查输入";
                    return false;
                }
            }

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(attInfo.ProductCategoryId);
            if (catInfo == null)
            {
                message = "失败失败，不存在此产品类型ID";
                return false;
            }

            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(attInfo.ProductCategoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            foreach (ProductCategoryAttributesModel item in dict.Values)
            {
                if (item.AttributeName == attInfo.AttributeName)
                {
                    message = "操作失败，本产品类型存在相同名称属性";
                    return false;
                }
            }

            attInfo.CategoryAttributeId = Guid.NewGuid().ToString();
            attInfo.SortOrder = dict.Count + 1;

            attInfo.NodeId = 0;
            attInfo.ParentNode = 0;
            attInfo.IsDisplay = 0;

            FieldInfo fieldInfo = new FieldInfo();
            fieldInfo.FieldName = attInfo.AttributeName;

            if (attInfo.FieldType == "string" || attInfo.FieldType == "custom")
            {
                fieldInfo.FieldType = "varchar";
                fieldInfo.MinLength = Convert.ToInt32(attInfo.FieldMinLength);
                fieldInfo.MaxLength = Convert.ToInt32(attInfo.FieldMaxLength);
            }
            else
            {
                fieldInfo.FieldType = attInfo.FieldType;
                fieldInfo.MinLength = Convert.ToInt32(attInfo.FieldMinLength);
                fieldInfo.MaxLength = Convert.ToInt32(attInfo.FieldMaxLength);
            }

            if (fieldInfo.MinLength < 0 && fieldInfo.FieldType == "varchar")
            {
                fieldInfo.MinLength = 50;
            }

            if (fieldInfo.MaxLength < 0 && fieldInfo.FieldType == "varchar")
            {
                fieldInfo.MaxLength = 50;
            }

            if (attInfo.FieldType == "text")
            {
                fieldInfo.FieldType = "varchar";
                fieldInfo.MaxLength = -1;
            }

            fieldInfo.DefaultValue = attInfo.DefaultValue;
            fieldInfo.Description = attInfo.Description;
            fieldInfo.IsNull = (attInfo.IsRequest == 0);

            string addFieldSQL = DTableUtil.GetAddFieldSQL(catInfo.TableName, fieldInfo);

            try
            {
                BeginTransaction();

                ExecuteNonQuery(addFieldSQL);

                if (Create(attInfo) == 1)
                {
                    GetProductCategoryAttributeList(attInfo.ProductCategoryId, true);
                    message = "成功创建产品类型属性";
                    result = true;

                    CommitTransaction();
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "创建产品类型属性失败，请与管理员联系";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建产品类型属性异常", ex);
                throw ex;
            }

            return result;
        }

        public ProductCategoryAttributesModel GetProductCategoryAttributeById(string productCategoryId, string attributeId)
        {
            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(productCategoryId, false);

            return (dict != null && dict.ContainsKey(attributeId)) ? dict[attributeId] : null;
        }

        public ProductCategoryAttributesModel GetProductCategoryAttributeByName(string productCategoryId, string attributeName)
        {
            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(productCategoryId, false);

            foreach (ProductCategoryAttributesModel item in dict.Values)
            {
                if (item.AttributeName == attributeName)
                {
                    return item;
                }
            }
            return null;
        }

        public List<string> GetProductCategoryAttributeGroupList(string productCategoryId, bool clear)
        {
            Dictionary<string, ProductCategoryAttributesModel> dict = GetProductCategoryAttributeList(productCategoryId, clear);
            List<string> list = new List<string>();

            if (dict != null)
            {
                foreach (ProductCategoryAttributesModel item in dict.Values)
                {
                    if (!string.IsNullOrEmpty(item.GroupName))
                    {
                        if (!list.Contains(item.GroupName))
                        {
                            list.Add(item.GroupName);
                        }
                    }
                }
            }

            return list;
        }

        public Dictionary<string, ProductCategoryAttributesModel> GetProductCategoryAttributeList(string productCategoryId, bool clear)
        {
            if (string.IsNullOrEmpty(productCategoryId))
                return null;

            Dictionary<string, ProductCategoryAttributesModel> dict = null;
            string cacheKey = CacheKey.PRODUCT_CATEGORY_ATTRIBUTE_DICT.GetKeyDefine(productCategoryId);
            dict = CacheUtil.Get<Dictionary<string, ProductCategoryAttributesModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetProductCategoryAttributeListFromDatabase(productCategoryId);
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


        private Dictionary<string, ProductCategoryAttributesModel> GetProductCategoryAttributeListFromDatabase(string productCategoryId)
        {
            Dictionary<string, ProductCategoryAttributesModel> dict = null;
            ParameterCollection pc = new ParameterCollection();
            pc.Add("product_category_id", productCategoryId);

            List<ProductCategoryAttributesModel> list = RetrieveMultiple(pc, OrderByCollection.Create("sort_order", "asc"));
            if (list != null)
            {
                dict = new Dictionary<string, ProductCategoryAttributesModel>();
                foreach (ProductCategoryAttributesModel item in list)
                {
                    dict.Add(item.CategoryAttributeId, item);
                }
            }

            return dict;
        }
	}
}

