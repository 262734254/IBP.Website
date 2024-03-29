/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-9
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
	/// RelRoleAction实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="rel_role_action")]
	public class RelRoleActionModel : BaseModel
	{
		private string _roleId = null;
		private string _actionId = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 角色ID 
		/// </summary>
		[TableMapping(FieldName="role_id")]
		public string RoleId
		{
			get { return _roleId; }
			set { _roleId = value; }
		}

		/// <summary>
		/// 操作ID 
		/// </summary>
		[TableMapping(FieldName="action_id")]
		public string ActionId
		{
			get { return _actionId; }
			set { _actionId = value; }
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

