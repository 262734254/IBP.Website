/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-31
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Serialization;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;

namespace IBP.Models
{
	/// <summary>
	/// ProductCategorySalesStatus实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="product_category_sales_status")]
	public class ProductCategorySalesStatusModel : BaseModel
	{
		private string _salesStatusId = null;
		private string _productCategoryId = null;
		private string _salestatusName = null;
		private string _description = null;
		private int? _status = null;
		private int? _sortOrder = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="sales_status_id",PrimaryKey=true)]
		public string SalesStatusId
		{
			get { return _salesStatusId; }
			set { _salesStatusId = value; }
		}

		/// <summary>
		/// 产品类别ID 
		/// </summary>
		[TableMapping(FieldName="product_category_id")]
		public string ProductCategoryId
		{
			get { return _productCategoryId; }
			set { _productCategoryId = value; }
		}

		/// <summary>
		/// 销售状态名称 
		/// </summary>
		[TableMapping(FieldName="salestatus_name")]
		public string SalestatusName
		{
			get { return _salestatusName; }
			set { _salestatusName = value; }
		}

		/// <summary>
		/// 描述 
		/// </summary>
		[TableMapping(FieldName="description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// 状态 
		/// </summary>
		[TableMapping(FieldName="status")]
		public int? Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// 排序索引 
		/// </summary>
		[TableMapping(FieldName="sort_order")]
		public int? SortOrder
		{
			get { return _sortOrder; }
			set { _sortOrder = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="created_on")]
		public DateTime? CreatedOn
		{
			get { return _createdOn; }
			set { _createdOn = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="created_by")]
		public string CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="modified_on")]
		public DateTime? ModifiedOn
		{
			get { return _modifiedOn; }
			set { _modifiedOn = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="modified_by")]
		public string ModifiedBy
		{
			get { return _modifiedBy; }
			set { _modifiedBy = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="status_code")]
		public int? StatusCode
		{
			get { return _statusCode; }
			set { _statusCode = value; }
		}

	}
}

