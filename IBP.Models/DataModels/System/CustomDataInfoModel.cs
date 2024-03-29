/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-10
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
	/// CustomDataInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="custom_data_info")]
	public class CustomDataInfoModel : BaseModel
	{
		private string _dataId = null;
		private string _dataName = null;
		private string _dataCode = null;
		private string _dataType = null;
		private string _fieldName = null;
		private string _fieldType = null;
		private int? _minLength = null;
		private int? _maxLength = null;
		private int? _requested = null;
		private int? _sortOrder = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="data_id",PrimaryKey=true)]
		public string DataId
		{
			get { return _dataId; }
			set { _dataId = value; }
		}

		/// <summary>
		/// 数据名称 
		/// </summary>
		[TableMapping(FieldName="data_name")]
		public string DataName
		{
			get { return _dataName; }
			set { _dataName = value; }
		}

		/// <summary>
		/// 数据编码 
		/// </summary>
		[TableMapping(FieldName="data_code")]
		public string DataCode
		{
			get { return _dataCode; }
			set { _dataCode = value; }
		}

		/// <summary>
		/// 数据类型 
		/// </summary>
		[TableMapping(FieldName="data_type")]
		public string DataType
		{
			get { return _dataType; }
			set { _dataType = value; }
		}

		/// <summary>
		/// 字段名称 
		/// </summary>
		[TableMapping(FieldName="field_name")]
		public string FieldName
		{
			get { return _fieldName; }
			set { _fieldName = value; }
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
		[TableMapping(FieldName="min_length")]
		public int? MinLength
		{
			get { return _minLength; }
			set { _minLength = value; }
		}

		/// <summary>
		/// 最大长度 
		/// </summary>
		[TableMapping(FieldName="max_length")]
		public int? MaxLength
		{
			get { return _maxLength; }
			set { _maxLength = value; }
		}

		/// <summary>
		/// 是否必填 
		/// </summary>
		[TableMapping(FieldName="requested")]
		public int? Requested
		{
			get { return _requested; }
			set { _requested = value; }
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

