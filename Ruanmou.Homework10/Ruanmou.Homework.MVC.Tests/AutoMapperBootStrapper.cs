using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.VModel;

namespace Ruanmou.Homework.MVC.Tests
{
    public class AutoMapperBootStrapper
    {

        public static void ConfigureAutoMapper()
        {
            Mapper.Initialize(x => x.CreateMap<User, UserView>());
        }
    }
}
