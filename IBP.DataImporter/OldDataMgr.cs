using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Common;
using IBP.Services;

namespace IBP.DataImporter
{
    public partial class OldDataMgr : Form
    {
        public OldDataMgr()
        {
            InitializeComponent();
        }

        private void btnInsertNewOrderResult_Click(object sender, EventArgs e)
        {
            btnInsertNewOrderResult.Enabled = false;

            string getOrderTypeSQL = "select workorder_type_id, [type_name] from workorder_type_info where workorder_type_id = '82a5f0f9-36ca-4f94-8325-daac77600541'";
            string insertNewResultSQL = @"
INSERT INTO [workorder_result_info]
           ([workorder_result_id]
           ,[workorder_type_id]
           ,[result_name]
           ,[description]
           ,[sort_order]
           ,[created_on]
           ,[created_by]
           ,[modified_on]
           ,[modified_by]
           ,[status_code])
     VALUES
           ($workorder_result_id$
           ,$workorder_type_id$
           ,$result_name$
           ,$description$
           ,$sort_order$
           ,GETDATE()
           ,$created_by$
           ,null
           ,null
           ,0)
";

//跟进意向（X）：临时状态
//    -跟进意向（Y）：临时状态
//    -跟进意向C（0.1%-30%）
//    -跟进意向B（30.1%-60%）
//    -跟进意向A（60.1%-99%）

            StringBuilder logsb = new StringBuilder();

            DataTable orderTypeTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getOrderTypeSQL);
            if (orderTypeTable != null)
            {
                ParameterCollection pc = new ParameterCollection();
                try
                {
                    NewDbHelper.OldDB.BeginTransaction();

                    for (int i = 0; i < orderTypeTable.Rows.Count; i++)
                    {
                        pc.Clear();
                        pc.Add("workorder_result_id", Guid.NewGuid().ToString());
                        pc.Add("workorder_type_id", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                        pc.Add("result_name", "跟进意向（X）");
                        pc.Add("description", "跟进意向（X）");
                        pc.Add("sort_order", 0);
                        pc.Add("created_by", "T060008004");

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertNewResultSQL, pc) != 1)
                        {
                            NewDbHelper.OldDB.RollbackTransaction();
                            logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果【跟进意向（X）】失败\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                            btnInsertNewOrderResult.Enabled = true;
                            return;
                        }

                        pc.Clear();
                        pc.Add("workorder_result_id", Guid.NewGuid().ToString());
                        pc.Add("workorder_type_id", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                        pc.Add("result_name", "跟进意向（Y）");
                        pc.Add("description", "跟进意向（Y）");
                        pc.Add("sort_order", 1);
                        pc.Add("created_by", "T060008004");

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertNewResultSQL, pc) != 1)
                        {
                            NewDbHelper.OldDB.RollbackTransaction();
                            logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果【跟进意向（Y）】失败\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                            btnInsertNewOrderResult.Enabled = true;
                            return;
                        }

                        pc.Clear();
                        pc.Add("workorder_result_id", Guid.NewGuid().ToString());
                        pc.Add("workorder_type_id", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                        pc.Add("result_name", "跟进意向C（0.1%-30%）");
                        pc.Add("description", "跟进意向C（0.1%-30%）");
                        pc.Add("sort_order", 2);
                        pc.Add("created_by", "T060008004");

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertNewResultSQL, pc) != 1)
                        {
                            NewDbHelper.OldDB.RollbackTransaction();
                            logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果【跟进意向C（0.1%-30%）】失败\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                            btnInsertNewOrderResult.Enabled = true;
                            return;
                        }

                        pc.Clear();
                        pc.Add("workorder_result_id", Guid.NewGuid().ToString());
                        pc.Add("workorder_type_id", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                        pc.Add("result_name", "跟进意向B（30.1%-60%）");
                        pc.Add("description", "跟进意向B（30.1%-60%）");
                        pc.Add("sort_order", 3);
                        pc.Add("created_by", "T060008004");

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertNewResultSQL, pc) != 1)
                        {
                            NewDbHelper.OldDB.RollbackTransaction();
                            logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果【跟进意向B（30.1%-60%）】失败\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                            btnInsertNewOrderResult.Enabled = true;
                            return;
                        }

