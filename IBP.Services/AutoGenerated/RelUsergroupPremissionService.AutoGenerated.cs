/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-2-10
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
	/// RelUsergroupPremission业务逻辑类
	/// </summary>
	public partial class RelUsergroupPremissionService
	{
		// 实例
		private static RelUsergroupPremissionService _instance = new RelUsergroupPremissionService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private RelUsergroupPremissionService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static RelUsergroupPremissionService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="relusergrouppremission">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(RelUsergroupPremissionModel relusergrouppremission)
		{
			int ret = 0;

			relusergrouppremission.CreatedBy = SessionUtil.Current.UserId;
			relusergrouppremission.CreatedOn = DateTime.Now;
			relusergrouppremission.ModifiedBy = SessionUtil.Current.UserId;
			relusergrouppremission.ModifiedOn = DateTime.Now;
			relusergrouppremission.StatusCode = 0;

			ret = DbUtil.Current.Create(relusergrouppremission);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <returns>实体</returns>
		public RelUsergroupPremissionModel Retrieve()
		{
			RelUsergroupPremissionModel relusergrouppremission = new RelUsergroupPremissionModel();
		
			DataTable dt = DbUtil.Current.Retrieve(relusergrouppremission);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			relusergrouppremission.ConvertFrom(dt);

			return relusergrouppremission;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<RelUsergroupPremissionModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<RelUsergroupPremissionModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<RelUsergroupPremissionModel> relusergrouppremissions = new List<RelUsergroupPremissionModel>();

			RelUsergroupPremissionModel relusergrouppremission = new RelUsergroupPremissionModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(relusergrouppremission, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				relusergrouppremission = new RelUsergroupPremissionModel();
				relusergrouppremission.ConvertFrom(dt, i);
				relusergrouppremissions.Add(relusergrouppremission);
			}

			return relusergrouppremissions;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relusergrouppremission">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(RelUsergroupPremissionModel relusergrouppremission)
		{
			int ret = 0;

			relusergrouppremission.ModifiedBy = SessionUtil.Current.UserId;
			relusergrouppremission.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(relusergrouppremission);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relusergrouppremission">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(RelUsergroupPremissionModel relusergrouppremission, ParameterCollection pc)
		{
			int ret = 0;

			relusergrouppremission.ModifiedBy = SessionUtil.Current.UserId;
			relusergrouppremission.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(relusergrouppremission, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>影响的记录行数</returns>
		public int Delete()
		{
			int ret = 0;

			RelUsergroupPremissionModel relusergrouppremission = new RelUsergroupPremissionModel();
			
			ret = DbUtil.Current.Delete(relusergrouppremission);

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

			RelUsergroupPremissionModel relusergrouppremission = new RelUsergroupPremissionModel();
			ret = DbUtil.Current.DeleteMultiple(relusergrouppremission, pc);

			return ret;
		}
	}
}

