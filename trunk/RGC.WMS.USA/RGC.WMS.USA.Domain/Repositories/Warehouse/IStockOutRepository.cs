using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Warehouse
{
    public interface IStockOutRepository : IRepository<StockOut>
    {
        void Create(long loginId, StockOut entity);

        void Update(long loginId, StockOut entity);

        int Delete(long loginId, StockOut entity);

        int Recovery(long loginId, StockOut entity);

        int DetailDelete(long loginId, StockOutDetail entity);

        int DetailRecovery(long loginId, StockOutDetail entity);

        int UpdateStatus(StockOut entity, long modifierUserId);

        StockOut Get(long id);

        List<StockOut> GetPage((string SearchType, string SearchKey, long WarehouseId, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count);
    }
}
