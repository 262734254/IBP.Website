/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-2-10
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
	/// RelUsergroupPremission实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="rel_usergroup_premission")]
	public class RelUsergroupPremissionModel : BaseModel
	{
		private string _usergroupId = null;
		private string _premissionType = null;
		private string _workorderTypeId = null;
		private int? _canSelect = null;
		private int? _canProcess = null;
		private int? _canClose = null;
		private int? _canDelete = null;
		private int? _isManage = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 用户组ID 
		/// </summary>
		[TableMapping(FieldName="usergroup_id")]
		public string UsergroupId
		{
			get { return _usergroupId; }
			set { _usergroupId = value; }
		}

		/// <summary>
		/// 权限类型 
		/// </summary>
		[TableMapping(FieldName="premission_type")]
		public string PremissionType
		{
			get { return _premissionType; }
			set { _premissionType = value; }
		}

		/// <summary>
		/// 工单类型ID 
		/// </summary>
		[TableMapping(FieldName="workorder_type_id")]
		public string WorkorderTypeId
		{
			get { return _workorderTypeId; }
			set { _workorderTypeId = value; }
		}

		/// <summary>
		/// 查询权限 
		/// </summary>
		[TableMapping(FieldName="can_select")]
		public int? CanSelect
		{
			get { return _canSelect; }
			set { _canSelect = value; }
		}

		/// <summary>
		/// 处理权限 
		/// </summary>
		[TableMapping(FieldName="can_process")]
		public int? CanProcess
		{
			get { return _canProcess; }
			set { _canProcess = value; }
		}

		/// <summary>
		/// 关闭权限 
		/// </summary>
		[TableMapping(FieldName="can_close")]
		public int? CanClose
		{
			get { return _canClose; }
			set { _canClose = value; }
		}

		/// <summary>
		/// 删除权限 
		/// </summary>
		[TableMapping(FieldName="can_delete")]
		public int? CanDelete
		{
			get { return _canDelete; }
			set { _canDelete = value; }
		}

		/// <summary>
		/// 完全管理权限 
		/// </summary>
		[TableMapping(FieldName="is_manage")]
		public int? IsManage
		{
			get { return _isManage; }
			set { _isManage = value; }
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

