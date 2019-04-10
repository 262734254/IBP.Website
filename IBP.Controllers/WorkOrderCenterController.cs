using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IBP.Services;
using IBP.Models;
using IBP.Common;
using Framework.Utilities;

namespace IBP.Controllers
{
    public class WorkOrderCenterController : BaseController
    {
        /// <summary>
        /// 批量创建工单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult BatchAddWorkOrder()
        {
            return View();
        }
        /// <summary>
        /// 批量创建工单
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        [AuthorizeFlag]
        public JsonResult DoBatchCustomerWorkOrder()
        {
          
            string message = "操作失败，请与管理员联系";
            WorkorderInfoModel workOrderInfo = new WorkorderInfoModel();
            workOrderInfo.WorkorderType = GetFormData("workordertype");
            if (string.IsNullOrEmpty(workOrderInfo.WorkorderType) || workOrderInfo.WorkorderType == "All")
            {
                return FailedJson("参数错误，请选择当前工单的类型(子级分类)");
            }

            workOrderInfo.Description = GetFormData("workOrderDesc");
            if (string.IsNullOrEmpty(GetFormData("proimary")))
            {
                return FailedJson("参数错误，请选择紧急程度");
            }
            workOrderInfo.Level = GetFormData("proimary");
            workOrderInfo.NowStatusId = GetFormData("workTypeStatus");
            workOrderInfo.NowResultId = GetFormData("workTypeResult");

  
            //workOrderInfo.RelCustomerId = customerId;
            workOrderInfo.RelOrderId = GetFormData("relorderDesc");
            if (Request.Form["userGroupList"] != null)
            {
                workOrderInfo.RelUsergroupId = Request.Form.GetValues("userGroupList")[0];

                if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
                {
                    workOrderInfo.NowProcessUserid = Request.Form.GetValues("groupUserList")[0];
                }
            }
            else
            {
                workOrderInfo.RelUsergroupId = SessionUtil.Current.UserGroup[0];
                workOrderInfo.NowProcessUserid = SessionUtil.Current.UserId;
            }
            if (!string.IsNullOrEmpty(GetFormData("advanceTime")))
            {
                workOrderInfo.AdvanceTime = Convert.ToDateTime(GetFormData("advanceTime"));
            }

            if (!string.IsNullOrEmpty(GetFormData("expiredTime")))
            {
                workOrderInfo.ExpiredTime = Convert.ToDateTime(GetFormData("expiredTime"));
            }

            CustomerContactInfoModel contactInfo  =null; 
            if (GetFormData("hasContactRecords") == "on")
            {
                contactInfo= new CustomerContactInfoModel();
                contactInfo.CustomerId = "";
                contactInfo.Description = workOrderInfo.Description;
                contactInfo.Directions = (GetFormData("calldirection") == "1") ? 1 : 2;
                contactInfo.Purpose = GetFormData("purpose");
                contactInfo.Results = GetFormData("result");
            }
               BatchAddWorkOrder batchAddWorkOrder = new BatchAddWorkOrder(WorkorderInfoService.Instance.GetBatchAddWorkOrder);
                batchAddWorkOrder = new BatchAddWorkOrder(WorkorderInfoService.Instance.GetBatchAddWorkOrder);
               if (WorkorderInfoService.Instance.DelegateBatchAddWorkOrder(workOrderInfo, contactInfo, out message, batchAddWorkOrder))
               {

                   return SuccessedJson(message, "CallCenter_CustomerMgr", "CallCenter_CustomerMgr", "closeCurrent", "/CallCenter/CustomerMgr");
               }
               else
               {
                   return FailedJson(message);
               }
     
           
        }
  
         /// <summary>
        /// 工单类型处理结果操作。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditWorkOrderTypeResult()
        {
            return View();
        }
        /// <summary>
        /// 编辑工单类型。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditWorkOrderTypeStatus()
        {
            return View();
        }
        /// <summary>
        /// 用户待审批工单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaittingApprovalWorkOrderForMe()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            ViewBag.WorkOrderProcessStatus = GetFormData("processStatus");
            ViewBag.WorkOrderAssignedStatus = GetFormData("assignedStatus");
            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.Owner, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.All, WorkOrderRemindType.All, WorkOrderCustomStatus.WaittingApproval, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }


