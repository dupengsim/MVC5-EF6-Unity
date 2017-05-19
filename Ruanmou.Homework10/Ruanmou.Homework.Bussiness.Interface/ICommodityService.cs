using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Homework.EF.Model;

namespace Ruanmou.Homework.Bussiness.Interface
{
    public interface ICommodityService : IBaseService
    {
        IQueryable<Commodity> QueryCommodity(Expression<Func<Commodity, bool>> where);
    }
}
