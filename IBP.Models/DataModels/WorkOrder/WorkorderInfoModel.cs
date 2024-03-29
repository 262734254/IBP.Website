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
    /// WorkorderInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "workorder_info")]
    public class WorkorderInfoModel : BaseModel
    {
        private string _workOrderId = null;
        private string _workorderCode = null;
        private string _workorderType = null;
        private string _nowStatusId = null;
        private string _nowResultId = null;
        private string _firstProcessUserid = null;
        private DateTime? _firstProcessTime = null;
        private string _nowProcessUserid = null;
        private string _nowContactResult = null;
        private string _relUsergroupId = null;
        private string _level = null;
        private string _relCustomerId = null;
        private string _relOrderId = null;
        private string _relOrderDescription = null;
        private string _closedUser = null;
        private DateTime? _advanceTime = null;
        private DateTime? _expiredTime = null;
        private DateTime? _closedTime = null;
        private string _description = null;
        private int? _qualityCheckStatus = null;
        private int? _statusForUser = null;
        private int? _processStatus = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "work_order_id", PrimaryKey = true)]
        public string WorkOrderId
        {
            get { return _workOrderId; }
            set { _workOrderId = value; }
        }

        /// <summary>
        /// 工单编号 
        /// </summary>
        [TableMapping(FieldName = "workorder_code")]
        public string WorkorderCode
        {
            get { return _workorderCode; }
            set { _workorderCode = value; }
        }

        /// <summary>
        /// 类型，0手机销售工单 
        /// </summary>
        [TableMapping(FieldName = "workorder_type")]
        public string WorkorderType
        {
            get { return _workorderType; }
            set { _workorderType = value; }
        }

        /// <summary>
        /// 当前状态 
        /// </summary>
        [TableMapping(FieldName = "now_status_id")]
        public string NowStatusId
        {
            get { return _nowStatusId; }
            set { _nowStatusId = value; }
        }

        /// <summary>
        /// 当前结果 
        /// </summary>
        [TableMapping(FieldName = "now_result_id")]
        public string NowResultId
        {
            get { return _nowResultId; }
            set { _nowResultId = value; }
        }

        /// <summary>
        /// 开始处理人 
        /// </summary>
        [TableMapping(FieldName = "first_process_userid")]
        public string FirstProcessUserid
        {
            get { return _firstProcessUserid; }
            set { _firstProcessUserid = value; }
        }

        /// <summary>
        /// 开始处理时间 
        /// </summary>
        [TableMapping(FieldName = "first_process_time")]
        public DateTime? FirstProcessTime
        {
            get { return _firstProcessTime; }
            set { _firstProcessTime = value; }
        }

        /// <summary>
        /// 当前处理人 
        /// </summary>
        [TableMapping(FieldName = "now_process_userid")]
        public string NowProcessUserid
        {
            get { return _nowProcessUserid; }
            set { _nowProcessUserid = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "now_contact_result")]
        public string NowContactResult
        {
            get { return _nowContactResult; }
            set { _nowContactResult = value; }
        }

        /// <summary>
        /// 关联的用户组ID 
        /// </summary>
        [TableMapping(FieldName = "rel_usergroup_id")]
        public string RelUsergroupId
        {
            get { return _relUsergroupId; }
            set { _relUsergroupId = value; }
        }

        /// <summary>
        /// 工单优先级ID 
        /// </summary>
        [TableMapping(FieldName = "level")]
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// 关联客户 
        /// </summary>
        [TableMapping(FieldName = "rel_customer_id")]
        public string RelCustomerId
        {
            get { return _relCustomerId; }
            set { _relCustomerId = value; }
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
        /// 关联订单描述 
        /// </summary>
        [TableMapping(FieldName = "rel_order_description")]
        public string RelOrderDescription
        {
            get { return _relOrderDescription; }
            set { _relOrderDescription = value; }
        }

        /// <summary>
        /// 工单关闭用户ID 
        /// </summary>
        [TableMapping(FieldName = "closed_user")]
        public string ClosedUser
        {
            get { return _closedUser; }
            set { _closedUser = value; }
        }

        /// <summary>
        /// 预约时间 
        /// </summary>
        [TableMapping(FieldName = "advance_time")]
        public DateTime? AdvanceTime
        {
            get { return _advanceTime; }
            set { _advanceTime = value; }
        }

        /// <summary>
        /// 过期时间 
        /// </summary>
        [TableMapping(FieldName = "expired_time")]
        public DateTime? ExpiredTime
        {
            get { return _expiredTime; }
            set { _expiredTime = value; }
        }

        /// <summary>
        /// 关闭时间 
        /// </summary>
        [TableMapping(FieldName = "closed_time")]
        public DateTime? ClosedTime
        {
            get { return _closedTime; }
            set { _closedTime = value; }
        }

        /// <summary>
        /// 工单描述 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 质检状态，0待检，1已检 
        /// </summary>
        [TableMapping(FieldName = "quality_check_status")]
        public int? QualityCheckStatus
        {
            get { return _qualityCheckStatus; }
            set { _qualityCheckStatus = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "status_for_user")]
        public int? StatusForUser
        {
            get { return _statusForUser; }
            set { _statusForUser = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "process_status")]
        public int? ProcessStatus
        {
            get { return _processStatus; }
            set { _processStatus = value; }
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
