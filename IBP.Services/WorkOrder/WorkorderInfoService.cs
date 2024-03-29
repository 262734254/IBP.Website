/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-15
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
    public delegate bool BatchAddWorkOrder(WorkorderInfoModel basicInfo, CustomerContactInfoModel contactInfo, out string message);


	/// <summary>
	/// WorkorderInfo业务逻辑类
	/// </summary>
	public partial class WorkorderInfoService : BaseService
	{
		// 在此添加你的代码...

        static bool locker = false;

        /// <summary>
        /// 获取工单统计报表领域模型。
        /// </summary>
        /// <param name="clear"></param>
        /// <returns></returns>
        public WorkOrderReportDomainModel GetWorkOrderReportDomainModel(bool clear)
        {
            string cacheKey = CacheKey.WORKORDER_REPORT_DOMAINMODEL;
            WorkOrderReportDomainModel report = CacheUtil.Get<WorkOrderReportDomainModel>(cacheKey);
            if (report == null || clear)
            {
                report = GetWorkOrderReportDomainModelFromDatabase();
                if (report != null)
                {
                    CacheUtil.Set(cacheKey, report);
                }
            }

            return report;
        }

        /// <summary>
        /// 从数据库中获取工单统计报表领域模型。
        /// </summary>
        /// <returns></returns>
        public WorkOrderReportDomainModel GetWorkOrderReportDomainModelFromDatabase()
        {
            WorkOrderReportDomainModel result = new WorkOrderReportDomainModel();

            string getTotalSQL = @"
-- 获取工单总数
SELECT COUNT(1) FROM workorder_info;
-- 获取各级别工单总数
SELECT [level],COUNT(1) FROM workorder_info group by [level];
-- 获取各处理状态工单总数
SELECT process_status,COUNT(1) FROM workorder_info group by process_status;
-- 获取各类型工单总数
SELECT workorder_type, COUNT(1) FROM workorder_info GROUP BY workorder_type;
";


            DataSet ds = ExecuteDataSet(getTotalSQL);
            if (ds == null && ds.Tables.Count != 3)
            {
                return null;
            }

            result.Total = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            CustomDataDomainModel levelDomainModel = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("工单级别", false);
            Dictionary<string, WorkorderTypeInfoModel> typeList = WorkorderTypeInfoService.Instance.GetWorkOrderDictionary(false);


            result.LevelReport = new Dictionary<string, WorkOrderLevelReport>();
            result.TypeReport = new Dictionary<string, WorkOrderTypeReport>();
            result.ProcessStatusReport = new Dictionary<string, WorkOrderProcessStatusReport>();

            #region 获取工单级别统计数据

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                WorkOrderLevelReport wlr = new WorkOrderLevelReport();
                wlr.LevelId = ds.Tables[1].Rows[i][0].ToString();
                if (levelDomainModel.ValueList.ContainsKey(wlr.LevelId) == false)
                {
                    throw new Exception("数据中存在未知的工单级别ID");
                }
                wlr.LevelName = levelDomainModel.ValueList[wlr.LevelId].DataValue;
                wlr.Total = Convert.ToInt32(ds.Tables[1].Rows[i][1]);

                result.LevelReport[wlr.LevelId] = wlr;
            }

            #endregion

            #region 获取工单处理状态统计数据

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                WorkOrderProcessStatusReport wpsr = new WorkOrderProcessStatusReport();
                wpsr.ProcessStatusId = ds.Tables[2].Rows[i][0].ToString();
                switch (wpsr.ProcessStatusId)
                {
                    case "0":
                        wpsr.ProcessStatusName = "待处理";
                        break;

                    case "1":
                        wpsr.ProcessStatusName = "处理中";
                        break;

                    case "2":
                        wpsr.ProcessStatusName = "已关闭";
                        break;

                    default:
                        break;
                }
                wpsr.Total = Convert.ToInt32(ds.Tables[2].Rows[i][1]);

                result.ProcessStatusReport[wpsr.ProcessStatusId] = wpsr;
            }

            #endregion

            #region 获取工单类型统计数据

            string getWorkStatusSQL = @"SELECT now_status_id,COUNT(1) FROM workorder_info WHERE workorder_type = $workorder_type$ GROUP BY now_status_id";
            string getWorkResultSQL = @"SELECT now_result_id,COUNT(1) FROM workorder_info WHERE workorder_type = $workorder_type$ GROUP BY now_result_id";
            ParameterCollection spc = new ParameterCollection();
            
            DataTable statusTable = null;
            DataTable resultTable = null;
            WorkOrderTypeDomainModel typeDomain = null;

            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                WorkOrderTypeReport wtr = new WorkOrderTypeReport();
                wtr.StatusReport = new Dictionary<string, WorkOrderTypeStatusReport>();
                wtr.ResultReport = new Dictionary<string, WorkOrderTypeResultReport>();

                wtr.TypeId = ds.Tables[3].Rows[i][0].ToString();
                if (typeList.ContainsKey(wtr.TypeId.ToUpper()) == false)
                {
                    throw new Exception("数据中存在未知的工单类型ID");
                }
                typeDomain = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(wtr.TypeId.ToUpper(), false);
                wtr.TypeName = typeDomain.TypeInfo.TypeName;
                wtr.Total = Convert.ToInt32(ds.Tables[3].Rows[i][1]);

                spc.Clear();
                spc.Add("workorder_type", wtr.TypeId);

                statusTable = ExecuteDataTable(getWorkStatusSQL, spc);
                if (statusTable != null)
                {
                    WorkOrderTypeStatusReport wotsp = null;
                    for (int j = 0; j < statusTable.Rows.Count; j++)
                    {
                        if (typeDomain.StatusList.ContainsKey(statusTable.Rows[j]["now_status_id"].ToString()) == false)
                        {
                            throw new Exception("数据中存在未知的工单类型状态ID");
                        }
                        wotsp = new WorkOrderTypeStatusReport();
                        wotsp.StatusId = statusTable.Rows[j]["now_status_id"].ToString();
                        wotsp.StatusName = typeDomain.StatusList[wotsp.StatusId].StatusName;
                        wotsp.Total = Convert.ToInt32(statusTable.Rows[j][1]);

                        wtr.StatusReport[wotsp.StatusId] = wotsp;
                    }
                }

                resultTable = ExecuteDataTable(getWorkResultSQL, spc);
                if (resultTable != null)
                {
                    WorkOrderTypeResultReport wotrp = null;
                    for (int j = 0; j < resultTable.Rows.Count; j++)
                    {
                        if (typeDomain.ResultList.ContainsKey(resultTable.Rows[j]["now_result_id"].ToString()) == false)
                        {
                            throw new Exception("数据中存在未知的工单类型处理结果ID");
                        }
                        wotrp = new WorkOrderTypeResultReport();
                        wotrp.ResultId = resultTable.Rows[j]["now_result_id"].ToString();
                        wotrp.ResultName = typeDomain.ResultList[wotrp.ResultId].ResultName;
                        wotrp.Total = Convert.ToInt32(resultTable.Rows[j][1]);

                        wtr.ResultReport[wotrp.ResultId] = wotrp;
                    }
                }

                result.TypeReport[wtr.TypeId] = wtr;
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 转交工单信息。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="assUserGroupId"></param>
        /// <param name="assUserId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AssignmentWorkOrder(List<string> workOrderIdList, string assUserGroupId, string assUserId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            if (workOrderIdList == null && workOrderIdList.Count == 0)
            {
                message = "操作失败，不存在的工单ID";
                return false;
            }

            UserGroupPremissionDomainModel userGroup = UserGroupInfoService.Instance.GetUserGroupDomainByGroupId(assUserGroupId, false);
            if (userGroup == null)
            {
                message = "操作失败，不存在的用户组ID";
                return false;
            }
            UserDomainModel user = null;
            UserDomainModel owner = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            if (assUserId != null)
            {
                user = UserInfoService.Instance.GetUserDomainModelById(assUserId, false);
                if (user == null)
                {
                    message = "操作失败，不存在的用户ID";
                    return false;
                }
            }

            try
            {
                BeginTransaction();
                string updateNowProcessUserIdSQL = "UPDATE workorder_info SET now_process_userid = NULL WHERE work_order_id = $work_order_id$";
                ParameterCollection pc = new ParameterCollection();

                foreach (string workOrderId in workOrderIdList)
                {
                    WorkOrderDomainModel wo = GetWorkorderDomainModelById(workOrderId, false);
                    if (wo == null)
                    {
                        RollbackTransaction();
                        message = "操作失败，不存在的工单ID";
                        return false;
                    }

                    WorkorderProcessInfoModel wp = new WorkorderProcessInfoModel();
                    wp.WorkorderId = wo.BasicInfo.WorkOrderId;
                    wp.WorkorderTypeId = wo.BasicInfo.WorkorderType;
                    wp.StatusCode = 0;
                    wp.ProcessId = GetGuid();
                    wp.BeforeStatus = wp.AfterStatus = wo.BasicInfo.NowStatusId;
                    wp.BeforeResult = wp.AfterResult = wo.BasicInfo.NowResultId;
                    wp.BeforeUserId = wo.BasicInfo.NowProcessUserid;
                    wp.AfterUserId = assUserId;

                    if (user == null)
                    {
                        wo.BasicInfo.NowProcessUserid = null;
                        wp.Description = string.Format("工单由【{0}（{1}）】转交至【{2}】处理。", owner.BasicInfo.WorkId.Replace("WORKID_", ""), owner.BasicInfo.CnName, userGroup.UserGroupInfo.GroupName);
                    }
                    else
                    {
                        wo.BasicInfo.NowProcessUserid = assUserId;
                        if (wo.BasicInfo.NowProcessUserid != assUserId)
                        {
                            wp.Description = string.Format("工单由【{0}（{1}）】转交至【{2}（{3}）】处理。", owner.BasicInfo.WorkId.Replace("WORKID_", ""), owner.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), user.BasicInfo.CnName);
                        }
                    }

                    wo.BasicInfo.RelUsergroupId = assUserGroupId;                    
                    wo.BasicInfo.StatusForUser = 0;

                    if (Update(wo.BasicInfo) != 1)
                    {
                        RollbackTransaction();
                        message = "操作失败，更新工单信息失败";
                        return false;
                    }
                    else
                    {
                        if (user == null)
                        {
                            pc.Clear();
                            pc.Add("work_order_id", wo.BasicInfo.WorkOrderId);
                            if (ExecuteNonQuery(updateNowProcessUserIdSQL, pc) != 1)
                            {
                                RollbackTransaction();
                                message = "操作失败，更新工单信息失败";
                                return false;
                            }
                        }

                        if (WorkorderProcessInfoService.Instance.Create(wp) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，创建工单处理记录信息失败";
                            return false;
                        }

                        GetWorkorderDomainModelById(wo.BasicInfo.WorkOrderId, true);
                    }
                }

                CommitTransaction();
                result = true;
                message = "成功转交工单至指定用户或组";
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("转交工单处理操作异常", ex);
                throw ex;
            }

            return result;
        }

        public List<string> GetWorkOrderIdList(GetWorkOrderRole getWorkorderRole, WorkOrderAssignedStatus assignedStatus, WorkOrderProcessStatus processStatus, WorkOrderRemindType remindType, WorkOrderCustomStatus customStatus, Dictionary<string, QueryItemDomainModel> queryCollection, int pageIndex, int pageSize, string orderField, string orderDirection, out int total)
        {
            total = 0;
            List<string> result = null;
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"
 FROM 
	workorder_info 
INNER JOIN 
	customer_basic_info 
ON 
	workorder_info.rel_customer_id = customer_basic_info.customer_id
INNER JOIN
	workorder_status_info
ON
	workorder_info.now_status_id = workorder_status_info.workorder_status_id
LEFT JOIN  
    user_info close_userinfo
ON 
    close_userinfo.user_id=workorder_info.closed_user 
LEFT JOIN 
    user_info create_userinfo
ON 
    create_userinfo.user_id = workorder_info.created_by 
LEFT JOIN 
    user_info now_process_workid
ON 
    now_process_workid.user_id = workorder_info.first_process_userid 
WHERE 
    1 = 1 
");
            
            ParameterCollection pc = new ParameterCollection();

            #region 构造工单特殊状态条件

            switch (customStatus)
            {
                case WorkOrderCustomStatus.All:
                    break;
                case WorkOrderCustomStatus.WaittingApproval:
                    sqlBuilder.Append(" AND workorder_status_info.custom_status = '待审批' ");
                    break;
                case WorkOrderCustomStatus.WaittingQualityCheck:
                    sqlBuilder.Append(" AND (workorder_info.quality_check_status = 0 OR workorder_info.quality_check_status = 2) AND process_status = 2 ");
                    break;
                case WorkOrderCustomStatus.QualityChecked:
                    sqlBuilder.Append(" AND workorder_info.quality_check_status = 1 AND process_status = 2 ");
                    break;

                default:
                    break;
            }

            #endregion

            #region 构造工单提醒条件

            switch (remindType)
            {
                case WorkOrderRemindType.All:
                    break;
                case WorkOrderRemindType.NoSet:
                    sqlBuilder.Append(" AND advance_time IS NULL ");
                    sqlBuilder.Append(" AND expired_time IS NULL ");
                    break;
                case WorkOrderRemindType.Advance:
                    sqlBuilder.Append(" AND DATEADD(HH,1,GETDATE()) > advance_time ");
                    break;
                case WorkOrderRemindType.Expired:
                    sqlBuilder.Append(" AND DATEADD(HH,1,GETDATE()) > expired_time ");
                    break;
                case WorkOrderRemindType.TwoHourAppointmentExpired:
                    sqlBuilder.Append(" AND advance_time > getdate() and advance_time < DATEADD(hour, 2, getdate())");
                    break;
                case WorkOrderRemindType.TwentyFourExpired:
                      sqlBuilder.Append(" AND advance_time > getdate() and advance_time < DATEADD(day, 1, getdate())");
                      break;
                case WorkOrderRemindType.ThreeDayExpired:
                      sqlBuilder.Append(" AND advance_time > getdate() and advance_time < DATEADD(day, 3, getdate())");
                      break;
                case WorkOrderRemindType.AllWillBeExpire:
                      sqlBuilder.Append(" AND advance_time <= getdate()");
                      break;
                case WorkOrderRemindType.ExpiredOrder:
                      sqlBuilder.Append(" AND expired_time <= getdate()");
                      break;
                case WorkOrderRemindType.TwohoursWillBeExpired:
                      sqlBuilder.Append(" AND expired_time > getdate() and expired_time < DATEADD(hour, 2, getdate())");
                      break;
                case WorkOrderRemindType.TwentyWillBeExpired:
                      sqlBuilder.Append(" AND expired_time > getdate() and expired_time < DATEADD(day, 1, getdate())");
                      break;
                case WorkOrderRemindType.ThreeDayWillBeExpired:
                      sqlBuilder.Append(" AND expired_time > getdate() and expired_time < DATEADD(day, 3, getdate())");
                      break;
                case WorkOrderRemindType.AllExpired:
                      sqlBuilder.Append(" AND expired_time<getdate()");
                      break;
                default:
                    break;
            }

            #endregion

            #region 构造处理状态条件

            switch (processStatus)
            {
                case WorkOrderProcessStatus.All:
                    break;
                case WorkOrderProcessStatus.Waitting:
                    sqlBuilder.Append(" AND process_status = 0 ");
                    break;
                case WorkOrderProcessStatus.Processing:
                    sqlBuilder.Append(" AND process_status = 1  AND workorder_status_info.custom_status <> '待审批' ");
                  break;
                case WorkOrderProcessStatus.Closed:
                    sqlBuilder.Append(" AND process_status = 2 ");
                    break;
                default:
                    break;
            }

            #endregion

            #region 构造分派条件

            switch (getWorkorderRole)
            {
                case GetWorkOrderRole.All:
                    switch (assignedStatus)
	                {
                        case WorkOrderAssignedStatus.All:
                            break;
                        case WorkOrderAssignedStatus.UnAssigned:
                            sqlBuilder.Append(" AND now_process_userid IS NULL ");
                            sqlBuilder.Append(" AND rel_usergroup_id IS NULL ");
                            break;
                        case WorkOrderAssignedStatus.AssignedToGroup:
                            sqlBuilder.Append(" AND now_process_userid IS NULL ");
                            sqlBuilder.Append(" AND rel_usergroup_id IS NOT NULL ");
                           break;
                        case WorkOrderAssignedStatus.AssignedToUser:
                            sqlBuilder.Append(" AND now_process_userid IS NOT NULL ");
                           break;
                        default:
                            break;
	                }
                    break;

                case GetWorkOrderRole.Owner:
                    sqlBuilder.Append(" AND now_process_userid = $now_process_userid$ ");
                    pc.Add("now_process_userid", SessionUtil.Current.UserId);
                    break;

                case GetWorkOrderRole.OwnerGroup:
                    UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);
                    if (user.InGroupList != null && user.InGroupList.Count > 0)
                    {
                        sqlBuilder.Append(" AND now_process_userid IS NULL ");
                        sqlBuilder.Append(" AND ( ");
                        for (int i = 0; i < user.InGroupList.Count; i++)
                        {
                            if (i == user.InGroupList.Count - 1)
                            {
                                sqlBuilder.AppendFormat(" rel_usergroup_id = $rel_group{0}$ ", i);
                            }
                            else
                            {
                                sqlBuilder.AppendFormat(" rel_usergroup_id = $rel_group{0}$ OR ", i);
                            }

                            pc.Add("rel_group" + i.ToString(), user.InGroupList[i]);
                        }

                        sqlBuilder.Append(" ) ");
                    }
                    else
                    {
                        // 不在任何组，将查询不出数据
                        return null;
                    }
                    break;

                default:
                    break;
            }

            #endregion

            int count = 0;

            #region 构造查询条件

            foreach (QueryItemDomainModel item in queryCollection.Values)
            {
               
                #region 针对用户ID做处理

                if (item.FieldType == "close_user_workid")
                {
                    item.FieldType = "close_userinfo.work_id";
                    item.SearchValue = "WORKID_" + item.SearchValue;
                }
                if (item.FieldType == "create_user_workid")
                {
                    item.FieldType = "create_userinfo.work_id";
                    item.SearchValue = "WORKID_" + item.SearchValue;
                }
                if (item.FieldType == "now_process_workid")
                {
                    item.FieldType = "now_process_workid.work_id";
                    item.SearchValue = "WORKID_" + item.SearchValue;
                }
              

                #endregion

                #region 针对工单时间做处理
                if (item.FieldType == "workorder_created_time")
                {
                    item.FieldType = "workorder_info.created_on";
        
                }

                if (item.FieldType == "workorder_close_time")
                {
                    item.FieldType = "workorder_info.closed_time";
                }
                if (item.FieldType == "workorder_firstprocess_time")
                {
                    item.FieldType = "workorder_info.first_process_time";
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

            DataTable dt = ExecuteDataTable("SELECT work_order_id " + sqlBuilder.ToString(), pc, pageIndex, pageSize, OrderByCollection.Create("workorder_info." + orderField, orderDirection));
            result = ModelConvertFrom(dt);

            return result;
        }

        /// <summary>
        /// 根据ID获取工单信息领域模型。
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <param name="clear"></param>
        /// <returns></returns>
        public WorkOrderDomainModel GetWorkorderDomainModelById(string workOrderId, bool clear)
        {
            if (string.IsNullOrEmpty(workOrderId))
                return null;

            string cacheKey = CacheKey.WORKORDER_DOMAINMODEL.GetKeyDefine(workOrderId);
            WorkOrderDomainModel model = CacheUtil.Get<WorkOrderDomainModel>(cacheKey);
            if (model == null || clear)
            {
                model = GetWorkorderDomainModelByIdFromDatabase(workOrderId);
                if (model != null)
                {
                    CacheUtil.Set(cacheKey, model);
                }
            }

            return model;
        }

        /// <summary>
        /// 从数据库获取工单信息领域模型。
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public WorkOrderDomainModel GetWorkorderDomainModelByIdFromDatabase(string workOrderId)
        {
            WorkOrderDomainModel model = null;

            WorkorderInfoModel basicInfo = Retrieve(workOrderId);
            if (basicInfo == null)
            {
                return null;
            }

            model = new WorkOrderDomainModel();
            model.BasicInfo = basicInfo;
            model.ProcessList = new Dictionary<string, WorkorderProcessInfoModel>();
            model.CheckList = new Dictionary<string, WorkorderChecksInfoModel>();

            ParameterCollection pc = new ParameterCollection();
            pc.Add("workorder_id",basicInfo.WorkOrderId);

            List<WorkorderProcessInfoModel> list = WorkorderProcessInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("created_on","desc"));
            if (list != null)
            {
                foreach (WorkorderProcessInfoModel item in list)
                {
                    model.ProcessList[item.ProcessId] = item;
                }
            }

            List<WorkorderChecksInfoModel> checkList = WorkorderChecksInfoService.Instance.RetrieveMultiple(pc, OrderByCollection.Create("created_on", "desc"));
            if (list != null)
            {
                foreach (WorkorderChecksInfoModel item in checkList)
                {
                    model.CheckList[item.WorkorderCheckId] = item;
                }
            }

            return model;
        }


        public bool DelegateBatchAddWorkOrder(WorkorderInfoModel basicInfo, CustomerContactInfoModel contactInfo, out string message, BatchAddWorkOrder wororder)
        {
            if (locker)
            {
                message = "正在操作，请稍后再试";
                return false;
            }

            if (wororder(basicInfo, contactInfo, out message))
            {
                return true;
            }
            else
            { return false; }
        }

        public bool GetBatchAddWorkOrder(WorkorderInfoModel basicInfo, CustomerContactInfoModel contactInfo, out string message)
        {
            locker = true;

            bool result = false;

            message = "操作投入，请与管理员联系";
            if (basicInfo == null)
            {
                locker = false;
                message = "参数错误，请与管理员联系";
                return false;
            }

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            queryCollection = System.Web.HttpContext.Current.Session["BathWorkOrder"] as Dictionary<string, QueryItemDomainModel>;
            List<string> customerList = CustomerInfoService.Instance.GetBatchCustomerIdList(queryCollection);

            if (customerList == null || customerList.Count == 0)
            {
                locker = false;
                message = "参数错误，请与管理员联系";
                return false;
            }
            try
            {
                BeginTransaction();

                foreach (string customerid in customerList)
                {
                    basicInfo.RelCustomerId = customerid;

                    basicInfo.WorkOrderId = GetGuid();
                    //basicInfo.FirstProcessTime = DateTime.Now;
                    //basicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                    //basicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                    basicInfo.WorkorderCode = "WO" + DateTime.Now.ToString("yyyyMMddhhmmssfff");
                    basicInfo.ProcessStatus = 0;
                    basicInfo.StatusForUser = 0;

                    if (contactInfo != null)
                    {
                        contactInfo.ContactId = GetGuid();
                        basicInfo.NowContactResult = contactInfo.Results;
                    }

                    if (Create(basicInfo) == 1)
                    {
                        WorkorderProcessInfoModel proc = new WorkorderProcessInfoModel();

                        proc.ProcessId = GetGuid();
                        proc.WorkorderId = basicInfo.WorkOrderId;
                        proc.WorkorderTypeId = basicInfo.WorkorderType;
                        proc.Description = basicInfo.Description;
                        proc.BeforeStatus = basicInfo.NowStatusId;
                        proc.BeforeResult = basicInfo.NowResultId;
                        proc.BeforeUserId = SessionUtil.Current.UserId;
                        proc.AfterStatus = basicInfo.NowStatusId;
                        proc.AfterResult = basicInfo.NowResultId;
                        proc.AfterUserId = SessionUtil.Current.UserId;
                        if (contactInfo != null)
                        {
                            proc.RelContactId = contactInfo.ContactId;
                        }

                        if (WorkorderProcessInfoService.Instance.Create(proc) == 1)
                        {
                            if (contactInfo != null)
                            {
                                contactInfo.RelWorkorderId = basicInfo.WorkOrderId;
                                PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(contactInfo.CustomerPhone, false);
                                if (loc != null)
                                {
                                    contactInfo.FromCityId = loc.ChinaId;
                                    contactInfo.FromCityName = loc.City;
                                }

                                if (CustomerContactInfoService.Instance.Create(contactInfo) == 1)
                                {
                                    CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
                                    phoneInfo.CustomerId = contactInfo.CustomerId;
                                    phoneInfo.PhoneNumber = contactInfo.CustomerPhone;

                                    if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(phoneInfo, out message))
                                    {
                                        message = "添加客户联系号码失败";
                                        RollbackTransaction();
                                        result = false;
                                        locker = false;
                                        return false;
                                    }

                                    CustomerInfoService.Instance.GetCustomerDomainModelById(basicInfo.RelCustomerId, true);
                                    message = "成功创建工单信息";
                                    result = true;
                                }
                                else
                                {
                                    RollbackTransaction();
                                    locker = false;
                                    message = "创建工单关联联系记录失败，请与管理员联系";
                                    result = false;
                                }
                            }
                            else
                            {
                                CustomerInfoService.Instance.GetCustomerDomainModelById(basicInfo.RelCustomerId, true);
                                message = "成功创建工单信息";
                                result = true;
                            }
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "创建工单处理记录失败，请与管理员联系";
                            locker = false;
                            result = false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "保存工单基本信息失败，请与管理员联系";
                        locker = false;
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功批量创建工单信息";
                result = true;
                locker = false;
            
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                locker = false;
                LogUtil.Error("创建工单异常", ex);
                throw ex;
            }

            locker = false;
            return result;
        }


        /// <summary>
        /// 新建工单。
        /// </summary>
        /// <param name="basicInfo"></param>
        /// <param name="contactInfo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CreateNewWorkOrder(WorkorderInfoModel basicInfo, CustomerContactInfoModel contactInfo, out string message)
        {
            bool result = false;
            message = "操作投入，请与管理员联系";

            if (basicInfo == null)
            {
                message = "参数错误，请与管理员联系";
                return false;
            }

            try
            {
                BeginTransaction();
                basicInfo.WorkOrderId = GetGuid();
                //basicInfo.FirstProcessTime = DateTime.Now;
                //basicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                //basicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                basicInfo.WorkorderCode = "W" + DateTime.Now.ToString("yyMMddhhmmssfff");
                basicInfo.ProcessStatus = 0;
                basicInfo.StatusForUser = 0;

                if (contactInfo != null)
                {
                    contactInfo.ContactId = GetGuid();
                    basicInfo.NowContactResult = contactInfo.Results;
                }

                if (Create(basicInfo) == 1)
                {
                    WorkorderProcessInfoModel proc = new WorkorderProcessInfoModel();

                    proc.ProcessId = GetGuid();
                    proc.WorkorderId = basicInfo.WorkOrderId;
                    proc.WorkorderTypeId = basicInfo.WorkorderType;
                    proc.Description = basicInfo.Description;
                    proc.BeforeStatus = basicInfo.NowStatusId;
                    proc.BeforeResult = basicInfo.NowResultId;
                    proc.BeforeUserId = SessionUtil.Current.UserId;
                    proc.AfterStatus = basicInfo.NowStatusId;
                    proc.AfterResult = basicInfo.NowResultId;
                    proc.AfterUserId = SessionUtil.Current.UserId;
                    if (contactInfo != null)
                    {
                        proc.RelContactId = contactInfo.ContactId;
                    }

                    if (WorkorderProcessInfoService.Instance.Create(proc) == 1)
                    {
                        if (contactInfo != null)
                        {
                            contactInfo.RelWorkorderId = basicInfo.WorkOrderId;
                            PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(contactInfo.CustomerPhone, false);
                            if (loc != null)
                            {
                                contactInfo.FromCityId = loc.ChinaId;
                                contactInfo.FromCityName = loc.City;
                            }

                            if (CustomerContactInfoService.Instance.Create(contactInfo) == 1)
                            {
                                CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
                                phoneInfo.CustomerId = contactInfo.CustomerId;
                                phoneInfo.PhoneNumber = contactInfo.CustomerPhone;

                                if (!CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(phoneInfo, out message))
                                {
                                    message = "添加客户联系号码失败";
                                    RollbackTransaction();
                                    result = false;
                                    return false;
                                }
                                CommitTransaction();
                                CustomerInfoService.Instance.GetCustomerDomainModelById(basicInfo.RelCustomerId, true);
                                message = "成功创建工单信息";
                                result = true;
                            }
                            else
                            {
                                RollbackTransaction();
                                message = "创建工单关联联系记录失败，请与管理员联系";
                                result = false;
                            }
                        }
                        else
                        {
                            CommitTransaction();
                            CustomerInfoService.Instance.GetCustomerDomainModelById(basicInfo.RelCustomerId, true);
                            message = "成功创建工单信息";
                            result = true;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "创建工单处理记录失败，请与管理员联系";
                        result = true;
                    }
                }
                else
                {
                    message = "保存工单基本信息失败，请与管理员联系";
                    return false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("创建工单异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 检查当前会话用户是否拥有关闭工单权限。
        /// </summary>
        /// <returns></returns>
        public bool HasCloseWorkOrderPremission()
        {
            return  PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "WorkOrderCenter", "DoCloseWorkOrder");
        }

        /// <summary>
        /// 检查当前会话用户是否拥有提交审批工单权限。
        /// </summary>
        /// <returns></returns>
        public bool HasSubmitApprovalWorkOrderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "WorkOrderCenter", "DoSubmitApprovalWorkOrder");
        }

        /// <summary>
        /// 检查当前会话用户是否拥有审批工单权限。
        /// </summary>
        /// <returns></returns>
        public bool HasApprovalWorkOrderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "WorkOrderCenter", "DoWorkOrderApproval");
        }

        /// <summary>
        /// 检查当前会话用户是否拥有提交质检工单权限。
        /// </summary>
        /// <returns></returns>
        public bool HasSubmitQualityCheckWorkOrderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "WorkOrderCenter", "DoSubmitQualityCheckedWorkOrder");
        }

        /// <summary>
        /// 检查当前会话用户是否拥有质检工单权限。
        /// </summary>
        /// <returns></returns>
        public bool HasQualityCheckWorkOrderPremission()
        {
            return PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, "WorkOrderCenter", "DoNewWorkOrderQualityChecked");
        }

        /// <summary>
        /// 修改工单过期时间。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="expiredTime"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ChangeWorkorderExpiredTime(List<string> workOrderIdList, DateTime expiredTime, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (expiredTime <= DateTime.Now)
                    {
                        message = "操作失败，设置的日期时间不能小于提交时的时间";
                        RollbackTransaction();
                        return false;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.AfterStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.AfterUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                    processInfo.Description = string.Format("本工单过期时间由【{0}（{1}）】改为【{2}】", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""),expiredTime.ToString());

                    workOrderDomainModel.BasicInfo.ExpiredTime = expiredTime;


                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "添加工单处理记录信息失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功更新工单过期时间";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("新建工单处理记录异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 修改工单预约时间。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="advanceTime"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ChangeWorkorderAdvanceTime(List<string> workOrderIdList, DateTime advanceTime, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (advanceTime <= DateTime.Now)
                    {
                        message = "操作失败，设置的日期时间不能小于提交时的时间";
                        RollbackTransaction();
                        return false;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.AfterStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.AfterUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                    processInfo.Description = string.Format("本工单预约时间由【{0}（{1}）】改为【{2}】", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), advanceTime.ToString());

                    workOrderDomainModel.BasicInfo.AdvanceTime = advanceTime;


                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "添加工单处理记录信息失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功更新工单预约时间";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("新建工单处理记录异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 直接关闭工单。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool CloseWorkOrder(List<string> workOrderIdList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.AfterStatus = typeDomainModel.EndStatusInfo.WorkorderStatusId;
                    processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.AfterUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                    processInfo.Description = string.Format("本工单由【{0}（{1}）】直接关闭", user.BasicInfo.CnName,user.BasicInfo.WorkId.Replace("WORKID_",""));

                    workOrderDomainModel.BasicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.EndStatusInfo.WorkorderStatusId;
                    workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 2;
                    workOrderDomainModel.BasicInfo.StatusForUser = 2;
                    workOrderDomainModel.BasicInfo.StatusCode = 0;


                    // 如果工单首次处理
                    if (workOrderDomainModel.BasicInfo.FirstProcessUserid == null || workOrderDomainModel.BasicInfo.FirstProcessTime == null)
                    {
                        workOrderDomainModel.BasicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                        workOrderDomainModel.BasicInfo.FirstProcessTime = DateTime.Now;
                    }

                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "添加工单处理记录信息失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功关闭当前工单";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("关闭工单异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 工单审批。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ApprovalWorkOrder(List<string> workOrderIdList, WorkOrderApprovalAction approvalAction, string assignedGroupId, string assignedUserId, string processDescription,  out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, true);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }
                    
                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    WorkorderProcessInfoModel lasestProcess = GetLasestWorkOrderProcessInfoModel(workOrderDomainModel.ProcessList);
                    if (lasestProcess == null)
                    {
                        message = "操作失败，当前工单还未开始处理";
                        RollbackTransaction();
                        return false;
                    }

                    switch (approvalAction)
                    {
                        case WorkOrderApprovalAction.Assignment:
                            workOrderDomainModel.BasicInfo.NowProcessUserid = assignedUserId;
                            workOrderDomainModel.BasicInfo.RelUsergroupId = assignedGroupId;
                            workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.BeginStatusInfo.WorkorderStatusId; // lasestProcess.AfterStatus;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 0;
                            workOrderDomainModel.BasicInfo.StatusForUser = 0;

                            processInfo.AfterStatus = typeDomainModel.BeginStatusInfo.WorkorderStatusId;;
                            processInfo.AfterResult = lasestProcess.AfterResult;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;

                            UserGroupPremissionDomainModel groupModel = UserGroupInfoService.Instance.GetUserGroupDomainByGroupId(assignedGroupId, false);
                            if (groupModel == null)
                            {
                                message = "操作失败，转交用户组ID不存在";
                                RollbackTransaction();
                                return false;
                            }

                            if (assignedUserId == null)
                            {
                                processInfo.Description = string.Format("【{0}（{1}）】审批本工单为未通过，并转交【{2}】处理。 {3}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), groupModel.UserGroupInfo.GroupName, processDescription);
                            }
                            else
                            {
                                UserDomainModel assignedUser = UserInfoService.Instance.GetUserDomainModelById(assignedUserId, false);
                                if (assignedUser == null)
                                {
                                    message = "失败失败，转交用户ID不存在";
                                    RollbackTransaction();
                                    return false;
                                }
                                processInfo.Description = string.Format("【{0}（{1}）】审批本工单为未通过，并转交【{2}（{3}）】处理。{4}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), assignedUser.BasicInfo.CnName, assignedUser.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                            }
                            break;

                        case WorkOrderApprovalAction.QualityChecked:
                            workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.QuilityCheckedStatusInfo.WorkorderStatusId;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                            workOrderDomainModel.BasicInfo.StatusForUser = 2;

                            processInfo.AfterStatus = typeDomainModel.QuilityCheckedStatusInfo.WorkorderStatusId;
                            processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;
                            processInfo.Description = string.Format("本工单由【{0}（{1}）】审批通过，并转交质检。{2}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                            break;

                        case WorkOrderApprovalAction.CloseWorkOrder:
                            processInfo.AfterStatus = typeDomainModel.EndStatusInfo.WorkorderStatusId;
                            processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;

                            workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                            workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                            workOrderDomainModel.BasicInfo.NowStatusId = processInfo.AfterStatus;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 2;
                            workOrderDomainModel.BasicInfo.StatusForUser = 2;
                            processInfo.Description = string.Format("本工单由【{0}（{1}）】审批通过，并关闭工单。{2}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                            break;

                        default:
                            break;
                    }

                    
                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;


                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "工单审批失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功工单审批";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("工单审批异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 工单质检。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool QualityCheckedWorkOrder(List<string> workOrderIdList, WorkOrderApprovalAction checkedAction, string assignedGroupId, string assignedUserId, string processDescription, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    WorkorderProcessInfoModel lasestProcess = GetLasestWorkOrderProcessInfoModel(workOrderDomainModel.ProcessList);
                    if (lasestProcess == null)
                    {
                        message = "操作失败，当前工单还未开始处理";
                        RollbackTransaction();
                        return false;
                    }

                    WorkorderChecksInfoModel checkInfo = new WorkorderChecksInfoModel();
                    checkInfo.WorkorderCheckId = GetGuid();
                    checkInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;

                    switch (checkedAction)
                    {
                        case WorkOrderApprovalAction.Assignment:
                            workOrderDomainModel.BasicInfo.NowProcessUserid = lasestProcess.AfterUserId;
                            workOrderDomainModel.BasicInfo.NowStatusId = lasestProcess.AfterStatus;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 0;
                            workOrderDomainModel.BasicInfo.StatusForUser = 0;
                            workOrderDomainModel.BasicInfo.QualityCheckStatus = 0;

                            processInfo.AfterStatus = lasestProcess.AfterStatus;
                            processInfo.AfterResult = lasestProcess.AfterResult;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;

                            checkInfo.CheckStatus = 0;

                            UserGroupPremissionDomainModel groupModel = UserGroupInfoService.Instance.GetUserGroupDomainByGroupId(assignedGroupId, false);
                            if (groupModel == null)
                            {
                                message = "操作失败，转交用户组ID不存在";
                                RollbackTransaction();
                                return false;
                            }

                            if (assignedUserId == null)
                            {
                                processInfo.Description = string.Format("【{0}（{1}）】质检评定为未通过，并转交【{2}】处理。 {3}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), groupModel.UserGroupInfo.GroupName, processDescription);
                                checkInfo.CheckDescription = processInfo.Description;
                            }
                            else
                            {
                                UserDomainModel assignedUser = UserInfoService.Instance.GetUserDomainModelById(assignedUserId, false);
                                if (assignedUser == null)
                                {
                                    message = "失败失败，转交用户ID不存在";
                                    RollbackTransaction();
                                    return false;
                                }
                                processInfo.Description = string.Format("【{0}（{1}）】质检评定为未通过，并转交【{2}（{3}）】处理。{4}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), assignedUser.BasicInfo.CnName, assignedUser.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                                checkInfo.CheckDescription = processInfo.Description;
                            }
                            break;

                        case WorkOrderApprovalAction.SubmitApproval:
                            workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                            workOrderDomainModel.BasicInfo.StatusForUser = 0;
                            workOrderDomainModel.BasicInfo.QualityCheckStatus = 1;

                            processInfo.AfterStatus = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                            processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;
                            processInfo.Description = string.Format("本工单由【{0}（{1}）】质检通过，并转交审批。{2}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                            
                            checkInfo.CheckDescription = processInfo.Description;
                            checkInfo.CheckStatus = 0;
                            break;

                        case WorkOrderApprovalAction.CloseWorkOrder:
                            processInfo.AfterStatus = typeDomainModel.EndStatusInfo.WorkorderStatusId;
                            processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                            processInfo.AfterUserId = SessionUtil.Current.UserId;

                            workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                            workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                            workOrderDomainModel.BasicInfo.ProcessStatus = 2;
                            workOrderDomainModel.BasicInfo.StatusForUser = 2;
                            workOrderDomainModel.BasicInfo.NowResultId = typeDomainModel.EndStatusInfo.WorkorderStatusId;
                            workOrderDomainModel.BasicInfo.QualityCheckStatus = 1;

                            processInfo.Description = string.Format("本工单由【{0}（{1}）】质检通过，并关闭工单。{2}", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""), processDescription);
                            
                            checkInfo.CheckDescription = processInfo.Description;
                            checkInfo.CheckStatus = 0;
                            break;

                        default:
                            break;
                    }


                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;


                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            if (WorkorderChecksInfoService.Instance.Create(checkInfo) == 1)
                            {
                                GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                                CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                            }
                            else
                            {
                                RollbackTransaction();
                                message = "插入工单质检记录失败，请与管理员联系";
                                return false;
                            }
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "插入工单处理记录失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功提交工单质检信息";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("工单质检异常", ex);
                throw ex;
            }

            return result;
        }


        public bool SubmitApprovalWorkOrder(List<string> workOrderIdList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (workOrderDomainModel.BasicInfo.NowStatusId == typeDomainModel.ApprovalStatusInfo.WorkorderStatusId)
                    {
                        continue;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    workOrderDomainModel.BasicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                    workOrderDomainModel.BasicInfo.StatusForUser = 1;
                    workOrderDomainModel.BasicInfo.StatusCode = 0;

                    processInfo.AfterStatus = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                    processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.AfterUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                    processInfo.Description = string.Format("本工单由【{0}（{1}）】提交审批", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""));


                    // 如果工单首次处理
                    if (workOrderDomainModel.BasicInfo.FirstProcessUserid == null || workOrderDomainModel.BasicInfo.FirstProcessTime == null)
                    {
                        workOrderDomainModel.BasicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                        workOrderDomainModel.BasicInfo.FirstProcessTime = DateTime.Now;
                    }

                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "提交工单审批失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功提交工单审批";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("提交工单审批异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 提交工单质检。
        /// </summary>
        /// <param name="workOrderIdList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool SubmitQualityCheckedWorkOrder(List<string> workOrderIdList, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = null;
            WorkOrderTypeDomainModel typeDomainModel = null;
            UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

            try
            {
                BeginTransaction();

                foreach (string workOrderId in workOrderIdList)
                {
                    workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
                    typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);

                    if (workOrderDomainModel == null)
                    {
                        message = "操作失败，不存在的工单ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (typeDomainModel == null)
                    {
                        message = "操作失败，不存在的工单类型ID";
                        RollbackTransaction();
                        return false;
                    }

                    if (workOrderDomainModel.BasicInfo.NowStatusId == typeDomainModel.QuilityCheckedStatusInfo.WorkorderStatusId)
                    {
                        continue;
                    }

                    WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
                    processInfo.ProcessId = GetGuid();
                    processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                    processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    workOrderDomainModel.BasicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.NowStatusId = typeDomainModel.QuilityCheckedStatusInfo.WorkorderStatusId;
                    workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                    workOrderDomainModel.BasicInfo.StatusForUser = 1;
                    workOrderDomainModel.BasicInfo.StatusCode = 0;

                    processInfo.AfterStatus = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                    processInfo.AfterResult = workOrderDomainModel.BasicInfo.NowResultId;
                    processInfo.AfterUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;

                    processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                    processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                    processInfo.Description = string.Format("本工单由【{0}（{1}）】提交质检", user.BasicInfo.CnName, user.BasicInfo.WorkId.Replace("WORKID_", ""));


                    // 如果工单首次处理
                    if (workOrderDomainModel.BasicInfo.FirstProcessUserid == null || workOrderDomainModel.BasicInfo.FirstProcessTime == null)
                    {
                        workOrderDomainModel.BasicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                        workOrderDomainModel.BasicInfo.FirstProcessTime = DateTime.Now;
                    }

                    if (Update(workOrderDomainModel.BasicInfo) == 1)
                    {
                        if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                        {
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                        }
                        else
                        {
                            RollbackTransaction();
                            message = "提交工单质检失败，请与管理员联系";
                            return false;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "更新工单信息失败，请与管理员联系";
                        return false;
                    }
                }

                CommitTransaction();
                message = "成功提交工单质检";
                result = true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("提交工单质检异常", ex);
                throw ex;
            }

            return result;
        }


        public bool CreateNewWorkorderProcessRecord(string workOrderId,string advance_time, bool closeOrder, string assignedUserGroupId, string assignedUserId, WorkorderProcessInfoModel processInfo, CustomerContactInfoModel contactInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            WorkOrderDomainModel workOrderDomainModel = GetWorkorderDomainModelById(workOrderId, false);
            WorkOrderTypeDomainModel typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(workOrderDomainModel.BasicInfo.WorkorderType, false);
            if (workOrderDomainModel == null)
            {
                message = "操作失败，不存在的工单ID";
                return false;
            }

            if (typeDomainModel == null)
            {
                message = "操作失败，不存在的工单类型ID";
                return false;
            }

            if (processInfo == null)
            {
                message = "操作失败，参数错误，请与管理员联系";
                return false;
            }

            try
            {
                processInfo.ProcessId = GetGuid();
                processInfo.BeforeResult = workOrderDomainModel.BasicInfo.NowResultId;
                processInfo.BeforeStatus = workOrderDomainModel.BasicInfo.NowStatusId;
                processInfo.BeforeUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;
                processInfo.WorkorderTypeId = workOrderDomainModel.BasicInfo.WorkorderType;
                processInfo.WorkorderId = workOrderDomainModel.BasicInfo.WorkOrderId;
                
                workOrderDomainModel.BasicInfo.NowProcessUserid = SessionUtil.Current.UserId;
                workOrderDomainModel.BasicInfo.NowStatusId = processInfo.AfterStatus;
                workOrderDomainModel.BasicInfo.NowResultId = processInfo.AfterResult;
                workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                workOrderDomainModel.BasicInfo.StatusForUser = 1;
                workOrderDomainModel.BasicInfo.StatusCode = 0;


                // 如果要关闭工单
                if (closeOrder || typeDomainModel.StatusList[processInfo.AfterStatus].StatusTag == 2)
                {
                    workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                    workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 2;
                    workOrderDomainModel.BasicInfo.StatusForUser = 2;
                }

                // 如果工单首次处理
                if (workOrderDomainModel.BasicInfo.FirstProcessUserid == null || workOrderDomainModel.BasicInfo.FirstProcessTime == null)
                {
                    workOrderDomainModel.BasicInfo.FirstProcessUserid = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.FirstProcessTime = DateTime.Now;
                }

                // 如果转交到组
                if (assignedUserGroupId != null)
                {
                    workOrderDomainModel.BasicInfo.RelUsergroupId = assignedUserGroupId;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 0;
                    workOrderDomainModel.BasicInfo.StatusForUser = 0;

                    if (assignedUserId == null)
                    {
                        workOrderDomainModel.BasicInfo.NowProcessUserid = null;
                    }
                }

                // 如果转交到用户
                if (assignedUserId != null)
                {
                    workOrderDomainModel.BasicInfo.NowProcessUserid = assignedUserId;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 0;
                    workOrderDomainModel.BasicInfo.StatusForUser = 0;
                }



                #region 加入根据处理结果自动设置状态逻辑

                if (typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "成功"
                    || typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "结束")
                {
                    processInfo.AfterStatus = typeDomainModel.ApprovalStatusInfo.WorkorderStatusId;
                    
                    //workOrderDomainModel.BasicInfo.ClosedTime = DateTime.Now;
                    //workOrderDomainModel.BasicInfo.ClosedUser = SessionUtil.Current.UserId;
                    workOrderDomainModel.BasicInfo.ProcessStatus = 1;
                    workOrderDomainModel.BasicInfo.StatusForUser = 0;
                    workOrderDomainModel.BasicInfo.NowStatusId = processInfo.AfterStatus;
                }

                #endregion   

                BeginTransaction();

                if (Update(workOrderDomainModel.BasicInfo) == 1)
                {
                    if (!string.IsNullOrEmpty(advance_time))
                    {
                        WorkorderInfoModel workordermodel = new WorkorderInfoModel();
                        workordermodel.AdvanceTime = Convert.ToDateTime(advance_time);
                        workordermodel.WorkOrderId = workOrderId;
                        if (WorkorderInfoService.Instance.Update(workordermodel) == 1)
                        {
                            message = "成功修改预约时间";
                        }
                    
                    }
                    if (contactInfo.CustomerPhone != "")
                    {
                        CustomerPhoneInfoModel phoneInfo = new CustomerPhoneInfoModel();
                        phoneInfo.CustomerId = contactInfo.CustomerId;
                        phoneInfo.PhoneNumber = contactInfo.CustomerPhone;
                        if (CustomerPhoneInfoService.Instance.CreateCustomerPhoneInfo(phoneInfo, out message))
                        {
                            message = "成功创建客户联系记录";
                        }
                    }
                    // 工单转交处理。
                    if (assignedUserGroupId != null && assignedUserId == null)
                    {
                        string updateNowProcessUserIdSQL = "UPDATE workorder_info SET now_process_userid = NULL WHERE work_order_id = $work_order_id$";
                        ParameterCollection pc = new ParameterCollection();
                        pc.Add("work_order_id", workOrderDomainModel.BasicInfo.WorkOrderId);

                        if (ExecuteNonQuery(updateNowProcessUserIdSQL, pc) != 1)
                        {
                            RollbackTransaction();
                            message = "操作失败，更新工单信息失败";
                            return false;
                        }
                        UserGroupPremissionDomainModel groupInfo = UserGroupInfoService.Instance.GetUserGroupDomainByGroupId(assignedUserGroupId, false);
                        UserDomainModel currUser = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);
                        processInfo.Description = string.Format("本工单由【{0}（{1}）】转交【{2}】处理。{3}", currUser.BasicInfo.CnName, currUser.BasicInfo.WorkId.Replace("WORKID_", ""), groupInfo.UserGroupInfo.GroupName, processInfo.Description);

                    }

                    if (assignedUserGroupId != null && assignedUserId != null)
                    {
                        UserGroupPremissionDomainModel groupInfo = UserGroupInfoService.Instance.GetUserGroupDomainByGroupId(assignedUserGroupId, false);
                        UserDomainModel assignedUser = UserInfoService.Instance.GetUserDomainModelById(assignedUserId, false);
                        UserDomainModel currUser = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);

                        processInfo.Description = string.Format("本工单由【{0}（{1}）】转交【{2}（{3}）】处理。{4}", currUser.BasicInfo.CnName, currUser.BasicInfo.WorkId.Replace("WORKID_", ""), assignedUser.BasicInfo.CnName, assignedUser.BasicInfo.WorkId.Replace("WORKID_", ""), processInfo.Description);
                    }

                    if (WorkorderProcessInfoService.Instance.Create(processInfo) == 1)
                    {
                        if (contactInfo != null)
                        {
                            PhoneLocationInfoModel loc = PhoneLocationInfoService.Instance.GetLocationInfo(contactInfo.CustomerPhone, false);
                            if (loc != null)
                            {
                                contactInfo.FromCityId = loc.ChinaId;
                                contactInfo.FromCityName = loc.City;
                            }

                            if (CustomerContactInfoService.Instance.Create(contactInfo) != 1)
                            {
                                RollbackTransaction();
                                message = "添加工单联系记录失败，请与管理员联系";
                                result = false;
                            }
                            else
                            {
                                CommitTransaction();
                                message = "成功添加工单处理记录";
                                GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                                CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                                result = true;
                            }
                        }
                        else
                        {
                            CommitTransaction();
                            message = "成功添加工单处理记录";
                            GetWorkorderDomainModelById(workOrderDomainModel.BasicInfo.WorkOrderId, true);
                            CustomerInfoService.Instance.GetCustomerDomainModelById(workOrderDomainModel.BasicInfo.RelCustomerId, true);
                            result = true;
                        }
                    }
                    else
                    {
                        RollbackTransaction();
                        message = "添加工单处理记录信息失败，请与管理员联系";
                        result = false;
                    }
                }
                else
                {
                    RollbackTransaction();
                    message = "更新工单信息失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                LogUtil.Error("新建工单处理记录异常", ex);
                throw ex;
            }

            return result;
        }

        public WorkorderProcessInfoModel GetLasestWorkOrderProcessInfoModel(Dictionary<string, WorkorderProcessInfoModel> source)
        {
            if (source == null)
                return null;
            if (source.Count == 0)
                return null;

            List<WorkorderProcessInfoModel> list = new List<WorkorderProcessInfoModel>();
            foreach (WorkorderProcessInfoModel item in source.Values)
            {
                list.Add(item);
            }

            return list[list.Count - 1];
        }
	}
}

