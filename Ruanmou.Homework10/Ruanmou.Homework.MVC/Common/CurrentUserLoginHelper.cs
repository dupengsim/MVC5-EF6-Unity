using Ruanmou.Homework.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ruanmou.Homework.Framework;
using Ruanmou.Homework.Framework.Extension;

namespace Ruanmou.Homework.MVC.Common
{
    public static class CurrentUserLoginHelper
    {
        /// <summary>
        ///  从Cookie中获取登录用户的信息
        /// </summary>
        /// <returns></returns>
        public static CurrentLoginUser GetCurrentLoginUserInfo()
        {
            var user = new CurrentLoginUser();
            var ucookie = HttpContext.Current.Request.Cookies[StaticConstant.Platform_LoginUserInfo];
            if (ucookie == null || ucookie.Values.Count == 0) return user;
            user = JsonHelper.StringToObject<CurrentLoginUser>(ucookie.Value);
            return user;
        }
    }
}