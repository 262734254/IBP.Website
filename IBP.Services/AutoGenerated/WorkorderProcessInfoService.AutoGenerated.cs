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
	/// WorkorderProcessInfo业务逻辑类
	/// </summary>
	public partial class WorkorderProcessInfoService
	{
		// 实例
		private static WorkorderProcessInfoService _instance = new WorkorderProcessInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderProcessInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderProcessInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workorderprocessinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderProcessInfoModel workorderprocessinfo)
		{
			int ret = 0;

			workorderprocessinfo.CreatedBy = SessionUtil.Current.UserId;
			workorderprocessinfo.CreatedOn = DateTime.Now;
			workorderprocessinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderprocessinfo.ModifiedOn = DateTime.Now;
			workorderprocessinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workorderprocessinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="processid"></param>
		/// <returns>实体</returns>
		public WorkorderProcessInfoModel Retrieve(string processid)
		{
			WorkorderProcessInfoModel workorderprocessinfo = new WorkorderProcessInfoModel();
			workorderprocessinfo.ProcessId = processid;
		
			DataTable dt = DbUtil.Current.Retrieve(workorderprocessinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workorderprocessinfo.ConvertFrom(dt);

			return workorderprocessinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderProcessInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderProcessInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderProcessInfoModel> workorderprocessinfos = new List<WorkorderProcessInfoModel>();

			WorkorderProcessInfoModel workorderprocessinfo = new WorkorderProcessInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workorderprocessinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workorderprocessinfo = new WorkorderProcessInfoModel();
				workorderprocessinfo.ConvertFrom(dt, i);
				workorderprocessinfos.Add(workorderprocessinfo);
			}

			return workorderprocessinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderprocessinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderProcessInfoModel workorderprocessinfo)
		{
			int ret = 0;

			workorderprocessinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderprocessinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workorderprocessinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderprocessinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderProcessInfoModel workorderprocessinfo, ParameterCollection pc)
		{
			int ret = 0;

			workorderprocessinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderprocessinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workorderprocessinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="processid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string processid)
		{
			int ret = 0;

			WorkorderProcessInfoModel workorderprocessinfo = new WorkorderProcessInfoModel();
			workorderprocessinfo.ProcessId = processid;
			
			ret = DbUtil.Current.Delete(workorderprocessinfo);

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

			WorkorderProcessInfoModel workorderprocessinfo = new WorkorderProcessInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workorderprocessinfo, pc);

			return ret;
		}
	}
}

