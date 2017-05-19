using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.Enums
{
    /// <summary>
    ///  用户角色枚举
    /// </summary>
    public enum UserTypeEnum
    {
        /// <summary>
        ///  普通用户
        /// </summary>
        [Description("普通用户")]
        Normal = 0,
        /// <summary>
        ///  管理员
        /// </summary>
        [Description("管理员")]
        Admin = 1,
        /// <summary>
        ///  超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 2
    }
}
