/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-22
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
	/// AutoDialerTaskInfo业务逻辑类
	/// </summary>
	public partial class AutoDialerTaskInfoService
	{
		// 实例
		private static AutoDialerTaskInfoService _instance = new AutoDialerTaskInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private AutoDialerTaskInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static AutoDialerTaskInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="autodialertaskinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(AutoDialerTaskInfoModel autodialertaskinfo)
		{
			int ret = 0;

			autodialertaskinfo.CreatedBy = SessionUtil.Current.UserId;
			autodialertaskinfo.CreatedOn = DateTime.Now;
			autodialertaskinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertaskinfo.ModifiedOn = DateTime.Now;
			autodialertaskinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(autodialertaskinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="autodialertaskid"></param>
		/// <returns>实体</returns>
		public AutoDialerTaskInfoModel Retrieve(string autodialertaskid)
		{
			AutoDialerTaskInfoModel autodialertaskinfo = new AutoDialerTaskInfoModel();
			autodialertaskinfo.AutoDialerTaskId = autodialertaskid;
		
			DataTable dt = DbUtil.Current.Retrieve(autodialertaskinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			autodialertaskinfo.ConvertFrom(dt);

			return autodialertaskinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<AutoDialerTaskInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<AutoDialerTaskInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<AutoDialerTaskInfoModel> autodialertaskinfos = new List<AutoDialerTaskInfoModel>();

			AutoDialerTaskInfoModel autodialertaskinfo = new AutoDialerTaskInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(autodialertaskinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				autodialertaskinfo = new AutoDialerTaskInfoModel();
				autodialertaskinfo.ConvertFrom(dt, i);
				autodialertaskinfos.Add(autodialertaskinfo);
			}

			return autodialertaskinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="autodialertaskinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(AutoDialerTaskInfoModel autodialertaskinfo)
		{
			int ret = 0;

			autodialertaskinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertaskinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(autodialertaskinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="autodialertaskinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(AutoDialerTaskInfoModel autodialertaskinfo, ParameterCollection pc)
		{
			int ret = 0;

			autodialertaskinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertaskinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(autodialertaskinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="autodialertaskid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string autodialertaskid)
		{
			int ret = 0;

			AutoDialerTaskInfoModel autodialertaskinfo = new AutoDialerTaskInfoModel();
			autodialertaskinfo.AutoDialerTaskId = autodialertaskid;
			
			ret = DbUtil.Current.Delete(autodialertaskinfo);

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

			AutoDialerTaskInfoModel autodialertaskinfo = new AutoDialerTaskInfoModel();
			ret = DbUtil.Current.DeleteMultiple(autodialertaskinfo, pc);

			return ret;
		}
	}
}

