/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-12
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
	/// SalesorderTypeInfo业务逻辑类
	/// </summary>
	public partial class SalesorderTypeInfoService
	{
		// 实例
		private static SalesorderTypeInfoService _instance = new SalesorderTypeInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderTypeInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderTypeInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesordertypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderTypeInfoModel salesordertypeinfo)
		{
			int ret = 0;

			salesordertypeinfo.CreatedBy = SessionUtil.Current.UserId;
			salesordertypeinfo.CreatedOn = DateTime.Now;
			salesordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypeinfo.ModifiedOn = DateTime.Now;
			salesordertypeinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salesordertypeinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesordertypeid"></param>
		/// <returns>实体</returns>
		public SalesorderTypeInfoModel Retrieve(string salesordertypeid)
		{
			SalesorderTypeInfoModel salesordertypeinfo = new SalesorderTypeInfoModel();
			salesordertypeinfo.SalesorderTypeId = salesordertypeid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesordertypeinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesordertypeinfo.ConvertFrom(dt);

			return salesordertypeinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderTypeInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderTypeInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderTypeInfoModel> salesordertypeinfos = new List<SalesorderTypeInfoModel>();

			SalesorderTypeInfoModel salesordertypeinfo = new SalesorderTypeInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesordertypeinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesordertypeinfo = new SalesorderTypeInfoModel();
				salesordertypeinfo.ConvertFrom(dt, i);
				salesordertypeinfos.Add(salesordertypeinfo);
			}

			return salesordertypeinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordertypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderTypeInfoModel salesordertypeinfo)
		{
			int ret = 0;

			salesordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesordertypeinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordertypeinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderTypeInfoModel salesordertypeinfo, ParameterCollection pc)
		{
			int ret = 0;

			salesordertypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesordertypeinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesordertypeid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesordertypeid)
		{
			int ret = 0;

			SalesorderTypeInfoModel salesordertypeinfo = new SalesorderTypeInfoModel();
			salesordertypeinfo.SalesorderTypeId = salesordertypeid;
			
			ret = DbUtil.Current.Delete(salesordertypeinfo);

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

			SalesorderTypeInfoModel salesordertypeinfo = new SalesorderTypeInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salesordertypeinfo, pc);

			return ret;
		}
	}
}

