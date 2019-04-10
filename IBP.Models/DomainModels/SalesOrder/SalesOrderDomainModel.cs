using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBP.Common;

namespace IBP.Models
{
    public class SalesOrderDomainModel
    {
        #region 订单数量状态名称
 
        #endregion

        /// <summary>
        /// 订单ID.
        /// </summary>
        public string SalesorderId
        {
            get
            {
                if (BasicInfo != null)
                {
                    return BasicInfo.SalesorderId;
                }

                return null;
            }
        }

        /// <summary>
        /// 订单编号。
        /// </summary>
        public string SalesorderCode
        {
            get
            {
                if (BasicInfo != null)
                {
                    return BasicInfo.SalesorderCode;
                }

                return null;
            }
        }

        /// <summary>
        /// 订单销售状态。
        /// </summary>
        public SalesOrderStatus SalesorderStatus
        {
            get
            {
                if (BasicInfo != null)
                {
                    return (SalesOrderStatus)Convert.ToInt32(BasicInfo.NowOrderStatusId);
                }

                return SalesOrderStatus.Exception;
            }
        }

        /// <summary>
        /// 订单支付方式。
        /// </summary>
        public OrderPayType OrderPayType
        {
            get
            {
                if (BasicInfo != null)
                {
                    return (OrderPayType)Convert.ToInt32(BasicInfo.PayType);
                }

                return OrderPayType.NoCardPosFullPayment_ICBC;
            }
        }

        /// <summary>
        /// 支付类型名称。
        /// </summary>
        public string OrderPaytypeName
        {
            get
            {
                switch (OrderPayType)
                {
                    case OrderPayType.NoCardPosFullPayment_CCB:
                        return "无卡POS全额(建行)";
                    case OrderPayType.NoCardPosFullPayment_ICBC:
                        return "无卡POS全额(工行)";
                    case OrderPayType.NoCardPosFullPayment_ABCHINA:
                        return "无卡POS全额(农行)";
                    case OrderPayType.NoCardPosFullPayment_BOC:
                        return "无卡POS全额(中行)";
                    case OrderPayType.NoCardPosInstallments_CCB:
                        return "无卡POS分期(建行)";
                    case OrderPayType.NoCardPosInstallments_ICBC:
                        return "无卡POS分期(工行)";
                    case OrderPayType.NoCardPosInstallments_ABCHINA:
                        return "无卡POS分期(农行)";
                    case OrderPayType.NoCardPosInstallments_BOC:
                        return "无卡POS分期(中行)";
                    case OrderPayType.CardPayWhenReceive_CCB:
                        return "货到刷卡全额(建行)";
                    case OrderPayType.CardPayWhenReceive_ICBC:
                        return "货到刷卡全额(工行)";
                    case OrderPayType.CardPayWhenReceive_ABCHINA:
                        return "货到刷卡全额(农行)";
                    case OrderPayType.CardPayWhenReceive_BOC:
                        return "货到刷卡全额(中行)";
                    case OrderPayType.CardPayWhenReceiveInstallments_CCB:
                        return "货到刷卡分期(建行)";
                    case OrderPayType.CardPayWhenReceiveInstallments_ICBC:
                        return "货到刷卡分期(工行)";
                    case OrderPayType.CardPayWhenReceiveInstallments_ABCHINA:
                        return "货到刷卡分期(农行)";
                    case OrderPayType.CardPayWhenReceiveInstallments_BOC:
                        return "货到刷卡分期(中行)";
                    case OrderPayType.CashPayWhenReceive:
                        return "货到付现";
                    default:
                        return "无卡POS全额(工行)";
                }
            }
        }

