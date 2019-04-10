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
	/// UserInfo业务逻辑类
	/// </summary>
	public partial class UserInfoService
	{
		// 实例
		private static UserInfoService _instance = new UserInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private UserInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static UserInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="userinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(UserInfoModel userinfo)
		{
			int ret = 0;

			userinfo.CreatedBy = SessionUtil.Current.UserId;
			userinfo.CreatedOn = DateTime.Now;
			userinfo.ModifiedBy = SessionUtil.Current.UserId;
			userinfo.ModifiedOn = DateTime.Now;
			userinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(userinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="userid"></param>
		/// <returns>实体</returns>
		public UserInfoModel Retrieve(string userid)
		{
			UserInfoModel userinfo = new UserInfoModel();
			userinfo.UserId = userid;
		
			DataTable dt = DbUtil.Current.Retrieve(userinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			userinfo.ConvertFrom(dt);

			return userinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<UserInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<UserInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<UserInfoModel> userinfos = new List<UserInfoModel>();

			UserInfoModel userinfo = new UserInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(userinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				userinfo = new UserInfoModel();
				userinfo.ConvertFrom(dt, i);
				userinfos.Add(userinfo);
			}

			return userinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="userinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(UserInfoModel userinfo)
		{
			int ret = 0;

			userinfo.ModifiedBy = SessionUtil.Current.UserId;
			userinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(userinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="userinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(UserInfoModel userinfo, ParameterCollection pc)
		{
			int ret = 0;

			userinfo.ModifiedBy = SessionUtil.Current.UserId;
			userinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(userinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="userid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string userid)
		{
			int ret = 0;

			UserInfoModel userinfo = new UserInfoModel();
			userinfo.UserId = userid;
			
			ret = DbUtil.Current.Delete(userinfo);

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

			UserInfoModel userinfo = new UserInfoModel();
			ret = DbUtil.Current.DeleteMultiple(userinfo, pc);

			return ret;
		}
	}
}

