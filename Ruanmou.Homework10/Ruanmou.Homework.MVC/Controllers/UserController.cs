using System;
using System.Web.Mvc;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.VModel;
using Ruanmou.Homework.Framework.Models;
using Ruanmou.Homework.MVC.Common;
using Ruanmou.Homework.Framework.Enums;
using Ruanmou.Homework.Web.Core.Filter;
using Ruanmou.Homework.Framework.Encrypt;

namespace Ruanmou.Homework.MVC.Controllers
{
    /// <summary>
    ///  用户信息管理
    /// </summary>
    [AuthorityAttribute]
    public class UserController : BaseController
    {
        #region identity
        private IUserMenuService _userMenuService = null;
        private IUserCompanyService _userCompanyService = null;
        public UserController(IUserMenuService userMenuServie, IUserCompanyService userCompanyService)
        {
            this._userMenuService = userMenuServie;
            this._userCompanyService = userCompanyService;
        }
        #endregion

        #region 分页展示用户列表
        /// <summary>
        ///  分页展示用户列表
        /// </summary>
        /// <param name="page">当前页码</param>
        /// <returns></returns>
        public ActionResult Index(int? page, string key)
        {
            UserPageModel pageModel = new UserPageModel();
            pageModel.page = page ?? 1;
            pageModel.rows = 15;
            pageModel.SearchKey = key;
            var datalist = _userMenuService.QueryUserPageList(pageModel);
            return View(datalist);
        }
        #endregion

        #region 添加|编辑界面
        /// <summary>
        ///  添加|编辑界面
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public ActionResult Add(int? id)
        {
            UserView user = null;
            if (id != null && id.HasValue)
            {
                user = this._userMenuService.GetUserById(id.Value);
                ViewBag.Title = "用户编辑";
            }
            else
            {
                ViewBag.Title = "用户添加";
            }
            ViewBag.CompanyList = this._userCompanyService.GetCompanyList();
            return View(user);
        } 
        #endregion

        #region 添加|编辑用户信息
        /// <summary>
        ///  添加|编辑用户信息
        /// </summary>
        /// <param name="model">前台视图传递的form表单数据</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(UserView model)
        {
            if (model.Id > 0)
                this._userMenuService.UpdateUserInfo(model);
            else
            {
                model.CreateTime = DateTime.Now;
                model.Password = MD5Encrypt.Encrypt(model.Password);
                this._userMenuService.AddNewUser(model);
            }
            return Json(new ResponseResult() { StatusCode = 100, Message = "保存成功" });
        } 
        #endregion

        #region 删除单个用户
        /// <summary>
        ///  删除单个用户
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public JsonResult Delete(int id)
        {
            this._userMenuService.Delete<User>(id);
            return Json(new ResponseResult() { StatusCode = 100, Message = "删除成功" });
        }
        #endregion

        #region 激活|冻结账号
        /// <summary>
        ///  激活|冻结账号
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="state">修改后的用户状态</param>
        /// <returns></returns>
        public JsonResult Update(int id, int state)
        {
            this._userMenuService.UpdateState(id, state);
            string result = "";
            switch (state)
            {
                case (int)UserStateEnum.Normal:
                    result = "激活成功";
                    break;
                case (int)UserStateEnum.Frozen:
                    result = "冻结成功";
                    break;
                default:
                    break;
            }
            return Json(new ResponseResult() { StatusCode = 100, Message = result });
        }
        #endregion
    }
}