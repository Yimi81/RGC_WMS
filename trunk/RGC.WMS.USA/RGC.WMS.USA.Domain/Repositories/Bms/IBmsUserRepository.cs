using HuigeTec.Core.Domain.Entities;
using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Bms
{
    public interface IBmsUserRepository : IRepository<BmsUserExtend>
    {
        void ForceRefreshDict();
        BmsUserExtend SingleGet(long Id);
        BmsUserExtend SingleGet(string loginName);
        List<BmsUserExtend> GetAllUserByKeys(List<long> ids);
        bool IsLoginNameExists(string loginName);
        bool IsLoginNameExists(string loginName, long userId);
        bool IsEmailAddressExists(string emailAddress);
        List<BmsUserExtend> AllGet();
        bool SingleInsert(BmsUserExtend user, List<Entities.Bms.BmsUserMenuExtend> systemList);
        bool SingleUpdate(BmsUserExtend user, List<Entities.Bms.BmsUserMenuExtend> addList, List<Entities.Bms.BmsUserMenuExtend> deleteList);
        int SingleUpdateStatus(BmsUserExtend user);
        bool UpdatePassword(BmsUserExtend user);
        bool SigleDelete(BmsUserExtend user,string operatorName);
        List<Entities.Bms.BmsUserMenuExtend> MenuListAllGet(long userId);
        bool UpdateMenu(long userId, List<Entities.Bms.BmsUserMenuExtend> addList, List<Entities.Bms.BmsUserMenuExtend> updateList);
        List<BmsUserExtend> PageQuery(SearchFilterDo searchFilter, out int count);

        /// <summary>
        /// 添加用户组织架构
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int AddOrganization(List<BmsUserOrganization> request);

        List<BmsUserExtend> GetOrganizationUserList(long orgId, string key, int pageSize, int currentPage, out int totalCount);
        List<BmsUserExtend> GetOtherOrganizationUserList(long orgId, string key, int pageSize, int currentPage, out int totalCount);

        int RemoveOrganization(long userId, long orgId,long loginId);

        int MenuUpdate(Int64 userId, List<Entities.Bms.BmsUserMenuExtend> add_list, List<Entities.Bms.BmsUserMenuExtend> update_list, List<Entities.Bms.BmsUserMenuExtend> delete_list);
        bool UpdateSystem(long userId, List<BmsUserSystem> addList, List<BmsUserSystem> deleteList);
        bool UpdateRole(long userId, List<BmsUserRole> addList, List<BmsUserRole> deleteList);
        bool UpdatePlatform(long userId, List<BmsUserPlatform> addList, List<BmsUserPlatform> deleteList);
    }
}
