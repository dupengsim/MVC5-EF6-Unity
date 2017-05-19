using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.Enums
{
    /// <summary>
    ///  用户状态枚举
    /// </summary>
    public enum UserStateEnum
    {
        /// <summary>
        ///  正常
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        ///  冻结
        /// </summary>
        [Description("冻结")]
        Frozen = 1,
        /// <summary>
        ///  删除
        /// </summary>
        [Description("删除")]
        Deleted = 2
    }
}