        /// <summary>
        /// 订单状态名称
        /// </summary>
        public string OrderStatusName
        {
            get
            {
                switch (SalesorderStatus)
                {
                    case SalesOrderStatus.WaitFollow:
                        return "待跟进";
                    case SalesOrderStatus.WaitCheck:
                        return "待质检";
                    case SalesOrderStatus.WaitCharge:
                        return "待扣款";
                    case SalesOrderStatus.WaitApproval:
                        return "待审批";
                    case SalesOrderStatus.WaitOpening:
                        return "待开户";
                    case SalesOrderStatus.WaitStocking:
                        return "待备货";
                    case SalesOrderStatus.WaitDelivery:
                        return "待发货";
                    case SalesOrderStatus.WaitSign:
                        return "待签收";
                    case SalesOrderStatus.WaitRecover:
                        return "待回收";
                    case SalesOrderStatus.WaitRefund:
                        return "待退款";
                    case SalesOrderStatus.WaitReturns:
                        return "待退货";
                    case SalesOrderStatus.Exception:
                        return "异常";
                    case SalesOrderStatus.Cancel:
                        return "已撤消";
                    case SalesOrderStatus.Successed:
                        return "成功";
                    case SalesOrderStatus.WaitCancelOpening:
                        return "待销户";

                    default:
                        return "异常";
                }
            }
        }

        /// <summary>
        /// 订单异常类型名称。
        /// </summary>
        public string ExceptionName
        {
            get
            {
                if (BasicInfo == null)
                {
                    return "";
                }
                if (BasicInfo.IsException == 1)
                {
                    return "";
                }

                switch (SalesorderStatus)
                {
                    case SalesOrderStatus.WaitCheck:
                        return "质检异常";
                    case SalesOrderStatus.WaitCharge:
                        return "扣款异常";
                    case SalesOrderStatus.WaitApproval:
                        return "审批异常";
                    case SalesOrderStatus.WaitOpening:
                        return "开户异常";
                    case SalesOrderStatus.WaitStocking:
                        return "备货异常";
                    case SalesOrderStatus.WaitDelivery:
                        return "发货异常";
                    case SalesOrderStatus.WaitSign:
                        return "签收异常";
                    case SalesOrderStatus.WaitRecover:
                        return "回收异常";
                    case SalesOrderStatus.WaitRefund:
                        return "退款异常";
                    case SalesOrderStatus.WaitReturns:
                        return "退货异常";
                    case SalesOrderStatus.WaitCancelOpening:
                        return "销户异常";

                    default:
                        return "未知异常";
                }
            }
        }

        /// <summary>
        /// 订单是否处于异常状态。
        /// </summary>
        public bool IsException
        {
            get
            {
                if (BasicInfo != null)
                {
                    return BasicInfo.IsException == 0;
                }

                return true;
            }
        }

