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
	/// CustomerBasicInfo业务逻辑类
	/// </summary>
	public partial class CustomerBasicInfoService
	{
		// 实例
		private static CustomerBasicInfoService _instance = new CustomerBasicInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerBasicInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerBasicInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerbasicinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerBasicInfoModel customerbasicinfo)
		{
			int ret = 0;

			customerbasicinfo.CreatedBy = SessionUtil.Current.UserId;
			customerbasicinfo.CreatedOn = DateTime.Now;
			customerbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerbasicinfo.ModifiedOn = DateTime.Now;
			customerbasicinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customerbasicinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="customerid"></param>
		/// <returns>实体</returns>
		public CustomerBasicInfoModel Retrieve(string customerid)
		{
			CustomerBasicInfoModel customerbasicinfo = new CustomerBasicInfoModel();
			customerbasicinfo.CustomerId = customerid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerbasicinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerbasicinfo.ConvertFrom(dt);

			return customerbasicinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerBasicInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerBasicInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerBasicInfoModel> customerbasicinfos = new List<CustomerBasicInfoModel>();

			CustomerBasicInfoModel customerbasicinfo = new CustomerBasicInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerbasicinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerbasicinfo = new CustomerBasicInfoModel();
				customerbasicinfo.ConvertFrom(dt, i);
				customerbasicinfos.Add(customerbasicinfo);
			}

			return customerbasicinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerbasicinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerBasicInfoModel customerbasicinfo)
		{
			int ret = 0;

			customerbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerbasicinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerbasicinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerbasicinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerBasicInfoModel customerbasicinfo, ParameterCollection pc)
		{
			int ret = 0;

			customerbasicinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerbasicinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerbasicinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="customerid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string customerid)
		{
			int ret = 0;

			CustomerBasicInfoModel customerbasicinfo = new CustomerBasicInfoModel();
			customerbasicinfo.CustomerId = customerid;
			
			ret = DbUtil.Current.Delete(customerbasicinfo);

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

			CustomerBasicInfoModel customerbasicinfo = new CustomerBasicInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customerbasicinfo, pc);

			return ret;
		}
	}
}

