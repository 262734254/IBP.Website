/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-1-27
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
	/// ChinaInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="china_info")]
	public class ChinaInfoModel : BaseModel
	{
		private int? _id = null;
		private int? _parentId = null;
		private int? _provinceAreaId = null;
		private string _provinceAreaName = null;
		private int? _provinceId = null;
		private string _provinceName = null;
		private int? _cityId = null;
		private string _cityName = null;
		private int? _countyId = null;
		private string _countyName = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 标识ID 
		/// </summary>
		[TableMapping(FieldName="id")]
		public int? Id
		{
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		/// 父ID 
		/// </summary>
		[TableMapping(FieldName="parent_id")]
		public int? ParentId
		{
			get { return _parentId; }
			set { _parentId = value; }
		}

		/// <summary>
		/// q区域ID 
		/// </summary>
		[TableMapping(FieldName="province_area_id")]
		public int? ProvinceAreaId
		{
			get { return _provinceAreaId; }
			set { _provinceAreaId = value; }
		}

		/// <summary>
		/// 区域名称 
		/// </summary>
		[TableMapping(FieldName="province_area_name")]
		public string ProvinceAreaName
		{
			get { return _provinceAreaName; }
			set { _provinceAreaName = value; }
		}

		/// <summary>
		/// 省份ID 
		/// </summary>
		[TableMapping(FieldName="province_id")]
		public int? ProvinceId
		{
			get { return _provinceId; }
			set { _provinceId = value; }
		}

		/// <summary>
		/// 省份名称 
		/// </summary>
		[TableMapping(FieldName="province_name")]
		public string ProvinceName
		{
			get { return _provinceName; }
			set { _provinceName = value; }
		}

		/// <summary>
		/// 城市ID 
		/// </summary>
		[TableMapping(FieldName="city_id")]
		public int? CityId
		{
			get { return _cityId; }
			set { _cityId = value; }
		}

		/// <summary>
		/// 城市名称 
		/// </summary>
		[TableMapping(FieldName="city_name")]
		public string CityName
		{
			get { return _cityName; }
			set { _cityName = value; }
		}

		/// <summary>
		/// 区县ID 
		/// </summary>
		[TableMapping(FieldName="county_id")]
		public int? CountyId
		{
			get { return _countyId; }
			set { _countyId = value; }
		}

		/// <summary>
		/// 区县名称 
		/// </summary>
		[TableMapping(FieldName="county_name")]
		public string CountyName
		{
			get { return _countyName; }
			set { _countyName = value; }
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

