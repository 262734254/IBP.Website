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
	/// SalesorderTypeStatusInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="salesorder_type_status_info")]
	public class SalesorderTypeStatusInfoModel : BaseModel
	{
		private string _salsorderStatusId = null;
		private string _salesorderTypeId = null;
		private int? _paymentType = null;
		private string _salesorderStatusName = null;
		private int? _status = null;
		private string _description = null;
		private int? _sortOrder = null;
		private string _createdBy = null;
		private DateTime? _createdOn = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="salsorder_status_id",PrimaryKey=true)]
		public string SalsorderStatusId
		{
			get { return _salsorderStatusId; }
			set { _salsorderStatusId = value; }
		}

		/// <summary>
		/// 销售订单类型ID 
		/// </summary>
		[TableMapping(FieldName="salesorder_type_id")]
		public string SalesorderTypeId
		{
			get { return _salesorderTypeId; }
			set { _salesorderTypeId = value; }
		}

		/// <summary>
		/// 支付方式：0分期，1全额，2到付，3在线支付 
		/// </summary>
		[TableMapping(FieldName="payment_type")]
		public int? PaymentType
		{
			get { return _paymentType; }
			set { _paymentType = value; }
		}

		/// <summary>
		/// 状态名称 
		/// </summary>
		[TableMapping(FieldName="salesorder_status_name")]
		public string SalesorderStatusName
		{
			get { return _salesorderStatusName; }
			set { _salesorderStatusName = value; }
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
		/// 描述信息 
		/// </summary>
		[TableMapping(FieldName="description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// 排序 
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
		[TableMapping(FieldName="created_by")]
		public string CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
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

