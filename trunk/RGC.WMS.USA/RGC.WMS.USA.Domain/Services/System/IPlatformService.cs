using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System.Do;

namespace RGC.WMS.USA.Domain.Services.System
{
    public interface IPlatformService: IDomainServiceBase
    {
        ResponseDo<string> ManageCreate(PlatformCreateOrUpdateDo createPlatform, long creatorUserId);
        ResponseDo<string> ManageModify(PlatformCreateOrUpdateDo modifyPlatform, long modifierUserId);
        ResponseDo<string> ManageDelete(long platformId, long modifierUserId);
        ResponseDo<PlatformCreateOrUpdateDo> ManageGetPlatformDetail(long platformId);
    }
}
