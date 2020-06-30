using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Warehouse
{
    public interface IWarehouseRepository : IRepository<WarehouseInfo>
    {
        void Create(long loginId, WarehouseInfo entity);

        void Update(long loginId, WarehouseInfo entity);

        void Delete(long loginId, WarehouseInfo entity);

        void Recovery(long loginId, WarehouseInfo entity);

        void UpdateStatus(WarehouseInfo entity, long modifierUserId);

        WarehouseInfo Get(long id);

        List<WarehouseInfo> GetPage((string SearchKey, int Status, int IsDeleted, int PageSize, int CurrentPage) searchFilter, out int count);

        bool IsExistWarehouseNumber(long iUnincludedId, string sNumber);

        void ForceRefreshWarehouseDict();

        List<(long Id, string Number, string Name, string PostCodePrefix)> GetWarehouseSimpleList();

        List<WarehouseInfo> GetWarehouseDict();
    }
}
