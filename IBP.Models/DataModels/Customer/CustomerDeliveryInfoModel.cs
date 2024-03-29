/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-17
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
	/// CustomerDeliveryInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="customer_delivery_info")]
	public class CustomerDeliveryInfoModel : BaseModel
	{
		private string _deliveryId = null;
		private string _customerId = null;
		private int? _deliveryType = null;
		private int? _deliveryRegionId = null;
		private string _deliveryAddress = null;
		private string _postCode = null;
		private string _consignee = null;
		private string _consigneePhone = null;
		private int? _needBills = null;
		private string _billTitle = null;
		private int? _status = null;
		private int? _sortOrder = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 主键ID (主键) 
		/// </summary>
		[TableMapping(FieldName="delivery_id",PrimaryKey=true)]
		public string DeliveryId
		{
			get { return _deliveryId; }
			set { _deliveryId = value; }
		}

		/// <summary>
		/// 客户ID 
		/// </summary>
		[TableMapping(FieldName="customer_id")]
		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		/// <summary>
		/// 配送属性，0家里，1公司，2其他 
		/// </summary>
		[TableMapping(FieldName="delivery_type")]
		public int? DeliveryType
		{
			get { return _deliveryType; }
			set { _deliveryType = value; }
		}

		/// <summary>
		/// 配送地区ID 
		/// </summary>
		[TableMapping(FieldName="delivery_region_id")]
		public int? DeliveryRegionId
		{
			get { return _deliveryRegionId; }
			set { _deliveryRegionId = value; }
		}

		/// <summary>
		/// 配送地址 
		/// </summary>
		[TableMapping(FieldName="delivery_address")]
		public string DeliveryAddress
		{
			get { return _deliveryAddress; }
			set { _deliveryAddress = value; }
		}

		/// <summary>
		/// 邮编 
		/// </summary>
		[TableMapping(FieldName="post_code")]
		public string PostCode
		{
			get { return _postCode; }
			set { _postCode = value; }
		}

		/// <summary>
		/// 收货人 
		/// </summary>
		[TableMapping(FieldName="consignee")]
		public string Consignee
		{
			get { return _consignee; }
			set { _consignee = value; }
		}

		/// <summary>
		/// 收货电话 
		/// </summary>
		[TableMapping(FieldName="consignee_phone")]
		public string ConsigneePhone
		{
			get { return _consigneePhone; }
			set { _consigneePhone = value; }
		}

		/// <summary>
		/// 发票 
		/// </summary>
		[TableMapping(FieldName="need_bills")]
		public int? NeedBills
		{
			get { return _needBills; }
			set { _needBills = value; }
		}

		/// <summary>
		/// 发票Title 
		/// </summary>
		[TableMapping(FieldName="bill_title")]
		public string BillTitle
		{
			get { return _billTitle; }
			set { _billTitle = value; }
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
		/// 排序索引 
		/// </summary>
		[TableMapping(FieldName="sort_order")]
		public int? SortOrder
		{
			get { return _sortOrder; }
			set { _sortOrder = value; }
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

