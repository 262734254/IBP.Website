/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;

using IBP.Common;
using IBP.Models;
using System.Text;

namespace IBP.Services
{
    public partial class SalesOrderInfoService : BaseService
    {
        
        #region 单键实例

        		// 实例
		private static SalesOrderInfoService _instance = new SalesOrderInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
        private SalesOrderInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
        public static SalesOrderInfoService Instance
		{
			get { return _instance; }
		}

        #endregion


        public string GetSalesorderProductNameList(SalesOrderDomainModel salesOrder)
        {
            if (salesOrder == null)
                return null;

            if (salesOrder.ProductList == null && salesOrder.ProductList.Count == 0)
                return "";

            StringBuilder sb = new StringBuilder();
            foreach (SalesorderProductInfoModel product in salesOrder.ProductList.Values)
            {
                if (product.ProductType == 0)
                {
                    SalePackageDomainModel packageInfo = SalesPackageInfoService.Instance.GetSalePackageDomainModelById(product.ProductId, false);
                    if (packageInfo != null && packageInfo.ProductCategoryList != null)
                    {
                        sb.AppendFormat("产品包【{0}】× {1}，包括如下产品：\r\n", product.ProductName, product.ProductCount);

                        foreach (ProductSalesGroupInfoModel group in packageInfo.ProductCategoryList.Values)
                        {
                            sb.AppendFormat("【{0} × 1】，\r\n", group.SaleGroupName);
                        }
                    }
                }
                else
                {
                    sb.AppendFormat("【{0} × {1}】，\r\n", product.ProductName, product.ProductCount);
                }
            }

            if (sb.Length > 2)
            {
                sb.Length = sb.Length - 3;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取订单状态名称。
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetSalesorderStatusName(SalesOrderStatus status)
        {
            switch (status)
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

        /// <summary>
        /// 获取支付类型中文名称。
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public string GetPayTypeName(int? typeId)
        {
            if (typeId == null)
            {
                return GetPayTypeName(OrderPayType.NoCardPosInstallments_ICBC);
            }
            else
            {
                return GetPayTypeName((OrderPayType)typeId);
            }
        }

        /// <summary>
        /// 获取支付类型中文名称。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetPayTypeName(OrderPayType type)
        {
            switch (type)
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

        public bool UpdateSalesorder(string salesOrderId, SalesorderChangeInfo changeData, out string message)
        {
            bool result = false;
            message = "";

            SalesOrderDomainModel salesorder = GetSalesorderDomainModelById(salesOrderId, false);

            if (salesorder == null)
            {
                message = "操作失败，订单ID不存在";
                return false;
            }

            UserDomainModel opUser = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            if (opUser == null)
            {
                message = "操作失败，用户信息丢失，请重新登录";
                return false;
            }

            if (changeData.ChangeToStatus == SalesOrderStatus.WaitApproval ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitCharge ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitCheck ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitDelivery ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitOpening || 
                changeData.ChangeToStatus == SalesOrderStatus.WaitRecover ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitSign ||
                changeData.ChangeToStatus == SalesOrderStatus.WaitStocking ||
                changeData.IsSuccessed == false)
            {
                if (CheckSalesorder(salesorder, out message) == false)
                {
                    return false;
                }
            }

            SalesorderProcessLogModel logModel = null;

            if (ChangeSalesorderStatus(salesorder, changeData, out logModel, out message) == false)
            {
                return false;
            }

            try
            {
                BeginTransaction();

                if (SalesorderBasicInfoService.Instance.Update(salesorder.BasicInfo) != 1)
                {
                    RollbackTransaction();
                    message = "更新销售订单基本信息失败，请与管理员联系";
                    return false;
                }

                if (salesorder.BasicInfo.NowOrderStatusId == Convert.ToInt32(SalesOrderStatus.Successed).ToString()
                    && salesorder.BasicInfo.RecoverTime == null
                    && salesorder.BasicInfo.RecoverUserId == null)
                {
                    // 创建WelcomeCall工单

                    WorkorderInfoModel workOrderInfo = new WorkorderInfoModel();
                    string customerId = salesorder.BasicInfo.CustomerId;

                    string workorderId = "097F1B35-F8A4-4C75-BFEE-EEDB3FAD458B";
                    WorkOrderTypeDomainModel typeModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workorderId, false);
                    if (typeModel == null)
                    {
                        RollbackTransaction();
                        message = "订单流转至成功状态，无法获取创建WelcomeCall工单所需信息，请与管理员联系";
                        return false;
                    }

                    workOrderInfo.WorkorderType = workorderId;

                    workOrderInfo.Description = string.Format("销售订单【{0}】流转至【成功】，创建WelcomeCall工单。", salesorder.SalesorderCode);
                    CustomDataDomainModel orderLevel = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工单级别", false);

                    workOrderInfo.Level = orderLevel.GetCustomDataValueDomainByDataValue("高").ValueId;
                    workOrderInfo.NowStatusId = typeModel.BeginStatusInfo.WorkorderStatusId;
                    workOrderInfo.NowResultId = typeModel.BeginResultInfo.WorkorderResultId;
                    workOrderInfo.RelCustomerId = customerId;
                    workOrderInfo.RelOrderId = salesorder.SalesorderId;
                    
                    // 默认分配至回访组
                    workOrderInfo.RelUsergroupId = "97AA778D-B5B4-4CC2-893E-0C873101DE98";
                                        
                    if (!WorkorderInfoService.Instance.CreateNewWorkOrder(workOrderInfo, null, out message))
                    {
                        RollbackTransaction();
                        return false;
                    }
                }

                if (SalesorderProcessLogService.Instance.Create(logModel) != 1)
                {
                    RollbackTransaction();
                    message = "操作失败，创建订单处理记录信息失败";
                    return false;
                }

                GetSalesorderDomainModelById(salesorder.SalesorderId, true);
                message = "成功更新销售订单信息";
                CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error(ex.Message, ex);
                message = "操作异常，" + ex.Message;
                result = false;
            }

            return result;
        }

        public bool UpdateSalesorder(string salesOrderId, ProductShoppingCartDomainModel shoppingCartInfo, out string message)
        {
            bool result = false;
            message = "";

            SalesOrderDomainModel oldSalesorder = GetSalesorderDomainModelById(salesOrderId, false);

            if (oldSalesorder == null)
            {
                message = "操作失败，订单ID不存在";
                return false;
            }

            // 从购物车实体构造订单领域模型。
            SalesOrderDomainModel salesorder = GetSalesorderDomainModelFromShoppingCart(shoppingCartInfo, oldSalesorder.SalesorderId, true, out message);

            if (salesorder == null)
            {
                message = "操作失败，订单资料不全，请检查";
                return false;
            }

            UserDomainModel opUser = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            if(opUser == null)
            {
                message = "操作失败，用户信息丢失，请重新登录";
                return false;
            }

            if (CheckSalesorderModifiedPremission(oldSalesorder, salesorder, out message) == false)
            {
                return false;
            }

            #region 复制旧订单ID及编号等数据

            salesorder.BasicInfo.SalesorderId = oldSalesorder.BasicInfo.SalesorderId;
            salesorder.BasicInfo.SalesorderTypeId = oldSalesorder.BasicInfo.SalesorderTypeId;
            salesorder.BasicInfo.SalesorderCode = oldSalesorder.BasicInfo.SalesorderCode;
            salesorder.BasicInfo.CustomerId = oldSalesorder.BasicInfo.CustomerId;
            salesorder.BasicInfo.CustomerName = oldSalesorder.BasicInfo.CustomerName;
            salesorder.BasicInfo.RelWorkorderId = oldSalesorder.BasicInfo.RelWorkorderId;
            salesorder.BasicInfo.IsDelayCheck = oldSalesorder.BasicInfo.IsDelayCheck;
            salesorder.BasicInfo.IsException = oldSalesorder.BasicInfo.IsException;

            salesorder.BasicInfo.FollowTime = oldSalesorder.BasicInfo.FollowTime;
            salesorder.BasicInfo.FollowRemark = oldSalesorder.BasicInfo.FollowRemark;
            

            salesorder.BasicInfo.ApprovalTime = oldSalesorder.BasicInfo.ApprovalTime;
            salesorder.BasicInfo.ApprovalUserId = oldSalesorder.BasicInfo.ApprovalUserId;
            salesorder.BasicInfo.ApprovalRemark = oldSalesorder.BasicInfo.ApprovalRemark;

            salesorder.BasicInfo.CancelTime = oldSalesorder.BasicInfo.CancelTime;
            salesorder.BasicInfo.CancelUserId = oldSalesorder.BasicInfo.CancelUserId;
            salesorder.BasicInfo.CancelRemark = oldSalesorder.BasicInfo.CancelRemark;

            salesorder.BasicInfo.CancelOpeningRemark = oldSalesorder.BasicInfo.CancelOpeningRemark;
            salesorder.BasicInfo.CancelOpeningTime = oldSalesorder.BasicInfo.CancelOpeningTime;
            salesorder.BasicInfo.CancelOpeningUserId = oldSalesorder.BasicInfo.CancelOpeningUserId;
            
            salesorder.BasicInfo.ChargeTime = oldSalesorder.BasicInfo.ChargeTime;
            salesorder.BasicInfo.ChargeUserId = oldSalesorder.BasicInfo.ChargeUserId;
            salesorder.BasicInfo.ChargeRemark = oldSalesorder.BasicInfo.ChargeRemark;
            salesorder.BasicInfo.ChargeBillCode = oldSalesorder.BasicInfo.ChargeBillCode;
            
            salesorder.BasicInfo.CheckedTime = oldSalesorder.BasicInfo.CheckedTime;
            salesorder.BasicInfo.CheckedUserId = oldSalesorder.BasicInfo.CheckedUserId;
            salesorder.BasicInfo.CheckedRemark = oldSalesorder.BasicInfo.CheckedRemark;

            salesorder.BasicInfo.CreatedBy = oldSalesorder.BasicInfo.CreatedBy;
            salesorder.BasicInfo.CreatedOn = oldSalesorder.BasicInfo.CreatedOn;

            salesorder.BasicInfo.DeliveryOrderCode = oldSalesorder.BasicInfo.DeliveryOrderCode;
            salesorder.BasicInfo.DeliveryCompanyId = oldSalesorder.BasicInfo.DeliveryCompanyId;
            salesorder.BasicInfo.DeliveryTime = oldSalesorder.BasicInfo.DeliveryTime;
            salesorder.BasicInfo.DeliveryUserId = oldSalesorder.BasicInfo.DeliveryUserId;
            salesorder.BasicInfo.DeliveryRemark = oldSalesorder.BasicInfo.DeliveryRemark;

            salesorder.BasicInfo.ExceptionDesc = oldSalesorder.BasicInfo.ExceptionDesc;
            salesorder.BasicInfo.ExceptionReason = oldSalesorder.BasicInfo.ExceptionReason;
            salesorder.BasicInfo.ExceptionTime = oldSalesorder.BasicInfo.ExceptionTime;
            salesorder.BasicInfo.ExceptionUserId = oldSalesorder.BasicInfo.ExceptionUserId;

            salesorder.BasicInfo.NowOrderStatusId = oldSalesorder.BasicInfo.NowOrderStatusId;
            salesorder.BasicInfo.NowOrderStatusName = oldSalesorder.BasicInfo.NowOrderStatusName;
            salesorder.BasicInfo.NowStatusDescription = oldSalesorder.BasicInfo.NowStatusDescription;
            
            salesorder.BasicInfo.OpeningTime = oldSalesorder.BasicInfo.OpeningTime;
            salesorder.BasicInfo.OpeningUserId = oldSalesorder.BasicInfo.OpeningUserId;
            salesorder.BasicInfo.OpeningRemark = oldSalesorder.BasicInfo.OpeningRemark;

            salesorder.BasicInfo.RecoverTime = oldSalesorder.BasicInfo.RecoverTime;
            salesorder.BasicInfo.RecoverUserId = oldSalesorder.BasicInfo.RecoverUserId;
            salesorder.BasicInfo.RecoverRemark = oldSalesorder.BasicInfo.RecoverRemark;

            salesorder.BasicInfo.RefundTime = oldSalesorder.BasicInfo.RefundTime;
            salesorder.BasicInfo.RefundUserId = oldSalesorder.BasicInfo.RefundUserId;
            salesorder.BasicInfo.RefundRemark = oldSalesorder.BasicInfo.RefundRemark;

            salesorder.BasicInfo.ProductReturnTime = oldSalesorder.BasicInfo.ProductReturnTime;
            salesorder.BasicInfo.ReturnUserId = oldSalesorder.BasicInfo.ReturnUserId;
            salesorder.BasicInfo.ReturnRemark = oldSalesorder.BasicInfo.ReturnRemark;

            salesorder.BasicInfo.CancelOpeningTime = oldSalesorder.BasicInfo.CancelOpeningTime;
            salesorder.BasicInfo.CancelOpeningUserId = oldSalesorder.BasicInfo.CancelOpeningUserId;
            salesorder.BasicInfo.CancelOpeningRemark = oldSalesorder.BasicInfo.CancelOpeningRemark;

            salesorder.BasicInfo.SignTime = oldSalesorder.BasicInfo.SignTime;
            salesorder.BasicInfo.SignUserId = oldSalesorder.BasicInfo.SignUserId;
            salesorder.BasicInfo.SignRemark = oldSalesorder.BasicInfo.SignRemark;

            salesorder.BasicInfo.StockingTime = oldSalesorder.BasicInfo.StockingTime;
            salesorder.BasicInfo.StockingUserId = oldSalesorder.BasicInfo.StockingUserId;
            salesorder.BasicInfo.StockingRemark = oldSalesorder.BasicInfo.StockingRemark;
            

            #endregion

            #region 更新信息日志
            
            SalesorderProcessLogModel logModel = new SalesorderProcessLogModel();
            logModel.Description = string.Format("【{0}/{1}】更新订单信息。", opUser.BasicInfo.CnName, opUser.WorkId);
            logModel.ProcessUserId = opUser.WorkId;
            logModel.SalesorderId = salesorder.SalesorderId;
            logModel.SalesorderProcessId = GetGuid();
            logModel.StatusId = salesorder.BasicInfo.NowOrderStatusId;
            logModel.StatusName = salesorder.BasicInfo.NowOrderStatusName;

            if (salesorder.BasicInfo.IsException == 0)
            {
                salesorder.BasicInfo.IsException = 1;
                logModel.Description = string.Format("【{0}/{1}】进行异常处理，订单已更新。", opUser.BasicInfo.CnName, opUser.WorkId);
            }

            #endregion

            try
            {
                BeginTransaction();

                #region 删除订单旧数据

                string deleteCommuniationPackageSQL = "DELETE FROM salesorder_communiationpackage_info WHERE salesorder_id = $saleOrder$";
                string deleteSalesProductSQL = "DELETE FROM salesorder_product_info WHERE salesorder_id = $saleOrder$";

                ParameterCollection pc = new ParameterCollection();
                pc.Add("saleOrder", salesorder.SalesorderId);

                DbUtil.IBPDBManager.IData.ExecuteNonQuery(deleteCommuniationPackageSQL, pc);
                DbUtil.IBPDBManager.IData.ExecuteNonQuery(deleteSalesProductSQL, pc);

                #endregion

                if (SalesorderBasicInfoService.Instance.Update(salesorder.BasicInfo) != 1)
                {
                    RollbackTransaction();
                    message = "操作失败，更新订单基本信息失败";
                    return false;
                }

                if (SalesorderProcessLogService.Instance.Create(logModel) != 1)
                {
                    RollbackTransaction();
                    message = "操作失败，创建订单处理记录信息失败";
                    return false;
                }

                if (salesorder.CommuniationPackageList != null)
                {
                    foreach (SalesorderCommuniationpackageInfoModel packageInfo in salesorder.CommuniationPackageList.Values)
                    {
                        if (SalesorderCommuniationpackageInfoService.Instance.Create(packageInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，更新订单套餐信息失败";
                            return false;
                        }
                    }
                }

                if (salesorder.ProductList != null)
                {
                    foreach (SalesorderProductInfoModel product in salesorder.ProductList.Values)
                    {
                        if (SalesorderProductInfoService.Instance.Create(product) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，更新订单产品信息失败";
                            return false;
                        }
                    }
                }

                CommitTransaction();
                message = "成功更新客户销售订单";
                GetSalesorderDomainModelById(salesorder.SalesorderId, true);
                //CustomerInfoService.Instance.GetCustomerDomainModelById(salesorder.BasicInfo.CustomerId, true);
                result = true;

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error(ex.Message, ex);
                message = "操作异常," + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 检查订单扣款后可修改的信息。
        /// </summary>
        /// <param name="oldSalesorder"></param>
        /// <param name="newSalesorder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool CheckSalesorderModifiedPremission(SalesOrderDomainModel oldSalesorder, SalesOrderDomainModel newSalesorder, out string message)
        {
            message = "检查订单修改权限失败，请与管理员联系";
            int statusId = Convert.ToInt32(oldSalesorder.SalesorderStatus);

            if (statusId > 2)
            {
                if (newSalesorder.BasicInfo.PayType != oldSalesorder.BasicInfo.PayType)
                {
                    message = "订单扣款后，支付方式不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayPrice != oldSalesorder.BasicInfo.PayPrice)
                {
                    message = "订单扣款后，订单金额不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayCardNumber != oldSalesorder.BasicInfo.PayCardNumber)
                {
                    message = "订单扣款后，支付卡号不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayCardPeriod != oldSalesorder.BasicInfo.PayCardPeriod)
                {
                    message = "订单扣款后，支付卡号有效期不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayCardSecuritycode != oldSalesorder.BasicInfo.PayCardSecuritycode)
                {
                    message = "订单扣款后，支付卡号安全码不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayIdcardTypeId != oldSalesorder.BasicInfo.PayIdcardTypeId)
                {
                    message = "订单扣款后，支付人证件类型不可修改，请检查";
                    return false;
                }

                if (newSalesorder.BasicInfo.PayIdcardNumber != oldSalesorder.BasicInfo.PayIdcardNumber)
                {
                    message = "订单扣款后，支付卡人证件号码不可修改，请检查";
                    return false;
                }

                foreach (SalesorderCommuniationpackageInfoModel package in newSalesorder.CommuniationPackageList.Values)
                {
                    if (oldSalesorder.CheckSalesorderCommuniationpackageExists(package.BindCommuniationpackageId) == false)
                    {
                        message = "订单扣款后，选择的话费套餐不可修改，请检查";
                        return false;
                    }
                }
            }

            if (statusId > 4)
            {
                foreach (SalesorderCommuniationpackageInfoModel package in newSalesorder.CommuniationPackageList.Values)
                {
                    if (oldSalesorder.CheckSalesorderCommuniationpackageDiffent(package) == true)
                    {
                        message = "订单开户后，选择的话费套餐信息不可修改，请检查";
                        return false;
                    }
                }
            }

            if (statusId >= 5)
            {
                if (oldSalesorder.BasicInfo.NeedInvoice == 0 && oldSalesorder.BasicInfo.InvoiceTitle != newSalesorder.BasicInfo.InvoiceTitle)
                {
                    message = "订单备货后，开据的发票不可修改，请检查";
                    return false;
                }
            }

            if (statusId >= 7)
            {
                if (oldSalesorder.BasicInfo.Remark != newSalesorder.BasicInfo.Remark
                    || oldSalesorder.BasicInfo.DeliveryAddress != newSalesorder.BasicInfo.DeliveryAddress
                    || oldSalesorder.BasicInfo.DeliveryReceiveCustomerName != newSalesorder.BasicInfo.DeliveryReceiveCustomerName
                    || oldSalesorder.BasicInfo.DeliveryReceivePhonenumber != newSalesorder.BasicInfo.DeliveryReceivePhonenumber
                    || oldSalesorder.BasicInfo.DeliveryCompanyId != newSalesorder.BasicInfo.DeliveryCompanyId)
                {
                    message = "订单发货后，配送信息不可修改，请检查";
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检查订单备货后可修改的信息。
        /// </summary>
        /// <param name="oldSalesorder"></param>
        /// <param name="newSalesorder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool CheckSalesorderModifiedPremissionWhenWaitStocking(SalesOrderDomainModel oldSalesorder, SalesOrderDomainModel newSalesorder, out string message)
        {
            message = "检查订单修改权限失败，请与管理员联系";



            foreach (SalesorderCommuniationpackageInfoModel package in newSalesorder.CommuniationPackageList.Values)
            {
                if (oldSalesorder.CheckSalesorderCommuniationpackageExists(package.BindCommuniationpackageId) == false)
                {
                    message = "订单扣款后，选择的话费套餐不可修改，请检查";
                    return false;
                }
            }

            return true;
        }

        public bool CreateNewSalesorder(ProductShoppingCartDomainModel shoppingCartInfo, string bindWorkorderId, bool isWaittingFollow, out string message)
        {
            bool result = false;
            message = "";

            // 从购物车实体构造订单领域模型。
            SalesOrderDomainModel salesorder = GetSalesorderDomainModelFromShoppingCart(shoppingCartInfo,null, isWaittingFollow, out message);

            if (salesorder != null && string.IsNullOrEmpty(bindWorkorderId) != true)
            {
                salesorder.BasicInfo.RelWorkorderId = bindWorkorderId;
            }

            if (isWaittingFollow == false && salesorder == null)
            {
                return false;
            }

            if (salesorder == null)
            {
                return false;
            }

            SalesorderProcessLogModel logModel = null;

            #region 初始状态

            SalesOrderStatus willChangeStatus = SalesOrderStatus.WaitFollow;

            if (isWaittingFollow)
            {
                // 如果只是提交待跟进
                willChangeStatus = SalesOrderStatus.WaitFollow;
            }
            else
            {
                if (Convert.ToInt32(salesorder.OrderPayType) > 8 )
                {
                    // 如果支付方式为到付，直接提交为待质检。
                    willChangeStatus = SalesOrderStatus.WaitCheck;
                }
                else
                {
                    // 否则提交为待扣款。
                    willChangeStatus = SalesOrderStatus.WaitCharge;
                }
            }

            if (salesorder.BasicInfo.FollowTime < DateTime.Now)
            {
                message = "操作失败，预约跟进时间不得早于当前时间。";
                return false;
            }



            #endregion

            if (willChangeStatus == SalesOrderStatus.WaitCharge)
            {
                if (CheckSalesorder(salesorder, out message) == false)
                {
                    return false;
                }


            }

            SalesorderChangeInfo changeData = new SalesorderChangeInfo(shoppingCartInfo.SalesOrderType, willChangeStatus, true);
            

            // 提交创建订单，状态为待扣款。
            ChangeSalesorderStatus(salesorder,  changeData, out logModel, out message);

            try
            {
                BeginTransaction();

                if (SalesorderBasicInfoService.Instance.Create(salesorder.BasicInfo) != 1)
                {
                    RollbackTransaction();
                    message = "操作失败，创建订单基本信息失败";
                    return false;
                }

                if (SalesorderProcessLogService.Instance.Create(logModel) != 1)
                {
                    RollbackTransaction();
                    message = "操作失败，创建订单处理记录信息失败";
                    return false;
                }

                if (salesorder.CommuniationPackageList != null)
                {
                    foreach (SalesorderCommuniationpackageInfoModel packageInfo in salesorder.CommuniationPackageList.Values)
                    {
                        if (SalesorderCommuniationpackageInfoService.Instance.Create(packageInfo) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，创建订单套餐信息失败";
                            return false;
                        }
                    }
                }

                if (salesorder.ProductList != null)
                {
                    foreach (SalesorderProductInfoModel product in salesorder.ProductList.Values)
                    {
                        if (SalesorderProductInfoService.Instance.Create(product) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，创建订单产品信息失败";
                            return false;
                        }
                    }
                }

                CommitTransaction();
                message = "成功创建客户销售订单";
                CustomerInfoService.Instance.GetCustomerDomainModelById(salesorder.BasicInfo.CustomerId, true);
                result = true;

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error(ex.Message, ex);
                message = "操作异常," + ex.Message;
            }

            return result;
        }

        public bool ChangeSalesorderStatus(SalesOrderDomainModel salesorder, SalesorderChangeInfo changedData, out SalesorderProcessLogModel logModel, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            logModel = null;

            if (salesorder == null)
            {
                message = "操作失败，订单信息异常";
                return false;
            }

            UserDomainModel opUser = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            logModel = new SalesorderProcessLogModel();
            logModel.SalesorderId = salesorder.SalesorderId;
            logModel.SalesorderProcessId = GetGuid(); 
            logModel.ProcessUserId = opUser.BasicInfo.UserId;

            if (changedData.IsSuccessed)
            {
                salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(changedData.ChangeToStatus).ToString();
                salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(changedData.ChangeToStatus);
                salesorder.BasicInfo.IsException = 1;
                salesorder.BasicInfo.ExceptionDesc = "";
                salesorder.BasicInfo.ExceptionReason = "";
                salesorder.BasicInfo.ExceptionTime = null;
                salesorder.BasicInfo.ExceptionUserId = "";

                logModel.StatusId = salesorder.BasicInfo.NowOrderStatusId;
                logModel.StatusName = salesorder.BasicInfo.NowOrderStatusName;
                logModel.Description = string.Format("【{0}/（{1}）】更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());

                switch (changedData.ChangeToStatus)
                {
                    case SalesOrderStatus.WaitFollow:
                        break;

                    case SalesOrderStatus.WaitCheck:
                        if (Convert.ToInt32(salesorder.OrderPayType) > 8)
                        {
                            if (salesorder.BasicInfo.SignTime != null && salesorder.BasicInfo.SignUserId != null)
                            {
                                salesorder.BasicInfo.ChargeTime = DateTime.Now;
                                salesorder.BasicInfo.ChargeUserId = opUser.UserId;
                                salesorder.BasicInfo.PayPosMachineId = changedData.ChangeInfo["posMachineId"].ToString();
                                salesorder.BasicInfo.ChargeBillCode = changedData.ChangeInfo["billCode"].ToString();

                                salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitRecover).ToString();
                                salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitRecover);
                            }

                            logModel.Description = string.Format("【{0}/（{1}）】执行【提交】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        }
                        else
                        {
                            salesorder.BasicInfo.ChargeTime = DateTime.Now;
                            salesorder.BasicInfo.ChargeUserId = opUser.UserId;
                            salesorder.BasicInfo.PayPosMachineId = changedData.ChangeInfo["posMachineId"].ToString();
                            salesorder.BasicInfo.ChargeBillCode = changedData.ChangeInfo["billCode"].ToString();

                            logModel.Description = string.Format("【{0}/（{1}）】执行【扣款】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.ChargeRemark = logModel.Description;
                        }
                        
                        break;

                    case SalesOrderStatus.WaitCharge:
                        if (salesorder.BasicInfo.DeliveryTime != null && salesorder.BasicInfo.DeliveryUserId != null)
                        {
                        }
                        //salesorder.BasicInfo.ChargeTime = DateTime.Now;
                        //salesorder.BasicInfo.ChargeUserId = opUser.UserId;
                        logModel.Description = string.Format("【{0}/（{1}）】执行【提交】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        break;

                    case SalesOrderStatus.WaitApproval:
                            salesorder.BasicInfo.CheckedTime = DateTime.Now;
                            salesorder.BasicInfo.CheckedUserId = opUser.UserId;
                        if (changedData.ChangeInfo["checkType"] != null && changedData.ChangeInfo["checkType"].ToString() == "2")
                        {
                            salesorder.BasicInfo.IsDelayCheck = 0;
                            logModel.Description = string.Format("【{0}/（{1}）】执行【延缓质检】操作，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        }
                        else
                        {
                            salesorder.BasicInfo.IsDelayCheck = 1;
                            salesorder.BasicInfo.ChargeBillCode = changedData.ChangeInfo.ContainsKey("billCode") ? changedData.ChangeInfo["billCode"].ToString() : null;
                            logModel.Description = string.Format("【{0}/（{1}）】执行【质检】操作判断通过，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        }
                                                
                        salesorder.BasicInfo.CheckedRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitOpening:                        
                        salesorder.BasicInfo.ApprovalTime = DateTime.Now;
                        salesorder.BasicInfo.ApprovalUserId = opUser.UserId;
                        logModel.Description = string.Format("【{0}/（{1}）】执行【审批】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        if (salesorder.CommuniationPackageList.Count == 0)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitStocking).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitStocking);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【审批】操作成功，订单更新为【待备货】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                        }
                        salesorder.BasicInfo.CheckedRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitStocking:
                        if (salesorder.CommuniationPackageList == null || salesorder.CommuniationPackageList.Count == 0)
                        {
                            salesorder.BasicInfo.ApprovalTime = DateTime.Now;
                            salesorder.BasicInfo.ApprovalUserId = opUser.UserId;
                            logModel.Description = string.Format("【{0}/（{1}）】执行【审批】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.ApprovalRemark = logModel.Description;
                        }
                        else
                        {
                            salesorder.BasicInfo.OpeningRemark = logModel.Description;
                            salesorder.BasicInfo.OpeningTime = DateTime.Now;
                            salesorder.BasicInfo.OpeningUserId = opUser.UserId;
                            logModel.Description = string.Format("【{0}/（{1}）】执行【开户】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        }
                        
                        break;

                    case SalesOrderStatus.WaitDelivery:
                        salesorder.BasicInfo.StockingTime = DateTime.Now;
                        salesorder.BasicInfo.StockingUserId = opUser.UserId;
                        logModel.Description = string.Format("【{0}/（{1}）】执行【备货】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.StockingRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitSign:
                        salesorder.BasicInfo.DeliveryTime = DateTime.Now;
                        salesorder.BasicInfo.DeliveryUserId = opUser.UserId;
                        salesorder.BasicInfo.DeliveryCompanyId = changedData.ChangeInfo["deliveryCompany"].ToString();
                        salesorder.BasicInfo.DeliveryOrderCode = changedData.ChangeInfo["deliveryCode"].ToString();
                        logModel.Description = string.Format("【{0}/（{1}）】执行【发货】操作成功，订单更新为【{2}】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.DeliveryRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitRecover:
                        salesorder.BasicInfo.RecoverTime = DateTime.Now;
                        salesorder.BasicInfo.RecoverUserId = opUser.UserId;
                        logModel.Description = string.Format("【{0}/（{1}）】执行【回收】操作成功;", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        //if (salesorder.BasicInfo.PayType > 8)
                        //{
                        //    salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitCharge).ToString();
                        //    salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitCharge);
                        //    logModel.Description = string.Format("【{0}/（{1}）】执行【签收】操作成功，订单更新为【待扣款】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                        //}
                        salesorder.BasicInfo.SignRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.Successed:
                        if (salesorder.BasicInfo.SignTime != null
                            && salesorder.BasicInfo.SignUserId != null
                            && salesorder.BasicInfo.RecoverUserId == null 
                            && salesorder.BasicInfo.RecoverTime == null)
                        {
                            salesorder.BasicInfo.RecoverTime = DateTime.Now;
                            salesorder.BasicInfo.RecoverUserId = opUser.UserId;
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Successed).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Successed);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【回收】操作成功，订单更新为【成功】状态", opUser.BasicInfo.CnName, opUser.WorkId);
                        }
                        else
                        {
                            salesorder.BasicInfo.SignTime = DateTime.Now;
                            salesorder.BasicInfo.SignUserId = opUser.UserId;
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Successed).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Successed);
                            //salesorder.BasicInfo.RecoverTime = DateTime.Now;
                            //salesorder.BasicInfo.RecoverUserId = opUser.UserId;
                            logModel.Description = string.Format("【{0}/（{1}）】执行【签收】操作成功，订单更新为【成功】状态", opUser.BasicInfo.CnName, opUser.WorkId);
                        }
                        break;

                    case SalesOrderStatus.WaitRefund:
                        salesorder.BasicInfo.RefundTime = DateTime.Now;
                        salesorder.BasicInfo.RefundUserId = opUser.UserId;
                        if (salesorder.BasicInfo.DeliveryTime != null && salesorder.BasicInfo.ProductReturnTime == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitReturns).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitReturns);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【退款】操作成功;订单更新为【待退货】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.RefundRemark = logModel.Description;
                        
                            return true;
                        }

                        if (salesorder.BasicInfo.OpeningTime != null && salesorder.BasicInfo.OpeningUserId == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitCancelOpening).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitCancelOpening);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【退款】操作成功;订单更新为【待销户】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.RefundRemark = logModel.Description;
                        
                            return true;
                        }

                        salesorder.BasicInfo.CancelTime = DateTime.Now;
                        salesorder.BasicInfo.CancelUserId = opUser.UserId;
                        salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Cancel).ToString();
                        salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Cancel);
                        logModel.Description = string.Format("【{0}/（{1}）】执行【退款】操作成功;订单更新为【已撤消】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.RefundRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitReturns:
                        salesorder.BasicInfo.ProductReturnTime = DateTime.Now;
                        salesorder.BasicInfo.ReturnUserId = opUser.UserId;
                        
                        if (salesorder.BasicInfo.OpeningTime != null && salesorder.BasicInfo.CancelOpeningTime == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitCancelOpening).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitCancelOpening);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【退货】操作成功;订单更新为【待销户】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.ReturnRemark = logModel.Description;
                        
                            return true;
                        }
                        if (salesorder.BasicInfo.ChargeTime != null && salesorder.BasicInfo.RefundTime == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitRefund).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitRefund);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【退货】操作成功;订单更新为【待退款】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.ReturnRemark = logModel.Description;
                        
                            return true;
                        }
                        salesorder.BasicInfo.CancelTime = DateTime.Now;
                        salesorder.BasicInfo.CancelUserId = opUser.UserId;
                        salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Cancel).ToString();
                        salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Cancel);
                        logModel.Description = string.Format("【{0}/（{1}）】执行【退货】操作成功;订单更新为【已撤消】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.ReturnRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.WaitCancelOpening:
                        salesorder.BasicInfo.CancelOpeningTime = DateTime.Now;
                        salesorder.BasicInfo.CancelOpeningUserId = opUser.UserId;
                        
                        if (salesorder.BasicInfo.DeliveryTime != null && salesorder.BasicInfo.ProductReturnTime == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitReturns).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitReturns);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【销户】操作成功;订单更新为【待退货】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.CancelOpeningRemark = logModel.Description;
                        
                            return true;
                        }
                        if (salesorder.BasicInfo.ChargeTime != null && salesorder.BasicInfo.RefundTime == null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitRefund).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitRefund);
                            logModel.Description = string.Format("【{0}/（{1}）】执行【销户】操作成功;订单更新为【待退款】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                            salesorder.BasicInfo.CancelOpeningRemark = logModel.Description;
                        
                            return true;
                        }
                        salesorder.BasicInfo.CancelTime = DateTime.Now;
                        salesorder.BasicInfo.CancelUserId = opUser.UserId;
                        salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Cancel).ToString();
                        salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Cancel);
                        logModel.Description = string.Format("【{0}/（{1}）】执行【销户】操作成功;订单更新为【已撤消】状态;{3} {4}", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.CancelOpeningRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.Exception:
                        salesorder.BasicInfo.ExceptionTime = DateTime.Now;
                        salesorder.BasicInfo.ExceptionUserId = opUser.UserId; 
                        break;

                    case SalesOrderStatus.Cancel:
                        salesorder.BasicInfo.ExceptionDesc = changedData.ChangeInfo["opDesc"].ToString();
                        salesorder.BasicInfo.ExceptionReason = changedData.ChangeInfo["opTypeId"].ToString();
                        salesorder.BasicInfo.ExceptionTime = DateTime.Now;
                        salesorder.BasicInfo.ExceptionUserId = opUser.UserId;
                        if (salesorder.BasicInfo.DeliveryTime != null || salesorder.BasicInfo.DeliveryUserId != null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitReturns).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitReturns);
                            logModel.Description = string.Format("【{0}/（{1}）】提交【撤消】成功; 订单已发货，当前转入【待退货】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                            salesorder.BasicInfo.CancelRemark = logModel.Description;
                        
                            return true;
                        }

                        if (salesorder.BasicInfo.OpeningTime != null || salesorder.BasicInfo.OpeningUserId != null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitCancelOpening).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitCancelOpening);
                            logModel.Description = string.Format("【{0}/（{1}）】提交【撤消】成功; 订单已开户，当前转入【待销户】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                            salesorder.BasicInfo.CancelRemark = logModel.Description;
                        
                            return true;
                        }

                        if (salesorder.BasicInfo.ChargeTime != null || salesorder.BasicInfo.ChargeUserId != null)
                        {
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.WaitRefund).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.WaitRefund);
                            logModel.Description = string.Format("【{0}/（{1}）】提交【撤消】成功; 订单已扣款，当前转入【待退款】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                            salesorder.BasicInfo.CancelRemark = logModel.Description;
                        
                            return true;
                        }


                        if (salesorder.BasicInfo.CancelTime == null || salesorder.BasicInfo.CancelUserId == null)
                        {
                            salesorder.BasicInfo.CancelTime = DateTime.Now;
                            salesorder.BasicInfo.CancelUserId = opUser.UserId;
                            salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Cancel).ToString();
                            salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Cancel);
                            logModel.Description = string.Format("【{0}/（{1}）】提交【撤消】操作; 订单转入【已撤消】状态;", opUser.BasicInfo.CnName, opUser.WorkId);
                            salesorder.BasicInfo.CancelRemark = logModel.Description;
                        }
                        break;

                    default:
                        break;
                }

            }
            else
            {
                //salesorder.BasicInfo.NowOrderStatusId = Convert.ToInt32(SalesOrderStatus.Exception).ToString();
                //salesorder.BasicInfo.NowOrderStatusName = GetSalesorderStatusName(SalesOrderStatus.Exception);
                salesorder.BasicInfo.ExceptionDesc = changedData.ChangeInfo["opDesc"].ToString();
                salesorder.BasicInfo.ExceptionReason = changedData.ChangeInfo["opTypeId"].ToString();
                salesorder.BasicInfo.ExceptionTime = DateTime.Now;
                salesorder.BasicInfo.ExceptionUserId = opUser.UserId;
                salesorder.BasicInfo.IsException = 0;

                logModel.StatusId = salesorder.BasicInfo.NowOrderStatusId;
                logModel.StatusName = salesorder.BasicInfo.NowOrderStatusName;
                logModel.Description = string.Format("【{0}/（{1}）】更新为【{2}】状态异常; {3} {4} ", opUser.BasicInfo.CnName, opUser.WorkId, GetSalesorderStatusName(changedData.ChangeToStatus), changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());

                switch (changedData.ChangeToStatus)
                {
                    case SalesOrderStatus.WaitFollow:
                        break;
                    case SalesOrderStatus.WaitCharge:
                        
                        break;

                    case SalesOrderStatus.WaitCheck:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【扣款】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        if (Convert.ToInt32(salesorder.OrderPayType) > 8)
                        {
                            salesorder.BasicInfo.ChargeRemark = logModel.Description;
                        }
                        break;

                    case SalesOrderStatus.WaitApproval:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【质检】操作判定未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.CheckedRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitOpening:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【审批】操作判定未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.ApprovalRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitStocking:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【开户】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.OpeningRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitDelivery:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【备货】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.StockingRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitSign:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【发货】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.DeliveryRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitRecover:
                       logModel.Description = string.Format("【{0}/（{1}）】执行【签收】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.SignRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.Successed:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【签收】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.RecoverRemark = logModel.Description;
                        break;

                    case SalesOrderStatus.Exception:
                        break;

                    case SalesOrderStatus.WaitRefund:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【退款】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.RefundRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.WaitReturns:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【退货】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.ReturnRemark = logModel.Description;
                        break;
                    case SalesOrderStatus.Cancel:
                        logModel.Description = string.Format("【{0}/（{1}）】执行【撤消】操作未通过; {2} {3} ", opUser.BasicInfo.CnName, opUser.WorkId, changedData.ChangeInfo["opType"].ToString(), changedData.ChangeInfo["opDesc"].ToString());
                        salesorder.BasicInfo.CancelRemark = logModel.Description;
                        break;
                    default:
                        break;
                }
            }

            result = true;

            return result;
        }

        public ProductShoppingCartDomainModel GetShoppingCartDomainModelBySalesorderId(string salesOrderId)
        {
            SalesOrderDomainModel salesorder = GetSalesorderDomainModelById(salesOrderId, false);

            return GetShoppingCartDomainModelFromSalesorderDomainModel(salesorder);
        }

        public ProductShoppingCartDomainModel GetShoppingCartDomainModelFromSalesorderDomainModel(SalesOrderDomainModel salesOrderDomainModel)
        {
            ProductShoppingCartDomainModel cartInfo = new ProductShoppingCartDomainModel();

            if (salesOrderDomainModel == null)
            { 
                return null; 
            }
            
            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(salesOrderDomainModel.BasicInfo.CustomerId, false);

            cartInfo.CustomerId = salesOrderDomainModel.BasicInfo.CustomerId;
            cartInfo.SalesOrderSource = salesOrderDomainModel.BasicInfo.OrderSource;
            if (salesOrderDomainModel.BasicInfo.FollowTime != null)
            {
                cartInfo.FollowTime = Convert.ToDateTime(salesOrderDomainModel.BasicInfo.FollowTime);
            }
            cartInfo.FollowRemark = salesOrderDomainModel.BasicInfo.FollowRemark;

            cartInfo.hasCommunicationPackage = (salesOrderDomainModel.CommuniationPackageList == null || salesOrderDomainModel.CommuniationPackageList.Count == 0);
            cartInfo.OrderDeliveryInfoId = salesOrderDomainModel.BasicInfo.DeliveryBindDeliveryId;
            cartInfo.OrderRemark = salesOrderDomainModel.BasicInfo.Remark;
            cartInfo.PayInfo = new OrderPaymentInfo();
            cartInfo.PayInfo.PayType = (salesOrderDomainModel.BasicInfo.PayType == null) ? OrderPayType.NoCardPosInstallments_ICBC : (OrderPayType)salesOrderDomainModel.BasicInfo.PayType;
            if (salesOrderDomainModel.BasicInfo.PayBindCreditcardId == null)
            {
                cartInfo.PayInfo.PayAccountInfo = null;
            }
            else
            {
                cartInfo.PayInfo.PayAccountInfo = (customer.CreditCardList.ContainsKey(salesOrderDomainModel.BasicInfo.PayBindCreditcardId)) ? customer.CreditCardList[salesOrderDomainModel.BasicInfo.PayBindCreditcardId] : null;
            }
            cartInfo.PriceTotal = (salesOrderDomainModel.BasicInfo.PayPrice == null) ? 0 : Convert.ToDecimal(salesOrderDomainModel.BasicInfo.PayPrice);
            cartInfo.SalesCityId = salesOrderDomainModel.BasicInfo.SalesCityId;
            cartInfo.ProductTotal = (salesOrderDomainModel.ProductList == null) ? 0 : salesOrderDomainModel.ProductList.Count;
            cartInfo.SalesOrderType = salesOrderDomainModel.BasicInfo.SalesorderTypeId;

            if (salesOrderDomainModel.ProductList != null && salesOrderDomainModel.ProductList.Count > 0)
            {
                cartInfo.ProductList = new Dictionary<string,ShoppingCartItemInfo>();

                foreach (SalesorderProductInfoModel product in salesOrderDomainModel.ProductList.Values)
                {
                    ShoppingCartItemInfo cartItem = new ShoppingCartItemInfo();
                    cartItem.Total = (product.ProductCount == null) ? 0 : Convert.ToInt32(product.ProductCount);
                    cartItem.ItemId = product.ProductId;

                    switch (product.ProductType)
                    {
                        case 0:
                            #region 产品包处理

                            SalePackageDomainModel packageDomainModel = SalesPackageInfoService.Instance.GetSalePackageDomainModelById(product.ProductId, false);
                            
                            cartItem.ItemType = "salespackage";
                            cartItem.IsSalesPackage = true;
                            cartItem.CommuniationPackageInfo = new Dictionary<string, CommuniationPackageInfo>();

                            cartItem.SalesPackageInfo = SalesPackageInfoService.Instance.GetSalePackageDomainModelById(product.ProductId, false);
                            Dictionary<string, ProductCategoryInfoModel> commDict = GetCommunicationPackageList(cartItem.SalesPackageInfo);
                            if (commDict != null && commDict.Count > 0)
                            {
                                cartItem.IsCommunicationPackage = true;
                                cartInfo.hasCommunicationPackage = true;
                                cartInfo.CommunicationPackageTotal += commDict.Count;

                                foreach (ProductCategoryInfoModel commInfo in commDict.Values)
                                {
                                    CommuniationPackageInfo packageInfo = new CommuniationPackageInfo();
                                    packageInfo.PackageInfoId = commInfo.ProductCategoryId;

                                    if (salesOrderDomainModel.CommuniationPackageList != null)
                                    {
                                        foreach (SalesorderCommuniationpackageInfoModel sci in salesOrderDomainModel.CommuniationPackageList.Values)
                                        {
                                            if (commInfo.ProductCategoryId == sci.BindCommuniationpackageId)
                                            {
                                                packageInfo.BindedMainPhoneNumberId = sci.MainPhonenumberId;
                                                packageInfo.PhoneOwnerInfoId = sci.OwnerBindCreditcardId;
                                                packageInfo.IsCollections = sci.IsCollection == 0;
                                                packageInfo.PackageInfoId = sci.BindCommuniationpackageId;
                                                packageInfo.CollectionInfoId = sci.CollectionBindCreditcardId;
                                                packageInfo.OpenningCityId = sci.OpeningCityId;
                                            }
                                        }
                                    }
                                    cartItem.CommuniationPackageInfo.Add(packageInfo.PackageInfoId, packageInfo);
                                }
                            }
                            #endregion
                            break;

                        case 1:
                            #region 产品类型
                            cartItem.ItemType = "productcategory";
                            cartItem.CommuniationPackageInfo = new Dictionary<string, CommuniationPackageInfo>();

                            cartItem.ProductCategory = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(product.ProductId);
                            ProductCategoryGroupInfoModel groupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupById(cartItem.ProductCategory.GroupName);
                            if (groupInfo != null && groupInfo.ProductCategoryGroupId.ToUpper() == "30D52946-A29A-4127-A7FE-39A0D6206BF6")
                            {
                                cartItem.IsCommunicationPackage = true;
                                cartInfo.hasCommunicationPackage = true;
                                cartInfo.CommunicationPackageTotal++;

                                CommuniationPackageInfo packageInfo = new CommuniationPackageInfo();
                                packageInfo.PackageInfoId = cartItem.ProductCategory.ProductCategoryId;

                                if (salesOrderDomainModel.CommuniationPackageList != null)
                                {
                                    foreach (SalesorderCommuniationpackageInfoModel sci in salesOrderDomainModel.CommuniationPackageList.Values)
                                    {
                                        if (cartItem.ProductCategory.ProductCategoryId == sci.BindCommuniationpackageId)
                                        {
                                            packageInfo.BindedMainPhoneNumberId = sci.MainPhonenumberId;
                                            packageInfo.PhoneOwnerInfoId = sci.OwnerBindCreditcardId;
                                            packageInfo.IsCollections = sci.IsCollection == 0;
                                            packageInfo.PackageInfoId = sci.BindCommuniationpackageId;
                                            packageInfo.CollectionInfoId = sci.CollectionBindCreditcardId;
                                            packageInfo.OpenningCityId = sci.OpeningCityId;
                                        }
                                    }
                                }
                                cartItem.CommuniationPackageInfo.Add(packageInfo.PackageInfoId, packageInfo);
                            }


                            #endregion
                            break;

                        case 2:
                            #region 产品单品
                            cartItem.ItemType = "productitem";
                            cartItem.ProductInfo = ProductInfoService.Instance.GetProductDomainInfoByProductId(product.ProductId, false);
                            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(cartItem.ProductInfo.BasicInfo.CategoryId);
                            if (catInfo != null && catInfo.ProductCategoryId.ToUpper() == "DB487569-F636-43B7-9D31-259CD59774B7")
                            {
                                cartItem.IsPhoneNumber = true;
                                cartInfo.hasPhoneNumber = true;
                            }
                            #endregion
                            break;

                        default:
                            break;
                    }

                    cartInfo.ProductList.Add(cartItem.ItemId, cartItem);
                }
            }

            return cartInfo;
        }

        public bool CheckSalesorder(string salesOrderId, out string message)
        {
            SalesOrderDomainModel salesorder = GetSalesorderDomainModelById(salesOrderId, false);

            return CheckSalesorder(salesorder, out message);
        }

        public bool CheckSalesorder(SalesOrderDomainModel salesorder, out string message)
        {
            message = "检查订单完整性失败，请检查订单信息";

            if (salesorder == null || salesorder.BasicInfo == null)
            {
                message = "操作失败，订单信息不存在";
                return false;
            }

            if (salesorder.BasicInfo.PayType == null)
            {
                message = "订单信息不完整，缺失支付方式，请检查";
                return false;
            }

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(salesorder.BasicInfo.CustomerId, false);
            if (customer == null)
            {
                message = "订单客户信息不存在，请检查";
                return false;
            }

            if (string.IsNullOrEmpty(customer.BasicInfo.CustomerName) ||
                customer.BasicInfo.CustomerName == "男士" ||
                customer.BasicInfo.CustomerName == "女士" ||
                customer.BasicInfo.CustomerName == "不详" ||
                customer.BasicInfo.CustomerName == "未知" ||
                customer.BasicInfo.CustomerName == "先生/小姐" ||
                customer.BasicInfo.CustomerName == "先生" ||
                customer.BasicInfo.CustomerName == "小姐")
            {
                message = "缺少订单客户姓名信息，请检查";
                return false;
            }

            if (salesorder.BasicInfo.PayType < 9)
            {
                if (salesorder.BasicInfo.PayType == null || salesorder.BasicInfo.PayType == 0)
                {
                    message = "订单信息不完整，请选择订单的支付方式";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayCardBankId))
                {
                    message = "订单信息不完整，缺失支付账户开户行信息，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayCardNumber))
                {
                    message = "订单信息不完整，缺失支付账户卡号信息，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayCardPeriod))
                {
                    message = "订单信息不完整，缺失支付账户有效期信息，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayCardSecuritycode))
                {
                    message = "订单信息不完整，缺失支付账户安全码信息，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayCustomerName))
                {
                    message = "订单信息不完整，缺失支付账户持卡人姓名，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayIdcardNumber))
                {
                    message = "订单信息不完整，缺失支付账户持卡人证件号码信息，请检查";
                    return false;
                }

                if (string.IsNullOrEmpty(salesorder.BasicInfo.PayIdcardTypeId))
                {
                    message = "订单信息不完整，缺失支付账户持卡人证件类型信息，请检查";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(salesorder.BasicInfo.DeliveryAddress))
            {
                message = "订单信息不完整，缺失配送地址信息，请检查";
                return false;
            }

            if (string.IsNullOrEmpty(salesorder.BasicInfo.DeliveryReceiveCustomerName))
            {
                message = "订单信息不完整，缺失配送收货人信息，请检查";
                return false;
            }

            if (string.IsNullOrEmpty(salesorder.BasicInfo.DeliveryReceivePhonenumber))
            {
                message = "订单信息不完整，缺失配送收货人电话信息，请检查";
                return false;
            }

            if (salesorder.CommuniationPackageList != null)
            {
                foreach (SalesorderCommuniationpackageInfoModel packageInfo in salesorder.CommuniationPackageList.Values)
                {
                    if (string.IsNullOrEmpty(packageInfo.BindCommuniationpackageId) == false
                        && string.IsNullOrEmpty(packageInfo.MainPhonenumberId) == true)
                    {
                        message = "订单信息不完整，缺失与话费套餐关联的手机号码，请检查";
                        return false;
                    }

                    if(string.IsNullOrEmpty(packageInfo.OwnerCustomerName))
                    {
                        message = "订单信息不完整，缺失话费套餐的机主姓名，请检查";
                        return false;
                    }

                    if(string.IsNullOrEmpty(packageInfo.IdcardTypeId))
                    {
                        message = "订单信息不完整，缺失话费套餐机主证件类型信息，请检查";
                        return false;
                    }

                    if (string.IsNullOrEmpty(packageInfo.IdcardNumber))
                    {
                        message = "订单信息不完整，缺失话费套餐机主证件号码信息，请检查";
                        return false;
                    }

                    if (packageInfo.IsCollection == 0)
                    {
                        if (string.IsNullOrEmpty(packageInfo.CollectionCardNumber))
                        {
                            message = "订单信息不完整，缺失话费套餐托收卡号信息，请检查";
                            return false;
                        }

                        if (string.IsNullOrEmpty(packageInfo.CollectionCustomerName))
                        {
                            message = "订单信息不完整，缺失话费套餐托收户名信息，请检查";
                            return false;
                        }
                    }
                }
            }

            if (salesorder.ProductList != null)
            {
                foreach (SalesorderProductInfoModel product in salesorder.ProductList.Values)
                {
                    switch (product.ProductType)
                    {
                        case 0:
                            // 此处判断产品包信息完整性。
                            break;

                        case 1:
                            // 此处判断产品类型信息完整性。
                            break;

                        case 2:
                            // 此处判断产品单品信息完整性。
                            break;

                        default:
                            break;
                    }
                }
            }

            return true;
        }

        public string CreateSalesorderCode()
        {
            string sql = "select COUNT(1) from salesorder_basic_info where DATEDIFF(DAY,created_on,GETDATE()) = 0";
            long total = Convert.ToInt64(DateTime.Now.ToString("yyMMdd")) * 10000 + Convert.ToInt32(DbUtil.IBPDBManager.IData.ExecuteScalar(sql)) + 1;

            return "S" + total.ToString();
        }

        /// <summary>
        /// 从购物车实体构造订单领域模型。
        /// </summary>
        /// <param name="cartInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private SalesOrderDomainModel GetSalesorderDomainModelFromShoppingCart(ProductShoppingCartDomainModel cartInfo, string oldSalesOrder, bool isWaittingFollow, out string message)
        {
            SalesOrderDomainModel salesorder = null;
            CustomerDomainModel customer = null;
            message = "操作失败，请与管理员联系";

            #region 有效性验证

            customer = CustomerInfoService.Instance.GetCustomerDomainModelById(cartInfo.CustomerId, false);

            if (customer == null)
            {
                message = "操作失败，不存在的客户ID";
                return null;
            }

            if (!isWaittingFollow)
            {
                if (cartInfo == null || cartInfo.ProductTotal == 0 || cartInfo.ProductList == null || cartInfo.ProductList.Count == 0)
                {
                    message = "操作失败，购物车中未选择产品";
                    return null;
                }

                if (cartInfo.PayInfo.PayType == 0)
                {
                    message = "操作失败，未指定订单支付方式";
                    return null;
                }

                if (Convert.ToInt32(cartInfo.PayInfo.PayType) < 9 && cartInfo.PayInfo.PayAccountInfo == null)
                {
                    message = "操作失败，未指定订单支付信息";
                    return null;
                }

                if (cartInfo.PayInfo.PayAccountInfo != null && Convert.ToInt32(cartInfo.PayInfo.PayType) < 9)
                {
                    if (string.IsNullOrEmpty(cartInfo.PayInfo.PayAccountInfo.CreditcardNumber)
                        || string.IsNullOrEmpty(cartInfo.PayInfo.PayAccountInfo.SecurityCode)
                        || string.IsNullOrEmpty(cartInfo.PayInfo.PayAccountInfo.Period))
                    {
                        message = "操作失败，订单支付信息不完整，请检查";
                        return null;
                    }
                }

                if (string.IsNullOrEmpty(cartInfo.OrderDeliveryInfoId))
                {
                    message = "操作失败，未指定订单配送信息";
                    return null;
                }

                int communiationPackageTotal = 0;
                int phoneNumberTotal = 0;

                foreach (ShoppingCartItemInfo cartItem in cartInfo.ProductList.Values)
                {
                    if (cartItem.IsPhoneNumber)
                    {
                        phoneNumberTotal++;
                    }

                    if (cartItem.IsCommunicationPackage && cartItem.CommuniationPackageInfo.Count > 0)
                    {
                        foreach (CommuniationPackageInfo package in cartItem.CommuniationPackageInfo.Values)
                        {
                            communiationPackageTotal++;

                            if (string.IsNullOrEmpty(package.PackageInfoId))
                            {
                                message = "操作失败，套餐信息不存在";
                                return null;
                            }

                            if (string.IsNullOrEmpty(package.BindedMainPhoneNumberId))
                            {
                                message = "操作失败，话费套餐未选择绑定的手机号码";
                                return null;
                            }

                            if (string.IsNullOrEmpty(package.PhoneOwnerInfoId))
                            {
                                message = "操作失败，话费套餐未设置机主信息";
                                return null;
                            }

                            if (package.IsCollections && string.IsNullOrEmpty(package.CollectionInfoId))
                            {
                                message = "操作失败，话费套餐选择了托收，但未设置托收信息";
                                return null;
                            }
                        }
                    }
                }

                if (communiationPackageTotal != phoneNumberTotal)
                {
                    message = "操作失败，选择的套餐总数与手机号码总数必须一致";
                    return null;
                }
            }
            //else
            //{
            //    if (cartInfo != null || cartInfo.ProductTotal != 0 || cartInfo.ProductList != null || cartInfo.ProductList.Count != 0)
            //    {


            //        int communiationPackageTotal = 0;
            //        int phoneNumberTotal = 0;

            //        foreach (ShoppingCartItemInfo cartItem in cartInfo.ProductList.Values)
            //        {
            //            if (cartItem.IsPhoneNumber)
            //            {
            //                phoneNumberTotal++;
            //            }

            //            if (cartItem.IsCommunicationPackage && cartItem.CommuniationPackageInfo.Count > 0)
            //            {
            //                foreach (CommuniationPackageInfo package in cartItem.CommuniationPackageInfo.Values)
            //                {
            //                    communiationPackageTotal++;
            //                }
            //            }
            //        }

            //        if (communiationPackageTotal != phoneNumberTotal)
            //        {
            //            message = "操作失败，选择的套餐总数与手机号码总数必须一致";
            //            return null;
            //        }
            //    }
            //}

            #endregion

            salesorder = new SalesOrderDomainModel();
            salesorder.BasicInfo = new SalesorderBasicInfoModel();
            salesorder.BasicInfo.SalesorderId = string.IsNullOrEmpty(oldSalesOrder) ? GetGuid() : oldSalesOrder;
            salesorder.BasicInfo.SalesorderCode = CreateSalesorderCode(); // string.Format("SO{0}", DateTime.Now.ToString("yyyyMMddhhmmssfff"));

            #region 订单基本信息

            salesorder.BasicInfo.SalesorderTypeId = cartInfo.SalesOrderType;
            salesorder.BasicInfo.OrderSource = cartInfo.SalesOrderSource;
            if (cartInfo.FollowTime != null)
            {
                salesorder.BasicInfo.FollowTime = cartInfo.FollowTime;
            }
            salesorder.BasicInfo.FollowRemark = cartInfo.FollowRemark;
            salesorder.BasicInfo.CustomerId = cartInfo.CustomerId;
            salesorder.BasicInfo.SalesCityId = cartInfo.SalesCityId;
            salesorder.BasicInfo.PayPrice = cartInfo.PriceTotal;
            salesorder.BasicInfo.PayType = Convert.ToInt32(cartInfo.PayInfo.PayType);
            salesorder.BasicInfo.Remark = cartInfo.OrderRemark;
            salesorder.BasicInfo.IsException = 1;

            #endregion

            #region 订单支付信息

            if (cartInfo.PayInfo != null && cartInfo.PayInfo.PayAccountInfo != null)
            {
                salesorder.BasicInfo.PayBindCreditcardId = cartInfo.PayInfo.PayAccountInfo.CreditcardId;
                salesorder.BasicInfo.PayBillAddress = cartInfo.PayInfo.PayAccountInfo.BillAddress;
                salesorder.BasicInfo.PayBillPostcode = cartInfo.PayInfo.PayAccountInfo.BillZipcode;
                salesorder.BasicInfo.PayCardBankId = cartInfo.PayInfo.PayAccountInfo.Bank;
                salesorder.BasicInfo.PayCardNumber = cartInfo.PayInfo.PayAccountInfo.CreditcardNumber;
                salesorder.BasicInfo.PayCardPeriod = cartInfo.PayInfo.PayAccountInfo.Period;
                salesorder.BasicInfo.PayCardSecuritycode = cartInfo.PayInfo.PayAccountInfo.SecurityCode;
                salesorder.BasicInfo.PayCityId = cartInfo.PayInfo.PayAccountInfo.BillChinaId.ToString();
                salesorder.BasicInfo.PayCustomerName = cartInfo.PayInfo.PayAccountInfo.CardUsername;
                salesorder.BasicInfo.PayIdcardNumber = cartInfo.PayInfo.PayAccountInfo.IdcardNumber;
                salesorder.BasicInfo.PayIdcardTypeId = cartInfo.PayInfo.PayAccountInfo.IdcardType;
            }

            #endregion

            #region 配送信息绑定

            if (string.IsNullOrEmpty(cartInfo.OrderDeliveryInfoId) == false && customer.DeliveryList.ContainsKey(cartInfo.OrderDeliveryInfoId))
            {
                salesorder.BasicInfo.DeliveryBindDeliveryId = cartInfo.OrderDeliveryInfoId;
                salesorder.BasicInfo.DeliveryAddress = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].DeliveryAddress;
                salesorder.BasicInfo.DeliveryChinaId = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].DeliveryRegionId;
                salesorder.BasicInfo.DeliveryPostcode = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].PostCode;
                salesorder.BasicInfo.DeliveryReceiveCustomerName = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].Consignee;
                salesorder.BasicInfo.DeliveryReceivePhonenumber = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].ConsigneePhone;
                salesorder.BasicInfo.DeliveryType = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].DeliveryType;
                salesorder.BasicInfo.NeedInvoice = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].NeedBills;
                salesorder.BasicInfo.InvoiceTitle = customer.DeliveryList[cartInfo.OrderDeliveryInfoId].BillTitle;
            }

            #endregion

            salesorder.ProductList = new Dictionary<string, SalesorderProductInfoModel>();
            salesorder.CommuniationPackageList = new Dictionary<string, SalesorderCommuniationpackageInfoModel>();
            SalesorderProductInfoModel product = null;
            //SalesorderCommuniationpackageInfoModel package = null;

            if (cartInfo.ProductList != null)
            {
                foreach (ShoppingCartItemInfo cartItem in cartInfo.ProductList.Values)
                {
                    #region 订单产品信息

                    product = new SalesorderProductInfoModel();
                    product.SalesorderId = salesorder.BasicInfo.SalesorderId;
                    product.SalesorderProductitemId = GetGuid();

                    switch (cartItem.ItemType.ToLower())
                    {
                        case "salespackage":
                            product.ItemPrice = cartItem.SalesPackageInfo.BasicInfo.PriceTotal;
                            product.ProductCount = cartItem.Total;
                            product.PriceTotal = product.ItemPrice * product.ProductCount;
                            product.ProductCode = cartItem.SalesPackageInfo.BasicInfo.PackageName;
                            product.ProductId = cartItem.SalesPackageInfo.BasicInfo.SalesPackageId;
                            product.ProductName = cartItem.SalesPackageInfo.BasicInfo.PackageName;
                            product.ProductType = 0;
                            break;

                        case "productcategory":
                            product.ItemPrice = cartItem.ProductCategory.ItemPrice;
                            product.ProductCount = cartItem.Total;
                            product.PriceTotal = product.ItemPrice * product.ProductCount;
                            product.ProductCode = cartItem.ProductCategory.CategoryCode;
                            product.ProductId = cartItem.ProductCategory.ProductCategoryId;
                            product.ProductName = cartItem.ProductCategory.CategoryName;
                            product.ProductType = 1;
                            break;

                        case "productitem":
                            product.ItemPrice = cartItem.ProductInfo.BasicInfo.ItemPrice;
                            product.ProductCount = cartItem.Total;
                            product.PriceTotal = product.ItemPrice * product.ProductCount;
                            product.ProductCode = cartItem.ProductInfo.BasicInfo.ProductCode;
                            product.ProductId = cartItem.ProductInfo.BasicInfo.ProductId;
                            product.ProductName = cartItem.ProductInfo.BasicInfo.ProductName;
                            product.ProductType = 2;
                            break;

                        default:
                            break;
                    }

                    salesorder.ProductList.Add(product.SalesorderProductitemId, product);

                    #endregion

                    #region 订单套餐信息

                    if (cartItem.IsCommunicationPackage && cartItem.CommuniationPackageInfo != null && cartItem.CommuniationPackageInfo.Count > 0)
                    {
                        foreach (CommuniationPackageInfo communiPackage in cartItem.CommuniationPackageInfo.Values)
                        {
                            SalesorderCommuniationpackageInfoModel package = new SalesorderCommuniationpackageInfoModel();
                            package.SalesorderCommunicationpackageId = GetGuid();
                            package.SalesorderId = salesorder.BasicInfo.SalesorderId;

                            // 套餐信息
                            package.BindCommuniationpackageId = communiPackage.PackageInfoId;
                            package.OpeningCityId = communiPackage.OpenningCityId;

                            // 绑定号码信息
                            ProductInfoDomainModel mainPhone = ProductInfoService.Instance.GetProductDomainInfoByProductId(communiPackage.BindedMainPhoneNumberId, false);
                            if (mainPhone != null)
                            {
                                package.MainPhonenumberId = communiPackage.BindedMainPhoneNumberId;
                                package.BindMainPhonenumber = mainPhone.BasicInfo.ProductName;
                            }
                            ProductInfoDomainModel subPhone = ProductInfoService.Instance.GetProductDomainInfoByProductId(communiPackage.BindedSubsidiaryPhoneNumberId, false);
                            if (subPhone != null)
                            {
                                package.SubsidiaryPhonenumberId = communiPackage.BindedSubsidiaryPhoneNumberId;
                                package.BindSubsidiaryPhonenumber = subPhone.BasicInfo.ProductName;
                            }

                            // 机主信息
                            if (string.IsNullOrEmpty(communiPackage.PhoneOwnerInfoId) == false && customer.CreditCardList.ContainsKey(communiPackage.PhoneOwnerInfoId))
                            {
                                package.OwnerBindCreditcardId = customer.CreditCardList[communiPackage.PhoneOwnerInfoId].CreditcardId;
                                package.OwnerCustomerName   = customer.CreditCardList[communiPackage.PhoneOwnerInfoId].CardUsername;
                                package.IdcardTypeId        = customer.CreditCardList[communiPackage.PhoneOwnerInfoId].IdcardType;
                                package.IdcardNumber        = customer.CreditCardList[communiPackage.PhoneOwnerInfoId].IdcardNumber;
                            }

                            // 托收信息
                            package.IsCollection = (communiPackage.IsCollections) ? 0 : 1;
                            if (communiPackage.IsCollections && string.IsNullOrEmpty(communiPackage.CollectionInfoId) == false && customer.CreditCardList.ContainsKey(communiPackage.CollectionInfoId))
                            {
                                package.CollectionBindCreditcardId  =  customer.CreditCardList[communiPackage.CollectionInfoId].CreditcardId;
                                package.CollectionBankId            =  customer.CreditCardList[communiPackage.CollectionInfoId].Bank;
                                package.CollectionCardNumber        =  customer.CreditCardList[communiPackage.CollectionInfoId].CreditcardNumber;
                                package.CollectionCustomerName      =  customer.CreditCardList[communiPackage.CollectionInfoId].CardUsername;
                                package.IdcardNumber                =  customer.CreditCardList[communiPackage.CollectionInfoId].IdcardNumber;
                                package.IdcardTypeId                =  customer.CreditCardList[communiPackage.CollectionInfoId].IdcardType;
                                package.CollectionBillCityId        =  ((customer.CreditCardList[communiPackage.CollectionInfoId].BillChinaId == null) ? null : customer.CreditCardList[communiPackage.CollectionInfoId].BillChinaId.ToString());
                                package.CollectionAddress           =  customer.CreditCardList[communiPackage.CollectionInfoId].BillAddress;
                            }
                            salesorder.CommuniationPackageList.Add(package.SalesorderCommunicationpackageId, package);
                        }
                    }

                    #endregion
                }
            }

            return salesorder;
        }


        public bool HasCancelSalesorderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "DoCancelSalesOrder");
        }

        public bool HasUpdateSalesorderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "DoUpdateSalesOrder");
        }

