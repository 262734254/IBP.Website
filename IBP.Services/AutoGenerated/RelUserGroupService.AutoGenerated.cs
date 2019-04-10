/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-2-7
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
	/// RelUserGroup业务逻辑类
	/// </summary>
	public partial class RelUserGroupService
	{
		// 实例
		private static RelUserGroupService _instance = new RelUserGroupService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private RelUserGroupService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static RelUserGroupService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="relusergroup">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(RelUserGroupModel relusergroup)
		{
			int ret = 0;

			relusergroup.CreatedBy = SessionUtil.Current.UserId;
			relusergroup.CreatedOn = DateTime.Now;
			relusergroup.ModifiedBy = SessionUtil.Current.UserId;
			relusergroup.ModifiedOn = DateTime.Now;
			relusergroup.StatusCode = 0;

			ret = DbUtil.Current.Create(relusergroup);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <returns>实体</returns>
		public RelUserGroupModel Retrieve()
		{
			RelUserGroupModel relusergroup = new RelUserGroupModel();
		
			DataTable dt = DbUtil.Current.Retrieve(relusergroup);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			relusergroup.ConvertFrom(dt);

			return relusergroup;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<RelUserGroupModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<RelUserGroupModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<RelUserGroupModel> relusergroups = new List<RelUserGroupModel>();

			RelUserGroupModel relusergroup = new RelUserGroupModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(relusergroup, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				relusergroup = new RelUserGroupModel();
				relusergroup.ConvertFrom(dt, i);
				relusergroups.Add(relusergroup);
			}

			return relusergroups;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relusergroup">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(RelUserGroupModel relusergroup)
		{
			int ret = 0;

			relusergroup.ModifiedBy = SessionUtil.Current.UserId;
			relusergroup.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(relusergroup);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relusergroup">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(RelUserGroupModel relusergroup, ParameterCollection pc)
		{
			int ret = 0;

			relusergroup.ModifiedBy = SessionUtil.Current.UserId;
			relusergroup.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(relusergroup, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>影响的记录行数</returns>
		public int Delete()
		{
			int ret = 0;

			RelUserGroupModel relusergroup = new RelUserGroupModel();
			
			ret = DbUtil.Current.Delete(relusergroup);

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

			RelUserGroupModel relusergroup = new RelUserGroupModel();
			ret = DbUtil.Current.DeleteMultiple(relusergroup, pc);

			return ret;
		}
	}
}

