using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruanmou.Homework.Framework.ImageHelper;
using Ruanmou.Homework.Framework.Extension;
using System.IO;
using System.Drawing.Imaging;
using Ruanmou.Homework.MVC.Unility;
using Ruanmou.Homework.Framework;
using Ruanmou.Homework.Framework.Models;

namespace Ruanmou.Homework.MVC.Controllers
{
    public class LoginController : Controller
    {

        #region 登录页面
        /// <summary>
        ///  登录页面
        /// </summary>
        /// <param name="reurl">登录前的访问界面地址</param>
        /// <returns></returns>
        public ActionResult Index(string reurl)
        {
            ViewBag.Reurl = reurl;
            return View();
        }
        #endregion

        #region 用户登录
        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="password">密码</param>
        /// <param name="verify">验证码</param>
        /// <returns></returns>
        public ActionResult Login(string name, string password, string verify)
        {
            UserManager.LoginResult result = this.HttpContext.UserLogin(name, password, verify);
            if (result == UserManager.LoginResult.Success)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("failed", result.GetRemark());
                return RedirectToAction("index", "login");
            }
        }
        /// <summary>
        ///  Ajax方式登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="verify"></param>
        /// <returns></returns>
        public JsonResult AjaxLogin(string name, string password, string verify)
        {
            ResponseResult response = null;
            UserManager.LoginResult result = this.HttpContext.UserLogin(name, password, verify);
            if (result == UserManager.LoginResult.Success)
            {
                response = new ResponseResult() { StatusCode = 100, Message = "登录成功" };
            }
            else
            {
                ModelState.AddModelError("failed", result.GetRemark());
                response = new ResponseResult() { StatusCode = 101, Message = "登录失败，" + result.GetRemark() };
            }
            return Json(response);
        }
        #endregion

        #region 退出
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            this.HttpContext.UserLogout();
            return RedirectToAction("Index", "login");
        }
        #endregion


        #region 创建验证码
        /// <summary>
        ///  创建验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session[StaticConstant.Platform_LoginVerifyCode] = code;
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Gif);
            return File(stream.ToArray(), "image/gif");//返回FileContentResult图片
        }
        #endregion
    }
}