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
	/// CustomerCreditcardInfo业务逻辑类
	/// </summary>
	public partial class CustomerCreditcardInfoService
	{
		// 实例
		private static CustomerCreditcardInfoService _instance = new CustomerCreditcardInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerCreditcardInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerCreditcardInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customercreditcardinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerCreditcardInfoModel customercreditcardinfo)
		{
			int ret = 0;

			customercreditcardinfo.CreatedBy = SessionUtil.Current.UserId;
			customercreditcardinfo.CreatedOn = DateTime.Now;
			customercreditcardinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercreditcardinfo.ModifiedOn = DateTime.Now;
			customercreditcardinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customercreditcardinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="creditcardid"></param>
		/// <returns>实体</returns>
		public CustomerCreditcardInfoModel Retrieve(string creditcardid)
		{
			CustomerCreditcardInfoModel customercreditcardinfo = new CustomerCreditcardInfoModel();
			customercreditcardinfo.CreditcardId = creditcardid;
		
			DataTable dt = DbUtil.Current.Retrieve(customercreditcardinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customercreditcardinfo.ConvertFrom(dt);

			return customercreditcardinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerCreditcardInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerCreditcardInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerCreditcardInfoModel> customercreditcardinfos = new List<CustomerCreditcardInfoModel>();

			CustomerCreditcardInfoModel customercreditcardinfo = new CustomerCreditcardInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customercreditcardinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customercreditcardinfo = new CustomerCreditcardInfoModel();
				customercreditcardinfo.ConvertFrom(dt, i);
				customercreditcardinfos.Add(customercreditcardinfo);
			}

			return customercreditcardinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customercreditcardinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerCreditcardInfoModel customercreditcardinfo)
		{
			int ret = 0;

			customercreditcardinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercreditcardinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customercreditcardinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customercreditcardinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerCreditcardInfoModel customercreditcardinfo, ParameterCollection pc)
		{
			int ret = 0;

			customercreditcardinfo.ModifiedBy = SessionUtil.Current.UserId;
			customercreditcardinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customercreditcardinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="creditcardid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string creditcardid)
		{
			int ret = 0;

			CustomerCreditcardInfoModel customercreditcardinfo = new CustomerCreditcardInfoModel();
			customercreditcardinfo.CreditcardId = creditcardid;
			
			ret = DbUtil.Current.Delete(customercreditcardinfo);

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

			CustomerCreditcardInfoModel customercreditcardinfo = new CustomerCreditcardInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customercreditcardinfo, pc);

			return ret;
		}
	}
}

