using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Purchase;
using RGC.WMS.USA.Domain.Repositories.Purchase;

namespace RGC.WMS.USA.Data.Repositories.Warehouse
{
    /// <summary>
    /// MeridianGo 2020/06/23
    /// </summary>
    public class PackingListDetailRepository : RepositoryBase<PackingListDetail>, IPackingListDetailRepository {
        public PackingListDetailRepository(
            DbContext context) : base(context)
        { }
    }
}
