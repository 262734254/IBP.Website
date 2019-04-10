/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-3
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
	/// DepartmentInfo业务逻辑类
	/// </summary>
	public partial class DepartmentInfoService
	{
		// 实例
		private static DepartmentInfoService _instance = new DepartmentInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private DepartmentInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static DepartmentInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="departmentinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(DepartmentInfoModel departmentinfo)
		{
			int ret = 0;

			departmentinfo.CreatedBy = SessionUtil.Current.UserId;
			departmentinfo.CreatedOn = DateTime.Now;
			departmentinfo.ModifiedBy = SessionUtil.Current.UserId;
			departmentinfo.ModifiedOn = DateTime.Now;
			departmentinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(departmentinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="departmentid"></param>
		/// <returns>实体</returns>
		public DepartmentInfoModel Retrieve(string departmentid)
		{
			DepartmentInfoModel departmentinfo = new DepartmentInfoModel();
			departmentinfo.DepartmentId = departmentid;
		
			DataTable dt = DbUtil.Current.Retrieve(departmentinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			departmentinfo.ConvertFrom(dt);

			return departmentinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<DepartmentInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<DepartmentInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<DepartmentInfoModel> departmentinfos = new List<DepartmentInfoModel>();

			DepartmentInfoModel departmentinfo = new DepartmentInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(departmentinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				departmentinfo = new DepartmentInfoModel();
				departmentinfo.ConvertFrom(dt, i);
				departmentinfos.Add(departmentinfo);
			}

			return departmentinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="departmentinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(DepartmentInfoModel departmentinfo)
		{
			int ret = 0;

			departmentinfo.ModifiedBy = SessionUtil.Current.UserId;
			departmentinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(departmentinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="departmentinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(DepartmentInfoModel departmentinfo, ParameterCollection pc)
		{
			int ret = 0;

			departmentinfo.ModifiedBy = SessionUtil.Current.UserId;
			departmentinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(departmentinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="departmentid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string departmentid)
		{
			int ret = 0;

			DepartmentInfoModel departmentinfo = new DepartmentInfoModel();
			departmentinfo.DepartmentId = departmentid;
			
			ret = DbUtil.Current.Delete(departmentinfo);

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

			DepartmentInfoModel departmentinfo = new DepartmentInfoModel();
			ret = DbUtil.Current.DeleteMultiple(departmentinfo, pc);

			return ret;
		}
	}
}

