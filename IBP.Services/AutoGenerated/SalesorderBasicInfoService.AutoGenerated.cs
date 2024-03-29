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
	/// SalesorderBasicInfo业务逻辑类
	/// </summary>
	public partial class SalesorderBasicInfoService
	{
		// 实例
		private static SalesorderBasicInfoService _instance = new SalesorderBasicInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderBasicInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderBasicInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesorderbasicinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderBasicInfoModel salesorderbasicinfo)
		{
			int ret = 0;

			salesorderbasicinfo.CreatedBy = SessionUtil.Current.UserId;
			salesorderbasicinfo.CreatedOn = DateTime.Now;
			salesorderbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderbasicinfo.ModifiedOn = DateTime.Now;
			salesorderbasicinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salesorderbasicinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesorderid"></param>
		/// <returns>实体</returns>
		public SalesorderBasicInfoModel Retrieve(string salesorderid)
		{
			SalesorderBasicInfoModel salesorderbasicinfo = new SalesorderBasicInfoModel();
			salesorderbasicinfo.SalesorderId = salesorderid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesorderbasicinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesorderbasicinfo.ConvertFrom(dt);

			return salesorderbasicinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderBasicInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderBasicInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderBasicInfoModel> salesorderbasicinfos = new List<SalesorderBasicInfoModel>();

			SalesorderBasicInfoModel salesorderbasicinfo = new SalesorderBasicInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesorderbasicinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesorderbasicinfo = new SalesorderBasicInfoModel();
				salesorderbasicinfo.ConvertFrom(dt, i);
				salesorderbasicinfos.Add(salesorderbasicinfo);
			}

			return salesorderbasicinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderbasicinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderBasicInfoModel salesorderbasicinfo)
		{
			int ret = 0;

			salesorderbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderbasicinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesorderbasicinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderbasicinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderBasicInfoModel salesorderbasicinfo, ParameterCollection pc)
		{
			int ret = 0;

			salesorderbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderbasicinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesorderbasicinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesorderid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesorderid)
		{
			int ret = 0;

			SalesorderBasicInfoModel salesorderbasicinfo = new SalesorderBasicInfoModel();
			salesorderbasicinfo.SalesorderId = salesorderid;
			
			ret = DbUtil.Current.Delete(salesorderbasicinfo);

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

			SalesorderBasicInfoModel salesorderbasicinfo = new SalesorderBasicInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salesorderbasicinfo, pc);

			return ret;
		}
	}
}

