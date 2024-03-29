/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-31
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
	/// PhoneLocationInfo业务逻辑类
	/// </summary>
	public partial class PhoneLocationInfoService
	{
		// 实例
		private static PhoneLocationInfoService _instance = new PhoneLocationInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private PhoneLocationInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static PhoneLocationInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="phonelocationinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(PhoneLocationInfoModel phonelocationinfo)
		{
			int ret = 0;

			phonelocationinfo.CreatedBy = SessionUtil.Current.UserId;
			phonelocationinfo.CreatedOn = DateTime.Now;
			phonelocationinfo.ModifiedBy = SessionUtil.Current.UserId;
			phonelocationinfo.ModifiedOn = DateTime.Now;
			phonelocationinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(phonelocationinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="phonecode"></param>
		/// <returns>实体</returns>
		public PhoneLocationInfoModel Retrieve(string phonecode)
		{
			PhoneLocationInfoModel phonelocationinfo = new PhoneLocationInfoModel();
			phonelocationinfo.PhoneCode = phonecode;
		
			DataTable dt = DbUtil.Current.Retrieve(phonelocationinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			phonelocationinfo.ConvertFrom(dt);

			return phonelocationinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<PhoneLocationInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<PhoneLocationInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<PhoneLocationInfoModel> phonelocationinfos = new List<PhoneLocationInfoModel>();

			PhoneLocationInfoModel phonelocationinfo = new PhoneLocationInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(phonelocationinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				phonelocationinfo = new PhoneLocationInfoModel();
				phonelocationinfo.ConvertFrom(dt, i);
				phonelocationinfos.Add(phonelocationinfo);
			}

			return phonelocationinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="phonelocationinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(PhoneLocationInfoModel phonelocationinfo)
		{
			int ret = 0;

			phonelocationinfo.ModifiedBy = SessionUtil.Current.UserId;
			phonelocationinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(phonelocationinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="phonelocationinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(PhoneLocationInfoModel phonelocationinfo, ParameterCollection pc)
		{
			int ret = 0;

			phonelocationinfo.ModifiedBy = SessionUtil.Current.UserId;
			phonelocationinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(phonelocationinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="phonecode"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string phonecode)
		{
			int ret = 0;

			PhoneLocationInfoModel phonelocationinfo = new PhoneLocationInfoModel();
			phonelocationinfo.PhoneCode = phonecode;
			
			ret = DbUtil.Current.Delete(phonelocationinfo);

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

			PhoneLocationInfoModel phonelocationinfo = new PhoneLocationInfoModel();
			ret = DbUtil.Current.DeleteMultiple(phonelocationinfo, pc);

			return ret;
		}
	}
}

