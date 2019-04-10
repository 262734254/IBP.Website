/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-13
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
	/// SalesorderProcessLog实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="salesorder_process_log")]
	public class SalesorderProcessLogModel : BaseModel
	{
		private string _salesorderProcessId = null;
		private string _salesorderId = null;
		private string _statusId = null;
		private string _statusName = null;
		private string _processUserId = null;
		private string _description = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="salesorder_process_id",PrimaryKey=true)]
		public string SalesorderProcessId
		{
			get { return _salesorderProcessId; }
			set { _salesorderProcessId = value; }
		}

		/// <summary>
		/// 订单ID 
		/// </summary>
		[TableMapping(FieldName="salesorder_id")]
		public string SalesorderId
		{
			get { return _salesorderId; }
			set { _salesorderId = value; }
		}

		/// <summary>
		/// 订单状态ID 
		/// </summary>
		[TableMapping(FieldName="status_id")]
		public string StatusId
		{
			get { return _statusId; }
			set { _statusId = value; }
		}

		/// <summary>
		/// 订单状态名称 
		/// </summary>
		[TableMapping(FieldName="status_name")]
		public string StatusName
		{
			get { return _statusName; }
			set { _statusName = value; }
		}

		/// <summary>
		/// 处理人ID 
		/// </summary>
		[TableMapping(FieldName="process_user_id")]
		public string ProcessUserId
		{
			get { return _processUserId; }
			set { _processUserId = value; }
		}

		/// <summary>
		/// 处理记录 
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

