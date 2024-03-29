/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-10
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
	/// CustomDataInfo业务逻辑类
	/// </summary>
	public partial class CustomDataInfoService
	{
		// 实例
		private static CustomDataInfoService _instance = new CustomDataInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomDataInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomDataInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customdatainfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomDataInfoModel customdatainfo)
		{
			int ret = 0;

			customdatainfo.CreatedBy = SessionUtil.Current.UserId;
			customdatainfo.CreatedOn = DateTime.Now;
			customdatainfo.ModifiedBy = SessionUtil.Current.UserId;
			customdatainfo.ModifiedOn = DateTime.Now;
			customdatainfo.StatusCode = 0;

			ret = DbUtil.Current.Create(customdatainfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="dataid"></param>
		/// <returns>实体</returns>
		public CustomDataInfoModel Retrieve(string dataid)
		{
			CustomDataInfoModel customdatainfo = new CustomDataInfoModel();
			customdatainfo.DataId = dataid;
		
			DataTable dt = DbUtil.Current.Retrieve(customdatainfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customdatainfo.ConvertFrom(dt);

			return customdatainfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomDataInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomDataInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomDataInfoModel> customdatainfos = new List<CustomDataInfoModel>();

			CustomDataInfoModel customdatainfo = new CustomDataInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customdatainfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customdatainfo = new CustomDataInfoModel();
				customdatainfo.ConvertFrom(dt, i);
				customdatainfos.Add(customdatainfo);
			}

			return customdatainfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customdatainfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomDataInfoModel customdatainfo)
		{
			int ret = 0;

			customdatainfo.ModifiedBy = SessionUtil.Current.UserId;
			customdatainfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customdatainfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customdatainfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomDataInfoModel customdatainfo, ParameterCollection pc)
		{
			int ret = 0;

			customdatainfo.ModifiedBy = SessionUtil.Current.UserId;
			customdatainfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customdatainfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="dataid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string dataid)
		{
			int ret = 0;

			CustomDataInfoModel customdatainfo = new CustomDataInfoModel();
			customdatainfo.DataId = dataid;
			
			ret = DbUtil.Current.Delete(customdatainfo);

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

			CustomDataInfoModel customdatainfo = new CustomDataInfoModel();
			ret = DbUtil.Current.DeleteMultiple(customdatainfo, pc);

			return ret;
		}
	}
}

