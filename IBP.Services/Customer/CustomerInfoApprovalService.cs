/*
版权信息：版权所有(C) 2012，JofoInfo Tech
作    者：周强
完成日期：2012-3-21
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
	/// <summary>
	/// CustomerInfoApproval业务逻辑类
	/// </summary>
	public partial class CustomerInfoApprovalService : BaseService
	{
		// 在此添加你的代码...

        /// <summary>
        /// 获取客户信息审核信息实体。
        /// </summary>
        /// <param name="approvalId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public CustomerInfoApprovalModel GetCustomerInfoApprovalModelById(string approvalId, bool clear)
        {
            CustomerInfoApprovalModel result = null;

            result = Retrieve(approvalId);

            return result;
        }

        /// <summary>
        /// 创建客户审批信息。
        /// </summary>
        /// <param name="approvalInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CreateCustomerInfoApproval(CustomerInfoApprovalModel approvalInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            approvalInfo.Status = 0;

            if (Create(approvalInfo) == 1)
            {
                result = true;
                message = "成功创建客户信息修改审批项目";
            }

            return result;
        }

        /// <summary>
        /// 获取指定状态的客户信息修改审批记录。
        /// </summary>
        /// <param name="approvalStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<CustomerInfoApprovalModel> GetApprovalList(Dictionary<string, QueryItemDomainModel> queryCollection, string approvalStatus, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            List<CustomerInfoApprovalModel> list = null;
            StringBuilder sqlBuilder = new StringBuilder();
            ParameterCollection pc = new ParameterCollection();
            total = 0;
            sqlBuilder.Append(@"
FROM 
    customer_info_approval
WHERE 
    1 = 1 ");

            if (!string.IsNullOrEmpty(approvalStatus))
            {
                sqlBuilder.Append(@" AND status = $status$ ");
                pc.Add("status", approvalStatus);
            }


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
            total = Convert.ToInt32(ExecuteScalar("SELECT  COUNT(1) " + sqlBuilder.ToString(), pc));
            DataTable dt = ExecuteDataTable("SELECT * " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create("customer_info_approval." + orderField, orderDirection));
            list = ModelConvertFrom<CustomerInfoApprovalModel>(dt);
            return list;
        }

        /// <summary>
        /// 更新指定客户信息修改审批记录。
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateCustomerApprovalStatus(List<string> idList, string approvalDescription, int status, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (idList == null || idList.Count == 0)
            {
                return false;
            }

            try
            {
                BeginTransaction();
                string sql = "update customer_info_approval set status = $status$, description = $description$ where approval_id = $id$";
                string updateCustomerSQL = "update customer_basic_info set level = $level$ where customer_id = $customer_id$";

                ParameterCollection pc = new ParameterCollection();
                pc.Add("status", status);
                pc.Add("id", "");
                pc.Add("customer_id", "");
                pc.Add("level", "");
                pc.Add("description", (approvalDescription == null) ? "" : approvalDescription);

                for (int i = 0; i < idList.Count; i++)
                {
                    pc["id"].Value = idList[i];

                    CustomerInfoApprovalModel approvalInfo = Retrieve(idList[i]);
                    if (approvalInfo == null)
                    {
                        message = "操作失败，不存在的客户信息修改审批ID";
                        return false;
                    }

                    if (ExecuteNonQuery(sql, pc) != 1)
                    {
                        RollbackTransaction();
                        message = "更新审批记录失败，请与管理员联系";
                        LogUtil.Debug(string.Format("更新ID为【{0}】的客户信息修改审批记录状态失败", approvalInfo.ApprovalId));

                        return false;
                    }
                    else
                    {
                        pc["customer_id"].Value = approvalInfo.CustomerId;

                        if (status == 2)
                        {
                            pc["level"].Value = approvalInfo.NewDataId;
                        }
                        else if (status == 1)
                        {
                            pc["level"].Value = approvalInfo.OldData;
                        }

                        if (ExecuteNonQuery(updateCustomerSQL, pc) != 1)
                        {
                            RollbackTransaction();
                            message = "更新客户等级信息失败，请与管理员联系";
                            LogUtil.Debug(string.Format("更新ID为【{0}】的客户信息修改审核记录状态失败，客户ID为【{1}】", approvalInfo.ApprovalId, approvalInfo.CustomerId));

                            return false;
                        }
                        else
                        {
                            CustomerMemoInfoModel memoInfo = new CustomerMemoInfoModel();
                            memoInfo.CustomerId = approvalInfo.CustomerId;
                            if (status == 1)
                            {
                                memoInfo.Memo = string.Format("客户信息修改审核未通过，“{0}”原值【{1}】改为【{2}】, {3}", approvalInfo.UpdateFieldName, approvalInfo.NewData, approvalInfo.OldData, approvalDescription);
                            }
                            else if (status == 2)
                            {
                                memoInfo.Memo = string.Format("客户信息修改审核通过，“{0}”原值【{1}】改为【{2}】, {3}", approvalInfo.UpdateFieldName, approvalInfo.OldData, approvalInfo.NewData, approvalDescription);
                            }

                            memoInfo.MemoId = GetGuid();
                            memoInfo.Status = 0;

                            if (CustomerMemoInfoService.Instance.CreateMemoInfo(memoInfo, out message) == false)
                            {
                                RollbackTransaction();
                                return false;
                            }

                            CustomerInfoService.Instance.GetCustomerDomainModelById(approvalInfo.CustomerId, true);
                        }

                    }
                }

                CommitTransaction();
                message = "成功提交客户审核信息";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("通过客户信息修改审批异常", ex);
                throw ex;
            }

            return result;
        }
	}
}

