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
	/// SalesorderProductInfo业务逻辑类
	/// </summary>
	public partial class SalesorderProductInfoService
	{
		// 实例
		private static SalesorderProductInfoService _instance = new SalesorderProductInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderProductInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderProductInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesorderproductinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderProductInfoModel salesorderproductinfo)
		{
			int ret = 0;

			salesorderproductinfo.CreatedBy = SessionUtil.Current.UserId;
			salesorderproductinfo.CreatedOn = DateTime.Now;
			salesorderproductinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderproductinfo.ModifiedOn = DateTime.Now;
			salesorderproductinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salesorderproductinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesorderproductitemid"></param>
		/// <returns>实体</returns>
		public SalesorderProductInfoModel Retrieve(string salesorderproductitemid)
		{
			SalesorderProductInfoModel salesorderproductinfo = new SalesorderProductInfoModel();
			salesorderproductinfo.SalesorderProductitemId = salesorderproductitemid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesorderproductinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesorderproductinfo.ConvertFrom(dt);

			return salesorderproductinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderProductInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderProductInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderProductInfoModel> salesorderproductinfos = new List<SalesorderProductInfoModel>();

			SalesorderProductInfoModel salesorderproductinfo = new SalesorderProductInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesorderproductinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesorderproductinfo = new SalesorderProductInfoModel();
				salesorderproductinfo.ConvertFrom(dt, i);
				salesorderproductinfos.Add(salesorderproductinfo);
			}

			return salesorderproductinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderproductinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderProductInfoModel salesorderproductinfo)
		{
			int ret = 0;

			salesorderproductinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderproductinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesorderproductinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesorderproductinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderProductInfoModel salesorderproductinfo, ParameterCollection pc)
		{
			int ret = 0;

			salesorderproductinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesorderproductinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesorderproductinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesorderproductitemid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesorderproductitemid)
		{
			int ret = 0;

			SalesorderProductInfoModel salesorderproductinfo = new SalesorderProductInfoModel();
			salesorderproductinfo.SalesorderProductitemId = salesorderproductitemid;
			
			ret = DbUtil.Current.Delete(salesorderproductinfo);

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

			SalesorderProductInfoModel salesorderproductinfo = new SalesorderProductInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salesorderproductinfo, pc);

			return ret;
		}
	}
}

