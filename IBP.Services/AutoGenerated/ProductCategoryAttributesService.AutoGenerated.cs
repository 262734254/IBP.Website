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
	/// ProductCategoryAttributes业务逻辑类
	/// </summary>
	public partial class ProductCategoryAttributesService
	{
		// 实例
		private static ProductCategoryAttributesService _instance = new ProductCategoryAttributesService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductCategoryAttributesService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductCategoryAttributesService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productcategoryattributes">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductCategoryAttributesModel productcategoryattributes)
		{
			int ret = 0;

            productcategoryattributes.CreatedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategoryattributes.CreatedOn = DateTime.Now;
            productcategoryattributes.ModifiedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productcategoryattributes.ModifiedOn = DateTime.Now;
			productcategoryattributes.StatusCode = 0;

			ret = DbUtil.Current.Create(productcategoryattributes);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="categoryattributeid"></param>
		/// <returns>实体</returns>
		public ProductCategoryAttributesModel Retrieve(string categoryattributeid)
		{
			ProductCategoryAttributesModel productcategoryattributes = new ProductCategoryAttributesModel();
			productcategoryattributes.CategoryAttributeId = categoryattributeid;
		
			DataTable dt = DbUtil.Current.Retrieve(productcategoryattributes);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productcategoryattributes.ConvertFrom(dt);

			return productcategoryattributes;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryAttributesModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryAttributesModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductCategoryAttributesModel> productcategoryattributess = new List<ProductCategoryAttributesModel>();

			ProductCategoryAttributesModel productcategoryattributes = new ProductCategoryAttributesModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productcategoryattributes, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productcategoryattributes = new ProductCategoryAttributesModel();
				productcategoryattributes.ConvertFrom(dt, i);
				productcategoryattributess.Add(productcategoryattributes);
			}

			return productcategoryattributess;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategoryattributes">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductCategoryAttributesModel productcategoryattributes)
		{
			int ret = 0;

			productcategoryattributes.ModifiedBy = SessionUtil.Current.UserId;
			productcategoryattributes.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productcategoryattributes);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategoryattributes">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductCategoryAttributesModel productcategoryattributes, ParameterCollection pc)
		{
			int ret = 0;

			productcategoryattributes.ModifiedBy = SessionUtil.Current.UserId;
			productcategoryattributes.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productcategoryattributes, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="categoryattributeid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string categoryattributeid)
		{
			int ret = 0;

			ProductCategoryAttributesModel productcategoryattributes = new ProductCategoryAttributesModel();
			productcategoryattributes.CategoryAttributeId = categoryattributeid;
			
			ret = DbUtil.Current.Delete(productcategoryattributes);

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

			ProductCategoryAttributesModel productcategoryattributes = new ProductCategoryAttributesModel();
			ret = DbUtil.Current.DeleteMultiple(productcategoryattributes, pc);

			return ret;
		}
	}
}

