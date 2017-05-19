using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ruanmou.Homework.EF.Model;
using Ruanmou.Homework.Framework.VModel;

namespace Ruanmou.Homework.Bussiness.Service.Mapping
{
    /// <summary>
    ///  用户信息DTO
    /// </summary>
    public static class UserMenuMapper
    {

        /// <summary>
        ///  将UserView转换成User实体
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User ConvertToUser(this UserView user)
        {
            var result = Mapper.Map<User>(user);
            return result;
        }
        public static IEnumerable<User> ConvertToUsers(this IEnumerable<UserView> users)
        {
            IList<User> result = new List<User>();
            foreach (var item in users)
            {
                result.Add(ConvertToUser(item));
            }
            return result;
        }

        public static UserView ConvertToUserView(this User user)
        {
            var result = Mapper.Map<UserView>(user);
            return result;
        }
        public static List<UserView> ConvertToUserViews(this List<User> users)
        {
            List<UserView> result = new List<UserView>();
            foreach (var item in users)
            {
                result.Add(ConvertToUserView(item));
            }
            return result;
        }



        public static Menu ConvertToMenu(this MenuView menu)
        {
            var result = Mapper.Map<Menu>(menu);
            result.CreateTime = DateTime.Now;
            return result;
        }
        public static IEnumerable<Menu> ConvertToMenus(this IEnumerable<MenuView> menus)
        {
            IList<Menu> result = new List<Menu>();
            foreach (var item in menus)
            {
                result.Add(ConvertToMenu(item));
            }
            return result;
        }
    }
}
