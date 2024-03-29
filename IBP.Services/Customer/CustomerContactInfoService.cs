/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-14
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
	/// CustomerContactInfo业务逻辑类
	/// </summary>
	public partial class CustomerContactInfoService : BaseService
	{
		// 在此添加你的代码...

        public bool CreateContactByPhoneNumber(string inComePhoneNumber, CustomerContactInfoModel contactInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            string sql = "SELECT TOP 1 customer_id FROM customer_contact_info WHERE customer_phone = $customer_phone$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_phone", inComePhoneNumber);

            object customerId = ExecuteScalar(sql, pc);
            if (customerId != null)
            {
                contactInfo.ContactId = GetGuid();
                contactInfo.CustomerId = customerId.ToString();
                contactInfo.CustomerPhone = inComePhoneNumber;
                PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(contactInfo.CustomerPhone, false);
                if (loc != null)
                {
                    contactInfo.FromCityId = loc.ChinaId;
                    contactInfo.FromCityName = loc.City;
                }


                if (Create(contactInfo) == 1)
                {
                    result = true;
                    message = "成功根据来电号码创建联系记录";
                    CustomerInfoService.Instance.GetCustomerDomainModelById(contactInfo.CustomerId, true);
                }
            }

            return result;
        }

        public bool CreateContactInfo(CustomerContactInfoModel contactInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            CustomerDomainModel customer = CustomerInfoService.Instance.GetCustomerDomainModelById(contactInfo.CustomerId, false);
            if (customer == null)
            {
                message = "操作失败，目标客户不存在";
                return false;
            }

            if (contactInfo != null)
            {
                contactInfo.ContactId = GetGuid();
                PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(contactInfo.CustomerPhone, false);
                if (loc != null)
                {
                    contactInfo.FromCityId = loc.ChinaId;
                    contactInfo.FromCityName = loc.City;
                }

                try
                {
                    BeginTransaction();
                    if (Create(contactInfo) == 1)
                    {                    
                        if (contactInfo.CustomerPhone != "")
                        {
                            CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
                            phoneInfo.CustomerId = contactInfo.CustomerId;
                            phoneInfo.PhoneNumber = contactInfo.CustomerPhone;
                            if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(phoneInfo, out message))
                            {
                                RollbackTransaction();
                                message = "添加客户联系号码失败";
                                return false;
                            }
                          
                        }
                        CommitTransaction();
                        result = true;
                        message = "成功创建客户联系记录";
                        CustomerInfoService.Instance.GetCustomerDomainModelById(contactInfo.CustomerId, true);
                    }
                    
                }
                catch (Exception ex)
                {
                                     
                RollbackTransaction();
                LogUtil.Error("建客户联系记录异常", ex);
                throw ex;
                }
            }

            return result;
        }

        public string GetOutCallNumber(CustomerContactInfoModel contactInfo)
        {
            if (contactInfo == null)
            {
                return "";
            }

            if (Framework.Utilities.RegexUtil.IsMobilePhone(contactInfo.CustomerPhone))
            {
                return (contactInfo.FromCityId == 472) ? "9" + contactInfo.CustomerPhone : "90" + contactInfo.CustomerPhone;
            }
            else
            {
                return (contactInfo.FromCityId == 472) ? "9" + contactInfo.CustomerPhone : "9" + contactInfo.CustomerPhone;
            }
        }
	}
}

