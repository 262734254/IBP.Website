/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-6
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
	/// CustomerExtAttributes实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="customer_ext_attributes")]
	public class CustomerExtAttributesModel : BaseModel
	{
		private string _extAttributeId = null;
		private string _attributeName = null;
		private string _group_id = null;
		private string _fieldType = null;
		private int? _fieldMinLength = null;
		private int? _fieldMaxLength = null;
		private string _customValue = null;
		private string _defaultValue = null;
		private int? _sortOrder = null;
		private string _description = null;
		private int? _status = null;
		private int? _isDisplay = null;
		private string _nodeId = null;
		private string _parnetId = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="ext_attribute_id",PrimaryKey=true)]
		public string ExtAttributeId
		{
			get { return _extAttributeId; }
			set { _extAttributeId = value; }
		}


		/// <summary>
		/// 属性名称 
		/// </summary>
		[TableMapping(FieldName="attribute_name")]
		public string AttributeName
		{
			get { return _attributeName; }
			set { _attributeName = value; }
		}

		/// <summary>
		/// 分组名称 
		/// </summary>
        [TableMapping(FieldName = "group_id")]
		public string GroupId
		{
			get { return _group_id; }
			set { _group_id = value; }
		}

		/// <summary>
		/// 属性类型名称 
		/// </summary>
		[TableMapping(FieldName="field_type")]
		public string FieldType
		{
			get { return _fieldType; }
			set { _fieldType = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="field_min_length")]
		public int? FieldMinLength
		{
			get { return _fieldMinLength; }
			set { _fieldMinLength = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="field_max_length")]
		public int? FieldMaxLength
		{
			get { return _fieldMaxLength; }
			set { _fieldMaxLength = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="custom_value")]
		public string CustomValue
		{
			get { return _customValue; }
			set { _customValue = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="default_value")]
		public string DefaultValue
		{
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}

		/// <summary>
		///  
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
		[TableMapping(FieldName="description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="status")]
		public int? Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="is_display")]
		public int? IsDisplay
		{
			get { return _isDisplay; }
			set { _isDisplay = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="node_id")]
		public string NodeId
		{
			get { return _nodeId; }
			set { _nodeId = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="parnet_id")]
		public string ParnetId
		{
			get { return _parnetId; }
			set { _parnetId = value; }
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

