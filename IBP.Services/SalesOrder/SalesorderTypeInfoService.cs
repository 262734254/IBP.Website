/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-12
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
	/// SalesorderTypeInfo业务逻辑类
	/// </summary>
	public partial class SalesorderTypeInfoService : BaseService
	{
		// 在此添加你的代码...


        /// <summary>
        /// 获取订单类型信息字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, SalesorderTypeInfoModel> GetSalesorderTypeInfoList(bool clear)
        {
            string cacheKey = CacheKey.SALESORDER_TYPE_INFO;

            Dictionary<string, SalesorderTypeInfoModel> dict = CacheUtil.Get<Dictionary<string, SalesorderTypeInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetSalesorderTypeInfoListFromDatabse();
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
        /// 根据ID获取域模型。
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public SalesorderTypeInfoModel GetSalesorderTypeInfoModel(string salesorder_type_id)
        {
            
            Dictionary<string, SalesorderTypeInfoModel> dict = GetSalesorderTypeInfoList(false);

            return (dict.ContainsKey(salesorder_type_id)) ? dict[salesorder_type_id] : null;
        }

        /// <summary>
        /// 从数据库中获取类型信息。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, SalesorderTypeInfoModel> GetSalesorderTypeInfoListFromDatabse()
        {
            Dictionary<string, SalesorderTypeInfoModel> dict = null;

            string sql = @"
SELECT [salesorder_type_id]
      ,[salesorder_type_name]
      ,[sort_order]
      ,[status]
      ,[description]
  FROM salesorder_type_info
";

            DataTable dt = ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dict = new Dictionary<string, SalesorderTypeInfoModel>();
                SalesorderTypeInfoModel model = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new SalesorderTypeInfoModel();
                    model.SalesorderTypeId = dt.Rows[i]["salesorder_type_id"].ToString();
                    model.SalesorderTypeName = dt.Rows[i]["salesorder_type_name"].ToString();
                    model.SortOrder = Convert.ToInt32(dt.Rows[i]["sort_order"].ToString());
                    model.Description = dt.Rows[i]["description"].ToString();
                    model.Status = Convert.ToInt32(dt.Rows[i]["status"]);

                    dict.Add(model.SalesorderTypeId, model);
                }
            }

            return dict;
        }

	}
}

