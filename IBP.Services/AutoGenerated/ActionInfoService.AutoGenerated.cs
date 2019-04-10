/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-8
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
	/// ActionInfo业务逻辑类
	/// </summary>
	public partial class ActionInfoService
	{
		// 实例
		private static ActionInfoService _instance = new ActionInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ActionInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ActionInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="actioninfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ActionInfoModel actioninfo)
		{
			int ret = 0;

			actioninfo.CreatedBy = SessionUtil.Current.UserId;
			actioninfo.CreatedOn = DateTime.Now;
			actioninfo.ModifiedBy = SessionUtil.Current.UserId;
			actioninfo.ModifiedOn = DateTime.Now;
			actioninfo.StatusCode = 0;

			ret = DbUtil.Current.Create(actioninfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="actionid"></param>
		/// <returns>实体</returns>
		public ActionInfoModel Retrieve(string actionid)
		{
			ActionInfoModel actioninfo = new ActionInfoModel();
			actioninfo.ActionId = actionid;
		
			DataTable dt = DbUtil.Current.Retrieve(actioninfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			actioninfo.ConvertFrom(dt);

			return actioninfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ActionInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ActionInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ActionInfoModel> actioninfos = new List<ActionInfoModel>();

			ActionInfoModel actioninfo = new ActionInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(actioninfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				actioninfo = new ActionInfoModel();
				actioninfo.ConvertFrom(dt, i);
				actioninfos.Add(actioninfo);
			}

			return actioninfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="actioninfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ActionInfoModel actioninfo)
		{
			int ret = 0;

			actioninfo.ModifiedBy = SessionUtil.Current.UserId;
			actioninfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(actioninfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="actioninfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ActionInfoModel actioninfo, ParameterCollection pc)
		{
			int ret = 0;

			actioninfo.ModifiedBy = SessionUtil.Current.UserId;
			actioninfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(actioninfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="actionid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string actionid)
		{
			int ret = 0;

			ActionInfoModel actioninfo = new ActionInfoModel();
			actioninfo.ActionId = actionid;
			
			ret = DbUtil.Current.Delete(actioninfo);

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

			ActionInfoModel actioninfo = new ActionInfoModel();
			ret = DbUtil.Current.DeleteMultiple(actioninfo, pc);

			return ret;
		}
	}
}

