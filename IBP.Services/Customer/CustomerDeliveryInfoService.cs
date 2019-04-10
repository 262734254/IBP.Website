/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-17
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

namespace IBP.Services
{
	/// <summary>
	/// CustomerDeliveryInfo业务逻辑类
	/// </summary>
	public partial class CustomerDeliveryInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool CreateCustomerDeliveryInfo(CustomerDeliveryInfoModel deliveryInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (deliveryInfo == null)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            if (string.IsNullOrEmpty(deliveryInfo.Consignee))
            {
                message = "收货人姓名不能为空，请检查输入";
                return false;
            }

            if (string.IsNullOrEmpty(deliveryInfo.ConsigneePhone))
            {
                message = "收货人电话不能为空，请检查输入";
                return false;
            }

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(deliveryInfo.CustomerId, false);
            if(customer == null)
            {
                message = "参数错误，不存在的客户ID,请与管理员联系";
                return false;
            }

            deliveryInfo.DeliveryId = GetGuid();            
            deliveryInfo.StatusCode = 0;

            try
            {
                BeginTransaction();

                if (Create(deliveryInfo) == 1)
                {
                    CommitTransaction();
                    message = "成功创建客户配送信息";
                    result = true;
                    CustomerInfoService.Instance.GetCustomerDomainModelById(deliveryInfo.CustomerId, true);
                }
                else
                {
                    RollbackTransaction();
                    message = "创建客户配送信息失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建客户配送信息异常", ex);
                throw ex;
            }

            return result;
        }
	}
}

