/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-15
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
    /// SalesorderProductInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "salesorder_product_info")]
    public class SalesorderProductInfoModel : BaseModel
    {
        private string _salesorderProductitemId = null;
        private string _salesorderId = null;
        private int? _productType = null;
        private string _productId = null;
        private string _productCode = null;
        private string _productName = null;
        private decimal? _itemPrice = null;
        private int? _productCount = null;
        private decimal? _priceTotal = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "salesorder_productitem_id", PrimaryKey = true)]
        public string SalesorderProductitemId
        {
            get { return _salesorderProductitemId; }
            set { _salesorderProductitemId = value; }
        }

        /// <summary>
        /// 销售订单ID 
        /// </summary>
        [TableMapping(FieldName = "salesorder_id")]
        public string SalesorderId
        {
            get { return _salesorderId; }
            set { _salesorderId = value; }
        }

        /// <summary>
        /// 产品类型：0产品包，1产品类型，2产品单品 
        /// </summary>
        [TableMapping(FieldName = "product_type")]
        public int? ProductType
        {
            get { return _productType; }
            set { _productType = value; }
        }

        /// <summary>
        /// 产品ID 
        /// </summary>
        [TableMapping(FieldName = "product_id")]
        public string ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        /// <summary>
        /// 产品代码 
        /// </summary>
        [TableMapping(FieldName = "product_code")]
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }

        /// <summary>
        /// 产品名称 
        /// </summary>
        [TableMapping(FieldName = "product_name")]
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        /// <summary>
        /// 价格 
        /// </summary>
        [TableMapping(FieldName = "item_price")]
        public decimal? ItemPrice
        {
            get { return _itemPrice; }
            set { _itemPrice = value; }
        }

        /// <summary>
        /// 订购数量 
        /// </summary>
        [TableMapping(FieldName = "product_count")]
        public int? ProductCount
        {
            get { return _productCount; }
            set { _productCount = value; }
        }

        /// <summary>
        /// 总额 
        /// </summary>
        [TableMapping(FieldName = "price_total")]
        public decimal? PriceTotal
        {
            get { return _priceTotal; }
            set { _priceTotal = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "created_on")]
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "created_by")]
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_on")]
        public DateTime? ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "modified_by")]
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "status_code")]
        public int? StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

    }
}
