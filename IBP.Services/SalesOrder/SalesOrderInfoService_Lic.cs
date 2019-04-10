/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-19
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Collections;
using Framework.Common;
using Framework.DataAccess;
using Framework.Utilities;
using IBP.Common;
using IBP.Models;
using System.Text;
using IBP.Models.DomainModels;


namespace IBP.Services
{
    public partial class SalesOrderInfoService : BaseService
    {
        /// <summary>
        /// 从缓存中获取销售订单领域模型。
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public SalesOrderDomainModel GetSalesorderDomainModelById(string salesOrderId, bool clear)
        {
            if (string.IsNullOrEmpty(salesOrderId))
                return null;

            string cacheKey = CacheKey.SALESORDER_DOMAINMODEL.GetKeyDefine(salesOrderId);
            SalesOrderDomainModel model = CacheUtil.Get<SalesOrderDomainModel>(cacheKey);

            if (model == null || clear)
            {
                model = GetSalesorderDomainModelByIdFromDatabse(salesOrderId);

                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }

        /// <summary>
        /// 从缓存中获取销售订单数量。
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public SalesOrderTotal GetSalesorderOreateTotal()
        {
           
            SalesOrderTotal TotalModel = new SalesOrderTotal();
            string cacheKey = CacheKey.SALESORDER_TOTAL;

            SalesOrderTotal model = CacheUtil.Get<SalesOrderTotal>(cacheKey);

            if (model == null )
            {
            
                DataTable table = ExecuteDataTable("SELECT  now_order_status_id,COUNT(now_order_status_id ) as tatal from salesorder_basic_info   group by now_order_status_id ");

                #region MyRegion
                if (table != null && table.Rows.Count > 0)
                {
                  

               
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                    
                        string status = table.Rows[i][0].ToString();
                        switch (status)
                        { 
                            case "1":
                                TotalModel.WaitFollow = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "2":
                                TotalModel.WaitCharge = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "3":
                                TotalModel.WaitCheck = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "4":
                                TotalModel.WaitApproval = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "5":
                                TotalModel.WaitOpening = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "6":
                                TotalModel.WaitStocking = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "7":
                                TotalModel.WaitDelivery = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "8":
                                TotalModel.WaitSign = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "9":
                                TotalModel.WaitRecover = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "10":
                                TotalModel.Successed = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "11":
                                TotalModel.Exception = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "12":
                                TotalModel.WaitRefund = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "13":
                                TotalModel.WaitReturns = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "14":
                                TotalModel.WaitCancelOpening = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;
                            case "15":
                                TotalModel.Cancel = (table.Rows[i][1].ToString() == null) ? "0" : table.Rows[i][1].ToString();
                                break;


                        }

                    }
                }
                if (TotalModel != null)
                {
                    CacheUtil.Set(cacheKey, TotalModel);
                } 
                #endregion


              
               
            }

            return TotalModel;
        }


     

        /// <summary>
        /// 从数据库中获取销售订单领域模型。
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <returns></returns>
        public SalesOrderDomainModel GetSalesorderDomainModelByIdFromDatabse(string salesOrderId)
        {
            SalesOrderDomainModel model = null;
            SalesorderBasicInfoModel SalesorderBasicInfo = SalesorderBasicInfoService.Instance.Retrieve(salesOrderId);
            if (SalesorderBasicInfo == null)
            {
                return null;
            }
            model = new SalesOrderDomainModel();
            model.BasicInfo = SalesorderBasicInfo;
            model.ProductList = new Dictionary<string, SalesorderProductInfoModel>();
            model.CommuniationPackageList = new Dictionary<string, SalesorderCommuniationpackageInfoModel>();
            model.ProcessLogs = new Dictionary<string, SalesorderProcessLogModel>();

            
            ParameterCollection pc = new ParameterCollection();
            pc.Add("salesorder_id", SalesorderBasicInfo.SalesorderId);
            List<SalesorderProductInfoModel> list = SalesorderProductInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("created_on", "desc"));
            if (list != null)
            {
                foreach (SalesorderProductInfoModel item in list)
                {
                    model.ProductList[item.ProductName] = item;
                }
            }
          
            List<SalesorderCommuniationpackageInfoModel> CommuniationList = SalesorderCommuniationpackageInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("created_on", "desc"));
            if (CommuniationList != null)
            {
                foreach (SalesorderCommuniationpackageInfoModel CommuniationPackageList in CommuniationList)
                {
                    model.CommuniationPackageList[CommuniationPackageList.BindCommuniationpackageId] = CommuniationPackageList;
                }
            }

