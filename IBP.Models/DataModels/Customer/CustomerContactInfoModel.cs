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
    /// CustomerContactInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "customer_contact_info")]
    public class CustomerContactInfoModel : BaseModel
    {
        private string _contactId = null;
        private string _customerId = null;
        private string _relWorkorderId = null;
        private string _customerPhone = null;
        private int? _fromCityId = null;
        private string _fromCityName = null;
        private string _calledNumber = null;
        private int? _directions = null;
        private string _purpose = null;
        private string _results = null;
        private string _description = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "contact_id", PrimaryKey = true)]
        public string ContactId
        {
            get { return _contactId; }
            set { _contactId = value; }
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
        ///  
        /// </summary>
        [TableMapping(FieldName = "rel_workorder_id")]
        public string RelWorkorderId
        {
            get { return _relWorkorderId; }
            set { _relWorkorderId = value; }
        }

        /// <summary>
        /// 联系电话 
        /// </summary>
        [TableMapping(FieldName = "customer_phone")]
        public string CustomerPhone
        {
            get { return _customerPhone; }
            set { _customerPhone = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "from_city_id")]
        public int? FromCityId
        {
            get { return _fromCityId; }
            set { _fromCityId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "from_city_name")]
        public string FromCityName
        {
            get { return _fromCityName; }
            set { _fromCityName = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "called_number")]
        public string CalledNumber
        {
            get { return _calledNumber; }
            set { _calledNumber = value; }
        }

        /// <summary>
        /// 联系方向 
        /// </summary>
        [TableMapping(FieldName = "directions")]
        public int? Directions
        {
            get { return _directions; }
            set { _directions = value; }
        }

        /// <summary>
        /// 联系目的 
        /// </summary>
        [TableMapping(FieldName = "purpose")]
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }

        /// <summary>
        /// 联系结果 
        /// </summary>
        [TableMapping(FieldName = "results")]
        public string Results
        {
            get { return _results; }
            set { _results = value; }
        }

        /// <summary>
        /// 联系记录 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
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
