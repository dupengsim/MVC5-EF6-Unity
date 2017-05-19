using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ruanmou.Homework.Web.Core.IOC;
using Ruanmou.Homework.MVC.App_Start;

namespace Ruanmou.Homework.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // 自定义HttpModule限制浏览器访问网站
            RouteConfig.RegistRefuse(RouteTable.Routes);
            // 自定义HttpHandler访问特定页面如：.readme
            RouteConfig.RegisterCustomRoutes(RouteTable.Routes);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // automapper实例化
            AutoMapperBootStrapper.ConfigureAutoMapper();
            // 控制器的实例化，内部通过unity进行依赖注入
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory());
        }
    }
}
