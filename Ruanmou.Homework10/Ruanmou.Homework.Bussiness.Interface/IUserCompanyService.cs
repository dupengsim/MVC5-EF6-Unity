using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Homework.EF.Model;

namespace Ruanmou.Homework.Bussiness.Interface
{
    public interface IUserCompanyService : IBaseService
    {

        /// <summary>
        ///  查询所有的公司列表
        /// </summary>
        /// <returns></returns>
        List<Company> GetCompanyList();
    }
}