        public bool HasSalesorderProcessPremission(SalesOrderStatus status, SalesOrderDomainModel salesorder)
        {
            if (status == SalesOrderStatus.Cancel
                && HasSalesorderProcessPremission(status)
                && salesorder.BasicInfo.CreatedBy == SessionUtil.Current.UserId)
            {
                return true;
            }

            return false;
        }

        public bool HasSalesorderProcessPremission(SalesOrderStatus status)
        {
            bool result = false;

            switch (status)
            {
                case SalesOrderStatus.WaitCharge:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderCharge");
                case SalesOrderStatus.WaitCheck:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderCheck");
                case SalesOrderStatus.WaitApproval:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderApproval");
                case SalesOrderStatus.WaitOpening:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderOpening");
                case SalesOrderStatus.WaitStocking:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderStocking");
                case SalesOrderStatus.WaitDelivery:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderDelivery");
                case SalesOrderStatus.WaitSign:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderSign");
                case SalesOrderStatus.WaitRecover:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderRecover");
                
                case SalesOrderStatus.WaitRefund:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderRefund");
                case SalesOrderStatus.WaitReturns:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderReturn");
                case SalesOrderStatus.WaitCancelOpening:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "SalesOrderCancelOpening");
                case SalesOrderStatus.Cancel:
                    return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "OrderCenter", "DoCancelSalesOrder");
                default:
                    break;
            }

