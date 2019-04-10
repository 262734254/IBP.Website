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
	/// UserGroupInfo业务逻辑类
	/// </summary>
	public partial class UserGroupInfoService
	{
		// 实例
		private static UserGroupInfoService _instance = new UserGroupInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private UserGroupInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static UserGroupInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="usergroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(UserGroupInfoModel usergroupinfo)
		{
			int ret = 0;

			usergroupinfo.CreatedBy = SessionUtil.Current.UserId;
			usergroupinfo.CreatedOn = DateTime.Now;
			usergroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			usergroupinfo.ModifiedOn = DateTime.Now;
			usergroupinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(usergroupinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="usergroupid"></param>
		/// <returns>实体</returns>
		public UserGroupInfoModel Retrieve(string usergroupid)
		{
			UserGroupInfoModel usergroupinfo = new UserGroupInfoModel();
			usergroupinfo.UserGroupId = usergroupid;
		
			DataTable dt = DbUtil.Current.Retrieve(usergroupinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			usergroupinfo.ConvertFrom(dt);

			return usergroupinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<UserGroupInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<UserGroupInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<UserGroupInfoModel> usergroupinfos = new List<UserGroupInfoModel>();

			UserGroupInfoModel usergroupinfo = new UserGroupInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(usergroupinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				usergroupinfo = new UserGroupInfoModel();
				usergroupinfo.ConvertFrom(dt, i);
				usergroupinfos.Add(usergroupinfo);
			}

			return usergroupinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="usergroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(UserGroupInfoModel usergroupinfo)
		{
			int ret = 0;

			usergroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			usergroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(usergroupinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="usergroupinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(UserGroupInfoModel usergroupinfo, ParameterCollection pc)
		{
			int ret = 0;

			usergroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			usergroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(usergroupinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="usergroupid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string usergroupid)
		{
			int ret = 0;

			UserGroupInfoModel usergroupinfo = new UserGroupInfoModel();
			usergroupinfo.UserGroupId = usergroupid;
			
			ret = DbUtil.Current.Delete(usergroupinfo);

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

			UserGroupInfoModel usergroupinfo = new UserGroupInfoModel();
			ret = DbUtil.Current.DeleteMultiple(usergroupinfo, pc);

			return ret;
		}
	}
}

