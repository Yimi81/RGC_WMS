using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Repositories.Warehouse;

namespace RGC.WMS.USA.Data.Repositories.Warehouse
{
    /// <summary>
    /// MeridianGo 2020/06/27
    /// </summary>
    public class StockOutDetailRepository : RepositoryBase<StockOutDetail>, IStockOutDetailRepository {
        public StockOutDetailRepository(
            DbContext context) : base(context)
        { }
    }
}
