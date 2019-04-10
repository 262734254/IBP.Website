using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Framework.Utilities;
using IBP.Common;
using IBP.Services;

namespace IBP.Controllers
{
    public class HomeController : BaseController
    {
        [AuthorizeFlag]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [AuthorizeFlag]
        public ActionResult ChangePwd()
        {
            return View();
        }

        /// <summary>
        /// 获取图片验证码。
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidCode()
        {
            string code = ImageUtil.CreateValidateCode(6);
            SessionUtil.AuthorizationCode = code;
            byte[] bytes = ImageUtil.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult Logout()
        {
            UserInfoService.Instance.Logout();
            return View("Login");
        }

        public ActionResult LoginDialog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DoLoginDialog()
        {
            //账户
            string LoginAccount = GetFormData("LoginInput").ToLower();
            if (string.IsNullOrEmpty(LoginAccount))
            {
                return FailedJson("登录名称不能为空");
            }

            //密码
            string LoginPassword = GetFormData("LoginPwd");
            if (string.IsNullOrEmpty(LoginPassword))
            {
                return FailedJson("密码不能为空！");
            }

            LoginStatusEnum loginEnum = UserInfoService.Instance.UserLogin(LoginAccount, LoginPassword);

            //验证码错误
            if (loginEnum == LoginStatusEnum.NameOrPwdErrorAndShowValidCode)
            {
                return FailedJson("用户名或密码错误");
            }
            //账户名不存在
            if (loginEnum == LoginStatusEnum.NotExists)
            {
                return FailedJson("账户名不存在");
            }
            //禁用
            if (loginEnum == LoginStatusEnum.Disabled)
            {
                return FailedJson("该用户已被禁用");
            }

            if (loginEnum == LoginStatusEnum.Success)
            {
                return SuccessedJson("登录成功", "Home_Index", "Home_Index", "closeCurrent", "/");
            }

            return FailedJson("用户名或密码错误");
        }


        [HttpPost]
        public JsonResult DoLogin()
        {
            //账户
            string LoginAccount = GetFormData("LoginInput").ToLower();
            if (string.IsNullOrEmpty(LoginAccount))
            {
                return Json("ACCOUNTNULL", "登录名称不能为空！");
            }

            //密码
            string LoginPassword = GetFormData("LoginPwd");
            if (string.IsNullOrEmpty(LoginPassword))
            {
                return Json("PASSWORDNULL", "密码不能为空！");
            }

            LoginStatusEnum loginEnum = UserInfoService.Instance.UserLogin(LoginAccount, LoginPassword, GetFormData("CodeNum"));

            //验证码错误
            if (loginEnum == LoginStatusEnum.NameOrPwdErrorAndShowValidCode)
            {
                return Json(new { code = "NUMCODEERROR", msg = "用户名或密码错误" });
            }
            //账户名不存在
            if (loginEnum == LoginStatusEnum.NotExists)
            {
                return Json(new { code = "ACCOUNTNOTEXISTS", msg = "账户名不存在" });
            }
            //禁用
            if (loginEnum == LoginStatusEnum.Disabled)
            {
                return Json(new { code = "Forbidden", msg = "该用户已被禁用" });
            }

            if (loginEnum == LoginStatusEnum.Success)
            {
                return Json(new { code = "ok" });
            }

            return Json(new { code = "FAILED", msg = "用户名或密码错误" });
        }
    }
}
