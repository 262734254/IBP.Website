/*
版权信息：版权所有(C) 2011，JofoInfo Tech
作    者：周强
完成日期：2011-12-22
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
	/// AutoDialerTaskInfo业务逻辑类
	/// </summary>
	public partial class AutoDialerTaskInfoService : BaseService
	{
		// 在此添加你的代码...

        /// <summary>
        /// 获取自动外呼任务总数。
        /// </summary>
        /// <returns></returns>
        public int GetGetAutoDialerTaskTotal()
        {
            string sql = @"select count(1) from auto_dialer_task_info";
            return Convert.ToInt32(ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取指定自动外呼任务结果信息。
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public AutoDialerTaskResultDomainModel GetAutoDialerTaskResult(string taskId)
        {
            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);
            if (taskInfo == null)
            {
                throw new Exception("获取自动外呼任务结果信息异常，任务ID不存在");
            }

            AutoDialerTaskResultDomainModel result = new AutoDialerTaskResultDomainModel();
            result.IVRProjectId = Convert.ToInt32(taskInfo.IvrDialerProjectId);
            result.TaskId = taskInfo.AutoDialerTaskId;
            result.PlanningNumberTotal = Convert.ToInt32(taskInfo.DialerNumberTotal);


            string sql = @"
select count(1) from DIALER_INFO  where free_id = $projectId$ ;
select dialerResult,count(1) from Office_AutoCalling where freeid = $projectId$ group by dialerResult;
SELECT remark, count(1) FROM [DIALER_INFO_HIS]  where free_id = $projectId$ group by remark;
";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("projectId", taskInfo.IvrDialerProjectId);

            DataSet ds = DbUtil.IVRDBManager.IData.ExecuteDataSet(sql, pc);
            if (ds != null && ds.Tables.Count == 3)
            {
                result.IVRSurplusNumberTotal = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                                
                result.ReturnCodeList = new Dictionary<string, string>();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    result.ReturnCodeList.Add(ds.Tables[1].Rows[i][0].ToString(), ds.Tables[1].Rows[i][1].ToString());
                }
                result.OutDialerStatusList = new Dictionary<string, string>();
                int counter = 0;
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    result.OutDialerStatusList.Add(ds.Tables[2].Rows[i][0].ToString(), ds.Tables[2].Rows[i][1].ToString());
                    counter += Convert.ToInt32(ds.Tables[2].Rows[i][1]);
                }

                result.OutDialerNumberTotal = counter;
            }

            return result;
        }

        /// <summary>
        /// 获取指定页自动外呼任务。
        /// </summary>
        /// <param name="taskCode"></param>
        /// <param name="taskStatus"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <param name="orderDirection"></param>
        /// <returns></returns>
        public List<AutoDialerTaskInfoModel> GetAutoDialerTaskList(string taskCode, string taskStatus, int pageIndex, int pageSize, string orderField, string orderDirection)
        {
            List<AutoDialerTaskInfoModel> list = null;

            ParameterCollection pc = new ParameterCollection();
            OrderByCollection obc = OrderByCollection.Create(orderField, orderDirection);

            string sql = string.Format("SELECT * FROM auto_dialer_task_info WHERE 1 = 1 {0} {1} ",
                string.IsNullOrEmpty(taskCode) ? "" : " AND auto_dialer_task_code = $taskCode$ ",
                string.IsNullOrEmpty(taskStatus) ? "" : " AND status = $taskStatus$"
                );

            if (!string.IsNullOrEmpty(taskCode))
            {
                pc.Add("taskCode", taskCode);
            }
            if (!string.IsNullOrEmpty(taskStatus))
            {
                pc.Add("taskStatus", taskStatus);
            }
            
            DataTable dt = ExecuteDataTable(sql, pc, pageIndex, pageSize,obc);

            list = ModelConvertFrom<AutoDialerTaskInfoModel>(dt);

            return list;
        }

        /// <summary>
        /// 执行自动外呼任务。
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
//        public bool RunAutoDialerTask(string taskId, out string message)
//        {
//            bool result = false;
//            message = "操作失败，请与管理员联系";

//            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);

//            if (taskInfo == null)
//            {
//                message = "操作失败，当前任务ID不存在";
//                return false;
//            }

//            if (taskInfo.Status != 0)
//            {
//                message = "操作失败，当前任务已经执行或完成";
//                return false;
//            }

//            if (taskInfo.DialerNumberTotal == 0)
//            {
//                message = "操作失败，当前任务外呼号码未添加";
//                return false;
//            }

//            #region 往TF_CMS数据库中插入数据

//            List<AutoDialerTaskNumberInfoModel> numberList = GetAutoDialerTaskNumberList(taskInfo.AutoDialerTaskId);
//            if (numberList != null && numberList.Count > 0)
//            {
//                string insertSql = @"
//INSERT INTO [DIALER_INFO]
//           ([DIALER_ID]
//           ,[PHONE]
//           ,[DIALER_TYPE]
//           ,[BEGIN_TIME]
//           ,[INTERVAL]
//           ,[TIMES]
//           ,[GENERATE_TIME]
//           ,[AGENT_ID]
//           ,[PRIORITY]
//           ,[LOCK_ID]
//           ,[LOCK_TIME]
//           ,[LOCK_PORT]
//           ,[FREE_ID]
//           ,[TRANSFER_AGENT]
//           ,[FILE_NAME]
//           ,[TTS_TEXT]
//           ,[CONTENT]
//           ,[EVENT_ID]
//           ,[MONEY_DATE]
//           ,[TOTAL_MONEY]
//           ,[NBH])
//     VALUES
//           ($numberId$,								-- 外拨任务编号
//            $number$,			                    -- 外拨号码
//            '',                                     -- 1,                                  	-- 外拨类型
//            $begintime$,                    		-- 外拨开始时间
//            $interval$,                     		-- 外拨时间间隔
//            $trycount$,                     		-- 外拨失败重试次数
//            GETDATE(),                          	-- 外拨任务添加时间，可默认为记录添加当前的系统时间
//            '',                                     -- null,                           		-- 座席ID(预留字段)
//            $priority$,                 			-- 优先级（需赋初值，1即可）
//            0,                      				-- 是否锁定（需赋初值，0即可）
//            '1900-1-1',                     		-- 任务锁定时间（需赋初值，1900-1-1即可）
//            -1,                                     -- null,                           		-- 锁定端口
//            1,                                      -- $taskId$,                       		-- 对应的外拨任务ProjectID
//            '0',                                	-- 是否转接座席
//            null,                               	-- 任务关联文件（预留字段）
//            null,                               	-- 外拨成功播放的TTS内容（预留字段）
//            null,                           		-- 任务关联文字内容
//            null,                               	-- 事件编号（预留字段）
//            null,                           		-- 计费日期（预留字段）
//            null,                               	-- 总费用（预留字段）
//            0)                          			-- 每小时费用（预留字段，需赋初值，0即可）
//
//";

//                try
//                {
//                    DbUtil.IVRDBManager.BeginTransaction();

//                    ParameterCollection ivrpc = new ParameterCollection();
//                    foreach (AutoDialerTaskNumberInfoModel numberInfo in numberList)
//                    {
//                        ivrpc.Clear();
//                        ivrpc.Add("numberId", numberInfo.AutoDialerNumberId);
//                        ivrpc.Add("number", "9" + numberInfo.DialerNumber);
//                        ivrpc.Add("begintime", taskInfo.BeginTime);
//                        ivrpc.Add("interval", taskInfo.Interval);
//                        ivrpc.Add("trycount", taskInfo.RetryCount);
//                        ivrpc.Add("priority", taskInfo.Priority);
//                        ivrpc.Add("taskId", taskInfo.AutoDialerTaskId);

//                        if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(insertSql, ivrpc) != 1)
//                        {
//                            RollbackTransaction();
//                            message = "操作失败，往IVR系统数据库传递自动外呼号码失败";
//                            return false;
//                        }
//                    }

//                    taskInfo.Status = 1;
//                    if (Update(taskInfo) == 1)
//                    {
//                        DbUtil.IVRDBManager.CommitTransaction();
//                        message = "成功启动该自动外呼任务";
//                        result = true;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    DbUtil.IVRDBManager.RollbackTransaction();
//                    message = "操作失败，往IVR系统数据库传递自动外呼号码异常";
//                    LogUtil.Error("往IVR系统数据库传递自动外呼号码异常", ex);
//                    return false;
//                }
//            }

//            #endregion


//            return result;
//        }

        /// <summary>
        /// 创建自动外呼任务。
        /// </summary>
        /// <param name="taskInfo"></param>
        /// <returns></returns>
        public bool CreateNewAutoDialerTask(AutoDialerTaskInfoModel taskInfo, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            taskInfo.AutoDialerTaskId = Guid.NewGuid().ToString();
            taskInfo.AutoDialerTaskCode = "ADT-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            taskInfo.DialerNumberTotal = 0;
            taskInfo.Status = 0;

            //if (taskInfo.BeginTime < DateTime.Now)
            //{
            //    message = "操作失败，开始外呼时间不得早于当前时间";
            //    return false;
            //}

            if (taskInfo.Interval < 1)
            {
                taskInfo.Interval = 1;
            }

            string insSql = @"
INSERT INTO [Dialer_Info_Project]
           ([ProjectName],[ProjectCmt],[CampaignID],[Status],[Start_Date],[End_Date],[Start_Time1],[End_Time1],[Start_Time2],[End_Time2],[Start_Time3],[End_Time3],[Start_Time4],[End_Time4],[Creator],[Modifier],[Create_Time],[Modify_Time],[Create_IP],[Modify_IP])
     VALUES
           ($projectName$,                                  -- <ProjectName, nvarchar(50),>
            $description$,                                  -- <ProjectCmt, nvarchar(50),>
            1,                                              -- <CampaignID, int,>
            'PAUSE',                                        -- <Status, nvarchar(50),>
            CONVERT(varchar(100), $beginTime$, 20),         -- <Start_Date, nvarchar(50),>
            CONVERT(varchar(100), $endTime$, 20),           -- <End_Date, nvarchar(50),>
            $startTime1$,                                   -- <Start_Time1, nvarchar(50),>
            $stopTime1$,                                    -- <End_Time1, nvarchar(50),>
            $startTime2$,                                   -- <Start_Time2, nvarchar(50),>
            $stopTime2$,                                    -- <End_Time2, nvarchar(50),>
            $startTime3$,                                   -- <Start_Time3, nvarchar(50),>
            $stopTime3$,                                    -- <End_Time3, nvarchar(50),>
            $startTime4$,                                   -- <Start_Time4, nvarchar(50),>
            $stopTime4$,                                    -- <End_Time4, nvarchar(50),>
            $creator$,                                      -- <Creator, nvarchar(50),>
            $modifier$,                                     -- <Modifier, nvarchar(50),>
            $createdOn$,                                    -- <Create_Time, nvarchar(50),>
            $modifiedOn$,                                   -- <Modify_Time, nvarchar(50),>
            $create_ip$,                                    -- <Create_IP, nvarchar(50),>
            $modified_ip$                                   -- <Modify_IP, nvarchar(50),>
)";

            ParameterCollection pc = new ParameterCollection();
            pc.Add("projectName", taskInfo.AutoDialerTaskName);
            pc.Add("description", taskInfo.Description);
            pc.Add("beginTime", Convert.ToDateTime(taskInfo.BeginTime));
            pc.Add("endTime", Convert.ToDateTime(taskInfo.EndTime));

            pc.Add("startTime1", taskInfo.StartTime1);
            pc.Add("startTime2", taskInfo.StartTime2);
            pc.Add("startTime3", taskInfo.StartTime3);
            pc.Add("startTime4", taskInfo.StartTime4);
            pc.Add("stopTime1", taskInfo.StopTime1);
            pc.Add("stopTime2", taskInfo.StopTime2);
            pc.Add("stopTime3", taskInfo.StopTime3);
            pc.Add("stopTime4", taskInfo.StopTime4);

            pc.Add("creator", SessionUtil.Current.UserId);
            pc.Add("modifier", taskInfo.AutoDialerTaskId);

            pc.Add("createdOn", DateTime.Now);
            pc.Add("modifiedOn", DateTime.Now);
            pc.Add("create_ip", SessionUtil.GetClientIPAddress());
            pc.Add("modified_ip", SessionUtil.GetClientIPAddress());

            try
            {
                DbUtil.IVRDBManager.BeginTransaction();
                if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(insSql, pc) > 0)
                {
                    string getIdSql = @"SELECT ProjectID FROM  Dialer_Info_Project WHERE Modifier = $modifier$";
                    pc.Clear();
                    pc.Add("modifier", taskInfo.AutoDialerTaskId);

                    object ivrProjectId = DbUtil.IVRDBManager.IData.ExecuteScalar(getIdSql, pc);
                    if (ivrProjectId == null)
                    {
                        DbUtil.IVRDBManager.RollbackTransaction();                        
                        message = "获取IVR系统自动外呼任务ID失败，请与管理员联系";
                        return false;
                    }

                    taskInfo.IvrDialerProjectId = Convert.ToInt32(ivrProjectId);

                    if (Create(taskInfo) == 1)
                    {
                        DbUtil.IVRDBManager.CommitTransaction();
                        result = true;
                        message = "成功报建自动外呼任务";
                    }
                    else
                    {
                        DbUtil.IVRDBManager.RollbackTransaction();
                        result = false;
                        message = "报建IBP系统自动外呼任务失败，请与管理员联系";
                    }
                }
                else
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                    result = false;
                    message = "创建IVR系统自动外呼任务失败，请与管理员联系";
                }
            }
            catch (Exception ex)
            {
                DbUtil.IVRDBManager.RollbackTransaction();
                LogUtil.Error("创建自动外呼任务异常", ex);
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取指定自动外呼任务号码列表。
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<AutoDialerTaskNumberInfoModel> GetAutoDialerTaskNumberList(string taskId)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("autodialer_task_id", taskId);

            return AutoDialerTaskNumberInfoService.Instance.RetrieveMultiple(pc);
        }

        /// <summary>
        /// 获取IVR系统自动外呼条目列表。
        /// </summary>
        /// <param name="ivrProjectId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetIVRAutoDialerTaskNumberListByProjectId(int ivrProjectId)
        {
            Dictionary<string, string> result = null;

            string sql = @"select dialer_id, phone from DIALER_INFO where free_id = $projectId$";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("projectId", ivrProjectId);

            DataTable dt = DbUtil.IVRDBManager.IData.ExecuteDataTable(sql, pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new Dictionary<string, string>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.Add(dt.Rows[i]["dialer_id"].ToString(), dt.Rows[i]["phone"].ToString().Substring(1, dt.Rows[i]["phone"].ToString().Length - 1));
                }
            }

            return result;
        }

        /// <summary>
        /// 更新自动外呼任务状态。
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskStatus"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateAutoDialerTaskStatus(string taskId, int taskStatus, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);
            if (taskInfo == null)
            {
                message = "操作失败，当前任务ID不存在";
                return false;
            }

            if (taskInfo.Status == 0 && taskStatus == 0)
            {
                message = "操作失败，当前任务已处于停止状态";
                return false;
            }

            if (taskInfo.Status == 1 && taskStatus == 1)
            {
                message = "操作失败，当前任务已处于运行状态";
                return false;
            }

            if (taskInfo.Status == 2 && taskStatus == 2)
            {
                message = "操作失败，当前任务已处于暂停状态";
                return false;
            }

            try
            {
                DbUtil.IBPDBManager.BeginTransaction();
                DbUtil.IVRDBManager.BeginTransaction();

                taskInfo.Status = taskStatus;

                string updateSql = @"UPDATE Dialer_Info_Project SET [Status] = $taskStatus$ WHERE ProjectID = $ProjectID$";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("ProjectID", taskInfo.IvrDialerProjectId);

                switch (taskStatus)
                {
                    case 0:
                        pc.Add("taskStatus", "FINISHED");
                        break;

                    case 1:
                        pc.Add("taskStatus", "RUNNING");
                        break;

                    case 2:
                        pc.Add("taskStatus", "PAUSE");
                        break;

                    default:
                        break;
                }

                if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(updateSql, pc) == 1)
                {
                    if (Update(taskInfo) == 1)
                    {
                        DbUtil.IVRDBManager.CommitTransaction();
                        DbUtil.IBPDBManager.CommitTransaction();
                        result = true;
                        message = "成功更新当前任务状态";
                    }
                    else
                    {
                        DbUtil.IVRDBManager.RollbackTransaction();
                        DbUtil.IBPDBManager.RollbackTransaction();
                        result = false;
                        message = "更新IBP系统自动外呼任务状态失败，请与管理员联系";
                    }
                }
                else
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                    DbUtil.IBPDBManager.RollbackTransaction();
                    result = false;
                    message = "更新IVR系统自动外呼任务状态失败，请与管理员联系";
                }
            }
            catch(Exception ex)
            {
                DbUtil.IVRDBManager.RollbackTransaction();
                DbUtil.IBPDBManager.RollbackTransaction();
                LogUtil.Error("更新自动外呼任务状态异常", ex);
                result = false;
                message = "更新自动外呼任务状态异常，请与管理员联系";
            }

            return result;
        }

        /// <summary>
        /// 添加自动外呼任务外呼号码条目。
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="numberList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddAutoDialerTaskNumbers(string taskId, List<string> numberList, out int nowTotal, out string message)
        {
            bool result = false;
            nowTotal = 0;
            message = "操作失败，请与管理员联系";

            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);

            if (taskInfo == null)
            {
                message = "操作失败，当前任务ID不存在";
                return false;
            }

            nowTotal = Convert.ToInt32(taskInfo.DialerNumberTotal);

            if (taskInfo.Status == 1)
            {
                message = "操作失败，当前任务正在执行中";
                return false;
            }

            #region 往TF_CMS数据库中插入数据

            if (numberList != null && numberList.Count > 0)
            {
                string insertSql = @"
INSERT INTO [DIALER_INFO]
           ([DIALER_ID]
           ,[PHONE]
           ,[DIALER_TYPE]
           ,[BEGIN_TIME]
           ,[INTERVAL]
           ,[TIMES]
           ,[GENERATE_TIME]
           ,[AGENT_ID]
           ,[PRIORITY]
           ,[LOCK_ID]
           ,[LOCK_TIME]
           ,[LOCK_PORT]
           ,[FREE_ID]
           ,[TRANSFER_AGENT]
           ,[FILE_NAME]
           ,[TTS_TEXT]
           ,[CONTENT]
           ,[EVENT_ID]
           ,[MONEY_DATE]
           ,[TOTAL_MONEY]
           ,[NBH])
     VALUES
           ($numberId$,								-- 外拨任务编号
            $number$,			                    -- 外拨号码
            '',                                     -- 1,                                  	-- 外拨类型
            $begintime$,                    		-- 外拨开始时间
            $interval$,                     		-- 外拨时间间隔
            $trycount$,                     		-- 外拨失败重试次数
            GETDATE(),                          	-- 外拨任务添加时间，可默认为记录添加当前的系统时间
            '',                                     -- null,                           		-- 座席ID(预留字段)
            $priority$,                 			-- 优先级（需赋初值，1即可）
            0,                      				-- 是否锁定（需赋初值，0即可）
            '1900-1-1',                     		-- 任务锁定时间（需赋初值，1900-1-1即可）
            -1,                                     -- null,                           		-- 锁定端口
            $projectId$,                            -- 对应的外拨任务ProjectID
            '0',                                	-- 是否转接座席
            null,                               	-- 任务关联文件（预留字段）
            null,                               	-- 外拨成功播放的TTS内容（预留字段）
            null,                           		-- 任务关联文字内容
            null,                               	-- 事件编号（预留字段）
            null,                           		-- 计费日期（预留字段）
            null,                               	-- 总费用（预留字段）
            0)                          			-- 每小时费用（预留字段，需赋初值，0即可）

";

                try
                {
                    DbUtil.IBPDBManager.BeginTransaction();
                    DbUtil.IVRDBManager.BeginTransaction();

                    AutoDialerTaskNumberInfoModel ibpNumberInfo = null;

                    ParameterCollection ivrpc = new ParameterCollection();
                    int counter = 0;

                    foreach (string numberInfo in numberList)
                    {
                        if (string.IsNullOrEmpty(numberInfo))
                        {
                            continue;
                        }
                        ibpNumberInfo = new AutoDialerTaskNumberInfoModel();
                        ibpNumberInfo.AutoDialerNumberId = Guid.NewGuid().ToString();
                        ibpNumberInfo.AutodialerTaskId = taskInfo.AutoDialerTaskId;
                        ibpNumberInfo.IvrDialerProjectId = taskInfo.IvrDialerProjectId;
                        ibpNumberInfo.DialerNumber = numberInfo.Replace("\r","").Replace("\n","").Replace("\t","");

                        if (AutoDialerTaskNumberInfoService.Instance.Create(ibpNumberInfo) == 1)
                        {
                            ivrpc.Clear();
                            ivrpc.Add("numberId", ibpNumberInfo.AutoDialerNumberId);
                            ivrpc.Add("number",  ibpNumberInfo.DialerNumber);
                            ivrpc.Add("begintime", taskInfo.BeginTime);
                            ivrpc.Add("interval", taskInfo.Interval);
                            ivrpc.Add("trycount", taskInfo.RetryCount);
                            ivrpc.Add("priority", taskInfo.Priority);
                            ivrpc.Add("projectId", taskInfo.IvrDialerProjectId);

                            if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(insertSql, ivrpc) != 1)
                            {
                                DbUtil.IBPDBManager.RollbackTransaction();
                                DbUtil.IVRDBManager.RollbackTransaction();
                                message = "操作失败，往IVR系统数据库传递自动外呼号码失败";
                                return false;
                            }

                            counter++;
                        }
                        else
                        {
                            DbUtil.IBPDBManager.RollbackTransaction();
                            DbUtil.IVRDBManager.RollbackTransaction();
                            message = "操作失败，往IBP系统数据库传递自动外呼号码失败";
                            return false;
                        }
                    }

                    string totalSql = @"SELECT COUNT(1) FROM auto_dialer_task_number_info WHERE autodialer_task_id = $taskId$";
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("taskId", taskInfo.AutoDialerTaskId);

                    nowTotal = Convert.ToInt32(DbUtil.IBPDBManager.IData.ExecuteScalar(totalSql, pc));

                    string updateSql = "UPDATE auto_dialer_task_info SET dialer_number_total = $total$ WHERE auto_dialer_task_id =  $taskId$";
                    pc.Add("total", nowTotal);

                    if (DbUtil.IBPDBManager.IData.ExecuteNonQuery(updateSql, pc) == 1)
                    {
                        DbUtil.IBPDBManager.CommitTransaction();
                        DbUtil.IVRDBManager.CommitTransaction();

                        message = string.Format("成功插入自动外呼号码共 {0} 行", counter);
                        result = true;
                    }
                    else
                    {
                        DbUtil.IBPDBManager.RollbackTransaction();
                        DbUtil.IVRDBManager.RollbackTransaction();
                        message = "操作失败，更新IBP系统自动外呼号码总数失败";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                    DbUtil.IBPDBManager.RollbackTransaction();
                    message = "操作失败，往IVR系统数据库传递自动外呼号码异常";
                    LogUtil.Error("往IVR系统数据库传递自动外呼号码异常", ex);
                    return false;
                }
            }

            #endregion


            return result;
        }

        public bool DeleteAutoDialerTask(string taskId, out string message)
        {
            bool result = false;
            message = "操作失败，请与管理员联系";

            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);

            if (taskInfo == null)
            {
                message = "操作失败，当前任务ID不存在";
                return false;
            }

            if (taskInfo.Status == 1)
            {
                message = "操作失败，当前任务正在执行中";
                return false;
            }

            #region 删除TF_CMS数据库中数据

            try
            {
                DbUtil.IBPDBManager.BeginTransaction();
                DbUtil.IVRDBManager.BeginTransaction();

                string IVR_delSQL1 = @"DELETE FROM DIALER_INFO WHERE FREE_ID = $projectId$";
                string IVR_delSQL2 = @"DELETE FROM DIALER_INFO_HIS WHERE FREE_ID = $projectId$";
                string IVR_delSQL3 = @"DELETE FROM Dialer_Info_Project WHERE ProjectID = $projectId$";

                ParameterCollection ivrpc = new ParameterCollection();
                ivrpc.Add("projectId", taskInfo.IvrDialerProjectId);

                if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(IVR_delSQL1, ivrpc) >= 0
                    && DbUtil.IVRDBManager.IData.ExecuteNonQuery(IVR_delSQL2, ivrpc) >= 0
                    && DbUtil.IVRDBManager.IData.ExecuteNonQuery(IVR_delSQL3, ivrpc) == 1)
                {
                    string IBP_delSQL1 = @"DELETE FROM auto_dialer_task_info WHERE auto_dialer_task_id = $taskId$";
                    string IBP_delSQL2 = @"DELETE FROM auto_dialer_task_number_info WHERE autodialer_task_id = $taskId$";
                    ivrpc.Clear();
                    ivrpc.Add("taskId", taskInfo.AutoDialerTaskId);

                    if (DbUtil.IBPDBManager.IData.ExecuteNonQuery(IBP_delSQL1, ivrpc) == 1
                        && DbUtil.IBPDBManager.IData.ExecuteNonQuery(IBP_delSQL2, ivrpc) >= 0)
                    {
                        DbUtil.IBPDBManager.CommitTransaction();
                        DbUtil.IVRDBManager.CommitTransaction();

                        message = "成功删除自动外呼任务";
                        result = true;
                    }
                    else
                    {
                        DbUtil.IBPDBManager.RollbackTransaction();
                        DbUtil.IVRDBManager.RollbackTransaction();
                        message = "操作失败，删除IBP系统数据库自动外呼任务失败";
                        return false;
                    }
                }
                else
                {
                    DbUtil.IBPDBManager.RollbackTransaction();
                    DbUtil.IVRDBManager.RollbackTransaction();
                    message = "操作失败，删除IVR系统数据库自动外呼任务失败";
                    return false;
                }


            }
            catch (Exception ex)
            {
                DbUtil.IVRDBManager.RollbackTransaction();
                DbUtil.IBPDBManager.RollbackTransaction();
                message = "操作失败，删除自动外呼任务异常";
                LogUtil.Error("删除自动外呼任务异常", ex);
                return false;
            }


            #endregion


            return result;
        }

        /// <summary>
        /// 删除自动外呼任务外呼号码。
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="numberList"></param>
        /// <param name="nowTotal"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool DeleteAutoDialerTaskNumbers(string taskId, List<string> numberList, out int nowTotal, out string message)
        {
            bool result = false;
            nowTotal = 0;
            message = "操作失败，请与管理员联系";

            AutoDialerTaskInfoModel taskInfo = Retrieve(taskId);

            if (taskInfo == null)
            {
                message = "操作失败，当前任务ID不存在";
                return false;
            }

            nowTotal = Convert.ToInt32(taskInfo.DialerNumberTotal);

            if (taskInfo.Status == 1)
            {
                message = "操作失败，当前任务正在执行中";
                return false;
            }

            #region 删除TF_CMS数据库中数据

            if (numberList != null && numberList.Count > 0)
            {
                string delIVRSql = @"
DELETE FROM 
    [DIALER_INFO]
WHERE
    [DIALER_ID] = $numberId$ AND FREE_ID = $ivrProjectId$
";

                try
                {
                    DbUtil.IBPDBManager.BeginTransaction();
                    DbUtil.IVRDBManager.BeginTransaction();

                    ParameterCollection ivrpc = new ParameterCollection();
                    int counter = 0;

                    foreach (string numberId in numberList)
                    {
                        if (string.IsNullOrEmpty(numberId))
                        {
                            continue;
                        }

                        if (AutoDialerTaskNumberInfoService.Instance.Delete(numberId) == 1)
                        {
                            ivrpc.Clear();
                            ivrpc.Add("numberId", numberId);
                            ivrpc.Add("ivrProjectId", taskInfo.IvrDialerProjectId);

                            if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(delIVRSql, ivrpc) != 1)
                            {
                                DbUtil.IBPDBManager.RollbackTransaction();
                                DbUtil.IVRDBManager.RollbackTransaction();
                                message = "操作失败，删除IVR系统数据库自动外呼号码失败";
                                return false;
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        else
                        {
                            DbUtil.IBPDBManager.RollbackTransaction();
                            DbUtil.IVRDBManager.RollbackTransaction();
                            message = "操作失败，删除IBP系统数据库自动外呼号码失败";
                            return false;
                        }
                    }

                    string totalSql = @"SELECT COUNT(1) FROM auto_dialer_task_number_info WHERE autodialer_task_id = $taskId$";
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("taskId", taskInfo.AutoDialerTaskId);

                    nowTotal = Convert.ToInt32(DbUtil.IBPDBManager.IData.ExecuteScalar(totalSql, pc));

                    string updateSql = "UPDATE auto_dialer_task_info SET dialer_number_total = $total$ WHERE auto_dialer_task_id =  $taskId$";
                    pc.Add("total", nowTotal);

                    if (DbUtil.IBPDBManager.IData.ExecuteNonQuery(updateSql, pc) == 1)
                    {
                        DbUtil.IBPDBManager.CommitTransaction();
                        DbUtil.IVRDBManager.CommitTransaction();

                        message = string.Format("成功删除自动外呼号码共 {0} 行", counter);
                        result = true;
                    }
                    else
                    {
                        DbUtil.IBPDBManager.RollbackTransaction();
                        DbUtil.IVRDBManager.RollbackTransaction();
                        message = "操作失败，更新IBP系统自动外呼号码总数失败";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                    DbUtil.IBPDBManager.RollbackTransaction();
                    message = "操作失败，往IVR系统数据库传递自动外呼号码异常";
                    LogUtil.Error("往IVR系统数据库传递自动外呼号码异常", ex);
                    return false;
                }
            }

            #endregion


            return result;
        }



        public bool UpdateAutoDialerTask(AutoDialerTaskInfoModel taskInfo, out string message)
        {
            message = "操作失败，请与管理员联系";
            bool result = false;

            if (taskInfo.Status == 1 || taskInfo.Status == 2)
            {
                message = "请将当前任务置为停止状态后再修改信息";
                return false;
            }

            try
            {
                DbUtil.IVRDBManager.BeginTransaction();

                string updateSql = @"
UPDATE 
    [Dialer_Info_Project]
   SET 
        [ProjectName] = $projectName$,
        [ProjectCmt] = $description$,
        [CampaignID] = 1,
        [Start_Date] = $beginTime$,                             
        [End_Date] = $endTime$,
        [Start_Time1] = $startTime1$,
        [End_Time1] = $stopTime1$,
        [Start_Time2] = $startTime2$,
        [End_Time2] = $stopTime2$,
        [Start_Time3] = $startTime3$,
        [End_Time3] = $stopTime3$,
        [Start_Time4] = $startTime4$,
        [End_Time4] = $stopTime4$,
        [Modify_Time] = GETDATE(),
        [Modify_IP] = $modified_ip$
 WHERE 
        [Modifier] = $modifier$ AND ProjectID = $projectId$
";

                ParameterCollection pc = new ParameterCollection();
                pc.Add("projectId", taskInfo.IvrDialerProjectId);
                pc.Add("projectName", taskInfo.AutoDialerTaskName);
                pc.Add("description", taskInfo.Description);
                pc.Add("beginTime", taskInfo.BeginTime);
                pc.Add("endTime", taskInfo.EndTime);

                pc.Add("startTime1", taskInfo.StartTime1);
                pc.Add("startTime2", taskInfo.StartTime2);
                pc.Add("startTime3", taskInfo.StartTime3);
                pc.Add("startTime4", taskInfo.StartTime4);
                pc.Add("stopTime1", taskInfo.StopTime1);
                pc.Add("stopTime2", taskInfo.StopTime2);
                pc.Add("stopTime3", taskInfo.StopTime3);
                pc.Add("stopTime4", taskInfo.StopTime4);

                pc.Add("modifier", taskInfo.AutoDialerTaskId);
                pc.Add("modified_ip", SessionUtil.GetClientIPAddress());

                if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(updateSql, pc) == 1)
                {
                    if (Update(taskInfo) == 1)
                    {
                        DbUtil.IVRDBManager.CommitTransaction();
                        message = "更新自动外呼任务基本信息成功";
                        result = true;
                    }
                    else
                    {
                        DbUtil.IVRDBManager.RollbackTransaction();
                        message = "更新IBP系统自动外呼任务基本信息失败，请与管理员联系";
                        result = false;
                    }
                }
                else
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                    message = "更新IVR系统自动外呼任务信息失败，请与管理员联系";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                DbUtil.IVRDBManager.RollbackTransaction();
                LogUtil.Error("更新自动外呼任务信息异常", ex);
                throw ex;
            }
            
            return result;
        }

        /// <summary>
        /// 获取自动外呼策略信息。
        /// </summary>
        /// <returns></returns>
        public DialerInfoCampaignModel GetIVRServerDialerCampaignInfo()
        {
            string sql = @"
SELECT [CampaignID]
      ,[CampaignName]
      ,[CampaignCmt]
      ,[TIMES]
      ,[INTERVAL]
      ,[AcdType]
      ,[RoutePoint]
      ,[MaxPorts]
      ,[MaxTimeOut]
  FROM 
    [Dialer_Info_Campaign]
  WHERE
    CampaignID = 1";

            DialerInfoCampaignModel result = null;
            DataTable dt = DbUtil.IVRDBManager.IData.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                result = new DialerInfoCampaignModel();
                result.Campaignid = Convert.ToInt32(dt.Rows[0]["CampaignID"]);
                result.Campaignname = dt.Rows[0]["CampaignName"].ToString();
                result.Campaigncmt = dt.Rows[0]["CampaignCmt"].ToString();

                result.Times = Convert.ToInt32(dt.Rows[0]["TIMES"]);
                result.Interval = Convert.ToInt32(dt.Rows[0]["INTERVAL"]);
                result.Acdtype = Convert.ToInt32(dt.Rows[0]["AcdType"]);
                result.Routepoint = dt.Rows[0]["RoutePoint"].ToString();
                result.Maxports = Convert.ToInt32(dt.Rows[0]["MaxPorts"]);
                result.Maxtimeout = Convert.ToInt32(dt.Rows[0]["MaxTimeOut"]);
            }

            return result;
        }

        /// <summary>
        /// 更新自动外呼策略信息。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateAutoDialerCampaign(DialerInfoCampaignModel info)
        {
            bool result = false;

            try
            {
                DbUtil.IVRDBManager.BeginTransaction();

                string sql = @"
UPDATE 
        [Dialer_Info_Campaign]
   SET 
        TIMES = $Times$,
        INTERVAL = $Interval$,
        MaxPorts = $Maxports$
 WHERE 
       CampaignID = 1
";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("Times", info.Times);
                pc.Add("Interval", info.Interval);
                pc.Add("Maxports", info.Maxports);

                if (DbUtil.IVRDBManager.IData.ExecuteNonQuery(sql, pc) > 0)
                {
                    DbUtil.IVRDBManager.CommitTransaction();
                    result = true;
                }
                else
                {
                    DbUtil.IVRDBManager.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                DbUtil.IVRDBManager.RollbackTransaction();
                LogUtil.Error("更新IVR系统自动外呼策略异常", ex);
                throw ex;
            }

            return result;
        }
	}
}

