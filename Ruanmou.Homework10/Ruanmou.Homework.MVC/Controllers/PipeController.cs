using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ruanmou.Homework.MVC.Controllers
{
    public class PipeController : Controller
    {
        // GET: Pipe
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  禁止访问的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbid()
        {
            return View();
        }
    }
}