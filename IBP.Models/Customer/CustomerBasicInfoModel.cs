/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
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
	/// CustomerBasicInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="customer_basic_info")]
	public class CustomerBasicInfoModel : BaseModel
	{
		private string _customerId = null;
		private string _customerCode = null;
		private int? _sex = null;
		private string _salesFrom = null;
		private string _level = null;
		private string _nowOwnerId = null;
		private string _mobilePhone = null;
		private string _homePhone = null;
		private string _officePhone = null;
		private string _otherPhone = null;
		private string _comeFrom = null;
		private string _carriers = null;
		private string _usingPhoneBrand = null;
		private string _usingPhoneType = null;
		private string _communicationConsumer = null;
		private string _preferredPhoneBrand = null;
		private int? _usingSmartphone = null;
		private string _mobilePhonePrice = null;
		private DateTime? _birthday = null;
		private string _idcardType = null;
		private string _idcardNumber = null;
		private int? _status = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="customer_id",PrimaryKey=true)]
		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		/// <summary>
		/// 客户编号 
		/// </summary>
		[TableMapping(FieldName="customer_code")]
		public string CustomerCode
		{
			get { return _customerCode; }
			set { _customerCode = value; }
		}

		/// <summary>
		/// 性别 
		/// </summary>
		[TableMapping(FieldName="sex")]
		public int? Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}

		/// <summary>
		/// 客户来源 
		/// </summary>
		[TableMapping(FieldName="sales_from")]
		public string SalesFrom
		{
			get { return _salesFrom; }
			set { _salesFrom = value; }
		}

		/// <summary>
		/// 客户等级 
		/// </summary>
		[TableMapping(FieldName="level")]
		public string Level
		{
			get { return _level; }
			set { _level = value; }
		}

		/// <summary>
		/// 当前归属用户 
		/// </summary>
		[TableMapping(FieldName="now_owner_id")]
		public string NowOwnerId
		{
			get { return _nowOwnerId; }
			set { _nowOwnerId = value; }
		}

		/// <summary>
		/// 手机号码 
		/// </summary>
		[TableMapping(FieldName="mobile_phone")]
		public string MobilePhone
		{
			get { return _mobilePhone; }
			set { _mobilePhone = value; }
		}

		/// <summary>
		/// 住宅电话 
		/// </summary>
		[TableMapping(FieldName="home_phone")]
		public string HomePhone
		{
			get { return _homePhone; }
			set { _homePhone = value; }
		}

		/// <summary>
		/// 办公电话 
		/// </summary>
		[TableMapping(FieldName="office_phone")]
		public string OfficePhone
		{
			get { return _officePhone; }
			set { _officePhone = value; }
		}

		/// <summary>
		/// 其他电话 
		/// </summary>
		[TableMapping(FieldName="other_phone")]
		public string OtherPhone
		{
			get { return _otherPhone; }
			set { _otherPhone = value; }
		}

		/// <summary>
		/// 归属地 
		/// </summary>
		[TableMapping(FieldName="come_from")]
		public string ComeFrom
		{
			get { return _comeFrom; }
			set { _comeFrom = value; }
		}

		/// <summary>
		/// 运营商 
		/// </summary>
		[TableMapping(FieldName="carriers")]
		public string Carriers
		{
			get { return _carriers; }
			set { _carriers = value; }
		}

		/// <summary>
		/// 在用手机品牌 
		/// </summary>
		[TableMapping(FieldName="using_phone_brand")]
		public string UsingPhoneBrand
		{
			get { return _usingPhoneBrand; }
			set { _usingPhoneBrand = value; }
		}

		/// <summary>
		/// 在用手机型号 
		/// </summary>
		[TableMapping(FieldName="using_phone_type")]
		public string UsingPhoneType
		{
			get { return _usingPhoneType; }
			set { _usingPhoneType = value; }
		}

		/// <summary>
		/// 通讯消费 
		/// </summary>
		[TableMapping(FieldName="communication_consumer")]
		public string CommunicationConsumer
		{
			get { return _communicationConsumer; }
			set { _communicationConsumer = value; }
		}

		/// <summary>
		/// 优选手机品牌 
		/// </summary>
		[TableMapping(FieldName="preferred_phone_brand")]
		public string PreferredPhoneBrand
		{
			get { return _preferredPhoneBrand; }
			set { _preferredPhoneBrand = value; }
		}

		/// <summary>
		/// 是否智能机 
		/// </summary>
		[TableMapping(FieldName="using_smartphone")]
		public int? UsingSmartphone
		{
			get { return _usingSmartphone; }
			set { _usingSmartphone = value; }
		}

		/// <summary>
		/// 手机价位 
		/// </summary>
		[TableMapping(FieldName="mobile_phone_price")]
		public string MobilePhonePrice
		{
			get { return _mobilePhonePrice; }
			set { _mobilePhonePrice = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="birthday")]
		public DateTime? Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

		/// <summary>
		/// 证件类型 
		/// </summary>
		[TableMapping(FieldName="idcard_type")]
		public string IdcardType
		{
			get { return _idcardType; }
			set { _idcardType = value; }
		}

		/// <summary>
		/// 证件号码 
		/// </summary>
		[TableMapping(FieldName="idcard_number")]
		public string IdcardNumber
		{
			get { return _idcardNumber; }
			set { _idcardNumber = value; }
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

