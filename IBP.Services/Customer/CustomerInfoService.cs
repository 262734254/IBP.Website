using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBP.Models;
using Framework.Utilities;
using Framework.Common;
using IBP.Common;
using System.Data;

namespace IBP.Services
{
    /// <summary>
    /// 客户信息业务类。
    /// </summary>
    public class CustomerInfoService : BaseService
    {
        #region 单键实例

        		// 实例
		private static CustomerInfoService _instance = new CustomerInfoService();

		/// <summary>
		/// 构造函数
		/// </summary>
        private CustomerInfoService()
		{
		}

		/// <summary>
		/// 类唯一实例
		/// </summary>
        public static CustomerInfoService Instance
		{
			get { return _instance; }
		}

        #endregion

        #region 处理客户来电


        /// <summary>
        /// 创建未知来电客户档案。
        /// </summary>
        /// <param name="inComeCallNumber"></param>
        /// <returns></returns>
        public bool CreateFirstInComeCallInfo(string inComeCallNumber, string calledNumber, out string customerId)
        {
            bool result = false;

            #region 客户基本信息

            CustomerBasicInfoModel basicInfo = new CustomerBasicInfoModel();
            basicInfo.CustomerId = GetGuid();
            customerId = basicInfo.CustomerId;
            basicInfo.CustomerCode = "C" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            basicInfo.OtherPhone = inComeCallNumber;
            basicInfo.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            basicInfo.CommunicationConsumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            basicInfo.PreferredPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            basicInfo.UsingPhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            basicInfo.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false).GetCustomDataValueDomainByDataValue("未知").ValueId;
            basicInfo.Level = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户等级", false).GetCustomDataValueDomainByDataValue("普通").ValueId;
            basicInfo.UsingSmartphone = 3;
            basicInfo.Sex = 2;

            if (UserInfoService.Instance.CheckInProjectGroup77())
            {
                basicInfo.SalesFrom = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户来源",false).GetCustomDataValueDomainByDataValue("40077项目").ValueId;
            }

            PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(inComeCallNumber, false);
            if (loc != null)
            {
                basicInfo.ChinaId = loc.ChinaId;
                basicInfo.ComeFrom = loc.City;
            }

            #endregion

