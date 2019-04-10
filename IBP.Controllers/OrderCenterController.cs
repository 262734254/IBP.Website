using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using IBP.Services;
using IBP.Models;
using IBP.Common;
using Aspose.Cells;
using System.Web;
using System.Web.UI;

namespace IBP.Controllers
{
    public class OrderCenterController : BaseController
    {
           /// <summary>
        /// 订单日排行榜视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderRank()
        {
            return View();
        }

        /// <summary>
        /// 延缓质检单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DelayCheckOrder()
        {

            #region 订单管理
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            SalesOrderStatus orderStatus = SalesOrderStatus.WaitFollow;
            switch (GetFormData("orderStatus"))
            {

                case "1":
                    orderStatus = SalesOrderStatus.WaitFollow;
                    break;
                case "2":
                    orderStatus = SalesOrderStatus.WaitCharge;
                    break;
                case "3":
                    orderStatus = SalesOrderStatus.WaitCheck;
                    break;
                case "4":
                    orderStatus = SalesOrderStatus.WaitApproval;
                    break;
                case "5":
                    orderStatus = SalesOrderStatus.WaitOpening;
                    break;
                case "6":
                    orderStatus = SalesOrderStatus.WaitStocking;
                    break;
                case "7":
                    orderStatus = SalesOrderStatus.WaitDelivery;
                    break;
                case "8":
                    orderStatus = SalesOrderStatus.WaitSign;
                    break;
                case "9":
                    orderStatus = SalesOrderStatus.WaitRecover;
                    break;
                case "10":
                    orderStatus = SalesOrderStatus.Successed;
                    break;
                case "11":
                    orderStatus = SalesOrderStatus.Exception;
                    break;
                case "12":
                    orderStatus = SalesOrderStatus.WaitRefund;
                    break;
                case "13":
                    orderStatus = SalesOrderStatus.WaitReturns;
                    break;
                case "14":
                    orderStatus = SalesOrderStatus.Cancel;
                    break;
                case "15":
                    orderStatus = SalesOrderStatus.WaitCancelOpening;
                    break;

                default:
                    orderStatus = SalesOrderStatus.WaitFollow;
                    break;
            }
            // 订单编号
            string orderCode = GetFormData("orderCode");
            if (!string.IsNullOrEmpty(orderCode))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_code";
                queryItem.SearchValue = orderCode;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            string payType = GetFormData("payType");
            if (!string.IsNullOrEmpty(payType))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.pay_type";
                queryItem.SearchValue = payType;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string incomePhoneNumber = GetFormData("incomePhoneNumber");
            if (!string.IsNullOrEmpty(incomePhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_subsidiary_phonenumber";
                queryItem.SearchValue = incomePhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string selectedPhoneNumber = GetFormData("selectPhoneNumber");
            if (!string.IsNullOrEmpty(selectedPhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_main_phonenumber";
                queryItem.SearchValue = selectedPhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            // 客户名称
            string customerName = GetFormData("customerName");
            if (!string.IsNullOrEmpty(customerName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.customer_name";
                queryItem.SearchValue = customerName;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户银卡号码
            string pay_card_number = GetFormData("pay_card_number");
            if (!string.IsNullOrEmpty(pay_card_number))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "pay_card_number";
                queryItem.SearchValue = pay_card_number;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 订单创建人
            string createdUserName = GetFormData("createdUserName");
            if (!string.IsNullOrEmpty(createdUserName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "created_user_info.work_id";
                queryItem.SearchValue = "WORKID_" + createdUserName;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            #region 创建时间
            string createdBeginTime = GetFormData("createdBeginTime");
            string createdEndTime = GetFormData("createdEndTime");

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            #endregion
            #region 跟进预约时间
            string followBeginTime = GetFormData("followBeginTime");
            string followEndTime = GetFormData("followEndTime");

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == true && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.BeginTime = Convert.ToDateTime(followBeginTime);
                queryItem.EndTime = Convert.ToDateTime(followEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            #endregion
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(true, false, null, ViewBag.exception, incomePhoneNumber, selectedPhoneNumber, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            ViewBag.Paytype = payType;
            ViewBag.QueryCollection = queryCollection;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;

            ViewBag.followBeginTime = followBeginTime;
            ViewBag.followEndTime = followEndTime;
            ViewBag.OrderStatus = GetFormData("orderStatus");
            return View();

            #endregion
        }

        /// <summary>
        /// 我的成功单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult MySuccessedOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, true, SalesOrderStatus.Successed, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            return View();
        }

          /// <summary>
        /// 修改销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditSalesOrderTypeStatusInfo()
        {
            return View();
        }
          /// <summary>
        /// 删除销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DeleteSalesOrderTypeStatusInfo()
        {
            string typeid = GetQueryString("typeid");
            string statusid = GetQueryString("id");
            string message = "";

            if (SalesorderTypeStatusInfoService.Instance.DeleteSalesOrderTypeStatusById(statusid,  out message))
            {
                return SuccessedJson(message, "OrderCenter_SalesOrderTypeManager", "OrderCenter_SalesOrderTypeManager", "forward", "/OrderCenter/SalesOrderTypeManager?id=" + typeid);
            }
            else
            {
                return FailedJson(message);
            }
        }
        /// <summary>
        /// 修改销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DoEditSalesOrderTypeStatusInfo()
        {
            SalesorderTypeStatusInfoModel model = new SalesorderTypeStatusInfoModel();
            model.SalsorderStatusId = GetFormData("salsorder_status_id");
            model.SalesorderStatusName = GetFormData("salesorder_status_name");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.Description = GetFormData("description");
            model.SalesorderTypeId = GetFormData("salsorder_type_id");
            if (SalesorderTypeStatusInfoService.Instance.EditSalesOrderTypeStatusInfo(model))
            {
                return SuccessedJson("成功修改状态信息", "OrderCenter_SalesOrderTypeManager", "OrderCenter_SalesOrderTypeManager", "closeCurrent", "/OrderCenter/SalesOrderTypeManager?id=" + model.SalesorderTypeId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }


    


        /// <summary>
        /// 销售订单类型信息表。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderTypeManager()
        {
            return View();
        }
        /// <summary>
        /// 销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesorderTypeStatusManager()
        {
            return View();
        }
        /// <summary>
        /// 添加销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddSalesOrderTypeStatusInfo()
        {
            return View();
        }
        /// <summary>
        /// 添加销售订单类型状态信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult DoAddSalesOrderTypeStatusInfo()
        {
            string message = "操作失败，请与管理员联系";
            SalesorderTypeStatusInfoModel model = new SalesorderTypeStatusInfoModel();
            model.SalsorderStatusId = Guid.NewGuid().ToString();
            model.SalesorderTypeId = GetFormData("salesorder_typeId");
            model.SalesorderStatusName = GetFormData("salesorder_status_name");
            model.Status = Convert.ToInt32(GetFormData("status"));
            model.Description = GetFormData("description");
            model.PaymentType = Convert.ToInt32(GetFormData("payment_type"));
            if (SalesorderTypeStatusInfoService.Instance.CreateSalesOrderTypeStatusInfo(model, out message))
            {
                return SuccessedJson(message, "OrderCenter_SalesOrderTypeManager", "OrderCenter_SalesOrderTypeManager", "closeCurrent", "/OrderCenter/SalesOrderTypeManager?id=" + model.SalesorderTypeId);
            }
            else
            {
                return FailedJson(message);
            }
        }
        
 
        
        
        /// <summary>
        /// 所有异常订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AllExceptionOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.Exception, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            return View();
        }

        /// <summary>
        /// 我的异常订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult MyExceptionOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();
          
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, true, SalesOrderStatus.Exception, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            return View();
        }


        /// <summary>
        /// 订单管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult OrderManager()
        {


            #region 订单管理
            InitPagerForm();

            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;

            SalesOrderStatus orderStatus = SalesOrderStatus.WaitFollow;
            switch (GetFormData("orderStatus"))
            {
                case "0":
                    orderStatus = SalesOrderStatus.All;
                    break;
                case "1":
                    orderStatus = SalesOrderStatus.WaitFollow;
                    break;
                case "2":
                    orderStatus = SalesOrderStatus.WaitCharge;
                    break;
                case "3":
                    orderStatus = SalesOrderStatus.WaitCheck;
                    break;
                case "4":
                    orderStatus = SalesOrderStatus.WaitApproval;
                    break;
                case "5":
                    orderStatus = SalesOrderStatus.WaitOpening;
                    break;
                case "6":
                    orderStatus = SalesOrderStatus.WaitStocking;
                    break;
                case "7":
                    orderStatus = SalesOrderStatus.WaitDelivery;
                    break;
                case "8":
                    orderStatus = SalesOrderStatus.WaitSign;
                    break;
                case "9":
                    orderStatus = SalesOrderStatus.WaitRecover;
                    break;
                case "10":
                    orderStatus = SalesOrderStatus.Successed;
                    break;
                case "11":
                    orderStatus = SalesOrderStatus.Exception;
                    break;
                case "12":
                    orderStatus = SalesOrderStatus.WaitRefund;
                    break;
                case "13":
                    orderStatus = SalesOrderStatus.WaitReturns;
                    break;
                case "14":
                    orderStatus = SalesOrderStatus.Cancel;
                    break;
                case "15":
                    orderStatus = SalesOrderStatus.WaitCancelOpening;
                    break;

                default:
                    orderStatus = SalesOrderStatus.All;
                    break;
            }
            // 订单编号
            string orderCode = GetFormData("orderCode");
            if (!string.IsNullOrEmpty(orderCode))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_code";
                queryItem.SearchValue = orderCode;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            string payType = GetFormData("payType");
            if (!string.IsNullOrEmpty(payType))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.pay_type";
                queryItem.SearchValue = payType;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string incomePhoneNumber = GetFormData("incomePhoneNumber");
            if (!string.IsNullOrEmpty(incomePhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_subsidiary_phonenumber";
                queryItem.SearchValue = incomePhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string selectedPhoneNumber = GetFormData("selectPhoneNumber");
            if (!string.IsNullOrEmpty(selectedPhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_main_phonenumber";
                queryItem.SearchValue = selectedPhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            // 客户名称
            string customerName = GetFormData("customerName");
            if (!string.IsNullOrEmpty(customerName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.customer_name";
                queryItem.SearchValue = customerName;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户银卡号码
            string pay_card_number = GetFormData("pay_card_number");
            if (!string.IsNullOrEmpty(pay_card_number))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "pay_card_number";
                queryItem.SearchValue = pay_card_number;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 订单创建人
            string createdUserName = GetFormData("createdUserName");
            if (!string.IsNullOrEmpty(createdUserName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "created_user_info.work_id";
                queryItem.SearchValue = "WORKID_" + createdUserName;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            #region 创建时间
            string createdBeginTime = GetFormData("createdBeginTime");
            string createdEndTime = GetFormData("createdEndTime");

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            
            #endregion
            #region 跟进预约时间
            string followBeginTime = GetFormData("followBeginTime");
            string followEndTime = GetFormData("followEndTime");

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == true && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.BeginTime = Convert.ToDateTime(followBeginTime);
                queryItem.EndTime = Convert.ToDateTime(followEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            #endregion
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, orderStatus, ViewBag.exception, incomePhoneNumber, selectedPhoneNumber, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            ViewBag.Paytype = payType;
            ViewBag.QueryCollection = queryCollection;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;

            ViewBag.followBeginTime = followBeginTime;
            ViewBag.followEndTime = followEndTime;
            ViewBag.OrderStatus = GetFormData("orderStatus");
            return View();
            
            #endregion
            //DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\产品信息导入表2.xls");

            //string importLogs = null, message = null;
            //ProductInfoService.Instance.ImportProductInfoList(ds, out importLogs, out message);
            //ProductInfoService.Instance.BatchDeleteAllProductCategories();
            //ProductInfoService.Instance.ImportProductCategories(ds.Tables["产品类型信息表"], out importLogs, out message);
            //ProductInfoService.Instance.ImportProductCategoryAttributes(ds.Tables["产品类型分组属性信息表"], out importLogs, out message);

            //return View();
        }

        /// <summary>
        /// 已撤消单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult RevokedOrder()
        {

            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.Cancel, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }



        /// <summary>
        /// 销售订单详细信息。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderDetail()
        {
            ViewBag.SalesOrder = (Request.QueryString["sid"] == null) ? null : SalesOrderInfoService.Instance.GetSalesorderDomainModelById(Request.QueryString["sid"].ToString(), false);
            ViewBag.Customer = (ViewBag.SalesOrder == null) ? null : CustomerInfoService.Instance.GetCustomerDomainModelById(ViewBag.SalesOrder.BasicInfo.CustomerId, false);

            return View();
        }

        /// <summary>
        /// 我的待跟进订单。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult MyFollowOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, true, SalesOrderStatus.WaitFollow, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;
            return View();
        }


        #region 订单扣款

        /// <summary>
        /// 待扣款订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitChargeOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitCharge, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }

        /// <summary>
        /// 销售订单扣款视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderCharge()
        {
            return View();
        }


        /// <summary>
        /// 订单扣款操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderCharge()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("chargeType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitCheck, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    string posMachineId = GetFormData("ddlPosMachine");
                    if (string.IsNullOrEmpty(posMachineId))
                    {
                        return FailedJson("请选择扣款POS机");
                    }
                    string billCode = GetFormData("chargeBillCode");
                    string succDesc = GetFormData("opSuccDesc");
                    if (string.IsNullOrEmpty(billCode))
                    {
                        return FailedJson("请填写扣款凭证小票单号");
                    }
                    changeData.ChangeInfo["opType"] = "扣款成功";
                    changeData.ChangeInfo["opDesc"] = (succDesc == null) ? "" : succDesc;
                    changeData.ChangeInfo["billCode"] = billCode;
                    changeData.ChangeInfo["posMachineId"] = posMachineId;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("扣款异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "扣款异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;
                    break;

                default:
                    break;
            }

     
            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);

                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitChargeOrder", "OrderCenter_WaitChargeOrder", "closeCurrent", "/OrderCenter/WaitChargeOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);

        }

        #endregion

        #region 订单质检

        /// <summary>
        /// 销售订单质检视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderCheck()
        {
            return View();
        }

        /// <summary>
        /// 销售订单质检操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderCheck()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string checkType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitApproval, false);
            changeData.ChangeInfo["checkType"] = checkType;

            switch (checkType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写订单质检描述。");
                    } 
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("质检异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;
                    
                    break;

                case "2":
                    changeData.IsSuccessed = true;                    
                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case"orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitCheckOrder", "OrderCenter_WaitCheckOrder", "closeCurrent", "/OrderCenter/WaitCheckOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待质检订单视图
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitCheckOrder()
        {
            #region 待质检订单视图
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitCheck, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion
        }

        #endregion

        #region 订单审批

        /// <summary>
        /// 销售订单审批视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderApproval()
        {
            return View();
        }

        /// <summary>
        /// 订单审批操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderApproval()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitOpening, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写订单审批描述。");
                    }
                    string exceptionType = GetFormData("exceptionType");
                    if (string.IsNullOrEmpty(exceptionType))
                    {
                        return FailedJson("请选择操作异常原因。");
                    }
                    changeData.ChangeInfo["opTypeId"] =  GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("审批异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitApprovalOrder", "OrderCenter_WaitApprovalOrder", "closeCurrent", "/OrderCenter/WaitApprovalOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);

        }


        /// <summary>
        /// 待审批订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitApprovalOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitApproval, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }

        #endregion 

        #region 订单开户

        /// <summary>
        /// 销售订单开户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderOpening()
        {
            return View();
  
        }

        /// <summary>
        /// 订单开户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderOpening()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitStocking, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("开卡异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitOpeningOrder", "OrderCenter_WaitOpeningOrder", "closeCurrent", "/OrderCenter/WaitOpeningOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待开户订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitOpeningOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitOpening, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }

        #endregion
        
        #region 订单备货

        /// <summary>
        /// 销售订单备货视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderStocking()
        {
            return View();
        }

        /// <summary>
        /// 订单备货操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderStocking()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitDelivery, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] =  GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("备货异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitStockingOrder", "OrderCenter_WaitStockingOrder", "closeCurrent", "/OrderCenter/WaitStockingOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待备货订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitStockingOrder()
        {
            #region 待备货订单视图
            InitPagerForm();
            InitSalesorderQueryCollection();
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitStocking, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion
        }

        #endregion

        #region 订单发货

        /// <summary>
        /// 销售订单发货视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderDelivery()
        {
            return View();
        }

        /// <summary>
        /// 订单发货操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderDelivery()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitSign, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    string deliveryCompany = GetFormData("deliveryCompany");
                    if (string.IsNullOrEmpty(deliveryCompany))
                    {
                        return FailedJson("请选择物流配送公司");
                    }
                    string deliveryCode = GetFormData("deliveryCode");
                    if (string.IsNullOrEmpty(deliveryCode))
                    {
                        return FailedJson("请填写物流配送单号");
                    }
                    changeData.ChangeInfo["deliveryCompany"] = deliveryCompany;
                    changeData.ChangeInfo["deliveryCode"] = deliveryCode;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常备注信息");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("发货异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitDeliveryOrder", "OrderCenter_WaitDeliveryOrder", "closeCurrent", "/OrderCenter/WaitDeliveryOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }


        /// <summary>
        /// 待发货订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitDeliveryOrder()
        {
            #region 待发货订单视图
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitDelivery, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion
        }

        #endregion

        #region 订单签收

        /// <summary>
        /// 销售订单签收视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderSign()
        {
            return View();
        }

        /// <summary>
        /// 订单签收操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderSign()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.Successed, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] =  GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("签收异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitSignOrder", "OrderCenter_WaitSignOrder", "closeCurrent", "/OrderCenter/WaitSignOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待签收订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitSignOrder()
        {
            #region 待签收订单视图
            InitPagerForm();

            InitSalesorderQueryCollection();
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitSign, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion

        }

        #endregion

        #region 订单回收

        /// <summary>
        /// 销售订单回收视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderRecover()
        {
            return View();
        }

        /// <summary>
        /// 订单回收。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderRecover()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.Successed, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("回收异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);

                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitRecoverOrder", "OrderCenter_WaitRecoverOrder", "closeCurrent", "/OrderCenter/WaitRecoverOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待回收订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitRecoverOrder()
        {
            #region 待签收订单视图
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitRecover, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion
        }

        #endregion

        #region 订单退款

        /// <summary>
        /// 销售订单退款视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderRefund()
        {
            return View();
        }

        /// <summary>
        /// 订单退款操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderRefund()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitRefund, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("退款异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitRefundOrder", "OrderCenter_WaitRefundOrder", "closeCurrent", "/OrderCenter/WaitRefundOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待撤消订单（退款）视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitRefundOrder()
        {
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitRefund, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }

        #endregion

        #region 订单退货

        /// <summary>
        /// 销售订单退货视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderReturn()
        {
            return View();
        }

        /// <summary>
        /// 订单退货操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderReturn()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitReturns, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    if (string.IsNullOrEmpty(GetFormData("exceptionType")))
                    {
                        return FailedJson("请选择操作异常原因");
                    }
                    changeData.ChangeInfo["opTypeId"] = GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =  CustomDataInfoService.Instance.GetCustomDataDomainModelByName("退货异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }



        /// <summary>
        /// 待退货订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitReturnsOrder()
        {
            #region 待发货订单视图
            InitPagerForm();

            InitSalesorderQueryCollection();

            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitReturns, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
            #endregion
        }


        #endregion

        #region 更新订单

        /// <summary>
        /// 更新订单信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult UpdateSalesorderInfo()
        {
            string orderId = GetQueryString("sid");
            ViewBag.Salesorder = SalesOrderInfoService.Instance.GetSalesorderDomainModelById(orderId, false);
            ViewBag.ShoppingCartInfo = SalesOrderInfoService.Instance.GetShoppingCartDomainModelFromSalesorderDomainModel(ViewBag.Salesorder);

            return View();
        }

        [AuthorizeFlag]
        public ActionResult CommunicationOpenAccountInfo()
        {
            InitShoppingCartInfo();

            return View("_CommunicationOpenAccountInfo");
        }

        protected void InitShoppingCartInfo()
        {
            ViewBag.ProductListString = (GetQueryString("pl") == null) ? null : GetQueryString("pl");
            string removeProductId = (GetQueryString("rmid") == null) ? null : GetQueryString("rmid");
            string openAccountInfoString = (GetQueryString("accinfo") == null) ? null : GetQueryString("accinfo");
            string ordertype = (GetQueryString("tid") == null) ? null : GetQueryString("tid");
            string cityid = (GetQueryString("city") == null) ? null : GetQueryString("city");
            string paytype = GetQueryString("paytype");

            string customerId = (GetQueryString("cid") == null) ? null : GetQueryString("cid");
            string creditCardIdString = (GetQueryString("creditid") == null) ? null : GetQueryString("creditid");
            string deliveryId = (GetQueryString("delid") == null) ? null : GetQueryString("delid");
            string remark = GetFormData("remark");
            string orderSource = GetQueryString("os");

            ViewBag.ShoppingCartInfo = SalesOrderInfoService.Instance.GetShoppingCartFromProductListString(orderSource, ordertype, customerId, cityid, paytype, creditCardIdString, deliveryId, remark, ViewBag.ProductListString, removeProductId, openAccountInfoString, null, null);
        }

        [AuthorizeFlag]
        public ActionResult GetProductSaleGuideInfo()
        {
            return View("_ProductSalesGuideInfo");
        }

        [AuthorizeFlag]
        public ActionResult GetPhoneSaleShoppingCartInfo()
        {
            InitShoppingCartInfo();

            return View("_ShoppingCartInfo");
        }


        [AuthorizeFlag]
        public ActionResult PhoneSaleOrderProductSelector()
        {
            ViewBag.ItemPrice = string.IsNullOrEmpty(GetQueryString("itp")) ? GetFormData("itemPrice") : GetQueryString("itp");
            ViewBag.SaleCity = string.IsNullOrEmpty(GetQueryString("city")) ? GetFormData("saleCityId") : GetQueryString("city");

            InitPagerForm();
            int total = 0;

            if (GetQueryString("pgid") == "salePackage")
            {
                ViewBag.PackageName = GetFormData("categoryName");

                ViewBag.SalePackageList = SalesPackageInfoService.Instance.GetSalePackageList(ViewBag.SaleCity, ViewBag.PackageName, true, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
                ViewBag.SalePackageTotal = total;
                return View("_SalePackageSelector");
            }

            if (ViewBag.ItemPrice == "1")
            {
                ViewBag.ProductGroupName = string.IsNullOrEmpty(GetQueryString("pgid")) ? GetFormData("groupId") : GetQueryString("pgid");
                ViewBag.CategoryName = GetFormData("categoryName");

                ViewBag.ProductCategoryList = ProductCategoryInfoService.Instance.GetProductCategoryListFromDatabase(ViewBag.CategoryName, ViewBag.ProductGroupName, ViewBag.SaleCity, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
                ViewBag.ProductCategoryTotal = total;

                return View("_ProductCategorySelector");
            }
            else
            {
                ViewBag.ProductCategoryId = string.IsNullOrEmpty(GetQueryString("pgid")) ? GetFormData("productCategoryId") : GetQueryString("pgid");

                #region 搜索条件（销售城市）

                CustomDataDomainModel SaleCity = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);

                Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
                QueryItemDomainModel queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "销售城市";
                queryItem.Operation = "equal";
                if (string.IsNullOrEmpty(ViewBag.SaleCity) == false && SaleCity.ValueList.ContainsKey(ViewBag.SaleCity))
                {
                    queryItem.SearchValue = SaleCity.ValueList[ViewBag.SaleCity].DataValue;
                }
                else
                {
                    queryItem.SearchValue = "";
                }

                queryCollection.Add(ProductCategoryAttributesService.Instance.GetProductCategoryAttributeByName(ViewBag.ProductCategoryId, "销售城市").CategoryAttributeId, queryItem);

                #endregion

                #region 搜索条件：产品名称

                ViewBag.CategoryName = GetFormData("categoryName");
                if (string.IsNullOrEmpty(ViewBag.CategoryName) == false)
                {
                     queryItem = new QueryItemDomainModel();
                    queryItem.FieldType = "产品名称";
                    queryItem.Operation = "contain";

                    queryItem.SearchValue = ViewBag.CategoryName;

                    queryCollection.Add(ProductCategoryAttributesService.Instance.GetProductCategoryAttributeByName(ViewBag.ProductCategoryId, "产品名称").CategoryAttributeId, queryItem);
                }


                #endregion

                #region 搜索条件：销售状态

                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "销售状态";
                queryItem.Operation = "equal";

                queryItem.SearchValue = "已开放";
                queryCollection.Add(ProductCategoryAttributesService.Instance.GetProductCategoryAttributeByName(ViewBag.ProductCategoryId, "销售状态").CategoryAttributeId, queryItem);

                #endregion

                ViewBag.ProductItemList = ProductInfoService.Instance.GetProductList(ViewBag.ProductCategoryId, queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
                ViewBag.ProductCategoryTotal = total;

                return View("_ProductItemSelector");
            }
        }

        [AuthorizeFlag]
        public ActionResult CustomerCreditCardSelector()
        {
            InitShoppingCartInfo();

            return View("_CustomerCreditCardSelector");
        }


        [AuthorizeFlag]
        public ActionResult CustomerDeliverySelector()
        {
            InitShoppingCartInfo();

            return View("_CustomerDeliverySelector");
        }


        #endregion

        #region 订单销户

        /// <summary>
        /// 订单销户视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderCancelOpening()
        {
            return View();
        }

        /// <summary>
        /// 订单销户操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSalesOrderCancelOpening()
        {
            string message = "操作失败，请与管理员联系";
            string page = GetFormData("page");
            string chargeType = GetFormData("opType");
            string orderId = GetFormData("orderId");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.WaitCancelOpening, false);

            switch (chargeType)
            {
                case "0":
                    changeData.IsSuccessed = true;
                    break;

                case "1":
                    changeData.IsSuccessed = false;
                    string opExceptionDesc = GetFormData("opExceptionDesc");
                    if (string.IsNullOrEmpty(opExceptionDesc))
                    {
                        return FailedJson("请填写异常原因");
                    }
                    string exceptionType = GetFormData("exceptionType");
                    if (string.IsNullOrEmpty(exceptionType))
                    {
                        return FailedJson("请选择操作异常原因。");
                    }
                    changeData.ChangeInfo["opTypeId"] =  GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =   GetFormData("exceptionType");
                    changeData.ChangeInfo["opType"] =   CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销户异常类型", false).GetCustomDataValueByValueId(GetFormData("exceptionType"), "异常");

                    changeData.ChangeInfo["opDesc"] = (opExceptionDesc == null) ? "" : opExceptionDesc;

                    break;

                default:
                    break;
            }


            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitCancelOpeningOrder", "OrderCenter_WaitCancelOpeningOrder", "closeCurrent", "/OrderCenter/WaitCancelOpeningOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        /// <summary>
        /// 待销户订单视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult WaitCancelOpeningOrder()
        {
            InitPagerForm();
            InitSalesorderQueryCollection();
            int total = 0;
            ViewBag.SalesorderIdList = SalesOrderInfoService.Instance.GetSalesorderIdList(null, false, SalesOrderStatus.WaitCancelOpening, ViewBag.exception, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalesorderTotal = total;

            return View();
        }

        #endregion

        #region 订单撤消

        /// <summary>
        /// 订单撤消视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalesOrderCancel()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoCancelSalesOrder()
        {
            string message = "操作失败，请与管理员联系";
            string orderId = GetFormData("orderId");
            string opTypeId = GetFormData("ddlOpType");
            string opDesc = GetFormData("opDesc");
            string page = GetFormData("page");

            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderId, SalesOrderStatus.Cancel, true);
            if (string.IsNullOrEmpty(opTypeId))
            {
                return FailedJson("请选择操作异常原因");
            }
            changeData.ChangeInfo["opTypeId"] = opTypeId;
            changeData.ChangeInfo["opDesc"] = opDesc;
            changeData.ChangeInfo["opType"] = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("订单撤消原因", false).GetCustomDataValueByValueId(opTypeId, "");
            
            if(SalesOrderInfoService.Instance.UpdateSalesorder(orderId,changeData,out message))
            {
                 switch (page)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    case "orderlist":
                        return SuccessedJson(message, "OrderCenter_WaitCancelOpeningOrder", "OrderCenter_WaitCancelOpeningOrder", "closeCurrent", "/OrderCenter/WaitCancelOpeningOrder");
                    default:
                        break;
                }
            }
            return FailedJson(message);
        }

        #endregion


        /// <summary>
        /// 我的销售订单
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult MySalesOrder()
        {
            return View();

 
        }


        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewSalesorderInfo()
        {
            string message = "操作失败，请与管理员联系";

            ViewBag.ProductListString = GetFormData("cartProductList");
            string removeProductId = "";
            string openAccountInfoString = GetFormData("hidOpenAccountInfo");
            string ordertype = GetFormData("orderType");
            string cityid = GetFormData("city");
            string followTime = GetFormData("followTime");
            string followRemark = GetFormData("followRemark");

            string customerId = GetFormData("hidCustomerId");
            string creditCardIdString = GetFormData("hidSelectCreditCardId");
            string orderPayType = GetFormData("ddlPayType");
            string deliveryId = GetFormData("hidOrderDeliveryId");
            string remark = GetFormData("orderRemark");
            bool isFollow = GetQueryString("type") == "follow";
            string page = GetQueryString("page");
            string orderSource = GetFormData("orderSource");
            string workorderId = GetQueryString("oid");

            ProductShoppingCartDomainModel cartInfo = SalesOrderInfoService.Instance.GetShoppingCartFromProductListString(orderSource, ordertype, customerId, cityid, orderPayType, creditCardIdString, deliveryId, remark, ViewBag.ProductListString, removeProductId, openAccountInfoString, followTime, followRemark);

            if (SalesOrderInfoService.Instance.CreateNewSalesorder(cartInfo,workorderId, isFollow, out message))
            {
                switch (page)
                {
                    case "customerdetail":
                        return SuccessedJson(message, customerId, customerId, "closeCurrent", "/CallCenter/customerinfo?cid=" + customerId);
                 
                    case "workorderdetail":
                        return SuccessedJson(message, workorderId, workorderId, "closeCurrent", "/WorkOrderCenter/workorderdetail?oid=" + workorderId);
                 
                    default:
                        break;
                }
            }

            return FailedJson(message);
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateSalesorderInfo()
        {
            string message = "操作失败，请与管理员联系";

            ViewBag.ProductListString = GetFormData("cartProductList");
            string removeProductId = "";
            string openAccountInfoString = GetFormData("hidOpenAccountInfo");
            string ordertype = GetFormData("orderType");
            string cityid = GetFormData("city");

            string customerId = GetFormData("hidCustomerId");
            string creditCardIdString = GetFormData("hidSelectCreditCardId");
            string orderPayType = GetFormData("ddlPayType");
            string deliveryId = GetFormData("hidOrderDeliveryId");
            string remark = GetFormData("orderRemark");
            bool isFollow = GetQueryString("type") == "follow";
            string page = GetQueryString("page");
            string salesOrderId = GetFormData("hidSalesorderId");
            string orderSource = GetFormData("orderSource");
            string followTime = GetFormData("followTime");
            string followRemark = GetFormData("followRemark");

            ProductShoppingCartDomainModel cartInfo = SalesOrderInfoService.Instance.GetShoppingCartFromProductListString(orderSource, ordertype, customerId, cityid, orderPayType, creditCardIdString, deliveryId, remark, ViewBag.ProductListString, removeProductId, openAccountInfoString, followTime, followRemark);

            if (SalesOrderInfoService.Instance.UpdateSalesorder(salesOrderId, cartInfo, out message))
            {
                switch (page)
                {
                    case "customerdetail":
                        return SuccessedJson(message, customerId, customerId, "closeCurrent", "/CallCenter/customerinfo?cid=" + customerId);
                    case "orderdetail":
                        return SuccessedJson(message, salesOrderId, salesOrderId, "closeCurrent", "/OrderCenter/SalesOrderDetail?sid=" + salesOrderId);
                    default:
                        break;
                }
            }

            return FailedJson(message);
        }


        /// <summary>
        /// 提交订单。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoSubmitSalesorder()
        {
            string message = "操作失败，请与管理员联系";

            string orderId = GetQueryString("sid");
            string orderType = GetQueryString("otype");
            string pagename = GetQueryString("page");
            SalesorderChangeInfo changeData = new SalesorderChangeInfo(orderType, SalesOrderStatus.WaitCharge, true);

            if (SalesOrderInfoService.Instance.UpdateSalesorder(orderId, changeData, out message))
            {
                switch (pagename)
                {
                    case "orderdetail":
                        return SuccessedJson(message, orderId, orderId, "closedCurrent", "/OrderCenter/SalesOrderDetail?sid=" + orderId);
                    default:
                        break;
                }
            }

            return FailedJson(message);
        }

        protected void InitSalesorderQueryCollection()
        {
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();
            QueryItemDomainModel queryItem = null;
            string orderstatus = GetFormData("exception");
            string salesFromId = GetFormData("salesFrom");
            if (salesFromId != "All" && salesFromId != null)
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "order_source";
                queryItem.SearchValue = salesFromId;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

           

            string product_name = GetFormData("product_name");
            if (!string.IsNullOrEmpty(product_name))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "product_name";
                queryItem.SearchValue = product_name;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            
            // 订单编号
            string orderCode = GetFormData("orderCode");
            if (!string.IsNullOrEmpty(orderCode))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_code";
                queryItem.SearchValue = orderCode;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string incomePhoneNumber = GetFormData("incomePhoneNumber");
            if (!string.IsNullOrEmpty(incomePhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_subsidiary_phonenumber";
                queryItem.SearchValue = incomePhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string selectedPhoneNumber = GetFormData("selectPhoneNumber");
            if (!string.IsNullOrEmpty(selectedPhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_main_phonenumber";
                queryItem.SearchValue = selectedPhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户名称
            string customerName = GetFormData("customerName");
            if (!string.IsNullOrEmpty(customerName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.customer_name";
                queryItem.SearchValue = customerName;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户银卡号码
            string pay_card_number = GetFormData("pay_card_number");
            if (!string.IsNullOrEmpty(pay_card_number))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "pay_card_number";
                queryItem.SearchValue = pay_card_number;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            string payType = GetFormData("payType");
            if (!string.IsNullOrEmpty(payType))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.pay_type";
                queryItem.SearchValue = payType;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            
            // 订单创建人
            string createdUserName = GetFormData("createdUserName");
            if (!string.IsNullOrEmpty(createdUserName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "created_user_info.work_id";
                queryItem.SearchValue ="WORKID_"+createdUserName;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            #region 创建时间
            string createdBeginTime = GetFormData("createdBeginTime");
            string createdEndTime = GetFormData("createdEndTime");

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            } 
            #endregion


            #region 跟进预约时间
            string followBeginTime = GetFormData("followBeginTime");
            string followEndTime = GetFormData("followEndTime");

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == true && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.SearchValue = followEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(followBeginTime) == false && string.IsNullOrEmpty(followEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.follow_time";
                queryItem.BeginTime = Convert.ToDateTime(followBeginTime);
                queryItem.EndTime = Convert.ToDateTime(followEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            } 
            #endregion
            ViewBag.queryCollection = queryCollection;
            ViewBag.Paytype = payType;
            ViewBag.incomePhoneNumber = incomePhoneNumber;
            ViewBag.selectedPhoneNumber = selectedPhoneNumber;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;

            ViewBag.followBeginTime = followBeginTime;
            ViewBag.followEndTime = followEndTime;
            ViewBag.exception = orderstatus;
        }

        #region EXCEL导出信息
        /// <summary>
        /// EXCEL导出查询条件
        /// </summary>
        protected void ExcelSalesorderQueryCollection()
        {
            #region 获取参数
            Dictionary<string, QueryItemDomainModel> queryCollection = new Dictionary<string, QueryItemDomainModel>();


            QueryItemDomainModel queryItem = null;



            // 订单编号
            string orderCode = Request["orderCode"];
            if (!string.IsNullOrEmpty(orderCode))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_code";
                queryItem.SearchValue = orderCode;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string incomePhoneNumber = Request["incomePhoneNumber"];
            if (!string.IsNullOrEmpty(incomePhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_subsidiary_phonenumber";
                queryItem.SearchValue = incomePhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string selectedPhoneNumber = Request["selectPhoneNumber"];
            if (!string.IsNullOrEmpty(selectedPhoneNumber))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_communiationpackage_info.bind_main_phonenumber";
                queryItem.SearchValue = selectedPhoneNumber;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户名称
            string customerName = Request["customerName"];
            if (!string.IsNullOrEmpty(customerName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "customer_basic_info.customer_name";
                queryItem.SearchValue = customerName;
                queryItem.Operation = "contain";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 客户银卡号码
            string pay_card_number = Request["pay_card_number"];
            if (!string.IsNullOrEmpty(pay_card_number))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "pay_card_number";
                queryItem.SearchValue = pay_card_number;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }
            string payType = Request["payType"];
            if (!string.IsNullOrEmpty(payType))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.pay_type";
                queryItem.SearchValue = payType;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            // 订单创建人
            string createdUserName = Request["createdUserName"];
            if (!string.IsNullOrEmpty(createdUserName))
            {
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "created_user_info.work_id";
                queryItem.SearchValue = "WORKID_" + createdUserName;
                queryItem.Operation = "equal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            string createdBeginTime = Request["createdBeginTime"];
            string createdEndTime = Request["createdEndTime"];

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == true)
            {
                // 如果开始时间不为空，结束时间为空，将查询订单创建时间大于等于createdBeginTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdBeginTime;
                queryItem.Operation = "greaterequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == true && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间为空，结束时间不为空，将查询订单创建时间小于等于createdEndTime的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.SearchValue = createdEndTime;
                queryItem.Operation = "lessequal";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            if (string.IsNullOrEmpty(createdBeginTime) == false && string.IsNullOrEmpty(createdEndTime) == false)
            {
                // 如果开始时间不为空，结束时间不为空，将查询订单创建时间在createdBeginTime与createdEndTime之间的记录。
                queryItem = new QueryItemDomainModel();
                queryItem.FieldType = "salesorder_basic_info.created_on";
                queryItem.BeginTime = Convert.ToDateTime(createdBeginTime);
                queryItem.EndTime = Convert.ToDateTime(createdEndTime);
                queryItem.Operation = "between";

                queryCollection[queryItem.FieldType] = queryItem;
            }

            ViewBag.queryCollection = queryCollection;
            ViewBag.Paytype = payType;
            ViewBag.incomePhoneNumber = incomePhoneNumber;
            ViewBag.selectedPhoneNumber = selectedPhoneNumber;
            ViewBag.createdBeginTime = createdBeginTime;
            ViewBag.createdEndTime = createdEndTime;


            #endregion
        }
        
         /// <summary>
        /// 导出所有订单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportOrderManager()
        {
            ExcelSalesorderQueryCollection();
          
            #region 类型判断
            SalesOrderStatus orderStatus = SalesOrderStatus.WaitFollow;
            switch (Request["orderStatus"])
            {

                case "1":
                    orderStatus = SalesOrderStatus.WaitFollow;
                    break;
                case "2":
                    orderStatus = SalesOrderStatus.WaitCharge;
                    break;
                case "3":
                    orderStatus = SalesOrderStatus.WaitCheck;
                    break;
                case "4":
                    orderStatus = SalesOrderStatus.WaitApproval;
                    break;
                case "5":
                    orderStatus = SalesOrderStatus.WaitOpening;
                    break;
                case "6":
                    orderStatus = SalesOrderStatus.WaitStocking;
                    break;
                case "7":
                    orderStatus = SalesOrderStatus.WaitDelivery;
                    break;
                case "8":
                    orderStatus = SalesOrderStatus.WaitSign;
                    break;
                case "9":
                    orderStatus = SalesOrderStatus.WaitRecover;
                    break;
                case "10":
                    orderStatus = SalesOrderStatus.Successed;
                    break;
                case "11":
                    orderStatus = SalesOrderStatus.Exception;
                    break;
                case "12":
                    orderStatus = SalesOrderStatus.WaitRefund;
                    break;
                case "13":
                    orderStatus = SalesOrderStatus.WaitReturns;
                    break;
                case "14":
                    orderStatus = SalesOrderStatus.Cancel;
                    break;
                case "15":
                    orderStatus = SalesOrderStatus.WaitCancelOpening;
                    break;

                default:
                    orderStatus = SalesOrderStatus.All;
                    break;
            } 
            #endregion
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, orderStatus, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();
            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("订单时间", "created_on"));
            cols.Add(new Pair("审批时间", "approval_time"));
            cols.Add(new Pair("订单来源", "order_source"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("绑定主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("绑定副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("扣款时间", "charge_time"));
            cols.Add(new Pair("扣款人", "charge_user_id"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("发货时间", "delivery_time"));
            cols.Add(new Pair("签收时间", "sign_time"));
            cols.Add(new Pair("客户建档时间", "customer_create_on"));
            
            // "product_name_list" 以\r\n进行分隔。
            //string product_name_list = "";
            //string[] productList = product_name_list.Split("\r");
            //if (productList != null && productList.Length > 0)
            //{
            //    for (int i = 0; i < productList.Length; i++)
            //    {
            //        cols.Add(new Pair("产品名称" + i.ToString(), "productItem" + i.ToString()));
            //        tb.Columns.Add("productItem" + i.ToString());
            //    }
            //}


            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);

            Response.End();

            return View();
        }

 
        /// <summary>
        /// 导出待退款信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportRefundOrder()
        {
            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitRefund, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();
            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("订单时间", "created_on"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("开户行", "pay_card_bank_id"));
            cols.Add(new Pair("信用卡卡号", "pay_card_number"));
            cols.Add(new Pair("有效期", "pay_card_period"));
            cols.Add(new Pair("安全码", "pay_card_securitycode"));
            cols.Add(new Pair("证件类型", "data_value"));
            cols.Add(new Pair("证件号码", "idcard_number"));
            cols.Add(new Pair("扣款金额", "pay_price"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("扣款人", "charge_user_id"));

            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);

            Response.End();

            return View();
        }

 
        /// <summary>
        /// 导出待回收信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportRecoverOrder()
        {
            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitRecover, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("发货时间", "delivery_time"));
            cols.Add(new Pair("签收时间", "sign_time"));
            cols.Add(new Pair("机主姓名", "owner_customer_name"));           
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("绑定主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("绑定副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("配送公司", "delivery_company_id"));
            cols.Add(new Pair("配送单号", "delivery_order_code"));
            cols.Add(new Pair("收货人电话", "delivery_receive_phonenumber"));
            cols.Add(new Pair("创建时间", "created_on"));
            cols.Add(new Pair("配送地址", "delivery_address"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("订单金额", "pay_price"));

            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);

            Response.End();

            return View();
        }

        /// <summary>
        /// 导出待签收信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportSignOrder()
        {
            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitSign, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("审批时间", "approval_time"));
            cols.Add(new Pair("发货时间", "delivery_time"));
            cols.Add(new Pair("机主姓名", "owner_customer_name"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("绑定主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("绑定副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("配送公司", "delivery_company_id"));
            cols.Add(new Pair("配送单号", "delivery_order_code"));
            cols.Add(new Pair("收货人电话", "delivery_receive_phonenumber"));
            cols.Add(new Pair("创建时间", "created_on"));
            cols.Add(new Pair("配送地址", "delivery_address"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("订单金额", "pay_price"));

            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);

            Response.End();

            return View();
        }



        /// <summary>
        /// 导出待审批信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportApprovalOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitApproval, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("扣款时间", "charge_time"));
            cols.Add(new Pair("扣款人", "charge_user_id"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("性别", "sex"));
            cols.Add(new Pair("手机号码", "mobile_phone"));
            cols.Add(new Pair("办公电话", "home_phone"));
            cols.Add(new Pair("其他号码", "other_phone"));
            cols.Add(new Pair("扣款金额", "pay_price"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);
            Response.End();

            return View();
        }





        /// <summary>
        /// 导出待退货信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportReturnsOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitReturns, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("订单时间", "created_on"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("开户行", "pay_card_bank_id"));
            cols.Add(new Pair("信用卡卡号", "pay_card_number"));
            cols.Add(new Pair("有效期", "pay_card_period"));
            cols.Add(new Pair("安全码", "pay_card_securitycode"));
            cols.Add(new Pair("证件类型", "data_value"));
            cols.Add(new Pair("证件号码", "idcard_number"));
            cols.Add(new Pair("扣款金额", "pay_price"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("扣款人", "charge_user_id"));
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);
            Response.End();

            return View();
        }

        /// <summary>
        /// 导出待扣款信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportChargeOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitCharge, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("订单时间", "created_on"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("开户行", "pay_card_bank_id"));
            cols.Add(new Pair("信用卡卡号", "pay_card_number"));
            cols.Add(new Pair("有效期", "pay_card_period"));
            cols.Add(new Pair("安全码", "pay_card_securitycode"));
            cols.Add(new Pair("证件类型", "data_value"));
            cols.Add(new Pair("证件号码", "idcard_number"));
            cols.Add(new Pair("扣款金额", "pay_price"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("订单备注", "remark"));
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);
            Response.End();

            return View();
        }

        /// <summary>
        /// 导出待备货信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportStockingOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitStocking, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("审批时间", "approval_time"));
            cols.Add(new Pair("机主姓名", "owner_customer_name"));
            cols.Add(new Pair("收货人电话", "delivery_receive_phonenumber"));
            cols.Add(new Pair("合约主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("合约副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("证件号码", "idcard_number"));
            cols.Add(new Pair("托收银行", "collection_bank_id"));
            cols.Add(new Pair("托收户名", "collection_customer_name"));
            cols.Add(new Pair("托收银行帐号", "collection_card_number"));
            cols.Add(new Pair("配送地址", "delivery_address"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("是否要发票", "need_invoice"));
            cols.Add(new Pair("订单备注", "remark"));
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);
            Response.End();

            return View();
        }
        /// <summary>
        /// 导出待开户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportOpeningOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitOpening, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("开户时间", "opening_time"));
            cols.Add(new Pair("机主姓名", "owner_customer_name"));
            cols.Add(new Pair("收货人电话", "delivery_receive_phonenumber"));
            cols.Add(new Pair("合约主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("合约副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("证件号码", "idcard_number"));
            cols.Add(new Pair("托收银行", "collection_bank_id"));
            cols.Add(new Pair("托收户名", "collection_customer_name"));
            cols.Add(new Pair("托收银行帐号", "collection_card_number"));
            cols.Add(new Pair("配送地址", "delivery_address"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("是否要发票", "need_invoice"));
            cols.Add(new Pair("订单备注", "remark"));        
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"), cols);
            Response.End();

            return View();
        }

        /// <summary>
        /// 导出待质检信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ExportCheckOrder()
        {

            ExcelSalesorderQueryCollection();
            DataTable tb = SalesOrderInfoService.Instance.GetDataTable(false, SalesOrderStatus.WaitCheck, ViewBag.incomePhoneNumber, ViewBag.selectedPhoneNumber, ViewBag.queryCollection);
            //HelperClass.DataTableToIList<Tab
            IList<Pair> cols = new List<Pair>();

            cols.Add(new Pair("订单编号", "salesorder_code"));
            cols.Add(new Pair("客户姓名", "customer_name"));
            cols.Add(new Pair("创建人", "cn_name"));
            cols.Add(new Pair("创建时间", "created_on"));
            cols.Add(new Pair("证件类型", "data_value"));
            cols.Add(new Pair("扣款金额", "pay_price"));
            cols.Add(new Pair("支付方式", "pay_type"));
            cols.Add(new Pair("审批时间", "approval_time"));
            cols.Add(new Pair("发货时间", "delivery_time"));
            cols.Add(new Pair("性别", "sex"));
            cols.Add(new Pair("产品名称", "category_name"));
            cols.Add(new Pair("合约主号码", "bind_main_phonenumber"));
            cols.Add(new Pair("合约副号码", "bind_subsidiary_phonenumber"));
            cols.Add(new Pair("扣款时间", "charge_time"));
            cols.Add(new Pair("扣款人", "charge_user_id"));
            cols.Add(new Pair("收货人", "delivery_receive_customer_name"));
            cols.Add(new Pair("收货人电话", "delivery_receive_phonenumber"));
            cols.Add(new Pair("配送地址", "delivery_address"));
            cols.Add(new Pair("是否要发票", "need_invoice"));
            cols.Add(new Pair("订单备注", "remark"));
            cols.Add(new Pair("签收时间", "sign_time"));
            cols.Add(new Pair("资料回收时间", "recover_time"));
            ExportToExcel(tb, Server.UrlEncode(DateTime.Now.ToString("yyyyMMddHHmmssfff")+".xls"), cols);
            Response.End();

            return View();
        }




        //public FileContentResult ddd()
        //{
        //    return "";
        //}

        /// <summary>
        /// 将DataTable数据生成Excel文件
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="filename">Excel 文件名</param>
        /// <param name="cols">数据与 Excel 列对应关系</param>
        /// <returns>是否导出成功</returns>
        public static bool ExportToExcel(DataTable data, string filename, IList<Pair> cols)
        {
            bool result = false;
            try
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];
                worksheet.AutoFitColumns();
                Cells cells = worksheet.Cells;

                int colidx = 0;
                int rowidx = 0;

                // 设置 Excel 工作薄的列名
                foreach (Pair col in cols)
                {
                    cells[rowidx, colidx].Style.Font.IsBold = true;
                    cells[rowidx, colidx].Style.HorizontalAlignment = TextAlignmentType.Center;
                    cells[rowidx, colidx++].PutValue(col.First);
                }

                // 将数据按顺序插入各 cell
                foreach (DataRow row in data.Rows)
                {
                    colidx = 0;
                    rowidx++;
                    foreach (Pair col in cols)
                    {
                       
                        cells[rowidx, colidx++].PutValue(row[col.Second.ToString()]);
                    }
                }

                workbook.Save(filename, FileFormatType.Default, SaveType.OpenInExcel, System.Web.HttpContext.Current.Response);
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.ToString());
            }

            return result;
        } 
        #endregion

        /// <summary>
        /// 获取订单数量JSON对象。
        /// </summary>
        /// <returns></returns>
        //[AuthorizeFlag]
        public JsonResult SalesOrderTotalJson()
        {

            SalesOrderTotal totalInfo = SalesOrderInfoService.Instance.GetSalesorderOreateTotal();
            return Json(new { SalesOrderTotal = totalInfo });
        }


    }
}