                        pc.Clear();
                        pc.Add("workorder_result_id", Guid.NewGuid().ToString());
                        pc.Add("workorder_type_id", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                        pc.Add("result_name", "跟进意向A（60.1%-99%）");
                        pc.Add("description", "跟进意向A（60.1%-99%）");
                        pc.Add("sort_order", 4);
                        pc.Add("created_by", "T060008004");

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertNewResultSQL, pc) != 1)
                        {
                            NewDbHelper.OldDB.RollbackTransaction();
                            logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果【跟进意向A（60.1%-99%）】失败\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                            btnInsertNewOrderResult.Enabled = true;
                            return;
                        }

                        logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果成功\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                    }

                    NewDbHelper.OldDB.CommitTransaction();
                }
                catch (Exception ex)
                {
                    NewDbHelper.OldDB.RollbackTransaction();
                    logBox.Text += string.Format("【{0}】: 插入工单类型异常，{1}\r\n", DateTime.Now.ToString(), ex.Message);
                    btnInsertNewOrderResult.Enabled = true;
                    return;
                }
            }

            logBox.Text += logsb.ToString();

            btnInsertNewOrderResult.Enabled = true;
        }

        private void btnClearNewOrderResult_Click(object sender, EventArgs e)
        {
            btnClearNewOrderResult.Enabled = false;
            //跟进意向（X）：临时状态
            //    -跟进意向（Y）：临时状态
            //    -跟进意向C（0.1%-30%）
            //    -跟进意向B（30.1%-60%）
            //    -跟进意向A（60.1%-99%）

            string sql = @"DELETE FROM [workorder_result_info] WHERE 
                            result_name = '跟进意向（X）' OR
                            result_name = '跟进意向（Y）' OR
                            result_name = '跟进意向C（0.1%-30%）' OR
                            result_name = '跟进意向B（30.1%-60%）' OR
                            result_name = '跟进意向A（60.1%-99%）' ";

            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql);
            logBox.Text += "成功清除新添加的工单处理记录";

            btnClearNewOrderResult.Enabled = true;
        }

        private void btnUpdateProcessResult_Click(object sender, EventArgs e)
        {
            btnUpdateProcessResult.Enabled = false;

            string getOrderTypeSQL = "select workorder_type_id, [type_name] from workorder_type_info";
            string getNewResultSQL = "select workorder_result_id, [result_name] from workorder_result_info where workorder_type_id = $typeId$";

            string getResultEnumSQL = @"
select p.after_result, r.result_name from (
select after_result from workorder_process_info where workorder_type_id = $typeId$ group by after_result) p 
left join workorder_result_info r on p.after_result = r.workorder_result_id;";

            string updateProcessResultSQL = "update workorder_process_info set after_result = $NewAfterResult$, description = cast([description] as varchar)  + '<br/>' + $description$  where after_result = $OldAfterResult$ and workorder_type_id  = $typeId$ ";

            Dictionary<string, string> resultList = null;
            DataTable resultTable = null;
            DataTable oldResultTable = null;
            StringBuilder logsb = new StringBuilder();
            ParameterCollection upc = new ParameterCollection();

            DataTable orderTypeTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getOrderTypeSQL);
            if (orderTypeTable != null)
            {
                ParameterCollection pc = new ParameterCollection();
                try
                {
                    NewDbHelper.OldDB.BeginTransaction();

                    for (int i = 0; i < orderTypeTable.Rows.Count; i++)
                    {
                        pc.Clear();
                        pc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());

                        resultTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getNewResultSQL, pc);
                        if (resultTable != null)
                        {
                            resultList = new Dictionary<string, string>();
                            for (int j = 0; j < resultTable.Rows.Count; j++)
                            {
                                resultList[resultTable.Rows[j]["result_name"].ToString()] = resultTable.Rows[j]["workorder_result_id"].ToString();
                            }

                           

                            oldResultTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getResultEnumSQL, pc);
                            if (oldResultTable != null)
                            {
                                for (int k = 0; k < oldResultTable.Rows.Count; k++)
                                {
                                    string oldResult = oldResultTable.Rows[k]["result_name"].ToString();
                                    if (string.IsNullOrEmpty(oldResult))
                                        oldResult = "null";

                                    int counter = 0;
                                    switch (oldResult)
                                    {
                                        case "null":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【无人接听】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【无人接听】转为【跟进意向（X）】共{2}行\r\n",DateTime.Now.ToString(),orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "无人接听":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【无人接听】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【无人接听】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "忙音占线":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【忙音占线】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【忙音占线】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "关机":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【关机】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【关机】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "停机":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【停机】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【停机】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "呼叫转移":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【呼叫转移】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【呼叫转移】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "不在服务区":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【不在服务区】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【不在服务区】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "直接挂机":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向C（0.1%-30%）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【直接挂机】转为【跟进意向C（0.1%-30%）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【直接挂机】转为【跟进意向C（0.1%-30%）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "余额不足":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【余额不足】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【余额不足】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "空号过期无效":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["结束"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【空号过期无效】转为【结束】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【空号过期无效】转为【结束】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;


                                        case "语言不通":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["结束"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【语言不通】转为【结束】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【语言不通】转为【结束】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "非本人":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【非本人】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【非本人】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "中断":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（X）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【中断】转为【跟进意向（X）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【中断】转为【跟进意向（X）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        case "跟进":
                                            upc.Clear();
                                            upc.Add("NewAfterResult", resultList["跟进意向（Y）"]);
                                            upc.Add("OldAfterResult", oldResultTable.Rows[k]["after_result"].ToString());
                                            upc.Add("typeId", orderTypeTable.Rows[i]["workorder_type_id"].ToString());
                                            upc.Add("description", "系统调整：本工单处理结果由【跟进】转为【跟进意向（Y）】");
                                            counter = NewDbHelper.OldDB.IData.ExecuteNonQuery(updateProcessResultSQL, upc);
                                            logsb.AppendFormat("【{0}】: 更新类型为【{1}】工单处理记录，由【跟进】转为【跟进意向（Y）】共{2}行\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString(), counter);
                                            break;

                                        default:
                                            break;
                                    }                                     
                                }
                            }
                        }

                        //logsb.AppendFormat("【{0}】: 插入工单类型【{1}】处理结果成功\r\n", DateTime.Now.ToString(), orderTypeTable.Rows[i]["type_name"].ToString());
                    }

                    NewDbHelper.OldDB.CommitTransaction();
                }
                catch (Exception ex)
                {
                    NewDbHelper.OldDB.RollbackTransaction();
                    logBox.Text += string.Format("【{0}】: 更新工单处理记录中的处理结果异常，{1}\r\n", DateTime.Now.ToString(), ex.Message);
                    btnInsertNewOrderResult.Enabled = true;
                    return;
                }
            }

            logBox.Text += logsb.ToString();

            btnUpdateProcessResult.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = @"select workorder_id from workorder_process_info where workorder_id not in
