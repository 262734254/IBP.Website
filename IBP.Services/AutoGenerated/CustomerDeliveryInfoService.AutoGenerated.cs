/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-17
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
	/// CustomerDeliveryInfo业务逻辑类
	/// </summary>
	public partial class CustomerDeliveryInfoService
	{
		// 实例
		private static CustomerDeliveryInfoService _instance = new CustomerDeliveryInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerDeliveryInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerDeliveryInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerdeliveryinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerDeliveryInfoModel customerdeliveryinfo)
		{
			int ret = 0;

			customerdeliveryinfo.CreatedBy = SessionUtil.Current.UserId;
			customerdeliveryinfo.CreatedOn = DateTime.Now;
			customerdeliveryinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerdeliveryinfo.ModifiedOn = DateTime.Now;
			customerdeliveryinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customerdeliveryinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="deliveryid"></param>
		/// <returns>实体</returns>
		public CustomerDeliveryInfoModel Retrieve(string deliveryid)
		{
			CustomerDeliveryInfoModel customerdeliveryinfo = new CustomerDeliveryInfoModel();
			customerdeliveryinfo.DeliveryId = deliveryid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerdeliveryinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerdeliveryinfo.ConvertFrom(dt);

			return customerdeliveryinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerDeliveryInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerDeliveryInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerDeliveryInfoModel> customerdeliveryinfos = new List<CustomerDeliveryInfoModel>();

			CustomerDeliveryInfoModel customerdeliveryinfo = new CustomerDeliveryInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerdeliveryinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerdeliveryinfo = new CustomerDeliveryInfoModel();
				customerdeliveryinfo.ConvertFrom(dt, i);
				customerdeliveryinfos.Add(customerdeliveryinfo);
			}

			return customerdeliveryinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerdeliveryinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerDeliveryInfoModel customerdeliveryinfo)
		{
			int ret = 0;

			customerdeliveryinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerdeliveryinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerdeliveryinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerdeliveryinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerDeliveryInfoModel customerdeliveryinfo, ParameterCollection pc)
		{
			int ret = 0;

			customerdeliveryinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerdeliveryinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerdeliveryinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="deliveryid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string deliveryid)
		{
			int ret = 0;

			CustomerDeliveryInfoModel customerdeliveryinfo = new CustomerDeliveryInfoModel();
			customerdeliveryinfo.DeliveryId = deliveryid;
			
			ret = DbUtil.Current.Delete(customerdeliveryinfo);

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

			CustomerDeliveryInfoModel customerdeliveryinfo = new CustomerDeliveryInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customerdeliveryinfo, pc);

			return ret;
		}
	}
}

