using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms.Do;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public interface IBmsRoleService : IDomainServiceBase
    {
        ResponseDo<string> ManageCreateRole(BmsRoleCreateOrUpdateDo createRole, long creatorUserId);
        ResponseDo<string> ManageModifyRole(BmsRoleCreateOrUpdateDo modifyRole, long modifierUserId);
        ResponseDo<string> ManageDeleteRole(long roleId, long modifierUserId);
        ResponseDo<BmsRoleCreateOrUpdateDo> ManageGetRoleByRoleId(long roleId);
    }
}
