/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-20
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
	/// ProductCategoryGroupInfo业务逻辑类
	/// </summary>
	public partial class ProductCategoryGroupInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool CreateProductCategoryGroupInfo(ProductCategoryGroupInfoModel groupInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (groupInfo == null)
            {
                message = "参数错误，请检查输入";
                return false;
            }

            if (GetProductCategoryGroupByName(groupInfo.GroupName) != null)
            {
                message = "数据库中已经存在相同名字的产品分组名称，请检查输入";
                return false;
            }

            //groupInfo.ProductCategoryGroupId = GetGuid();

            if (Create(groupInfo) != 1)
            {
                message = "创建产品分组失败，请与管理员联系";
                result = false;
            }
            else
            {
                GetProductCategoryGroupList(true);
                message = "成功创建产品分组";
                result = true;
            }

            
            return result;
        }


        public bool UpdateProductCategoryGroupInfo(ProductCategoryGroupInfoModel groupInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (groupInfo == null)
            {
                message = "参数错误，请检查输入";
                return false;
            }


            if (Update(groupInfo) != 1)
            {
                message = "更新产品分组信息失败，请与管理员联系";
                result = false;
            }
            else
            {
                message = "成功更新产品分组";
                GetProductCategoryGroupList(true);
                result = true;
            }


            return result;
        }

        public List<ProductCategoryGroupInfoModel> GetProductCategoryGroupFromDatabase(string groupName, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            string sql = "";
            DataTable dt = null;
            List<ProductCategoryGroupInfoModel> result = null;
            ParameterCollection pc = new ParameterCollection();
            OrderByCollection obc = OrderByCollection.Create(orderField, orderDirection);
            total = 0;

            sql = string.Format("FROM product_category_group_info WHERE 1 = 1 {0} ",
                string.IsNullOrEmpty(groupName) ? "" : " AND group_name = $groupName$ ");

            if (!string.IsNullOrEmpty(groupName))
            {
                pc.Add("groupName", groupName);
            }

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sql, pc));
            dt = ExecuteDataTable("SELECT * " + sql, pc, pageIndex, pageSize, obc);

            result = ModelConvertFrom<ProductCategoryGroupInfoModel>(dt);

            return result;
        }

        public Dictionary<string, ProductCategoryGroupInfoModel> GetProductCategoryGroupList(bool clear)
        {
            string cacheKey = CacheKey.PRODUCT_CATEGORY_GROUP_DICT;
            Dictionary<string, ProductCategoryGroupInfoModel> dict = CacheUtil.Get<Dictionary<string, ProductCategoryGroupInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                List<ProductCategoryGroupInfoModel> list = RetrieveMultiple(new ParameterCollection(), OrderByCollection.Create("sort_order", "asc"));

                if (list != null)
                {
                    dict = new Dictionary<string, ProductCategoryGroupInfoModel>();
                    foreach (ProductCategoryGroupInfoModel item in list)
                    {
                        dict[item.ProductCategoryGroupId] = item;
                    }

                    CacheUtil.Set(cacheKey, dict);
                }
            }

            return dict;
        }

        public ProductCategoryGroupInfoModel GetProductCategoryGroupByName(string groupName)
        {
            ProductCategoryGroupInfoModel result = null;

            Dictionary<string, ProductCategoryGroupInfoModel> dict = GetProductCategoryGroupList(false);
            if (dict != null)
            {
                foreach (ProductCategoryGroupInfoModel item in dict.Values)
                {
                    if (item.GroupName == groupName)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        public ProductCategoryGroupInfoModel GetProductCategoryGroupById(string groupId)
        {
            ProductCategoryGroupInfoModel result = null;

            Dictionary<string, ProductCategoryGroupInfoModel> dict = GetProductCategoryGroupList(false);
            if (dict != null)
            {
                foreach (ProductCategoryGroupInfoModel item in dict.Values)
                {
                    if (item.ProductCategoryGroupId == groupId)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        public ProductCategoryGroupInfoModel GetProductCategoryGroupByCategoryId(string productCategoryId)
        {
            ProductCategoryGroupInfoModel result = null;
            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(productCategoryId);
            if (catInfo == null)
            {
                return null;
            }

            Dictionary<string, ProductCategoryGroupInfoModel> dict = GetProductCategoryGroupList(false);
            if (dict != null)
            {
                foreach (ProductCategoryGroupInfoModel item in dict.Values)
                {
                    if (item.ProductCategoryGroupId == catInfo.GroupName)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }
	}
}

