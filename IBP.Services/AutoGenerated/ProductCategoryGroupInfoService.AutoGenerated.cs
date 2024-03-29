/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-20
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
	/// ProductCategoryGroupInfo业务逻辑类
	/// </summary>
	public partial class ProductCategoryGroupInfoService
	{
		// 实例
		private static ProductCategoryGroupInfoService _instance = new ProductCategoryGroupInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductCategoryGroupInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductCategoryGroupInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productcategorygroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductCategoryGroupInfoModel productcategorygroupinfo)
		{
			int ret = 0;

			productcategorygroupinfo.CreatedBy = SessionUtil.Current.UserId;
			productcategorygroupinfo.CreatedOn = DateTime.Now;
			productcategorygroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productcategorygroupinfo.ModifiedOn = DateTime.Now;
			productcategorygroupinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(productcategorygroupinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="productcategorygroupid"></param>
		/// <returns>实体</returns>
		public ProductCategoryGroupInfoModel Retrieve(string productcategorygroupid)
		{
			ProductCategoryGroupInfoModel productcategorygroupinfo = new ProductCategoryGroupInfoModel();
			productcategorygroupinfo.ProductCategoryGroupId = productcategorygroupid;
		
			DataTable dt = DbUtil.Current.Retrieve(productcategorygroupinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productcategorygroupinfo.ConvertFrom(dt);

			return productcategorygroupinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryGroupInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductCategoryGroupInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductCategoryGroupInfoModel> productcategorygroupinfos = new List<ProductCategoryGroupInfoModel>();

			ProductCategoryGroupInfoModel productcategorygroupinfo = new ProductCategoryGroupInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productcategorygroupinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productcategorygroupinfo = new ProductCategoryGroupInfoModel();
				productcategorygroupinfo.ConvertFrom(dt, i);
				productcategorygroupinfos.Add(productcategorygroupinfo);
			}

			return productcategorygroupinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategorygroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductCategoryGroupInfoModel productcategorygroupinfo)
		{
			int ret = 0;

			productcategorygroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productcategorygroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productcategorygroupinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productcategorygroupinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductCategoryGroupInfoModel productcategorygroupinfo, ParameterCollection pc)
		{
			int ret = 0;

			productcategorygroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productcategorygroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productcategorygroupinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="productcategorygroupid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string productcategorygroupid)
		{
			int ret = 0;

			ProductCategoryGroupInfoModel productcategorygroupinfo = new ProductCategoryGroupInfoModel();
			productcategorygroupinfo.ProductCategoryGroupId = productcategorygroupid;
			
			ret = DbUtil.Current.Delete(productcategorygroupinfo);

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

			ProductCategoryGroupInfoModel productcategorygroupinfo = new ProductCategoryGroupInfoModel();
			ret = DbUtil.Current.DeleteMultiple(productcategorygroupinfo, pc);

			return ret;
		}
	}
}

