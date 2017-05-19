using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.Models
{
    /// <summary>
    ///  当前登录账户信息
    /// </summary>
    public class CurrentLoginUser
    {
        public int Id { get; set; }
        [Display(Name = "账号")]
        public string Name { get; set; }
        public string Account { get; set; }
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public DateTime LoginTime { get; set; }

        public List<UserMenu> UserMenus { get; set; }
    }

    public class UserMenu
    {
        /// <summary>
        ///  菜单编号
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        ///  菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        ///  菜单地址
        /// </summary>
        public string MenuUrl { get; set; }
    }
}