(select work_order_id from work_order_info)";
            string delSQL = "delete from work_order_info where work_order_id = $work_order_id$";

            DataTable dt = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            if (dt != null)
            {
                int counter = 0;
                ParameterCollection pc = new ParameterCollection();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("work_order_id", Guid.Parse(dt.Rows[i][0].ToString()));
                    counter = counter + NewDbHelper.OldDB.IData.ExecuteNonQuery(delSQL, pc);
                }

                logBox.Text += string.Format("【{0}】: 成功工单处理记录表中无工单信息的记录共{1}条\r\n",DateTime.Now, counter);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            string sql = "select * from work_order_info";
            string resultSql = "select * from workorder_result_info";
            string updateResultNameSQL = "update work_order_info set now_result_name = $resultName$ where work_order_id = $workorderId$";
            string updateResultNameSQL2 = "update work_order_info set now_result_name = $resultName$, now_result_id = $nowResultId$ where work_order_id = $workorderId$";
            string getProcessSQL = "select top 1 * from workorder_process_info where workorder_id = $workorderId$ order by created_on desc";

            string getStatusSQL = "select workorder_status_id,status_name from workorder_status_info";
            string updateOrderStatusNameSQL = "update work_order_info set now_status_name = $statusName$ where now_status  = $statusId$";
            string updateOrderStatusIdSQL2 = "update work_order_info set now_status_name = $statusName$, now_status  = $statusId$ where work_order_id  = $workorderId$";

            Dictionary<string, string> resultList = null;
            ParameterCollection pc = new ParameterCollection();
            DataTable processTable = null;
            Dictionary<string, string> statusList = null;

            DataTable statusTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getStatusSQL);
             if (statusTable != null)
             {
                 statusList = new Dictionary<string, string>();
                 for (int i = 0; i < statusTable.Rows.Count; i++)
                 {
                     statusList[statusTable.Rows[i]["workorder_status_id"].ToString().ToUpper()] = statusTable.Rows[i]["status_name"].ToString();
                 }
             }

            DataTable resultTable = NewDbHelper.OldDB.IData.ExecuteDataTable(resultSql);
            if (resultTable != null)
            {
                resultList = new Dictionary<string, string>();
                for (int i = 0; i < resultTable.Rows.Count; i++)
                {
                    resultList[resultTable.Rows[i]["workorder_result_id"].ToString().ToUpper()] = resultTable.Rows[i]["result_name"].ToString();
                }
            }

            DataTable orderTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            for (int i = 0; i < orderTable.Rows.Count; i++)
            {
                if (resultList.ContainsKey(orderTable.Rows[i]["now_result_id"].ToString()))
                {
                    pc.Clear();
                    pc.Add("resultName", resultList[orderTable.Rows[i]["now_result_id"].ToString()]);
                    pc.Add("workorderId", orderTable.Rows[i]["work_order_id"].ToString());

                    NewDbHelper.OldDB.IData.ExecuteNonQuery(updateResultNameSQL, pc);
                }
                else
                {
                    pc.Clear();
                    pc.Add("workorderId", orderTable.Rows[i]["work_order_id"].ToString());
                    processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getProcessSQL, pc);
                    if (processTable != null && processTable.Rows.Count > 0)
                    {
                        pc.Add("nowResultId", processTable.Rows[0]["after_result"].ToString());
                        pc.Add("resultName", resultList[processTable.Rows[0]["after_result"].ToString().ToUpper()]);

                        NewDbHelper.OldDB.IData.ExecuteNonQuery(updateResultNameSQL2, pc);
                    }
                }

                if (statusList.ContainsKey(orderTable.Rows[i]["now_status"].ToString().ToUpper()))
                {
                    pc.Clear();
                    pc.Add("statusName", statusList[orderTable.Rows[i]["now_status"].ToString().ToUpper()]);
                    pc.Add("statusId", orderTable.Rows[i]["now_status"].ToString());
                    pc.Add("workorderId", orderTable.Rows[i]["work_order_id"].ToString());

                    NewDbHelper.OldDB.IData.ExecuteNonQuery(updateOrderStatusNameSQL, pc);
                }
                else
                {
                    if (processTable == null)
                    {
                        processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getProcessSQL, pc);
                        if (processTable != null && processTable.Rows.Count > 0)
                        {
                            pc.Clear();
                            pc.Add("statusName", statusList[processTable.Rows[0]["before_result"].ToString().ToUpper()]);
                            pc.Add("statusId", processTable.Rows[0]["before_result"].ToString());
                            pc.Add("workorderId", orderTable.Rows[i]["work_order_id"].ToString());

                            NewDbHelper.OldDB.IData.ExecuteNonQuery(updateOrderStatusIdSQL2, pc);
                        }
                    }
                }
            }

            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            string sql = @"
select 
	p.process_id, p.after_result,r.result_name, p.before_result, s.status_name, p.workorder_type_id 
from 
	workorder_process_info p 
left join 
	workorder_result_info r 
on 
	p.after_result = r.workorder_result_id
left join 
	workorder_status_info s
on
	p.before_result = s.workorder_status_id
where
	r.result_name = '结束' and s.status_name <> '已关闭'";

            string sql2 = @"
select 
	p.process_id, p.after_result,r.result_name, p.before_result, s.status_name, p.workorder_type_id 
from 
	workorder_process_info p 
left join 
	workorder_result_info r 
on 
	p.after_result = r.workorder_result_id
left join 
	workorder_status_info s
on
	p.before_result = s.workorder_status_id
where
	r.result_name = '成功' and s.status_name <> '已关闭'";

            string getClosedStatusSQL = "select workorder_status_id from workorder_status_info where workorder_type_id = $workorder_type_id$ and status_name = '已关闭'";
            string updateStatusSQL = "update workorder_process_info set before_result = $statusId$ where process_id = $processId$";

            DataTable processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            ParameterCollection pc = new ParameterCollection();

            if (processTable != null)
            {
                for (int i = 0; i < processTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("workorder_type_id", processTable.Rows[i]["workorder_type_id"].ToString());

                    object closeStatusId = NewDbHelper.OldDB.IData.ExecuteScalar(getClosedStatusSQL, pc);

                    if (closeStatusId != null)
                    {
                        pc.Clear();
                        pc.Add("statusId", closeStatusId.ToString());
                        pc.Add("processId", processTable.Rows[i]["process_id"].ToString());

                        NewDbHelper.OldDB.IData.ExecuteNonQuery(updateStatusSQL, pc);
                    }
                }
            }

            processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql2);

            if (processTable != null)
            {
                for (int i = 0; i < processTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("workorder_type_id", processTable.Rows[i]["workorder_type_id"].ToString());

                    object closeStatusId = NewDbHelper.OldDB.IData.ExecuteScalar(getClosedStatusSQL, pc);

                    if (closeStatusId != null)
                    {
                        pc.Clear();
                        pc.Add("statusId", closeStatusId.ToString());
                        pc.Add("processId", processTable.Rows[i]["process_id"].ToString());

                        NewDbHelper.OldDB.IData.ExecuteNonQuery(updateStatusSQL, pc);
                    }
                }
            }


            string sql3 = @"
select 
	p.process_id, p.after_result,r.result_name, p.before_result, s.status_name, p.workorder_type_id, p.description
from 
	workorder_process_info p 
left join 
	workorder_result_info r 
on 
	p.after_result = r.workorder_result_id
left join 
	workorder_status_info s
on
	p.before_result = s.workorder_status_id
where
	r.result_name <> '成功' 
	and r.result_name <> '结束'  
	and s.status_name = '已关闭'
    and p.workorder_type_id= '29807854-4C2C-4D4C-A3C7-9585812C7629'
order by 
	s.status_name";

            //string getProcessStatusSQL = "select workorder_status_id from workorder_status_info where workorder_type_id = $workorder_type_id$ and status_name = '处理中'";
            string getProcessStatusSQL = "select workorder_status_id from workorder_status_info where workorder_type_id = '29807854-4C2C-4D4C-A3C7-9585812C7629' and status_name = '处理中'";

            processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql3);

            if (processTable != null)
            {
                for (int i = 0; i < processTable.Rows.Count; i++)
                {
                    //pc.Clear();
                    //pc.Add("workorder_type_id", processTable.Rows[i]["workorder_type_id"].ToString());

                    //object processStatusId = DbHelper.OldDB.IData.ExecuteScalar(getProcessStatusSQL, pc);

                    //if (processStatusId != null)
                    //{
                        pc.Clear();
                        pc.Add("statusId", "0EB01CFD-C974-45CF-979E-4B0110BE7FC2");
                        pc.Add("processId", processTable.Rows[i]["process_id"].ToString());

                        NewDbHelper.OldDB.IData.ExecuteNonQuery(updateStatusSQL, pc);
                    //}
                }
            }

            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;

            string sql = @"
select 
	p.process_id, p.after_result,r.result_name, p.before_result, s.status_name, p.workorder_type_id,p.description
from 
	workorder_process_info p 
left join 
	workorder_result_info r 
