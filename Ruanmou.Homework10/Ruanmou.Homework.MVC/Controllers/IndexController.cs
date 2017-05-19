using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;

namespace Ruanmou.Homework.MVC.Controllers
{
    public class IndexController : Controller
    {

        private IUserMenuService _userMenuService = null;

        public IndexController(IUserMenuService userMenuService)
        {
            this._userMenuService = userMenuService;
        }

        // GET: Index
        public ActionResult Index()
        {
            var user = this._userMenuService.Find<User>(2);
            
            return View();
        }
    }
}