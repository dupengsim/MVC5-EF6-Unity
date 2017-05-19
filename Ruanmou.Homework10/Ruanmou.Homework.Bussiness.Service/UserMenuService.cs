using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.Models;
using Ruanmou.Homework.Framework.VModel;
using Ruanmou.Homework.Framework.Mapper;
using Ruanmou.Homework.Framework.ExtendExpression;
using Ruanmou.Homework.Bussiness.Service.Mapping;

namespace Ruanmou.Homework.Bussiness.Service
{
    public class UserMenuService : BaseService, IUserMenuService
    {
        #region Identity
        //操作基于这些完成
        private DbSet<User> _UserDbSet = null;
        private DbSet<Menu> _MenuDbSet = null;
        private DbSet<UserMenuMapping> _MappingDbSet = null;
        public UserMenuService(DbContext dbContext)
            : base(dbContext)
        {
            this._MappingDbSet = dbContext.Set<UserMenuMapping>();
            this._UserDbSet = dbContext.Set<User>();
            this._MenuDbSet = dbContext.Set<Menu>();
        }
        public UserMenuService(DbContext dbContext, int id)
            : base(dbContext)
        {
            this._MappingDbSet = dbContext.Set<UserMenuMapping>();
            this._UserDbSet = dbContext.Set<User>();
            this._MenuDbSet = dbContext.Set<Menu>();
        }
        #endregion Identity

        /// <summary>
        ///  根据账号用户信息实体
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns></returns>
        public User UserLogin(string account)
        {
            return this._UserDbSet.FirstOrDefault(c => c.Mobile.Equals(account) || c.Account.Equals(account) || c.Email.Equals(account));
        }

        /// <summary>
        ///  修改用户最后登录的时间
        /// </summary>
        /// <param name="user"></param>
        public void LastLogin(User user)
        {
            user.LastLoginTime = DateTime.Now;
            base.Update(user);
        }

        public void UserLastLogin(User user)
        {
            //using (JDContext context = new JDContext())
            //{
            //    user.LastLoginTime = DateTime.Now;
            //    context.SaveChanges();
            //}
        }

        /// <summary>
        /// a 增用户 (随机测试10个用户)
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public IEnumerable<User> InsertUsers(IEnumerable<User> users)
        {
            return base.Insert<User>(users);
        }

        /// <summary>
        /// b 增菜单 (随机测试10个菜单，要求起码三层父子关系id/parentid，SourcePath=父SourcePath+/+GUID)
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public Menu InsertMenu(Menu menu)
        {
            return base.Insert<Menu>(menu);
        }

        /// <summary>
        /// c 设置某个用户和10个菜单的映射关系（User  Menu  UserMenuMapping）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        public void MappingUserMenu(User user, IEnumerable<Menu> menus)
        {
            var mappingList = menus.Select(o => new UserMenuMapping() { UserId = user.Id, MenuId = o.Id });
            base.Insert<UserMenuMapping>(mappingList);
        }

        /// <summary>
        /// d 找出某用户拥有的全部菜单列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Menu> QueryMenuByUser(User user)
        {
            var menuIdList = _MappingDbSet.Where(o => o.UserId == user.Id).Select(o => o.MenuId);//并不会执行
            return _MenuDbSet.Where(o => menuIdList.Contains(o.Id)).ToList();
        }

        /// <summary>
        /// e 找出拥有某菜单的全部用户列表
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public List<User> QueryUserByMenu(Menu menu)
        {
            var userIdList = this._MappingDbSet.Where(o => o.MenuId == menu.Id).Select(o => o.UserId);
            return this._UserDbSet.Where(o => userIdList.Contains(o.Id)).ToList();
        }

        /// <summary>
        /// f 根据菜单id找出全部子菜单的列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Menu> QueryMenuByID(int id)
        {
            return this._MenuDbSet.Where(o => o.SourcePath.StartsWith(this._MenuDbSet.Where(m => m.Id == id).Select(m => m.SourcePath).FirstOrDefault())).ToList();
        }

        /// <summary>
        /// g 找出名字中包含"系统"的菜单列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Menu> QueryMenuByKeyWord(string keyword)
        {
            return this._MenuDbSet.Where(o => o.Name.Contains(keyword)).ToList();
        }

