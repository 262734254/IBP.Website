/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
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
	/// ProductCategoryInfo业务逻辑类
	/// </summary>
	public partial class ProductCategoryInfoService
	{
		// 实例
		private static ProductCategoryInfoService _instance = new ProductCategoryInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductCategoryInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductCategoryInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productcategoryinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductCategoryInfoModel productcategoryinfo)
		{
			int ret = 0;

            productcategoryinfo.CreatedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategoryinfo.CreatedOn = DateTime.Now;
            productcategoryinfo.ModifiedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategoryinfo.ModifiedOn = DateTime.Now;
			productcategoryinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(productcategoryinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="productcategoryid"></param>
		/// <returns>实体</returns>
		public ProductCategoryInfoModel Retrieve(string productcategoryid)
		{
			ProductCategoryInfoModel productcategoryinfo = new ProductCategoryInfoModel();
			productcategoryinfo.ProductCategoryId = productcategoryid;
		
			DataTable dt = DbUtil.Current.Retrieve(productcategoryinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productcategoryinfo.ConvertFrom(dt);

			return productcategoryinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductCategoryInfoModel> productcategoryinfos = new List<ProductCategoryInfoModel>();

			ProductCategoryInfoModel productcategoryinfo = new ProductCategoryInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productcategoryinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productcategoryinfo = new ProductCategoryInfoModel();
				productcategoryinfo.ConvertFrom(dt, i);
				productcategoryinfos.Add(productcategoryinfo);
			}

			return productcategoryinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategoryinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductCategoryInfoModel productcategoryinfo)
		{
			int ret = 0;

            productcategoryinfo.ModifiedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategoryinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productcategoryinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategoryinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductCategoryInfoModel productcategoryinfo, ParameterCollection pc)
		{
			int ret = 0;

			productcategoryinfo.ModifiedBy = SessionUtil.Current.UserId;
			productcategoryinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productcategoryinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="productcategoryid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string productcategoryid)
		{
			int ret = 0;

			ProductCategoryInfoModel productcategoryinfo = new ProductCategoryInfoModel();
			productcategoryinfo.ProductCategoryId = productcategoryid;
			
			ret = DbUtil.Current.Delete(productcategoryinfo);

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

			ProductCategoryInfoModel productcategoryinfo = new ProductCategoryInfoModel();
			ret = DbUtil.Current.DeleteMultiple(productcategoryinfo, pc);

			return ret;
		}
	}
}

