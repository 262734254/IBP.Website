/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-15
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
	/// WorkorderInfo业务逻辑类
	/// </summary>
	public partial class WorkorderInfoService
	{
		// 实例
		private static WorkorderInfoService _instance = new WorkorderInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workorderinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderInfoModel workorderinfo)
		{
			int ret = 0;

			workorderinfo.CreatedBy = SessionUtil.Current.UserId;
			workorderinfo.CreatedOn = DateTime.Now;
			workorderinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderinfo.ModifiedOn = DateTime.Now;
			workorderinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workorderinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="workorderid"></param>
		/// <returns>实体</returns>
		public WorkorderInfoModel Retrieve(string workorderid)
		{
			WorkorderInfoModel workorderinfo = new WorkorderInfoModel();
			workorderinfo.WorkOrderId = workorderid;
		
			DataTable dt = DbUtil.Current.Retrieve(workorderinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workorderinfo.ConvertFrom(dt);

			return workorderinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderInfoModel> workorderinfos = new List<WorkorderInfoModel>();

			WorkorderInfoModel workorderinfo = new WorkorderInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workorderinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workorderinfo = new WorkorderInfoModel();
				workorderinfo.ConvertFrom(dt, i);
				workorderinfos.Add(workorderinfo);
			}

			return workorderinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderInfoModel workorderinfo)
		{
			int ret = 0;

			workorderinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workorderinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderInfoModel workorderinfo, ParameterCollection pc)
		{
			int ret = 0;

			workorderinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workorderinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="workorderid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string workorderid)
		{
			int ret = 0;

			WorkorderInfoModel workorderinfo = new WorkorderInfoModel();
			workorderinfo.WorkOrderId = workorderid;
			
			ret = DbUtil.Current.Delete(workorderinfo);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int DeleteMultiple(ParameterCollection pc)
		{
			int ret = 0;

			WorkorderInfoModel workorderinfo = new WorkorderInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workorderinfo, pc);

			return ret;
		}
	}
}