on 
	p.after_result = r.workorder_result_id
left join 
	workorder_status_info s
on
	p.before_result = s.workorder_status_id
where
	r.result_name <> '成功'
	and r.result_name <> '结束'
	and s.status_name = '未处理'";

            string getProcessStatusSQL = "select workorder_status_id from workorder_status_info where workorder_type_id = $workorder_type_id$ and status_name = '处理中'";
            string updateStatusSQL = "update workorder_process_info set before_result = $statusId$ where process_id = $processId$";

            DataTable processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            ParameterCollection pc = new ParameterCollection();
            object processingStatusId = null;
            int counter = 0;
            if (processTable != null)
            {
                for (int i = 0; i < processTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("workorder_type_id", processTable.Rows[i]["workorder_type_id"].ToString().ToUpper());
                    processingStatusId = NewDbHelper.OldDB.IData.ExecuteScalar(getProcessStatusSQL, pc);

                    if (processingStatusId != null)
                    {
                        pc.Add("processId", processTable.Rows[i]["process_id"].ToString());
                        pc.Add("statusId", processingStatusId.ToString());

                        counter += NewDbHelper.OldDB.IData.ExecuteNonQuery(updateStatusSQL, pc);
                    }
                }
            }

            logBox.Text += string.Format("成功更新工单处理记录信息表中{0}行记录，状态由【未处理】转为【处理中】", counter);

            button4.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;

            string getHasProcessRecordWorkOrderSQL = @"
select * from work_order_info where work_order_id in(
select workorder_id from workorder_process_info)";

            string getProcessRecordByWorkOrderIdSQL = @"
select top 1 p.process_id, p.before_result,p.after_result,r.result_name,s.status_name from 
	workorder_process_info p
left join 
	workorder_result_info r 
on 
	p.after_result = r.workorder_result_id
left join 
	workorder_status_info s
on
	p.before_result = s.workorder_status_id
where 
	p.workorder_id = $WorkOrderId$
order by 
	p.created_on desc
";
            string updateWorkOrderSQL = @"
update work_order_info set
	now_result_id = $ResultId$,
	now_result_name = $ResultName$,
	now_status = $StatusId$,
	now_status_name = $StatusName$,
	status_code = $StatusCode$
