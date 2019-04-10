/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-27
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
	/// SalesPackageInfo业务逻辑类
	/// </summary>
	public partial class SalesPackageInfoService
	{
		// 实例
		private static SalesPackageInfoService _instance = new SalesPackageInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesPackageInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesPackageInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salespackageinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesPackageInfoModel salespackageinfo)
		{
			int ret = 0;

			salespackageinfo.CreatedBy = SessionUtil.Current.UserId;
			salespackageinfo.CreatedOn = DateTime.Now;
			salespackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salespackageinfo.ModifiedOn = DateTime.Now;
			salespackageinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salespackageinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salespackageid"></param>
		/// <returns>实体</returns>
		public SalesPackageInfoModel Retrieve(string salespackageid)
		{
			SalesPackageInfoModel salespackageinfo = new SalesPackageInfoModel();
			salespackageinfo.SalesPackageId = salespackageid;
		
			DataTable dt = DbUtil.Current.Retrieve(salespackageinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salespackageinfo.ConvertFrom(dt);

			return salespackageinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesPackageInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesPackageInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesPackageInfoModel> salespackageinfos = new List<SalesPackageInfoModel>();

			SalesPackageInfoModel salespackageinfo = new SalesPackageInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salespackageinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salespackageinfo = new SalesPackageInfoModel();
				salespackageinfo.ConvertFrom(dt, i);
				salespackageinfos.Add(salespackageinfo);
			}

			return salespackageinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salespackageinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesPackageInfoModel salespackageinfo)
		{
			int ret = 0;

			salespackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salespackageinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salespackageinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salespackageinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesPackageInfoModel salespackageinfo, ParameterCollection pc)
		{
			int ret = 0;

			salespackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salespackageinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salespackageinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salespackageid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salespackageid)
		{
			int ret = 0;

			SalesPackageInfoModel salespackageinfo = new SalesPackageInfoModel();
			salespackageinfo.SalesPackageId = salespackageid;
			
			ret = DbUtil.Current.Delete(salespackageinfo);

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

			SalesPackageInfoModel salespackageinfo = new SalesPackageInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salespackageinfo, pc);

			return ret;
		}
	}
}

