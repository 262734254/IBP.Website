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
	/// SalesorderTypeStatusInfo业务逻辑类
	/// </summary>
	public partial class SalesorderTypeStatusInfoService
	{
		// 实例
		private static SalesorderTypeStatusInfoService _instance = new SalesorderTypeStatusInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderTypeStatusInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderTypeStatusInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesordertypestatusinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderTypeStatusInfoModel salesordertypestatusinfo)
		{
			int ret = 0;

			salesordertypestatusinfo.CreatedBy = SessionUtil.Current.UserId;
			salesordertypestatusinfo.CreatedOn = DateTime.Now;
			salesordertypestatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypestatusinfo.ModifiedOn = DateTime.Now;
			salesordertypestatusinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salesordertypestatusinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salsorderstatusid"></param>
		/// <returns>实体</returns>
		public SalesorderTypeStatusInfoModel Retrieve(string salsorderstatusid)
		{
			SalesorderTypeStatusInfoModel salesordertypestatusinfo = new SalesorderTypeStatusInfoModel();
			salesordertypestatusinfo.SalsorderStatusId = salsorderstatusid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesordertypestatusinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesordertypestatusinfo.ConvertFrom(dt);

			return salesordertypestatusinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderTypeStatusInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderTypeStatusInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderTypeStatusInfoModel> salesordertypestatusinfos = new List<SalesorderTypeStatusInfoModel>();

			SalesorderTypeStatusInfoModel salesordertypestatusinfo = new SalesorderTypeStatusInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesordertypestatusinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesordertypestatusinfo = new SalesorderTypeStatusInfoModel();
				salesordertypestatusinfo.ConvertFrom(dt, i);
				salesordertypestatusinfos.Add(salesordertypestatusinfo);
			}

			return salesordertypestatusinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordertypestatusinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderTypeStatusInfoModel salesordertypestatusinfo)
		{
			int ret = 0;

			salesordertypestatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypestatusinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesordertypestatusinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordertypestatusinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderTypeStatusInfoModel salesordertypestatusinfo, ParameterCollection pc)
		{
			int ret = 0;

			salesordertypestatusinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordertypestatusinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesordertypestatusinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salsorderstatusid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salsorderstatusid)
		{
			int ret = 0;

			SalesorderTypeStatusInfoModel salesordertypestatusinfo = new SalesorderTypeStatusInfoModel();
			salesordertypestatusinfo.SalsorderStatusId = salsorderstatusid;
			
			ret = DbUtil.Current.Delete(salesordertypestatusinfo);

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

			SalesorderTypeStatusInfoModel salesordertypestatusinfo = new SalesorderTypeStatusInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salesordertypestatusinfo, pc);

			return ret;
		}
	}
}

