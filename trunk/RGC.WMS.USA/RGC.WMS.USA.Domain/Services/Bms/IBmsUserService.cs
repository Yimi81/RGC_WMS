using HuigeTec.Core.Domain.Entities;
using HuigeTec.Core.Domain.Services;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public interface IBmsUserService : IDomainServiceBase
    {
        ResponseDo<BmsUserExtend> GetDetail(long userId);
        ResponseDo<string> CreateBmsUser(BmsUserCreateOrUpdateDo createUser, long creatorUserId);
        ResponseDo<string> UpdateBmsUser(BmsUserCreateOrUpdateDo createUser, long ModifierUserId);

        ResponseDo<string> UpdateBmsUserStatus(BmsUserCreateOrUpdateDo updateUser, long ModifierUserId);
        ResponseDo<string> ChangeBmsUserPassword(Tuple<long, string, string> input, long ModifierUserId);
        ResponseDo<string> DeleteBmsUser(long userId, long ModifierUserId);

        int AddOrganization(List<BmsUserOrganization> request);
        ResponsePageDo<BmsUserExtend> GetOrganizationUserList(long orgId, string key, int PageSize, int currentPage);
        ResponsePageDo<BmsUserExtend> GetOtherOrganizationUserList(long orgId, string key, int PageSize, int currentPage);

        ResponseDo<string> RemoveOrganization(long userId, long organizationId, long loginId);

        ResponseDo<int> MenuUpdate(Int64 userId, List<Entities.Bms.BmsUserMenuExtend> add_list, List<Entities.Bms.BmsUserMenuExtend> update_list, List<Entities.Bms.BmsUserMenuExtend> delete_list);

        ResponseDo<List<Entities.Bms.BmsUserMenuExtend>> MenuListAllGet(long UserId);

        #region 系统
        ResponseDo<List<long>> GetUserSystemIds(long userId);
        ResponseDo<string> UpdateGrantedSystem(List<long> systemIds, long userId, long modifierUserId, out List<long> addIdList, out List<long> deleteIdList);
        #endregion

        #region 角色
        ResponseDo<List<long>> GetUserRoleIds(long userId);
        ResponseDo<string> UpdateGrantedRole(List<long> roleIds, long userId, long modifierUserId);
        ResponseDo<List<Entities.Bms.BmsUserExtend>> GetRoleGrantedUsers(long roleId, int currentPage, out int count);
        #endregion

        #region 平台
        ResponseDo<List<long>> GetUserPlatformIds(long userId);
        ResponseDo<string> UpdateGrantedPlatform(List<long> platformIds, long userId, long modifierUserId);
        ResponseDo<List<Entities.Bms.BmsUserExtend>> GetPlatformGrantedUsers(long platformId, int currentPage, out int count);
        #endregion
    }

}
