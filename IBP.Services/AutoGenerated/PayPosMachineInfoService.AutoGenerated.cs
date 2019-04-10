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
	/// PayPosMachineInfo业务逻辑类
	/// </summary>
	public partial class PayPosMachineInfoService
	{
		// 实例
		private static PayPosMachineInfoService _instance = new PayPosMachineInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private PayPosMachineInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static PayPosMachineInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="payposmachineinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(PayPosMachineInfoModel payposmachineinfo)
		{
			int ret = 0;

			payposmachineinfo.CreatedBy = SessionUtil.Current.UserId;
			payposmachineinfo.CreatedOn = DateTime.Now;
			payposmachineinfo.ModifiedBy = SessionUtil.Current.UserId;
			payposmachineinfo.ModifiedOn = DateTime.Now;
			payposmachineinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(payposmachineinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="posmachineid"></param>
		/// <returns>实体</returns>
		public PayPosMachineInfoModel Retrieve(string posmachineid)
		{
			PayPosMachineInfoModel payposmachineinfo = new PayPosMachineInfoModel();
			payposmachineinfo.PosMachineId = posmachineid;
		
			DataTable dt = DbUtil.Current.Retrieve(payposmachineinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			payposmachineinfo.ConvertFrom(dt);

			return payposmachineinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<PayPosMachineInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<PayPosMachineInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<PayPosMachineInfoModel> payposmachineinfos = new List<PayPosMachineInfoModel>();

			PayPosMachineInfoModel payposmachineinfo = new PayPosMachineInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(payposmachineinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				payposmachineinfo = new PayPosMachineInfoModel();
				payposmachineinfo.ConvertFrom(dt, i);
				payposmachineinfos.Add(payposmachineinfo);
			}

			return payposmachineinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="payposmachineinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(PayPosMachineInfoModel payposmachineinfo)
		{
			int ret = 0;

			payposmachineinfo.ModifiedBy = SessionUtil.Current.UserId;
			payposmachineinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(payposmachineinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="payposmachineinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(PayPosMachineInfoModel payposmachineinfo, ParameterCollection pc)
		{
			int ret = 0;

			payposmachineinfo.ModifiedBy = SessionUtil.Current.UserId;
			payposmachineinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(payposmachineinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="posmachineid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string posmachineid)
		{
			int ret = 0;

			PayPosMachineInfoModel payposmachineinfo = new PayPosMachineInfoModel();
			payposmachineinfo.PosMachineId = posmachineid;
			
			ret = DbUtil.Current.Delete(payposmachineinfo);

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

			PayPosMachineInfoModel payposmachineinfo = new PayPosMachineInfoModel();
			ret = DbUtil.Current.DeleteMultiple(payposmachineinfo, pc);

			return ret;
		}
	}
}

