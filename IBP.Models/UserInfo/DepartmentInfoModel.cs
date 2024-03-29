/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-3
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
	/// DepartmentInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="department_info")]
	public class DepartmentInfoModel : BaseModel
	{
		private string _departmentId = null;
		private string _parentId = null;
		private string _departmentName = null;
		private string _description = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 部门ID (主键) 
		/// </summary>
		[TableMapping(FieldName="department_id",PrimaryKey=true)]
		public string DepartmentId
		{
			get { return _departmentId; }
			set { _departmentId = value; }
		}

		/// <summary>
		/// 上级部门ID 
		/// </summary>
		[TableMapping(FieldName="parent_id")]
		public string ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
		/// 部门名称 
		/// </summary>
		[TableMapping(FieldName="department_name")]
		public string DepartmentName
		{
			get { return _departmentName; }
			set { _departmentName = value; }
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

