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
	/// AutoDialerTaskInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="auto_dialer_task_info")]
	public class AutoDialerTaskInfoModel : BaseModel
	{
		private string _autoDialerTaskId = null;
		private string _autoDialerTaskCode = null;
		private string _autoDialerTaskName = null;
		private DateTime? _beginTime = null;
		private DateTime? _endTime = null;
		private string _startTime1 = null;
		private string _startTime2 = null;
		private string _startTime3 = null;
		private string _startTime4 = null;
		private string _stopTime1 = null;
		private string _stopTime2 = null;
		private string _stopTime3 = null;
		private string _stopTime4 = null;
		private int? _interval = null;
		private int? _ivrDialerProjectId = null;
		private int? _retryCount = null;
		private int? _priority = null;
		private int? _dialerNumberTotal = null;
		private string _description = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 自动外呼任务ID (主键) 
		/// </summary>
		[TableMapping(FieldName="auto_dialer_task_id",PrimaryKey=true)]
		public string AutoDialerTaskId
		{
			get { return _autoDialerTaskId; }
			set { _autoDialerTaskId = value; }
		}

		/// <summary>
		/// 自动外呼任务编号 
		/// </summary>
		[TableMapping(FieldName="auto_dialer_task_code")]
		public string AutoDialerTaskCode
		{
			get { return _autoDialerTaskCode; }
			set { _autoDialerTaskCode = value; }
		}

		/// <summary>
		/// 自动外呼任务名称 
		/// </summary>
		[TableMapping(FieldName="auto_dialer_task_name")]
		public string AutoDialerTaskName
		{
			get { return _autoDialerTaskName; }
			set { _autoDialerTaskName = value; }
		}

		/// <summary>
		/// 外呼开始时间 
		/// </summary>
		[TableMapping(FieldName="begin_time")]
		public DateTime? BeginTime
		{
			get { return _beginTime; }
			set { _beginTime = value; }
		}

		/// <summary>
		/// 外呼结束时间 
		/// </summary>
		[TableMapping(FieldName="end_time")]
		public DateTime? EndTime
		{
			get { return _endTime; }
			set { _endTime = value; }
		}

		/// <summary>
		/// 起始时段1 
		/// </summary>
		[TableMapping(FieldName="start_time1")]
		public string StartTime1
		{
			get { return _startTime1; }
			set { _startTime1 = value; }
		}

		/// <summary>
		/// 起始时段2 
		/// </summary>
		[TableMapping(FieldName="start_time2")]
		public string StartTime2
		{
			get { return _startTime2; }
			set { _startTime2 = value; }
		}

		/// <summary>
		/// 起始时段3 
		/// </summary>
		[TableMapping(FieldName="start_time3")]
		public string StartTime3
		{
			get { return _startTime3; }
			set { _startTime3 = value; }
		}

		/// <summary>
		/// 起始时段4 
		/// </summary>
		[TableMapping(FieldName="start_time4")]
		public string StartTime4
		{
			get { return _startTime4; }
			set { _startTime4 = value; }
		}

		/// <summary>
		/// 截止时段1 
		/// </summary>
		[TableMapping(FieldName="stop_time1")]
		public string StopTime1
		{
			get { return _stopTime1; }
			set { _stopTime1 = value; }
		}

		/// <summary>
		/// 截止时段2 
		/// </summary>
		[TableMapping(FieldName="stop_time2")]
		public string StopTime2
		{
			get { return _stopTime2; }
			set { _stopTime2 = value; }
		}

		/// <summary>
		/// 截止时段3 
		/// </summary>
		[TableMapping(FieldName="stop_time3")]
		public string StopTime3
		{
			get { return _stopTime3; }
			set { _stopTime3 = value; }
		}

		/// <summary>
		/// 截止时段4 
		/// </summary>
		[TableMapping(FieldName="stop_time4")]
		public string StopTime4
		{
			get { return _stopTime4; }
			set { _stopTime4 = value; }
		}

		/// <summary>
		/// 外呼间隔时间，单位：分钟 
		/// </summary>
		[TableMapping(FieldName="interval")]
		public int? Interval
		{
			get { return _interval; }
			set { _interval = value; }
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
		/// 失败尝试次数 
		/// </summary>
		[TableMapping(FieldName="retry_count")]
		public int? RetryCount
		{
			get { return _retryCount; }
			set { _retryCount = value; }
		}

		/// <summary>
		/// 优先级 
		/// </summary>
		[TableMapping(FieldName="priority")]
		public int? Priority
		{
			get { return _priority; }
			set { _priority = value; }
		}

		/// <summary>
		/// 外呼号码总数 
		/// </summary>
		[TableMapping(FieldName="dialer_number_total")]
		public int? DialerNumberTotal
		{
			get { return _dialerNumberTotal; }
			set { _dialerNumberTotal = value; }
		}

		/// <summary>
		/// 任务描述 
		/// </summary>
		[TableMapping(FieldName="description")]
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		/// <summary>
		/// 状态，0新建，1执行中，2执行完成，3失败 
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

