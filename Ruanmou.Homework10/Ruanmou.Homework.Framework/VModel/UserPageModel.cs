using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Homework.Framework.Models;

namespace Ruanmou.Homework.Framework.VModel
{
    /// <summary>
    ///  用户管理列表查询时的参数model
    /// </summary>
    public class UserPageModel : PageRequest
    {
        /// <summary>
        ///  搜索关键字
        /// </summary>
        public string SearchKey { get; set; }
    }
}
