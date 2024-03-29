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
	/// SalesorderProcessLog业务逻辑类
	/// </summary>
	public partial class SalesorderProcessLogService
	{
		// 实例
		private static SalesorderProcessLogService _instance = new SalesorderProcessLogService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderProcessLogService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderProcessLogService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesorderprocesslog">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderProcessLogModel salesorderprocesslog)
		{
			int ret = 0;

			salesorderprocesslog.CreatedBy = SessionUtil.Current.UserId;
			salesorderprocesslog.CreatedOn = DateTime.Now;
			salesorderprocesslog.ModifiedBy = SessionUtil.Current.UserId;
			salesorderprocesslog.ModifiedOn = DateTime.Now;
			salesorderprocesslog.StatusCode = 0;

			ret = DbUtil.Current.Create(salesorderprocesslog);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesorderprocessid"></param>
		/// <returns>实体</returns>
		public SalesorderProcessLogModel Retrieve(string salesorderprocessid)
		{
			SalesorderProcessLogModel salesorderprocesslog = new SalesorderProcessLogModel();
			salesorderprocesslog.SalesorderProcessId = salesorderprocessid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesorderprocesslog);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesorderprocesslog.ConvertFrom(dt);

			return salesorderprocesslog;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderProcessLogModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderProcessLogModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderProcessLogModel> salesorderprocesslogs = new List<SalesorderProcessLogModel>();

			SalesorderProcessLogModel salesorderprocesslog = new SalesorderProcessLogModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesorderprocesslog, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesorderprocesslog = new SalesorderProcessLogModel();
				salesorderprocesslog.ConvertFrom(dt, i);
				salesorderprocesslogs.Add(salesorderprocesslog);
			}

			return salesorderprocesslogs;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderprocesslog">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderProcessLogModel salesorderprocesslog)
		{
			int ret = 0;

			salesorderprocesslog.ModifiedBy = SessionUtil.Current.UserId;
			salesorderprocesslog.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesorderprocesslog);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderprocesslog">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderProcessLogModel salesorderprocesslog, ParameterCollection pc)
		{
			int ret = 0;

			salesorderprocesslog.ModifiedBy = SessionUtil.Current.UserId;
			salesorderprocesslog.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesorderprocesslog, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesorderprocessid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesorderprocessid)
		{
			int ret = 0;

			SalesorderProcessLogModel salesorderprocesslog = new SalesorderProcessLogModel();
			salesorderprocesslog.SalesorderProcessId = salesorderprocessid;
			
			ret = DbUtil.Current.Delete(salesorderprocesslog);

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

			SalesorderProcessLogModel salesorderprocesslog = new SalesorderProcessLogModel();
			ret = DbUtil.Current.DeleteMultiple(salesorderprocesslog, pc);

			return ret;
		}
	}
}

