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
	/// AutoDialerTaskNumberInfo业务逻辑类
	/// </summary>
	public partial class AutoDialerTaskNumberInfoService
	{
		// 实例
		private static AutoDialerTaskNumberInfoService _instance = new AutoDialerTaskNumberInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private AutoDialerTaskNumberInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static AutoDialerTaskNumberInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="autodialertasknumberinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(AutoDialerTaskNumberInfoModel autodialertasknumberinfo)
		{
			int ret = 0;

			autodialertasknumberinfo.CreatedBy = SessionUtil.Current.UserId;
			autodialertasknumberinfo.CreatedOn = DateTime.Now;
			autodialertasknumberinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertasknumberinfo.ModifiedOn = DateTime.Now;
			autodialertasknumberinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(autodialertasknumberinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="autodialernumberid"></param>
		/// <returns>实体</returns>
		public AutoDialerTaskNumberInfoModel Retrieve(string autodialernumberid)
		{
			AutoDialerTaskNumberInfoModel autodialertasknumberinfo = new AutoDialerTaskNumberInfoModel();
			autodialertasknumberinfo.AutoDialerNumberId = autodialernumberid;
		
			DataTable dt = DbUtil.Current.Retrieve(autodialertasknumberinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			autodialertasknumberinfo.ConvertFrom(dt);

			return autodialertasknumberinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<AutoDialerTaskNumberInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<AutoDialerTaskNumberInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<AutoDialerTaskNumberInfoModel> autodialertasknumberinfos = new List<AutoDialerTaskNumberInfoModel>();

			AutoDialerTaskNumberInfoModel autodialertasknumberinfo = new AutoDialerTaskNumberInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(autodialertasknumberinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				autodialertasknumberinfo = new AutoDialerTaskNumberInfoModel();
				autodialertasknumberinfo.ConvertFrom(dt, i);
				autodialertasknumberinfos.Add(autodialertasknumberinfo);
			}

			return autodialertasknumberinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="autodialertasknumberinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(AutoDialerTaskNumberInfoModel autodialertasknumberinfo)
		{
			int ret = 0;

			autodialertasknumberinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertasknumberinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(autodialertasknumberinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="autodialertasknumberinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(AutoDialerTaskNumberInfoModel autodialertasknumberinfo, ParameterCollection pc)
		{
			int ret = 0;

			autodialertasknumberinfo.ModifiedBy = SessionUtil.Current.UserId;
			autodialertasknumberinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(autodialertasknumberinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="autodialernumberid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string autodialernumberid)
		{
			int ret = 0;

			AutoDialerTaskNumberInfoModel autodialertasknumberinfo = new AutoDialerTaskNumberInfoModel();
			autodialertasknumberinfo.AutoDialerNumberId = autodialernumberid;
			
			ret = DbUtil.Current.Delete(autodialertasknumberinfo);

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

			AutoDialerTaskNumberInfoModel autodialertasknumberinfo = new AutoDialerTaskNumberInfoModel();
			ret = DbUtil.Current.DeleteMultiple(autodialertasknumberinfo, pc);

			return ret;
		}
	}
}

