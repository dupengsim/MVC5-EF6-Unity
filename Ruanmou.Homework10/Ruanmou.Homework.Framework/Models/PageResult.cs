using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Homework.Framework.Models
{
    public class PageResult<T>
    {
        /// <summary>
        ///  数据总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        ///  当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        ///  每页显示的数据量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前查询到的数据集合
        /// </summary>
        public List<T> DataList { get; set; }

        /// <summary>
        ///  计算出总共有多少页
        /// </summary>
        public int pageCount
        {
            get
            {
                if (PageSize <= 0) return 0;
                return TotalCount % PageSize == 0 ? TotalCount / PageSize : ((TotalCount / PageSize) + 1);
            }
            set { }
        }
    }
}
