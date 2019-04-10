using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace IBP.Controllers
{
    public class NoAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        #region IAuthorizationFilter 成员

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            // 标记使用，因此是空方法
        }

        #endregion
    }
}
