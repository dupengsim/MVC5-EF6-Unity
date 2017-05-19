using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.VModel;
using Ruanmou.Homework.Framework.Models;
using Ruanmou.Homework.MVC.Common;
using Ruanmou.Homework.Framework.Enums;
using Ruanmou.Homework.Web.Core.Filter;

namespace Ruanmou.Homework.MVC.Controllers
{
    /// <summary>
    ///  菜单管理
    /// </summary>
    [AuthorityAttribute]
    public class MenuController : BaseController
    {

        #region identity
        private IUserMenuService _userMenuService = null;
        public MenuController(IUserMenuService userMenuService)
        {
            this._userMenuService = userMenuService;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region 加载菜单树形目录
        /// <summary>
        ///  加载菜单树形目录
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuTreeList()
        {
            var datalist = this._userMenuService.GetMenuTreeList();
            return Json(datalist);
        }
        #endregion

        #region 根据菜单编号删除菜单
        /// <summary>
        ///  根据菜单编号删除菜单
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            this._userMenuService.DeleteMenuById(id);
            return Json(new ResponseResult() { StatusCode = 100, Message = "删除成功" });
        }
        #endregion

        #region 修改菜单的名称
        /// <summary>
        ///  修改菜单的名称
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <param name="newName">修改后的菜单名称</param>
        /// <returns></returns>
        public JsonResult Update(int id, string newName)
        {
            this._userMenuService.UpdateMenuName(id, newName);
            return Json(new ResponseResult() { StatusCode = 100, Message = "修改成功" });
        }
        #endregion

        #region 修改菜单信息
        /// <summary>
        ///  修改菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResult UpdateModel(MenuView menu)
        {
            this._userMenuService.UpdateMenu(menu);
            return Json(new ResponseResult() { StatusCode = 100, Message = "修改成功" });
        }
        #endregion

        #region 添加新的菜单
        /// <summary>
        ///  添加新的菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResult Add(MenuView menu)
        {
            this._userMenuService.AddMenu(menu);
            return Json(new ResponseResult() { StatusCode = 100, Message = "添加成功" });
        } 
        #endregion
    }

}