/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-3-21
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
	/// CustomerInfoApproval业务逻辑类
	/// </summary>
	public partial class CustomerInfoApprovalService
	{
		// 实例
		private static CustomerInfoApprovalService _instance = new CustomerInfoApprovalService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomerInfoApprovalService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomerInfoApprovalService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customerinfoapproval">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomerInfoApprovalModel customerinfoapproval)
		{
			int ret = 0;

			customerinfoapproval.CreatedBy = SessionUtil.Current.UserId;
			customerinfoapproval.CreatedOn = DateTime.Now;
			customerinfoapproval.ModifiedBy = SessionUtil.Current.UserId;
			customerinfoapproval.ModifiedOn = DateTime.Now;
			customerinfoapproval.StatusCode = 0;

			ret = DbUtil.Current.Create(customerinfoapproval);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="approvalid"></param>
		/// <returns>实体</returns>
		public CustomerInfoApprovalModel Retrieve(string approvalid)
		{
			CustomerInfoApprovalModel customerinfoapproval = new CustomerInfoApprovalModel();
			customerinfoapproval.ApprovalId = approvalid;
		
			DataTable dt = DbUtil.Current.Retrieve(customerinfoapproval);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customerinfoapproval.ConvertFrom(dt);

			return customerinfoapproval;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomerInfoApprovalModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomerInfoApprovalModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomerInfoApprovalModel> customerinfoapprovals = new List<CustomerInfoApprovalModel>();

			CustomerInfoApprovalModel customerinfoapproval = new CustomerInfoApprovalModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customerinfoapproval, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customerinfoapproval = new CustomerInfoApprovalModel();
				customerinfoapproval.ConvertFrom(dt, i);
				customerinfoapprovals.Add(customerinfoapproval);
			}

			return customerinfoapprovals;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerinfoapproval">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomerInfoApprovalModel customerinfoapproval)
		{
			int ret = 0;

			customerinfoapproval.ModifiedBy = SessionUtil.Current.UserId;
			customerinfoapproval.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customerinfoapproval);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customerinfoapproval">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomerInfoApprovalModel customerinfoapproval, ParameterCollection pc)
		{
			int ret = 0;

			customerinfoapproval.ModifiedBy = SessionUtil.Current.UserId;
			customerinfoapproval.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customerinfoapproval, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="approvalid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string approvalid)
		{
			int ret = 0;

			CustomerInfoApprovalModel customerinfoapproval = new CustomerInfoApprovalModel();
			customerinfoapproval.ApprovalId = approvalid;
			
			ret = DbUtil.Current.Delete(customerinfoapproval);

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

			CustomerInfoApprovalModel customerinfoapproval = new CustomerInfoApprovalModel();
			ret = DbUtil.Current.DeleteMultiple(customerinfoapproval, pc);

			return ret;
		}
	}
}

