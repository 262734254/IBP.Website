/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-7-17
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
    /// SalesorderBasicInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "salesorder_basic_info")]
    public class SalesorderBasicInfoModel : BaseModel
    {
        private string _salesorderId = null;
        private string _salesorderCode = null;
        private string _customerId = null;
        private string _customerName = null;
        private string _salesorderTypeId = null;
        private string _relWorkorderId = null;
        private string _orderSource = null;
        private string _salesCityId = null;
        private int? _payType = null;
        private decimal? _payPrice = null;
        private string _nowOrderStatusId = null;
        private string _nowOrderStatusName = null;
        private string _nowStatusDescription = null;
        private string _remark = null;
        private string _payCityId = null;
        private string _payBindCreditcardId = null;
        private string _payCustomerName = null;
        private string _payIdcardTypeId = null;
        private string _payIdcardNumber = null;
        private string _payCardBankId = null;
        private string _payCardNumber = null;
        private string _payCardPeriod = null;
        private string _payCardSecuritycode = null;
        private string _payBillPostcode = null;
        private string _payBillAddress = null;
        private string _payPosMachineId = null;
        private DateTime? _followTime = null;
        private string _followRemark = null;
        private DateTime? _chargeTime = null;
        private string _chargeBillCode = null;
        private string _chargeUserId = null;
        private DateTime? _approvalTime = null;
        private string _approvalUserId = null;
        private DateTime? _openingTime = null;
        private string _openingUserId = null;
        private DateTime? _stockingTime = null;
        private string _stockingUserId = null;
        private DateTime? _deliveryTime = null;
        private string _deliveryUserId = null;
        private string _deliveryBindDeliveryId = null;
        private string _deliveryCompanyId = null;
        private string _deliveryOrderCode = null;
        private int? _deliveryType = null;
        private int? _deliveryChinaId = null;
        private string _deliveryPostcode = null;
        private string _deliveryAddress = null;
        private string _deliveryReceiveCustomerName = null;
        private string _deliveryReceivePhonenumber = null;
        private DateTime? _signTime = null;
        private string _signUserId = null;
        private int? _needInvoice = null;
        private string _invoiceTitle = null;
        private DateTime? _recoverTime = null;
        private string _recoverUserId = null;
        private DateTime? _refundTime = null;
        private string _refundUserId = null;
        private DateTime? _productReturnTime = null;
        private string _returnUserId = null;
        private DateTime? _cancelOpeningTime = null;
        private string _cancelOpeningUserId = null;
        private DateTime? _cancelTime = null;
        private string _cancelUserId = null;
        private DateTime? _checkedTime = null;
        private string _checkedUserId = null;
        private int? _isDelayCheck = null;
        private int? _isException = null;
        private DateTime? _exceptionTime = null;
        private string _exceptionUserId = null;
        private string _exceptionReason = null;
        private string _exceptionDesc = null;
        private string _chargeRemark = null;
        private string _checkedRemark = null;
        private string _approvalRemark = null;
        private string _openingRemark = null;
        private string _stockingRemark = null;
        private string _deliveryRemark = null;
        private string _signRemark = null;
        private string _recoverRemark = null;
        private string _refundRemark = null;
        private string _returnRemark = null;
        private string _cancelRemark = null;
        private string _cancelOpeningRemark = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "salesorder_id", PrimaryKey = true)]
        public string SalesorderId
        {
            get { return _salesorderId; }
            set { _salesorderId = value; }
        }

        /// <summary>
        /// 订单编号 
        /// </summary>
        [TableMapping(FieldName = "salesorder_code")]
        public string SalesorderCode
        {
            get { return _salesorderCode; }
            set { _salesorderCode = value; }
        }

        /// <summary>
        /// 所属客户ID 
        /// </summary>
        [TableMapping(FieldName = "customer_id")]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "customer_name")]
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }

        /// <summary>
        /// 订单类型ID 
        /// </summary>
        [TableMapping(FieldName = "salesorder_type_id")]
        public string SalesorderTypeId
        {
            get { return _salesorderTypeId; }
            set { _salesorderTypeId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "rel_workorder_id")]
        public string RelWorkorderId
        {
            get { return _relWorkorderId; }
            set { _relWorkorderId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "order_source")]
        public string OrderSource
        {
            get { return _orderSource; }
            set { _orderSource = value; }
        }

        /// <summary>
        /// 销售城市ID 
        /// </summary>
        [TableMapping(FieldName = "sales_city_id")]
        public string SalesCityId
        {
            get { return _salesCityId; }
            set { _salesCityId = value; }
        }

        /// <summary>
        /// 支付方式：0分期，1全额，2到付，3在线支付 
        /// </summary>
        [TableMapping(FieldName = "pay_type")]
        public int? PayType
        {
            get { return _payType; }
            set { _payType = value; }
        }

        /// <summary>
        /// 订单总额 
        /// </summary>
        [TableMapping(FieldName = "pay_price")]
        public decimal? PayPrice
        {
            get { return _payPrice; }
            set { _payPrice = value; }
        }

        /// <summary>
        /// 当前状态ID 
        /// </summary>
        [TableMapping(FieldName = "now_order_status_id")]
        public string NowOrderStatusId
        {
            get { return _nowOrderStatusId; }
            set { _nowOrderStatusId = value; }
        }

        /// <summary>
        /// 当前状态名称 
        /// </summary>
        [TableMapping(FieldName = "now_order_status_name")]
        public string NowOrderStatusName
        {
            get { return _nowOrderStatusName; }
            set { _nowOrderStatusName = value; }
        }

        /// <summary>
        /// 当前状态描述 
        /// </summary>
        [TableMapping(FieldName = "now_status_description")]
        public string NowStatusDescription
        {
            get { return _nowStatusDescription; }
            set { _nowStatusDescription = value; }
        }

        /// <summary>
        /// 订单备注 
        /// </summary>
        [TableMapping(FieldName = "remark")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 开户城市 
        /// </summary>
        [TableMapping(FieldName = "pay_city_id")]
        public string PayCityId
        {
            get { return _payCityId; }
            set { _payCityId = value; }
        }

        /// <summary>
        /// 绑定的客户持卡信息ID 
        /// </summary>
        [TableMapping(FieldName = "pay_bind_creditcard_id")]
        public string PayBindCreditcardId
        {
            get { return _payBindCreditcardId; }
            set { _payBindCreditcardId = value; }
        }

        /// <summary>
        /// 支付人姓名 
        /// </summary>
        [TableMapping(FieldName = "pay_customer_name")]
        public string PayCustomerName
        {
            get { return _payCustomerName; }
            set { _payCustomerName = value; }
        }

        /// <summary>
        /// 支付人证件类型ID 
        /// </summary>
        [TableMapping(FieldName = "pay_idcard_type_id")]
        public string PayIdcardTypeId
        {
            get { return _payIdcardTypeId; }
            set { _payIdcardTypeId = value; }
        }

        /// <summary>
        /// 支付人证件号码 
        /// </summary>
        [TableMapping(FieldName = "pay_idcard_number")]
        public string PayIdcardNumber
        {
            get { return _payIdcardNumber; }
            set { _payIdcardNumber = value; }
        }

        /// <summary>
        /// 开户行 
        /// </summary>
        [TableMapping(FieldName = "pay_card_bank_id")]
        public string PayCardBankId
        {
            get { return _payCardBankId; }
            set { _payCardBankId = value; }
        }

        /// <summary>
        /// 支付卡号码 
        /// </summary>
        [TableMapping(FieldName = "pay_card_number")]
        public string PayCardNumber
        {
            get { return _payCardNumber; }
            set { _payCardNumber = value; }
        }

        /// <summary>
        /// 支付卡有效期 
        /// </summary>
        [TableMapping(FieldName = "pay_card_period")]
        public string PayCardPeriod
        {
            get { return _payCardPeriod; }
            set { _payCardPeriod = value; }
        }

        /// <summary>
        /// 支付卡安全码 
        /// </summary>
        [TableMapping(FieldName = "pay_card_securitycode")]
        public string PayCardSecuritycode
        {
            get { return _payCardSecuritycode; }
            set { _payCardSecuritycode = value; }
        }

        /// <summary>
        /// 支付账单邮编 
        /// </summary>
        [TableMapping(FieldName = "pay_bill_postcode")]
        public string PayBillPostcode
        {
            get { return _payBillPostcode; }
            set { _payBillPostcode = value; }
        }

        /// <summary>
        /// 支付账单地址 
        /// </summary>
        [TableMapping(FieldName = "pay_bill_address")]
        public string PayBillAddress
        {
            get { return _payBillAddress; }
            set { _payBillAddress = value; }
        }

        /// <summary>
        /// 扣款POS机ID 
        /// </summary>
        [TableMapping(FieldName = "pay_pos_machine_id")]
        public string PayPosMachineId
        {
            get { return _payPosMachineId; }
            set { _payPosMachineId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "follow_time")]
        public DateTime? FollowTime
        {
            get { return _followTime; }
            set { _followTime = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "follow_remark")]
        public string FollowRemark
        {
            get { return _followRemark; }
            set { _followRemark = value; }
        }

        /// <summary>
        /// 扣款时间 
        /// </summary>
        [TableMapping(FieldName = "charge_time")]
        public DateTime? ChargeTime
        {
            get { return _chargeTime; }
            set { _chargeTime = value; }
        }

        /// <summary>
        /// 扣款凭证号码 
        /// </summary>
        [TableMapping(FieldName = "charge_bill_code")]
        public string ChargeBillCode
        {
            get { return _chargeBillCode; }
            set { _chargeBillCode = value; }
        }

        /// <summary>
        /// 扣款人ID 
        /// </summary>
        [TableMapping(FieldName = "charge_user_id")]
        public string ChargeUserId
        {
            get { return _chargeUserId; }
            set { _chargeUserId = value; }
        }

        /// <summary>
        /// 审批时间 
        /// </summary>
        [TableMapping(FieldName = "approval_time")]
        public DateTime? ApprovalTime
        {
            get { return _approvalTime; }
            set { _approvalTime = value; }
        }

        /// <summary>
        /// 审批人ID 
        /// </summary>
        [TableMapping(FieldName = "approval_user_id")]
        public string ApprovalUserId
        {
            get { return _approvalUserId; }
            set { _approvalUserId = value; }
        }

        /// <summary>
        /// 开户时间 
        /// </summary>
        [TableMapping(FieldName = "opening_time")]
        public DateTime? OpeningTime
        {
            get { return _openingTime; }
            set { _openingTime = value; }
        }

        /// <summary>
        /// 开户人ID 
        /// </summary>
        [TableMapping(FieldName = "opening_user_id")]
        public string OpeningUserId
        {
            get { return _openingUserId; }
            set { _openingUserId = value; }
        }

        /// <summary>
        /// 备货时间 
        /// </summary>
        [TableMapping(FieldName = "stocking_time")]
        public DateTime? StockingTime
        {
            get { return _stockingTime; }
            set { _stockingTime = value; }
        }

        /// <summary>
        /// 备货人ID 
        /// </summary>
        [TableMapping(FieldName = "stocking_user_id")]
        public string StockingUserId
        {
            get { return _stockingUserId; }
            set { _stockingUserId = value; }
        }

        /// <summary>
        /// 发货时间 
        /// </summary>
        [TableMapping(FieldName = "delivery_time")]
        public DateTime? DeliveryTime
        {
            get { return _deliveryTime; }
            set { _deliveryTime = value; }
        }

        /// <summary>
        /// 发货人ID 
        /// </summary>
        [TableMapping(FieldName = "delivery_user_id")]
        public string DeliveryUserId
        {
            get { return _deliveryUserId; }
            set { _deliveryUserId = value; }
        }

        /// <summary>
        /// 绑定的客户配送ID 
        /// </summary>
        [TableMapping(FieldName = "delivery_bind_delivery_id")]
        public string DeliveryBindDeliveryId
        {
            get { return _deliveryBindDeliveryId; }
            set { _deliveryBindDeliveryId = value; }
        }

        /// <summary>
        /// 物流公司ID 
        /// </summary>
        [TableMapping(FieldName = "delivery_company_id")]
        public string DeliveryCompanyId
        {
            get { return _deliveryCompanyId; }
            set { _deliveryCompanyId = value; }
        }

        /// <summary>
        /// 物流配送单号 
        /// </summary>
        [TableMapping(FieldName = "delivery_order_code")]
        public string DeliveryOrderCode
        {
            get { return _deliveryOrderCode; }
            set { _deliveryOrderCode = value; }
        }

        /// <summary>
        /// 配送属性，0公司，1家里 
        /// </summary>
        [TableMapping(FieldName = "delivery_type")]
        public int? DeliveryType
        {
            get { return _deliveryType; }
            set { _deliveryType = value; }
        }

        /// <summary>
        /// 配送地址ID 
        /// </summary>
        [TableMapping(FieldName = "delivery_china_id")]
        public int? DeliveryChinaId
        {
            get { return _deliveryChinaId; }
            set { _deliveryChinaId = value; }
        }

        /// <summary>
        /// 配送地区邮编 
        /// </summary>
        [TableMapping(FieldName = "delivery_postcode")]
        public string DeliveryPostcode
        {
            get { return _deliveryPostcode; }
            set { _deliveryPostcode = value; }
        }

        /// <summary>
        /// 配送详细地址 
        /// </summary>
        [TableMapping(FieldName = "delivery_address")]
        public string DeliveryAddress
        {
            get { return _deliveryAddress; }
            set { _deliveryAddress = value; }
        }

        /// <summary>
        /// 收货人 
        /// </summary>
        [TableMapping(FieldName = "delivery_receive_customer_name")]
        public string DeliveryReceiveCustomerName
        {
            get { return _deliveryReceiveCustomerName; }
            set { _deliveryReceiveCustomerName = value; }
        }

        /// <summary>
        /// 收货人电话 
        /// </summary>
        [TableMapping(FieldName = "delivery_receive_phonenumber")]
        public string DeliveryReceivePhonenumber
        {
            get { return _deliveryReceivePhonenumber; }
            set { _deliveryReceivePhonenumber = value; }
        }

        /// <summary>
        /// 签收时间 
        /// </summary>
        [TableMapping(FieldName = "sign_time")]
        public DateTime? SignTime
        {
            get { return _signTime; }
            set { _signTime = value; }
        }

        /// <summary>
        /// 签收人ID 
        /// </summary>
        [TableMapping(FieldName = "sign_user_id")]
        public string SignUserId
        {
            get { return _signUserId; }
            set { _signUserId = value; }
        }

        /// <summary>
        /// 是否需要发票 
        /// </summary>
        [TableMapping(FieldName = "need_invoice")]
        public int? NeedInvoice
        {
            get { return _needInvoice; }
            set { _needInvoice = value; }
        }

        /// <summary>
        /// 发票标题 
        /// </summary>
        [TableMapping(FieldName = "invoice_title")]
        public string InvoiceTitle
        {
            get { return _invoiceTitle; }
            set { _invoiceTitle = value; }
        }

        /// <summary>
        /// 资料回收时间 
        /// </summary>
        [TableMapping(FieldName = "recover_time")]
        public DateTime? RecoverTime
        {
            get { return _recoverTime; }
            set { _recoverTime = value; }
        }

        /// <summary>
        /// 回收人ID 
        /// </summary>
        [TableMapping(FieldName = "recover_user_id")]
        public string RecoverUserId
        {
            get { return _recoverUserId; }
            set { _recoverUserId = value; }
        }

        /// <summary>
        /// 退款时间 
        /// </summary>
        [TableMapping(FieldName = "refund_time")]
        public DateTime? RefundTime
        {
            get { return _refundTime; }
            set { _refundTime = value; }
        }

        /// <summary>
        /// 退款人ID 
        /// </summary>
        [TableMapping(FieldName = "refund_user_id")]
        public string RefundUserId
        {
            get { return _refundUserId; }
            set { _refundUserId = value; }
        }

        /// <summary>
        /// 退货时间 
        /// </summary>
        [TableMapping(FieldName = "product_return_time")]
        public DateTime? ProductReturnTime
        {
            get { return _productReturnTime; }
            set { _productReturnTime = value; }
        }

        /// <summary>
        /// 退货人ID 
        /// </summary>
        [TableMapping(FieldName = "return_user_id")]
        public string ReturnUserId
        {
            get { return _returnUserId; }
            set { _returnUserId = value; }
        }

        /// <summary>
        /// 消户时间 
        /// </summary>
        [TableMapping(FieldName = "cancel_opening_time")]
        public DateTime? CancelOpeningTime
        {
            get { return _cancelOpeningTime; }
            set { _cancelOpeningTime = value; }
        }

        /// <summary>
        /// 消户人ID 
        /// </summary>
        [TableMapping(FieldName = "cancel_opening_user_id")]
        public string CancelOpeningUserId
        {
            get { return _cancelOpeningUserId; }
            set { _cancelOpeningUserId = value; }
        }

        /// <summary>
        /// 撤消时间 
        /// </summary>
        [TableMapping(FieldName = "cancel_time")]
        public DateTime? CancelTime
        {
            get { return _cancelTime; }
            set { _cancelTime = value; }
        }

        /// <summary>
        /// 撤消人ID 
        /// </summary>
        [TableMapping(FieldName = "cancel_user_id")]
        public string CancelUserId
        {
            get { return _cancelUserId; }
            set { _cancelUserId = value; }
        }

        /// <summary>
        /// 质检时间 
        /// </summary>
        [TableMapping(FieldName = "checked_time")]
        public DateTime? CheckedTime
        {
            get { return _checkedTime; }
            set { _checkedTime = value; }
        }

        /// <summary>
        /// 质检人ID 
        /// </summary>
        [TableMapping(FieldName = "checked_user_id")]
        public string CheckedUserId
        {
            get { return _checkedUserId; }
            set { _checkedUserId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "is_delay_check")]
        public int? IsDelayCheck
        {
            get { return _isDelayCheck; }
            set { _isDelayCheck = value; }
        }

        /// <summary>
        /// 是否异常 
        /// </summary>
        [TableMapping(FieldName = "is_exception")]
        public int? IsException
        {
            get { return _isException; }
            set { _isException = value; }
        }

        /// <summary>
        /// 异常时间 
        /// </summary>
        [TableMapping(FieldName = "exception_time")]
        public DateTime? ExceptionTime
        {
            get { return _exceptionTime; }
            set { _exceptionTime = value; }
        }

        /// <summary>
        /// 设置异常用户ID 
        /// </summary>
        [TableMapping(FieldName = "exception_user_id")]
        public string ExceptionUserId
        {
            get { return _exceptionUserId; }
            set { _exceptionUserId = value; }
        }

        /// <summary>
        /// 异常原因ID 
        /// </summary>
        [TableMapping(FieldName = "exception_reason")]
        public string ExceptionReason
        {
            get { return _exceptionReason; }
            set { _exceptionReason = value; }
        }

        /// <summary>
        /// 异常描述 
        /// </summary>
        [TableMapping(FieldName = "exception_desc")]
        public string ExceptionDesc
        {
            get { return _exceptionDesc; }
            set { _exceptionDesc = value; }
        }

        /// <summary>
        /// 扣款备注 
        /// </summary>
        [TableMapping(FieldName = "charge_remark")]
        public string ChargeRemark
        {
            get { return _chargeRemark; }
            set { _chargeRemark = value; }
        }

        /// <summary>
        /// 质检备注 
        /// </summary>
        [TableMapping(FieldName = "checked_remark")]
        public string CheckedRemark
        {
            get { return _checkedRemark; }
            set { _checkedRemark = value; }
        }

        /// <summary>
        /// 审批备注 
        /// </summary>
        [TableMapping(FieldName = "approval_remark")]
        public string ApprovalRemark
        {
            get { return _approvalRemark; }
            set { _approvalRemark = value; }
        }

        /// <summary>
        /// 开户备注 
        /// </summary>
        [TableMapping(FieldName = "opening_remark")]
        public string OpeningRemark
        {
            get { return _openingRemark; }
            set { _openingRemark = value; }
        }

        /// <summary>
        /// 备货备注 
        /// </summary>
        [TableMapping(FieldName = "stocking_remark")]
        public string StockingRemark
        {
            get { return _stockingRemark; }
            set { _stockingRemark = value; }
        }

        /// <summary>
        /// 发货备注 
        /// </summary>
        [TableMapping(FieldName = "delivery_remark")]
        public string DeliveryRemark
        {
            get { return _deliveryRemark; }
            set { _deliveryRemark = value; }
        }

        /// <summary>
        /// 签收备注 
        /// </summary>
        [TableMapping(FieldName = "sign_remark")]
        public string SignRemark
        {
            get { return _signRemark; }
            set { _signRemark = value; }
        }

        /// <summary>
        /// 回收备注 
        /// </summary>
        [TableMapping(FieldName = "recover_remark")]
        public string RecoverRemark
        {
            get { return _recoverRemark; }
            set { _recoverRemark = value; }
        }

        /// <summary>
        /// 退款备注 
        /// </summary>
        [TableMapping(FieldName = "refund_remark")]
        public string RefundRemark
        {
            get { return _refundRemark; }
            set { _refundRemark = value; }
        }

        /// <summary>
        /// 退货备注 
        /// </summary>
        [TableMapping(FieldName = "return_remark")]
        public string ReturnRemark
        {
            get { return _returnRemark; }
            set { _returnRemark = value; }
        }

        /// <summary>
        /// 撤消备注 
        /// </summary>
        [TableMapping(FieldName = "cancel_remark")]
        public string CancelRemark
        {
            get { return _cancelRemark; }
            set { _cancelRemark = value; }
        }

        /// <summary>
        /// 消户备注 
        /// </summary>
        [TableMapping(FieldName = "cancel_opening_remark")]
        public string CancelOpeningRemark
        {
            get { return _cancelOpeningRemark; }
            set { _cancelOpeningRemark = value; }
        }

        /// <summary>
        /// 订单创建时间 
        /// </summary>
        [TableMapping(FieldName = "created_on")]
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        /// 创建者ID 
        /// </summary>
        [TableMapping(FieldName = "created_by")]
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_on")]
        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_by")]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "status_code")]
        public int? StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

    }
}
