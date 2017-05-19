using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ruanmou.Homework.MVC.Common
{
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var response = context.HttpContext.Response;
            if (Data == null) return;
            ContentType = "application/json;charset=utf-8";
            ContentEncoding = System.Text.Encoding.UTF8;
            //这里使用自定义日期格式，默认是ISO8601格式    
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            var jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.Converters.Add(timeConverter);
            response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, jSetting));
        }
    }
}