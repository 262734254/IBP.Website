/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-5-10
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
    /// ProductCategoryInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "product_category_info")]
    public class ProductCategoryInfoModel : BaseModel
    {
        private string _productCategoryId = null;
        private string _categoryName = null;
        private string _categoryCode = null;
        private string _saleCity = null;
        private decimal? _itemPrice = null;
        private string _groupName = null;
        private string _tableName = null;
        private string _description = null;
        private string _salesGuide = null;
        private string _remark = null;
        private int? _sortOrder = null;
        private int? _status = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "product_category_id", PrimaryKey = true)]
        public string ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; }
        }

        /// <summary>
        /// 类别名称 
        /// </summary>
        [TableMapping(FieldName = "category_name")]
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "category_code")]
        public string CategoryCode
        {
            get { return _categoryCode; }
            set { _categoryCode = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "sale_city")]
        public string SaleCity
        {
            get { return _saleCity; }
            set { _saleCity = value; }
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
        ///  
        /// </summary>
        [TableMapping(FieldName = "group_name")]
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "table_name")]
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        /// <summary>
        /// 描述信息 
        /// </summary>
        [TableMapping(FieldName = "description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "sales_guide")]
        public string SalesGuide
        {
            get { return _salesGuide; }
            set { _salesGuide = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "remark")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 排序索引 
        /// </summary>
        [TableMapping(FieldName = "sort_order")]
        public int? SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
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
