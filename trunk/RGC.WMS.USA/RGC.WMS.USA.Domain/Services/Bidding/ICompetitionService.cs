using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bidding.Do;

namespace RGC.WMS.USA.Domain.Services.Bidding
{
    public interface ICompetitionService:IDomainServiceBase
    {
        ResponseDo<string> Update(long loginId, CompetitionEditDo dto);
        ResponseDo<string> Create(long loginId, CompetitionEditDo dto);
        ResponseDo<string> IfExist(long platformId, long compId, string factoryModel);
        ResponsePageDo<(string value, long id)> GetSearchList(string key);
    }
}
