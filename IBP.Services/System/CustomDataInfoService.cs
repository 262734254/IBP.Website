/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-10
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
	/// CustomDataInfo业务逻辑类
	/// </summary>
	public partial class CustomDataInfoService : BaseService
	{
		// 在此添加你的代码...

        /// <summary>
        /// 下移指定枚举值。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="valueId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool MoveDownCustomDataValue(string dataId, string valueId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            CustomDataDomainModel dataInfo = GetCustomDataDomainModelById(dataId, false);
            CustomDataValueDomainModel valueInfo = (dataInfo.ValueList.ContainsKey(valueId)) ? dataInfo.ValueList[valueId] : null;

            if (valueInfo == null)
            {
                message = "操作失败，该枚举值不存在";
                return false;
            }

            if (valueInfo.SortOrder == dataInfo.ValueList.Count)
            {
                message = "操作失败，该枚举值已为最下序列号";
                return false;
            }

            CustomDataValueDomainModel downValueInfo = null;
            foreach (CustomDataValueDomainModel item in dataInfo.ValueList.Values)
            {
                if (item.SortOrder == valueInfo.SortOrder + 1)
                {
                    downValueInfo = item;
                }
            }

            if (downValueInfo != null)
            {
                string sql = @"UPDATE custom_data_value SET sort_order = $sort_order$ WHERE value_id = $value_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("value_id", valueInfo.ValueId);
                    pc.Add("sort_order", valueInfo.SortOrder + 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("value_id", downValueInfo.ValueId);
                        pc.Add("sort_order", downValueInfo.SortOrder - 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetCustomDataDomainModelById(dataId, true);
                            message = "成功下移该自定义枚举值";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("下移自定义枚举信息排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }


        /// <summary>
        /// 上移指定枚举值。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="valueId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool MoveUpCustomDataValue(string dataId, string valueId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            CustomDataDomainModel dataInfo = GetCustomDataDomainModelById(dataId, false);
            CustomDataValueDomainModel valueInfo = (dataInfo.ValueList.ContainsKey(valueId)) ? dataInfo.ValueList[valueId] : null;

            if (valueInfo == null)
            {
                message = "操作失败，该枚举值不存在";
                return false;
            }

            if (valueInfo.SortOrder <= 1)
            {
                message = "操作失败，该枚举值已为最上序列号";
                return false;
            }

            CustomDataValueDomainModel upValueInfo = null;
            foreach (CustomDataValueDomainModel item in dataInfo.ValueList.Values)
            {
                if (item.SortOrder == valueInfo.SortOrder - 1)
                {
                    upValueInfo = item;
                }
            }

            if (upValueInfo != null)
            {
                string sql = @"UPDATE custom_data_value SET sort_order = $sort_order$ WHERE value_id = $value_id$";

                try
                {
                    BeginTransaction();
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("value_id", valueInfo.ValueId);
                    pc.Add("sort_order", valueInfo.SortOrder - 1);

                    if (ExecuteNonQuery(sql, pc) > 0)
                    {
                        pc.Clear();
                        pc.Add("value_id", upValueInfo.ValueId);
                        pc.Add("sort_order", upValueInfo.SortOrder + 1);

                        if (ExecuteNonQuery(sql, pc) > 0)
                        {
                            CommitTransaction();
                            GetCustomDataDomainModelById(dataId, true);
                            message = "成功上移该自定义枚举值";
                            return true;
                        }
                    }

                    RollbackTransaction();
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("上移自定义枚举信息排序索引异常", ex);
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// 删除指定枚举值。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="valueId"></param>
        /// <returns></returns>
        public bool DeleteCustomDataValue(string dataId, string valueId)
        {
            bool result = false;

            try
            {
                BeginTransaction();

                if (CustomDataValueService.Instance.Delete(valueId) > 0)
                {
                    CustomDataDomainModel dataInfo = GetCustomDataDomainModelById(dataId, true);
                    string sql = @"UPDATE custom_data_value SET sort_order = $sort_order$ WHERE value_id = $value_id$";
                    ParameterCollection pc = new ParameterCollection();

                    int index = 1;
                    foreach(CustomDataValueDomainModel item in dataInfo.ValueList.Values)
                    {
                        item.SortOrder = index;
                        index++;

                        pc.Clear();
                        pc.Add("value_id", item.ValueId);
                        pc.Add("sort_order", item.SortOrder);

                        if (ExecuteNonQuery(sql, pc) != 1)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    CommitTransaction();
                    result = true;
                }

                RollbackTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除自定义枚举值异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 更新指定枚举值。
        /// </summary>
        /// <param name="valueInfo"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public bool UpdateCustomDataValue(CustomDataValueDomainModel valueInfo, string dataId)
        {
            bool result = false;

            CustomDataValueModel dataModel = new CustomDataValueModel();
            dataModel.ValueId = valueInfo.ValueId;
            dataModel.DataId = dataId;
            dataModel.DataValue = valueInfo.DataValue;
            dataModel.DataValueCode = valueInfo.DataValueCode;
            dataModel.SortOrder = valueInfo.SortOrder;
            dataModel.Status = valueInfo.Status;

            if (CustomDataValueService.Instance.Update(dataModel) > 0)
            {
                GetCustomDataDomainModelById(dataId, true);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 新建自定义枚举值。
        /// </summary>
        /// <param name="valueInfo"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public bool NewCustomDataValue(CustomDataValueDomainModel valueInfo, string dataId)
        {
            bool result = false;
            CustomDataDomainModel dataInfo = GetCustomDataDomainModelById(dataId, false);

            valueInfo.ValueId = Guid.NewGuid().ToString();
            valueInfo.DataId = dataId;
            valueInfo.SortOrder = dataInfo.ValueList.Count + 1;
            CustomDataValueModel dataModel = new CustomDataValueModel();
            dataModel.ValueId = valueInfo.ValueId;
            dataModel.DataId = dataId;
            dataModel.DataValue = valueInfo.DataValue;
            dataModel.DataValueCode = valueInfo.DataValueCode;
            dataModel.SortOrder = valueInfo.SortOrder;
            dataModel.Status = valueInfo.Status;

            if (CustomDataValueService.Instance.Create(dataModel) > 0)
            {
                GetCustomDataDomainModelById(dataId, true);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 根据ID获取自定义枚举信息。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public CustomDataDomainModel GetCustomDataDomainModelById(string dataId, bool clear)
        {
            if (string.IsNullOrEmpty(dataId))
                return null;

            string cacheKey = CacheKey.CUSTOM_DATA_INFO_BYID.GetKeyDefine(dataId);

            CustomDataDomainModel model = CacheUtil.Get<CustomDataDomainModel>(cacheKey);

            if (model == null || clear)
            {
                model = GetCustomDataDomainModelByIdFromDatabase(dataId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
                else
                {
                    CacheUtil.Remove(cacheKey);
                }
            }            

            return model;
        }

        /// <summary>
        /// 根据枚举名称获取自定义枚举值信息。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public CustomDataDomainModel GetCustomDataDomainModelByName(string dataName, bool clear)
        {
            Dictionary<string, CustomDataDomainModel> dict = GetCustomDataDomainModelList(false);
            foreach (CustomDataDomainModel item in dict.Values)
            {
                if (item.DataName == dataName)
                {
                    return GetCustomDataDomainModelById(item.DataId, clear);
                }
            }

            return null;
        }



        /// <summary>
        /// 获取自定义枚举字典。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public Dictionary<string, CustomDataDomainModel> GetCustomDataDomainModelList(bool clear)
        {
            string cacheKey = CacheKey.CUSTOM_DATA_DICT;

            Dictionary<string, CustomDataDomainModel> dict = CacheUtil.Get<Dictionary<string, CustomDataDomainModel>>(cacheKey);

            if (dict == null || clear)
            {
                dict = GetCustomDataDomainModelListFromDatabase();
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
        /// 根据ID从数据库获取自定义枚举信息。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public CustomDataDomainModel GetCustomDataDomainModelByIdFromDatabase(string dataId)
        {
            CustomDataDomainModel model = null;
            
            string sql = @"SELECT * FROM custom_data_info WHERE data_id = $data_id$ ORDER BY sort_order ASC";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("data_id", dataId);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                model = new CustomDataDomainModel();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new CustomDataDomainModel();
                    model.DataCode = dt.Rows[i]["data_code"].ToString();
                    model.DataId = dt.Rows[i]["data_id"].ToString();
                    model.DataName = dt.Rows[i]["data_name"].ToString();
                    model.DataType = dt.Rows[i]["data_type"].ToString();
                    model.FieldName = dt.Rows[i]["field_name"].ToString();
                    model.FieldType = dt.Rows[i]["field_type"].ToString();
                    model.MaxLength = Convert.ToInt32(dt.Rows[i]["max_length"]);
                    model.MinLength = Convert.ToInt32(dt.Rows[i]["min_length"]);
                    model.Requested = dt.Rows[i]["requested"].ToString() == "0";
                    model.SortOrder = Convert.ToInt32(dt.Rows[i]["sort_order"]);
                    model.Status = Convert.ToInt32(dt.Rows[i]["status"]);

                    List<CustomDataValueDomainModel> valueList = GetCustomDataValueDomainModelListByDataIdFromDatabase(model.DataId);
                    if (valueList != null)
                    {
                        model.ValueList = new Dictionary<string, CustomDataValueDomainModel>();
                        foreach(CustomDataValueDomainModel valueItem in valueList)
                        {
                            model.ValueList.Add(valueItem.ValueId, valueItem);
                        }
                    }
                }
            }

            return model;
        }

        public List<CustomDataValueDomainModel> GetCustomDataValueDomainModelListByDataIdFromDatabase(string dataId)
        {
            List<CustomDataValueDomainModel> list = null;

            string sql = "SELECT * FROM custom_data_value WHERE data_id = $data_id$ ORDER BY sort_order ASC";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("data_id", dataId);
            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<CustomDataValueDomainModel>();
                CustomDataValueDomainModel model = null;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new CustomDataValueDomainModel();

                    model.DataId = dt.Rows[i]["data_id"].ToString();
                    model.DataValue = dt.Rows[i]["data_value"].ToString();
                    model.DataValueCode = dt.Rows[i]["data_value_code"].ToString();
                    model.SortOrder = Convert.ToInt32(dt.Rows[i]["sort_order"]);
                    model.Status = Convert.ToInt32(dt.Rows[i]["status"]);
                    model.ValueId = dt.Rows[i]["value_id"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 从数据库获取自定义枚举字典。
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CustomDataDomainModel> GetCustomDataDomainModelListFromDatabase()
        {
            Dictionary<string, CustomDataDomainModel> list = null;

            string sql = @"SELECT * FROM custom_data_info ORDER BY sort_order ASC";

            DataTable dt = ExecuteDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new Dictionary<string, CustomDataDomainModel>();
                CustomDataDomainModel model = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    model = new CustomDataDomainModel();
                    model.DataCode = dt.Rows[i]["data_code"].ToString();
                    model.DataId = dt.Rows[i]["data_id"].ToString();
                    model.DataName = dt.Rows[i]["data_name"].ToString();
                    model.DataType = dt.Rows[i]["data_type"].ToString();
                    model.FieldName = dt.Rows[i]["field_name"].ToString();
                    model.FieldType = dt.Rows[i]["field_type"].ToString();
                    model.MaxLength = Convert.ToInt32(dt.Rows[i]["max_length"]);
                    model.MinLength = Convert.ToInt32(dt.Rows[i]["min_length"]);
                    model.Requested = dt.Rows[i]["requested"].ToString() == "0";
                    model.SortOrder = Convert.ToInt32(dt.Rows[i]["sort_order"]);
                    model.Status = Convert.ToInt32(dt.Rows[i]["status"]);

                    list.Add(model.DataId, model);
                }
            }

            return list;
        }
	}
}

