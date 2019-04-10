using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace IBP.Controllers
{
    [HandleError]
    [OutputCache(Location = OutputCacheLocation.None)]
    public class ErrorController : Controller
    {
        public ActionResult Index(string error)
        {
            ViewBag.Error = error.Replace("\r\n", "<br/>");
            ViewBag.Description = "";
            return View();
        }

        public ActionResult NoPermission(string error)
        {
            ViewBag.Error = error.Replace("\r\n", "<br/>");
            ViewBag.Description = "";
            return View("Index");
        }

        public ActionResult HttpError404(string error)
        {
            ViewBag.Error = "Sorry, an error occurred while processing your request. (404)";
            ViewBag.Description = error.Replace("\r\n", "<br/>");
            return View("Index");
        }

        public ActionResult HttpError500(string error)
        {
            ViewBag.Error = "Sorry, an error occurred while processing your request. (500)";
            ViewBag.Description = error.Replace("\r\n", "<br/>");
            return View("Index");
        }

        public ActionResult General(string error)
        {
            ViewBag.Error = "Sorry, an error occurred while processing your request.";
            ViewBag.Description = error.Replace("\r\n", "<br/>");
            return View("Index");
        }

    }

}
