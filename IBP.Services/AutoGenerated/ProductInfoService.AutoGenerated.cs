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
	/// ProductInfo业务逻辑类
	/// </summary>
	public partial class ProductInfoService
	{
		// 实例
		private static ProductInfoService _instance = new ProductInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductInfoModel productinfo)
		{
			int ret = 0;

			productinfo.CreatedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productinfo.CreatedOn = DateTime.Now;
            productinfo.ModifiedBy = (SessionUtil.Current == null) ? "C792D747-6B74-4A58-BB5B-D98EF420F99F" : SessionUtil.Current.UserId;
			productinfo.ModifiedOn = DateTime.Now;
			productinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(productinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="productid"></param>
		/// <returns>实体</returns>
		public ProductInfoModel Retrieve(string productid)
		{
			ProductInfoModel productinfo = new ProductInfoModel();
			productinfo.ProductId = productid;
		
			DataTable dt = DbUtil.Current.Retrieve(productinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productinfo.ConvertFrom(dt);

			return productinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductInfoModel> productinfos = new List<ProductInfoModel>();

			ProductInfoModel productinfo = new ProductInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productinfo = new ProductInfoModel();
				productinfo.ConvertFrom(dt, i);
				productinfos.Add(productinfo);
			}

			return productinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductInfoModel productinfo)
		{
			int ret = 0;

			productinfo.ModifiedBy = SessionUtil.Current.UserId;
			productinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductInfoModel productinfo, ParameterCollection pc)
		{
			int ret = 0;

			productinfo.ModifiedBy = SessionUtil.Current.UserId;
			productinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="productid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string productid)
		{
			int ret = 0;

			ProductInfoModel productinfo = new ProductInfoModel();
			productinfo.ProductId = productid;
			
			ret = DbUtil.Current.Delete(productinfo);

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

			ProductInfoModel productinfo = new ProductInfoModel();
			ret = DbUtil.Current.DeleteMultiple(productinfo, pc);

			return ret;
		}
	}
}

