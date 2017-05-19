using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ruanmou.Homework.MVC.Common
{
    /// <summary>
    ///  扩展方法
    /// </summary>
    public static class HtmlHelpExtension
    {
        /// <summary>
        /// 通用分页代码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="jump"></param>
        /// <returns></returns>
        public static string HtmlHelpBuildPage(this HtmlHelper helper, int pageIndex, int pageCount, string jump)
        {
            if (pageCount <= 1) return "";
            // pageIndex 从 0开始 
            if (jump.IndexOf("?") > -1)
            {
                jump = jump + "&page=";
            }
            else
            {
                jump = jump + "?page=";
            }
            var ne_half = 2;
            var upper_limit = pageCount - 4;
            var start = pageIndex > ne_half ? Math.Max(Math.Min(pageIndex - ne_half, upper_limit), 0) : 0;
            var end = pageIndex > ne_half ? Math.Min(pageIndex + ne_half, pageCount) : Math.Min(4, pageCount);
            StringBuilder html = new StringBuilder("<nav style='float:right;'><ul class=\"pagination\">");
            if (pageIndex > 0)
            {
                html.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>", jump, pageIndex);
            }

            if (start > 0)
            {
                var end1 = Math.Min(1, start);
                for (var i = 0; i < end1; i++)
                {
                        html.AppendFormat("<li><a href=\"{1}{2}\">{0}</a></li>", i + 1, jump, (i + 1));
                }
                if (1 < start)
                {
                    html.AppendFormat("<li><a>...</a></li>");
                }
            }
            for (var i = start; i < end; i++)
            {
                if (pageIndex == i)
                {
                    html.AppendFormat("<li class=\"active\"><a href=\"{1}{2}\">{0}</a></li>", i + 1, jump, (i + 1));
                }
                else
                {
                    html.AppendFormat("<li><a href=\"{1}{2}\">{0}</a></li>", i + 1, jump, (i + 1));
                }

            }
            if (end < pageCount)
            {
                if (pageCount - 1 > end)
                {
                    html.AppendFormat("<li><a>...</a></li>");
                }
                var begin = Math.Max(pageCount - 1, end);
                for (var i = begin; i < pageCount; i++)
                {
                    html.AppendFormat("<li><a href=\"{1}{2}\">{0}</a></li>", i + 1, jump, (i + 1));
                }

            }
            if (pageIndex < pageCount - 1)
            {
                html.AppendFormat("<li><a href=\"{0}{1}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>", jump, pageIndex + 2);
            }
            html.AppendFormat("</ul></nav>");
            return html.ToString();
        }
    }
}