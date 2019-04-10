using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using IBP.Models;
using IBP.Services;
using Framework.Utilities;

namespace IBP.Controllers
{
    public class SystemController : BaseController
    {

        #region 自定义枚举信息管理


        /// <summary>
        /// 自定义信息管理视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePassWord()
        {
            return View();
        }

        /// <summary>
        /// 修改用户密码。
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomInfo()
        {
            return View();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateUserInfo()
        {
            UserInfoModel model = new UserInfoModel();
            model.UserId = GetFormData("UserId");
            model.LoginPwd = GetFormData("login_pwd");           
            model.CtiUserPwd = GetFormData("cti_user_pwd");        
            if (UserInfoService.Instance.UpdateUserInfo(model))
            {
                return SuccessedJson("成功更新用户信息", "_DefaultHeader", "_DefaultHeader", "closeCurrent", "/System/DoUpdateUserInfo");
            }
            else
            {

                return FailedJson("操作失败，请与管理员联系");
            }





        }
        

        /// <summary>
        /// 自定义枚举值列表视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult CustomValueList()
        {
            return View();
        }

        /// <summary>
        /// 添加自定义枚举值视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddCustomDataValue()
        {
            return View();
        }

        /// <summary>
        /// 编辑自定义枚举值视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditCustomDataValue()
        {
            return View();
        }

        /// <summary>
        /// 更新自定义枚举值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoUpdateCustomValue()
        {
            CustomDataValueDomainModel model = new CustomDataValueDomainModel();

            model.ValueId = GetFormData("valueId");
            model.DataId = GetFormData("dataId");
            model.DataValue = GetFormData("dataValue");
            model.DataValueCode = GetFormData("valueCode");
            model.SortOrder = Convert.ToInt32(GetFormData("sortOrder"));
            model.Status = Convert.ToInt32(GetFormData("valueStatus"));

            if (CustomDataInfoService.Instance.UpdateCustomDataValue(model, model.DataId))
            {
                return SuccessedJson("成功更新枚举值成员", "System_CustomInfo", "System_CustomInfo", "forward", "/system/customvaluelist?cid=" + model.DataId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        /// <summary>
        /// 上移自定义枚举值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveUpCustomValue()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (CustomDataInfoService.Instance.MoveUpCustomDataValue(dataId, valueId, out message))
            {
                return SuccessedJson(message, "System_CustomInfo", "System_CustomInfo", "forward", "/system/customvaluelist?cid=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 上移自定义枚举值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoMoveDownCustomValue()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];
            string message = "";

            if (CustomDataInfoService.Instance.MoveDownCustomDataValue(dataId, valueId, out message))
            {
                return SuccessedJson(message, "System_CustomInfo", "System_CustomInfo", "forward", "/system/customvaluelist?cid=" + dataId);
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 移除自定义枚举值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoRemoveCustomValue()
        {
            string dataId = GetQueryString("id").Split('|')[0];
            string valueId = GetQueryString("id").Split('|')[1];

            if (CustomDataInfoService.Instance.DeleteCustomDataValue(dataId, valueId))
            {
                return SuccessedJson("成功更新枚举值成员", "System_CustomInfo", "System_CustomInfo", "forward", "/system/customvaluelist?cid=" + dataId);
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }


        /// <summary>
        /// 添加自定义枚举值操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoNewCustomValue()
        {
            CustomDataValueDomainModel model = new CustomDataValueDomainModel();

            model.ValueId = Guid.NewGuid().ToString();
            model.DataId = GetFormData("dataId");
            model.DataValue = GetFormData("dataValue");
            model.DataValueCode = GetFormData("valueCode");
            model.Status = Convert.ToInt32(GetFormData("valueStatus"));

            if (CustomDataInfoService.Instance.NewCustomDataValue(model, model.DataId))
            {
                return SuccessedJson("成功添加枚举值成员", "System_CustomInfo", "System_CustomInfo", "forward", "/system/custominfo?cid=" + model.DataId + "&=" + new Random().Next().ToString());
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        #endregion

        #region 银行卡信息管理

        /// <summary>
        /// 添加银行卡信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult AddBankCardInfo()
        {
            return View();
        }

        /// <summary>
        /// 编辑银行卡信息视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult EditBankCardInfo()
        {
            return View();
        }

        /// <summary>
        /// 银行卡信息管理视图。
        /// </summary>
        /// <returns></returns>
        [AuthorizeFlag]
        public ActionResult BankCardInfo()
        {
            GetBankCardInfo();
            return View();
        }

        /// <summary>
        /// 批量删除银行卡信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoBatchDeleteBankCardInfo()
        {
            string message = "操作失败，请与管理员联系";
            List<string> idList = Request.Form.GetValues("ids").ToList();

            if (BankcardTypeInfoService.Instance.BatchDeleteBankCardsById(idList, out message))
            {
                return SuccessedJson(message, "System_BankCardInfo", "System_BankCardInfo", "forward", "/system/bankcardinfo");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 删除银行卡信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoDeleteBankCardInfo()
        {
            string message = "操作失败，请与管理员联系";

            if (BankcardTypeInfoService.Instance.DeleteBankCardInfoById(GetQueryString("cid"), out message))
            {
                return SuccessedJson(message, "System_BankCardInfo", "System_BankCardInfo", "forward", "/system/bankcardinfo");
            }
            else
            {
                return FailedJson(message);
            }
        }

        /// <summary>
        /// 更新银行卡信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoEditBankCardInfo()
        {
            BankcardTypeInfoModel model = new BankcardTypeInfoModel();
            model.BankcardEnumValue = GetFormData("card_fun");
            model.BankcardTypeId = GetFormData("cardId");
            model.BankEnumValue = GetFormData("openBankId");
            model.CardBinCode = GetFormData("card_bin_code");
            model.CardBrand = GetFormData("card_brand");
            model.CardLevel = GetFormData("card_level");
            model.CardNature = GetFormData("card_nav");
            model.CardType = GetFormData("card_type");
            model.CurrencyType = GetFormData("card_curreny");

            if (model.BankcardTypeId == null ||
                model.BankcardEnumValue == null ||
                model.BankEnumValue == null ||
                model.CardBinCode == null ||
                model.CardBrand == null ||
                model.CardLevel == null ||
                model.CardNature == null ||
                model.CardType == null ||
                model.CurrencyType == null)
            {
                LogUtil.Debug("更新卡信息操作失败，提交数据异常，请与管理员联系");
                return FailedJson("操作失败，提交数据异常，请与管理员联系");
            }

            if (BankcardTypeInfoService.Instance.UpdateBankCardInfo(model))
            {
                return SuccessedJson("成功更新银行卡信息", "System_BankCardInfo", "System_BankCardInfo", "forward", "/system/bankcardinfo");
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        /// <summary>
        /// 新建银行卡信息操作。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeFlag]
        public JsonResult DoAddBankCardInfo()
        {
            BankcardTypeInfoModel model = new BankcardTypeInfoModel();
            model.BankcardEnumValue = GetFormData("card_fun");
            model.BankcardTypeId = Guid.NewGuid().ToString();
            model.BankEnumValue = GetFormData("openBankId");
            model.CardBinCode = GetFormData("card_bin_code");
            model.CardBrand = GetFormData("card_brand");
            model.CardLevel = GetFormData("card_level");
            model.CardNature = GetFormData("card_nav");
            model.CardType = GetFormData("card_type");
            model.CurrencyType = GetFormData("card_curreny");

            if (model.BankcardEnumValue == null ||
                model.BankEnumValue == null ||
                model.CardBinCode == null ||
                model.CardBrand == null ||
                model.CardLevel == null ||
                model.CardNature == null ||
                model.CardType == null ||
                model.CurrencyType == null)
            {
                LogUtil.Debug("新建卡信息操作失败，提交数据异常，请与管理员联系");
                return FailedJson("操作失败，提交数据异常，请与管理员联系");
            }

            if (BankcardTypeInfoService.Instance.NewBankCardTypeInfo(model))
            {
                return SuccessedJson("成功新建银行卡信息", "System_BankCardInfo", "System_BankCardInfo", "forward", "/system/bankcardinfo");
            }
            else
            {
                return FailedJson("操作失败，请与管理员联系");
            }
        }

        protected void GetBankCardInfo()
        {
            
            ViewBag.OpenBankId = GetFormData("openBankId");
            ViewBag.BinCode = GetFormData("cardBinNum");
            ViewBag.PageIndex = Convert.ToInt32((GetFormData("pageNum") == null) ? "1" : GetFormData("pageNum"));
            ViewBag.PageSize = Convert.ToInt32((GetFormData("numPerPage") == null) ? "20" : GetFormData("numPerPage"));
            ViewBag.OrderField = (GetFormData("orderField") == null) ? "card_bin_code" : GetFormData("orderField");
            ViewBag.OrderDirection = (GetFormData("orderDirection") == null) ? "asc" : GetFormData("orderDirection");
            ViewBag.BankCardTotal = BankcardTypeInfoService.Instance.GetBankCardTotalByOpenBankId(ViewBag.OpenBankId);
            ViewBag.BankCardList = BankcardTypeInfoService.Instance.GetBankCardListByOpenBankId(ViewBag.OpenBankId, ViewBag.BinCode, ViewBag.PageIndex, ViewBag.PageSize, ViewBag.OrderField, ViewBag.OrderDirection);
        }

        #endregion


        /// <summary>
        /// 系统日志管理视图。
        /// </summary>
        /// <returns></returns>
        public ActionResult SysLogs()
        {
            return View();
        }
    }
}
