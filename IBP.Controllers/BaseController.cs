using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics;
using Framework.Utilities;
using IBP.Services;
using Framework.Common;
using IBP.Common;
using System.Web;
using System.Web.Routing;

namespace IBP.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 封装跳转的方法
        /// </summary>
        /// <param name="ActionName"></param>
        /// <param name="ControllerName"></param>
        /// <returns></returns>
        protected RedirectToRouteResult RedirectToNewAction(string ActionName, string ControllerName)
        {
            return RedirectToRoute("Default", new { controller = ControllerName, action = ActionName });
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            if (SessionUtil.Current == null)
            {
                UserInfoService.Instance.SetVisitorsSessionInfo();
            }

            LogUtil.Debug(string.Format("开始校验当前用户是否有权限执行方法{0}/{1}.", controllerName, actionName));

            // 权限验证
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                // 如果 action 有 AuthorizeFlagAttribute 特性标记，表示需要验证
                if (attributes[i] is AuthorizeFlagAttribute)
                {
                    if (!PermissionService.HasPermission(SessionUtil.Current.RoleId, SessionUtil.Current.UserGroup, controllerName, actionName))
                    {
                        throw new AuthorizationException(LangUtil.GetLanguage("oc_no_permission"));
                    }
                    else                    
                    {
                        break;
                    }
                }
            }
            stopwatch.Stop();
            LogUtil.Debug(string.Format("校验当前用户是否有权限执行方法{0}/{1}结束,时间为{2}毫秒。"
                , controllerName
                , actionName
                , stopwatch.ElapsedMilliseconds));
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            ViewData["controllerName"] = controllerName;
            ViewData["actionName"] = actionName;

            LogUtil.Debug("Action开始：" + controllerName +
                                                    "/" +
                                                    actionName +
                                                    "。详细参数：");
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (!Request.Headers.AllKeys.Contains("ajax"))
            {
                //if ((filterContext.Result is JsonResult) || (filterContext.Result is ContentResult))
                //if ((filterContext.Result is JsonResult))
                //{
                //    string domainName = Request.Url.Host;
                //    //string outText = "<script type=\"text/javascript\">try{document.domain='" + domainName + "';}catch(x){}</script>";
                //    Response.Write(domainName);
                //}
            }

            LogUtil.Debug("Action结束：" +
                                                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                                                    "/" +
                                                    filterContext.ActionDescriptor.ActionName +
                                                    "。详细参数：");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            ////Request.IsAjaxRequest();
            string useAjax = Request.Headers["ajax"];
            int statusCode = 200;

            if (useAjax == "true")
            {
                #region ajax 请求

                if (filterContext.Exception is MessageException)
                {
                    MessageException me = filterContext.Exception as MessageException;
                    if (string.IsNullOrEmpty(me.Detail))
                    {
                        filterContext.Result = Json(me.Code, me.Message);
                    }
                    else
                    {
                        filterContext.Result = Json(me.Code, me.Message, me.Detail);
                    }
                }
                else if (filterContext.Exception is AuthenticationException)
                {
                    AuthenticationException ae = filterContext.Exception as AuthenticationException;
                    filterContext.Result = Json("auth", ae.Message);
                }
                else if (filterContext.Exception is AuthorizationException)
                {
                    AuthorizationException ae = filterContext.Exception as AuthorizationException;
                    filterContext.Result = NoPermissionJson();
                }
                else
                {
                    // 未知的异常全部写入Error级别的日志
                    Exception realException = filterContext.Exception;
                    while (realException.InnerException != null)
                    {
                        realException = realException.InnerException;
                    }

                    string errorMessage = realException.Message;
                    string action = "url:" + filterContext.HttpContext.Request.Url
                        + "; Type: " + filterContext.HttpContext.Request.RequestType;
                    //// 未知的异常全部写入Error级别的日志
                    LogUtil.Error("错误信息：" + action + "; " + errorMessage + " 堆栈信息："
                        + realException.StackTrace);

                    // 根据配置决定是否向客户端输出详细信息
                    if (ConfigUtil.GetReleaseSetting().ToLower() == "true")
                    {
                        filterContext.Result = Json("error", filterContext.Exception.Message, filterContext.Exception.StackTrace);
                    }
                    else
                    {
                        string msg = GetErrorDescription(filterContext.Exception.Message);
                        filterContext.Result = Json("msg", msg);
                    }
                }
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = statusCode;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                #endregion
            }
            else
            {
                #region 普通回发请求

                if (filterContext.Exception is MessageException)
                {
                    MessageException me = filterContext.Exception as MessageException;

                    if (string.IsNullOrEmpty(me.Detail))
                    {
                        RedirectToError("Error", "Index", this.ControllerContext.HttpContext, me.Message);
                    }
                    else
                    {
                        RedirectToError("Error", "Index", this.ControllerContext.HttpContext, me.Message + me.Detail);
                    }
                }
                else if (filterContext.Exception is AuthenticationException) // 未经认证时，需要返回到登陆页面
                {
                    string errorMessage = filterContext.Exception.Message;
                    LogUtil.Error("错误信息：" + errorMessage + " 堆栈信息：" + filterContext.Exception.StackTrace);

                    filterContext.ExceptionHandled = true;
                    RedirectToLogin(this.ControllerContext.HttpContext);
                    //RedirectToError("Home", "Login", this.ControllerContext.HttpContext, errorMessage);
                }
                else if (filterContext.Exception is AuthorizationException) // 没有权限时，跳到错误页面并提示“没有权限”的信息
                {
                    string errorMessage = filterContext.Exception.Message;
                    LogUtil.Error("错误信息：" + errorMessage + " 堆栈信息：" + filterContext.Exception.StackTrace);

                    filterContext.ExceptionHandled = true;
                    RedirectToLogin(this.ControllerContext.HttpContext);
                    //RedirectToError("Home", "Login", this.ControllerContext.HttpContext, errorMessage);
                    
                    //AuthorizationException ae = filterContext.Exception as AuthorizationException;

                    //string errorMessage = filterContext.Exception.Message;
                    //LogUtil.Error("错误信息：" + errorMessage + " 堆栈信息：" + filterContext.Exception.StackTrace);

                    //filterContext.ExceptionHandled = true;
                    //RedirectToError("Error", "NoPermission", this.ControllerContext.HttpContext, errorMessage);
                }
                else
                {
                    Exception realException = filterContext.Exception;
                    while (realException.InnerException != null)
                    {
                        realException = realException.InnerException;
                    }

                    string errorMessage = realException.Message;

                    string action = "url:" + filterContext.HttpContext.Request.Url
                        + "; Type: " + filterContext.HttpContext.Request.RequestType;
                    //// 未知的异常全部写入Error级别的日志
                    LogUtil.Error("错误信息：" + action + "; " + errorMessage + " 堆栈信息：" + realException.StackTrace);

                    // 根据配置决定是否向客户端输出详细信息
                    if (ConfigUtil.GetReleaseSetting().ToLower() == "true")
                    {
                        RedirectToError("Error", "Index", this.ControllerContext.HttpContext, errorMessage + realException.StackTrace);
                    }
                    else
                    {
                        string msg = GetErrorDescription(realException.Message);
                        RedirectToError("Error", "Index", this.ControllerContext.HttpContext, msg);
                    }

                    filterContext.ExceptionHandled = true;
                }

                #endregion
            }
        }

        protected JsonResult SuccessedJson(string returnMessage, string returnNavTabId, string returnRel, string returnCallbackType, string returnForwardUrl)
        {
            //string json = "{\"statusCode\":\"200\",\"message\":\"\u64cd\u4f5c\u6210\u529f\",\"navTabId\":\"pagination\",\"rel\":\"\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { statusCode = "200", message = returnMessage, navTabId = returnNavTabId, rel = returnRel, callbackType = returnCallbackType, forwardUrl = returnForwardUrl};

            return jr;
        }

        protected JsonResult SuccessedJson(string returnMessage, string returnNavTabId, string returnRel, string returnCallbackType, string returnForwardUrl, object extParameter)
        {
            //string json = "{\"statusCode\":\"200\",\"message\":\"\u64cd\u4f5c\u6210\u529f\",\"navTabId\":\"pagination\",\"rel\":\"\",\"callbackType\":\"closeCurrent\",\"forwardUrl\":\"\"}";
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { statusCode = "200", message = returnMessage, navTabId = returnNavTabId, rel = returnRel, callbackType = returnCallbackType, forwardUrl = returnForwardUrl, extPara = extParameter };

            return jr;
        }

        protected JsonResult FailedJson(string returnMessage)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { statusCode = "300", message = returnMessage };

            return jr;
        }

        protected JsonResult NoPermissionJson()
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { statusCode = "301", message = "您没有当前操作权限", navTabId="", callbackType = "", forwardUrl=""};

            return jr;
        }

        protected JsonResult Json(object obj)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = obj;

            return jr;
        }

        protected JsonResult Json(object obj, object objtwo)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { obj = obj, objtwo = objtwo };
            return jr;
        }

        protected JsonResult Json(object obj, object existsObj, int pageCount)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { obj = obj, existsObj = existsObj, count = pageCount };
            return jr;
        }

        protected JsonResult Json(object obj, int pageCount)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { obj = obj, count = pageCount };
            return jr;
        }

        //protected JsonResult Json(object obj1, object obj2)
        //{
        //    JsonResult jr = new JsonResult();
        //    jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    jr.Data = new { one = obj1, two = obj2 };
        //    return jr;
        //}
        protected JsonResult Json(object obj1, object obj2, object obj3)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet; 
            jr.Data = new { one = obj1, two = obj2, three = obj3 };
            return jr;
        }

        protected JsonResult Json(object obj1, object obj2, object obj3, object obj4)
        {
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = new { one = obj1, two = obj2, three = obj3, four = obj4 };
            return jr;
        }

        /// <summary>
        /// 以 html 格式返回 json 数据
        /// </summary>
        /// <param name="message">json字符</param>
        /// <returns>返回json</returns>
        protected ContentResult JsonHtml(string message)
        {
            return JsonHtml("", message, "");
        }

        /// <summary>
        /// 以 html 格式返回 json 数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected ContentResult JsonHtml(string code, string message)
        {
            return JsonHtml(code, message, "");
        }

        /// <summary>
        /// 以 html 格式返回 json 数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        protected ContentResult JsonHtml(string code, string message, string detail)
        {
            ContentResult contentResult = new ContentResult();
            contentResult.Content = string.Format("<script type=\"text/javascript\">document.domain=\"{3}\";</script>{{\"code\":\"{0}\",\"msg\":\"{1}\",\"detail\":\"{2}\"}}", code, message, detail, Request.Url.DnsSafeHost);
            contentResult.ContentType = "text/html";

            return contentResult;
        }

        protected ContentResult JScriptHtml(string script)
        {
            ContentResult contentResult = new ContentResult();
            contentResult.Content = string.Format("<script type=\"text/javascript\">{0}</script>", script);
            contentResult.ContentType = "text/html";

            return contentResult;
        }

        protected string GetFormData(string name)
        {
            return Request.Form[name];
        }

        [ValidateInput(false)]
        protected string GetFormDataNotValidate(string name)
        {
            return Request.Form[name];
        }

        protected string GetQueryString(string name)
        {
            return Request.QueryString[name];
        }

        /// <summary>
        /// 友好错误提示
        /// </summary>
        /// <param name="controllerName">controllerName</param>
        /// <param name="actionName">actionName</param>
        /// <param name="httpContextBase">httpContextBase</param>
        /// <param name="errors">errors</param>
        private void RedirectToError(string controllerName, string actionName,
                                                HttpContextBase httpContextBase, string errors)
        {
            httpContextBase.Response.Clear();
            httpContextBase.Server.ClearError();
            IController errorController = new ErrorController();
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", controllerName);
            routeData.Values.Add("action", actionName);
            routeData.Values.Add("error", errors);
            errorController.Execute(new RequestContext(httpContextBase, routeData));
        }

        private void RedirectToLogin(HttpContextBase httpContextBase)
        {
            httpContextBase.Response.Clear();
            httpContextBase.Server.ClearError();
            IController homeController = new HomeController();
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Login");
            
            homeController.Execute(new RequestContext(httpContextBase, routeData));
        }

        private ContentResult RedirectToAction(string controllerName, string actionName)
        {
            ContentResult contentResult = new ContentResult();
            contentResult.Content = string.Format("<script type=\"text/javascript\">window.location.replace('/{0}/{1}');</script>", controllerName, actionName);
            contentResult.ContentType = "text/html";
            return contentResult;
        }

        /// <summary>
        /// 解释错误提示,所有未知错误，均提示网络异常，如为系统异常之类，客户看了会对我们系统的可靠性产生怀疑
        /// </summary>
        /// <param name="error">error</param>
        /// <param name="errors">errors</param>
        private string GetErrorDescription(string error)
        {
            if (error.IndexOf("Evt.HRP.Common.LangUtil") >= 0)
            {
                return "多语言文件读取失败！";
            }

            return LangUtil.GetLanguage("bc_network_exception");
        }

        protected string GetGuid()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        protected void InitPagerForm()
        {
            ViewBag.OrderField = (string.IsNullOrEmpty(GetFormData("orderField"))) ? "created_on" : GetFormData("orderField");
            ViewBag.OrderDirection = (string.IsNullOrEmpty(GetFormData("orderDirection"))) ? "desc" : GetFormData("orderDirection");
            ViewBag.PageIndex = Convert.ToInt32((string.IsNullOrEmpty(GetFormData("pageNum"))) ? "1" : GetFormData("pageNum"));
            ViewBag.PageSize = Convert.ToInt32((string.IsNullOrEmpty(GetFormData("numPerPage"))) ? "20" : GetFormData("numPerPage"));
        }

        protected void InitCustomerBasicInfo()
        {
            ViewBag.CustomerComeFrom = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户来源", false);
            ViewBag.CustomerLevel = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("客户等级", false);
            ViewBag.Carriers = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("运营商", false);
            ViewBag.PhoneBrand = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("优选品牌", false);
            ViewBag.Consumer = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("通讯消费", false);
            ViewBag.MobilePhonePrice = CustomDataInfoService.Instance.GetCustomDataDomainModelByName("手机价位", false);
        }
    }
}