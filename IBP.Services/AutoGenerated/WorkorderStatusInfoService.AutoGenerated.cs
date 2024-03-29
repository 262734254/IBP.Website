/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
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
	/// WorkorderStatusInfo业务逻辑类
	/// </summary>
	public partial class WorkorderStatusInfoService
	{
		// 实例
		private static WorkorderStatusInfoService _instance = new WorkorderStatusInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderStatusInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderStatusInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workorderstatusinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderStatusInfoModel workorderstatusinfo)
		{
			int ret = 0;

			workorderstatusinfo.CreatedBy = SessionUtil.Current.UserId;
			workorderstatusinfo.CreatedOn = DateTime.Now;
			workorderstatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderstatusinfo.ModifiedOn = DateTime.Now;
			workorderstatusinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workorderstatusinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="workorderstatusid"></param>
		/// <returns>实体</returns>
		public WorkorderStatusInfoModel Retrieve(string workorderstatusid)
		{
			WorkorderStatusInfoModel workorderstatusinfo = new WorkorderStatusInfoModel();
			workorderstatusinfo.WorkorderStatusId = workorderstatusid;
		
			DataTable dt = DbUtil.Current.Retrieve(workorderstatusinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workorderstatusinfo.ConvertFrom(dt);

			return workorderstatusinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderStatusInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderStatusInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderStatusInfoModel> workorderstatusinfos = new List<WorkorderStatusInfoModel>();

			WorkorderStatusInfoModel workorderstatusinfo = new WorkorderStatusInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workorderstatusinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workorderstatusinfo = new WorkorderStatusInfoModel();
				workorderstatusinfo.ConvertFrom(dt, i);
				workorderstatusinfos.Add(workorderstatusinfo);
			}

			return workorderstatusinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderstatusinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderStatusInfoModel workorderstatusinfo)
		{
			int ret = 0;

			workorderstatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderstatusinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workorderstatusinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderstatusinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderStatusInfoModel workorderstatusinfo, ParameterCollection pc)
		{
			int ret = 0;

			workorderstatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderstatusinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workorderstatusinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="workorderstatusid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string workorderstatusid)
		{
			int ret = 0;

			WorkorderStatusInfoModel workorderstatusinfo = new WorkorderStatusInfoModel();
			workorderstatusinfo.WorkorderStatusId = workorderstatusid;
			
			ret = DbUtil.Current.Delete(workorderstatusinfo);

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

			WorkorderStatusInfoModel workorderstatusinfo = new WorkorderStatusInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workorderstatusinfo, pc);

			return ret;
		}
	}
}

