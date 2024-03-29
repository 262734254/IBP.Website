/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-23
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
	/// RelUserVoteitem业务逻辑类
	/// </summary>
	public partial class RelUserVoteitemService
	{
		// 实例
		private static RelUserVoteitemService _instance = new RelUserVoteitemService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private RelUserVoteitemService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static RelUserVoteitemService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="reluservoteitem">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(RelUserVoteitemModel reluservoteitem)
		{
			int ret = 0;

			reluservoteitem.CreatedBy = SessionUtil.Current.UserId;
			reluservoteitem.CreatedOn = DateTime.Now;
			reluservoteitem.ModifiedBy = SessionUtil.Current.UserId;
			reluservoteitem.ModifiedOn = DateTime.Now;
			reluservoteitem.StatusCode = 0;

			ret = DbUtil.Current.Create(reluservoteitem);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="uservoteid"></param>
		/// <returns>实体</returns>
		public RelUserVoteitemModel Retrieve(string uservoteid)
		{
			RelUserVoteitemModel reluservoteitem = new RelUserVoteitemModel();
			reluservoteitem.UserVoteId = uservoteid;
		
			DataTable dt = DbUtil.Current.Retrieve(reluservoteitem);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			reluservoteitem.ConvertFrom(dt);

			return reluservoteitem;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<RelUserVoteitemModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<RelUserVoteitemModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<RelUserVoteitemModel> reluservoteitems = new List<RelUserVoteitemModel>();

			RelUserVoteitemModel reluservoteitem = new RelUserVoteitemModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(reluservoteitem, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				reluservoteitem = new RelUserVoteitemModel();
				reluservoteitem.ConvertFrom(dt, i);
				reluservoteitems.Add(reluservoteitem);
			}

			return reluservoteitems;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="reluservoteitem">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(RelUserVoteitemModel reluservoteitem)
		{
			int ret = 0;

			reluservoteitem.ModifiedBy = SessionUtil.Current.UserId;
			reluservoteitem.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(reluservoteitem);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="reluservoteitem">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(RelUserVoteitemModel reluservoteitem, ParameterCollection pc)
		{
			int ret = 0;

			reluservoteitem.ModifiedBy = SessionUtil.Current.UserId;
			reluservoteitem.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(reluservoteitem, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="uservoteid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string uservoteid)
		{
			int ret = 0;

			RelUserVoteitemModel reluservoteitem = new RelUserVoteitemModel();
			reluservoteitem.UserVoteId = uservoteid;
			
			ret = DbUtil.Current.Delete(reluservoteitem);

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

			RelUserVoteitemModel reluservoteitem = new RelUserVoteitemModel();
			ret = DbUtil.Current.DeleteMultiple(reluservoteitem, pc);

			return ret;
		}
	}
}

