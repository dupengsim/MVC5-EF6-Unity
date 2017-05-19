using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework;
using Ruanmou.Homework.Framework.Models;
using Ruanmou.Homework.Framework.VModel;

namespace Ruanmou.Homework.Bussiness.Interface
{
    [UserHandlerAttribute]
    public interface IUserMenuService : IBaseService
    {

        /// <summary>
        ///  根据账号用户信息实体
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        User UserLogin(string account);

        /// <summary>
        ///  修改用户最后登录的时间
        /// </summary>
        /// <param name="user"></param>
        void LastLogin(User user);

        void UserLastLogin(User user);

        /// <summary>
        /// a 增用户 (随机测试10个用户)
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        IEnumerable<User> InsertUsers(IEnumerable<User> users);

        /// <summary>
        /// b 增菜单 (随机测试10个菜单，要求起码三层父子关系id/parentid，SourcePath=父SourcePath+/+GUID)
        /// 这里一个一个菜单添加，因为下级菜单的parentid需要上级菜单的id
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        Menu InsertMenu(Menu menu);

        /// <summary>
        /// c 设置某个用户和10个菜单的映射关系（User  Menu  UserMenuMapping）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        void MappingUserMenu(User user, IEnumerable<Menu> menus);

        /// <summary>
        /// d 找出某用户拥有的全部菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Menu> QueryMenuByUser(User user);

        /// <summary>
        /// e 找出拥有某菜单的全部用户列表
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        List<User> QueryUserByMenu(Menu menu);

        /// <summary>
        /// f 根据菜单id找出全部子菜单的列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Menu> QueryMenuByID(int id);

        /// <summary>
        /// g 找出名字中包含"系统"的菜单列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<Menu> QueryMenuByKeyWord(string keyword);

        /// <summary>
        /// h 物理删除某用户的时候，删除其全部的映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        void DeleteMappingByUser(User user);

        /// <summary>
        /// i 物理删除某菜单的时候，删除其全部的映射
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        void DeleteMappingByMenu(Menu menu);

        IQueryable<Menu> QueryMenu(Expression<Func<Menu, bool>> where);

        /// <summary>
        ///  分页查询用户列表
        /// </summary>
        /// <param name="pageModel">参数model</param>
        /// <returns></returns>
        PageResult<UserView> QueryUserPageList(UserPageModel pageModel);

        /// <summary>
        ///  更新用户的状态
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="state">修改后的状态</param>
        void UpdateState(int id, int state);

        /// <summary>
        ///  根据id查询单个用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        UserView GetUserById(int id);
        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="model">修改后的用户信息实体</param>
        void UpdateUserInfo(UserView model);
        /// <summary>
        ///  添加新的用户
        /// </summary>
        /// <param name="model">新的用户信息实体</param>
        void AddNewUser(UserView model);
        /// <summary>
        ///  查询树形菜单的数据源
        /// </summary>
        /// <returns></returns>
        List<MenuTreeView> GetMenuTreeList();
        /// <summary>
        ///  删除单个菜单
        /// </summary>
        /// <param name="menuId">菜单编号</param>
        void DeleteMenuById(int menuId);
        /// <summary>
        ///   编辑菜单名称
        /// </summary>
        /// <param name="menuId">菜单编号</param>
        /// <param name="menuName">修改后的菜单名称</param>
        void UpdateMenuName(int menuId, string menuName);
        /// <summary>
        ///  添加新的菜单
        /// </summary>
        /// <param name="menu">新的菜单实体信息</param>
        void AddMenu(MenuView menu);
        /// <summary>
        ///  修改菜单信息
        /// </summary>
        /// <param name="menu">修改后的菜单实体</param>
        void UpdateMenu(MenuView menu);
    }
}
