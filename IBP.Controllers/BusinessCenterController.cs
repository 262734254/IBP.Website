using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IBP.Services;
using IBP.Models;
using System.Data;

namespace IBP.Controllers
{
    public class BusinessCenterController : BaseController
    {
        /// <summary>
        /// 广播管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult BroadcastManager()
        {
            DataSet ds = ProductInfoService.Instance.ImportProductCategoryFromExcel("C:\\projects\\InssinBusinessPlatform\\trunk\\06-程序源码\\01-源码\\IBP.Website\\uploads\\templates\\营销计划导入表.xls");

            string importLogs = null, message = null;
            SalesPackageInfoService.Instance.ImportSalePackageFromExcel(ds, out importLogs, out message);

            return View();
        }

        /// <summary>
        /// 号码管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult NumberManager()
        {
            return View();
        }

        /// <summary>
        /// 订单管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult OrderManager()
        {
            return View();
        }

        /// <summary>
        /// 项目管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult ProjectManager()
        {
            return View();
        }


        #region 销售项目管理 

        /// <summary>
        /// 销售项目管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalePackageManager()
        {
            GetSalePackageInfo();
            return View();
        }

        /// <summary>
        /// 营销项目产品选择面板。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult SalePackageProductSelector()
        {
            return View();
        }

        protected void GetSalePackageInfo()
        {
            ViewBag.CityId = GetFormData("cityId");
            ViewBag.PackageName = GetFormData("packageName");

            InitPagerForm();

            int total = 0;
            ViewBag.SalePackageList = SalesPackageInfoService.Instance.GetSalePackageList(ViewBag.CityId, ViewBag.PackageName, false, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection, out total);
            ViewBag.SalePackageTotal = total;
        }

        /// <summary>
        /// 添加销售项目视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddSalePackageInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuthorizeFlag]
        public JsonResult DoAddSalePackageInfo()
        {
            SalesPackageInfoModel package = new SalesPackageInfoModel();
            CustomDataDomainModel SaleCityList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);
            this.ValidateRequest = false;
            package.BeginTime = Convert.ToDateTime(GetFormData("beginTime"));
            package.EndTime = Convert.ToDateTime(GetFormData("endTime"));
            package.Location = GetFormData("location");
            package.MonthKeepPrice = Convert.ToDecimal(GetFormData("monthKeep"));
            package.MonthReturnPrice = Convert.ToDecimal(GetFormData("monthReturns"));
            package.PackageName = GetFormData("packageName");
            package.PriceTotal = Convert.ToDecimal(GetFormData("totalPrice"));
            package.Remark = GetFormData("remark");
            package.ReturnMonths = Convert.ToInt32(GetFormData("returnMonths"));
            package.SalePrice = Convert.ToInt32(GetFormData("salePrice"));
            package.SalesCityId = GetFormData("city");
            package.SalesCityName = SaleCityList.ValueList[package.SalesCityId].DataValue;
            package.StagePrice = Convert.ToDecimal(GetFormData("stagePrice"));
            package.Stages = Convert.ToInt32(GetFormData("stages"));
            package.StoredPrice = Convert.ToDecimal(GetFormData("storedPrice"));
            package.Description = Request.Form["description"];
            package.SalesGuide = Request.Form["salesGuide"];
            this.ValidateRequest = true;

            List<string> productCategoryIdList = Request.Form.GetValues("chkProCat").ToList();

            string message = "失败失败，请与管理员联系";

            if (SalesPackageInfoService.Instance.CreateSalePackageInfo(package, productCategoryIdList, out message))
            {
                return SuccessedJson(message, "BusinessCenter_SalePackageManager", "BusinessCenter_SalePackageManager", "closeCurrent", "/businesscenter/salepackagemanager");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateSalePackageInfo()
        {
            SalesPackageInfoModel package = new SalesPackageInfoModel();
            CustomDataDomainModel SaleCityList = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("销售城市", false);
            package.SalesPackageId = GetFormData("packageId");
            package.BeginTime = Convert.ToDateTime(GetFormData("beginTime"));
            package.EndTime = Convert.ToDateTime(GetFormData("endTime"));
            package.Location = GetFormData("location");
            package.MonthKeepPrice = Convert.ToDecimal(GetFormData("monthKeep"));
            package.MonthReturnPrice = Convert.ToDecimal(GetFormData("monthReturns"));
            package.PackageName = GetFormData("packageName");
            package.PriceTotal = Convert.ToDecimal(GetFormData("totalPrice"));
            package.Remark = GetFormData("remark");
            package.ReturnMonths = Convert.ToInt32(GetFormData("returnMonths"));
            package.SalePrice = Convert.ToDecimal(GetFormData("salePrice"));
            package.SalesCityId = GetFormData("city");
            package.SalesCityName = SaleCityList.ValueList[package.SalesCityId].DataValue;

            package.StagePrice = Convert.ToDecimal(GetFormData("stagePrice"));
            package.Stages = Convert.ToInt32(GetFormData("stages"));
            package.StoredPrice = Convert.ToDecimal(GetFormData("storedPrice"));

            if (Request.Form["chkProCat"] == null)
            {
                return FailedJson("操作失败，请选择产品包包含的产品组成。");
            }

            List<string> productCategoryIdList = Request.Form.GetValues("chkProCat").ToList();

            string message = "操作失败，请与管理员联系";

            if (SalesPackageInfoService.Instance.UpdateSalePackageInfo(package, productCategoryIdList, out message))
            {
                return SuccessedJson(message, "BusinessCenter_SalePackageManager", "BusinessCenter_SalePackageManager", "closeCurrent", "/businesscenter/salepackagemanager");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoBatchDeleteSalePackageInfo()
        {
            string message = "失败失败，请与管理员联系";
            List<string> idList = Request.Form.GetValues("ids").ToList();

            if (SalesPackageInfoService.Instance.BatchDeleteSalePackages(idList, out message))
            {
                return SuccessedJson(message, "BusinessCenter_SalePackageManager", "BusinessCenter_SalePackageManager", "forward", "/businesscenter/salepackagemanager");
            }
            else
            {
                return FailedJson(message);
            }
        }

        [AuthorizeFlag]
        public ActionResult EditSalePackageInfo()
        {
            return View();
        }

        #endregion

        #region 问卷管理模块

        /// <summary>
        /// 题库管理视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult QuestionLib()
        {
            return View();
        }

        /// <summary>
        /// 问卷管理视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult Examination()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult NewExamQuestion()
        {
            return View();
        }

        #endregion
    }
}
