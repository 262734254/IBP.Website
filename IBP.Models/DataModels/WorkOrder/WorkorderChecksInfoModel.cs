/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-3-26
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
	/// WorkorderChecksInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="workorder_checks_info")]
	public class WorkorderChecksInfoModel : BaseModel
	{
		private string _workorderCheckId = null;
		private string _workorderId = null;
		private int? _checkStatus = null;
		private string _checkDescription = null;
		private int? _checkResult = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="workorder_check_id",PrimaryKey=true)]
		public string WorkorderCheckId
		{
			get { return _workorderCheckId; }
			set { _workorderCheckId = value; }
		}

		/// <summary>
		/// 工单ID 
		/// </summary>
		[TableMapping(FieldName="workorder_id")]
		public string WorkorderId
		{
			get { return _workorderId; }
			set { _workorderId = value; }
		}

		/// <summary>
		/// 质检状态 
		/// </summary>
		[TableMapping(FieldName="check_status")]
		public int? CheckStatus
		{
			get { return _checkStatus; }
			set { _checkStatus = value; }
		}

		/// <summary>
		/// 质检评定 
		/// </summary>
		[TableMapping(FieldName="check_description")]
		public string CheckDescription
		{
			get { return _checkDescription; }
			set { _checkDescription = value; }
		}

		/// <summary>
		/// 质检评分 
		/// </summary>
		[TableMapping(FieldName="check_result")]
		public int? CheckResult
		{
			get { return _checkResult; }
			set { _checkResult = value; }
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

