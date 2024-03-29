/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-6-4
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
    /// SalesPackageInfo实体类
    /// </summary>
    [Serializable]
    [TableMapping(TableName = "sales_package_info")]
    public class SalesPackageInfoModel : BaseModel
    {
        private string _salesPackageId = null;
        private string _packageName = null;
        private string _salesCityId = null;
        private string _salesCityName = null;
        private int? _status = null;
        private string _location = null;
        private DateTime? _beginTime = null;
        private DateTime? _endTime = null;
        private decimal? _priceTotal = null;
        private decimal? _salePrice = null;
        private decimal? _storedPrice = null;
        private int? _returnMonths = null;
        private decimal? _monthReturnPrice = null;
        private decimal? _monthKeepPrice = null;
        private int? _stages = null;
        private decimal? _stagePrice = null;
        private string _productCategoryList = null;
        private string _remark = null;
        private string _description = null;
        private string _salesGuide = null;
        private DateTime? _createdOn = null;
        private string _createdBy = null;
        private DateTime? _modifiedOn = null;
        private string _modifiedBy = null;
        private int? _statusCode = null;

        /// <summary>
        /// 主键ID (主键) 
        /// </summary>
        [TableMapping(FieldName = "sales_package_id", PrimaryKey = true)]
        public string SalesPackageId
        {
            get { return _salesPackageId; }
            set { _salesPackageId = value; }
        }

        /// <summary>
        /// 销售项目名称 
        /// </summary>
        [TableMapping(FieldName = "package_name")]
        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        /// <summary>
        /// 销售城市ID 
        /// </summary>
        [TableMapping(FieldName = "sales_city_id")]
        public string SalesCityId
        {
            get { return _salesCityId; }
            set { _salesCityId = value; }
        }

        /// <summary>
        /// 销售城市名称 
        /// </summary>
        [TableMapping(FieldName = "sales_city_name")]
        public string SalesCityName
        {
            get { return _salesCityName; }
            set { _salesCityName = value; }
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
        /// 产品定位 
        /// </summary>
        [TableMapping(FieldName = "location")]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
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
        /// 业务总额 
        /// </summary>
        [TableMapping(FieldName = "price_total")]
        public decimal? PriceTotal
        {
            get { return _priceTotal; }
            set { _priceTotal = value; }
        }

        /// <summary>
        /// 购机金额 
        /// </summary>
        [TableMapping(FieldName = "sale_price")]
        public decimal? SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = value; }
        }

        /// <summary>
        /// 预存话费 
        /// </summary>
        [TableMapping(FieldName = "stored_price")]
        public decimal? StoredPrice
        {
            get { return _storedPrice; }
            set { _storedPrice = value; }
        }

        /// <summary>
        /// 返还月数 
        /// </summary>
        [TableMapping(FieldName = "return_months")]
        public int? ReturnMonths
        {
            get { return _returnMonths; }
            set { _returnMonths = value; }
        }

        /// <summary>
        /// 每月返还 
        /// </summary>
        [TableMapping(FieldName = "month_return_price")]
        public decimal? MonthReturnPrice
        {
            get { return _monthReturnPrice; }
            set { _monthReturnPrice = value; }
        }

        /// <summary>
        /// 每月补存 
        /// </summary>
        [TableMapping(FieldName = "month_keep_price")]
        public decimal? MonthKeepPrice
        {
            get { return _monthKeepPrice; }
            set { _monthKeepPrice = value; }
        }

        /// <summary>
        /// 分期数 
        /// </summary>
        [TableMapping(FieldName = "stages")]
        public int? Stages
        {
            get { return _stages; }
            set { _stages = value; }
        }

        /// <summary>
        /// 每期金额 
        /// </summary>
        [TableMapping(FieldName = "stage_price")]
        public decimal? StagePrice
        {
            get { return _stagePrice; }
            set { _stagePrice = value; }
        }

        /// <summary>
        ///  
        /// </summary>
        [TableMapping(FieldName = "product_category_list")]
        public string ProductCategoryList
        {
            get { return _productCategoryList; }
            set { _productCategoryList = value; }
        }

        /// <summary>
        /// 备注 
        /// </summary>
        [TableMapping(FieldName = "remark")]
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        ///  
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
