/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-7
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
	/// CustomerAttributeGroupInfo业务逻辑类
	/// </summary>
	public partial class CustomerAttributeGroupInfoService
	{
		// 实例
		private static CustomerAttributeGroupInfoService _instance = new CustomerAttributeGroupInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerAttributeGroupInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerAttributeGroupInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerattributegroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerAttributeGroupInfoModel customerattributegroupinfo)
		{
			int ret = 0;

			customerattributegroupinfo.CreatedBy = SessionUtil.Current.UserId;
			customerattributegroupinfo.CreatedOn = DateTime.Now;
			customerattributegroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerattributegroupinfo.ModifiedOn = DateTime.Now;
			customerattributegroupinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customerattributegroupinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="groupid"></param>
		/// <param name="groupid"></param>
		/// <returns>实体</returns>
		public CustomerAttributeGroupInfoModel Retrieve(string groupid)
		{
			CustomerAttributeGroupInfoModel customerattributegroupinfo = new CustomerAttributeGroupInfoModel();
			customerattributegroupinfo.GroupId = groupid;
			customerattributegroupinfo.GroupId = groupid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerattributegroupinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerattributegroupinfo.ConvertFrom(dt);

			return customerattributegroupinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerAttributeGroupInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerAttributeGroupInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerAttributeGroupInfoModel> customerattributegroupinfos = new List<CustomerAttributeGroupInfoModel>();

			CustomerAttributeGroupInfoModel customerattributegroupinfo = new CustomerAttributeGroupInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerattributegroupinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerattributegroupinfo = new CustomerAttributeGroupInfoModel();
				customerattributegroupinfo.ConvertFrom(dt, i);
				customerattributegroupinfos.Add(customerattributegroupinfo);
			}

			return customerattributegroupinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerattributegroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerAttributeGroupInfoModel customerattributegroupinfo)
		{
			int ret = 0;

			customerattributegroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerattributegroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerattributegroupinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerattributegroupinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerAttributeGroupInfoModel customerattributegroupinfo, ParameterCollection pc)
		{
			int ret = 0;

			customerattributegroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			customerattributegroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerattributegroupinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="groupid"></param>
		/// <param name="groupid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string groupid)
		{
			int ret = 0;

			CustomerAttributeGroupInfoModel customerattributegroupinfo = new CustomerAttributeGroupInfoModel();
			customerattributegroupinfo.GroupId = groupid;
			customerattributegroupinfo.GroupId = groupid;
			
			ret = DbUtil.Current.Delete(customerattributegroupinfo);

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

			CustomerAttributeGroupInfoModel customerattributegroupinfo = new CustomerAttributeGroupInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customerattributegroupinfo, pc);

			return ret;
		}
	}
}

