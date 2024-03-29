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
	/// VoteProjectItemInfo业务逻辑类
	/// </summary>
	public partial class VoteProjectItemInfoService
	{
		// 实例
		private static VoteProjectItemInfoService _instance = new VoteProjectItemInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private VoteProjectItemInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static VoteProjectItemInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="voteprojectiteminfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(VoteProjectItemInfoModel voteprojectiteminfo)
		{
			int ret = 0;

			voteprojectiteminfo.CreatedBy = SessionUtil.Current.UserId;
			voteprojectiteminfo.CreatedOn = DateTime.Now;
			voteprojectiteminfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectiteminfo.ModifiedOn = DateTime.Now;
			voteprojectiteminfo.StatusCode = 0;

			ret = DbUtil.Current.Create(voteprojectiteminfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="voteitemid"></param>
		/// <returns>实体</returns>
		public VoteProjectItemInfoModel Retrieve(string voteitemid)
		{
			VoteProjectItemInfoModel voteprojectiteminfo = new VoteProjectItemInfoModel();
			voteprojectiteminfo.VoteItemId = voteitemid;
		
			DataTable dt = DbUtil.Current.Retrieve(voteprojectiteminfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			voteprojectiteminfo.ConvertFrom(dt);

			return voteprojectiteminfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<VoteProjectItemInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<VoteProjectItemInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<VoteProjectItemInfoModel> voteprojectiteminfos = new List<VoteProjectItemInfoModel>();

			VoteProjectItemInfoModel voteprojectiteminfo = new VoteProjectItemInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(voteprojectiteminfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				voteprojectiteminfo = new VoteProjectItemInfoModel();
				voteprojectiteminfo.ConvertFrom(dt, i);
				voteprojectiteminfos.Add(voteprojectiteminfo);
			}

			return voteprojectiteminfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="voteprojectiteminfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(VoteProjectItemInfoModel voteprojectiteminfo)
		{
			int ret = 0;

			voteprojectiteminfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectiteminfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(voteprojectiteminfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="voteprojectiteminfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(VoteProjectItemInfoModel voteprojectiteminfo, ParameterCollection pc)
		{
			int ret = 0;

			voteprojectiteminfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectiteminfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(voteprojectiteminfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="voteitemid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string voteitemid)
		{
			int ret = 0;

			VoteProjectItemInfoModel voteprojectiteminfo = new VoteProjectItemInfoModel();
			voteprojectiteminfo.VoteItemId = voteitemid;
			
			ret = DbUtil.Current.Delete(voteprojectiteminfo);

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

			VoteProjectItemInfoModel voteprojectiteminfo = new VoteProjectItemInfoModel();
			ret = DbUtil.Current.DeleteMultiple(voteprojectiteminfo, pc);

			return ret;
		}
	}
}

