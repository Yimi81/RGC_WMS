using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public interface IBmsOrganizationService : IDomainServiceBase
    {

        ResponseDo<BmsOrganization> ManageCreateOrganization(BmsOrganization request, long userId);

        ResponseDo<BmsOrganization> ManageDeleteOrganization(Int64 menuId, long userId);

        ResponseDo<BmsOrganization> ManageModifyOrganization(BmsOrganization request, long userId);

        ResponseDo<List<BmsOrganization>> ManageGetOrganizationTree();
        ResponseDo<List<BmsOrganization>> ManageGetChildrenOrganization(long organizationId);
        ResponseDo<BmsOrganization> ManageGetOrganizationDetail(long organizationId);
        ResponseDo<BmsOrganization> ManageSingleGet(long id);

        /// <summary>
        /// 获取组织架构Id路径
        /// demo ：1,2,4
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetOrgIds(string Ids, long Id);
        List<BmsOrganizationCascaderDo> UserCascaderGet(Int64 parentOrgId, List<string> orgIds);

        ResponseDo<int> ManageMenuUpdate(long organizationId, List<BmsOrganizationMenu> add_list, List<BmsOrganizationMenu> delete_list);

        List<long> GrantedMenuIdsQuery(long organizationId);
    }
}
