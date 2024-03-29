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
	/// CustomDataValue实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="custom_data_value")]
	public class CustomDataValueModel : BaseModel
	{
		private string _valueId = null;
		private string _dataId = null;
		private string _dataValue = null;
		private string _dataValueCode = null;
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
		[TableMapping(FieldName="value_id",PrimaryKey=true)]
		public string ValueId
		{
			get { return _valueId; }
			set { _valueId = value; }
		}

		/// <summary>
		/// 所属数据ID 
		/// </summary>
		[TableMapping(FieldName="data_id")]
		public string DataId
		{
			get { return _dataId; }
			set { _dataId = value; }
		}

		/// <summary>
		/// 数据值 
		/// </summary>
		[TableMapping(FieldName="data_value")]
		public string DataValue
		{
			get { return _dataValue; }
			set { _dataValue = value; }
		}

		/// <summary>
		/// 数据值编码 
		/// </summary>
		[TableMapping(FieldName="data_value_code")]
		public string DataValueCode
		{
			get { return _dataValueCode; }
			set { _dataValueCode = value; }
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

