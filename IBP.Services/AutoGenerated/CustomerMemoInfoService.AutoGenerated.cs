/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
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
	/// CustomerMemoInfo业务逻辑类
	/// </summary>
	public partial class CustomerMemoInfoService
	{
		// 实例
		private static CustomerMemoInfoService _instance = new CustomerMemoInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerMemoInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerMemoInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customermemoinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerMemoInfoModel customermemoinfo)
		{
			int ret = 0;

			customermemoinfo.CreatedBy = SessionUtil.Current.UserId;
			customermemoinfo.CreatedOn = DateTime.Now;
			customermemoinfo.ModifiedBy = SessionUtil.Current.UserId;
			customermemoinfo.ModifiedOn = DateTime.Now;
			customermemoinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customermemoinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="memoid"></param>
		/// <returns>实体</returns>
		public CustomerMemoInfoModel Retrieve(string memoid)
		{
			CustomerMemoInfoModel customermemoinfo = new CustomerMemoInfoModel();
			customermemoinfo.MemoId = memoid;
		
			DataTable dt = DbUtil.Current.Retrieve(customermemoinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customermemoinfo.ConvertFrom(dt);

			return customermemoinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerMemoInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerMemoInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerMemoInfoModel> customermemoinfos = new List<CustomerMemoInfoModel>();

			CustomerMemoInfoModel customermemoinfo = new CustomerMemoInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customermemoinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customermemoinfo = new CustomerMemoInfoModel();
				customermemoinfo.ConvertFrom(dt, i);
				customermemoinfos.Add(customermemoinfo);
			}

			return customermemoinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customermemoinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerMemoInfoModel customermemoinfo)
		{
			int ret = 0;

			customermemoinfo.ModifiedBy = SessionUtil.Current.UserId;
			customermemoinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customermemoinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customermemoinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerMemoInfoModel customermemoinfo, ParameterCollection pc)
		{
			int ret = 0;

			customermemoinfo.ModifiedBy = SessionUtil.Current.UserId;
			customermemoinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customermemoinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="memoid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string memoid)
		{
			int ret = 0;

			CustomerMemoInfoModel customermemoinfo = new CustomerMemoInfoModel();
			customermemoinfo.MemoId = memoid;
			
			ret = DbUtil.Current.Delete(customermemoinfo);

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

			CustomerMemoInfoModel customermemoinfo = new CustomerMemoInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customermemoinfo, pc);

			return ret;
		}
	}
}

