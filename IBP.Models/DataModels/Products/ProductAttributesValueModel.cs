/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
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
	/// ProductAttributesValue实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="product_attributes_value")]
	public class ProductAttributesValueModel : BaseModel
	{
		private string _valueId = null;
		private string _productId = null;
		private string _productCategoryId = null;
		private string _attributeId = null;
		private string _attributeValue = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="value_id",PrimaryKey=true)]
		public string ValueId
		{
			get { return _valueId; }
			set { _valueId = value; }
		}

		/// <summary>
		/// 产品ID 
		/// </summary>
		[TableMapping(FieldName="product_id")]
		public string ProductId
		{
			get { return _productId; }
			set { _productId = value; }
		}

		/// <summary>
		/// 类别ID 
		/// </summary>
		[TableMapping(FieldName="product_category_id")]
		public string ProductCategoryId
		{
			get { return _productCategoryId; }
			set { _productCategoryId = value; }
		}

		/// <summary>
		/// 属性ID 
		/// </summary>
		[TableMapping(FieldName="attribute_id")]
		public string AttributeId
		{
			get { return _attributeId; }
			set { _attributeId = value; }
		}

		/// <summary>
		/// 属性值 
		/// </summary>
		[TableMapping(FieldName="attribute_value")]
		public string AttributeValue
		{
			get { return _attributeValue; }
			set { _attributeValue = value; }
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

