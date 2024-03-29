/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-23
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
    /// VoteProjectItemInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "vote_project_item_info")]
    public class VoteProjectItemInfoModel : BaseModel
    {
        private string _voteItemId = null;
        private string _voteProjectId = null;
        private string _itemTitle = null;
        private string _description = null;
        private string _attachmentPath = null;
        private int? _voteTotal = null;
        private int? _voteScore = null;
        private string _candidater = null;
        private string _candidaterName = null;
        private int? _sortOrder = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "vote_item_id", PrimaryKey = true)]
        public string VoteItemId
        {
            get { return _voteItemId; }
            set { _voteItemId = value; }
        }

        /// <summary>
        /// 投票项目ID 
        /// </summary>
        [TableMapping(FieldName = "vote_project_id")]
        public string VoteProjectId
        {
            get { return _voteProjectId; }
            set { _voteProjectId = value; }
        }

        /// <summary>
        /// 栏目标题 
        /// </summary>
        [TableMapping(FieldName = "item_title")]
        public string ItemTitle
        {
            get { return _itemTitle; }
            set { _itemTitle = value; }
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
        /// 附件路径 
        /// </summary>
        [TableMapping(FieldName = "attachment_path")]
        public string AttachmentPath
        {
            get { return _attachmentPath; }
            set { _attachmentPath = value; }
        }

        /// <summary>
        /// 得票总数 
        /// </summary>
        [TableMapping(FieldName = "vote_total")]
        public int? VoteTotal
        {
            get { return _voteTotal; }
            set { _voteTotal = value; }
        }

        /// <summary>
        /// 得票总分 
        /// </summary>
        [TableMapping(FieldName = "vote_score")]
        public int? VoteScore
        {
            get { return _voteScore; }
            set { _voteScore = value; }
        }

        /// <summary>
        /// 候选人ID 
        /// </summary>
        [TableMapping(FieldName = "candidater")]
        public string Candidater
        {
            get { return _candidater; }
            set { _candidater = value; }
        }

        /// <summary>
        /// 候选人名称 
        /// </summary>
        [TableMapping(FieldName = "candidater_name")]
        public string CandidaterName
        {
            get { return _candidaterName; }
            set { _candidaterName = value; }
        }

        /// <summary>
        /// 排序索引 
        /// </summary>
        [TableMapping(FieldName = "sort_order")]
        public int? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
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
