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
	/// VoteProjectInfo业务逻辑类
	/// </summary>
	public partial class VoteProjectInfoService
	{
		// 实例
		private static VoteProjectInfoService _instance = new VoteProjectInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private VoteProjectInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static VoteProjectInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="voteprojectinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(VoteProjectInfoModel voteprojectinfo)
		{
			int ret = 0;

			voteprojectinfo.CreatedBy = SessionUtil.Current.UserId;
			voteprojectinfo.CreatedOn = DateTime.Now;
			voteprojectinfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectinfo.ModifiedOn = DateTime.Now;
			voteprojectinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(voteprojectinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="voteprojectid"></param>
		/// <returns>实体</returns>
		public VoteProjectInfoModel Retrieve(string voteprojectid)
		{
			VoteProjectInfoModel voteprojectinfo = new VoteProjectInfoModel();
			voteprojectinfo.VoteProjectId = voteprojectid;
		
			DataTable dt = DbUtil.Current.Retrieve(voteprojectinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			voteprojectinfo.ConvertFrom(dt);

			return voteprojectinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<VoteProjectInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<VoteProjectInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<VoteProjectInfoModel> voteprojectinfos = new List<VoteProjectInfoModel>();

			VoteProjectInfoModel voteprojectinfo = new VoteProjectInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(voteprojectinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				voteprojectinfo = new VoteProjectInfoModel();
				voteprojectinfo.ConvertFrom(dt, i);
				voteprojectinfos.Add(voteprojectinfo);
			}

			return voteprojectinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="voteprojectinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(VoteProjectInfoModel voteprojectinfo)
		{
			int ret = 0;

			voteprojectinfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(voteprojectinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="voteprojectinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(VoteProjectInfoModel voteprojectinfo, ParameterCollection pc)
		{
			int ret = 0;

			voteprojectinfo.ModifiedBy = SessionUtil.Current.UserId;
			voteprojectinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(voteprojectinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="voteprojectid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string voteprojectid)
		{
			int ret = 0;

			VoteProjectInfoModel voteprojectinfo = new VoteProjectInfoModel();
			voteprojectinfo.VoteProjectId = voteprojectid;
			
			ret = DbUtil.Current.Delete(voteprojectinfo);

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

			VoteProjectInfoModel voteprojectinfo = new VoteProjectInfoModel();
			ret = DbUtil.Current.DeleteMultiple(voteprojectinfo, pc);

			return ret;
		}
	}
}

