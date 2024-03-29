/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-13
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
	/// BankcardTypeInfo业务逻辑类
	/// </summary>
	public partial class BankcardTypeInfoService
	{
		// 实例
		private static BankcardTypeInfoService _instance = new BankcardTypeInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private BankcardTypeInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static BankcardTypeInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="bankcardtypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(BankcardTypeInfoModel bankcardtypeinfo)
		{
			int ret = 0;

			bankcardtypeinfo.CreatedBy = SessionUtil.Current.UserId;
			bankcardtypeinfo.CreatedOn = DateTime.Now;
			bankcardtypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			bankcardtypeinfo.ModifiedOn = DateTime.Now;
			bankcardtypeinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(bankcardtypeinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="bankcardtypeid"></param>
		/// <returns>实体</returns>
		public BankcardTypeInfoModel Retrieve(string bankcardtypeid)
		{
			BankcardTypeInfoModel bankcardtypeinfo = new BankcardTypeInfoModel();
			bankcardtypeinfo.BankcardTypeId = bankcardtypeid;
		
			DataTable dt = DbUtil.Current.Retrieve(bankcardtypeinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			bankcardtypeinfo.ConvertFrom(dt);

			return bankcardtypeinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<BankcardTypeInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<BankcardTypeInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<BankcardTypeInfoModel> bankcardtypeinfos = new List<BankcardTypeInfoModel>();

			BankcardTypeInfoModel bankcardtypeinfo = new BankcardTypeInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(bankcardtypeinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				bankcardtypeinfo = new BankcardTypeInfoModel();
				bankcardtypeinfo.ConvertFrom(dt, i);
				bankcardtypeinfos.Add(bankcardtypeinfo);
			}

			return bankcardtypeinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="bankcardtypeinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(BankcardTypeInfoModel bankcardtypeinfo)
		{
			int ret = 0;

			bankcardtypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			bankcardtypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(bankcardtypeinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="bankcardtypeinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(BankcardTypeInfoModel bankcardtypeinfo, ParameterCollection pc)
		{
			int ret = 0;

			bankcardtypeinfo.ModifiedBy = SessionUtil.Current.UserId;
			bankcardtypeinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(bankcardtypeinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="bankcardtypeid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string bankcardtypeid)
		{
			int ret = 0;

			BankcardTypeInfoModel bankcardtypeinfo = new BankcardTypeInfoModel();
			bankcardtypeinfo.BankcardTypeId = bankcardtypeid;
			
			ret = DbUtil.Current.Delete(bankcardtypeinfo);

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

			BankcardTypeInfoModel bankcardtypeinfo = new BankcardTypeInfoModel();
			ret = DbUtil.Current.DeleteMultiple(bankcardtypeinfo, pc);

			return ret;
		}
	}
}

