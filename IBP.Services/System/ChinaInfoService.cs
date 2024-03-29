/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-27
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
	/// ChinaInfo业务逻辑类
	/// </summary>
	public partial class ChinaInfoService : BaseService
	{
		// 在此添加你的代码...
        private DataTable _chinaInfoTable = null;
        //private Dictionary<string, string> _provinceAreaList = null;
        //private Dictionary<string, string> _provinceList = null;
        //private Dictionary<string, string> _cityList = null;
        //private Dictionary<string, string> _countyList = null;


        private DataTable ChinaInfoTable
        {
            get
            {
                if (_chinaInfoTable == null)
                {
                    _chinaInfoTable = ExecuteDataTable("SELECT * FROM china_info");
                }

                return _chinaInfoTable;
            }
        }

        public ChinaInfoModel GetChinaInfo(string chinaId)
        {
            if (string.IsNullOrEmpty(chinaId))
                return null;

            ChinaInfoModel result = null;

            string filterSQL = string.Format("id = " + chinaId);
            DataRow[] hasRows = ChinaInfoTable.Select(filterSQL);

            if (hasRows.Length > 0)
            {
                result = new ChinaInfoModel();
                ModelConvertFrom(result, hasRows[0]);
            }

            return result;
        }

        /// <summary>
        /// 获取省份地区。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetProvinceAreaList()
        {
            Dictionary<string, string> dict = null;

            string fielterSQL = string.Format("parent_id = 0");
            DataRow[] hasRows = ChinaInfoTable.Select(fielterSQL);

            if (hasRows.Length > 0)
            {
                dict = new Dictionary<string, string>();
                for (int i = 0; i < hasRows.Length; i++)
                {
                    dict[hasRows[i]["province_area_id"].ToString()] = hasRows[i]["province_area_name"].ToString();
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取省份列表。
        /// </summary>
        /// <param name="provinceAreaId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetProvinceList(int? provinceAreaId)
        {
            Dictionary<string, string> dict = null;

            string fielterSQL = (provinceAreaId == null)
                ? string.Format("province_id is not null and city_id is null")
                : string.Format("province_area_id = {0} null and province_id is not null and city_id is null", provinceAreaId);

            DataRow[] hasRows = ChinaInfoTable.Select(fielterSQL);

            if (hasRows.Length > 0)
            {
                dict = new Dictionary<string, string>();
                for (int i = 0; i < hasRows.Length; i++)
                {
                    dict[hasRows[i]["province_id"].ToString()] = hasRows[i]["province_name"].ToString();
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取城市列表。
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetCityList(int? provinceId)
        {
            Dictionary<string, string> dict = null;

            string fielterSQL = (provinceId == null)
                ? string.Format("city_id is not null and county_id is null")
                : string.Format("province_id = {0} and city_id is not null and county_id is null", provinceId);

            DataRow[] hasRows = ChinaInfoTable.Select(fielterSQL);

            if (hasRows.Length > 0)
            {
                dict = new Dictionary<string, string>();
                for (int i = 0; i < hasRows.Length; i++)
                {
                    dict[hasRows[i]["city_id"].ToString()] = hasRows[i]["city_name"].ToString();
                }
            }

            return dict;
        }

        /// <summary>
        /// 获取县区列表。
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetCountyList(int? cityId)
        {
            Dictionary<string, string> dict = null;

            string fielterSQL = (cityId == null)
                ? string.Format("county_id is not null")
                : string.Format("city_id = {0}", cityId);

            DataRow[] hasRows = ChinaInfoTable.Select(fielterSQL);

            if (hasRows.Length > 0)
            {
                dict = new Dictionary<string, string>();
                for (int i = 0; i < hasRows.Length; i++)
                {
                    dict[hasRows[i]["county_id"].ToString()] = hasRows[i]["county_name"].ToString();
                }
            }

            return dict;
        }


        public ChinaInfoModel GetChinaInfoById(string chinaId, bool clear)
        {
            if (string.IsNullOrEmpty(chinaId))
                return null;

            string cacheKey = CacheKey.CHINA_INFO_MODEL.GetKeyDefine(chinaId);
            ChinaInfoModel model = CacheUtil.Get<ChinaInfoModel>(cacheKey);

            if (model == null || clear)
            {
                model = GetChinaInfoByIdFromDatabase(chinaId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }

        public ChinaInfoModel GetChinaInfoByIdFromDatabase(string chinaId)
        {
            ChinaInfoModel model = null;

            string sql = "select * from china_info where id = $id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("id", chinaId);

            DataTable dt = ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                model = new ChinaInfoModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }
	}
}

