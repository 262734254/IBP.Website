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
    /// RelUserGroup实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "rel_user_group")]
    public class RelUserGroupModel : BaseModel
    {
        private string _userId = null;
        private string _groupId = null;
        private int? _userStatusInGroup = null;
        private int? _roleInGroup = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 用户ID 
        /// </summary>
        [TableMapping(FieldName = "user_id")]
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// 组ID 
        /// </summary>
        [TableMapping(FieldName = "group_id")]
        public string GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        /// <summary>
        /// 用户在组中状态 
        /// </summary>
        [TableMapping(FieldName = "user_status_in_group")]
        public int? UserStatusInGroup
        {
            get { return _userStatusInGroup; }
            set { _userStatusInGroup = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "role_in_group")]
        public int? RoleInGroup
        {
            get { return _roleInGroup; }
            set { _roleInGroup = value; }
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
