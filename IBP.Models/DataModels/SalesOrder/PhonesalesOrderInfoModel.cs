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
	/// PhonesalesOrderInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="phonesales_order_info")]
	public class PhonesalesOrderInfoModel : BaseModel
	{
		private string _salesorderId = null;
		private DateTime? _chargeTime = null;
		private string _chargeUserId = null;
		private DateTime? _approvalTime = null;
		private string _approvalUserId = null;
		private DateTime? _openingTime = null;
		private string _openingUserId = null;
		private DateTime? _stockingTime = null;
		private string _stockingUserId = null;
		private DateTime? _deliveryTime = null;
		private string _deliveryUserId = null;
		private string _deliveryCompanyId = null;
		private string _deliveryOrderCode = null;
		private DateTime? _signTime = null;
		private string _signUserId = null;
		private DateTime? _recoverTime = null;
		private string _recoverUserId = null;
		private DateTime? _refundTime = null;
		private string _refundUserId = null;
		private DateTime? _productReturnTime = null;
		private string _returnUserId = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private DateTime? _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 销售订单ID 
		/// </summary>
		[TableMapping(FieldName="salesorder_id")]
		public string SalesorderId
		{
			get { return _salesorderId; }
			set { _salesorderId = value; }
		}

		/// <summary>
		/// 扣款时间 
		/// </summary>
		[TableMapping(FieldName="charge_time")]
		public DateTime? ChargeTime
		{
			get { return _chargeTime; }
			set { _chargeTime = value; }
		}

		/// <summary>
		/// 扣款人ID 
		/// </summary>
		[TableMapping(FieldName="charge_user_id")]
		public string ChargeUserId
		{
			get { return _chargeUserId; }
			set { _chargeUserId = value; }
		}

		/// <summary>
		/// 审批时间 
		/// </summary>
		[TableMapping(FieldName="approval_time")]
		public DateTime? ApprovalTime
		{
			get { return _approvalTime; }
			set { _approvalTime = value; }
		}

		/// <summary>
		/// 审批人ID 
		/// </summary>
		[TableMapping(FieldName="approval_user_id")]
		public string ApprovalUserId
		{
			get { return _approvalUserId; }
			set { _approvalUserId = value; }
		}

		/// <summary>
		/// 开户时间 
		/// </summary>
		[TableMapping(FieldName="opening_time")]
		public DateTime? OpeningTime
		{
			get { return _openingTime; }
			set { _openingTime = value; }
		}

		/// <summary>
		/// 开户人ID 
		/// </summary>
		[TableMapping(FieldName="opening_user_id")]
		public string OpeningUserId
		{
			get { return _openingUserId; }
			set { _openingUserId = value; }
		}

		/// <summary>
		/// 备货时间 
		/// </summary>
		[TableMapping(FieldName="stocking_time")]
		public DateTime? StockingTime
		{
			get { return _stockingTime; }
			set { _stockingTime = value; }
		}

		/// <summary>
		/// 备货人ID 
		/// </summary>
		[TableMapping(FieldName="stocking_user_id")]
		public string StockingUserId
		{
			get { return _stockingUserId; }
			set { _stockingUserId = value; }
		}

		/// <summary>
		/// 发货时间 
		/// </summary>
		[TableMapping(FieldName="delivery_time")]
		public DateTime? DeliveryTime
		{
			get { return _deliveryTime; }
			set { _deliveryTime = value; }
		}

		/// <summary>
		/// 发货人ID 
		/// </summary>
		[TableMapping(FieldName="delivery_user_id")]
		public string DeliveryUserId
		{
			get { return _deliveryUserId; }
			set { _deliveryUserId = value; }
		}

		/// <summary>
		/// 物流公司ID 
		/// </summary>
		[TableMapping(FieldName="delivery_company_id")]
		public string DeliveryCompanyId
		{
			get { return _deliveryCompanyId; }
			set { _deliveryCompanyId = value; }
		}

		/// <summary>
		/// 物流配送单号 
		/// </summary>
		[TableMapping(FieldName="delivery_order_code")]
		public string DeliveryOrderCode
		{
			get { return _deliveryOrderCode; }
			set { _deliveryOrderCode = value; }
		}

		/// <summary>
		/// 签收时间 
		/// </summary>
		[TableMapping(FieldName="sign_time")]
		public DateTime? SignTime
		{
			get { return _signTime; }
			set { _signTime = value; }
		}

		/// <summary>
		/// 签收人ID 
		/// </summary>
		[TableMapping(FieldName="sign_user_id")]
		public string SignUserId
		{
			get { return _signUserId; }
			set { _signUserId = value; }
		}

		/// <summary>
		/// 资料回收时间 
		/// </summary>
		[TableMapping(FieldName="recover_time")]
		public DateTime? RecoverTime
		{
			get { return _recoverTime; }
			set { _recoverTime = value; }
		}

		/// <summary>
		/// 回收人ID 
		/// </summary>
		[TableMapping(FieldName="recover_user_id")]
		public string RecoverUserId
		{
			get { return _recoverUserId; }
			set { _recoverUserId = value; }
		}

		/// <summary>
		/// 退款时间 
		/// </summary>
		[TableMapping(FieldName="refund_time")]
		public DateTime? RefundTime
		{
			get { return _refundTime; }
			set { _refundTime = value; }
		}

		/// <summary>
		/// 退款人ID 
		/// </summary>
		[TableMapping(FieldName="refund_user_id")]
		public string RefundUserId
		{
			get { return _refundUserId; }
			set { _refundUserId = value; }
		}

		/// <summary>
		/// 退货时间 
		/// </summary>
		[TableMapping(FieldName="product_return_time")]
		public DateTime? ProductReturnTime
		{
			get { return _productReturnTime; }
			set { _productReturnTime = value; }
		}

		/// <summary>
		/// 退货人ID 
		/// </summary>
		[TableMapping(FieldName="return_user_id")]
		public string ReturnUserId
		{
			get { return _returnUserId; }
			set { _returnUserId = value; }
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
		public DateTime? ModifiedBy
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

