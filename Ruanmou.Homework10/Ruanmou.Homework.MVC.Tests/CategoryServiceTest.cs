using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using System.Configuration;
using System.IO;
using Microsoft.Practices.Unity.Configuration;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.Bussiness.Service;
using Ruanmou.Homework.Web.Core.IOC;

namespace Ruanmou.Homework.MVC.Tests
{
    [TestClass]
    public class CategoryServiceTest
    {

        private ICategoryService _categoryService = null;
        private IUserCompanyService _userCompanyService = null;
        private IUserMenuService _userMenuService = null;
        /// <summary>
        ///  初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // 实例化automapper
            AutoMapperBootStrapper.ConfigureAutoMapper();
            #region 方式一
            //ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            //fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\CfgFiles\\Unity.Config.xml");//找配置文件的路径
            //Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            //UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);

            //IUnityContainer container = new UnityContainer();
            //section.Configure(container, "testContainer"); 
            #endregion

            #region 方式二
            IUnityContainer container = DIFactory.GetContainer("testContainer");
            _categoryService = container.Resolve<ICategoryService>();
            _userCompanyService = container.Resolve<IUserCompanyService>();
            _userMenuService = container.Resolve<IUserMenuService>();
            #endregion

            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<IPhone, ApplePhone>();
            //_phone = container.Resolve<IPhone>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
        [Description("测试查询所有商品类别的方法")]
        public void TestGetAllCategory()
        {
            var datalist = _categoryService.CacheAllCategory();
            Assert.IsNotNull(datalist);
        }

        [TestMethod]
        [Description("测试查询所有公司列表的方法")]
        public void TestGetCompanyList()
        {
            var datalist = _userCompanyService.GetCompanyList();
            Assert.IsNotNull(datalist);
        }

        [TestMethod]
        [Description("测试根据id查询单个用户信息")]
        public void TestGetUserById()
        {
            var user = this._userMenuService.GetUserById(2);
            Assert.IsNotNull(user);
        }

        [TestMethod]
        [Description("测试查询菜单的树形目录结构")]
        public void TestGetMenuTreeList()
        {
            var datalist = this._userMenuService.GetMenuTreeList();
            Assert.IsNotNull(datalist);
        }
    }
}
