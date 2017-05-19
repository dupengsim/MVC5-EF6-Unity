using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.VModel;

namespace Ruanmou.Homework.MVC.App_Start
{
    public class AutoMapperBootStrapper
    {

        public static void ConfigureAutoMapper()
        {
            // 这两个写在一起，前一个会被后者覆盖，导致出错！！！
            //Mapper.Initialize(x => x.CreateMap<UserView, User>());
            //Mapper.Initialize(x => x.CreateMap<User, UserView>());
            //Mapper.Initialize(x => x.CreateMap<MenuView, Menu>());
            // 最新版本中少了很多方法，没有此版本好用
            Mapper.CreateMap<User, UserView>();
            Mapper.CreateMap<UserView, User>();
            Mapper.CreateMap<MenuView, Menu>();
        }
    }
}