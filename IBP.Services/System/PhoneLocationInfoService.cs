/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-31
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
	/// PhoneLocationInfo业务逻辑类
	/// </summary>
	public partial class PhoneLocationInfoService : BaseService
	{
		// 在此添加你的代码...

        public PhoneLocationInfoModel GetLocationInfo(string phoneNumber, bool clear)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return null;

            string cacheKey = CacheKey.LOCATION_MODEL.GetKeyDefine(phoneNumber);
            PhoneLocationInfoModel result = CacheUtil.Get<PhoneLocationInfoModel>(cacheKey);

            if (result == null || clear)
            {
                result = GetLocationInfoFromDatabase(phoneNumber);
                if (result != null)
                {
                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;
        }


        public PhoneLocationInfoModel GetLocationInfoFromDatabase(string phoneNumber)
        {
            PhoneLocationInfoModel result = null;
            string sql = "select top 1 * from phone_location_info where phone_code = $phone_code$;";

            ParameterCollection pc = new ParameterCollection();
            if (phoneNumber.Length < 6)
                return null;

            pc.Add("phone_code", phoneNumber.Substring(0, 7));
            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                result = new PhoneLocationInfoModel();
                ModelConvertFrom(result, dt, 0);
            }
            else
            {
                sql = @"select top 1 city,china_id from phone_location_info where region_code = substring($phone_code$, 0, datalength(region_code) + 1)";
                pc.Clear();
                pc.Add("phone_code", phoneNumber);
                dt = ExecuteDataTable(sql, pc);

                if (dt != null && dt.Rows.Count > 0)
                {
                    result = new PhoneLocationInfoModel();
                    ModelConvertFrom(result, dt, 0);
                }
            }

            return result;
        }
	}
}

