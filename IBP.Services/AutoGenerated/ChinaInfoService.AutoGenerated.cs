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
	/// ChinaInfo业务逻辑类
	/// </summary>
	public partial class ChinaInfoService
	{
		// 实例
		private static ChinaInfoService _instance = new ChinaInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ChinaInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ChinaInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="chinainfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ChinaInfoModel chinainfo)
		{
			int ret = 0;

			chinainfo.CreatedBy = SessionUtil.Current.UserId;
			chinainfo.CreatedOn = DateTime.Now;
			chinainfo.ModifiedBy = SessionUtil.Current.UserId;
			chinainfo.ModifiedOn = DateTime.Now;
			chinainfo.StatusCode = 0;

			ret = DbUtil.Current.Create(chinainfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <returns>实体</returns>
		public ChinaInfoModel Retrieve()
		{
			ChinaInfoModel chinainfo = new ChinaInfoModel();
		
			DataTable dt = DbUtil.Current.Retrieve(chinainfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			chinainfo.ConvertFrom(dt);

			return chinainfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ChinaInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ChinaInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ChinaInfoModel> chinainfos = new List<ChinaInfoModel>();

			ChinaInfoModel chinainfo = new ChinaInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(chinainfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				chinainfo = new ChinaInfoModel();
				chinainfo.ConvertFrom(dt, i);
				chinainfos.Add(chinainfo);
			}

			return chinainfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="chinainfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ChinaInfoModel chinainfo)
		{
			int ret = 0;

			chinainfo.ModifiedBy = SessionUtil.Current.UserId;
			chinainfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(chinainfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="chinainfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ChinaInfoModel chinainfo, ParameterCollection pc)
		{
			int ret = 0;

			chinainfo.ModifiedBy = SessionUtil.Current.UserId;
			chinainfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(chinainfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>影响的记录行数</returns>
		public int Delete()
		{
			int ret = 0;

			ChinaInfoModel chinainfo = new ChinaInfoModel();
			
			ret = DbUtil.Current.Delete(chinainfo);

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

			ChinaInfoModel chinainfo = new ChinaInfoModel();
			ret = DbUtil.Current.DeleteMultiple(chinainfo, pc);

			return ret;
		}
	}
}

