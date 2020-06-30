using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Bms;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bms
{
    public class BmsOrganizationRepository : RepositoryBase<BmsOrganization>, IBmsOrganizationRepository
    {
        private static IUnitOfWork _unitOfWork;

        private static readonly object _locker = new object();

        private static Dictionary<long, BmsOrganization> OrganizationDict;

        private static IRepository<BmsOrganizationMenu> _bmsOrganizationMenuRepository;
        private static IRepository<BmsUserOrganization> _bmsUserOrganizationRepository;
        private static IRepository<BmsUserMenuExtend> _bmsUserMenuRepository;

        public BmsOrganizationRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<BmsOrganizationMenu> bmsOrganizationMenuRepository,
            IRepository<BmsUserOrganization> bmsUserOrganizationRepository,
            IRepository<BmsUserMenuExtend> bmsUserMenuRepository
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _bmsOrganizationMenuRepository = bmsOrganizationMenuRepository;
            _bmsUserOrganizationRepository = bmsUserOrganizationRepository;
            _bmsUserMenuRepository = bmsUserMenuRepository;
        }


        public void RefreshOrganizationDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (OrganizationDict == null || OrganizationDict.Count == 0)
                {
                    OrganizationDict = new Dictionary<Int64, BmsOrganization>();
                    OrganizationDict = GetOrganizationDictFromDB();
                }
            }
        }

        public void ForceRefreshDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                OrganizationDict = new Dictionary<Int64, BmsOrganization>();
                OrganizationDict = GetOrganizationDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部组织结构
        /// </summary>
        public Dictionary<Int64, BmsOrganization> GetOrganizationDictFromDB()
        {
            var dict = new Dictionary<Int64, BmsOrganization>();

            var list = TableNoTracking.ToList();
            var organizationMenuList = _bmsOrganizationMenuRepository.TableNoTracking.ToList();

            if (list != null && list.Count() > 0)
            {
                dict = list.ToDictionary(p => p.Id);
                foreach (var organization in dict.Values)
                {
                    organization.OrganizationMenuDict = organizationMenuList.Where(p => p.OrganizationId == organization.Id).ToDictionary(p => p.MenuId);
                }
            }

            return dict;
        }

        /// <summary>
        /// 单个获取
        /// </summary>
        public BmsOrganization SingleGet(Int64 Id)
        {
            var obj = new BmsOrganization();

            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            if (OrganizationDict.Keys.Contains(Id))
            {
                obj = OrganizationDict[Id];
            }
            return obj;
        }

        /// <summary>
        /// 单个新增
        /// </summary>
        public BmsOrganization SingleInsert(BmsOrganization request)
        {
            Insert(request);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                //新增成功
                if (OrganizationDict == null || OrganizationDict.Count == 0)
                {
                    RefreshOrganizationDict();
                }
                lock (_locker)
                {
                    if (OrganizationDict.Keys.Contains(request.Id) == false)
                    {
                        OrganizationDict.Add(request.Id, request);
                    }
                }
            }
            return request;
        }

        /// <summary>
        /// 单个更新数据库
        /// </summary>
        /// <param name="obj"></param>
        public int SingleUpdate(BmsOrganization obj)
        {
            Update(obj);
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                if (OrganizationDict == null || OrganizationDict.Count == 0)
                {
                    RefreshOrganizationDict();
                }
                lock (_locker)
                {
                    if (OrganizationDict.Keys.Contains(obj.Id))
                    {
                        OrganizationDict[obj.Id] = obj;
                    }
                    else
                    {
                        RefreshOrganizationDict();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 单个删除
        /// </summary>
        public int SingleDelete(Int64 loginId, Int64 Id)
        {
            var obj = DeepCopyByReflect(SingleGet(Id));
            var excute = 0;
            using (var transaction = BeginTransaction())
            {
                try
                {
                    obj.IsDeleted = true;
                    obj.DeleterUserId = loginId;
                    obj.DeletionTime = DateTime.Now;
                    Update(obj);
                    excute = _unitOfWork.SaveChanges();
                    if (excute > 0)
                    {
                        var request = new BmsUserOrganization();
                        request = _bmsUserOrganizationRepository.TableNoTracking.FirstOrDefault(p => p.OrganizationId == Id);
                        if (request != null && request.Id > 0)
                        {
                            _bmsUserOrganizationRepository.Delete(request);
                            excute = _unitOfWork.SaveChanges();
                        }
                        if (excute > 0)
                        {
                            var req = new BmsUserMenuExtend();
                            var te = _bmsUserMenuRepository.TableNoTracking.Where(p => p.OrganizationId == Id);
                            if (te.Count() > 0)
                            {
                                foreach (var item in te)
                                {
                                    req = item;
                                    _bmsUserMenuRepository.Delete(req);
                                }
                                excute = _unitOfWork.SaveChanges();
                            }
                        }
                        if (OrganizationDict.Keys.Contains(Id))
                        {
                            OrganizationDict.Remove(Id);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return excute;
        }

        public List<BmsOrganization> AllListGet()
        {
            var result = new List<BmsOrganization>();
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            if (OrganizationDict.Count > 0)
            {
                result = OrganizationDict.Values.OrderBy(p => p.Id).ToList();
            }
            return result;
        }

        /// <summary>
        /// 获取组织架构Id路径
        /// demo ：1,2,4
        /// 根据组织架构路径获取整个路径的显示名称
        /// </summary>
        /// <param name="orgIds"></param>
        /// <returns></returns>
        public String GetOrgIdsName(string orgIds)
        {
            var orgsName = string.Empty;
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            lock (_locker)
            {
                var Ids = orgIds.Split(',').ToList();
                if (Ids != null && Ids.Any())
                {
                    foreach (var Id in Ids)
                    {
                        var org = OrganizationDict.Values.FirstOrDefault(p => p.Id.ToString() == Id);
                        if (org != null && org.Id.ToString() == Id)
                        {
                            if (!string.IsNullOrEmpty(org.DisplayName))
                            {
                                orgsName = orgsName + org.DisplayName + "/";
                            }
                        }
                    }
                    orgsName = orgsName.Substring(0, orgsName.Length - 1);
                }
            }
            return orgsName;
        }

        /// <summary>
        /// 获取完整的组织架构
        /// </summary>
        public List<BmsOrganization> TreeGet(Int64 parentId)
        {
            var list = new List<BmsOrganization>();
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            foreach (BmsOrganization organization in OrganizationDict.Values.Where(p => p.ParentId == parentId && !p.IsDeleted).OrderBy(p => p.ShowOrder))
            {
                if (organization.ParentId == parentId)
                {
                    var tree = new BmsOrganization();
                    tree.Code = organization.Code;
                    tree.Id = organization.Id;
                    tree.DisplayName = organization.DisplayName;
                    tree.ShowOrder = organization.ShowOrder;
                    tree.ParentId = organization.ParentId;
                    tree.Name = organization.Name;
                    tree.Children = TreeGet(organization.Id);

                    list.Add(tree);
                }
            }
            return list;
        }

        public List<BmsOrganization> ChildrenGet(Int64 parentId)
        {
            var list = new List<BmsOrganization>();
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            foreach (BmsOrganization organization in OrganizationDict.Values.Where(p => p.ParentId == parentId && !p.IsDeleted).OrderBy(p => p.ShowOrder))
            {
                if (organization.ParentId == parentId)
                {
                    var tree = new BmsOrganization();
                    tree.Code = organization.Code;
                    tree.Id = organization.Id;
                    tree.DisplayName = organization.DisplayName;
                    tree.ShowOrder = organization.ShowOrder;
                    tree.ParentId = organization.ParentId;
                    tree.Name = organization.Name;
                    list.Add(tree);
                }
            }
            return list;
        }

        #region 后台生成组织架构级联选择器相关操作

        #region 根据organizationId获取所有上级，同级和下级
        public List<string> GetOrganizationParent(long organizationId)
        {
            var orgIds = new List<string>();

            var org = OrganizationDict.Values.FirstOrDefault(p => p.Id == organizationId);

            if (org != null && org.ParentId > 0)
            {
                orgIds.Add(org.ParentId.ToString());
                orgIds = orgIds.Union(GetOrganizationParent(org.ParentId)).ToList();
            }
            return orgIds;
        }

        public List<string> GetOrganizationChildren(long organizationId)
        {
            var orgIds = new List<string>();

            var orgList = OrganizationDict.Values.Where(p => p.ParentId == organizationId);

            if (orgList != null && orgList.Any())
            {
                foreach (var org in orgList)
                {
                    orgIds.Add(org.Id.ToString());
                    orgIds = orgIds.Union(GetOrganizationChildren(org.Id)).ToList(); ;
                }
            }
            return orgIds;
        }
        #endregion

        /// <summary>
        /// 获取完整组织架构，标记可操作和不看操作,用于cascader
        /// </summary>
        public List<BmsOrganization> UserCascaderGet(Int64 parentOrgId, List<string> orgIds)
        {
            var list = new List<BmsOrganization>();
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            foreach (BmsOrganization organization in OrganizationDict.Values.Where(p => p.ParentId == parentOrgId).OrderBy(p => p.ShowOrder))
            {
                if (organization.ParentId == parentOrgId)
                {
                    var cascader = new BmsOrganization();
                    cascader.Id = organization.Id;
                    cascader.Name = organization.Name;
                    cascader.DisplayName = organization.DisplayName;
                    cascader.Children = UserCascaderGet(organization.Id, orgIds);
                    if (!cascader.Children.Any())
                    {
                        cascader.Children = null;
                    }

                    list.Add(cascader);
                }
            }
            return list;
        }
        #endregion

        /// <summary>
        /// 获取完整的菜单树
        /// </summary>
        public List<BmsOrganization> WholeTreeGet(Int64 parentMenuId, List<Int64> menuIds)
        {
            var list = new List<BmsOrganization>();
            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            foreach (BmsOrganization menu in OrganizationDict.Values.Where(p => p.ParentId == parentMenuId).OrderBy(p => p.ShowOrder))
            {
                if (menu.ParentId == parentMenuId)
                {
                    var tree = new BmsOrganization();
                    tree.Id = menu.Id;
                    tree.DisplayName = menu.DisplayName;
                    tree.ShowOrder = menu.ShowOrder;
                    tree.ParentId = menu.ParentId;
                    tree.Children = WholeTreeGet(menu.Id, menuIds);

                    list.Add(tree);
                }
            }
            return list;
        }

        /// <summary>
        /// 更新岗位的菜单权限
        /// </summary>
        public int MenuUpdate(long organizationId, List<BmsOrganizationMenu> add_list, List<BmsOrganizationMenu> delete_list)
        {
            //新增列表
            if (add_list != null && add_list.Count > 0)
            {
                foreach (var obj in add_list)
                {
                    _bmsOrganizationMenuRepository.Insert(obj);
                }
            }
            //删除列表
            if (delete_list != null && delete_list.Count > 0)
            {
                foreach (var obj in delete_list)
                {
                    _bmsOrganizationMenuRepository.Delete(obj);

                }
            }
            var result = _unitOfWork.SaveChanges();
            if (result > 0)
            {
                //更新数据字典
                lock (_locker)
                {
                    if (OrganizationDict == null || OrganizationDict.Count == 0)
                    {
                        RefreshOrganizationDict();
                    }
                    else
                    {
                        if (OrganizationDict.Keys.Contains(organizationId))
                        {
                            var organization = OrganizationDict[organizationId];
                            if (add_list != null && add_list.Count > 0)
                            {
                                foreach (var obj in add_list)
                                {
                                    if (organization.OrganizationMenuDict.Keys.Contains(obj.MenuId) == false)
                                        organization.OrganizationMenuDict.Add(obj.MenuId, obj);
                                }
                            }
                            if (delete_list != null && delete_list.Count > 0)
                            {
                                foreach (var obj in delete_list)
                                {
                                    if (organization.OrganizationMenuDict.Keys.Contains(obj.MenuId))
                                        organization.OrganizationMenuDict.Remove(obj.MenuId);
                                }
                            }
                        }
                    }


                }
            }
            return result;
        }

        public List<long> GrantedMenuIdsQuery(long organizationId)
        {
            var result = new List<long>();

            if (OrganizationDict == null || OrganizationDict.Count == 0)
            {
                RefreshOrganizationDict();
            }
            if (OrganizationDict.Count > 0 && OrganizationDict.Keys.Contains(organizationId))
            {
                if (OrganizationDict[organizationId].OrganizationMenuDict.Any())
                {
                    result = OrganizationDict[organizationId].OrganizationMenuDict.Keys.ToList();
                }
            }
            return result;
        }
    }
}
