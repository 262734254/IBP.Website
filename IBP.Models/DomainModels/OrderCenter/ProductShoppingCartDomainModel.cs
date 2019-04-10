using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBP.Common;

namespace IBP.Models
{
    public class ProductShoppingCartDomainModel
    {
        /// <summary>
        /// 订单类型。
        /// </summary>
        public string SalesOrderType { get; set; }


        /// <summary>
        /// 订单来源。
        /// </summary>
        public string SalesOrderSource { get; set; }

        /// <summary>
        /// 预约跟进时间。
        /// </summary>
        public DateTime? FollowTime { get; set; }

        /// <summary>
        /// 预约跟进备注。
        /// </summary>
        public string FollowRemark { get; set; }

        /// <summary>
        /// 客户ID。
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 销售城市。
        /// </summary>
        public string SalesCityId { get; set; }

        /// <summary>
        /// 购物车产品列表。
        /// </summary>
        public Dictionary<string, ShoppingCartItemInfo> ProductList { get; set; }

        /// <summary>
        /// 产品总数。
        /// </summary>
        public int ProductTotal { get; set; }

        /// <summary>
        /// 业务总额。
        /// </summary>
        public decimal PriceTotal { get; set; }

        /// <summary>
        /// 购物车中是否包含电信套餐。
        /// </summary>
        public bool hasCommunicationPackage { get; set; }

        /// <summary>
        /// 购物车中包含电信套餐数目。
        /// </summary>
        public int CommunicationPackageTotal { get; set; }

        /// <summary>
        /// 购物车中是否包含手机号码。
        /// </summary>
        public bool hasPhoneNumber { get; set; }

        /// <summary>
        /// 支付信息。
        /// </summary>
        public OrderPaymentInfo PayInfo { get; set; }

        /// <summary>
        /// 订单配送信息。
        /// </summary>
        public string OrderDeliveryInfoId { get; set; }

        /// <summary>
        /// 订单备注信息。
        /// </summary>
        public string OrderRemark { get; set; }

        public int PhoneNumberTotal
        {
            get
            {
                int total = 0;
                if (ProductList != null && ProductList.Count > 0)
                {
                    foreach (ShoppingCartItemInfo item in ProductList.Values)
                    {
                        if (item.IsPhoneNumber)
                        {
                            total++;
                        }
                    }
                }
                return total;
            }
        }

        public int CommuniationPackageTotal
        {
            get
            {
                int total = 0;
                if (ProductList != null && ProductList.Count > 0)
                {
                    foreach (ShoppingCartItemInfo item in ProductList.Values)
                    {
                        if (item.IsCommunicationPackage)
                        {
                            total++;
                        }
                        else
                        {
                            if (item.IsSalesPackage)
                            {
                                if (item.CommuniationPackageInfo != null && item.CommuniationPackageInfo.Count > 0)
                                {
                                    total = total + item.CommuniationPackageInfo.Count;
                                }
                            }
                        }
                    }
                }
                return total;
            }
        }

        public string GetProductListString()
        {
            StringBuilder sb = new StringBuilder();
            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (ShoppingCartItemInfo item in ProductList.Values)
                {
                    if (item.ItemId == "undefined")
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}|{1}|{2},", item.ItemId, item.ItemType,item.Total);
                }
            }