            return result;
        }

        public List<string> GetSalesorderIdList(bool? isDelayCheck, bool isCreatedBy, SalesOrderStatus? orderStatus, string exception, string incomePhoneNumber, string selectedPhoneNumber, Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            List<string> result = null;
            total = 0;

            StringBuilder sqlBuilder = new StringBuilder();
            ParameterCollection pc = new ParameterCollection();

            sqlBuilder.Append(@"
FROM 
    salesorder_basic_info 
LEFT JOIN
    customer_basic_info
ON
    salesorder_basic_info.customer_id = customer_basic_info.customer_id
LEFT JOIN
    user_info as created_user_info
ON
    salesorder_basic_info.created_by = created_user_info.user_id
LEFT JOIN
   Salesorder_communiationpackage_info
ON 
    salesorder_basic_info.salesorder_id=Salesorder_communiationpackage_info.salesorder_id

LEFT JOIN
   salesorder_product_info
ON 
    salesorder_basic_info.salesorder_id=salesorder_product_info.salesorder_id
WHERE 
    1 = 1 ");

            if (orderStatus != null)
            {
                if (orderStatus != SalesOrderStatus.All)
                {


                    if (orderStatus == SalesOrderStatus.Exception)
                    {
                        sqlBuilder.Append(@" AND is_exception = 0 ");
                        if (!string.IsNullOrEmpty(exception))
                        {
                            sqlBuilder.Append(@" AND now_order_status_id = $exception$ ");
                            pc.Add("exception", Convert.ToInt32(exception));
                        }
                    }
                    else
                    {
                        if (orderStatus == SalesOrderStatus.WaitRecover)
                        {
                            sqlBuilder.Append(@" AND now_order_status_id = $orderStatus$ ");
                            sqlBuilder.Append(@" AND recover_time IS NULL AND recover_user_id IS NULL ");
                            sqlBuilder.Append(@" AND  is_exception <> 0  ");
                            pc.Add("orderStatus", Convert.ToInt32(SalesOrderStatus.Successed));
                        }
                        else
                        {
                            sqlBuilder.Append(@" AND now_order_status_id = $orderStatus$ ");
                            sqlBuilder.Append(@" AND  is_exception <> 0  ");
                            pc.Add("orderStatus", Convert.ToInt32(orderStatus));
                        }
                    }
                }
            }

            if (isDelayCheck != null)
            {
                if (isDelayCheck == true)
                {
                    sqlBuilder.Append(@" AND is_delay_check = 0 ");
                }
                else if (isDelayCheck == false)
                {
                    sqlBuilder.Append(@" AND is_delay_check != 0 ");
                }
            }

          #region 根据用户名查询
		    if (isCreatedBy == true)
            {
                sqlBuilder.Append(@" AND salesorder_basic_info.created_by = $created_by$  ");

                pc.Add("created_by", SessionUtil.Current.UserId);
            } 
	     #endregion
            #region 所选号码查询条件

            if (!string.IsNullOrEmpty(selectedPhoneNumber))
            {
                sqlBuilder.Append(@" AND salesorder_basic_info.salesorder_id IN (SELECT salesorder_basic_info.salesorder_id FROM salesorder_communiationpackage_info WHERE bind_main_phonenumber = $selectedPhoneNumber$ or bind_subsidiary_phonenumber = $selectedPhoneNumber$ ) ");

                pc.Add("selectedPhoneNumber", selectedPhoneNumber);
            }

            #endregion

            #region 来电号码查询条件

            if (string.IsNullOrEmpty(incomePhoneNumber) == false)
            {
                sqlBuilder.Append(@" AND customer_basic_info.customer_id IN (SELECT customer_basic_info.customer_id FROM customer_phone_info WHERE phone_number LIKE $incomePhoneNumber$)");

                pc.Add("incomePhoneNumber", string.Format("%{0}%", incomePhoneNumber));
            }

            #endregion

            #region 构造查询条件
            int count = 0;
            foreach (QueryItemDomainModel item in queryCollection.Values)
            {
                
                switch (item.Operation)
                {
                    case "equal":
                        sqlBuilder.AppendFormat(@" AND {0} = $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "notequal":
                        sqlBuilder.AppendFormat(@" AND {0} <> $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "contain":
                        sqlBuilder.AppendFormat(@" AND {0} LIKE $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), "%" + item.SearchValue + "%");
                        break;

                    case "greater":
                        sqlBuilder.AppendFormat(@" AND {0} > $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "greaterequal":
                        sqlBuilder.AppendFormat(@" AND {0} >= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "less":
                        sqlBuilder.AppendFormat(@" AND {0} < $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "lessequal":
                        sqlBuilder.AppendFormat(@" AND {0} <= $value{1}$", item.FieldType, count);
                        pc.Add("value" + count.ToString(), item.SearchValue);
                        break;

                    case "between":
                        sqlBuilder.AppendFormat(@" AND {0} BETWEEN $begin{1}$ AND $end{1}$", item.FieldType, count);
                        pc.Add("begin" + count.ToString(), item.BeginTime);
                        pc.Add("end" + count.ToString(), item.EndTime);
                        break;

                    case "today":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(DAY,{0},GETDATE()) = 0", item.FieldType);
                        break;

                    case "week":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(WEEK,{0},GETDATE()) = 0", item.FieldType);
                        break;

                    case "month":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(MONTH,{0},GETDATE()) = 0", item.FieldType);
                        break;

                    case "quarter":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(QUARTER,{0},GETDATE()) = 0", item.FieldType);
                        break;

                    case "year":
                        sqlBuilder.AppendFormat(@" AND DATEDIFF(YEAR,{0},GETDATE()) = 0", item.FieldType);
                        break;

                    default:
                        break;
                }

                count++;
            }

            #endregion

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(DISTINCT(salesorder_basic_info.salesorder_id)) " + sqlBuilder.ToString(), pc));

            DataTable dt = ExecuteDataTable("SELECT DISTINCT salesorder_basic_info.salesorder_id, " + "salesorder_basic_info." + orderField  + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create("salesorder_basic_info." + orderField, orderDirection));
            result = ModelConvertFrom(dt);

            return result;
        }

        /// <summary>
        /// 根据表单数据构造购物车实体。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="creditCardIdString"></param>
        /// <param name="deliveryInfoId"></param>
        /// <param name="orderRemark"></param>
        /// <param name="productListString"></param>
        /// <param name="removeProductId"></param>
        /// <param name="openAccountInfo"></param>
        /// <returns></returns>
        public ProductShoppingCartDomainModel GetShoppingCartFromProductListString(string orderSource, string orderType, string customerId, string salesCityId, string payType, string creditCardIdString, string deliveryInfoId, string orderRemark, string productListString, string removeProductId, string openAccountInfo, string followTime, string followRemark)
        {
            ProductShoppingCartDomainModel shoppingCartInfo = new ProductShoppingCartDomainModel();
            shoppingCartInfo.OrderRemark = orderRemark;
            shoppingCartInfo.CustomerId = customerId;
            shoppingCartInfo.SalesCityId = salesCityId;
            shoppingCartInfo.SalesOrderType = orderType;
            shoppingCartInfo.SalesOrderSource = orderSource;

            if (string.IsNullOrEmpty(followTime) == false)
            {
                shoppingCartInfo.FollowTime = Convert.ToDateTime(followTime);
            }
            shoppingCartInfo.FollowRemark = followRemark;

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(customerId, false);
            if (customer != null)
            {
                shoppingCartInfo.PayInfo = new OrderPaymentInfo();

                if (string.IsNullOrEmpty(payType) == false)
                {
                    shoppingCartInfo.PayInfo.PayType = (OrderPayType)Convert.ToInt32(payType);
                }
                if (string.IsNullOrEmpty(creditCardIdString) == false)
                {
                    if (customer.CreditCardList.ContainsKey(creditCardIdString))
                    {
                        shoppingCartInfo.PayInfo.PayAccountInfo = customer.CreditCardList[creditCardIdString];
                    }
                    else
                    {
                        if (customer.CreditCardList != null && customer.CreditCardList.Count > 0)
                        {
                            foreach (CustomerCreditcardInfoModel credit in customer.CreditCardList.Values)
                            {
                                shoppingCartInfo.PayInfo.PayAccountInfo = credit;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (customer.CreditCardList != null && customer.CreditCardList.Count > 0)
                    {
                        foreach (CustomerCreditcardInfoModel credit in customer.CreditCardList.Values)
                        {
                            if (1 == 1 
                                // && string.IsNullOrEmpty(credit.Bank) == false
                                && string.IsNullOrEmpty(credit.CardUsername) == false
                                && string.IsNullOrEmpty(credit.CreditcardNumber) == false
                                // && string.IsNullOrEmpty(credit.IdcardNumber) == false
                                // && string.IsNullOrEmpty(credit.IdcardType) == false
                                && string.IsNullOrEmpty(credit.Period) == false
                                && string.IsNullOrEmpty(credit.SecurityCode) == false)
                            {
                                shoppingCartInfo.PayInfo = new OrderPaymentInfo();
                                shoppingCartInfo.PayInfo.PayType = OrderPayType.NoCardPosInstallments_ICBC;
                                shoppingCartInfo.PayInfo.PayAccountInfo = credit;

                                break;
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(deliveryInfoId) == false &&
                    customer.DeliveryList != null &&
                    customer.DeliveryList.ContainsKey(deliveryInfoId))
                {
                    shoppingCartInfo.OrderDeliveryInfoId = customer.DeliveryList[deliveryInfoId].DeliveryId;
                }
                else
                {
                    if (customer.DeliveryList != null && customer.DeliveryList.Count > 0)
                    {
                        foreach (CustomerDeliveryInfoModel delInfo in customer.DeliveryList.Values)
                        {
                            if (1 == 1
                                && string.IsNullOrEmpty(delInfo.DeliveryAddress) == false
                                // && delInfo.DeliveryRegionId != null
                                // && delInfo.DeliveryType != null
                                && string.IsNullOrEmpty(delInfo.Consignee) == false
                                && string.IsNullOrEmpty(delInfo.ConsigneePhone) == false)
                            {
                                shoppingCartInfo.OrderDeliveryInfoId = delInfo.DeliveryId;
                                break;
                            }
                            else
                            {
                                shoppingCartInfo.OrderDeliveryInfoId = "";
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(productListString))
            {
                return shoppingCartInfo;
            }

            if (productListString.EndsWith(","))
            {
                productListString = productListString.Substring(0, productListString.Length - 1);
            }

            string[] productItem = productListString.Split(',');

            string[] openAccountItem = (openAccountInfo == null) ? null : openAccountInfo.Split(',');

            if (productItem != null && productItem.Length > 0)
            {
                shoppingCartInfo.ProductList = new Dictionary<string, ShoppingCartItemInfo>();
                shoppingCartInfo.CommunicationPackageTotal = 0;

                ShoppingCartItemInfo itemInfo = null;

                for (int i = 0; i < productItem.Length; i++)
                {
                    itemInfo = new ShoppingCartItemInfo();
                    itemInfo.ItemId = productItem[i].Split('|')[0];
                    if (itemInfo.ItemId == "undefined")
                    {
                        continue;
                    }
                    itemInfo.Total = Convert.ToInt32(productItem[i].Split('|')[2]);
                    itemInfo.ItemType = productItem[i].Split('|')[1];
                    itemInfo.CommuniationPackageInfo = new Dictionary<string, CommuniationPackageInfo>();

                    if (itemInfo.ItemId == removeProductId)
                    {
                        ProductInfoDomainModel product = ProductInfoService.Instance.GetProductDomainInfoByProductId(itemInfo.ItemId, false);
                        if (product != null)
                        {
                            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(product.BasicInfo.CategoryId);
                            ProductCategoryGroupInfoModel thisGroupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupByCategoryId(catInfo.ProductCategoryId);
                            if (thisGroupInfo != null && thisGroupInfo.IsItemPrice == 0)
                            {
                                if (!ProductInfoService.Instance.ChangeProductSaleStatus(catInfo.ProductCategoryId, product.BasicInfo.ProductId, "已开放"))
                                {
                                    throw new Exception("解锁产品销售状态失败，请与管理员联系");
                                }
                            }
                        }
                        continue;
                    }

                    switch (itemInfo.ItemType.ToLower())
                    {
                        case "productcategory":
                            itemInfo.ProductCategory = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(itemInfo.ItemId);
                            ProductCategoryGroupInfoModel groupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupById(itemInfo.ProductCategory.GroupName);
                            if (groupInfo != null && groupInfo.ProductCategoryGroupId.ToUpper() == "30D52946-A29A-4127-A7FE-39A0D6206BF6")
                            {
                                itemInfo.IsCommunicationPackage = true;
                                shoppingCartInfo.hasCommunicationPackage = true;
                                shoppingCartInfo.CommunicationPackageTotal++;

                                CommuniationPackageInfo packageInfo = new CommuniationPackageInfo();
                                packageInfo.PackageInfoId = itemInfo.ProductCategory.ProductCategoryId;

                                if (openAccountItem != null)
                                {
                                    for (int j = 0; j < openAccountItem.Length; j++)
                                    {
                                        if (string.IsNullOrEmpty(openAccountItem[j]))
                                            continue;

                                        if (itemInfo.ItemId == openAccountItem[j].Split('|')[6])
                                        {
                                            packageInfo.BindedMainPhoneNumberId = openAccountItem[j].Split('|')[1];
                                            packageInfo.PhoneOwnerInfoId = openAccountItem[j].Split('|')[2];
                                            packageInfo.OpenningCityId = openAccountItem[j].Split('|')[5];

                                            if (openAccountItem[j].Split('|').Length == 7)
                                            {
                                                if (openAccountItem[j].Split('|')[4] == "0")
                                                {
                                                    packageInfo.CollectionInfoId = null;
                                                    packageInfo.IsCollections = false;
                                                }
                                                else
                                                {
                                                    packageInfo.CollectionInfoId = openAccountItem[j].Split('|')[3];
                                                    packageInfo.IsCollections = true;
                                                }
                                            }
                                            else
                                            {
                                                packageInfo.CollectionInfoId = null;
                                            }
                                            break;
                                        }
                                    }
                                }
                                itemInfo.CommuniationPackageInfo.Add(packageInfo.PackageInfoId, packageInfo);
                            }
                            break;

                        case "productitem":
                            itemInfo.ProductInfo = ProductInfoService.Instance.GetProductDomainInfoByProductId(itemInfo.ItemId, false);
                            ProductCategoryInfoModel catInfo = ProductCategoryInfoService.Instance.GetProductCategoryInfoById(itemInfo.ProductInfo.BasicInfo.CategoryId);
                            ProductCategoryGroupInfoModel thisGroupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupByCategoryId(catInfo.ProductCategoryId);
                            if (thisGroupInfo != null && thisGroupInfo.IsItemPrice == 0)
                            {
                                if (!ProductInfoService.Instance.ChangeProductSaleStatus(catInfo.ProductCategoryId, itemInfo.ProductInfo.BasicInfo.ProductId, "已锁定"))
                                {
                                    throw new Exception("锁定产品销售状态失败，请与管理员联系");
                                }
                            }

                            if (catInfo != null && catInfo.ProductCategoryId.ToUpper() == "DB487569-F636-43B7-9D31-259CD59774B7")
                            {
                                itemInfo.IsPhoneNumber = true;
                                shoppingCartInfo.hasPhoneNumber = true;
                            }
                            break;

                        case "salespackage":
                            itemInfo.SalesPackageInfo = SalesPackageInfoService.Instance.GetSalePackageDomainModelById(itemInfo.ItemId, false);
                            Dictionary<string, ProductCategoryInfoModel> commDict = GetCommunicationPackageList(itemInfo.SalesPackageInfo);
                            if (commDict != null && commDict.Count > 0)
                            {
                                itemInfo.IsCommunicationPackage = true;
                                shoppingCartInfo.hasCommunicationPackage = true;
                                shoppingCartInfo.CommunicationPackageTotal += commDict.Count;

                                foreach (ProductCategoryInfoModel commInfo in commDict.Values)
                                {
                                    CommuniationPackageInfo packageInfo = new CommuniationPackageInfo();
                                    packageInfo.PackageInfoId = commInfo.ProductCategoryId;

                                    if (openAccountItem != null)
                                    {
                                        for (int j = 0; j < openAccountItem.Length; j++)
                                        {
                                            if (string.IsNullOrEmpty(openAccountItem[j]))
                                                continue;

                                            if (commInfo.ProductCategoryId == openAccountItem[j].Split('|')[6])
                                            {
                                                packageInfo.BindedMainPhoneNumberId = openAccountItem[j].Split('|')[1];
                                                packageInfo.PhoneOwnerInfoId = openAccountItem[j].Split('|')[2];
                                                packageInfo.OpenningCityId = openAccountItem[j].Split('|')[5];

                                                if (openAccountItem[j].Split('|').Length == 7)
                                                {
                                                    if (openAccountItem[j].Split('|')[4] == "0")
                                                    {
                                                        packageInfo.CollectionInfoId = null;
                                                        packageInfo.IsCollections = false;
                                                    }
                                                    else
                                                    {
                                                        packageInfo.CollectionInfoId = openAccountItem[j].Split('|')[3];
                                                        packageInfo.IsCollections = true;
                                                    }
                                                }
                                                else
                                                {
                                                    packageInfo.CollectionInfoId = null;
                                                } break;
                                            }
                                        }
                                    }
                                    itemInfo.CommuniationPackageInfo.Add(packageInfo.PackageInfoId, packageInfo);
                                }
                            }
                            break;

                        default:
                            break;
                    }

                    if (shoppingCartInfo.ProductList.ContainsKey(itemInfo.ItemId))
                    {
                        shoppingCartInfo.ProductList[itemInfo.ItemId].Total += itemInfo.Total;
                    }
                    else
                    {
                        shoppingCartInfo.ProductList[itemInfo.ItemId] = itemInfo;
                    }
                }

                if (shoppingCartInfo != null && shoppingCartInfo.ProductList.Count > 0)
                {
                    shoppingCartInfo.ProductTotal = 0;
                    foreach (ShoppingCartItemInfo item in shoppingCartInfo.ProductList.Values)
                    {
                        shoppingCartInfo.ProductTotal += item.Total;
                        switch (item.ItemType.ToLower())
                        {
                            case "productcategory":
                                shoppingCartInfo.PriceTotal += (item.ProductCategory.ItemPrice == null) ? 0 : Convert.ToDecimal(item.ProductCategory.ItemPrice * item.Total);
                                break;

                            case "productitem":
                                shoppingCartInfo.PriceTotal += (item.ProductInfo.BasicInfo.ItemPrice == null) ? 0 : Convert.ToDecimal(item.ProductInfo.BasicInfo.ItemPrice * item.Total);
                                break;

                            case "salespackage":
                                shoppingCartInfo.PriceTotal += (item.SalesPackageInfo.BasicInfo.PriceTotal == null) ? 0 : Convert.ToDecimal(item.SalesPackageInfo.BasicInfo.PriceTotal * item.Total);                                
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return shoppingCartInfo;
        }

        public Dictionary<string, ProductCategoryInfoModel> GetCommunicationPackageList(SalePackageDomainModel packageInfo)
        {
            Dictionary<string, ProductCategoryInfoModel> result = new Dictionary<string,ProductCategoryInfoModel>();

            if (packageInfo != null && packageInfo.ProductCategoryList != null)
            {
                foreach (ProductSalesGroupInfoModel catInfo in packageInfo.ProductCategoryList.Values)
                {
                    ProductCategoryGroupInfoModel groupInfo = ProductCategoryGroupInfoService.Instance.GetProductCategoryGroupByCategoryId(catInfo.ProductCategoryId);
                    if (groupInfo != null && groupInfo.ProductCategoryGroupId.ToUpper() == "30D52946-A29A-4127-A7FE-39A0D6206BF6")
                    {
                        result.Add(catInfo.ProductCategoryId, ProductCategoryInfoService.Instance.GetProductCategoryInfoById(catInfo.ProductCategoryId));
                    }
                }
            }

            return result;
        }
    }
}
