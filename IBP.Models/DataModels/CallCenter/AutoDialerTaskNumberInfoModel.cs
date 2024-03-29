/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-27
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
	/// AutoDialerTaskNumberInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="auto_dialer_task_number_info")]
	public class AutoDialerTaskNumberInfoModel : BaseModel
	{
		private string _autoDialerNumberId = null;
		private string _autodialerTaskId = null;
		private string _dialerNumber = null;
		private string _province = null;
		private string _city = null;
		private int? _valid = null;
		private int? _connected = null;
		private int? _callTimes = null;
		private int? _recallCount = null;
		private int? _ivrDialerProjectId = null;
		private string _calledResult = null;
		private string _returnCode = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="auto_dialer_number_id",PrimaryKey=true)]
		public string AutoDialerNumberId
		{
			get { return _autoDialerNumberId; }
			set { _autoDialerNumberId = value; }
		}

		/// <summary>
		/// 关联的任务ID 
		/// </summary>
		[TableMapping(FieldName="autodialer_task_id")]
		public string AutodialerTaskId
		{
			get { return _autodialerTaskId; }
			set { _autodialerTaskId = value; }
		}

		/// <summary>
		/// 外呼号码 
		/// </summary>
		[TableMapping(FieldName="dialer_number")]
		public string DialerNumber
		{
			get { return _dialerNumber; }
			set { _dialerNumber = value; }
		}

		/// <summary>
		/// 省份 
		/// </summary>
		[TableMapping(FieldName="province")]
		public string Province
		{
			get { return _province; }
			set { _province = value; }
		}

		/// <summary>
		/// 城市 
		/// </summary>
		[TableMapping(FieldName="city")]
		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		/// <summary>
		/// 是否有效 
		/// </summary>
		[TableMapping(FieldName="valid")]
		public int? Valid
		{
			get { return _valid; }
			set { _valid = value; }
		}

		/// <summary>
		/// 是否接通 
		/// </summary>
		[TableMapping(FieldName="connected")]
		public int? Connected
		{
			get { return _connected; }
			set { _connected = value; }
		}

		/// <summary>
		/// 通话时长 
		/// </summary>
		[TableMapping(FieldName="call_times")]
		public int? CallTimes
		{
			get { return _callTimes; }
			set { _callTimes = value; }
		}

		/// <summary>
		/// 重拔次数 
		/// </summary>
		[TableMapping(FieldName="recall_count")]
		public int? RecallCount
		{
			get { return _recallCount; }
			set { _recallCount = value; }
		}

		/// <summary>
		/// IVR系统自动外呼项目ID 
		/// </summary>
		[TableMapping(FieldName="ivr_dialer_project_id")]
		public int? IvrDialerProjectId
		{
			get { return _ivrDialerProjectId; }
			set { _ivrDialerProjectId = value; }
		}

		/// <summary>
		/// 呼叫结果 
		/// </summary>
		[TableMapping(FieldName="called_result")]
		public string CalledResult
		{
			get { return _calledResult; }
			set { _calledResult = value; }
		}

		/// <summary>
		/// 返回代码 
		/// </summary>
		[TableMapping(FieldName="return_code")]
		public string ReturnCode
		{
			get { return _returnCode; }
			set { _returnCode = value; }
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

