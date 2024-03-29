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
	/// CustomerContactInfo业务逻辑类
	/// </summary>
	public partial class CustomerContactInfoService
	{
		// 实例
		private static CustomerContactInfoService _instance = new CustomerContactInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerContactInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerContactInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customercontactinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerContactInfoModel customercontactinfo)
		{
			int ret = 0;

			customercontactinfo.CreatedBy = SessionUtil.Current.UserId;
			customercontactinfo.CreatedOn = DateTime.Now;
			customercontactinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercontactinfo.ModifiedOn = DateTime.Now;
			customercontactinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customercontactinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="contactid"></param>
		/// <returns>实体</returns>
		public CustomerContactInfoModel Retrieve(string contactid)
		{
			CustomerContactInfoModel customercontactinfo = new CustomerContactInfoModel();
			customercontactinfo.ContactId = contactid;
		
			DataTable dt = DbUtil.Current.Retrieve(customercontactinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customercontactinfo.ConvertFrom(dt);

			return customercontactinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerContactInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerContactInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerContactInfoModel> customercontactinfos = new List<CustomerContactInfoModel>();

			CustomerContactInfoModel customercontactinfo = new CustomerContactInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customercontactinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customercontactinfo = new CustomerContactInfoModel();
				customercontactinfo.ConvertFrom(dt, i);
				customercontactinfos.Add(customercontactinfo);
			}

			return customercontactinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customercontactinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerContactInfoModel customercontactinfo)
		{
			int ret = 0;

			customercontactinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercontactinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customercontactinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customercontactinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerContactInfoModel customercontactinfo, ParameterCollection pc)
		{
			int ret = 0;

			customercontactinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercontactinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customercontactinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="contactid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string contactid)
		{
			int ret = 0;

			CustomerContactInfoModel customercontactinfo = new CustomerContactInfoModel();
			customercontactinfo.ContactId = contactid;
			
			ret = DbUtil.Current.Delete(customercontactinfo);

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

			CustomerContactInfoModel customercontactinfo = new CustomerContactInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customercontactinfo, pc);

			return ret;
		}
	}
}

