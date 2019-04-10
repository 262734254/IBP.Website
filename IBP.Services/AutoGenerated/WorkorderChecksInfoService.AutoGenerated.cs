/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-3-26
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
	/// WorkorderChecksInfo业务逻辑类
	/// </summary>
	public partial class WorkorderChecksInfoService
	{
		// 实例
		private static WorkorderChecksInfoService _instance = new WorkorderChecksInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderChecksInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderChecksInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workorderchecksinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderChecksInfoModel workorderchecksinfo)
		{
			int ret = 0;

			workorderchecksinfo.CreatedBy = SessionUtil.Current.UserId;
			workorderchecksinfo.CreatedOn = DateTime.Now;
			workorderchecksinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderchecksinfo.ModifiedOn = DateTime.Now;
			workorderchecksinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workorderchecksinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="workordercheckid"></param>
		/// <returns>实体</returns>
		public WorkorderChecksInfoModel Retrieve(string workordercheckid)
		{
			WorkorderChecksInfoModel workorderchecksinfo = new WorkorderChecksInfoModel();
			workorderchecksinfo.WorkorderCheckId = workordercheckid;
		
			DataTable dt = DbUtil.Current.Retrieve(workorderchecksinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workorderchecksinfo.ConvertFrom(dt);

			return workorderchecksinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderChecksInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderChecksInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderChecksInfoModel> workorderchecksinfos = new List<WorkorderChecksInfoModel>();

			WorkorderChecksInfoModel workorderchecksinfo = new WorkorderChecksInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workorderchecksinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workorderchecksinfo = new WorkorderChecksInfoModel();
				workorderchecksinfo.ConvertFrom(dt, i);
				workorderchecksinfos.Add(workorderchecksinfo);
			}

			return workorderchecksinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderchecksinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderChecksInfoModel workorderchecksinfo)
		{
			int ret = 0;

			workorderchecksinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderchecksinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workorderchecksinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workorderchecksinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderChecksInfoModel workorderchecksinfo, ParameterCollection pc)
		{
			int ret = 0;

			workorderchecksinfo.ModifiedBy = SessionUtil.Current.UserId;
			workorderchecksinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workorderchecksinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="workordercheckid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string workordercheckid)
		{
			int ret = 0;

			WorkorderChecksInfoModel workorderchecksinfo = new WorkorderChecksInfoModel();
			workorderchecksinfo.WorkorderCheckId = workordercheckid;
			
			ret = DbUtil.Current.Delete(workorderchecksinfo);

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

			WorkorderChecksInfoModel workorderchecksinfo = new WorkorderChecksInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workorderchecksinfo, pc);

			return ret;
		}
	}
}

