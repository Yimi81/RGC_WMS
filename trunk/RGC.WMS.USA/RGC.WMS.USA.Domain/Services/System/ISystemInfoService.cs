using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System.Do;

namespace RGC.WMS.USA.Domain.Services.System
{
    public interface ISystemInfoService: IDomainServiceBase
    {
        ResponseDo<string> CreateSystem(SystemCreateOrUpdateDo createUser, long creatorUserId);
        ResponseDo<SystemCreateOrUpdateDo> GetSystemDetail(long systemId);
        ResponseDo<string> ModifySystem(SystemCreateOrUpdateDo modifySystem, long modifierUserId);
        ResponseDo<string> DeleteSystem(long systemId, long deleterUserId);
    }
}
