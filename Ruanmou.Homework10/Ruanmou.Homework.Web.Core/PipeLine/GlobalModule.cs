using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Ruanmou.Homework.Framework.Log;

namespace Ruanmou.Homework.Web.Core.PipeLine
{
    public class GlobalModule : IHttpModule
    {

        private static Logger logger = Logger.CreateLogger(typeof(GlobalModule));

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);//Asp.net处理的第一个事件，表示处理的开始

            context.EndRequest += new EventHandler(context_EndRequest);//本次请求处理完成
        }

        // 处理BeginRequest 事件的实际代码
        private DateTime dt;
        void context_BeginRequest(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            //context.Response.Write(string.Format("<h1 style='color:#00f'>来自GlobalModule 的处理，{0}请求到达</h1><hr>", DateTime.Now.ToString()));
            logger.Info(string.Format("<h1 style='color:#00f'>来自GlobalModule 的处理，{0}请求到达</h1><hr>", DateTime.Now.ToString()));
        }

        // 处理EndRequest 事件的实际代码
        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            //context.Response.Write(string.Format("<hr><h1 style='color:#f00'>来自GlobalModule的处理，{0}请求结束</h1>", DateTime.Now.ToString()));
            logger.Info(string.Format("<hr><h1 style='color:#f00'>来自GlobalModule的处理，{0}请求结束</h1>", DateTime.Now.ToString()));
            ////处理地址重写
            if (context.Request.Url.AbsolutePath.Equals("/user/index", StringComparison.OrdinalIgnoreCase))
            {
                TimeSpan ts = DateTime.Now - dt;
                context.Response.Write(context.Request.UserAgent);// 输出当前浏览器的版本信息
                context.Response.Write(string.Format("<h1 style='color:#00f'>当前网站的作者：iPeng；页面耗时：{0}毫秒</h1><hr>", (ts.Minutes * 60 * 60 + ts.Seconds * 60 + ts.Milliseconds)));
                logger.Info(string.Format("<h1 style='color:#00f'>当前网站的作者：iPeng；页面耗时：{0}毫秒</h1><hr>", (ts.Minutes * 60 * 60 + ts.Seconds * 60 + ts.Milliseconds)));
            }
        }
    }
}
