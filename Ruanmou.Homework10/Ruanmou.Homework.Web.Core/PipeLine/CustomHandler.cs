using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Ruanmou.Homework.Web.Core.PipeLine
{
    /// <summary>
    /// 扩展IRouteHandler，
    /// 扩展IHttpHandler
    /// </summary>
    public class CustomHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.Url.AbsoluteUri;
            if (url.IndexOf(".") > -1 && url.Substring(url.LastIndexOf(".") + 1) == "readme")
            {
                HttpRequest Request = context.Request;
                HttpResponse Response = context.Response;
                Response.Write("<html>");
                Response.Write("<body>");
                Response.Write("<h1>这是一条来自系统的消息，通过访问.readme的后缀进行访问....</h1>");
                Response.Write(string.Format("<h1>当前地址为：{0}</h1>", url));
                Response.Write("</body>");
                Response.Write("</html>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        //public IHttpHandler GetHttpHandler(RequestContext requestContext)
        //{
        //    throw new NotImplementedException();
        //}
    }

    //public class MyHttpHandler : IHttpHandler
    //{
    //    public MyHttpHandler(RequestContext requestContext)
    //    {
    //        Console.WriteLine("自定义的IHttpHandler");
    //    }



    //}
}
