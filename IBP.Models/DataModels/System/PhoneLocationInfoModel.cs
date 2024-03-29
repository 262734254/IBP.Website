/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-31
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
	/// PhoneLocationInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="phone_location_info")]
	public class PhoneLocationInfoModel : BaseModel
	{
		private string _phoneCode = null;
		private int? _chinaId = null;
		private string _province = null;
		private string _city = null;
		private string _regionCode = null;
		private string _zipCode = null;
		private string _brand = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 号段 (主键) 
		/// </summary>
		[TableMapping(FieldName="phone_code",PrimaryKey=true)]
		public string PhoneCode
		{
			get { return _phoneCode; }
			set { _phoneCode = value; }
		}

		/// <summary>
		///  
		/// </summary>
		[TableMapping(FieldName="china_id")]
		public int? ChinaId
		{
			get { return _chinaId; }
			set { _chinaId = value; }
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
		/// 区号 
		/// </summary>
		[TableMapping(FieldName="region_code")]
		public string RegionCode
		{
			get { return _regionCode; }
			set { _regionCode = value; }
		}

		/// <summary>
		/// 邮编 
		/// </summary>
		[TableMapping(FieldName="zip_code")]
		public string ZipCode
		{
			get { return _zipCode; }
			set { _zipCode = value; }
		}

		/// <summary>
		/// 品牌 
		/// </summary>
		[TableMapping(FieldName="brand")]
		public string Brand
		{
			get { return _brand; }
			set { _brand = value; }
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

