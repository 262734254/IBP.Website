/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-13
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
	/// BankcardTypeInfo实体类
	/// </summary>
	[Serializable]
	[TableMapping(TableName="bankcard_type_info")]
	public class BankcardTypeInfoModel : BaseModel
	{
		private string _bankcardTypeId = null;
		private string _bankEnumValue = null;
		private string _cardBinCode = null;
		private string _cardType = null;
		private string _cardNature = null;
		private string _cardBrand = null;
		private string _currencyType = null;
		private string _cardLevel = null;
		private string _bankcardEnumValue = null;
		private DateTime? _createdOn = null;
		private string _createdBy = null;
		private DateTime? _modifiedOn = null;
		private string _modifiedBy = null;
		private int? _statusCode = null;

		/// <summary>
		/// 银行卡类型ID (主键) 
		/// </summary>
		[TableMapping(FieldName="bankcard_type_id",PrimaryKey=true)]
		public string BankcardTypeId
		{
			get { return _bankcardTypeId; }
			set { _bankcardTypeId = value; }
		}

		/// <summary>
		/// 开卡行枚举值 
		/// </summary>
		[TableMapping(FieldName="bank_enum_value")]
		public string BankEnumValue
		{
			get { return _bankEnumValue; }
			set { _bankEnumValue = value; }
		}

		/// <summary>
		/// BIN号 
		/// </summary>
		[TableMapping(FieldName="card_bin_code")]
		public string CardBinCode
		{
			get { return _cardBinCode; }
			set { _cardBinCode = value; }
		}

		/// <summary>
		/// 卡类型 
		/// </summary>
		[TableMapping(FieldName="card_type")]
		public string CardType
		{
			get { return _cardType; }
			set { _cardType = value; }
		}

		/// <summary>
		/// 卡性质 
		/// </summary>
		[TableMapping(FieldName="card_nature")]
		public string CardNature
		{
			get { return _cardNature; }
			set { _cardNature = value; }
		}

		/// <summary>
		/// 卡品牌 
		/// </summary>
		[TableMapping(FieldName="card_brand")]
		public string CardBrand
		{
			get { return _cardBrand; }
			set { _cardBrand = value; }
		}

		/// <summary>
		/// 币种 
		/// </summary>
		[TableMapping(FieldName="currency_type")]
		public string CurrencyType
		{
			get { return _currencyType; }
			set { _currencyType = value; }
		}

		/// <summary>
		/// 卡级别 
		/// </summary>
		[TableMapping(FieldName="card_level")]
		public string CardLevel
		{
			get { return _cardLevel; }
			set { _cardLevel = value; }
		}

		/// <summary>
		/// 卡功能鉴别枚举值 
		/// </summary>
		[TableMapping(FieldName="bankcard_enum_value")]
		public string BankcardEnumValue
		{
			get { return _bankcardEnumValue; }
			set { _bankcardEnumValue = value; }
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

