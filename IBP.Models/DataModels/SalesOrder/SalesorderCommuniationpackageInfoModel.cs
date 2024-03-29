/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-7-10
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
    /// SalesorderCommuniationpackageInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "salesorder_communiationpackage_info")]
    public class SalesorderCommuniationpackageInfoModel : BaseModel
    {
        private string _salesorderCommunicationpackageId = null;
        private string _salesorderId = null;
        private string _openingCityId = null;
        private string _bindCommuniationpackageId = null;
        private string _bindMainPhonenumber = null;
        private string _bindSubsidiaryPhonenumber = null;
        private string _mainPhonenumberId = null;
        private string _subsidiaryPhonenumberId = null;
        private string _ownerBindCreditcardId = null;
        private string _ownerCustomerName = null;
        private string _idcardTypeId = null;
        private string _idcardNumber = null;
        private int? _isCollection = null;
        private string _collectionBindCreditcardId = null;
        private string _collectionBankId = null;
        private string _collectionCustomerName = null;
        private string _collectionCardNumber = null;
        private string _collectionBillCityId = null;
        private string _collectionAddress = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "salesorder_communicationpackage_id", PrimaryKey = true)]
        public string SalesorderCommunicationpackageId
        {
            get { return _salesorderCommunicationpackageId; }
            set { _salesorderCommunicationpackageId = value; }
        }

        /// <summary>
        /// 销售订单ID 
        /// </summary>
        [TableMapping(FieldName = "salesorder_id")]
        public string SalesorderId
        {
            get { return _salesorderId; }
            set { _salesorderId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "opening_city_id")]
        public string OpeningCityId
        {
            get { return _openingCityId; }
            set { _openingCityId = value; }
        }

        /// <summary>
        /// 绑定的产品ID 
        /// </summary>
        [TableMapping(FieldName = "bind_communiationpackage_id")]
        public string BindCommuniationpackageId
        {
            get { return _bindCommuniationpackageId; }
            set { _bindCommuniationpackageId = value; }
        }

        /// <summary>
        /// 绑定的主号码 
        /// </summary>
        [TableMapping(FieldName = "bind_main_phonenumber")]
        public string BindMainPhonenumber
        {
            get { return _bindMainPhonenumber; }
            set { _bindMainPhonenumber = value; }
        }

        /// <summary>
        /// 绑定的副号码 
        /// </summary>
        [TableMapping(FieldName = "bind_subsidiary_phonenumber")]
        public string BindSubsidiaryPhonenumber
        {
            get { return _bindSubsidiaryPhonenumber; }
            set { _bindSubsidiaryPhonenumber = value; }
        }

        /// <summary>
        /// 主号码产品ID 
        /// </summary>
        [TableMapping(FieldName = "main_phonenumber_id")]
        public string MainPhonenumberId
        {
            get { return _mainPhonenumberId; }
            set { _mainPhonenumberId = value; }
        }

        /// <summary>
        /// 副号码ID 
        /// </summary>
        [TableMapping(FieldName = "subsidiary_phonenumber_id")]
        public string SubsidiaryPhonenumberId
        {
            get { return _subsidiaryPhonenumberId; }
            set { _subsidiaryPhonenumberId = value; }
        }

        /// <summary>
        /// 机主信息绑定的持卡ID 
        /// </summary>
        [TableMapping(FieldName = "owner_bind_creditcard_id")]
        public string OwnerBindCreditcardId
        {
            get { return _ownerBindCreditcardId; }
            set { _ownerBindCreditcardId = value; }
        }

        /// <summary>
        /// 机主姓名 
        /// </summary>
        [TableMapping(FieldName = "owner_customer_name")]
        public string OwnerCustomerName
        {
            get { return _ownerCustomerName; }
            set { _ownerCustomerName = value; }
        }

        /// <summary>
        /// 证件类型ID 
        /// </summary>
        [TableMapping(FieldName = "idcard_type_id")]
        public string IdcardTypeId
        {
            get { return _idcardTypeId; }
            set { _idcardTypeId = value; }
        }

        /// <summary>
        /// 证件号码 
        /// </summary>
        [TableMapping(FieldName = "idcard_number")]
        public string IdcardNumber
        {
            get { return _idcardNumber; }
            set { _idcardNumber = value; }
        }

        /// <summary>
        /// 是否托收 
        /// </summary>
        [TableMapping(FieldName = "is_collection")]
        public int? IsCollection
        {
            get { return _isCollection; }
            set { _isCollection = value; }
        }

        /// <summary>
        /// 托收信息绑定的持卡ID 
        /// </summary>
        [TableMapping(FieldName = "collection_bind_creditcard_id")]
        public string CollectionBindCreditcardId
        {
            get { return _collectionBindCreditcardId; }
            set { _collectionBindCreditcardId = value; }
        }

        /// <summary>
        /// 托收银行 
        /// </summary>
        [TableMapping(FieldName = "collection_bank_id")]
        public string CollectionBankId
        {
            get { return _collectionBankId; }
            set { _collectionBankId = value; }
        }

        /// <summary>
        /// 托收户名 
        /// </summary>
        [TableMapping(FieldName = "collection_customer_name")]
        public string CollectionCustomerName
        {
            get { return _collectionCustomerName; }
            set { _collectionCustomerName = value; }
        }

        /// <summary>
        /// 托收账号 
        /// </summary>
        [TableMapping(FieldName = "collection_card_number")]
        public string CollectionCardNumber
        {
            get { return _collectionCardNumber; }
            set { _collectionCardNumber = value; }
        }

        /// <summary>
        /// 托收账单城市ID 
        /// </summary>
        [TableMapping(FieldName = "collection_bill_city_id")]
        public string CollectionBillCityId
        {
            get { return _collectionBillCityId; }
            set { _collectionBillCityId = value; }
        }

        /// <summary>
        /// 托收账单地址 
        /// </summary>
        [TableMapping(FieldName = "collection_address")]
        public string CollectionAddress
        {
            get { return _collectionAddress; }
            set { _collectionAddress = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "created_on")]
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        ///  
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
