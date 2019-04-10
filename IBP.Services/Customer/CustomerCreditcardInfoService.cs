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
	/// CustomerCreditcardInfo业务逻辑类
	/// </summary>
	public partial class CustomerCreditcardInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool UpdateCreditcardInfo(CustomerCreditcardInfoModel creditInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (creditInfo == null)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(creditInfo.CustomerId, false);
            if (customer == null)
            {
                message = "参数错误，不存在的客户ID,请与管理员联系";
                return false;
            }

            CustomerSecurityInfoDomainModel securityInfo = CustomerInfoService.Instance.GetCustomerSecurityInfo(creditInfo.IvrDataId, false);
            if (securityInfo == null)
            {
                message = "未能从IVR系统获取客户输入的敏感信息,请与管理员联系";
                return false;
            }

            creditInfo.CreditcardNumber = securityInfo.CreditCardNumber;
            creditInfo.IdcardNumber = securityInfo.IdCardNumber;
            creditInfo.Period = securityInfo.PeriodCode;
            creditInfo.SecurityCode = securityInfo.SecurityCode;

            if (creditInfo.CreditcardNumber.Length > 6)
            {
                BankcardTypeInfoModel bankCardTypeModel = BankcardTypeInfoService.Instance.GetBankCardInfoByBinCode(creditInfo.CreditcardNumber.Substring(0, 6));
                if (bankCardTypeModel != null)
                {
                    creditInfo.CardType = bankCardTypeModel.CardType;
                    creditInfo.CardLevel = bankCardTypeModel.CardLevel;
                    creditInfo.CardBrand = bankCardTypeModel.CardBrand;
                    creditInfo.CanbeStage = (bankCardTypeModel.BankcardEnumValue == "FEB39D81-26EC-4A20-97F2-F148FDC87AFD") ? 0 : 1;
                }
            }

            if (string.IsNullOrEmpty(creditInfo.CreditcardNumber))
            {
                message = "未能从IVR系统获取客户输入的敏感信息,请检查输入";
                return false;
            }

            try
            {
                BeginTransaction();

                if (Update(creditInfo) == 1)
                {
                    CommitTransaction();
                    message = "成功更新客户持卡信息";
                    result = true;
                    CustomerInfoService.Instance.GetCustomerDomainModelById(creditInfo.CustomerId, true);
                }
                else
                {
                    RollbackTransaction();
                    message = "更新客户持卡信息失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新客户持卡信息异常", ex);
                throw ex;
            }

            return result;
        }

        public bool CreateCreditcardInfo(CustomerCreditcardInfoModel creditInfo, string securityCode, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (creditInfo == null || string.IsNullOrEmpty(securityCode))
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(creditInfo.CustomerId, false);
            if (customer == null)
            {
                message = "参数错误，不存在的客户ID,请与管理员联系";
                return false;
            }

            CustomerSecurityInfoDomainModel securityInfo = CustomerInfoService.Instance.GetCustomerSecurityInfo(securityCode, false);
            if (securityInfo == null)
            {
                message = "未能从IVR系统获取客户输入的敏感信息,请与管理员联系";
                return false;
            }

            creditInfo.CreditcardId = GetGuid();
            creditInfo.StatusCode = 0;
            creditInfo.CreditcardNumber = securityInfo.CreditCardNumber;
            creditInfo.IdcardNumber = securityInfo.IdCardNumber;
            creditInfo.Period = securityInfo.PeriodCode;
            creditInfo.SecurityCode = securityInfo.SecurityCode;
            creditInfo.IvrDataId = securityCode;
            creditInfo.Status = 0;

            switch (creditInfo.InfoType)
            {
                case "getcollection":
                    creditInfo.InfoType = "订单托收信息";
                    break;

                case "getidcard":
                    creditInfo.InfoType = "套餐机主信息";
                    break;

                default:
                    creditInfo.InfoType = "客户银卡信息";
                    break;
            }

            if (creditInfo.CreditcardNumber.Length > 6)
            {
                BankcardTypeInfoModel bankCardTypeModel = BankcardTypeInfoService.Instance.GetBankCardInfoByBinCode(creditInfo.CreditcardNumber.Substring(0, 6));
                if (bankCardTypeModel != null)
                {
                    creditInfo.CardType = bankCardTypeModel.CardType;
                    creditInfo.CardLevel = bankCardTypeModel.CardLevel;
                    creditInfo.CardBrand = bankCardTypeModel.CardBrand;
                    creditInfo.CanbeStage = (bankCardTypeModel.BankcardEnumValue == "FEB39D81-26EC-4A20-97F2-F148FDC87AFD") ? 0 : 1;
                }
            }

            if (string.IsNullOrEmpty(creditInfo.CreditcardNumber) && string.IsNullOrEmpty(creditInfo.IdcardNumber))
            {
                message = "未能从IVR系统获取客户输入的敏感信息,请检查输入";
                return false;
            }

            try
            {
                BeginTransaction();

                if (Create(creditInfo) == 1)
                {
                    CommitTransaction();
                    message = "成功创建客户持卡信息";
                    result = true;
                    CustomerInfoService.Instance.GetCustomerDomainModelById(creditInfo.CustomerId, true);
                }
                else
                {
                    RollbackTransaction();
                    message = "创建客户持卡信息失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建客户持卡信息异常", ex);
                throw ex;
            }

            return result;
        }
	}
}

