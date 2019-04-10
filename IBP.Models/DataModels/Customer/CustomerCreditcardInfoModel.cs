/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-7
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
    /// CustomerCreditcardInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "customer_creditcard_info")]
    public class CustomerCreditcardInfoModel : BaseModel
    {
        private string _creditcardId = null;
        private string _infoType = null;
        private string _ivrDataId = null;
        private string _customerId = null;
        private string _creditcardNumber = null;
        private string _bank = null;
        private string _openingAddress = null;
        private string _cardLevel = null;
        private string _period = null;
        private string _securityCode = null;
        private string _cardType = null;
        private string _cardUsername = null;
        private string _idcardType = null;
        private string _idcardNumber = null;
        private string _cardBrand = null;
        private int? _canbeStage = null;
        private int? _mainCard = null;
        private string _billAddress = null;
        private int? _billChinaId = null;
        private string _billZipcode = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "creditcard_id", PrimaryKey = true)]
        public string CreditcardId
        {
            get { return _creditcardId; }
            set { _creditcardId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "info_type")]
        public string InfoType
        {
            get { return _infoType; }
            set { _infoType = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "ivr_data_id")]
        public string IvrDataId
        {
            get { return _ivrDataId; }
            set { _ivrDataId = value; }
        }

        /// <summary>
        /// 客户ID 
        /// </summary>
        [TableMapping(FieldName = "customer_id")]
        public string CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        /// <summary>
        /// 卡号 
        /// </summary>
        [TableMapping(FieldName = "creditcard_number")]
        public string CreditcardNumber
        {
            get { return _creditcardNumber; }
            set { _creditcardNumber = value; }
        }

        /// <summary>
        /// 开户行 
        /// </summary>
        [TableMapping(FieldName = "bank")]
        public string Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        /// <summary>
        /// 开户地 
        /// </summary>
        [TableMapping(FieldName = "opening_address")]
        public string OpeningAddress
        {
            get { return _openingAddress; }
            set { _openingAddress = value; }
        }

        /// <summary>
        /// 卡级别 
        /// </summary>
        [TableMapping(FieldName = "card_level")]
        public string CardLevel
        {
            get { return _cardLevel; }
            set { _cardLevel = value; }
        }

        /// <summary>
        /// 有效期 
        /// </summary>
        [TableMapping(FieldName = "period")]
        public string Period
        {
            get { return _period; }
            set { _period = value; }
        }

        /// <summary>
        /// 安全码 
        /// </summary>
        [TableMapping(FieldName = "security_code")]
        public string SecurityCode
        {
            get { return _securityCode; }
            set { _securityCode = value; }
        }

        /// <summary>
        /// 卡类型 
        /// </summary>
        [TableMapping(FieldName = "card_type")]
        public string CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        /// <summary>
        /// 持卡人姓名 
        /// </summary>
        [TableMapping(FieldName = "card_username")]
        public string CardUsername
        {
            get { return _cardUsername; }
            set { _cardUsername = value; }
        }

        /// <summary>
        /// 持卡人证件类型 
        /// </summary>
        [TableMapping(FieldName = "idcard_type")]
        public string IdcardType
        {
            get { return _idcardType; }
            set { _idcardType = value; }
        }

        /// <summary>
        /// 持卡人证件号码 
        /// </summary>
        [TableMapping(FieldName = "idcard_number")]
        public string IdcardNumber
        {
            get { return _idcardNumber; }
            set { _idcardNumber = value; }
        }

        /// <summary>
        /// 信用卡品牌 
        /// </summary>
        [TableMapping(FieldName = "card_brand")]
        public string CardBrand
        {
            get { return _cardBrand; }
            set { _cardBrand = value; }
        }

        /// <summary>
        /// 是否分期 
        /// </summary>
        [TableMapping(FieldName = "canbe_stage")]
        public int? CanbeStage
        {
            get { return _canbeStage; }
            set { _canbeStage = value; }
        }

        /// <summary>
        /// 是否主要使用卡 
        /// </summary>
        [TableMapping(FieldName = "main_card")]
        public int? MainCard
        {
            get { return _mainCard; }
            set { _mainCard = value; }
        }

        /// <summary>
        /// 账单地址 
        /// </summary>
        [TableMapping(FieldName = "bill_address")]
        public string BillAddress
        {
            get { return _billAddress; }
            set { _billAddress = value; }
        }

        /// <summary>
        /// 账单地址ID 
        /// </summary>
        [TableMapping(FieldName = "bill_china_id")]
        public int? BillChinaId
        {
            get { return _billChinaId; }
            set { _billChinaId = value; }
        }

        /// <summary>
        /// 账单邮编 
        /// </summary>
        [TableMapping(FieldName = "bill_zipcode")]
        public string BillZipcode
        {
            get { return _billZipcode; }
            set { _billZipcode = value; }
        }

        /// <summary>
        /// 状态 
        /// </summary>
        [TableMapping(FieldName = "status")]
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
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
