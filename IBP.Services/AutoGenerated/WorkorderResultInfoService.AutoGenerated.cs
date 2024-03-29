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
	/// WorkorderResultInfo业务逻辑类
	/// </summary>
	public partial class WorkorderResultInfoService
	{
		// 实例
		private static WorkorderResultInfoService _instance = new WorkorderResultInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderResultInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderResultInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workorderresultinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderResultInfoModel workorderresultinfo)
		{
			int ret = 0;

			workorderresultinfo.CreatedBy = SessionUtil.Current.UserId;
			workorderresultinfo.CreatedOn = DateTime.Now;
			workorderresultinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderresultinfo.ModifiedOn = DateTime.Now;
			workorderresultinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workorderresultinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="workorderresultid"></param>
		/// <returns>实体</returns>
		public WorkorderResultInfoModel Retrieve(string workorderresultid)
		{
			WorkorderResultInfoModel workorderresultinfo = new WorkorderResultInfoModel();
			workorderresultinfo.WorkorderResultId = workorderresultid;
		
			DataTable dt = DbUtil.Current.Retrieve(workorderresultinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workorderresultinfo.ConvertFrom(dt);

			return workorderresultinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderResultInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderResultInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderResultInfoModel> workorderresultinfos = new List<WorkorderResultInfoModel>();

			WorkorderResultInfoModel workorderresultinfo = new WorkorderResultInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workorderresultinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workorderresultinfo = new WorkorderResultInfoModel();
				workorderresultinfo.ConvertFrom(dt, i);
				workorderresultinfos.Add(workorderresultinfo);
			}

			return workorderresultinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderresultinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderResultInfoModel workorderresultinfo)
		{
			int ret = 0;

			workorderresultinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderresultinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workorderresultinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderresultinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderResultInfoModel workorderresultinfo, ParameterCollection pc)
		{
			int ret = 0;

			workorderresultinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderresultinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workorderresultinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="workorderresultid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string workorderresultid)
		{
			int ret = 0;

			WorkorderResultInfoModel workorderresultinfo = new WorkorderResultInfoModel();
			workorderresultinfo.WorkorderResultId = workorderresultid;
			
			ret = DbUtil.Current.Delete(workorderresultinfo);

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

			WorkorderResultInfoModel workorderresultinfo = new WorkorderResultInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workorderresultinfo, pc);

			return ret;
		}
	}
}

