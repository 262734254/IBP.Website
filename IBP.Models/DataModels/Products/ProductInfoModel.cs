/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-5-17
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
    /// ProductInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "product_info")]
    public class ProductInfoModel : BaseModel
    {
        private string _productId = null;
        private string _categoryId = null;
        private string _productCode = null;
        private string _productName = null;
        private decimal? _itemPrice = null;
        private string _salesStatus = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "product_id", PrimaryKey = true)]
        public string ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        /// <summary>
        /// 类别ID 
        /// </summary>
        [TableMapping(FieldName = "category_id")]
        public string CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        /// <summary>
        /// 产品编号 
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
        ///  
        /// </summary>
        [TableMapping(FieldName = "item_price")]
        public decimal? ItemPrice
        {
            get { return _itemPrice; }
            set { _itemPrice = value; }
        }

        /// <summary>
        /// 销售状态 
        /// </summary>
        [TableMapping(FieldName = "sales_status")]
        public string SalesStatus
        {
            get { return _salesStatus; }
            set { _salesStatus = value; }
        }

        /// <summary>
        /// 状态 
        /// </summary>
        [TableMapping(FieldName = "status")]
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
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
