/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-11-27
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
	/// RoleInfo业务逻辑类
	/// </summary>
	public partial class RoleInfoService
	{
		// 实例
		private static RoleInfoService _instance = new RoleInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private RoleInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static RoleInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="roleinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(RoleInfoModel roleinfo)
		{
			int ret = 0;

			roleinfo.CreatedBy = SessionUtil.Current.UserId;
			roleinfo.CreatedOn = DateTime.Now;
			roleinfo.ModifiedBy = SessionUtil.Current.UserId;
			roleinfo.ModifiedOn = DateTime.Now;
			roleinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(roleinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="roleid"></param>
		/// <returns>实体</returns>
		public RoleInfoModel Retrieve(string roleid)
		{
			RoleInfoModel roleinfo = new RoleInfoModel();
			roleinfo.RoleId = roleid;
		
			DataTable dt = DbUtil.Current.Retrieve(roleinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			roleinfo.ConvertFrom(dt);

			return roleinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<RoleInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<RoleInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<RoleInfoModel> roleinfos = new List<RoleInfoModel>();

			RoleInfoModel roleinfo = new RoleInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(roleinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				roleinfo = new RoleInfoModel();
				roleinfo.ConvertFrom(dt, i);
				roleinfos.Add(roleinfo);
			}

			return roleinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="roleinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(RoleInfoModel roleinfo)
		{
			int ret = 0;

			roleinfo.ModifiedBy = SessionUtil.Current.UserId;
			roleinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(roleinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="roleinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(RoleInfoModel roleinfo, ParameterCollection pc)
		{
			int ret = 0;

			roleinfo.ModifiedBy = SessionUtil.Current.UserId;
			roleinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(roleinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="roleid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string roleid)
		{
			int ret = 0;

			RoleInfoModel roleinfo = new RoleInfoModel();
			roleinfo.RoleId = roleid;
			
			ret = DbUtil.Current.Delete(roleinfo);

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

			RoleInfoModel roleinfo = new RoleInfoModel();
			ret = DbUtil.Current.DeleteMultiple(roleinfo, pc);

			return ret;
		}
	}
}

