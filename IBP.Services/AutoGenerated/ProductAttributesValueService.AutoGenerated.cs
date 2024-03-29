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
	/// ProductAttributesValue业务逻辑类
	/// </summary>
	public partial class ProductAttributesValueService
	{
		// 实例
		private static ProductAttributesValueService _instance = new ProductAttributesValueService();

		/// <summary>
		/// 构造函数
		/// </summary>
		private ProductAttributesValueService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
		public static ProductAttributesValueService Instance
		{
			get { return _instance; }
		}

		/// <summary>
		/// 新建
		/// </summary>
		/// <param name="productattributesvalue">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Create(ProductAttributesValueModel productattributesvalue)
		{
			int ret = 0;

			productattributesvalue.CreatedBy = SessionUtil.Current.UserId;
			productattributesvalue.CreatedOn = DateTime.Now;
			productattributesvalue.ModifiedBy = SessionUtil.Current.UserId;
			productattributesvalue.ModifiedOn = DateTime.Now;
			productattributesvalue.StatusCode = 0;

			ret = DbUtil.Current.Create(productattributesvalue);

			return ret;
		}

		/// <summary>
		/// 根据主键获取实体
		/// </summary>
		/// <param name="valueid"></param>
		/// <returns>实体</returns>
		public ProductAttributesValueModel Retrieve(string valueid)
		{
			ProductAttributesValueModel productattributesvalue = new ProductAttributesValueModel();
			productattributesvalue.ValueId = valueid;
		
			DataTable dt = DbUtil.Current.Retrieve(productattributesvalue);
			if (dt.Rows.Count < 1)
			{
				return null;
			}

			productattributesvalue.ConvertFrom(dt);

			return productattributesvalue;
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <returns>实体</returns>
		public List<ProductAttributesValueModel> RetrieveMultiple(ParameterCollection pc)
		{
			return RetrieveMultiple(pc, null);
		}

		/// <summary>
		/// 根据条件获取实体集合
		/// </summary>
		/// <param name="pc">pc</param>
		/// <param name="obc">obc</param>
		/// <returns>实体</returns>
		public List<ProductAttributesValueModel> RetrieveMultiple(ParameterCollection pc, OrderByCollection obc)
		{
			List<ProductAttributesValueModel> productattributesvalues = new List<ProductAttributesValueModel>();

			ProductAttributesValueModel productattributesvalue = new ProductAttributesValueModel();
			DataTable dt = DbUtil.Current.RetrieveMultiple(productattributesvalue, pc, obc);
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				productattributesvalue = new ProductAttributesValueModel();
				productattributesvalue.ConvertFrom(dt, i);
				productattributesvalues.Add(productattributesvalue);
			}

			return productattributesvalues;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productattributesvalue">实体</param>
		/// <returns>影响的记录行数</returns>
		public int Update(ProductAttributesValueModel productattributesvalue)
		{
			int ret = 0;

			productattributesvalue.ModifiedBy = SessionUtil.Current.UserId;
			productattributesvalue.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.Update(productattributesvalue);

			return ret;
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="productattributesvalue">实体</param>
		/// <param name="pc">pc</param>
		/// <returns>影响的记录行数</returns>
		public int UpdateMultiple(ProductAttributesValueModel productattributesvalue, ParameterCollection pc)
		{
			int ret = 0;

			productattributesvalue.ModifiedBy = SessionUtil.Current.UserId;
			productattributesvalue.ModifiedOn = DateTime.Now;

			ret = DbUtil.Current.UpdateMultiple(productattributesvalue, pc);

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

			ProductAttributesValueModel productattributesvalue = new ProductAttributesValueModel();
			productattributesvalue.ValueId = valueid;
			
			ret = DbUtil.Current.Delete(productattributesvalue);

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

			ProductAttributesValueModel productattributesvalue = new ProductAttributesValueModel();
			ret = DbUtil.Current.DeleteMultiple(productattributesvalue, pc);

			return ret;
		}
	}
}