        public string ProductNameList
        {
            get
            {
                if (ProductList == null && ProductList.Count == 0)
                    return "";

                StringBuilder sb = new StringBuilder();
                foreach (SalesorderProductInfoModel product in ProductList.Values)
                {
                    sb.AppendFormat("【{0} × {1}】，\r\n", product.ProductName, product.ProductCount);
                }

                if (sb.Length > 2)
                {
                    sb.Length = sb.Length - 3;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 订单基本信息。
        /// </summary>
        public SalesorderBasicInfoModel BasicInfo { get; set; }

        /// <summary>
        /// 订单套餐列表。
        /// </summary>
        public Dictionary<string,SalesorderCommuniationpackageInfoModel> CommuniationPackageList { get; set; }

        /// <summary>
        /// 订单产品列表。
        /// </summary>
        public Dictionary<string,SalesorderProductInfoModel> ProductList { get; set; }

        /// <summary>
        /// 订单处理记录。
        /// </summary>
        public Dictionary<string, SalesorderProcessLogModel> ProcessLogs { get; set; }

        /// <summary>
        /// 检查是否包含指定的套餐ID。
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public bool CheckSalesorderCommuniationpackageExists(string packageId)
        {
            if (CommuniationPackageList == null || CommuniationPackageList.Count == 0)
                return false;

            foreach (SalesorderCommuniationpackageInfoModel package in CommuniationPackageList.Values)
            {
                if (package.BindCommuniationpackageId == packageId)
                    return true;
            }

            return false;
        }

        public bool CheckSalesorderCommuniationpackageDiffent(SalesorderCommuniationpackageInfoModel sourcePackage)
        {
            if (CommuniationPackageList == null || CommuniationPackageList.Count == 0)
                return false;

            foreach (SalesorderCommuniationpackageInfoModel package in CommuniationPackageList.Values)
            {
                if (package.BindCommuniationpackageId == sourcePackage.BindCommuniationpackageId)
                {
                    if (package.MainPhonenumberId != sourcePackage.MainPhonenumberId
                        || package.SubsidiaryPhonenumberId != sourcePackage.SubsidiaryPhonenumberId
                        || package.OwnerCustomerName != sourcePackage.OwnerCustomerName
                        || package.IdcardNumber != sourcePackage.IdcardNumber
                        || package.IdcardTypeId != sourcePackage.IdcardTypeId
                        || package.CollectionBankId != sourcePackage.CollectionBankId
                        || package.CollectionCardNumber != sourcePackage.CollectionCardNumber
                        || package.CollectionCustomerName != sourcePackage.CollectionCustomerName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

       
    }

    public class SalesorderChangeInfo
    {
        /// <summary>
        /// 订单类型。
        /// </summary>
        public string SalesorderType { get; set; }

        /// <summary>
        /// 需要变更的订单属性键值对。
        /// </summary>
        public Dictionary<string, object> ChangeInfo = null;

        /// <summary>
        /// 变更的状态。
        /// </summary>
        public SalesOrderStatus ChangeToStatus { get; set; }

        /// <summary>
        /// 操作是否成功。
        /// </summary>
        public bool IsSuccessed { get; set; }


        public SalesorderChangeInfo(string orderType, SalesOrderStatus status, bool isSuccessed)
        {
            this.SalesorderType = orderType;

            this.ChangeToStatus = status;
            this.IsSuccessed = isSuccessed;

            ChangeInfo = new Dictionary<string, object>();
            ChangeInfo.Add("opTypeId", "");
            ChangeInfo.Add("opDesc", "");
            ChangeInfo.Add("opType", "");
        }
    }


    public class SalesOrderTotal
    {

        private string waitCharge = null;
        /// <summary>
        /// 代扣款
        /// </summary>
        public string WaitCharge
        {
            get { return waitCharge; }
            set { waitCharge = value; }
        }



        private string waitFollow = null;
        /// <summary>
        ///  待跟进
        /// </summary>
        public string WaitFollow
        {
            get { return waitFollow; }
            set { waitFollow = value; }
        }

        private string waitCheck = null;
        /// <summary>
        /// 待质检
        /// </summary>
        public string WaitCheck
        {
            get { return waitCheck; }
            set { waitCheck = value; }
        }
        private string waitApproval = null;
        /// <summary>
        /// 待审批
        /// </summary>
        public string WaitApproval
        {
            get { return waitApproval; }
            set { waitApproval = value; }
        }
        private string waitOpening = null;
        /// <summary>
        /// 待开户
        /// </summary>
        public string WaitOpening
        {
            get { return waitOpening; }
            set { waitOpening = value; }
        }
        private string waitStocking = null;
        /// <summary>
        /// 待备货
        /// </summary>
        public string WaitStocking
        {
            get { return waitStocking; }
            set { waitStocking = value; }
        }
        private string waitDelivery = null;

        public string WaitDelivery
        {
            get { return waitDelivery; }
            set { waitDelivery = value; }
        }

        private string waitSign = null;
        /// <summary>
        /// 待签收。
        /// </summary>
        public string WaitSign
        {
            get { return waitSign; }
            set { waitSign = value; }
        }


        private string waitRecover = null;
        /// <summary>
        /// 待回收。
        /// </summary>
        public string WaitRecover
        {
            get { return waitRecover; }
            set { waitRecover = value; }
        }

        private string successed = null;
        /// <summary>
        /// 成功。
        /// </summary>
        public string Successed
        {
            get { return successed; }
            set { successed = value; }
        }


        private string exception = null;

        /// <summary>
        /// 异常。
        /// </summary>
        public string Exception
        {
            get { return exception; }
            set { exception = value; }
        }


        private string waitRefund = null;
        /// <summary>
        /// 待退款。
        /// </summary>
        public string WaitRefund
        {
            get { return waitRefund; }
            set { waitRefund = value; }
        }


        private string waitReturns = null;
        /// <summary>
        /// 待退货。
        /// </summary>
        public string WaitReturns
        {
            get { return waitReturns; }
            set { waitReturns = value; }
        }


        private string waitCancelOpening = null;
        /// <summary>
        /// 待销户。
        /// </summary>
        public string WaitCancelOpening
        {
            get { return waitCancelOpening; }
            set { waitCancelOpening = value; }
        }


        private string cancel = null;
        /// <summary>
        /// 撤消。
        /// </summary>
        public string Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }



    } 
}
