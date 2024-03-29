/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-17
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
    /// ProductSalesGroupInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "product_sales_group_info")]
    public class ProductSalesGroupInfoModel : BaseModel
    {
        private string _saleGroupId = null;
        private string _saleCityId = null;
        private string _salePackageId = null;
        private string _saleGroupName = null;
        private string _productCategoryId = null;
        private DateTime? _beginTime = null;
        private DateTime? _endTime = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键 (主键) 
        /// </summary>
        [TableMapping(FieldName = "sale_group_id", PrimaryKey = true)]
        public string SaleGroupId
        {
            get { return _saleGroupId; }
            set { _saleGroupId = value; }
        }

        /// <summary>
        /// 销售城市ID 
        /// </summary>
        [TableMapping(FieldName = "sale_city_id")]
        public string SaleCityId
        {
            get { return _saleCityId; }
            set { _saleCityId = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "sale_package_id")]
        public string SalePackageId
        {
            get { return _salePackageId; }
            set { _salePackageId = value; }
        }

        /// <summary>
        /// 产品销售组名 
        /// </summary>
        [TableMapping(FieldName = "sale_group_name")]
        public string SaleGroupName
        {
            get { return _saleGroupName; }
            set { _saleGroupName = value; }
        }

        /// <summary>
        /// 产品类型ID 
        /// </summary>
        [TableMapping(FieldName = "product_category_id")]
        public string ProductCategoryId
        {
            get { return _productCategoryId; }
            set { _productCategoryId = value; }
        }

        /// <summary>
        /// 有效起始时间 
        /// </summary>
        [TableMapping(FieldName = "begin_time")]
        public DateTime? BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        /// <summary>
        /// 有效截止时间 
        /// </summary>
        [TableMapping(FieldName = "end_time")]
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
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
