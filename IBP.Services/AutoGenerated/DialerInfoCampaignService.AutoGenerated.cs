/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-26
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
	/// DialerInfoCampaign业务逻辑类
	/// </summary>
	public partial class DialerInfoCampaignService
	{
		// 实例
		private static DialerInfoCampaignService _instance = new DialerInfoCampaignService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private DialerInfoCampaignService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static DialerInfoCampaignService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="dialerinfocampaign">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(DialerInfoCampaignModel dialerinfocampaign)
		{
			int ret = 0;

			dialerinfocampaign.CreatedBy = SessionUtil.Current.UserId;
			dialerinfocampaign.CreatedOn = DateTime.Now;
			dialerinfocampaign.ModifiedBy = SessionUtil.Current.UserId;
			dialerinfocampaign.ModifiedOn = DateTime.Now;
			dialerinfocampaign.StatusCode = 0;

			ret = DbUtil.Current.Create(dialerinfocampaign);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <returns>实体</returns>
		public DialerInfoCampaignModel Retrieve()
		{
			DialerInfoCampaignModel dialerinfocampaign = new DialerInfoCampaignModel();
		
			DataTable dt = DbUtil.Current.Retrieve(dialerinfocampaign);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			dialerinfocampaign.ConvertFrom(dt);

			return dialerinfocampaign;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<DialerInfoCampaignModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<DialerInfoCampaignModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<DialerInfoCampaignModel> dialerinfocampaigns = new List<DialerInfoCampaignModel>();

			DialerInfoCampaignModel dialerinfocampaign = new DialerInfoCampaignModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(dialerinfocampaign, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dialerinfocampaign = new DialerInfoCampaignModel();
				dialerinfocampaign.ConvertFrom(dt, i);
				dialerinfocampaigns.Add(dialerinfocampaign);
			}

			return dialerinfocampaigns;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dialerinfocampaign">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(DialerInfoCampaignModel dialerinfocampaign)
		{
			int ret = 0;

			dialerinfocampaign.ModifiedBy = SessionUtil.Current.UserId;
			dialerinfocampaign.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(dialerinfocampaign);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="dialerinfocampaign">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(DialerInfoCampaignModel dialerinfocampaign, ParameterCollection pc)
		{
			int ret = 0;

			dialerinfocampaign.ModifiedBy = SessionUtil.Current.UserId;
			dialerinfocampaign.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(dialerinfocampaign, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <returns>影响的记录行数</returns>
		public int Delete()
		{
			int ret = 0;

			DialerInfoCampaignModel dialerinfocampaign = new DialerInfoCampaignModel();
			
			ret = DbUtil.Current.Delete(dialerinfocampaign);

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

			DialerInfoCampaignModel dialerinfocampaign = new DialerInfoCampaignModel();
			ret = DbUtil.Current.DeleteMultiple(dialerinfocampaign, pc);

			return ret;
		}
	}
}

