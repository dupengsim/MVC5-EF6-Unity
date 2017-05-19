using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ruanmou.Homework.Web.Core.PipeLine;

namespace Ruanmou.Homework.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        /// <summary>
        ///  自定义特殊路由：禁止chrome浏览器访问网站
        /// </summary>
        /// <param name="routes"></param>
        public static void RegistRefuse(RouteCollection routes)
        {
            routes.Add("myRoute", new BroswerRoute());
        }

        /// <summary>
        /// 这个是扩展了IRouteHandler和IHttpHandler
        /// </summary>
        public static void RegisterCustomRoutes(RouteCollection routes)
        {
            Route route = new Route("ipeng/{other}", new CustomRouteHandler());
            RouteTable.Routes.Add(route);
        }
    }
}
