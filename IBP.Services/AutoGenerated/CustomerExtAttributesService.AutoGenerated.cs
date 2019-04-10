/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-6
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
	/// CustomerExtAttributes业务逻辑类
	/// </summary>
	public partial class CustomerExtAttributesInfoService
	{
		// 实例
		private static CustomerExtAttributesInfoService _instance = new CustomerExtAttributesInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerExtAttributesInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerExtAttributesInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerextattributes">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerExtAttributesModel customerextattributes)
		{
			int ret = 0;

			customerextattributes.CreatedBy = SessionUtil.Current.UserId;
			customerextattributes.CreatedOn = DateTime.Now;
			customerextattributes.ModifiedBy = SessionUtil.Current.UserId;
			customerextattributes.ModifiedOn = DateTime.Now;
			customerextattributes.StatusCode = 0;

			ret = DbUtil.Current.Create(customerextattributes);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="extattributeid"></param>
		/// <returns>实体</returns>
		public CustomerExtAttributesModel Retrieve(string extattributeid)
		{
			CustomerExtAttributesModel customerextattributes = new CustomerExtAttributesModel();
			customerextattributes.ExtAttributeId = extattributeid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerextattributes);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerextattributes.ConvertFrom(dt);

			return customerextattributes;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerExtAttributesModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerExtAttributesModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerExtAttributesModel> customerextattributess = new List<CustomerExtAttributesModel>();

			CustomerExtAttributesModel customerextattributes = new CustomerExtAttributesModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerextattributes, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerextattributes = new CustomerExtAttributesModel();
				customerextattributes.ConvertFrom(dt, i);
				customerextattributess.Add(customerextattributes);
			}

			return customerextattributess;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerextattributes">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerExtAttributesModel customerextattributes)
		{
			int ret = 0;

			customerextattributes.ModifiedBy = SessionUtil.Current.UserId;
			customerextattributes.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerextattributes);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerextattributes">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerExtAttributesModel customerextattributes, ParameterCollection pc)
		{
			int ret = 0;

			customerextattributes.ModifiedBy = SessionUtil.Current.UserId;
			customerextattributes.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerextattributes, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="extattributeid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string extattributeid)
		{
			int ret = 0;

			CustomerExtAttributesModel customerextattributes = new CustomerExtAttributesModel();
			customerextattributes.ExtAttributeId = extattributeid;
			
			ret = DbUtil.Current.Delete(customerextattributes);

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

			CustomerExtAttributesModel customerextattributes = new CustomerExtAttributesModel();
			ret = DbUtil.Current.DeleteMultiple(customerextattributes, pc);

			return ret;
		}
	}
}