        /// <summary>
        /// h 物理删除某用户的时候，删除其全部的映射
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void DeleteMappingByUser(User user)
        {
            var mappingList = this._MappingDbSet.Where(o => o.UserId == user.Id);
            using (var trans = base.Context.Database.BeginTransaction())
            {//同一个context，每次都即时commit，使用BeginTransaction(事务方式一)
                try
                {
                    base.Delete<User>(user);
                    //throw new Exception("123");//会取消前一次的提交
                    base.Delete<UserMenuMapping>(mappingList);//执行查询，按条执行sql，一个连接
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    if (trans != null)
                        trans.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// i 物理删除某菜单的时候，删除其全部的映射
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public void DeleteMappingByMenu(Menu menu)
        {
            var menuList = this._MenuDbSet.Where(o => o.SourcePath.StartsWith(menu.SourcePath));
            var mappingList = this._MappingDbSet.Where(o => menuList.Select(m => m.Id).Contains(o.MenuId));
            this._MappingDbSet.RemoveRange(mappingList);
            this._MenuDbSet.RemoveRange(menuList);
            base.Commit();//移除多个对象，使用base.Commit即SaveChanges来完成事务(事务方式二)
            //执行查询，按条执行sql，一个连接
        }

        public IQueryable<Menu> QueryMenu(Expression<Func<Menu, bool>> where)
        {
            return this._MenuDbSet.Where(where);
        }

        /// <summary>
        ///  分页查询用户列表
        /// </summary>
        /// <param name="pageModel">参数model</param>
        /// <returns></returns>
        public PageResult<UserView> QueryUserPageList(UserPageModel pageModel)
        {
            Expression<Func<User, bool>> lambdaWhere = c => c.Id > 0;
            Expression<Func<User, DateTime>> lambdaOrderBy = c => c.CreateTime;
            if (!string.IsNullOrWhiteSpace(pageModel.SearchKey))
            {
                lambdaWhere = lambdaWhere.And<User>(c => c.Name.Contains(pageModel.SearchKey)
                || c.Account.Contains(pageModel.SearchKey)
                || c.Email.Contains(pageModel.SearchKey)
                || c.Mobile.Contains(pageModel.SearchKey));
            }
            var result = base.QueryPage(lambdaWhere, pageModel.rows, pageModel.page, lambdaOrderBy, false);
            PageResult<UserView> datalist = new PageResult<UserView>();
            datalist.pageCount = result.pageCount;
            datalist.PageIndex = result.PageIndex;
            datalist.PageSize = result.PageSize;
            datalist.TotalCount = result.TotalCount;
            datalist.DataList = result.DataList.ConvertToUserViews();
            return datalist;
        }

        /// <summary>
        ///  根据id查询单个用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public UserView GetUserById(int id)
        {
            var user = base.Find<User>(id);
            return user.Mapper<UserView>();
        }

        /// <summary>
        ///  更新用户的状态
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="state">修改后的状态</param>
        public void UpdateState(int id, int state)
        {
            var user = base.Find<User>(id);
            if (user == null)
                throw new Exception("entity is not exist!");
            user.State = state;
            base.Update<User>(user);
        }

        /// <summary>
        ///  更新用户信息
        /// </summary>
        /// <param name="model">修改后的用户信息实体</param>
        public void UpdateUserInfo(UserView model)
        {
            var user = base.Find<User>(model.Id);
            user.Account = model.Account;
            user.CompanyId = model.CompanyId;
            user.CompanyName = model.CompanyName;
            user.Email = model.Email;
            user.Id = model.Id;
            user.Mobile = model.Mobile;
            user.Name = model.Name;
            user.Password = model.Password;
            user.State = model.State;
            user.UserType = model.UserType;
            base.Update<User>(user);
        }

        /// <summary>
        ///  添加新的用户
        /// </summary>
        /// <param name="model">新的用户信息实体</param>
        public void AddNewUser(UserView model)
        {
            User user = model.ConvertToUser();
            base.Insert<User>(user);
        }

        /// <summary>
        ///  查询树形菜单的数据源
        /// </summary>
        /// <returns></returns>
        public List<MenuTreeView> GetMenuTreeList()
        {
            var result = new List<MenuTreeView>();
            var menuList = this._MenuDbSet.Where(c => c.State == 0).ToList();
            foreach (var item in menuList.Where(c => c.ParentId == 0))
            {
                var ent = new MenuTreeView();
                ent.id = item.Id;
                ent.pId = item.ParentId;
                ent.name = item.Name;
                ent.Description = item.Description;
                ent.Url = item.Url;
                ent.SourcePath = item.SourcePath;
                ent.MenuLevel = item.MenuLevel;
                ent.Sort = item.Sort;
                BuildChildrenTree(ent, menuList);
                result.Add(ent);
            }
            return result;
        }
        /// <summary>
        ///  递归绑定子目录
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="datalist"></param>
        private void BuildChildrenTree(MenuTreeView parent, List<Menu> datalist)
        {
            parent.children = new List<MenuTreeView>();
            foreach (var item in datalist.Where(c => c.ParentId == parent.id))
            {
                var ent = new MenuTreeView();
                ent.id = item.Id;
                ent.pId = item.ParentId;
                ent.name = item.Name;
                ent.Description = item.Description;
                ent.Url = item.Url;
                ent.SourcePath = item.SourcePath;
                ent.MenuLevel = item.MenuLevel;
                ent.Sort = item.Sort;
                BuildChildrenTree(ent, datalist);
                parent.children.Add(ent);
            }
        }

        /// <summary>
        ///  删除单个菜单
        /// </summary>
        /// <param name="menuId">菜单编号</param>
        public void DeleteMenuById(int menuId)
        {
            var menu = base.Find<Menu>(menuId);
            menu.State = 1;
            base.Update<Menu>(menu);
        }

        /// <summary>
        ///   编辑菜单名称
        /// </summary>
        /// <param name="menuId">菜单编号</param>
        /// <param name="menuName">修改后的菜单名称</param>
        public void UpdateMenuName(int menuId, string menuName)
        {
            var menu = base.Find<Menu>(menuId);
            menu.Name = menuName;
            base.Update<Menu>(menu);
        }

        /// <summary>
        ///  修改菜单信息
        /// </summary>
        /// <param name="menu">修改后的菜单实体</param>
        public void UpdateMenu(MenuView menu)
        {
            Menu ent = this.Find<Menu>(menu.Id);
            ent.Description = menu.Description;
            ent.LastModifyTime = DateTime.Now;
            ent.MenuLevel = menu.MenuLevel;
            ent.Name = menu.Name;
            ent.ParentId = menu.ParentId;
            ent.Sort = menu.Sort;
            ent.SourcePath = menu.SourcePath;
            ent.Url = menu.Url;
            base.Update<Menu>(ent);
        }

        /// <summary>
        ///  添加新的菜单
        /// </summary>
        /// <param name="menu">新的菜单实体信息</param>
        public void AddMenu(MenuView menu)
        {
            Menu ent = menu.ConvertToMenu();
            base.Insert<Menu>(ent);
        }

        /// <summary>
        /// 释放EF数据连接
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