            return sb.ToString();
        }

        public string GetPhoneNumberBindCreditId(string phoneNumberId)
        {
            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (ShoppingCartItemInfo item in ProductList.Values)
                {
                    if (item.CommuniationPackageInfo != null && item.CommuniationPackageInfo.Count > 0)
                    {
                        foreach (CommuniationPackageInfo pack in item.CommuniationPackageInfo.Values)
                        {
                            if (pack.BindedMainPhoneNumberId == phoneNumberId)
                            {
                                return pack.PhoneOwnerInfoId;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public string GetPhoneNumberBindCollectionId(string phoneNumberId)
        {
            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (ShoppingCartItemInfo item in ProductList.Values)
                {
                    if (item.CommuniationPackageInfo != null && item.CommuniationPackageInfo.Count > 0)
                    {
                        foreach (CommuniationPackageInfo pack in item.CommuniationPackageInfo.Values)
                        {
                            if (pack.BindedMainPhoneNumberId == phoneNumberId)
                            {
                                if (pack.IsCollections)
                                {
                                    return pack.CollectionInfoId;
                                }
                                else
                                {
                                    return null;
                                }                               
                            }
                        }
                    }
                }
            }

            return null;
        }


        public string GetCommuniationPackageString()
        {
            StringBuilder sb = new StringBuilder();

            if (ProductList != null && ProductList.Count > 0)
            {
                foreach (ShoppingCartItemInfo item in ProductList.Values)
                {
                    if (item.CommuniationPackageInfo != null && item.CommuniationPackageInfo.Count > 0)
                    {
                        foreach (CommuniationPackageInfo pack in item.CommuniationPackageInfo.Values)
                        {
                            if (pack.CollectionInfoId == null)
                            {
                                sb.AppendFormat("{0}|{1}|{2}|none|0,", pack.PackageInfoId, pack.BindedMainPhoneNumberId, pack.PhoneOwnerInfoId);
                            }
                            else
                            {
                                sb.AppendFormat("{0}|{1}|{2}|{3}|1,", pack.PackageInfoId, pack.BindedMainPhoneNumberId, pack.PhoneOwnerInfoId,  pack.CollectionInfoId);
                            }
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 订单购物车中单项项目信息。
    /// </summary>
    /// <remarks>
    /// 因为订购的产品有可能是品类信息（如手机，内存卡，套餐等，是一个品类，价格属性放在品类信息中），
    /// 也有可能是产品单品信息（价格属性是放在某品类的每个产品中，如手机号码，每个号码都可能具备不同的价格）
    /// 通过ItemType去区分单项信息是品类还是单品，如果是品类，那么就从ProductCategoryInfoModel中获取信息，
    /// 如果是产品单品，则从ProductInfoDomainModel获取信息。
    /// </remarks>
    public class ShoppingCartItemInfo
    {
        /// <summary>
        /// 产品类型
        /// </summary>
        /// <remarks>
        /// ProductItem: 产品单品
        /// ProductCategory： 产品品类
        /// SalesPackage:产品包
        /// </remarks>
        public string ItemType { get; set; }

        /// <summary>
        /// 项目产品ID
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 产品数量。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 是否电信套餐。
        /// </summary>
        public bool IsCommunicationPackage { get; set; }

        /// <summary>
        /// 是否营销产品包。
        /// </summary>
        public bool IsSalesPackage { get; set; }

        /// <summary>
        /// 是否手机号码。
        /// </summary>
        public bool IsPhoneNumber { get; set; }

        /// <summary>
        /// 产品单品信息。
        /// </summary>
        public ProductInfoDomainModel ProductInfo { get; set; }

        /// <summary>
        /// 产品品类信息。
        /// </summary>
        public ProductCategoryInfoModel ProductCategory { get; set; }

        /// <summary>
        /// 产品包类型。
        /// </summary>
        public SalePackageDomainModel SalesPackageInfo { get; set; }

        /// <summary>
        /// 产品包内包含的电话合约套餐列表。
        /// </summary>
        public Dictionary<string, CommuniationPackageInfo> CommuniationPackageInfo { get; set; }
    }

    public class CommuniationPackageInfo
    {
        /// <summary>
        /// 开户城市。
        /// </summary>
        public string OpenningCityId { get; set; }

        /// <summary>
        /// 绑定的主号码。
        /// </summary>
        public string BindedMainPhoneNumberId { get; set; }

        /// <summary>
        /// 绑定的副号码。
        /// </summary>
        public string BindedSubsidiaryPhoneNumberId { get; set; }

        /// <summary>
        /// 机主信息。
        /// </summary>
        public string PhoneOwnerInfoId { get; set; }

        /// <summary>
        /// 是否托收。
        /// </summary>
        public bool IsCollections { get; set; }

        /// <summary>
        /// 托收信息。
        /// </summary>
        public string CollectionInfoId { get; set; }

        /// <summary>
        /// 电信套餐信息。
        /// </summary>
        public string PackageInfoId { get; set; }
    }


    /// <summary>
    /// 支付信息。
    /// </summary>
    public class OrderPaymentInfo
    {
        /// <summary>
        /// 支付方式。
        /// </summary>
        public  OrderPayType PayType {get;set;}

        /// <summary>
        /// 支付账号信息。
        /// </summary>
        public CustomerCreditcardInfoModel PayAccountInfo {get;set;}
    }
}
