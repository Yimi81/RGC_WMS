using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Repositories.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public class BmsOrganizationService : IBmsOrganizationService
    {
        private readonly IBmsOrganizationRepository _bmsOrganizationRepository;
        public BmsOrganizationService(
            IBmsOrganizationRepository bmsOrganizationRepository)
        {
            _bmsOrganizationRepository = bmsOrganizationRepository;
        }
        public ResponseDo<BmsOrganization> ManageCreateOrganization(BmsOrganization request, long userId)
        {
            var result = new ResponseDo<BmsOrganization>();
            result.Data = new BmsOrganization();
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new CustomException("组织结构的名称必填", 1);

            var organization = new BmsOrganization();
            organization.Name = request.Name.Trim();
            organization.DisplayName = request.DisplayName.ToEmpty();
            organization.Code = request.Code;
            organization.ShowOrder = request.ShowOrder;
            organization.ParentId = request.ParentId;
            organization.CreationTime = DateTime.Now;

            _bmsOrganizationRepository.SingleInsert(organization);

            if (organization.Id > 0)
            {
                result.Code = 0;
                result.Success = true;
                result.Data = organization;
            }
            return result;
        }

        public ResponseDo<BmsOrganization> ManageDeleteOrganization(Int64 OrganizationId, long userId)
        {
            var result = new ResponseDo<BmsOrganization>();
            if (OrganizationId <= 0)
                throw new CustomException("参数异常", 1);

            var excute = _bmsOrganizationRepository.SingleDelete(userId, OrganizationId);
            if (excute <= 0)
                throw new CustomException("组织架构删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<BmsOrganization> ManageModifyOrganization(BmsOrganization request, long userId)
        {
            var result = new ResponseDo<BmsOrganization>();
            result.Data = new BmsOrganization();
            if (request.Id <= 0)
                throw new CustomException("参数异常", 1);

            var organization = _bmsOrganizationRepository.SingleGet(request.Id);

            organization.Name = request.Name.ToEmpty();
            organization.Code = request.Code;
            organization.ShowOrder = request.ShowOrder;
            organization.DisplayName = request.DisplayName;
            organization.LastModificationTime = DateTime.Now;
            organization.LastModifierUserId = userId;

            var excute = _bmsOrganizationRepository.SingleUpdate(organization);
            if (excute <= 0)
                throw new CustomException("组织架构更新失败", 1);

            result.Code = 0;
            result.Success = true;
            result.Data = organization;
            return result;
        }

        public ResponseDo<List<BmsOrganization>> ManageGetOrganizationTree()
        {
            var result = new ResponseDo<List<BmsOrganization>>();
            result.Data = _bmsOrganizationRepository.TreeGet(0);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<List<BmsOrganization>> ManageGetChildrenOrganization(long organizationId)
        {
            var result = new ResponseDo<List<BmsOrganization>>();
            result.Data = _bmsOrganizationRepository.ChildrenGet(organizationId);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<BmsOrganization> ManageGetOrganizationDetail(long organizationId)
        {
            var result = new ResponseDo<BmsOrganization>();
            result.Data = _bmsOrganizationRepository.SingleGet(organizationId);
            result.Code = 0;
            result.Success = true;
            return result;
        }
        public ResponseDo<BmsOrganization> ManageSingleGet(long id)
        {
            var result = new ResponseDo<BmsOrganization>();
            result.Data = _bmsOrganizationRepository.SingleGet(id);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取用户完整组织架构，区分可选择，不可选择
        /// </summary>
        public List<BmsOrganizationCascaderDo> UserCascaderGet(Int64 parentOrgId, List<string> orgIds)
        {
            var result = new List<BmsOrganizationCascaderDo>();
            if (orgIds != null && orgIds.Any())
            {
                var OrganizationList = _bmsOrganizationRepository.AllListGet();
                foreach (BmsOrganization organization in OrganizationList.Where(p => p.ParentId == parentOrgId).OrderBy(p => p.ShowOrder))
                {
                    if (organization.ParentId == parentOrgId)
                    {
                        var cascader = new BmsOrganizationCascaderDo();

                        if (orgIds.Contains(organization.Id.ToString()))
                        {
                            cascader.Disabled = false;
                        }

                        cascader.OrganizationId = organization.Id;
                        cascader.Name = organization.Name;
                        cascader.DisplayName = organization.DisplayName;
                        cascader.Children = UserCascaderGet(organization.Id, orgIds);
                        if (!cascader.Children.Any())
                        {
                            cascader.Children = null;
                        }

                        result.Add(cascader);
                    }
                }
            }
            return result;

        }

        public string GetOrgIds(string Ids, long Id)
        {

            var org = _bmsOrganizationRepository.SingleGet(Id);
            if (org != null && org.Id > 0)
            {
                Ids = org.Id.ToString() + "," + Ids;

                if (org.ParentId != 0)
                {
                    Ids = GetOrgIds(Ids, org.ParentId);
                }
            }

            return Ids;
        }

        public ResponseDo<int> ManageMenuUpdate(long organizationId, List<BmsOrganizationMenu> add_list, List<BmsOrganizationMenu> delete_list)
        {
            var result = new ResponseDo<int>();
            //新增列表
            var excute = _bmsOrganizationRepository.MenuUpdate(organizationId, add_list, delete_list);
            if (excute <= 0)
                throw new CustomException("菜单更新失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        public List<long> GrantedMenuIdsQuery(long organizationId)
        {
            var result = _bmsOrganizationRepository.GrantedMenuIdsQuery(organizationId);
            return result;
        }
    }
}