        /// <summary>
        /// 用户已关闭工单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ClosedWorkOrderForMe()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowStatus = GetFormData("nowStatus");
            if (nowStatus != "All" && string.IsNullOrEmpty(nowStatus) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_status_id";
                queryItem.SearchValue = nowStatus;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(nowResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowContactResult = GetFormData("nowContactResult");
            if (nowContactResult != "All" && string.IsNullOrEmpty(nowContactResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_contact_result";
                queryItem.SearchValue = nowContactResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            WorkOrderRemindType workOrderRemind = WorkOrderRemindType.All;
            switch (GetFormData("workRemind"))
            {
                case "noset":
                    workOrderRemind = WorkOrderRemindType.NoSet;
                    ViewBag.WorkOrderRemind = "noset";
                    break;

                case "advanceOrder":
                    workOrderRemind = WorkOrderRemindType.Advance;
                    ViewBag.WorkOrderRemind = "advanceOrder";
                    break;

                case "expiredOrder":
                    workOrderRemind = WorkOrderRemindType.Expired;
                    ViewBag.WorkOrderRemind = "expiredOrder";
                    break;

                case "TwoHourAppointmentExpired":
                    workOrderRemind = WorkOrderRemindType.TwoHourAppointmentExpired;
                    ViewBag.WorkOrderRemind = "TwoHourAppointmentExpired";
                    break;
                case "TwentyFourExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyFourExpired;
                    ViewBag.WorkOrderRemind = "TwentyFourExpired";
                    break;
                case "ThreeDayExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayExpired";
                    break;
                case "AllWillBeExpire":
                    workOrderRemind = WorkOrderRemindType.AllWillBeExpire;
                    ViewBag.WorkOrderRemind = "AllWillBeExpire";
                    break;
                case "TwohoursWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwohoursWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwohoursWillBeExpired";
                    break;
                case "TwentyWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwentyWillBeExpired";
                    break;
                case "ThreeDayWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayWillBeExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayWillBeExpired";
                    break;
                case "AllExpired":
                    workOrderRemind = WorkOrderRemindType.AllExpired;
                    ViewBag.WorkOrderRemind = "AllExpired";
                    break;

                default:
                    break;
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.Owner, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.Closed, workOrderRemind, WorkOrderCustomStatus.All, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

      
        /// <summary>
        /// 用户已质检工单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult QualityCheckedWorkOrderForMe()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            ViewBag.WorkOrderProcessStatus = GetFormData("processStatus");
            ViewBag.WorkOrderAssignedStatus = GetFormData("assignedStatus");
            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.Owner, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.All, WorkOrderRemindType.All, WorkOrderCustomStatus.QualityChecked, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

        /// <summary>
        /// 待处理工单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult PendingWorkOrder()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
               queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
           


            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            WorkOrderRemindType workOrderRemind = WorkOrderRemindType.All;
            switch (GetFormData("workRemind"))
            {
                case "noset":
                    workOrderRemind = WorkOrderRemindType.NoSet;
                    ViewBag.WorkOrderRemind = "noset";
                    break;

                case "advanceOrder":
                    workOrderRemind = WorkOrderRemindType.Advance;
                    ViewBag.WorkOrderRemind = "advanceOrder";
                    break;

                case "expiredOrder":
                    workOrderRemind = WorkOrderRemindType.Expired;
                    ViewBag.WorkOrderRemind = "expiredOrder";
                    break;

                case "TwoHourAppointmentExpired":
                    workOrderRemind = WorkOrderRemindType.TwoHourAppointmentExpired;
                    ViewBag.WorkOrderRemind = "TwoHourAppointmentExpired";
                    break;
                case "TwentyFourExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyFourExpired;
                    ViewBag.WorkOrderRemind = "TwentyFourExpired";
                    break;
                case "ThreeDayExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayExpired";
                    break;
                case "AllWillBeExpire":
                    workOrderRemind = WorkOrderRemindType.AllWillBeExpire;
                    ViewBag.WorkOrderRemind = "AllWillBeExpire";
                    break;
                case "TwohoursWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwohoursWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwohoursWillBeExpired";
                    break;
                case "TwentyWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwentyWillBeExpired";
                    break;
                case "ThreeDayWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayWillBeExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayWillBeExpired";
                    break;
                case "AllExpired":
                    workOrderRemind = WorkOrderRemindType.AllExpired;
                    ViewBag.WorkOrderRemind = "AllExpired";
                    break;

                default:
                    break;
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.Owner,WorkOrderAssignedStatus.All,  WorkOrderProcessStatus.Waitting, workOrderRemind, WorkOrderCustomStatus.All, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

        /// <summary>
        /// 处理中工单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ProcessingWorkOrder()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowStatus = GetFormData("nowStatus");
            if (nowStatus != "All" && string.IsNullOrEmpty(nowStatus) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_status_id";
                queryItem.SearchValue = nowStatus;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(nowResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowContactResult = GetFormData("nowContactResult");
            if (nowContactResult != "All" && string.IsNullOrEmpty(nowContactResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_contact_result";
                queryItem.SearchValue = nowContactResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            WorkOrderRemindType workOrderRemind = WorkOrderRemindType.All;
            switch (GetFormData("workRemind"))
            {
                case "noset":
                    workOrderRemind = WorkOrderRemindType.NoSet;
                    ViewBag.WorkOrderRemind = "noset";
                    break;

                case "advanceOrder":
                    workOrderRemind = WorkOrderRemindType.Advance;
                    ViewBag.WorkOrderRemind = "advanceOrder";
                    break;

                case "expiredOrder":
                    workOrderRemind = WorkOrderRemindType.Expired;
                    ViewBag.WorkOrderRemind = "expiredOrder";
                    break;

                case "TwoHourAppointmentExpired":
                    workOrderRemind = WorkOrderRemindType.TwoHourAppointmentExpired;
                    ViewBag.WorkOrderRemind = "TwoHourAppointmentExpired";
                    break;
                case "TwentyFourExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyFourExpired;
                    ViewBag.WorkOrderRemind = "TwentyFourExpired";
                    break;
                case "ThreeDayExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayExpired";
                    break;
                case "AllWillBeExpire":
                    workOrderRemind = WorkOrderRemindType.AllWillBeExpire;
                    ViewBag.WorkOrderRemind = "AllWillBeExpire";
                    break;
                case "TwohoursWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwohoursWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwohoursWillBeExpired";
                    break;
                case "TwentyWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwentyWillBeExpired";
                    break;
                case "ThreeDayWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayWillBeExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayWillBeExpired";
                    break;
                case "AllExpired":
                    workOrderRemind = WorkOrderRemindType.AllExpired;
                    ViewBag.WorkOrderRemind = "AllExpired";
                    break;

                default:
                    break;
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.Owner, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.Processing, workOrderRemind, WorkOrderCustomStatus.All, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

        /// <summary>
        /// 工单详细信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderDetail()
        {
            InitCustomerBasicInfo();
            return View();
        }

        /// <summary>
        /// 新增工单处理记录视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewWorkOrderProcessRecord()
        {
            return View();
        }

        /// <summary>
        /// 工单质检管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderQualityCheckedMgr()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            ViewBag.WorkOrderProcessStatus = GetFormData("processStatus");
            ViewBag.WorkOrderAssignedStatus = GetFormData("assignedStatus");
            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.All, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.All, WorkOrderRemindType.All, WorkOrderCustomStatus.WaittingQualityCheck, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

        /// <summary>
        /// 工单审批管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderApprovalMgr()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            ViewBag.WorkOrderProcessStatus = GetFormData("processStatus");
            ViewBag.WorkOrderAssignedStatus = GetFormData("assignedStatus");
            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.All, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.All, WorkOrderRemindType.All, WorkOrderCustomStatus.WaittingApproval, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }

        /// <summary>
        /// 工单审批视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewWorkOrderApproval()
        {
            return View();
        }

        /// <summary>
        /// 执行工单审批操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoWorkOrderApproval()
        {
            string message = "操作失败，请与管理员联系";
            string pageName = GetFormData("pageName");

            string assUserId = null;
            string assUserGroupId = null;
            string approvalDesc = GetFormData("approvalDesc");

            List<string> workOrderIdList = GetFormData("workOrderIdList").Split(',').ToList();

            WorkOrderApprovalAction action = WorkOrderApprovalAction.QualityChecked;
            switch (GetFormData("approvalAction"))
            {
                case "assignment":
                    action = WorkOrderApprovalAction.Assignment;
                    if (Request.Form["userGroupList"] != null)
                    {
                        assUserGroupId = Request.Form.GetValues("userGroupList")[0];

                        if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
                        {
                            assUserId = Request.Form.GetValues("groupUserList")[0];
                        }
                    }
                    else
                    {
                        if (workOrderIdList != null && workOrderIdList.Count == 1)
                        {
                            WorkOrderDomainModel workOrderDomainModel = WorkorderInfoService.Instance.GetWorkorderDomainModelById(workOrderIdList[0], false);
                            if (workOrderDomainModel != null)
                            {
                                assUserGroupId = workOrderDomainModel.BasicInfo.RelUsergroupId;
                                assUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;
                            }
                        }
                    }

                    break;
                case "qualitychecked":
                    action = WorkOrderApprovalAction.QualityChecked;
                    break;
                case "closeworkorder":
                    action = WorkOrderApprovalAction.CloseWorkOrder;
                    break;

                default:
                    break;
            }

            if (WorkorderInfoService.Instance.ApprovalWorkOrder(workOrderIdList, action, assUserGroupId, assUserId, approvalDesc, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderIdList[0], workOrderIdList[0], "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderIdList[0]);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 工单质检视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NewWorkOrderQualityChecked()
        {
            return View();
        }

        /// <summary>
        /// 工单质检操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewWorkOrderQualityChecked()
        {
            string message = "操作失败，请与管理员联系";
            string pageName = GetFormData("pageName");

            string assUserId = null;
            string assUserGroupId = null;
            string qualityCheckedDesc = GetFormData("qualitycheckedDesc");

            List<string> workOrderIdList = GetFormData("workOrderIdList").Split(',').ToList();

            WorkOrderApprovalAction action = WorkOrderApprovalAction.QualityChecked;
            switch (GetFormData("approvalAction"))
            {
                case "assignment":
                    action = WorkOrderApprovalAction.Assignment;
                    if (Request.Form["userGroupList"] != null)
                    {

                        assUserGroupId = Request.Form.GetValues("userGroupList")[0];

                        if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
                        {
                            assUserId = Request.Form.GetValues("groupUserList")[0];
                        }
                    }
                    else
                    {
                        if (workOrderIdList != null && workOrderIdList.Count == 1)
                        {
                            WorkOrderDomainModel workOrderDomainModel = WorkorderInfoService.Instance.GetWorkorderDomainModelById(workOrderIdList[0], false);
                            if (workOrderDomainModel != null)
                            {
                                assUserGroupId = workOrderDomainModel.BasicInfo.RelUsergroupId;
                                assUserId = workOrderDomainModel.BasicInfo.NowProcessUserid;
                            }
                        }
                    }
                    break;
                case "approval":
                    action = WorkOrderApprovalAction.SubmitApproval;
                    break;
                case "closeworkorder":
                    action = WorkOrderApprovalAction.CloseWorkOrder;
                    break;

                default:
                    break;
            }

            if (WorkorderInfoService.Instance.QualityCheckedWorkOrder(workOrderIdList, action, assUserGroupId, assUserId, qualityCheckedDesc, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderIdList[0], workOrderIdList[0], "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderIdList[0]);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 提交工单审批。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSubmitApprovalWorkOrder()
        {
            string message = "操作失败，请与管理员联系";
            string workOrderId = GetQueryString("oid");
            string pageName = GetQueryString("page");

            List<string> workOrderIdList = new List<string>();
            if (workOrderId != null)
            {
                workOrderIdList.Add(workOrderId);
            }
            else
            {
                workOrderIdList = Request.Form.GetValues("ids").ToList();
            }

            if (WorkorderInfoService.Instance.SubmitApprovalWorkOrder(workOrderIdList, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "forward", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "forward", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "forward", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "forward", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "forward", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 提交工单质检。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSubmitQualityCheckedWorkOrder()
        {
            string message = "操作失败，请与管理员联系";
            string workOrderId = GetQueryString("oid");
            string pageName = GetQueryString("page");

            List<string> workOrderIdList = new List<string>();
            if (workOrderId != null)
            {
                workOrderIdList.Add(workOrderId);
            }
            else
            {
                workOrderIdList = Request.Form.GetValues("ids").ToList();
            }

            if (WorkorderInfoService.Instance.SubmitQualityCheckedWorkOrder(workOrderIdList, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "forward", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "forward", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "forward", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "forward", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "forward", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 关闭工单。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoCloseWorkOrder()
        {
            string message = "操作失败，请与管理员联系";
            string workOrderId = GetQueryString("oid");
            string pageName = GetQueryString("page");

            List<string> workOrderIdList = new List<string>();
            if (workOrderId != null)
            {
                workOrderIdList.Add(workOrderId);
            }
            else
            {
                workOrderIdList = Request.Form.GetValues("ids").ToList();
            }

            if (WorkorderInfoService.Instance.CloseWorkOrder(workOrderIdList, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "forward", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "forward", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "forward", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "forward", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "forward", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 添加工单处理记录操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewWorkOrderProcessRecord()
        {
            string message = "操作失败，请与管理员联系";
            string workOrderId = GetFormData("workOrderId");
            string customerId = GetFormData("customerId");
            string pageName = GetFormData("pageName");
            bool closeOrder = GetFormData("closeWorkOrder") == "true";
           string advance_time=GetFormData("advanceTime");
            WorkorderProcessInfoModel processInfo = new WorkorderProcessInfoModel();
            processInfo.WorkorderId = workOrderId;
            processInfo.AfterStatus = GetFormData("workTypeStatus");            

            processInfo.AfterResult = GetFormData("workTypeResult");
            if (string.IsNullOrEmpty(processInfo.AfterResult))
            {
                return FailedJson("请选择当前处理结果。");
            }


            processInfo.Description = GetFormData("workOrderDesc");
            WorkOrderDomainModel WorkOrder = WorkorderInfoService.Instance.GetWorkorderDomainModelById(workOrderId, false);
            WorkOrderTypeDomainModel typeDomainModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(WorkOrder.BasicInfo.WorkorderType, false);
            if (typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "跟进意向C（0.1%-30%）"
                ||typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "跟进意向（X）"
                ||typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "跟进意向（Y）"
                ||typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "跟进意向C（30.1%-60%）"
                ||typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "跟进意向C（60.1%-99%）")
            {
                if (string.IsNullOrEmpty(advance_time))
                {
                    return FailedJson("请填写预约时间");
                }

            }
            if (typeDomainModel.ResultList[processInfo.AfterResult].ResultName == "结束")
              
            {
                if (string.IsNullOrEmpty(GetFormData("workOrderDesc")))
                {
                    return FailedJson("请填写描述记录");
                }
            }
            string assignedGroupId = null;
            if (Request.Form["userGroupList"] != null)
            {
                assignedGroupId = Request.Form.GetValues("userGroupList")[0];
            }

            string assignedUserId = null;
            if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
            {
                assignedUserId = Request.Form.GetValues("groupUserList")[0];
            }
            
            CustomerContactInfoModel contactInfo = null;
            if (GetFormData("hasContactRecords") == "on")
            {
                contactInfo = new CustomerContactInfoModel();
                contactInfo.CustomerId = customerId;
                contactInfo.CustomerPhone = (string.IsNullOrEmpty(GetFormData("otherNumber"))) ? GetFormData("contactPhone") : GetFormData("otherNumber");
                if (contactInfo.CustomerPhone == "" || contactInfo.CustomerPhone == null)
                {
                    return FailedJson("参数错误，请填写联系记录的联系号码");
                }
                contactInfo.Description = processInfo.Description;
                contactInfo.Directions = (GetFormData("calldirection") == "1") ? 1 : 2;
                contactInfo.Purpose = GetFormData("purpose");
                contactInfo.Results = GetFormData("result");

                if (string.IsNullOrEmpty(contactInfo.Results))
                {
                    return FailedJson("参数错误，请填写联系结果。");
                }
            }
          
            if (WorkorderInfoService.Instance.CreateNewWorkorderProcessRecord(workOrderId, advance_time, closeOrder,assignedGroupId,assignedUserId, processInfo, contactInfo, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 工单转交视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderAssignment()
        {
            return View();
        }

        /// <summary>
        /// 工单管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderManager()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId == "root")
            {
                return FailedJson("请选择一个二级分类的工单类型进行搜索过滤。");
            }

            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowStatus = GetFormData("nowStatus");
            if (nowStatus != "All" && string.IsNullOrEmpty(nowStatus) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_status_id";
                queryItem.SearchValue = nowStatus;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowResult = GetFormData("nowResult");
            if (nowResult != "All" && string.IsNullOrEmpty(nowResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_result_id";
                queryItem.SearchValue = nowResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string nowContactResult = GetFormData("nowContactResult");
            if (nowContactResult != "All" && string.IsNullOrEmpty(nowContactResult) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "now_contact_result";
                queryItem.SearchValue = nowContactResult;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string customer_name = GetFormData("customer_name_out");
            if (string.IsNullOrEmpty(customer_name) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_name";
                queryItem.SearchValue = customer_name;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string phoneNumber = GetFormData("phone_number");
            if (string.IsNullOrEmpty(phoneNumber) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "mobile_phone";
                queryItem.SearchValue = phoneNumber;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }


      
            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));
                        queryItem.BeginTime = Convert.ToDateTime(GetFormData("begin_" + queryItem.FieldType));
                        queryItem.EndTime = Convert.ToDateTime(GetFormData("end_" + queryItem.FieldType));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }

            WorkOrderProcessStatus processStatusEnum  = WorkOrderProcessStatus.All;
            switch (GetFormData("processStatus"))
            {
                case "0":
                    processStatusEnum = WorkOrderProcessStatus.Waitting;
                    break;

                case "1":
                    processStatusEnum = WorkOrderProcessStatus.Processing;
                    break;

                case "2":
                    processStatusEnum = WorkOrderProcessStatus.Closed;
                    break;

                default:
                    processStatusEnum = WorkOrderProcessStatus.All;
                    break;
            }

            WorkOrderAssignedStatus assignedStatusEnum = WorkOrderAssignedStatus.All;
            switch (GetFormData("assignedStatus"))
            {
                case "0":
                    assignedStatusEnum = WorkOrderAssignedStatus.UnAssigned;
                    break;
                case "1":
                    assignedStatusEnum = WorkOrderAssignedStatus.AssignedToGroup;
                    break;
                case "2":
                    assignedStatusEnum = WorkOrderAssignedStatus.AssignedToUser;
                    break;
                default:
                    assignedStatusEnum = WorkOrderAssignedStatus.All;
                    break;
            }

            WorkOrderRemindType workOrderRemind = WorkOrderRemindType.All;
            switch (GetFormData("workRemind"))
            {
                case "noset":
                    workOrderRemind = WorkOrderRemindType.NoSet;
                    ViewBag.WorkOrderRemind = "noset";
                    break;

                case "advanceOrder":
                    workOrderRemind = WorkOrderRemindType.Advance;
                    ViewBag.WorkOrderRemind = "advanceOrder";
                    break;

                case "expiredOrder":
                    workOrderRemind = WorkOrderRemindType.Expired;
                    ViewBag.WorkOrderRemind = "expiredOrder";
                    break;

                case "TwoHourAppointmentExpired":
                    workOrderRemind = WorkOrderRemindType.TwoHourAppointmentExpired;
                    ViewBag.WorkOrderRemind = "TwoHourAppointmentExpired";
                    break;
                case "TwentyFourExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyFourExpired;
                    ViewBag.WorkOrderRemind = "TwentyFourExpired";
                    break;
                case "ThreeDayExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayExpired";
                    break;
                case "AllWillBeExpire":
                    workOrderRemind = WorkOrderRemindType.AllWillBeExpire;
                    ViewBag.WorkOrderRemind = "AllWillBeExpire";
                    break;
                case "TwohoursWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwohoursWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwohoursWillBeExpired";
                    break;
                case "TwentyWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwentyWillBeExpired";
                    break;
                case "ThreeDayWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayWillBeExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayWillBeExpired";
                    break;
                case "AllExpired":
                    workOrderRemind = WorkOrderRemindType.AllExpired;
                    ViewBag.WorkOrderRemind = "AllExpired";
                    break;

                default:
                    break;
            }

            //string end_time = GetFormData("end_time");
            //if (string.IsNullOrEmpty(end_time) == false)
            //{
            //    queryItem = new QueryItemDomainModel();
            //    queryItem.FieldType = "created_time";
            //    queryItem.SearchValue = end_time;
            //    queryItem.EndTime = Convert.ToDateTime(end_time);
            //    queryItem.Operation = "between";

            //    queryCollection[queryItem.FieldType] = queryItem;
            //}
            ViewBag.WorkOrderProcessStatus = GetFormData("processStatus");
            ViewBag.WorkOrderAssignedStatus = GetFormData("assignedStatus");
            ViewBag.QueryCollection = queryCollection;

            int total = 0;

            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.All, assignedStatusEnum, processStatusEnum, workOrderRemind, WorkOrderCustomStatus.All, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();

        }

        /// <summary>
        /// 修改工单过期时间视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ChangeWorkorderExpiredTime()
        {
            return View();
        }

        /// <summary>
        /// 更改工单过期时间。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoChangeWorkorderExpiredTime()
        {

            string message = "操作失败，请与管理员联系";
            string workOrderId = GetFormData("workOrderIdList");
            string pageName = GetFormData("pageName");
            DateTime expiredTime = DateTime.Now.AddDays(-1);
            try
            {
                expiredTime = Convert.ToDateTime(GetFormData("expiredTime"));
            }
            catch
            {
                return FailedJson("操作失败，请填写正确的过期时间");
            }

            List<string> workOrderIdList = workOrderId.Split(',').ToList();


            if (WorkorderInfoService.Instance.ChangeWorkorderExpiredTime(workOrderIdList, expiredTime, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 修改工单预约时间视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ChangeWorkorderAdvanceTime()
        {
            return View();
        }

        /// <summary>
        /// 更改工单预约时间。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoChangeWorkorderAdvanceTime()
        {

            string message = "操作失败，请与管理员联系";
            string workOrderId = GetFormData("workOrderIdList");
            string pageName = GetFormData("pageName");
            DateTime advanceTime = DateTime.Now.AddDays(-1);
            try
            {
                advanceTime = Convert.ToDateTime(GetFormData("advanceTime"));
            }
            catch
            {
                return FailedJson("操作失败，请填写正确的预约时间");
            }

            List<string> workOrderIdList = workOrderId.Split(',').ToList();

            if (WorkorderInfoService.Instance.ChangeWorkorderAdvanceTime(workOrderIdList, advanceTime, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderId, workOrderId, "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderId);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 执行工单转交操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public ActionResult DoAssignmentWorkOrder()
        {
            string assUserId = null;
            string assUserGroupId = null;

            if (GetFormData("processFor") == "me")
            {
                UserDomainModel user = UserInfoService.Instance.GetUserDomainModelById(SessionUtil.Current.UserId, false);
                if (user.InGroupList == null && user.InGroupList.Count == 0)
                {
                    return FailedJson("操作失败，请联系管理员将您分配到一个有权限处理工单的用户组中");
                }

                assUserGroupId = user.InGroupList[0];
                assUserId = SessionUtil.Current.UserId;
            }
            else
            {
                if (Request.Form["userGroupList"] == null)
                {
                    return FailedJson("参数错误，请填写分配的用户组");
                }
                assUserGroupId = Request.Form.GetValues("userGroupList")[0];

                if (Request.Form["groupUserList"] != null && Request.Form.GetValues("groupUserList").Length > 0)
                {
                    assUserId = Request.Form.GetValues("groupUserList")[0];
                }
            }


            List<string> workOrderIdList = GetFormData("workOrderIdList").Split(',').ToList();
            string message = "";
            string pageName = GetFormData("pageName");

            if (WorkorderInfoService.Instance.AssignmentWorkOrder(workOrderIdList, assUserGroupId, assUserId, out message))
            {
                switch (pageName)
                {
                    case "detail":
                        return SuccessedJson(message, workOrderIdList[0], workOrderIdList[0], "closeCurrent", "/workordercenter/workorderdetail?oid=" + workOrderIdList[0]);
                    case "ingroup":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrderInGroup", "WorkOrderCenter_PendingWorkOrderInGroup", "closeCurrent", "/workordercenter/pendingworkorderingroup");
                    case "pending":
                        return SuccessedJson(message, "WorkOrderCenter_PendingWorkOrder", "WorkOrderCenter_PendingWorkOrder", "closeCurrent", "/workordercenter/pendingworkorder");
                    case "process":
                        return SuccessedJson(message, "WorkOrderCenter_ProcessingWorkOrder", "WorkOrderCenter_ProcessingWorkOrder", "closeCurrent", "/workordercenter/processingworkorder");
                    case "manager":
                        return SuccessedJson(message, "WorkOrderCenter_WorkOrderManager", "WorkOrderCenter_WorkOrderManager", "closeCurrent", "/workordercenter/workordermanager");
                    default:
                        break;
                }

                return FailedJson(message);
            }
            else
            {
                return FailedJson(message);
            }
        }
        
        /// <summary>
        /// 工单类型管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderTypeMgr()
        {
            return View();
        }

        /// <summary>
        /// 工单类型信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WorkOrderTypeDetails()
        {
            return View();
        }

        /// <summary>
        /// 获取工单类型信息领域模型。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public JsonResult GetWorkTypeDomainModelJson()
        {
            string typeId = GetQueryString("wotid");
            WorkOrderTypeDomainModel typeModel = WorkorderTypeInfoService.Instance.GetTypeDomainModelById(typeId, true);
            if (typeModel != null)
            {
                return Json(new { StatusList = typeModel.StatusList.ToList(), ResultList = typeModel.ResultList.ToList() });
            }
            else
            {
                return FailedJson("不存在的工单类型ID");
            }
        }

        /// <summary>
        /// 删除工单类型状态值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteWorkOrderTypeStatus()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.DeleteTypeStatusInfo(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 删除工单类型处理结果值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteWorkOrderTypeResult()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.DeleteTypeResultInfo(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 上移工单类型状态值。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveUpWorkOrderTypeStatus()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.MoveUpTypeStatusSortOrder(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 下移工单类型状态值。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveDownWorkOrderTypeStatus()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.MoveDownTypeStatusSortOrder(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 上移工单类型处理结果。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveUpWorkOrderTypeResult()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.MoveUpTypeResultSortOrder(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 下移工单类型处理结果。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveDownWorkOrderTypeResult()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (WorkorderTypeInfoService.Instance.MoveDownTypeResultSortOrder(dataId, valueId, out message))
            {
                return SuccessedJson(message, "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "forward", "/workordercenter/workordertypemgr?id=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 添加工单处理结果视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddWorkOrderTypeResult()
        {
            return View();
        }

        /// <summary>
        /// 添加工单类型状态视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddWorkOrderTypeStatus()
        {
            return View();
        }

        /// <summary>
        /// 本组待处理工单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult PendingWorkOrderInGroup()
        {
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            string customer_name = GetFormData("customer_name_out");
            if (string.IsNullOrEmpty(customer_name) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_name";
                queryItem.SearchValue = customer_name;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderTypeId = GetFormData("workorderType");
            if (workorderTypeId != "All" && workorderTypeId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_type";
                queryItem.SearchValue = workorderTypeId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string workorderLevelId = GetFormData("workorderLevel");
            if (workorderLevelId != "All" && string.IsNullOrEmpty(workorderLevelId) == false)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "workorder_info.level";
                queryItem.SearchValue = workorderLevelId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }


            string[] queryArr = Request.Form.AllKeys;
            if (queryArr != null)
            {
                foreach (string currKey in queryArr)
                {
                    if (currKey.StartsWith("sel_") && GetFormData(currKey) == "1")
                    {
                        queryItem = new QueryItemDomainModel();
                        queryItem.FieldType = currKey.Replace("sel_", "");
                        queryItem.SearchValue = GetFormData(queryItem.FieldType);
                        queryItem.Operation = GetFormData(currKey.Replace("sel_", "op_"));

                        queryCollection[queryItem.FieldType] = queryItem;
                    }
                }
            }
        
            WorkOrderRemindType workOrderRemind = WorkOrderRemindType.All;
            
            switch (GetFormData("workRemind"))
            {
                case "noset":
                    workOrderRemind = WorkOrderRemindType.NoSet;
                    ViewBag.WorkOrderRemind = "noset";
                    break;

                case "advanceOrder":
                    workOrderRemind = WorkOrderRemindType.Advance;
                    ViewBag.WorkOrderRemind = "advanceOrder";
                    break;

                case "expiredOrder":
                    workOrderRemind = WorkOrderRemindType.Expired;
                    ViewBag.WorkOrderRemind = "expiredOrder";
                    break;

                case "TwoHourAppointmentExpired":
                    workOrderRemind = WorkOrderRemindType.TwoHourAppointmentExpired;
                    ViewBag.WorkOrderRemind = "TwoHourAppointmentExpired";
                    break;
                case "TwentyFourExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyFourExpired;
                    ViewBag.WorkOrderRemind = "TwentyFourExpired";
                    break;
                case "ThreeDayExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayExpired";
                    break;
                case "AllWillBeExpire":
                    workOrderRemind = WorkOrderRemindType.AllWillBeExpire;
                    ViewBag.WorkOrderRemind = "AllWillBeExpire";
                    break;
                case "TwohoursWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwohoursWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwohoursWillBeExpired";
                    break;
                case "TwentyWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.TwentyWillBeExpired;
                    ViewBag.WorkOrderRemind = "TwentyWillBeExpired";
                    break;
                case "ThreeDayWillBeExpired":
                    workOrderRemind = WorkOrderRemindType.ThreeDayWillBeExpired;
                    ViewBag.WorkOrderRemind = "ThreeDayWillBeExpired";
                    break;
                case "AllExpired":
                    workOrderRemind = WorkOrderRemindType.AllExpired;
                    ViewBag.WorkOrderRemind = "AllExpired";
                    break;
                    
                default:
                    break;
            }

            ViewBag.QueryCollection = queryCollection;

            int total = 0;
            ViewBag.WorkorderIdList = WorkorderInfoService.Instance.GetWorkOrderIdList(GetWorkOrderRole.OwnerGroup, WorkOrderAssignedStatus.All, WorkOrderProcessStatus.Waitting, workOrderRemind, WorkOrderCustomStatus.All, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.WorkorderTotal = total;

            return View();
        }


        /// <summary>
        /// 编辑工单类型状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoEditWorkOrderTypeStatus()
        {
            WorkorderStatusInfoModel model = new WorkorderStatusInfoModel();
            model.WorkorderTypeId = GetFormData("workorderTypeId");
            model.WorkorderStatusId = GetFormData("workStatusId");
            model.StatusName = GetFormData("statusName");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.StatusTag = Convert.ToInt32(GetFormData("statustag"));
            model.Description = GetFormData("statusDesc");
            model.CustomStatus = GetFormData("customtag");

            if (WorkorderTypeInfoService.Instance.EditTypeStatusInfo(model))
            {
                return SuccessedJson("成功修改状态信息", "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "closeCurrent", "/workordercenter/workordertypemgr?id=" + model.WorkorderTypeId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }
        /// <summary>
        /// 新建工单类型状态操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewWorkOrderTypeStatus()
        {
            WorkorderStatusInfoModel model = new WorkorderStatusInfoModel();
            model.WorkorderStatusId = Guid.NewGuid().ToString();
            model.WorkorderTypeId = GetFormData("workOrderTypeId");
            model.StatusName = GetFormData("statusName");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.StatusTag = Convert.ToInt32(GetFormData("statustag"));
            model.Description = GetFormData("statusDesc");
            model.CustomStatus = GetFormData("customtag");

            if (WorkorderTypeInfoService.Instance.CreateTypeStatusInfo(model))
            {
                return SuccessedJson("成功添加工单类型状态值", "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "closeCurrent", "/workordercenter/workordertypemgr?id=" + model.WorkorderTypeId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }


        /// <summary>
        /// 编辑工单类型处理结果操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoEditWorkOrderTypeResult()
        {
            WorkorderResultInfoModel model = new WorkorderResultInfoModel();
            model.WorkorderResultId = GetFormData("workResultId");
            model.WorkorderTypeId = GetFormData("workOrderTypeId");
            model.ResultName = GetFormData("resultName");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.Description = GetFormData("resultDesc");
            model.IsBegin = Convert.ToInt32(GetFormData("isbegin"));

            if (WorkorderTypeInfoService.Instance.UpdateTypeResultInfo(model))
            {
                return SuccessedJson("成功修改工单类型处理结果", "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "closeCurrent", "/workordercenter/workordertypemgr?id=" + model.WorkorderTypeId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }
        /// <summary>
        /// 新建工单类型处理结果操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewWorkOrderTypeResult()
        {
            WorkorderResultInfoModel model = new WorkorderResultInfoModel();
            model.WorkorderResultId = Guid.NewGuid().ToString();
            model.WorkorderTypeId = GetFormData("workOrderTypeId");
            model.ResultName = GetFormData("resultName");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.Description = GetFormData("resultDesc");
            model.IsBegin = Convert.ToInt32(GetFormData("isbegin"));
            if (WorkorderTypeInfoService.Instance.CreateTypeResultInfo(model))
            {
                return SuccessedJson("成功添加工单类型处理结果", "WorkOrderCenter_WorkOrderTypeMgr", "WorkOrderCenter_WorkOrderTypeMgr", "closeCurrent", "/workordercenter/workordertypemgr?id=" + model.WorkorderTypeId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

     
        
    }
}
