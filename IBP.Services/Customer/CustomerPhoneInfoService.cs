/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-4-11
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
	/// CustomerPhoneInfo业务逻辑类
	/// </summary>
	public partial class CustomerPhoneInfoService : BaseService
	{
		// 在此添加你的代码...

        public string GetOutCallNumber(CustomerPhoneInfoModel phoneInfo)
        {
            if (phoneInfo == null)
            {
                return "";
            }

            if (Framework.Utilities.RegexUtil.IsMobilePhone(phoneInfo.PhoneNumber))
            {
                return (phoneInfo.FromCityId == "472") ? "9" + phoneInfo.PhoneNumber : "90" + phoneInfo.PhoneNumber;
            }
            else
            {
                return (phoneInfo.FromCityId == "472") ? "9" + phoneInfo.PhoneNumber : "9" + phoneInfo.PhoneNumber;
            }
        }

        public bool CreateCustomerPhoneInfo(string phoneNumber, string customerId, out string message)
        {
            message = "操作失败，请与管理员联系";

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(customerId))
            {
                message = "参数错误，联系号码或客户ID不能为空";
                return false;
            }

            CustomerDomainModel customerInfo = CustomerInfoService.Instance.GetCustomerDomainModelById(customerId, false);
            if (customerInfo == null)
            {
                message = "操作失败，不存在的客户ID";
                return false;
            }

            CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
            phoneInfo.PhoneNumber = phoneNumber;
            phoneInfo.CustomerId = customerId;

            return CreateCustomerPhoneInfo(phoneInfo, out message);
        }



        public bool UpdateCustomerPhoneInfo(string oldphoneNumber ,string phoneNumber, string customerId, out string message)
        {
            bool result = false;
            if (string.IsNullOrEmpty(oldphoneNumber))
            {
                if (CreateCustomerPhoneInfo(phoneNumber, customerId, out message) == true)
                {
                    message = "成功更新客户联系号码";
                    result = true;
                    return result;
                }
                else
                {
                    message = "更新客户联系号码参数错误，请与管理员联系";
                    return false;
                }
            }
            message = "操作失败，请与管理员联系";
           
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(customerId))
            {
                message = "参数错误，联系号码或客户ID不能为空";
                return false;
            }

            CustomerDomainModel customerInfo = CustomerInfoService.Instance.GetCustomerDomainModelById(customerId, false);
            if (customerInfo == null)
            {
                message = "操作失败，不存在的客户ID";
                return false;
            }

            CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
            phoneInfo.PhoneNumber = phoneNumber;
            phoneInfo.CustomerId = customerId;

            string checkExistsSQL = "SELECT * FROM customer_phone_info WHERE phone_number = $oldphoneNumber$ AND customer_id = $CustomerId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("CustomerId", phoneInfo.CustomerId);
            pc.Add("oldphoneNumber", oldphoneNumber);

            string  phoneid = ExecuteScalar(checkExistsSQL, pc).ToString() ;
            if (string.IsNullOrEmpty(phoneid))
            {
                message = "更新客户联系号码参数错误，请与管理员联系";
                return false;
            }



            phoneInfo.PhoneId = phoneid;
            phoneInfo.CallStatus = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码状态", false).GetCustomDataValueDomainByDataValue("正常").ValueId;
            phoneInfo.PhoneType = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码类型", false).GetCustomDataValueDomainByDataValue("其他电话").ValueId;
            phoneInfo.Status = 0;
            PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(phoneInfo.PhoneNumber, false);
            if (loc != null)
            {
                phoneInfo.FromCityId = loc.ChinaId.ToString();
                phoneInfo.FromCityName = loc.City;
            }
            else
            {
                message = "填写号码无法判断归属地，请检查。如果是固定电话，请加上区号";
                //return false;
            }

            if (Update(phoneInfo) == 1)
            {
                message = "成功更新客户联系号码";
                return true;
            }

            return result;
        }

        public bool CreateCustomerPhoneInfo(CustomerPhoneInfoModel phoneInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";
            if (phoneInfo == null || string.IsNullOrEmpty(phoneInfo.CustomerId) || string.IsNullOrEmpty(phoneInfo.PhoneNumber))
            {
                message = "添加客户联系号码参数错误，请与管理员联系";
                return false;
            }

            string checkExistsSQL = "SELECT COUNT(1) FROM customer_phone_info WHERE phone_number = $phoneNumber$ AND customer_id = $CustomerId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("CustomerId", phoneInfo.CustomerId);
            pc.Add("phoneNumber", phoneInfo.PhoneNumber);

            bool exists = ExecuteScalar(checkExistsSQL, pc).ToString() != "0";

            if (exists)
            {
                message = "操作中止，该联系号码已经存在";
                return true;
            }

            phoneInfo.PhoneId = GetGuid();
            phoneInfo.CallStatus = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码状态", false).GetCustomDataValueDomainByDataValue("正常").ValueId;
            phoneInfo.PhoneType = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码类型", false).GetCustomDataValueDomainByDataValue("其他电话").ValueId;
            phoneInfo.Status = 0;
            PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(phoneInfo.PhoneNumber, false);
            if (loc != null)
            {
                phoneInfo.FromCityId = loc.ChinaId.ToString();
                phoneInfo.FromCityName = loc.City;
            }
            else
            {
                message = "填写号码无法判断归属地，请检查。如果是固定电话，请加上区号";
                //return false;
            }

            if (Create(phoneInfo) == 1)
            {
                message = "成功添加客户联系号码";
                return true;
            }

            return result;
        }
	}
}

