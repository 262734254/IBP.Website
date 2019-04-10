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
	/// WorkorderTypeInfo业务逻辑类
	/// </summary>
	public partial class WorkorderTypeInfoService
	{
		// 实例
		private static WorkorderTypeInfoService _instance = new WorkorderTypeInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private WorkorderTypeInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static WorkorderTypeInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="workordertypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(WorkorderTypeInfoModel workordertypeinfo)
		{
			int ret = 0;

			workordertypeinfo.CreatedBy = SessionUtil.Current.UserId;
			workordertypeinfo.CreatedOn = DateTime.Now;
			workordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			workordertypeinfo.ModifiedOn = DateTime.Now;
			workordertypeinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(workordertypeinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="workordertypeid"></param>
		/// <returns>实体</returns>
		public WorkorderTypeInfoModel Retrieve(string workordertypeid)
		{
			WorkorderTypeInfoModel workordertypeinfo = new WorkorderTypeInfoModel();
			workordertypeinfo.WorkorderTypeId = workordertypeid;
		
			DataTable dt = DbUtil.Current.Retrieve(workordertypeinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			workordertypeinfo.ConvertFrom(dt);

			return workordertypeinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<WorkorderTypeInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<WorkorderTypeInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<WorkorderTypeInfoModel> workordertypeinfos = new List<WorkorderTypeInfoModel>();

			WorkorderTypeInfoModel workordertypeinfo = new WorkorderTypeInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(workordertypeinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				workordertypeinfo = new WorkorderTypeInfoModel();
				workordertypeinfo.ConvertFrom(dt, i);
				workordertypeinfos.Add(workordertypeinfo);
			}

			return workordertypeinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workordertypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(WorkorderTypeInfoModel workordertypeinfo)
		{
			int ret = 0;

			workordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			workordertypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(workordertypeinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="workordertypeinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(WorkorderTypeInfoModel workordertypeinfo, ParameterCollection pc)
		{
			int ret = 0;

			workordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			workordertypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(workordertypeinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="workordertypeid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string workordertypeid)
		{
			int ret = 0;

			WorkorderTypeInfoModel workordertypeinfo = new WorkorderTypeInfoModel();
			workordertypeinfo.WorkorderTypeId = workordertypeid;
			
			ret = DbUtil.Current.Delete(workordertypeinfo);

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

			WorkorderTypeInfoModel workordertypeinfo = new WorkorderTypeInfoModel();
			ret = DbUtil.Current.DeleteMultiple(workordertypeinfo, pc);

			return ret;
		}
	}
}