            List<SalesorderProcessLogModel> ProcessLogModel = SalesorderProcessLogService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("created_on", "desc"));
            if (ProcessLogModel != null)
            {
                foreach (SalesorderProcessLogModel ProcessLogsList in ProcessLogModel)
                {
                    model.ProcessLogs[ProcessLogsList.SalesorderProcessId] = ProcessLogsList;
                }
            }


            return model;

        }


        /// <summary>
        /// 修改订单状态。
        /// </summary>
        /// <param name="statusInfo"></param>
        /// <returns></returns>
        public bool UpdateSalesorderOrderstatus(SalesorderBasicInfoModel model, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;
            string UpdateSalesorderOrderstatus = "UPDATE salesorder_basic_info SET now_order_status_id = $now_order_status_id$ WHERE salesorder_id = $salesorder_id$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("now_order_status_id", model.NowOrderStatusId);
            pc.Add("salesorder_id", model.SalesorderId);

            if (ExecuteNonQuery(UpdateSalesorderOrderstatus, pc) != 1)
            {
               
                message = "操作失败，请与管理员联系";
                return false;
            }
            else
            {
                result = true;
                message = "修改成功";
                GetSalesorderDomainModelById(model.NowOrderStatusId, true);
            }

            return result;
        }


        public DataTable GetDataTable(bool isCreatedBy, SalesOrderStatus orderStatus, string incomePhoneNumber, string selectedPhoneNumber, Dictionary<string, QueryItemDomainModel> queryCollection)
        {
            #region MyRegion
            DataTable dt = new DataTable();


            StringBuilder sqlBuilder = new StringBuilder();
            ParameterCollection pc = new ParameterCollection();
            sqlBuilder.Append(@"
  customer_basic_info.created_on as customer_create_on,pay_card_number,pay_card_period,pay_card_securitycode
,order_source=(SELECT data_value FROM custom_data_value WHERE value_id=order_source),pay_card_bank_id=(SELECT data_value FROM custom_data_value where value_id=pay_card_bank_id)
 ,opening_time,collection_card_number,collection_customer_name,mobile_phone,home_phone,other_phone
 ,collection_bank_id  =(SELECT data_value FROM custom_data_value where value_id=package.collection_bank_id)
 ,package.idcard_number,package.owner_customer_name,charge_user_id=(select created_user_info.cn_name from user_info where user_id=charge_user_id)
,recover_time,sign_time,salesorder_basic_info.remark,need_invoice= case when pay_type='0' then '是' when pay_type='1' then '否' end 
,delivery_address,delivery_receive_phonenumber,delivery_receive_customer_name,charge_time, package.bind_main_phonenumber
,package.bind_subsidiary_phonenumber,category_name, sex= case when pay_type='0' then '男' when pay_type='1' then '女' end 
,salesorder_basic_info.delivery_time,salesorder_basic_info.approval_time,salesorder_basic_info.salesorder_code, customer_basic_info.customer_name
,custom_data_value.data_value,salesorder_basic_info.pay_price,pay_type= case when pay_type='0' then '分期' when pay_type='1' then '全额' when pay_type='2' then '到付' when pay_type='3' then '在线支付' end 
,created_user_info.cn_name,salesorder_basic_info.created_on
 FROM salesorder_basic_info

LEFT JOIN  customer_basic_info
 ON salesorder_basic_info.customer_id = customer_basic_info.customer_id
LEFT JOIN  custom_data_value
ON
 salesorder_basic_info.pay_idcard_type_id=custom_data_value.value_id
 
 LEFT JOIN
    user_info as created_user_info
ON
    salesorder_basic_info.created_by = created_user_info.user_id
    
LEFT JOIN
   salesorder_communiationpackage_info as package
ON
    salesorder_basic_info.salesorder_id = package.salesorder_id
    
LEFT JOIN
   product_category_info as product
ON
     package.bind_communiationpackage_id = product.product_category_id

WHERE 
    1 = 1
    
 ");
            if (orderStatus != SalesOrderStatus.All)
            {

                if (orderStatus == SalesOrderStatus.Exception)
                {
                    sqlBuilder.Append(@" AND is_exception = 0 ");
                }
                else
                {
                    sqlBuilder.Append(@" AND now_order_status_id = $orderStatus$ and is_exception<>0");
                    pc.Add("orderStatus", Convert.ToInt32(orderStatus));
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

            dt = ExecuteDataTable("SELECT  " + sqlBuilder.ToString(), pc);



            return dt; 
            #endregion

        }


        
    }
}
