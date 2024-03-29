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
	/// SalesorderCommuniationpackageInfo业务逻辑类
	/// </summary>
	public partial class SalesorderCommuniationpackageInfoService
	{
		// 实例
		private static SalesorderCommuniationpackageInfoService _instance = new SalesorderCommuniationpackageInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private SalesorderCommuniationpackageInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static SalesorderCommuniationpackageInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="salesordercommuniationpackageinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo)
		{
			int ret = 0;

			salesordercommuniationpackageinfo.CreatedBy = SessionUtil.Current.UserId;
			salesordercommuniationpackageinfo.CreatedOn = DateTime.Now;
			salesordercommuniationpackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordercommuniationpackageinfo.ModifiedOn = DateTime.Now;
			salesordercommuniationpackageinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(salesordercommuniationpackageinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesordercommunicationpackageid"></param>
		/// <returns>实体</returns>
		public SalesorderCommuniationpackageInfoModel Retrieve(string salesordercommunicationpackageid)
		{
			SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo = new SalesorderCommuniationpackageInfoModel();
			salesordercommuniationpackageinfo.SalesorderCommunicationpackageId = salesordercommunicationpackageid;
		
			DataTable dt = DbUtil.Current.Retrieve(salesordercommuniationpackageinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			salesordercommuniationpackageinfo.ConvertFrom(dt);

			return salesordercommuniationpackageinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<SalesorderCommuniationpackageInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<SalesorderCommuniationpackageInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<SalesorderCommuniationpackageInfoModel> salesordercommuniationpackageinfos = new List<SalesorderCommuniationpackageInfoModel>();

			SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo = new SalesorderCommuniationpackageInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(salesordercommuniationpackageinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				salesordercommuniationpackageinfo = new SalesorderCommuniationpackageInfoModel();
				salesordercommuniationpackageinfo.ConvertFrom(dt, i);
				salesordercommuniationpackageinfos.Add(salesordercommuniationpackageinfo);
			}

			return salesordercommuniationpackageinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordercommuniationpackageinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo)
		{
			int ret = 0;

			salesordercommuniationpackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordercommuniationpackageinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(salesordercommuniationpackageinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="salesordercommuniationpackageinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo, ParameterCollection pc)
		{
			int ret = 0;

			salesordercommuniationpackageinfo.ModifiedBy = SessionUtil.Current.UserId;
			salesordercommuniationpackageinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(salesordercommuniationpackageinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesordercommunicationpackageid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesordercommunicationpackageid)
		{
			int ret = 0;

			SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo = new SalesorderCommuniationpackageInfoModel();
			salesordercommuniationpackageinfo.SalesorderCommunicationpackageId = salesordercommunicationpackageid;
			
			ret = DbUtil.Current.Delete(salesordercommuniationpackageinfo);

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

			SalesorderCommuniationpackageInfoModel salesordercommuniationpackageinfo = new SalesorderCommuniationpackageInfoModel();
			ret = DbUtil.Current.DeleteMultiple(salesordercommuniationpackageinfo, pc);

			return ret;
		}
	}
}

