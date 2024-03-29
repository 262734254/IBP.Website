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
	/// CustomDataValue业务逻辑类
	/// </summary>
	public partial class CustomDataValueService
	{
		// 实例
		private static CustomDataValueService _instance = new CustomDataValueService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private CustomDataValueService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static CustomDataValueService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="customdatavalue">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(CustomDataValueModel customdatavalue)
		{
			int ret = 0;

			customdatavalue.CreatedBy = SessionUtil.Current.UserId;
			customdatavalue.CreatedOn = DateTime.Now;
			customdatavalue.ModifiedBy = SessionUtil.Current.UserId;
			customdatavalue.ModifiedOn = DateTime.Now;
			customdatavalue.StatusCode = 0;

			ret = DbUtil.Current.Create(customdatavalue);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="valueid"></param>
		/// <returns>实体</returns>
		public CustomDataValueModel Retrieve(string valueid)
		{
			CustomDataValueModel customdatavalue = new CustomDataValueModel();
			customdatavalue.ValueId = valueid;
		
			DataTable dt = DbUtil.Current.Retrieve(customdatavalue);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			customdatavalue.ConvertFrom(dt);

			return customdatavalue;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<CustomDataValueModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<CustomDataValueModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<CustomDataValueModel> customdatavalues = new List<CustomDataValueModel>();

			CustomDataValueModel customdatavalue = new CustomDataValueModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(customdatavalue, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				customdatavalue = new CustomDataValueModel();
				customdatavalue.ConvertFrom(dt, i);
				customdatavalues.Add(customdatavalue);
			}

			return customdatavalues;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customdatavalue">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(CustomDataValueModel customdatavalue)
		{
			int ret = 0;

			customdatavalue.ModifiedBy = SessionUtil.Current.UserId;
			customdatavalue.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(customdatavalue);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="customdatavalue">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(CustomDataValueModel customdatavalue, ParameterCollection pc)
		{
			int ret = 0;

			customdatavalue.ModifiedBy = SessionUtil.Current.UserId;
			customdatavalue.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(customdatavalue, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="valueid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string valueid)
		{
			int ret = 0;

			CustomDataValueModel customdatavalue = new CustomDataValueModel();
			customdatavalue.ValueId = valueid;
			
			ret = DbUtil.Current.Delete(customdatavalue);

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

			CustomDataValueModel customdatavalue = new CustomDataValueModel();
			ret = DbUtil.Current.DeleteMultiple(customdatavalue, pc);

			return ret;
		}
	}
}

