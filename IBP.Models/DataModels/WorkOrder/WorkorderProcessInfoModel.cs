/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-5-13
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
    /// WorkorderProcessInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "workorder_process_info")]
    public class WorkorderProcessInfoModel : BaseModel
    {
        private string _processId = null;
        private string _workorderId = null;
        private string _workorderTypeId = null;
        private string _beforeStatus = null;
        private string _afterStatus = null;
        private string _beforeResult = null;
        private string _afterResult = null;
        private string _beforeUserId = null;
        private string _afterUserId = null;
        private string _relContactId = null;
        private string _description = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "process_id", PrimaryKey = true)]
        public string ProcessId
        {
            get { return _processId; }
            set { _processId = value; }
        }

        /// <summary>
        /// 工单ID 
        /// </summary>
        [TableMapping(FieldName = "workorder_id")]
        public string WorkorderId
        {
            get { return _workorderId; }
            set { _workorderId = value; }
        }

        /// <summary>
        /// 工单类型ID 
        /// </summary>
        [TableMapping(FieldName = "workorder_type_id")]
        public string WorkorderTypeId
        {
            get { return _workorderTypeId; }
            set { _workorderTypeId = value; }
        }

        /// <summary>
        /// 处理前状态 
        /// </summary>
        [TableMapping(FieldName = "before_status")]
        public string BeforeStatus
        {
            get { return _beforeStatus; }
            set { _beforeStatus = value; }
        }

        /// <summary>
        /// 处理后状态 
        /// </summary>
        [TableMapping(FieldName = "after_status")]
        public string AfterStatus
        {
            get { return _afterStatus; }
            set { _afterStatus = value; }
        }

        /// <summary>
        /// 处理前的结果 
        /// </summary>
        [TableMapping(FieldName = "before_result")]
        public string BeforeResult
        {
            get { return _beforeResult; }
            set { _beforeResult = value; }
        }

        /// <summary>
        /// 处理后的结果 
        /// </summary>
        [TableMapping(FieldName = "after_result")]
        public string AfterResult
        {
            get { return _afterResult; }
            set { _afterResult = value; }
        }

        /// <summary>
        /// 处理前用户ID 
        /// </summary>
        [TableMapping(FieldName = "before_user_id")]
        public string BeforeUserId
        {
            get { return _beforeUserId; }
            set { _beforeUserId = value; }
        }

        /// <summary>
        /// 处理后用户ID 
        /// </summary>
        [TableMapping(FieldName = "after_user_id")]
        public string AfterUserId
        {
            get { return _afterUserId; }
            set { _afterUserId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "rel_contact_id")]
        public string RelContactId
        {
            get { return _relContactId; }
            set { _relContactId = value; }
        }

        /// <summary>
        /// 备注 
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
