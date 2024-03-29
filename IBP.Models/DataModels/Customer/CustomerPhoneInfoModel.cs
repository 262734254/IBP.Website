/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-5-11
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
    /// CustomerPhoneInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "customer_phone_info")]
    public class CustomerPhoneInfoModel : BaseModel
    {
        private string _phoneId = null;
        private string _customerId = null;
        private string _phoneNumber = null;
        private string _phoneType = null;
        private string _fromCityName = null;
        private string _fromCityId = null;
        private string _relOrderId = null;
        private string _description = null;
        private string _callStatus = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "phone_id", PrimaryKey = true)]
        public string PhoneId
        {
            get { return _phoneId; }
            set { _phoneId = value; }
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
        /// 联系号码 
        /// </summary>
        [TableMapping(FieldName = "phone_number")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        /// <summary>
        /// 号码类型 
        /// </summary>
        [TableMapping(FieldName = "phone_type")]
        public string PhoneType
        {
            get { return _phoneType; }
            set { _phoneType = value; }
        }

        /// <summary>
        /// 归属城市 
        /// </summary>
        [TableMapping(FieldName = "from_city_name")]
        public string FromCityName
        {
            get { return _fromCityName; }
            set { _fromCityName = value; }
        }

        /// <summary>
        /// 归属城市ID 
        /// </summary>
        [TableMapping(FieldName = "from_city_id")]
        public string FromCityId
        {
            get { return _fromCityId; }
            set { _fromCityId = value; }
        }

        /// <summary>
        /// 关联订单ID 
        /// </summary>
        [TableMapping(FieldName = "rel_order_id")]
        public string RelOrderId
        {
            get { return _relOrderId; }
            set { _relOrderId = value; }
        }

        /// <summary>
        /// 备注描述 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 号码状态 
        /// </summary>
        [TableMapping(FieldName = "call_status")]
        public string CallStatus
        {
            get { return _callStatus; }
            set { _callStatus = value; }
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
