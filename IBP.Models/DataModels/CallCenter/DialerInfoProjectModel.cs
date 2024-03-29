/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-26
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
	/// DialerInfoProject实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="Dialer_Info_Project")]
	public class DialerInfoProjectModel : BaseModel
	{
		private int? _projectID = null;
		private string _projectName = null;
		private string _projectCmt = null;
		private int? _campaignID = null;
		private string _status = null;
		private string _startDate = null;
		private string _endDate = null;
		private string _startTime1 = null;
		private string _endTime1 = null;
		private string _startTime2 = null;
		private string _endTime2 = null;
		private string _startTime3 = null;
		private string _endTime3 = null;
		private string _startTime4 = null;
		private string _endTime4 = null;
		private string _creator = null;
		private string _modifier = null;
		private string _createTime = null;
		private string _modifyTime = null;
		private string _createIP = null;
		private string _modifyIP = null;

		/// <summary>
		///  (主键) 
		/// </summary>
		[TableMapping(FieldName="ProjectID",PrimaryKey=true)]
		public int? Projectid
		{
			get { return _projectID; }
			set { _projectID = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="ProjectName")]
		public string Projectname
		{
			get { return _projectName; }
			set { _projectName = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="ProjectCmt")]
		public string Projectcmt
		{
			get { return _projectCmt; }
			set { _projectCmt = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="CampaignID")]
		public int? Campaignid
		{
			get { return _campaignID; }
			set { _campaignID = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Status")]
		public string Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Start_Date")]
		public string StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="End_Date")]
		public string EndDate
		{
			get { return _endDate; }
			set { _endDate = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Start_Time1")]
		public string StartTime1
		{
			get { return _startTime1; }
			set { _startTime1 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="End_Time1")]
		public string EndTime1
		{
			get { return _endTime1; }
			set { _endTime1 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Start_Time2")]
		public string StartTime2
		{
			get { return _startTime2; }
			set { _startTime2 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="End_Time2")]
		public string EndTime2
		{
			get { return _endTime2; }
			set { _endTime2 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Start_Time3")]
		public string StartTime3
		{
			get { return _startTime3; }
			set { _startTime3 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="End_Time3")]
		public string EndTime3
		{
			get { return _endTime3; }
			set { _endTime3 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Start_Time4")]
		public string StartTime4
		{
			get { return _startTime4; }
			set { _startTime4 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="End_Time4")]
		public string EndTime4
		{
			get { return _endTime4; }
			set { _endTime4 = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Creator")]
		public string Creator
		{
			get { return _creator; }
			set { _creator = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Modifier")]
		public string Modifier
		{
			get { return _modifier; }
			set { _modifier = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Create_Time")]
		public string CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Modify_Time")]
		public string ModifyTime
		{
			get { return _modifyTime; }
			set { _modifyTime = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Create_IP")]
		public string CreateIp
		{
			get { return _createIP; }
			set { _createIP = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="Modify_IP")]
		public string ModifyIp
		{
			get { return _modifyIP; }
			set { _modifyIP = value; }
		}

	}
}