            try
            {
                BeginTransaction();

                // 创建客户基本档案
                if (CustomerBasicInfoService.Instance.Create(basicInfo) > 0)
                {
                    #region 创建联系号码

                    CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
                    phoneInfo.PhoneId = GetGuid();
                    phoneInfo.CallStatus = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码状态", false).GetCustomDataValueDomainByDataValue("正常").ValueId;
                    phoneInfo.CustomerId = basicInfo.CustomerId;
                    if (loc != null)
                    {
                        phoneInfo.FromCityId = loc.ChinaId.ToString();
                        phoneInfo.FromCityName = loc.City;
                    }
                    phoneInfo.PhoneNumber = inComeCallNumber;
                    phoneInfo.PhoneType = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("号码类型", false).GetCustomDataValueDomainByDataValue("其他电话").ValueId;
                    phoneInfo.Status = 0;

                    // 创建客户联系记录
                    if (CustomerPhoneInfoService.Instance.Create(phoneInfo) == 0)
                    {
                        RollbackTransaction();
                        LogUtil.Error("创建未知来电客户联系号码记录失败");
                        return false;
                    }

                    #endregion

                    #region 创建联系记录

                    CustomerContactInfoModel contactInfo = new CustomerContactInfoModel();
                    contactInfo.ContactId = GetGuid();
                    contactInfo.CustomerId = basicInfo.CustomerId;
                    contactInfo.CustomerPhone = inComeCallNumber;
                    contactInfo.CalledNumber = calledNumber;
                    contactInfo.Purpose = "首次来电";
                    contactInfo.Results = "首次来电";

                    // 来电0，去电1
                    contactInfo.Directions = 0;
                    contactInfo.Description = "首次来电，系统自动创建客户档案及联系记录";
                    contactInfo.Status = 0;
                    if (loc != null)
                    {
                        contactInfo.FromCityId = loc.ChinaId;
                        contactInfo.FromCityName = loc.City;
                    }

                    // 创建客户联系记录
                    if (CustomerContactInfoService.Instance.Create(contactInfo) == 0)
                    {
                        RollbackTransaction();
                        LogUtil.Error("创建未知来电客户联系记录失败");
                        return false;
                    }

                    #endregion

                    CommitTransaction();
                    CustomerInfoService.Instance.GetCustomerDomainModelById(customerId, true);
                    result = true;

                }
                else
                {
                    RollbackTransaction();
                    LogUtil.Error("创建未知来电客户基本信息档案失败");
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建未知来电客户档案异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 检查指定电话号码是否存在于联系记录。
        /// </summary>
        /// <param name="callNumber"></param>
        /// <returns></returns>
        public string CheckExistCallNumber(string callNumber)
        {

//            string sql = @"
//SELECT 
//	top 1 customer_id
//  FROM 
//	customer_contact_info
//  WHERE
//	customer_phone = $customer_phone$
//";

            string sql2 = "SELECT top 1 customer_id,* FROM customer_phone_info WHERE phone_number = $customer_phone$";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_phone", callNumber);

            object customerId = ExecuteScalar(sql2, pc);

            return (customerId == null) ? null : customerId.ToString();
        }


        /// <summary>
        /// 处理来电操作，如果是新来电则创建客户档案。
        /// </summary>
        /// <param name="inComeCallNumber"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public void ProcessInComeCall(string inComeCallNumber, string calledNumber, out string customerId, out bool isFirstIncomeCall)
        {
            customerId = CheckExistCallNumber(inComeCallNumber);
            string message = "";
            isFirstIncomeCall = false;

            if (customerId == null)
            {
                isFirstIncomeCall = true;
                CreateFirstInComeCallInfo(inComeCallNumber,calledNumber, out customerId);
            }
            else
            {
                

                CustomerContactInfoModel contactInfo = new CustomerContactInfoModel();
                contactInfo.Description = string.Format("{0} 客户来电咨询", DateTime.Now);
                contactInfo.Directions = 0;
                contactInfo.Status = 0;
                contactInfo.CustomerPhone = inComeCallNumber;
                contactInfo.CalledNumber = calledNumber;
                contactInfo.CustomerId = customerId;
                //contactInfo.Purpose = "来电咨询";
                //contactInfo.Results = "来电咨询";
                

                CustomerContactInfoService.Instance.CreateContactByPhoneNumber(inComeCallNumber, contactInfo, out message);
                customerId = contactInfo.CustomerId;
            }
        }

        /// <summary>
        /// 更新首次来电信息。
        /// </summary>
        /// <param name="customerBasicInfo"></param>
        /// <param name="customerMemo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateFirstInComeCall(CustomerBasicInfoModel customerBasicInfo, string customerMemo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (customerBasicInfo == null)
            {
                message = "输入参数异常，请检查输入";
                return false;
            }

            customerBasicInfo.CreatedBy = SessionUtil.Current.UserId;

            try
            {
                BeginTransaction();

                if (CustomerBasicInfoService.Instance.Update(customerBasicInfo) == 1)
                {
                    CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
                    memoInfo.CustomerId = customerBasicInfo.CustomerId;
                    memoInfo.Memo = customerMemo;
                    memoInfo.MemoId = GetGuid();
                    memoInfo.Status = 0;

                    if (CustomerMemoInfoService.Instance.Create(memoInfo) == 1)
                    {
                        CustomerInfoService.Instance.GetCustomerDomainModelById(customerBasicInfo.CustomerId, true);
                        CommitTransaction();
                        message = "操作成功";
                        result = true;
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "处理首次来电创建客户备注信息失败";
                        result = false;
                    }
                }
                else
                {
                    RollbackTransaction();
                    message = "更新客户基本信息失败";
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("更新首次来电信息异常", ex);
                throw ex;
            }

            return result;
        }

        #endregion

        #region 获取客户信息领域模型 

        /// <summary>
        /// 根据客户ID获取客户领域模型。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public CustomerDomainModel GetCustomerDomainModelById(string customerId, bool clear)
        {
            if (string.IsNullOrEmpty(customerId))
                return null;

            string cacheKey = CacheKey.CUSTOMER_DOMAINMODEL.GetKeyDefine(customerId);

            CustomerDomainModel model = CacheUtil.Get<CustomerDomainModel>(cacheKey);
            if (model == null || clear)
            {
                model = GetCustomerDomainModelByIdFromDatabase(customerId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }

        private CustomerDomainModel GetCustomerDomainModelByIdFromDatabase(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return null;

            CustomerDomainModel model = null;
            CustomerBasicInfoModel basicInfo = CustomerBasicInfoService.Instance.Retrieve(customerId);
            if (basicInfo == null)
                return null;

            model = new CustomerDomainModel();
            model.BasicInfo = basicInfo;
            model.MemoList = GetCustomerMemoListByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.ContactList = GetCustomerContactListByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.WorkorderList = GetCustomerWorkorderListByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.SalesOrderList = GetCustomerSalesorderListByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.CreditCardList = GetCustomerCreditcardByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.DeliveryList = GetCustomerDeliveryByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.PhoneNumberList = GetCustomerPhoneListByIdFromDatabase(basicInfo.CustomerId, "created_on", "desc");
            model.AttributeList = GetCustomerAttributeListFromDatabase(basicInfo.CustomerId);

            model.MemoList = (model.MemoList == null) ? new Dictionary<string, CustomerMemoInfoModel>() : model.MemoList;
            model.ContactList = (model.ContactList == null) ? new Dictionary<string, CustomerContactInfoModel>() : model.ContactList;
            model.WorkorderList = (model.WorkorderList == null) ? new List<string>() : model.WorkorderList;
            model.SalesOrderList = (model.SalesOrderList == null) ? new List<string>() : model.SalesOrderList;
            model.CreditCardList = (model.CreditCardList == null) ? new Dictionary<string, CustomerCreditcardInfoModel>() : model.CreditCardList;
            model.DeliveryList = (model.DeliveryList == null) ? new Dictionary<string, CustomerDeliveryInfoModel>() : model.DeliveryList;
            model.PhoneNumberList = (model.PhoneNumberList == null) ? new Dictionary<string, CustomerPhoneInfoModel>() : model.PhoneNumberList;
            model.AttributeList = (model.AttributeList == null) ? new Dictionary<string, Dictionary<string, string>>() : model.AttributeList;

            return model;
        }

        /// <summary>
        /// 从数据库获取客户联系号码列表信息。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected Dictionary<string, CustomerPhoneInfoModel> GetCustomerPhoneListByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            Dictionary<string, CustomerPhoneInfoModel> dict = new Dictionary<string, CustomerPhoneInfoModel>();
            string getWorkorderSQL = "SELECT * FROM customer_phone_info WHERE customer_id = $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getWorkorderSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            dict = ModelConvertFrom<CustomerPhoneInfoModel>(dt, "phone_number");

            return dict;
        }


        /// <summary>
        /// 从数据库获取客户工单信息。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected List<string> GetCustomerWorkorderListByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            List<string> list = new List<string>();
            string getWorkorderSQL = "SELECT work_order_id FROM workorder_info WHERE rel_customer_id = $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getWorkorderSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            list = ModelConvertFrom(dt);

            return list;
        }

        /// <summary>
        /// 从数据库中获取客户销售订单记录信息。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected List<string> GetCustomerSalesorderListByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            List<string> list = new List<string>();
            string getWorkorderSQL = "SELECT salesorder_id FROM salesorder_basic_info WHERE customer_id = $customer_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getWorkorderSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            list = ModelConvertFrom(dt);

            return list;
        }

        /// <summary>
        /// 从数据库获取客户备注信息。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected Dictionary<string, CustomerMemoInfoModel> GetCustomerMemoListByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            Dictionary<string, CustomerMemoInfoModel> dict = new Dictionary<string,CustomerMemoInfoModel>();

            string getMemoSQL = @"SELECT * FROM customer_memo_info WHERE customer_id = $customer_id$ ";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getMemoSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            dict = ModelConvertFrom<CustomerMemoInfoModel>(dt, "memo_id");

            return dict;
        }

