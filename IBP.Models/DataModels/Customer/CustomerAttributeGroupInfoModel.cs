/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-7
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
	/// CustomerAttributeGroupInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="customer_attribute_group_info")]
	public class CustomerAttributeGroupInfoModel : BaseModel
	{
		private string _groupId = null;
		private string _groupName = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;
		private string _tabname = null;

		/// <summary>
		///  (主键) 
		/// </summary>
		[TableMapping(FieldName="group_id",PrimaryKey=true)]
		public string GroupId
		{
			get { return _groupId; }
			set { _groupId = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="group_name")]
		public string GroupName
		{
			get { return _groupName; }
			set { _groupName = value; }
		}

		/// <summary>
		///  
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

		/// <summary>
		///  
		/// </summary>
        [TableMapping(FieldName = "tabname")]
		public string Tabname
		{
			get { return _tabname; }
			set { _tabname = value; }
		}

	}
}

