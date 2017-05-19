using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ruanmou.Homework.MVC.Common
{
    public class BaseController : Controller
    {
        #region 重写基类方法
        /// <summary>该类用于将 JSON 格式的内容发送到响应</summary>
        /// <param name="data">返回的数据</param>
        /// <param name="contentType">返回内容的类型</param>
        /// <param name="contentEncoding">返回内容的编码</param>
        /// <returns>返回Json格式</returns>
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding)
        {
            return new CustomJsonResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding };
        }
        /// <summary>该类用于将 JSON 格式的内容发送到响应</summary>
        /// <param name="data">返回的数据</param>
        /// <param name="jsonRequest">设置一个值，该值指示是否允许来自客户端的 HTTP GET 请求</param>
        /// <returns>返回Json格式</returns>
        public new JsonResult Json(object data, JsonRequestBehavior jsonRequest)
        {
            return new CustomJsonResult { Data = data, JsonRequestBehavior = jsonRequest };
        }
        /// <summary>该类用于将 JSON 格式的内容发送到响应</summary>
        /// <param name="data">返回的数据</param>
        /// <returns>返回Json格式</returns>
        public new JsonResult Json(object data)
        {
            return new CustomJsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion
    }
}