using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Bidding;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Bidding
{
    public interface ICompetitionRepository : IRepository<Competition>
    {
        public Competition SingleGet(long Id);

        public int SingleAdd(Competition dto);

        public int SingleUpdate(Competition entity);

        public int BatchUpdatePrice(List<CompetitionDaily> obj);


        public int BatchAdd(List<CompetitionDaily> entity);
        public int UpdateStatus(long modifierUserId, long id, bool isValid);
        public int SingleDelete(long loginId, long Id);

        public List<Competition> GetAll();

        public List<Competition> GetAllList();

        public Competition GetByPlatformUniqueId(long platformId, string uniqueId);

        public List<Competition> GetPlatformCompetitionList(long PlatformId, string key, long itemId, long productId, int? status, int pageSize, int currentPage,out int count);

        public List<Competition> GetOtherPlatformCompetitionList(long PlatformId, string key, int pageSize, int currentPage, out int count);

        public List<Competition> CompetitionPageQuery(string key, long itemId, int pageSize, int currentPage, out int count);

        public List<Competition> CompetitionPlatformPageQuery(string key, long ProductId, int pageSize, int currentPage, out int count);

        public int BatchChangeStatus(List<Competition> entity);

        List<(string value, long id)> SearchPageQuery(string key);
    }
}
