/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-11
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
	/// CustomerPhoneInfo业务逻辑类
	/// </summary>
	public partial class CustomerPhoneInfoService
	{
		// 实例
		private static CustomerPhoneInfoService _instance = new CustomerPhoneInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerPhoneInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerPhoneInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerphoneinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerPhoneInfoModel customerphoneinfo)
		{
			int ret = 0;

			customerphoneinfo.CreatedBy = SessionUtil.Current.UserId;
			customerphoneinfo.CreatedOn = DateTime.Now;
			customerphoneinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerphoneinfo.ModifiedOn = DateTime.Now;
			customerphoneinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customerphoneinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="phoneid"></param>
		/// <returns>实体</returns>
		public CustomerPhoneInfoModel Retrieve(string phoneid)
		{
			CustomerPhoneInfoModel customerphoneinfo = new CustomerPhoneInfoModel();
			customerphoneinfo.PhoneId = phoneid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerphoneinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerphoneinfo.ConvertFrom(dt);

			return customerphoneinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerPhoneInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerPhoneInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerPhoneInfoModel> customerphoneinfos = new List<CustomerPhoneInfoModel>();

			CustomerPhoneInfoModel customerphoneinfo = new CustomerPhoneInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerphoneinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerphoneinfo = new CustomerPhoneInfoModel();
				customerphoneinfo.ConvertFrom(dt, i);
				customerphoneinfos.Add(customerphoneinfo);
			}

			return customerphoneinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerphoneinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerPhoneInfoModel customerphoneinfo)
		{
			int ret = 0;

			customerphoneinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerphoneinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerphoneinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerphoneinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerPhoneInfoModel customerphoneinfo, ParameterCollection pc)
		{
			int ret = 0;

			customerphoneinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerphoneinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerphoneinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="phoneid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string phoneid)
		{
			int ret = 0;

			CustomerPhoneInfoModel customerphoneinfo = new CustomerPhoneInfoModel();
			customerphoneinfo.PhoneId = phoneid;
			
			ret = DbUtil.Current.Delete(customerphoneinfo);

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

			CustomerPhoneInfoModel customerphoneinfo = new CustomerPhoneInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customerphoneinfo, pc);

			return ret;
		}
	}
}

