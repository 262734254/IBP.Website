/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-24
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
    /// VoteProjectInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "vote_project_info")]
    public class VoteProjectInfoModel : BaseModel
    {
        private string _voteProjectId = null;
        private string _projectName = null;
        private string _description = null;
        private DateTime? _beginTime = null;
        private DateTime? _endTime = null;
        private int? _canMuiltVote = null;
        private int? _canAnonymous = null;
        private int? _canModifyVote = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "vote_project_id", PrimaryKey = true)]
        public string VoteProjectId
        {
            get { return _voteProjectId; }
            set { _voteProjectId = value; }
        }

        /// <summary>
        /// 投票项目名称 
        /// </summary>
        [TableMapping(FieldName = "project_name")]
        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        /// <summary>
        /// 描述信息 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 开始时 
        /// </summary>
        [TableMapping(FieldName = "begin_time")]
        public DateTime? BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        /// <summary>
        /// 结束时间 
        /// </summary>
        [TableMapping(FieldName = "end_time")]
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// 是否可重复投票 
        /// </summary>
        [TableMapping(FieldName = "can_muilt_vote")]
        public int? CanMuiltVote
        {
            get { return _canMuiltVote; }
            set { _canMuiltVote = value; }
        }

        /// <summary>
        /// 是否匿名投票 
        /// </summary>
        [TableMapping(FieldName = "can_anonymous")]
        public int? CanAnonymous
        {
            get { return _canAnonymous; }
            set { _canAnonymous = value; }
        }

        /// <summary>
        /// 是否可修改投票 
        /// </summary>
        [TableMapping(FieldName = "can_modify_vote")]
        public int? CanModifyVote
        {
            get { return _canModifyVote; }
            set { _canModifyVote = value; }
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
