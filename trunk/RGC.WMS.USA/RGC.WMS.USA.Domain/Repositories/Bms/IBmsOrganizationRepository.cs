using HuigeTec.Core.Domain.Repositories;
using RGC.WMS.USA.Domain.Entities.Bms;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Repositories.Bms
{
    public interface IBmsOrganizationRepository : IRepository<BmsOrganization>
    {
        void ForceRefreshDict();
        BmsOrganization SingleGet(Int64 Id);

        /// <summary>
        /// 单个新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BmsOrganization SingleInsert(BmsOrganization request);

        /// <summary>
        /// 单个更新数据库
        /// </summary>
        /// <param name="obj"></param>
        int SingleUpdate(BmsOrganization obj);

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="Id"></param>
        int SingleDelete(Int64 loginId, Int64 Id);


        List<BmsOrganization> AllListGet();

    

        /// <summary>
        /// 根据组织架构路径获取整个路径的显示名称
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        String GetOrgIdsName(string orgIds);

        /// <summary>
        /// 获取完整的组织架构
        /// </summary>
        /// <param name="parentMenuId">父菜单Id</param>
        /// <returns></returns>
        List<BmsOrganization> TreeGet(Int64 parentId);
        List<BmsOrganization> ChildrenGet(Int64 parentId);

        #region 后台生成组织架构级联选择器相关操作

        #region 根据organizationId获取所有上级，同级和下级
        List<string> GetOrganizationParent(long organizationId);
        List<string> GetOrganizationChildren(long organizationId);
        #endregion

        /// <summary>
        /// 获取完整组织架构，标记可操作和不看操作,用于cascader
        /// </summary>
        /// <param name="parentOrgId">父菜单Id</param>
        /// <returns></returns>
        List<BmsOrganization> UserCascaderGet(Int64 parentOrgId, List<string> orgIds);
        #endregion

        /// <summary>
        /// 获取完整的菜单树
        /// </summary>
        /// <param name="parentMenuId">父菜单Id</param>
        /// <param name="menuIds">授权的菜单Id</param>
        /// <returns></returns>
        List<BmsOrganization> WholeTreeGet(Int64 parentMenuId, List<Int64> menuIds);


        /// <summary>
        /// 更新岗位的菜单权限
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="add_list"></param>
        /// <param name="delete_list"></param>
        /// <returns></returns>
        int MenuUpdate(long organizationId, List<BmsOrganizationMenu> add_list, List<BmsOrganizationMenu> delete_list);



        List<long> GrantedMenuIdsQuery(long organizationId);
    }
}