        /// <summary>
        /// 从数据库获取客户联系记录信息。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected Dictionary<string, CustomerContactInfoModel> GetCustomerContactListByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            Dictionary<string, CustomerContactInfoModel> dict = new Dictionary<string, CustomerContactInfoModel>();

            string getMemoSQL = @"SELECT * FROM customer_contact_info WHERE customer_id = $customer_id$ ";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getMemoSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            dict = ModelConvertFrom<CustomerContactInfoModel>(dt, "contact_id");

            return dict;
        }

        /// <summary>
        /// 从数据库获取客户信用卡信息列表。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected Dictionary<string, CustomerCreditcardInfoModel> GetCustomerCreditcardByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            Dictionary<string, CustomerCreditcardInfoModel> dict = new Dictionary<string, CustomerCreditcardInfoModel>();

            string getMemoSQL = @"SELECT * FROM customer_creditcard_info WHERE customer_id = $customer_id$ ";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getMemoSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            dict = ModelConvertFrom<CustomerCreditcardInfoModel>(dt, "creditcard_id");

            return dict;
        }

        /// <summary>
        /// 从数据库获取客户配送地址信息列表。
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orderByField"></param>
        /// <param name="orderDicection"></param>
        /// <returns></returns>
        protected Dictionary<string, CustomerDeliveryInfoModel> GetCustomerDeliveryByIdFromDatabase(string customerId, string orderByField, string orderDicection)
        {
            Dictionary<string, CustomerDeliveryInfoModel> dict = new Dictionary<string, CustomerDeliveryInfoModel>();

            string getMemoSQL = @"SELECT * FROM customer_delivery_info WHERE customer_id = $customer_id$ ";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("customer_id", customerId);

            DataTable dt = ExecuteDataTable(getMemoSQL, pc, OrderByCollection.Create(orderByField, orderDicection));
            dict = ModelConvertFrom<CustomerDeliveryInfoModel>(dt, "delivery_id");

            return dict;
        }

        protected Dictionary<string, Dictionary<string, string>> GetCustomerAttributeListFromDatabase(string customerId)
        {
            return null;
        }

        #endregion

        #region 查询客户信息

        public List<string> GetBatchCustomerIdList(Dictionary<string, QueryItemDomainModel> queryCollection)
        {

          
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.Append(@"
FROM
    customer_basic_info  
LEFT JOIN  
    user_info create_userinfo
ON 
    create_userinfo.user_id=customer_basic_info.created_by 
WHERE 1=1 ");
            ParameterCollection pc = new ParameterCollection();
            int count = 0;

            #region 构造查询条件

            foreach (QueryItemDomainModel item in queryCollection.Values)
            {

                #region 针对电话号码做处理

                if (item.FieldType == "phone_number")
                {
                    pc.Clear();
                    pc.Add("PhoneNumber", item.SearchValue);

                    StringBuilder Builder = new StringBuilder();

                    Builder.Append(@"
                    FROM
                    customer_phone_info  
                    WHERE 1=1 AND phone_number = $PhoneNumber$ ");

                    DataTable customer_phonetable = ExecuteDataTable("SELECT customer_id " + Builder.ToString(), pc);
                    if (customer_phonetable != null && customer_phonetable.Rows.Count > 0)
                    {
                        result = new List<string>();
                        for (int i = 0; i < customer_phonetable.Rows.Count; i++)
                        {
                            result.Add(customer_phonetable.Rows[i][0].ToString());
                        }
                    }

                    return result;

                }
                #endregion

                #region 针对用户ID做处理
                if (item.FieldType == "create_customer_id")
                {
                    item.FieldType = "create_userinfo.work_id";
                    item.SearchValue = "WORKID_" + item.SearchValue;
                }

                #endregion
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


            DataTable dt = ExecuteDataTable("SELECT customer_id " + sqlBuilder.ToString(), pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i][0].ToString());
                }
            }

            return result;
        }

        #endregion

        #region 查询客户信息

        public List<string> GetCustomerIdList(Dictionary<string,QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {

            total = 0;
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();
        
            sqlBuilder.Append(@"
FROM
    customer_basic_info  
LEFT JOIN  
    user_info create_userinfo
ON 
    create_userinfo.user_id=customer_basic_info.created_by 
WHERE 1=1 ");
            ParameterCollection pc = new ParameterCollection();
            int count = 0;

            #region 构造查询条件

            if (queryCollection.ContainsKey("call_status") && queryCollection["call_status"].SearchValue != "0")
            {
                queryCollection.Remove("call_status");
            }

            foreach (QueryItemDomainModel item in queryCollection.Values)
            {

                #region 针对电话号码做处理

                if (item.FieldType == "customer_basic_info.phone_number")
                {
                    pc.Clear();
                    pc.Add("PhoneNumber", "%" + item.SearchValue + "%");

                    StringBuilder Builder = new StringBuilder();

                    Builder.Append(@"
                    FROM
                    customer_phone_info  
                    WHERE 1=1 AND phone_number LIKE $PhoneNumber$ ");

                    total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + Builder.ToString(), pc));
                    DataTable customer_phonetable = ExecuteDataTable("SELECT customer_id " + Builder.ToString(), pc, pageIndex, pageSize);
                    if (customer_phonetable != null && customer_phonetable.Rows.Count > 0)
                    {
                        result = new List<string>();
                        for (int i = 0; i < customer_phonetable.Rows.Count; i++)
                        {
                            result.Add(customer_phonetable.Rows[i][0].ToString());
                        }
                    }

                    return result;

                } 
                #endregion

                #region 针对用户ID做处理
                if (item.FieldType == "create_customer_id")
                {
                    item.FieldType = "create_userinfo.work_id";
                    item.SearchValue = "WORKID_" + item.SearchValue;
                }

                #endregion
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

            total = Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) " + sqlBuilder.ToString(),pc));

            DataTable dt = ExecuteDataTable("SELECT customer_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create("customer_basic_info." + orderField, orderDirection));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i][0].ToString());
                }
            }

            return result;
        }

        #endregion

        #region 修改客户信息

        public bool ChangeCustomerCallBackInfo(string customerId, string SetOrCancel, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (SetOrCancel == "0" || SetOrCancel == "1")
            {
                CustomerDomainModel customer = GetCustomerDomainModelById(customerId, false);
                if (customer == null)
                {
                    message = "操作失败，不存在的客户信息。";
                    return false;
                }

                UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);
                if (user == null)
                {
                    message = "操作失败，当前登录用户信息丢失。";
                    return false;
                }

                customer.BasicInfo.CallStatus = Convert.ToInt32(SetOrCancel);

                CustomerMemoInfoModel memo = new CustomerMemoInfoModel();
                memo.CustomerId = customer.BasicInfo.CustomerId;
                memo.MemoId = GetGuid();
                memo.Memo = string.Format("【{0}】{1}当前客户CallBack任务。", user.NameAndWorkId, (SetOrCancel == "0") ? "添加" : "取消");
                memo.Status = 0;

                try
                {
                    BeginTransaction();

                    if (CustomerBasicInfoService.Instance.Update(customer.BasicInfo) != 1)
                    {
                        message = "操作失败，更新客户信息失败。";
                        return false;
                    }

                    if (CustomerMemoInfoService.Instance.Create(memo) != 1)
                    {
                        message = "操作失败，添加客户备注信息失败。";
                        return false;
                    }

                    CommitTransaction();
                    GetCustomerDomainModelById(customer.BasicInfo.CustomerId, true);
                    message = "操作成功";
                    result = true;
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error(ex.Message, ex);
                    message = "操作异常，" + ex.Message;
                }
            }
            else
            {
                message = "操作失败，传递参数异常，请与管理员联系。";
                return false;
            }

            return result;
        }

        /// <summary>
        /// 更新客户基本信息。
        /// </summary>
        /// <param name="basicInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateCustomerBasicInfo(CustomerBasicInfoModel basicInfo, out string message)
        {
            bool result = false;
            bool isOtherPhone = false;
            bool isMobilePhone = false;
            bool isHomePhone = false;
            message = "操作失败，请与管理员联系";

            CustomerDomainModel customer = GetCustomerDomainModelById(basicInfo.CustomerId, false);
            if (customer == null)
            {
                message = "不存在的客户信息ID，请与管理员联系";
                return false;
            }

            #region 创建客户信息修改备注

            CustomDataDomainModel CarriersList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false);
            CustomDataDomainModel Consumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false);
            CustomDataDomainModel CustomerLevel = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户等级", false);
            CustomDataDomainModel MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false);
            CustomDataDomainModel PhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false);
            CustomDataDomainModel CustomerComeFrom = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户来源", false);

            StringBuilder updateMmeo = new StringBuilder();
            updateMmeo.AppendFormat("客户【{0}】信息修改：", customer.BasicInfo.CustomerName);
            bool needApproval = false;
            string approvalTaskId = GetGuid();

            if (basicInfo.Birthday != null && basicInfo.Birthday != customer.BasicInfo.Birthday)
            {
                updateMmeo.AppendFormat("生日由【{0}】改为【{1}】；", customer.BasicInfo.Birthday, basicInfo.Birthday);
            }

            if (basicInfo.Carriers != null && basicInfo.Carriers != customer.BasicInfo.Carriers)
            {
                updateMmeo.AppendFormat("运营商由【{0}】改为【{1}】；", 
                    CarriersList.ValueList[customer.BasicInfo.Carriers].DataValue, 
                    CarriersList.ValueList[basicInfo.Carriers].DataValue);
            }

            if (basicInfo.ComeFrom != null && basicInfo.ComeFrom != customer.BasicInfo.ComeFrom)
            {
                updateMmeo.AppendFormat("归属地由【{0}】改为【{1}】；", customer.BasicInfo.ComeFrom, basicInfo.ComeFrom);
            }

            if (basicInfo.CommunicationConsumer != null && basicInfo.CommunicationConsumer != customer.BasicInfo.CommunicationConsumer)
            {
                updateMmeo.AppendFormat("通讯消费由【{0}】改为【{1}】；",
                    Consumer.ValueList[customer.BasicInfo.CommunicationConsumer].DataValue,
                    Consumer.ValueList[basicInfo.CommunicationConsumer].DataValue);
            }

            if (basicInfo.CustomerName != null && basicInfo.CustomerName != customer.BasicInfo.CustomerName)
            {
                updateMmeo.AppendFormat("客户名称由【{0}】改为【{1}】；", customer.BasicInfo.CustomerName, basicInfo.CustomerName);
            }

            if (!string.IsNullOrEmpty(basicInfo.HomePhone) && !string.IsNullOrEmpty(customer.BasicInfo.HomePhone) && basicInfo.HomePhone != customer.BasicInfo.HomePhone)
            {
                updateMmeo.AppendFormat("固定电话由【{0}】改为【{1}】；", customer.BasicInfo.HomePhone, basicInfo.HomePhone);
                isHomePhone = true;
            }

            if (basicInfo.IdcardNumber != null && basicInfo.IdcardNumber != customer.BasicInfo.IdcardNumber)
            {
                updateMmeo.AppendFormat("身份证号码由【{0}】改为【{1}】；",
                    customer.BasicInfo.IdcardNumber.Substring(0, 6) + "********" + customer.BasicInfo.IdcardNumber.Substring(customer.BasicInfo.IdcardNumber.Length -4, 4),
                    basicInfo.IdcardNumber.Substring(0, 6) + "********" + basicInfo.IdcardNumber.Substring(basicInfo.IdcardNumber.Length - 4, 4));
            }

            if (basicInfo.IdcardType != null && basicInfo.IdcardType != customer.BasicInfo.IdcardType)
            {
                updateMmeo.AppendFormat("证件类型由【{0}】改为【{1}】；", customer.BasicInfo.IdcardType, basicInfo.IdcardType);
            }

            if (basicInfo.Level != null && basicInfo.Level != customer.BasicInfo.Level)
            {
                needApproval = true;
                updateMmeo.AppendFormat("客户等级由【{0}】改为【{1}】，审批通过后更新；",
                     CustomerLevel.ValueList[customer.BasicInfo.Level].DataValue,
                     CustomerLevel.ValueList[basicInfo.Level].DataValue);

                CustomerInfoApprovalModel approvalInfo = new CustomerInfoApprovalModel();
                approvalInfo.ApprovalId = GetGuid();
                approvalInfo.ApprovalTaskId = approvalTaskId;
                approvalInfo.CustomerId = basicInfo.CustomerId;
                approvalInfo.NewData = CustomerLevel.ValueList[basicInfo.Level].DataValue;
                approvalInfo.NewDataId = basicInfo.Level;
                approvalInfo.OldData = CustomerLevel.ValueList[customer.BasicInfo.Level].DataValue;
                approvalInfo.OldDataId = customer.BasicInfo.Level;
                approvalInfo.UpdateField = "level";
                approvalInfo.UpdateFieldName = "客户等级";

                if (CustomerInfoApprovalService.Instance.CreateCustomerInfoApproval(approvalInfo, out message) == false)
                {
                    return false;
                }
                else
                {
                    basicInfo.Level = null;
                }
            }

            if (!string.IsNullOrEmpty(basicInfo.MobilePhone) && basicInfo.MobilePhone != customer.BasicInfo.MobilePhone)
            {
                updateMmeo.AppendFormat("手机号码由【{0}】改为【{1}】；", customer.BasicInfo.MobilePhone, basicInfo.MobilePhone);
                isMobilePhone = true;
            }

            if (!string.IsNullOrEmpty(basicInfo.MobilePhonePrice) && basicInfo.MobilePhonePrice != customer.BasicInfo.MobilePhonePrice)
            {
                updateMmeo.AppendFormat("手机价位由【{0}】改为【{1}】；",
                     MobilePhonePrice.ValueList[customer.BasicInfo.MobilePhonePrice].DataValue,
                     MobilePhonePrice.ValueList[basicInfo.MobilePhonePrice].DataValue);
            }

            if (!string.IsNullOrEmpty(basicInfo.OtherPhone) && basicInfo.OtherPhone != customer.BasicInfo.OtherPhone)
            {
                updateMmeo.AppendFormat("其他号码由【{0}】改为【{1}】；", customer.BasicInfo.OtherPhone, basicInfo.OtherPhone);
                isOtherPhone = true;
            }

            if (!string.IsNullOrEmpty(basicInfo.PreferredPhoneBrand) && basicInfo.PreferredPhoneBrand != customer.BasicInfo.PreferredPhoneBrand)
            {
                updateMmeo.AppendFormat("优选品牌由【{0}】改为【{1}】；",
                     PhoneBrand.ValueList[customer.BasicInfo.PreferredPhoneBrand].DataValue,
                     PhoneBrand.ValueList[basicInfo.PreferredPhoneBrand].DataValue);
            }

            if (basicInfo.SalesFrom != null && basicInfo.SalesFrom != customer.BasicInfo.SalesFrom)
            {
                updateMmeo.AppendFormat("客户来源由【{0}】改为【{1}】；",
                    (customer.BasicInfo.SalesFrom == null || CustomerComeFrom.ValueList.ContainsKey(customer.BasicInfo.SalesFrom) == false) ? "" : CustomerComeFrom.ValueList[customer.BasicInfo.SalesFrom].DataValue,
                     CustomerComeFrom.ValueList[basicInfo.SalesFrom].DataValue);
            }

            if (basicInfo.Sex != null && basicInfo.Sex != customer.BasicInfo.Sex)
            {
                updateMmeo.AppendFormat("性别由【{0}】改为【{1}】；",
                    (customer.BasicInfo.Sex == 0) ? "先生" : ((customer.BasicInfo.Sex == 1) ? "女士" : "未知"),
                    (basicInfo.Sex == 0) ? "先生" : ((basicInfo.Sex == 1) ? "女士" : "未知"));
            }

            if (basicInfo.UsingPhoneBrand != null && basicInfo.UsingPhoneBrand != customer.BasicInfo.UsingPhoneBrand)
            {
                updateMmeo.AppendFormat("在用品牌由【{0}】改为【{1}】；",
                     PhoneBrand.ValueList[customer.BasicInfo.UsingPhoneBrand].DataValue,
                     PhoneBrand.ValueList[basicInfo.UsingPhoneBrand].DataValue);
            }

            if (basicInfo.UsingPhoneType != null && basicInfo.UsingPhoneType != customer.BasicInfo.UsingPhoneType)
            {
                updateMmeo.AppendFormat("在用手机型号由【{0}】改为【{1}】；", customer.BasicInfo.UsingPhoneType, basicInfo.UsingPhoneType);
            }

            if (basicInfo.UsingSmartphone != null && basicInfo.UsingSmartphone != customer.BasicInfo.UsingSmartphone)
            {
                updateMmeo.AppendFormat("是否使用智能机由【{0}】改为【{1}】；", 
                    (customer.BasicInfo.UsingSmartphone == 1) ? "是" : ((customer.BasicInfo.UsingSmartphone == 2) ? "否" : "未知"), 
                    (basicInfo.UsingSmartphone == 1) ? "是" : ((basicInfo.UsingSmartphone == 2) ? "否" : "未知"));
            }

            if (updateMmeo.ToString() != string.Format("客户【{0}】信息修改：", basicInfo.CustomerName))
            {
                try
                {
                    BeginTransaction();

                    CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
                    memoInfo.CustomerId = basicInfo.CustomerId;
                    memoInfo.Memo = updateMmeo.ToString();

                    if (isOtherPhone==true)
                    {
                        if (CustomerPhoneInfoService.Instance.UpdateCustomerPhoneInfo(customer.BasicInfo.OtherPhone, basicInfo.OtherPhone, basicInfo.CustomerId, out message) == false)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    if (isMobilePhone == true)
                    {
                        if (CustomerPhoneInfoService.Instance.UpdateCustomerPhoneInfo(customer.BasicInfo.MobilePhone, basicInfo.MobilePhone, basicInfo.CustomerId, out message) == false)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    if (isHomePhone == true)
                    {
                        if (CustomerPhoneInfoService.Instance.UpdateCustomerPhoneInfo(customer.BasicInfo.HomePhone, basicInfo.HomePhone, basicInfo.CustomerId, out message) == false)
                        {
                            RollbackTransaction();
                            return false;
                        }
                    }

                    if (CustomerMemoInfoService.Instance.CreateMemoInfo(memoInfo, out message) == false)
                    {
                        RollbackTransaction();
                        return false;
                    }

                    if (CustomerBasicInfoService.Instance.Update(basicInfo) == 1)
                    {
                        CommitTransaction();
                        CustomerInfoService.Instance.GetCustomerDomainModelById(memoInfo.CustomerId, true);
                        message = (needApproval) ? "操作成功，部分客户基本信息需要审批后更新" : "成功更新客户基本信息";
                        result = true;
                    }
                }
                catch (Exception ex)
                {
                    RollbackTransaction();
                    LogUtil.Error("更新客户基本信息异常", ex);
                    throw ex;
                }
            }

            #endregion


            return result;
        }

        #endregion

        #region 获取客户敏感信息数据

        /// <summary>
        /// 从IVR系统数据库获取客户敏感信息。
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        public CustomerSecurityInfoDomainModel GetCustomerSecurityInfo(string securityCode, bool mark)
        {
            CustomerSecurityInfoDomainModel result = null;

            string sql = "select * from dbo.sensitive_info where user_id = $id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("id", securityCode);
            DataTable dt = DbUtil.IVRDBManager.IData.ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new CustomerSecurityInfoDomainModel();
                result.CustomerId = dt.Rows[0]["user_id"].ToString().Split('_')[0];
                result.DataTag = dt.Rows[0]["user_id"].ToString().Split('_')[1];
                result.CreditCardNumber = (dt.Rows[0]["creditcard_number"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["creditcard_number"].ToString())) ? "" : dt.Rows[0]["creditcard_number"].ToString();
                result.SecurityCode = (dt.Rows[0]["security_code"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["security_code"].ToString())) ? "" : dt.Rows[0]["security_code"].ToString();
                result.PeriodCode = (dt.Rows[0]["period"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["period"].ToString())) ? "" : dt.Rows[0]["period"].ToString();
                result.IdCardNumber = (dt.Rows[0]["idcard_number"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["idcard_number"].ToString())) ? "" : dt.Rows[0]["idcard_number"].ToString();

                result.IdCardNumber = result.IdCardNumber.Replace('*', 'X');

                result.CreditCardNumber_Base64 = IBP.Common.CommonUtil.GetBase64(result.CreditCardNumber);
                result.IdCardNumber_Base64 = IBP.Common.CommonUtil.GetBase64(result.IdCardNumber);
                result.PeriodCode_Base64 = IBP.Common.CommonUtil.GetBase64(result.PeriodCode);
                result.SecurityCode_Base64 = IBP.Common.CommonUtil.GetBase64(result.SecurityCode);

                result.IncomeCallNumber = (dt.Rows[0]["incoming_phone_number"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["incoming_phone_number"].ToString())) ? "" : dt.Rows[0]["incoming_phone_number"].ToString();
                result.AnswerCallNumber = (dt.Rows[0]["called_phone_number"] == DBNull.Value || string.IsNullOrEmpty(dt.Rows[0]["called_phone_number"].ToString())) ? "" : dt.Rows[0]["called_phone_number"].ToString();
                result.IncomeCallNumber_Base64 = IBP.Common.CommonUtil.GetBase64(result.IncomeCallNumber);
                result.AnswerCallNumber_Base64 = IBP.Common.CommonUtil.GetBase64(result.AnswerCallNumber);

                result.OperatorCode = SessionUtil.Current.ExtAttributes["CtiUserId"].ToString();
                result.OperatorCode_Base64 = IBP.Common.CommonUtil.GetBase64(result.OperatorCode);

                if (mark && string.IsNullOrEmpty(result.CreditCardNumber) == false)
                {
                    result.CreditCardNumber = string.Format("{0}******{1}", result.CreditCardNumber.Substring(0, 6), result.CreditCardNumber.Substring(result.CreditCardNumber.Length - 4, 4));
                }

                if (mark && string.IsNullOrEmpty(result.SecurityCode) == false)
                {
                    result.SecurityCode = "***";
                }

                if (mark && string.IsNullOrEmpty(result.IdCardNumber) == false)
                {
                    result.IdCardNumber = string.Format("{0}********{1}", result.IdCardNumber.Substring(0, 5), result.IdCardNumber.Substring(result.IdCardNumber.Length - 4, 4));
                }
            }

            return result;
        }

        #endregion
    }
}
