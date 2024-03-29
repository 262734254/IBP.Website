/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-1
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
	/// ProductCategoryAttributes实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="product_category_attributes")]
	public class ProductCategoryAttributesModel : BaseModel
	{
		private string _categoryAttributeId = null;
		private string _productCategoryId = null;
		private string _attributeName = null;
		private int? _nodeId = null;
		private int? _parentNode = null;
		private string _groupName = null;
		private int? _isDisplay = null;
		private string _fieldType = null;
		private int? _fieldMinLength = null;
		private int? _fieldMaxLength = null;
		private string _customValue = null;
		private string _defaultValue = null;
		private int? _isRequest = null;
		private int? _sortOrder = null;
		private int? _status = null;
		private string _description = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="category_attribute_id",PrimaryKey=true)]
		public string CategoryAttributeId
		{
			get { return _categoryAttributeId; }
			set { _categoryAttributeId = value; }
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
		/// 产品属性名称 
		/// </summary>
		[TableMapping(FieldName="attribute_name")]
		public string AttributeName
		{
			get { return _attributeName; }
			set { _attributeName = value; }
		}

		/// <summary>
		/// 节点ID 
		/// </summary>
		[TableMapping(FieldName="node_id")]
		public int? NodeId
		{
			get { return _nodeId; }
			set { _nodeId = value; }
		}

		/// <summary>
		/// 上级节点 
		/// </summary>
		[TableMapping(FieldName="parent_node")]
		public int? ParentNode
		{
			get { return _parentNode; }
			set { _parentNode = value; }
		}

		/// <summary>
		/// 组名称 
		/// </summary>
		[TableMapping(FieldName="group_name")]
		public string GroupName
		{
			get { return _groupName; }
			set { _groupName = value; }
		}

		/// <summary>
		/// 是否用于显示 
		/// </summary>
		[TableMapping(FieldName="is_display")]
		public int? IsDisplay
		{
			get { return _isDisplay; }
			set { _isDisplay = value; }
		}

		/// <summary>
		/// 字段类型 
		/// </summary>
		[TableMapping(FieldName="field_type")]
		public string FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; }
		}

		/// <summary>
		/// 最小长度 
		/// </summary>
		[TableMapping(FieldName="field_min_length")]
		public int? FieldMinLength
		{
			get { return _fieldMinLength; }
			set { _fieldMinLength = value; }
		}

		/// <summary>
		/// 最大长度 
		/// </summary>
		[TableMapping(FieldName="field_max_length")]
		public int? FieldMaxLength
		{
			get { return _fieldMaxLength; }
			set { _fieldMaxLength = value; }
		}

		/// <summary>
		/// 可选值列表 
		/// </summary>
		[TableMapping(FieldName="custom_value")]
		public string CustomValue
		{
			get { return _customValue; }
			set { _customValue = value; }
		}

		/// <summary>
		/// 默认值 
		/// </summary>
		[TableMapping(FieldName="default_value")]
		public string DefaultValue
		{
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}

		/// <summary>
		/// 是否必填 
		/// </summary>
		[TableMapping(FieldName="is_request")]
		public int? IsRequest
		{
			get { return _isRequest; }
			set { _isRequest = value; }
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
		/// 状态 
		/// </summary>
		[TableMapping(FieldName="status")]
		public int? Status
		{
			get { return _status; }
			set { _status = value; }
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

