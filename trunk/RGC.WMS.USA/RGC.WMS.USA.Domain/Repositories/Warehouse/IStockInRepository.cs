using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Warehouse
{
    public interface IStockInRepository : IRepository<StockIn>
    {
        void Create(long loginId, StockIn entity);

        void Update(long loginId, StockIn entity);

        int Delete(long loginId, StockIn entity);

        int Recovery(long loginId, StockIn entity);

        int DetailDelete(long loginId, StockInDetail entity);

        int DetailRecovery(long loginId, StockInDetail entity);

        int UpdateStatus(StockIn entity, long modifierUserId);

        StockIn Get(long id);

        List<StockIn> GetPage((string SearchType, string SearchKey, long WarehouseId, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count);
    }
}
