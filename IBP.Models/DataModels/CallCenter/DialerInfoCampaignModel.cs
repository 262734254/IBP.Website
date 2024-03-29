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
	/// DialerInfoCampaign实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="Dialer_Info_Campaign")]
	public class DialerInfoCampaignModel : BaseModel
	{
		private int? _campaignID = null;
		private string _campaignName = null;
		private string _campaignCmt = null;
		private int? _tIMES = null;
		private int? _iNTERVAL = null;
		private int? _acdType = null;
		private string _routePoint = null;
		private int? _maxPorts = null;
		private int? _maxTimeOut = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 和Dialer_Info_Project的CampaignID对应。 
		/// </summary>
		[TableMapping(FieldName="CampaignID")]
		public int? Campaignid
		{
			get { return _campaignID; }
			set { _campaignID = value; }
		}

		/// <summary>
		/// 策略名称 
		/// </summary>
		[TableMapping(FieldName="CampaignName")]
		public string Campaignname
		{
			get { return _campaignName; }
			set { _campaignName = value; }
		}

		/// <summary>
		/// 策略描述 
		/// </summary>
		[TableMapping(FieldName="CampaignCmt")]
		public string Campaigncmt
		{
			get { return _campaignCmt; }
			set { _campaignCmt = value; }
		}

		/// <summary>
		/// 外拨失败重试次数 
		/// </summary>
		[TableMapping(FieldName="TIMES")]
		public int? Times
		{
			get { return _tIMES; }
			set { _tIMES = value; }
		}

		/// <summary>
		/// 外拨失败重试时间间隔 
		/// </summary>
		[TableMapping(FieldName="INTERVAL")]
		public int? Interval
		{
			get { return _iNTERVAL; }
			set { _iNTERVAL = value; }
		}

		/// <summary>
		/// 最大振铃次数 
		/// </summary>
		[TableMapping(FieldName="AcdType")]
		public int? Acdtype
		{
			get { return _acdType; }
			set { _acdType = value; }
		}

		/// <summary>
		/// 排队技能组名称 
		/// </summary>
		[TableMapping(FieldName="RoutePoint")]
		public string Routepoint
		{
			get { return _routePoint; }
			set { _routePoint = value; }
		}

		/// <summary>
		/// 最大并发端口数 
		/// </summary>
		[TableMapping(FieldName="MaxPorts")]
		public int? Maxports
		{
			get { return _maxPorts; }
			set { _maxPorts = value; }
		}

		/// <summary>
		/// 本策略的最大超时时间 
		/// </summary>
		[TableMapping(FieldName="MaxTimeOut")]
		public int? Maxtimeout
		{
			get { return _maxTimeOut; }
			set { _maxTimeOut = value; }
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

