/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-31
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
	/// ProductCategorySalesStatus业务逻辑类
	/// </summary>
	public partial class ProductCategorySalesStatusService
	{
		// 实例
		private static ProductCategorySalesStatusService _instance = new ProductCategorySalesStatusService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductCategorySalesStatusService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductCategorySalesStatusService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productcategorysalesstatus">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductCategorySalesStatusModel productcategorysalesstatus)
		{
			int ret = 0;

            productcategorysalesstatus.CreatedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategorysalesstatus.CreatedOn = DateTime.Now;
            productcategorysalesstatus.ModifiedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategorysalesstatus.ModifiedOn = DateTime.Now;
			productcategorysalesstatus.StatusCode = 0;

			ret = DbUtil.Current.Create(productcategorysalesstatus);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salesstatusid"></param>
		/// <returns>实体</returns>
		public ProductCategorySalesStatusModel Retrieve(string salesstatusid)
		{
			ProductCategorySalesStatusModel productcategorysalesstatus = new ProductCategorySalesStatusModel();
			productcategorysalesstatus.SalesStatusId = salesstatusid;
		
			DataTable dt = DbUtil.Current.Retrieve(productcategorysalesstatus);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productcategorysalesstatus.ConvertFrom(dt);

			return productcategorysalesstatus;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductCategorySalesStatusModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductCategorySalesStatusModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductCategorySalesStatusModel> productcategorysalesstatuss = new List<ProductCategorySalesStatusModel>();

			ProductCategorySalesStatusModel productcategorysalesstatus = new ProductCategorySalesStatusModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productcategorysalesstatus, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productcategorysalesstatus = new ProductCategorySalesStatusModel();
				productcategorysalesstatus.ConvertFrom(dt, i);
				productcategorysalesstatuss.Add(productcategorysalesstatus);
			}

			return productcategorysalesstatuss;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategorysalesstatus">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductCategorySalesStatusModel productcategorysalesstatus)
		{
			int ret = 0;

			productcategorysalesstatus.ModifiedBy = SessionUtil.Current.UserId;
			productcategorysalesstatus.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productcategorysalesstatus);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategorysalesstatus">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductCategorySalesStatusModel productcategorysalesstatus, ParameterCollection pc)
		{
			int ret = 0;

			productcategorysalesstatus.ModifiedBy = SessionUtil.Current.UserId;
			productcategorysalesstatus.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productcategorysalesstatus, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salesstatusid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salesstatusid)
		{
			int ret = 0;

			ProductCategorySalesStatusModel productcategorysalesstatus = new ProductCategorySalesStatusModel();
			productcategorysalesstatus.SalesStatusId = salesstatusid;
			
			ret = DbUtil.Current.Delete(productcategorysalesstatus);

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

			ProductCategorySalesStatusModel productcategorysalesstatus = new ProductCategorySalesStatusModel();
			ret = DbUtil.Current.DeleteMultiple(productcategorysalesstatus, pc);

			return ret;
		}
	}
}

