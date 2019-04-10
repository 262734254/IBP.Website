using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace IBP.Controllers
{
    public class ReportCenterController : BaseController
    {
        /// <summary>
        /// 工单统计信息报表。
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkOrderStatisticsReport()
        {
            return View();
        }

        /// <summary>
        /// 订单管理报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderReport()
        {
            return View();
        }

        /// <summary>
        /// 业务分析报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult BusinessAnalysisReport()
        {
            return View();
        }

        /// <summary>
        /// 联系管理报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult ContactReport()
        {
            return View();
        }

        /// <summary>
        /// 客户管理报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerReport()
        {
            return View();
        }

        /// <summary>
        /// 客户转化率报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerConversionRate()
        {
            return View();
        }

        /// <summary>
        /// 工单管理报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkOrderReport()
        {
            return View();
        }

        /// <summary>
        /// 扣款报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult ChargeOrderReport()
        {
            return View();
        }

        /// <summary>
        /// 物流综合查询视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryQuery()
        {
            return View();
        }

        /// <summary>
        /// 开户报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult OpeningOrderReport()
        {
            return View();
        }

        /// <summary>
        /// 回收报表视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult RecoverOrderReport()
        {
            return View();
        }

    }
}
