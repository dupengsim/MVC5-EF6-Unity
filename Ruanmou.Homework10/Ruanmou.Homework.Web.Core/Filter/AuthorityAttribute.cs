using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ruanmou.Homework.Web.Core.Filter
{
    /// <summary>
    ///  会员登录身份认证过滤
    /// </summary>
    public class AuthorityAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var ucookie = httpContext.Request.Cookies["platform_loginuser"];
            if (ucookie == null || ucookie.Values.Count == 0) return false;
            return true;
        }
        /// <summary>
        ///  认证失败后的处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Clear();
            var refs = filterContext.HttpContext.Request.Url;
            filterContext.Result = new RedirectResult("~/login/index?reurl=" + refs);
        }
    }
}
