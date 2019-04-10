using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    public class ProductCategoryDomainModel
    {
        /// <summary>
        /// 产品类别基础信息。
        /// </summary>
        public ProductCategoryInfoModel BasicInfo { get; set; }

        /// <summary>
        /// 所属产品组信息。
        /// </summary>
        public ProductCategoryGroupInfoModel GroupInfo { get; set; }

        /// <summary>
        /// 本产品类型属性列表。
        /// </summary>
        public Dictionary<string, ProductCategoryAttributesModel> AttributeList { get; set; }

        /// <summary>
        /// 本产品类型销售状态列表。
        /// </summary>
        public Dictionary<string, ProductCategorySalesStatusModel> SalestatusList { get; set; }

        /// <summary>
        /// 获取指定属性默认值。
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttributeDefaultValue(string attributeName)
        {
            if (AttributeList == null)
                return "";

            foreach (ProductCategoryAttributesModel item in AttributeList.Values)
            {
                if (item.AttributeName == attributeName)
                {
                    return item.DefaultValue;
                } 
            }

            return "";
        }

        public string GetSalestatusIdByName(string statusName)
        {
            if (SalestatusList == null || string.IsNullOrEmpty(statusName))
                return null;

            foreach (ProductCategorySalesStatusModel status in SalestatusList.Values)
            {
                if (status.SalestatusName == statusName)
                    return status.SalesStatusId;
            }

            return null;
        }

        public ProductCategorySalesStatusModel GetSalestatusInfoModelByName(string statusName)
        {
            if (SalestatusList == null || string.IsNullOrEmpty(statusName))
                return null;

            foreach (ProductCategorySalesStatusModel status in SalestatusList.Values)
            {
                if (status.SalestatusName == statusName)
                    return status;
            }

            return null;
        }
    }
}
