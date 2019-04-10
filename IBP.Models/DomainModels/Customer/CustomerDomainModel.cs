using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBP.Models
{
    /// <summary>
    /// 客户信息领域模型。
    /// </summary>
    public class CustomerDomainModel
    {
        /// <summary>
        /// 客户基本信息。
        /// </summary>
        public CustomerBasicInfoModel BasicInfo { get; set; }

        /// <summary>
        /// 客户联系号码列表。
        /// </summary>
        public Dictionary<string, CustomerPhoneInfoModel> PhoneNumberList { get; set; }

        /// <summary>
        /// 客户备注信息列表。
        /// </summary>
        public Dictionary<string, CustomerMemoInfoModel> MemoList { get; set; }

        /// <summary>
        /// 客户联系记录信息列表。
        /// </summary>
        public Dictionary<string, CustomerContactInfoModel> ContactList { get; set; }

        /// <summary>
        /// 客户工单记录列表。
        /// </summary>
        public List<string> WorkorderList { get; set; }

        /// <summary>
        /// 客户销售订单记录列表。
        /// </summary>
        public List<string> SalesOrderList { get; set; }

        /// <summary>
        /// 信用卡信息列表。
        /// </summary>
        public Dictionary<string, CustomerCreditcardInfoModel> CreditCardList { get; set; }

        /// <summary>
        /// 配送地址信息列表。
        /// </summary>
        public Dictionary<string, CustomerDeliveryInfoModel> DeliveryList { get; set; }

        /// <summary>
        /// 客户属性信息列表。
        /// </summary>
        /// <remarks>
        /// Dictionary<属性分组名称, Dictionary<属性名称,属性值>>
        /// </remarks>
        public Dictionary<string, Dictionary<string, string>> AttributeList { get; set; }
    }
}
