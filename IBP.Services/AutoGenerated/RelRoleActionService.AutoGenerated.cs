/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-9
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
	/// RelRoleAction业务逻辑类
	/// </summary>
	public partial class RelRoleActionService
	{
		// 实例
		private static RelRoleActionService _instance = new RelRoleActionService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private RelRoleActionService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static RelRoleActionService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="relroleaction">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(RelRoleActionModel relroleaction)
		{
			int ret = 0;

			relroleaction.CreatedBy = SessionUtil.Current.UserId;
			relroleaction.CreatedOn = DateTime.Now;
			relroleaction.ModifiedBy = SessionUtil.Current.UserId;
			relroleaction.ModifiedOn = DateTime.Now;
			relroleaction.StatusCode = 0;

			ret = DbUtil.Current.Create(relroleaction);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <returns>实体</returns>
		public RelRoleActionModel Retrieve()
		{
			RelRoleActionModel relroleaction = new RelRoleActionModel();
		
			DataTable dt = DbUtil.Current.Retrieve(relroleaction);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			relroleaction.ConvertFrom(dt);

			return relroleaction;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<RelRoleActionModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<RelRoleActionModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<RelRoleActionModel> relroleactions = new List<RelRoleActionModel>();

			RelRoleActionModel relroleaction = new RelRoleActionModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(relroleaction, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				relroleaction = new RelRoleActionModel();
				relroleaction.ConvertFrom(dt, i);
				relroleactions.Add(relroleaction);
			}

			return relroleactions;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relroleaction">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(RelRoleActionModel relroleaction)
		{
			int ret = 0;

			relroleaction.ModifiedBy = SessionUtil.Current.UserId;
			relroleaction.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(relroleaction);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="relroleaction">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(RelRoleActionModel relroleaction, ParameterCollection pc)
		{
			int ret = 0;

			relroleaction.ModifiedBy = SessionUtil.Current.UserId;
			relroleaction.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(relroleaction, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>影响的记录行数</returns>
		public int Delete()
		{
			int ret = 0;

			RelRoleActionModel relroleaction = new RelRoleActionModel();
			
			ret = DbUtil.Current.Delete(relroleaction);

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

			RelRoleActionModel relroleaction = new RelRoleActionModel();
			ret = DbUtil.Current.DeleteMultiple(relroleaction, pc);

			return ret;
		}
	}
}

