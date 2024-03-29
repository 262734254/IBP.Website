/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-13
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
	/// PayPosMachineInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="pay_pos_machine_info")]
	public class PayPosMachineInfoModel : BaseModel
	{
		private string _posMachineId = null;
		private string _payFromCityId = null;
		private string _payMachineName = null;
		private string _posMachineCode = null;
		private string _description = null;
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
		[TableMapping(FieldName="pos_machine_id",PrimaryKey=true)]
		public string PosMachineId
		{
			get { return _posMachineId; }
			set { _posMachineId = value; }
		}

		/// <summary>
		/// POS机所属城市ID 
		/// </summary>
		[TableMapping(FieldName="pay_from_city_id")]
		public string PayFromCityId
		{
			get { return _payFromCityId; }
			set { _payFromCityId = value; }
		}

		/// <summary>
		/// POS机名称标识 
		/// </summary>
		[TableMapping(FieldName="pay_machine_name")]
		public string PayMachineName
		{
			get { return _payMachineName; }
			set { _payMachineName = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="pos_machine_code")]
		public string PosMachineCode
		{
			get { return _posMachineCode; }
			set { _posMachineCode = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// 排序索引 
		/// </summary>
		[TableMapping(FieldName="sort_order")]
		public int? SortOrder
		{
			get { return _sortOrder; }
			set { _sortOrder = value; }
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

