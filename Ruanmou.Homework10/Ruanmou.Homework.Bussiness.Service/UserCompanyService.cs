using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;

namespace Ruanmou.Homework.Bussiness.Service
{
    public class UserCompanyService : BaseService, IUserCompanyService
    {
        #region Identity
        //操作基于这些完成
        private DbSet<User> _UserDbSet = null;
        private DbSet<Company> _CompanyDbSet = null;
        public UserCompanyService(DbContext dbContext)
            : base(dbContext)
        {
            this._UserDbSet = dbContext.Set<User>();
            this._CompanyDbSet = dbContext.Set<Company>();
        }
        #endregion Identity

        #region 查询所有的公司列表
        /// <summary>
        ///  查询所有的公司列表
        /// </summary>
        /// <returns></returns>
        public List<Company> GetCompanyList()
        {
            return this._CompanyDbSet.OrderByDescending(c => c.CreateTime).ToList();
        } 
        #endregion
    }
}
