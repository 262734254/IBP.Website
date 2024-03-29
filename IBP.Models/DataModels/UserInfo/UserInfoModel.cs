/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-3-29
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
    /// UserInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "user_info")]
    public class UserInfoModel : BaseModel
    {
        private string _userId = null;
        private string _roleId = null;
        private string _roleIdInGroup = null;
        private string _workId = null;
        private string _cnName = null;
        private string _enName = null;
        private string _loginPwd = null;
        private string _loginName = null;
        private string _ctiUserId = null;
        private string _ctiUserPwd = null;
        private string _userEmail = null;
        private DateTime? _entryDate = null;
        private DateTime? _positiveDate = null;
        private DateTime? _leaveDate = null;
        private string _departmentId = null;
        private string _postName = null;
        private string _teamName = null;
        private string _workStatus = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 用户ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "user_id", PrimaryKey = true)]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// 角色ID 
        /// </summary>
        [TableMapping(FieldName = "role_id")]
        public string RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "role_id_in_group")]
        public string RoleIdInGroup
        {
            get { return _roleIdInGroup; }
            set { _roleIdInGroup = value; }
        }

        /// <summary>
        /// 工号 
        /// </summary>
        [TableMapping(FieldName = "work_id")]
        public string WorkId
        {
            get { return _workId; }
            set { _workId = value; }
        }

        /// <summary>
        /// 中文名 
        /// </summary>
        [TableMapping(FieldName = "cn_name")]
        public string CnName
        {
            get { return _cnName; }
            set { _cnName = value; }
        }

        /// <summary>
        /// 英文名 
        /// </summary>
        [TableMapping(FieldName = "en_name")]
        public string EnName
        {
            get { return _enName; }
            set { _enName = value; }
        }

        /// <summary>
        /// 登录密码 
        /// </summary>
        [TableMapping(FieldName = "login_pwd")]
        public string LoginPwd
        {
            get { return _loginPwd; }
            set { _loginPwd = value; }
        }

        /// <summary>
        /// 登录名称 
        /// </summary>
        [TableMapping(FieldName = "login_name")]
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "cti_user_id")]
        public string CtiUserId
        {
            get { return _ctiUserId; }
            set { _ctiUserId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "cti_user_pwd")]
        public string CtiUserPwd
        {
            get { return _ctiUserPwd; }
            set { _ctiUserPwd = value; }
        }

        /// <summary>
        /// 用户邮箱 
        /// </summary>
        [TableMapping(FieldName = "user_email")]
        public string UserEmail
        {
            get { return _userEmail; }
            set { _userEmail = value; }
        }

        /// <summary>
        /// 入职日期 
        /// </summary>
        [TableMapping(FieldName = "entry_date")]
        public DateTime? EntryDate
        {
            get { return _entryDate; }
            set { _entryDate = value; }
        }

        /// <summary>
        /// 转正日期 
        /// </summary>
        [TableMapping(FieldName = "positive_date")]
        public DateTime? PositiveDate
        {
            get { return _positiveDate; }
            set { _positiveDate = value; }
        }

        /// <summary>
        /// 离职日期 
        /// </summary>
        [TableMapping(FieldName = "leave_date")]
        public DateTime? LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }

        /// <summary>
        /// 部门ID 
        /// </summary>
        [TableMapping(FieldName = "department_id")]
        public string DepartmentId
        {
            get { return _departmentId; }
            set { _departmentId = value; }
        }

        /// <summary>
        /// 职务 
        /// </summary>
        [TableMapping(FieldName = "post_name")]
        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        /// <summary>
        /// 团队名称 
        /// </summary>
        [TableMapping(FieldName = "team_name")]
        public string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; }
        }

        /// <summary>
        /// 工作状态：1试用，2转正，3离职 
        /// </summary>
        [TableMapping(FieldName = "work_status")]
        public string WorkStatus
        {
            get { return _workStatus; }
            set { _workStatus = value; }
        }

        /// <summary>
        /// 账号状态，0启用，1禁用 
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
