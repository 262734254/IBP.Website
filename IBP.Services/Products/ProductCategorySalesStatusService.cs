/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-31
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
	/// ProductCategorySalesStatus业务逻辑类
	/// </summary>
	public partial class ProductCategorySalesStatusService : BaseService
	{
		// 在此添加你的代码...

        public ProductCategorySalesStatusModel GetProductCategorySalesStatusInfoById(string categoryId, string statusId)
        {
            Dictionary<string, ProductCategorySalesStatusModel> dict = GetProductCategorySalesStatusList(categoryId, false);

            return (dict != null && dict.ContainsKey(statusId)) ? dict[statusId] : null;
        }

        public bool MoveUpProductCategorySaleStatus(string categoryId, string statusId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategorySalesStatusModel> dict = GetProductCategorySalesStatusList(categoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategorySalesStatusModel currInfo = GetProductCategorySalesStatusInfoById(categoryId, statusId);
            if (currInfo == null)
            {
                message = "操作失败，不存在的产品类型销售状态ID";
                return false;
            }

            if (currInfo.SortOrder == 1)
            {
                message = "操作失败，当前产品类型销售状态已经是最上序列";
                return false;
            }

            ProductCategorySalesStatusModel upInfo = null;
            foreach(ProductCategorySalesStatusModel item in dict.Values)
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
                    GetProductCategorySalesStatusList(categoryId, true);
                    result = true;
                    message = "成功上移选中产品类型销售状态";
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "操作失败，上移选中产品类型销售状态失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("上移选中产品类型销售状态异常", ex);
                result = false;
                message = "操作失败，上移选中产品类型销售状态异常";
            }

            return result;
        }


        public bool MoveDownProductCategorySaleStatus(string categoryId, string statusId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            Dictionary<string, ProductCategorySalesStatusModel> dict = GetProductCategorySalesStatusList(categoryId, false);
            if (dict == null)
            {
                message = "操作失败，不存在的产品类型ID";
                return false;
            }

            ProductCategorySalesStatusModel currInfo = GetProductCategorySalesStatusInfoById(categoryId, statusId);
            if (currInfo == null)
            {
                message = "操作失败，不存在的产品类型销售状态ID";
                return false;
            }

            if (currInfo.SortOrder == dict.Count)
            {
                message = "操作失败，当前产品类型销售状态已经是最下序列";
                return false;
            }

            ProductCategorySalesStatusModel downInfo = null;
            foreach (ProductCategorySalesStatusModel item in dict.Values)
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
                    GetProductCategorySalesStatusList(categoryId, true);
                    result = true;
                    message = "成功下移选中产品类型销售状态";
                }
                else
                {
                    RollbackTransaction();
                    result = false;
                    message = "操作失败，下移选中产品类型销售状态失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("下移选中产品类型销售状态异常", ex);
                result = false;
                message = "操作失败，下移选中产品类型销售状态异常";
            }

            return result;
        }



        public Dictionary<string, ProductCategorySalesStatusModel> GetProductCategorySalesStatusList(string productCategoryId, bool clear)
        {
            if (string.IsNullOrEmpty(productCategoryId))
                return null;

            Dictionary<string, ProductCategorySalesStatusModel> dict = null;
            string cacheKey = CacheKey.PRODUCT_CATEGORY_SALESTATUS_DICT.GetKeyDefine(productCategoryId);
            dict = CacheUtil.Get<Dictionary<string, ProductCategorySalesStatusModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetProductCategorySalesStatusListFromDatabase(productCategoryId);
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

        public Dictionary<string, ProductCategorySalesStatusModel> GetProductCategorySalesStatusListFromDatabase(string productCategoryId)
        {
            Dictionary<string, ProductCategorySalesStatusModel> dict = null;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("product_category_id", productCategoryId);
            List<ProductCategorySalesStatusModel> list = RetrieveMultiple(pc, OrderByCollection.Create("sort_order", "asc"));
            if (list != null && list.Count > 0)
            {
                dict = new Dictionary<string, ProductCategorySalesStatusModel>();
                foreach (ProductCategorySalesStatusModel item in list)
                {
                    dict.Add(item.SalesStatusId, item);
                }
            }

            return dict;
        }

        public bool CreateProductCategorySaleStatus(ProductCategorySalesStatusModel statusInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(statusInfo.ProductCategoryId);
            if (catInfo == null)
            {
                message = "操作失败，不存在产品类型信息";
                return false;
            }

            Dictionary<string, ProductCategorySalesStatusModel> dict = GetProductCategorySalesStatusList(catInfo.ProductCategoryId, false);
            if (dict != null)
            {
                foreach (ProductCategorySalesStatusModel item in dict.Values)
                {
                    if (item.SalestatusName == statusInfo.SalestatusName)
                    {
                        message = "操作失败，存在相同名称的销售状态";
                        return false;
                    }
                }
            }

            statusInfo.SalesStatusId = Guid.NewGuid().ToString();
            statusInfo.SortOrder = (dict == null) ? 1 : dict.Count + 1;

            if (Create(statusInfo) == 1)
            {
                GetProductCategorySalesStatusList(statusInfo.ProductCategoryId, true);
                result = true;
                message = "成功创建本产品类型销售状态";
            }

            return result;
        }


        public bool UpdateProductCategorySaleStatus(ProductCategorySalesStatusModel statusInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(statusInfo.ProductCategoryId);
            if (catInfo == null)
            {
                message = "操作失败，不存在产品类型信息";
                return false;
            }

            Dictionary<string, ProductCategorySalesStatusModel> dict = GetProductCategorySalesStatusList(catInfo.ProductCategoryId, false);
            if (dict != null)
            {
                foreach (ProductCategorySalesStatusModel item in dict.Values)
                {
                    if (item.SalestatusName == statusInfo.SalestatusName && item.SalesStatusId != statusInfo.SalesStatusId)
                    {
                        message = "操作失败，存在相同名称的销售状态";
                        return false;
                    }
                }
            }

            if (Update(statusInfo) == 1)
            {
                GetProductCategorySalesStatusList(statusInfo.ProductCategoryId, true);
                result = true;
                message = "成功更新本产品类型销售状态";
            }

            return result;
        }
	}
}

