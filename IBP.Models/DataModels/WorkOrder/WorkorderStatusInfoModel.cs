/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-5
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
    /// WorkorderStatusInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "workorder_status_info")]
    public class WorkorderStatusInfoModel : BaseModel
    {
        private string _workorderStatusId = null;
        private string _workorderTypeId = null;
        private string _statusName = null;
        private string _description = null;
        private int? _statusTag = null;
        private string _customStatus = null;
        private int? _status = null;
        private int? _sortOrder = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "workorder_status_id", PrimaryKey = true)]
        public string WorkorderStatusId
        {
            get { return _workorderStatusId; }
            set { _workorderStatusId = value; }
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
        /// 状态名称 
        /// </summary>
        [TableMapping(FieldName = "status_name")]
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 描述 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 开始标志 
        /// </summary>
        [TableMapping(FieldName = "status_tag")]
        public int? StatusTag
        {
            get { return _statusTag; }
            set { _statusTag = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "custom_status")]
        public string CustomStatus
        {
            get { return _customStatus; }
            set { _customStatus = value; }
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
        /// 排序 
        /// </summary>
        [TableMapping(FieldName = "sort_order")]
        public int? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
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
