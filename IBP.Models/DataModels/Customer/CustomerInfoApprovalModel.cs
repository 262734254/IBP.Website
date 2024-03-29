/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-6
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
    /// CustomerInfoApproval实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "customer_info_approval")]
    public class CustomerInfoApprovalModel : BaseModel
    {
        private string _approvalId = null;
        private string _approvalTaskId = null;
        private string _customerId = null;
        private string _updateField = null;
        private string _updateFieldName = null;
        private string _oldDataId = null;
        private string _oldData = null;
        private string _newDataId = null;
        private string _newData = null;
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
        [TableMapping(FieldName = "approval_id", PrimaryKey = true)]
        public string ApprovalId
        {
            get { return _approvalId; }
            set { _approvalId = value; }
        }

        /// <summary>
        /// 审核任务标识 
        /// </summary>
        [TableMapping(FieldName = "approval_task_id")]
        public string ApprovalTaskId
        {
            get { return _approvalTaskId; }
            set { _approvalTaskId = value; }
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
        /// 更新字段 
        /// </summary>
        [TableMapping(FieldName = "update_field")]
        public string UpdateField
        {
            get { return _updateField; }
            set { _updateField = value; }
        }

        /// <summary>
        /// 更新字段名称 
        /// </summary>
        [TableMapping(FieldName = "update_field_name")]
        public string UpdateFieldName
        {
            get { return _updateFieldName; }
            set { _updateFieldName = value; }
        }

        /// <summary>
        /// 原值ID 
        /// </summary>
        [TableMapping(FieldName = "old_data_id")]
        public string OldDataId
        {
            get { return _oldDataId; }
            set { _oldDataId = value; }
        }

        /// <summary>
        /// 原值 
        /// </summary>
        [TableMapping(FieldName = "old_data")]
        public string OldData
        {
            get { return _oldData; }
            set { _oldData = value; }
        }

        /// <summary>
        /// 新值ID 
        /// </summary>
        [TableMapping(FieldName = "new_data_id")]
        public string NewDataId
        {
            get { return _newDataId; }
            set { _newDataId = value; }
        }

        /// <summary>
        /// 新值 
        /// </summary>
        [TableMapping(FieldName = "new_data")]
        public string NewData
        {
            get { return _newData; }
            set { _newData = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        ///  
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
