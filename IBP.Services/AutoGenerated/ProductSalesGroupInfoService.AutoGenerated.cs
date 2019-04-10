/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-27
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
	/// ProductSalesGroupInfo业务逻辑类
	/// </summary>
	public partial class ProductSalesGroupInfoService
	{
		// 实例
		private static ProductSalesGroupInfoService _instance = new ProductSalesGroupInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductSalesGroupInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductSalesGroupInfoService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productsalesgroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductSalesGroupInfoModel productsalesgroupinfo)
		{
			int ret = 0;

			productsalesgroupinfo.CreatedBy = SessionUtil.Current.UserId;
			productsalesgroupinfo.CreatedOn = DateTime.Now;
			productsalesgroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productsalesgroupinfo.ModifiedOn = DateTime.Now;
			productsalesgroupinfo.StatusCode = 0;

			ret = DbUtil.Current.Create(productsalesgroupinfo);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="salegroupid"></param>
		/// <returns>实体</returns>
		public ProductSalesGroupInfoModel Retrieve(string salegroupid)
		{
			ProductSalesGroupInfoModel productsalesgroupinfo = new ProductSalesGroupInfoModel();
			productsalesgroupinfo.SaleGroupId = salegroupid;
		
			DataTable dt = DbUtil.Current.Retrieve(productsalesgroupinfo);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productsalesgroupinfo.ConvertFrom(dt);

			return productsalesgroupinfo;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductSalesGroupInfoModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductSalesGroupInfoModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductSalesGroupInfoModel> productsalesgroupinfos = new List<ProductSalesGroupInfoModel>();

			ProductSalesGroupInfoModel productsalesgroupinfo = new ProductSalesGroupInfoModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productsalesgroupinfo, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productsalesgroupinfo = new ProductSalesGroupInfoModel();
				productsalesgroupinfo.ConvertFrom(dt, i);
				productsalesgroupinfos.Add(productsalesgroupinfo);
			}

			return productsalesgroupinfos;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productsalesgroupinfo">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductSalesGroupInfoModel productsalesgroupinfo)
		{
			int ret = 0;

			productsalesgroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productsalesgroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productsalesgroupinfo);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productsalesgroupinfo">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductSalesGroupInfoModel productsalesgroupinfo, ParameterCollection pc)
		{
			int ret = 0;

			productsalesgroupinfo.ModifiedBy = SessionUtil.Current.UserId;
			productsalesgroupinfo.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productsalesgroupinfo, pc);

			return ret;
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="salegroupid"></param>
		/// <returns>影响的记录行数</returns>
		public int Delete(string salegroupid)
		{
			int ret = 0;

			ProductSalesGroupInfoModel productsalesgroupinfo = new ProductSalesGroupInfoModel();
			productsalesgroupinfo.SaleGroupId = salegroupid;
			
			ret = DbUtil.Current.Delete(productsalesgroupinfo);

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

			ProductSalesGroupInfoModel productsalesgroupinfo = new ProductSalesGroupInfoModel();
			ret = DbUtil.Current.DeleteMultiple(productsalesgroupinfo, pc);

			return ret;
		}
	}
}

