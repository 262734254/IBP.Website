/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-12
*/

using System.Collections.Generic;
using System.Data;
using Framework.Common;
using Framework.Utilities;
using IBP.Common;
using IBP.Models;

using System;

namespace IBP.Services
{
	/// <summary>
	/// SalesorderTypeStatusInfo业务逻辑类
	/// </summary>
	public partial class SalesorderTypeStatusInfoService : BaseService
	{
		// 在此添加你的代码...

        /// <summary>
        /// 根据ID删除订单类型状态值。
        /// </summary>
        /// <param name="statusid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteSalesOrderTypeStatusById(string statusid, out string message)
        {

            message = "操作失败，请与管理员联系";
            bool result = false;

            try
            {
                BeginTransaction();

                if (Delete(statusid) > 0)
                {
                    CommitTransaction();
                    result = true;
                    message = "成功删除订单类型状态值";
                    GetSalesOrderTypeStatusModelById(statusid, true);
                }
                else
                {
                    RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("删除订单类型状态值异常", ex);
                throw ex;
            }

            return result;
        }
        /// <summary>
        /// 检查类型名称是否存在。
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        private bool CheckExistSalesorderStatusName(string salesorder_status_name)
        {
            bool result = false;

            string sql = @"
SELECT 
    COUNT(1) 
FROM 
    salesorder_type_status_info 
WHERE 
    salesorder_status_name = $salesorder_status_name$
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("salesorder_status_name", salesorder_status_name);
            result = Convert.ToInt32(ExecuteScalar(sql, pc)) > 0;

            return result;
        }
        /// <summary>
        /// 编辑类型状态操作。
        /// </summary>
        /// <param name="statusInfo"></param>
        /// <returns></returns>
        public bool EditSalesOrderTypeStatusInfo(SalesorderTypeStatusInfoModel model)
        {
            bool result = false;

            if (SalesorderTypeStatusInfoService.Instance.Update(model) > 0)
            {
                result = true;
                GetSalesOrderTypeStatusModelById(model.SalsorderStatusId, true);
            }

            return result;
        }
      
        /// <summary>
        /// 新建订单类型状态值。
        /// </summary>
        /// <param name="statusInfo"></param>
        /// <returns></returns>
        public bool CreateSalesOrderTypeStatusInfo(SalesorderTypeStatusInfoModel model,out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            if (CheckExistSalesorderStatusName(model.SalesorderStatusName))
            {
                message = string.Format("操作失败，已经存在名为【{0}】状态名称", model.SalesorderStatusName);
                return false;
            }

            if (SalesorderTypeStatusInfoService.Instance.Create(model) > 0)
            {
                message = "添加成功";
                result = true;
                GetSalesOrderTypeStatusModelById(model.SalsorderStatusId, true);
            }
            return result;
        }

        protected SalesorderTypeStatusInfoModel GetSalesOrderTypeStatusModelByIdFromDatabase(string usalsorder_status_id)
        {
            SalesorderTypeStatusInfoModel model = null;

            string sql = @"
SELECT [salsorder_status_id]
      ,[salesorder_type_id]
      ,[payment_type]
      ,[salesorder_status_name]
      ,[status]
      ,[description]
      ,[sort_order]
      ,[created_by]
      ,[created_on]
      ,[modified_on]
      ,[modified_by]
      ,[status_code]
  FROM salesorder_type_status_info WHERE salsorder_status_id = $salsorder_status_id$ ";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("salsorder_status_id", usalsorder_status_id);

            DataTable dt = ExecuteDataTable(sql, pc);

            if (dt != null && dt.Rows.Count > 0)
            {

                model = new SalesorderTypeStatusInfoModel();
                ModelConvertFrom(model, dt, 0);
            }

            return model;
        }
        /// <summary>
        /// 根据ID获取用户领域模型。
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public SalesOrderTypeStatusDomainModel GetSalesOrderTypeStatusModelById(string salsorder_status_id, bool clear)
        {
            if (string.IsNullOrEmpty(salsorder_status_id))
                return null;

            string cacheKey = CacheKey.GetKeyDefine(CacheKey.SALESORDER_TYPE_STATUS_INFO, salsorder_status_id);
            SalesOrderTypeStatusDomainModel result = CacheUtil.Get<SalesOrderTypeStatusDomainModel>(cacheKey);

            if (result == null || clear)
            {
                SalesorderTypeStatusInfoModel basicInfo = GetSalesOrderTypeStatusModelByIdFromDatabase(salsorder_status_id);
                if (basicInfo != null)
                {
                    result = new SalesOrderTypeStatusDomainModel();
                    result.BasicInfo = basicInfo;

                    CacheUtil.Set(cacheKey, result);
                }
            }

            return result;
        }



        /// <summary>
        /// 获取订单类型状态信息。
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<SalesorderTypeStatusInfoModel> GetSalesorderTypeStatusInfoList(string salesorder_type_id)
        {
            string sql = @"
SELECT [salsorder_status_id]
      ,[salesorder_type_id]
      ,[payment_type]
      ,[salesorder_status_name]
      ,[status]
      ,[description]
      ,[sort_order]
      ,[created_by]
      ,[created_on]
      ,[modified_on]
      ,[modified_by]
      ,[status_code]
  FROM salesorder_type_status_info WHERE salesorder_type_id = $salesorder_type_id$ ";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("salesorder_type_id", salesorder_type_id);

            return ModelConvertFrom<SalesorderTypeStatusInfoModel>(ExecuteDataTable(sql, pc));
        }
	}
}