where
	work_order_id = $WorkOrderId$";

            DataTable hasProcessRecordWorkOrderTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getHasProcessRecordWorkOrderSQL);
            DataTable processTable = null;

            ParameterCollection pc = new ParameterCollection();
            ParameterCollection upc = new ParameterCollection();
            int counter = 0;

            if (hasProcessRecordWorkOrderTable != null)
            {
                for (int i = 0; i < hasProcessRecordWorkOrderTable.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("WorkOrderId", hasProcessRecordWorkOrderTable.Rows[i]["work_order_id"].ToString());
                    processTable = NewDbHelper.OldDB.IData.ExecuteDataTable(getProcessRecordByWorkOrderIdSQL, pc);

                    if (processTable != null && processTable.Rows.Count > 0)
                    {
                        upc.Clear();
                        upc.Add("ResultId", processTable.Rows[0]["after_result"].ToString());
                        upc.Add("ResultName", processTable.Rows[0]["result_name"].ToString());
                        upc.Add("StatusId", processTable.Rows[0]["before_result"].ToString());
                        upc.Add("StatusName", processTable.Rows[0]["status_name"].ToString());
                        upc.Add("WorkOrderId", hasProcessRecordWorkOrderTable.Rows[i]["work_order_id"].ToString());

                        switch (processTable.Rows[0]["result_name"].ToString())
                        {
                            case "成功":
                                upc.Add("StatusCode", 3);
                                break;
                            case "结束":
                                upc.Add("StatusCode", 3);
                                break;
                            default:
                                upc.Add("StatusCode", 2);
                                break;
                        }

                        counter += NewDbHelper.OldDB.IData.ExecuteNonQuery(updateWorkOrderSQL, upc);
                    }
                }

                logBox.Text += string.Format("成功更新工单信息表中{0}行记录，当前状态及处理结束与对应的最后一条工单处理记录中的状态和结果保持一致", counter);
            }


            button5.Enabled = true;
                 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;

            string oldDataSQL = "select o.process_id, o.description from workorder_process_info o";
            string updateSQL = "update workorder_process_info set description = $description$ where process_id = $process_id$";
            ParameterCollection pc = new ParameterCollection();

            DataTable dt = NewDbHelper.OldDB.IData.ExecuteDataTable(oldDataSQL);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    pc.Clear();
                    pc.Add("process_id", dt.Rows[i]["process_id"].ToString());
                    pc.Add("description", dt.Rows[i]["description"].ToString());

                    NewDbHelper.NewDB.IData.ExecuteNonQuery(updateSQL, pc);
                }
            }

            button6.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Enabled = false;

            //string sql1 = @"delete from Sys_CodeType where code = 'C00057' and Name = '家校通项目' and CodeType = 1";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);

            //string sql2 = @"insert into Sys_CodeType (code, Name, CodeType, mark, ShowSort, IsShow) values('C00057','家校通项目', 1, null, 1,0)";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql2);

            string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00057')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3);

            string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00057')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);


            string sql4 = @"delete from C_Info where CLY = 'C00057'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4);
                      

            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        2,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        '', -- 固定电话
        '', -- 办公电话
        '', -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
      '4C967D01-4BFD-41FD-AFB9-6C213C4373B1',
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', --$UserId$,
      1,
      $OrderMemo$
      )                ";

            DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\家校通项目数据.xls");

            string oldCustomerId = null; 
            int skip = 0; 
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();
                

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机号码"].ToString()))
                    {
                        skip++;
                        continue;
                    }

                    pc.Clear();
                    oldCustomerId = string.Format("{0}", 201205240000 + i);

                    pc.Add("CustomerSource", "C00057"); // 家校通项目

                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("MobilePhone", ds.Tables[0].Rows[i]["手机号码"].ToString());
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["用户姓名"].ToString());
                    pc.Add("MemoContent",
                        string.Format("家校通项目导入数据：“宝安福永中心小学”“{0}”“{1}”的家长",
                        ds.Tables[0].Rows[i]["班级"].ToString(),
                        ds.Tables[0].Rows[i]["学生姓名"].ToString()));

                    pc.Add("OrderMemo", string.Format("中国电信家校通回访及智能版家校通升级销售：“宝安福永中心小学”“{0}”“{1}”的家长",
                        ds.Tables[0].Rows[i]["班级"].ToString(),
                        ds.Tables[0].Rows[i]["学生姓名"].ToString()));

                    switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    {
                        case "高":
                            pc.Add("OrderLevel", "C00003");
                            break;

                        case "中":
                            pc.Add("OrderLevel", "C00002");
                            break;

                        case "低":
                            pc.Add("OrderLevel", "C00001");
                            break;

                        default:
                            pc.Add("OrderLevel", "C00003");
                            break;
                    }

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");

                    //T060009189
                    //T060009157
                    //T060009191
                    //T060009192
                    //T060009193
                    //T060009187
                    //T060009195
                    if (i >= 0 && i < 250)
                    {
                        pc["UserId"].Value = "T060009189";
                    }
                    if (i >= 250 && i < 500)
                    {
                        pc["UserId"].Value = "T060009157";
                    }
                    if (i >= 500 && i < 750)
                    {
                        pc["UserId"].Value = "T060009191";
                    }
                    if (i >= 750 && i < 1000)
                    {
                        pc["UserId"].Value = "T060009192";
                    }
                    if (i >= 1000 && i < 1250)
                    {
                        pc["UserId"].Value = "T060009193";
                    }
                    if (i >= 1250 && i < 1500)
                    {
                        pc["UserId"].Value = "T060009195";
                    }
                    if (i >= 1500 && i < 2000)
                    {
                        pc["UserId"].Value = "T060009187";
                    }


                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行",ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button7.Enabled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;

            //string sql1 = @"delete from Sys_CodeType where code = 'C00057' and Name = '家校通项目' and CodeType = 1";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);

            //string sql2 = @"insert into Sys_CodeType (code, Name, CodeType, mark, ShowSort, IsShow) values('C00057','家校通项目', 1, null, 1,0)";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql2);

            string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00061')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3);

            string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00061')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);


            string sql4 = @"delete from C_Info where CLY = 'C00061'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4);


            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        2,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        '', -- 固定电话
        '', -- 办公电话
        '', -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
      '08A2AC41-9A49-4BF5-B694-5312957ACC89',
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', --$UserId$,
      1,
      $OrderMemo$
      )                ";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\家校通项目数据.xls");

            string getDataSQL = "select * from school_jm_detail";
            DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(getDataSQL);

            ParameterCollection pc = new ParameterCollection();

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                pc.Clear();
                oldCustomerId = string.Format("{0}", 201206240000 + i);

                pc.Add("CustomerSource", "C00061"); // 家校通项目

                pc.Add("WorkorderCode", "WK" + oldCustomerId);
                pc.Add("CustomerId", "D" + oldCustomerId);
                pc.Add("MobilePhone", dt.Rows[i]["phone"].ToString());
                pc.Add("CustomerName", "未知");
                pc.Add("MemoContent",
                    string.Format("电信家校通回访数据：深圳市宝安区锦明学校“{0}”的家长",
                    dt.Rows[i]["sname"].ToString()));

                pc.Add("OrderMemo", string.Format("电信家校通回访数据：深圳市宝安区锦明学校“{0}”的家长",
                    dt.Rows[i]["sname"].ToString()));

                switch (dt.Rows[i]["orderlevel"].ToString())
                {
                    case "high":
                        pc.Add("OrderLevel", "C00003");
                        break;

                    case "narmal":
                        pc.Add("OrderLevel", "C00002");
                        break;

                    case "低":
                        pc.Add("OrderLevel", "C00001");
                        break;

                    default:
                        pc.Add("OrderLevel", "C00003");
                        break;
                }

                pc.Add("UserId", "T060009186");


                try
                {
                    NewDbHelper.OldDB.BeginTransaction();

                    if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                    {
                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                            {
                                NewDbHelper.OldDB.CommitTransaction();
                                succ++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    NewDbHelper.OldDB.RollbackTransaction();
                    logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                    failed++;
                }
            }

            logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", dt.Rows.Count, succ, skip, failed);


            button8.Enabled = true;


            //string sql = "select sort, sname, phone from school_jm_detail";
            //string updatesql = "update school_jm_detail set f6 = '1' where sort = $sort$";
            //ParameterCollection pc = new ParameterCollection(); 

            //Dictionary<string, string> dict = new Dictionary<string, string>();
            //DataTable dt = NewDbHelper.NewDB.IData.ExecuteDataTable(sql);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string key = string.Format("{0}_{1}", dt.Rows[i]["sname"], dt.Rows[i]["phone"]);

            //        if (dict.ContainsKey(key))
            //        {
            //            pc.Clear();
            //            pc.Add("sort", dt.Rows[i]["sort"]);

            //            NewDbHelper.NewDB.IData.ExecuteNonQuery(updatesql, pc);
            //        }
            //        else
            //        {
            //            dict[key] = key;
            //        }
            //    }
            //}

            //int newcount = dict.Count;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;

            string sql1 = @"delete from Sys_CodeType where code = 'C00062' and Name = '68项目（01）' and CodeType = 1";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);

            string sql2 = @"insert into Sys_CodeType (code, Name, CodeType, mark, ShowSort, IsShow) values('C00062','68项目（01）', 1, null, 1,0)";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql2);

            string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00062')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3);

            string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00062')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);


            string sql4 = @"delete from C_Info where CLY = 'C00062'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4);


            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
      '6D0053E8-58F1-4F02-B373-9E5D18BE0AD0',
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.NewDB.IData.ExecuteDataSet("select * from school_tj_detail");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["固话"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他号码"].ToString();
                    }

                    if (pc["MobilePhone"].Value == "" && pc["HomePhone"].Value == "" && pc["OtherPhone"].Value == "")
                    {
                        string aa = "";
                    }

                    switch (ds.Tables[0].Rows[i]["性别"].ToString())
                    {
                        case "男":
                            pc.Add("Sex", "0"); 
                            break;

                        case "女":
                            pc.Add("Sex", "1"); 
                            break;

                        default:
                            pc.Add("Sex", "2"); 
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12006290000 + i);

                    pc.Add("CustomerSource", "C00062"); // 家校通项目

                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["客户姓名"].ToString());
                    pc.Add("MemoContent",ds.Tables[0].Rows[i]["客户备注"].ToString());

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单备注"].ToString());

                    switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    {
                        case "高":
                            pc.Add("OrderLevel", "C00003");
                            break;

                        case "中":
                            pc.Add("OrderLevel", "C00002");
                            break;

                        case "低":
                            pc.Add("OrderLevel", "C00001");
                            break;

                        default:
                            pc.Add("OrderLevel", "C00003");
                            break;
                    }

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");
 
                  
                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button9.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;

            //string sql1 = @"delete from Sys_CodeType where code = 'C00063' and Name = '68项目（02）' and CodeType = 1";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);
            //string sql1 = @"delete from Sys_CodeType where code = 'C00064' and Name = '68项目（03）' and CodeType = 1";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);
            //string sql1 = @"delete from Sys_CodeType where code = 'C00065' and Name = '68项目（04）' and CodeType = 1";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql1);

            //string sql2 = @"insert into Sys_CodeType (code, Name, CodeType, mark, ShowSort, IsShow) values('C00062','68项目（01）', 1, null, 1,0)";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql2);

            string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00063')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3);
            string sql3_1 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00064')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3_1);
            string sql3_2 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00065')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3_2);

            string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00063')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);
            string sql5_1 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00064')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5_1);
            string sql5_2 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00065')";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5_2);


            string sql4 = @"delete from C_Info where CLY = 'C00063'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4);
            string sql4_1 = @"delete from C_Info where CLY = 'C00064'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4_1);
            string sql4_2 = @"delete from C_Info where CLY = 'C00065'";
            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4_2);


            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
        $WorkorderTypeId$,
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.NewDB.IData.ExecuteDataSet("select * from school_68_2");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");
                    pc.Add("CustomerSource", "");
                    pc.Add("WorkorderTypeId", "");

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["固话"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他号码"].ToString();
                    }

                    //if (pc["MobilePhone"].Value == "" && pc["HomePhone"].Value == "" && pc["OtherPhone"].Value == "")
                    //{
                    //    string aa = "";
                    //}

                    //switch (ds.Tables[0].Rows[i]["性别"].ToString())
                    //{
                    //    case "男":
                    //        pc.Add("Sex", "0");
                    //        break;

                    //    case "女":
                    //        pc.Add("Sex", "1");
                    //        break;

                    //    default:
                    //        pc.Add("Sex", "2");
                    //        break;
                    //}

                    switch (ds.Tables[0].Rows[i]["客户来源"].ToString())
                    {
                        case "68项目(02)":
                            pc["CustomerSource"].Value = "C00063";
                            pc["WorkorderTypeId"].Value = "693F9CB9-97EA-4D63-B86B-938B88BB9006";
                            break;

                        case "68项目(03)":
                            pc["CustomerSource"].Value = "C00064";
                            pc["WorkorderTypeId"].Value = "06CBDEE2-4BE6-4A51-836F-4235929979C9";
                            break;

                        case "68项目(04)":
                            pc["CustomerSource"].Value = "C00065";
                            pc["WorkorderTypeId"].Value = "7966BB3F-BF75-4E48-A12B-F6E7F1D55C1E";
                            break;

                        default:
                            throw new Exception("客户来源不明确");
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12006300000 + i);

                   
                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["客户姓名"].ToString());
                    pc.Add("MemoContent", ds.Tables[0].Rows[i]["客户备注"].ToString());
                    pc.Add("Sex", "2");

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单描述"].ToString());

                    switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    {
                        case "高":
                            pc.Add("OrderLevel", "C00003");
                            break;

                        case "中":
                            pc.Add("OrderLevel", "C00002");
                            break;

                        case "低":
                            pc.Add("OrderLevel", "C00001");
                            break;

                        default:
                            pc.Add("OrderLevel", "C00003");
                            break;
                    }

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");


                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button9.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button11.Enabled = false;

          
            //string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00068')";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3); 

            //string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00068')";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);


            //string sql4 = @"delete from C_Info where CLY = 'C00068'";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4); 


            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
        $WorkorderTypeId$,
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";


            string checksql1 = "select count(1) from c_info where sj = $MobilePhone$";
            string checksql2 = "select count(1) from c_info where tel = $HomePhone$";
            string checksql3 = "select count(1) from c_info where othertel = $OtherPhone$";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.NewDB.IData.ExecuteDataSet("select * from school_68_last");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");
                    pc.Add("CustomerSource", "");
                    pc.Add("WorkorderTypeId", "");
                    pc.Add("OrderLevel", "");
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["固话"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他电话"].ToString();
                    }

                    
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql1, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql2, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql3, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00001";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00003";
                    }

                    switch (ds.Tables[0].Rows[i]["客户来源"].ToString())
                    {
                        case "68项目(05)":
                            pc["CustomerSource"].Value = "C00068";
                            pc["WorkorderTypeId"].Value = "D857EBB2-BF45-480D-8F33-D56F2B8D1D4A";
                            break;
                             

                        default:
                            continue;
                            //throw new Exception("客户来源不明确");
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12007055000 + i);


                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["客户姓名"].ToString());
                    pc.Add("MemoContent", ds.Tables[0].Rows[i]["客户备注"].ToString());
                    pc.Add("Sex", "2");

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单描述"].ToString());

                    //pc.Add("OrderLevel", "C00002");

                    //switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    //{
                    //    case "高":
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;

                    //    case "中":
                    //        pc.Add("OrderLevel", "C00002");
                    //        break;

                    //    case "低":
                    //        pc.Add("OrderLevel", "C00001");
                    //        break;

                    //    default:
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;
                    //}

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");


                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button11.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;

            #region sj

            //string sql = "select * from c_info where cly = 'C00068'";

            //string sql2 = "select * from c_info where cly = 'C00068' and sj = $MobilePhone$";

            //string sql3 = "update c_info set cly='68temp' where id = $CustomerId$ ";

            //string sql4 = "select * from work_order_info where rel_customer_id = $CustomerId$  and workorder_type = 'D857EBB2-BF45-480D-8F33-D56F2B8D1D4A'";

            //string sql5 = "select count(1) from workorder_process_info where workorder_id = $WorkorderId$ and workorder_type_id = 'D857EBB2-BF45-480D-8F33-D56F2B8D1D4A';";

            //string sql6 = "update work_order_info set workorder_type = '68temp' where work_order_id =  $WorkorderId$ ";

            //DataTable allData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            //DataTable cfData = null;
            //DataTable orderData = null;

            //ParameterCollection pc = new ParameterCollection();

            //if (allData != null && allData.Rows.Count > 0)
            //{
            //    for (int i = 0; i < allData.Rows.Count; i++)
            //    {
            //        if (allData.Rows[i]["sj"] == DBNull.Value)
            //        {
            //            continue;
            //        }

            //        if(string.IsNullOrEmpty(allData.Rows[i]["sj"].ToString()))
            //        {
            //            continue;
            //        }
            //        pc.Clear();
            //        pc.Add("CustomerId", allData.Rows[i]["id"].ToString());
            //        pc.Add("MobilePhone", allData.Rows[i]["sj"].ToString());
            //        pc.Add("WorkorderId", "");

            //        cfData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql2, pc);

            //        if (cfData != null && cfData.Rows.Count > 1)
            //        {
            //            for (int j = 0; j < cfData.Rows.Count; j++)
            //            {
            //                if (j > 0)
            //                {
            //                    orderData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql4, pc);
            //                    pc["WorkorderId"].Value = orderData.Rows[0]["work_order_id"].ToString();

            //                    NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3, pc);
            //                    NewDbHelper.OldDB.IData.ExecuteNonQuery(sql6, pc);

            //                    //if (orderData != null && orderData.Rows.Count > 0)
            //                    //{
            //                    //    for (int k = 0; k < orderData.Rows.Count; k++)
            //                    //    {
            //                    //        pc["WorkorderId"].Value =  orderData.Rows[k]["work_order_id"].ToString();
            //                    //        bool hasProcess = NewDbHelper.OldDB.IData.ExecuteScalar(sql5, pc).ToString() != "0";
            //                    //        if (hasProcess == false)
            //                    //        {
            //                    //            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3, pc);
            //                    //            NewDbHelper.OldDB.IData.ExecuteNonQuery(sql6, pc);
            //                    //        }
            //                    //    }
            //                    //}
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            #region otherTel

            string sql = "select * from c_info where cly = 'C00068'";

            string sql2 = "select * from c_info where cly = 'C00068' and othertel = $MobilePhone$";

            string sql3 = "update c_info set cly='68temp' where id = $CustomerId$ ";

            string sql4 = "select * from work_order_info where rel_customer_id = $CustomerId$  and workorder_type = 'D857EBB2-BF45-480D-8F33-D56F2B8D1D4A'";

            string sql5 = "select count(1) from workorder_process_info where workorder_id = $WorkorderId$ and workorder_type_id = 'D857EBB2-BF45-480D-8F33-D56F2B8D1D4A';";

            string sql6 = "update work_order_info set workorder_type = '68temp' where work_order_id =  $WorkorderId$ ";

            DataTable allData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql);
            DataTable cfData = null;
            DataTable orderData = null;

            ParameterCollection pc = new ParameterCollection();

            if (allData != null && allData.Rows.Count > 0)
            {
                for (int i = 0; i < allData.Rows.Count; i++)
                {
                    if (allData.Rows[i]["othertel"] == DBNull.Value)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(allData.Rows[i]["othertel"].ToString()))
                    {
                        continue;
                    }
                    pc.Clear();
                    pc.Add("CustomerId", allData.Rows[i]["id"].ToString());
                    pc.Add("MobilePhone", allData.Rows[i]["othertel"].ToString());
                    pc.Add("WorkorderId", "");

                    cfData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql2, pc);

                    if (cfData != null && cfData.Rows.Count > 1)
                    {
                        for (int j = 0; j < cfData.Rows.Count; j++)
                        {
                            if (j > 0)
                            {
                                orderData = NewDbHelper.OldDB.IData.ExecuteDataTable(sql4, pc);
                                pc["WorkorderId"].Value = orderData.Rows[0]["work_order_id"].ToString();

                                NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3, pc);
                                NewDbHelper.OldDB.IData.ExecuteNonQuery(sql6, pc); 
                            }
                        }
                    }
                }
            }

            #endregion

            button12.Enabled = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Enabled = false;

            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
        $WorkorderTypeId$,
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";


            string checksql1 = "select count(1) from c_info where sj = $MobilePhone$";
            string checksql2 = "select count(1) from c_info where tel = $HomePhone$";
            string checksql3 = "select count(1) from c_info where othertel = $OtherPhone$";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.OldDB.IData.ExecuteDataSet(@"

select * from c_info 
inner join school_68_5_all
on c_info.sj = school_68_5_all.[手机]
where sj in (
select [手机] from school_68_5_all where [固话] in 
(
	select distinct [固话] from school_68_5_all where [固话] not in 
	(
		select tel from c_info where tel is not null and tel <> ''
	)
))
");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");
                    pc.Add("CustomerSource", "");
                    pc.Add("WorkorderTypeId", "");
                    pc.Add("OrderLevel", "");
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["固话"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他电话"].ToString();
                    }


                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql1, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql2, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql3, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他电话"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00001";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固话"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00003";
                    }

                    switch (ds.Tables[0].Rows[i]["客户来源"].ToString())
                    {
                        case "68项目(05)":
                            pc["CustomerSource"].Value = "C00068";
                            pc["WorkorderTypeId"].Value = "D857EBB2-BF45-480D-8F33-D56F2B8D1D4A";
                            break;


                        default:
                            continue;
                            //throw new Exception("客户来源不明确");
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12007060000 + i);


                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["客户姓名"].ToString());
                    pc.Add("MemoContent", ds.Tables[0].Rows[i]["客户备注"].ToString());
                    pc.Add("Sex", "2");

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单描述"].ToString());

                    //pc.Add("OrderLevel", "C00002");

                    //switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    //{
                    //    case "高":
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;

                    //    case "中":
                    //        pc.Add("OrderLevel", "C00002");
                    //        break;

                    //    case "低":
                    //        pc.Add("OrderLevel", "C00001");
                    //        break;

                    //    default:
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;
                    //}

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");
                    pc["CustomerId"].Value = ds.Tables[0].Rows[i]["id"].ToString();

                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        //if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        //{
                        //    if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                        //    {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button13.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button14.Enabled = false;


            //string sql3 = @"delete from C_BZXX where cID in(select id from C_Info where CLY = 'C00068')";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql3); 

            //string sql5 = @"delete from work_order_info where rel_customer_id in (select id from C_Info where CLY = 'C00068')";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql5);


            //string sql4 = @"delete from C_Info where CLY = 'C00068'";
            //NewDbHelper.OldDB.IData.ExecuteNonQuery(sql4); 


            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
        $WorkorderTypeId$,
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";


            string checksql1 = "select count(1) from c_info where sj = $MobilePhone$";
            string checksql2 = "select count(1) from c_info where tel = $HomePhone$";
            string checksql3 = "select count(1) from c_info where othertel = $OtherPhone$";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.OldDB.IData.ExecuteDataSet("select * from school_68_7");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");
                    pc.Add("CustomerSource", "");
                    pc.Add("WorkorderTypeId", "");
                    pc.Add("OrderLevel", "");
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["住宅"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他号码"].ToString();
                    }


                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql1, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql2, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString())
                        && NewDbHelper.OldDB.IData.ExecuteScalar(checksql3, pc).ToString() != "0")
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == true
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00001";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == true)
                    {
                        pc["OrderLevel"].Value = "C00002";
                    }

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                        && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == false)
                    {
                        pc["OrderLevel"].Value = "C00003";
                    }

                    switch (ds.Tables[0].Rows[i]["客户来源"].ToString())
                    {
                        case "68项目(07)":
                            pc["CustomerSource"].Value = "C00070";
                            pc["WorkorderTypeId"].Value = "83B8E902-8DD2-4824-9961-9805E5261415";
                            break;


                        default:
                            continue;
                            //throw new Exception("客户来源不明确");
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12007070000 + i);


                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["客户姓名"].ToString());
                    pc.Add("MemoContent", ds.Tables[0].Rows[i]["客户备注"].ToString());
                    pc.Add("Sex", "2");

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单备注"].ToString());

                    //pc.Add("OrderLevel", "C00002");

                    //switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    //{
                    //    case "高":
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;

                    //    case "中":
                    //        pc.Add("OrderLevel", "C00002");
                    //        break;

                    //    case "低":
                    //        pc.Add("OrderLevel", "C00001");
                    //        break;

                    //    default:
                    //        pc.Add("OrderLevel", "C00003");
                    //        break;
                    //}

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");


                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button14.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Enabled = false;

            string insertCustomerSQL = @"insert into C_Info
                                            (ID, CName, CSex, CLY, CSNF, CSYF, SJ, Tel, BGTel, OtherTel, CLevel, AddDate, CSource, CSType, C_Address)
      values
     ( 
        $CustomerId$, 
        $CustomerName$, 
        $Sex$,   -- 性别(0男，1女，2不详)
        $CustomerSource$,
        null, --出生年份
        null, --出生月份
        $MobilePhone$,
        $HomePhone$, -- 固定电话
        '', -- 办公电话
        $OtherPhone$, -- 其他电话
        'C00001', -- 级别
        getdate(),
        'T060009186',  --$UserId$, -- 客户归属
        1,
        '深圳')";

            string insertCustomerMemoSQL = @"
