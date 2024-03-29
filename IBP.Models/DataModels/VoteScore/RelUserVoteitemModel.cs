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
	/// RelUserVoteitem实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="rel_user_voteitem")]
	public class RelUserVoteitemModel : BaseModel
	{
		private string _userVoteId = null;
		private string _userId = null;
		private string _voteProjectId = null;
		private string _voteItemId = null;
		private string _score = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="user_vote_id",PrimaryKey=true)]
		public string UserVoteId
		{
			get { return _userVoteId; }
			set { _userVoteId = value; }
		}

		/// <summary>
		/// 用户ID 
		/// </summary>
		[TableMapping(FieldName="user_id")]
		public string UserId
		{
			get { return _userId; }
			set { _userId = value; }
		}

		/// <summary>
		/// 投票项目ID 
		/// </summary>
		[TableMapping(FieldName="vote_project_id")]
		public string VoteProjectId
		{
			get { return _voteProjectId; }
			set { _voteProjectId = value; }
		}

		/// <summary>
		/// 投票栏目ID 
		/// </summary>
		[TableMapping(FieldName="vote_item_id")]
		public string VoteItemId
		{
			get { return _voteItemId; }
			set { _voteItemId = value; }
		}

		/// <summary>
		/// 投票分数 
		/// </summary>
		[TableMapping(FieldName="score")]
		public string Score
		{
			get { return _score; }
			set { _score = value; }
		}

		/// <summary>
		/// 状态 
		/// </summary>
		[TableMapping(FieldName="status")]
		public int? Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="created_on")]
		public DateTime? CreatedOn
		{
			get { return _createdOn; }
			set { _createdOn = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="created_by")]
		public string CreatedBy
		{
			get { return _createdBy; }
			set { _createdBy = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="modified_on")]
		public DateTime? ModifiedOn
		{
			get { return _modifiedOn; }
			set { _modifiedOn = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="modified_by")]
		public string ModifiedBy
		{
			get { return _modifiedBy; }
			set { _modifiedBy = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="status_code")]
		public int? StatusCode
		{
			get { return _statusCode; }
			set { _statusCode = value; }
		}

	}
}

