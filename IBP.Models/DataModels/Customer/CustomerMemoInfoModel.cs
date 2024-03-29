/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-16
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
	/// CustomerMemoInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="customer_memo_info")]
	public class CustomerMemoInfoModel : BaseModel
	{
		private string _memoId = null;
		private string _customerId = null;
		private string _memo = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="memo_id",PrimaryKey=true)]
		public string MemoId
		{
			get { return _memoId; }
			set { _memoId = value; }
		}

		/// <summary>
		/// 客户ID 
		/// </summary>
		[TableMapping(FieldName="customer_id")]
		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		/// <summary>
		/// 备注信息 
		/// </summary>
		[TableMapping(FieldName="memo")]
		public string Memo
		{
			get { return _memo; }
			set { _memo = value; }
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