insert into C_BZXX
(CID,AddDate,UserID,Notes)
values
($CustomerId$,getdate(),'T060009186',$MemoContent$)";

            string insertWorkorderSQL = @"insert into work_order_info(
        work_order_id, workorder_code, workorder_type, now_result_id,
        rel_usergroup_id, 
        [level], 
        rel_customer_id, 
        rel_order_id,
        created_on, created_by, status_code,C_ntext) 
      values
     ( 
        NEWID(),
        $WorkorderCode$,
        $WorkorderTypeId$,
       '0',
      'dd4d590e-150f-4728-900c-ac2f7b966d9a',
      $OrderLevel$,
      $CustomerId$,
      '',
      GETDATE(),
      'T060009186', 
      1,
      $OrderMemo$
      )                ";


            string checksql1 = "select count(1) from c_info where sj = $MobilePhone$";
            string checksql2 = "select count(1) from c_info where tel = $HomePhone$";
            string checksql3 = "select count(1) from c_info where othertel = $OtherPhone$";

            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\副本68项目（01）.xls");
            DataSet ds = NewDbHelper.OldDB.IData.ExecuteDataSet("select * from school_68_8");

            string oldCustomerId = null;
            int skip = 0;
            int succ = 0;
            int failed = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ParameterCollection pc = new ParameterCollection();


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pc.Clear();

                    pc.Add("MobilePhone", "");
                    pc.Add("HomePhone", "");
                    pc.Add("OtherPhone", "");
                    pc.Add("CustomerSource", "");
                    pc.Add("WorkorderTypeId", "");
                    //pc.Add("OrderLevel", "");
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()))
                    {
                        pc["MobilePhone"].Value = ds.Tables[0].Rows[i]["手机"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["固定电话"].ToString()))
                    {
                        pc["HomePhone"].Value = ds.Tables[0].Rows[i]["固定电话"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()))
                    {
                        pc["OtherPhone"].Value = ds.Tables[0].Rows[i]["其他号码"].ToString();
                    }


                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql1, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql2, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString())
                    //    && NewDbHelper.OldDB.IData.ExecuteScalar(checksql3, pc).ToString() != "0")
                    //{
                    //    continue;
                    //}

                    //if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == true
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()) == true)
                    //{
                    //    pc["OrderLevel"].Value = "C00002";
                    //}

                    //if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == true
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == false
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["其他号码"].ToString()) == false)
                    //{
                    //    pc["OrderLevel"].Value = "C00001";
                    //}

                    //if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == true)
                    //{
                    //    pc["OrderLevel"].Value = "C00002";
                    //}

                    //if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["手机"].ToString()) == false
                    //    && string.IsNullOrEmpty(ds.Tables[0].Rows[i]["住宅"].ToString()) == false)
                    //{
                    //    pc["OrderLevel"].Value = "C00003";
                    //}

                    switch (ds.Tables[0].Rows[i]["客户来源"].ToString())
                    {
                        case "68项目（08）":
                            pc["CustomerSource"].Value = "C00071";
                            pc["WorkorderTypeId"].Value = "ED020179-BBDD-4260-8EF3-1852F26622BF";
                            break;


                        default:
                            continue;
                            //throw new Exception("客户来源不明确");
                            break;
                    }

                    oldCustomerId = string.Format("{0}", 12007140000 + i);


                    pc.Add("WorkorderCode", "WK" + oldCustomerId);
                    pc.Add("CustomerId", "D" + oldCustomerId);
                    pc.Add("CustomerName", ds.Tables[0].Rows[i]["姓名"].ToString());
                    pc.Add("MemoContent", ds.Tables[0].Rows[i]["客户备注"].ToString());
                    pc.Add("Sex", "2");

                    pc.Add("OrderMemo", ds.Tables[0].Rows[i]["工单描述"].ToString());

                   // pc.Add("OrderLevel", "C00002");

                    switch (ds.Tables[0].Rows[i]["工单级别"].ToString())
                    {
                        case "高":
                            pc.Add("OrderLevel", "C00003");
                            break;

                        case "中":
                            pc.Add("OrderLevel", "C00002");
                            break;

                        case "低":
                            pc.Add("OrderLevel", "C00001");
                            break;

                        default:
                            pc.Add("OrderLevel", "C00003");
                            break;
                    }

                    //pc.Add("OrderLevel", (ds.Tables[0].Rows[i]["工单级别"].ToString() == "高") ? "C00003" : "C00001");
                    pc.Add("UserId", "T060009186");


                    try
                    {
                        NewDbHelper.OldDB.BeginTransaction();

                        if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerSQL, pc) == 1)
                        {
                            if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertCustomerMemoSQL, pc) == 1)
                            {
                                if (NewDbHelper.OldDB.IData.ExecuteNonQuery(insertWorkorderSQL, pc) == 1)
                                {
                                    NewDbHelper.OldDB.CommitTransaction();
                                    succ++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NewDbHelper.OldDB.RollbackTransaction();
                        logBox.Text += string.Format("插入数据异常{0}", ex.Message + ex.StackTrace);
                        failed++;
                    }
                }

                logBox.Text += string.Format("总记录数{0}行，成功导入{1}行， 忽略{2}行，失败{3}行", ds.Tables[0].Rows.Count, succ, skip, failed);
            }

            button15.Enabled = true;

        }
    }
}
