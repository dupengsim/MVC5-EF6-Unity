using System;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using Ruanmou.Homework.Bussiness.Interface;
using Ruanmou.Homework.EF.Model;

namespace Ruanmou.Homework.Bussiness.Service
{
    public class CommodityService : BaseService, ICommodityService
    {
        private DbSet<Commodity> _CommodityDbSet = null;
        public CommodityService(DbContext dbContext)
            : base(dbContext)
        {
            this._CommodityDbSet = dbContext.Set<Commodity>();
        }

        public IQueryable<Commodity> QueryCommodity(Expression<Func<Commodity, bool>> where)
        {
            return this._CommodityDbSet.Where(where);
        }

        public override void Dispose()
        {
            Console.WriteLine("已释放");
            base.Dispose();
        }
    }
}
