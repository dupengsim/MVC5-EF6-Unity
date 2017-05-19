using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ruanmou.Homework.Web.Core.PipeLine
{
    public class BroswerRoute : RouteBase
    {

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request.UserAgent.IndexOf("Chrome/56.0.2924.87") >= 0)
            {
                RouteData rd = new RouteData(this, new MvcRouteHandler());
                rd.Values.Add("controller", "Pipe");
                rd.Values.Add("action", "Forbid");
                return rd;
            }
            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
