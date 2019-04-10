/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-13
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
	/// PayPosMachineInfo业务逻辑类
	/// </summary>
    public partial class PayPosMachineInfoService : BaseService
    {
        // 在此添加你的代码...

        public PayPosMachineInfoModel GetPosMachineById(string posMachineId, bool clear)
        {
            string cacheKey = CacheKey.POS_MACHINE_DATAMODEL.GetKeyDefine(posMachineId);

            PayPosMachineInfoModel model = CacheUtil.Get<PayPosMachineInfoModel>(cacheKey);
            
            if (model == null || clear)
            {
                model = Retrieve(posMachineId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }


        public Dictionary<string, PayPosMachineInfoModel> GetPosMachineList(bool clear)
        {
            string cacheKey = CacheKey.POS_MACHINE_DICT;

            Dictionary<string, PayPosMachineInfoModel> dict = CacheUtil.Get<Dictionary<string, PayPosMachineInfoModel>>(cacheKey);

            if (dict == null || clear)
            {
                List<PayPosMachineInfoModel> list = RetrieveMultiple(new ParameterCollection());
                if (list != null)
                {
                    dict = new Dictionary<string, PayPosMachineInfoModel>();
                    foreach (PayPosMachineInfoModel item in list)
                    {
                        dict[item.PosMachineId] = item;
                    }

                    CacheUtil.Set(cacheKey, dict);
                }
            }

            return dict;
        }

    }
}

