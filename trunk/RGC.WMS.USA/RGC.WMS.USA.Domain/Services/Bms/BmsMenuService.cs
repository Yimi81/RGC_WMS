using HuigeTec.Core.Domain.Entities;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bms.Do;
using RGC.WMS.USA.Domain.Repositories.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Domain.Services.Bms
{
    public class BmsMenuService : IBmsMenuService
    {

        private readonly IBmsMenuRepository _bmsMenuRepositories;
        public BmsMenuService(
            IBmsMenuRepository bmsMenuRepositories)
        {
            _bmsMenuRepositories = bmsMenuRepositories;
        }
        /// <summary>
        /// 后台创建菜单
        /// </summary>
        public ResponseDo<BmsMenu> CreateMenu(BmsMenu request)
        {
            var result = new ResponseDo<BmsMenu>();
            request.Name = request.Name.ToEmpty();
            request.IcoName = request.IcoName.ToEmpty();
            if (request.Type == BmsMenuType.System)
                request.ParentId = 0;

            var excute = _bmsMenuRepositories.SingleInsert(request);
            if (excute <= 0 || request.Id <= 0)
                throw new CustomException("菜单添加失败", 1);

            result.Data = request;
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<string> DeleteMenu(Int64 menuId, long deleterUserId)
        {
            var result = new ResponseDo<string>();
            if (menuId <= 0)
                throw new CustomException("数据异常", 1);

            var excute = _bmsMenuRepositories.SingleDelete(menuId, deleterUserId);
            if (!excute)
                throw new CustomException("菜单删除失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        public ResponseDo<BmsMenu> ModifyMenu(BmsMenu request)
        {
            var result = new ResponseDo<BmsMenu>();
            var menu = _bmsMenuRepositories.SingleGet(request.Id);

            menu.Name = request.Name.ToEmpty();
            menu.AuthorizeCode = request.AuthorizeCode.ToEmpty();
            menu.IcoName = request.IcoName.ToEmpty();
            menu.Path = request.Path.ToEmpty();
            menu.Type = request.Type;
            menu.Remark = request.Remark.ToEmpty();
            menu.SeqNo = request.SeqNo;

            menu.LastModificationTime = DateTime.Now;
            menu.LastModifierUserId = request.LastModifierUserId;

            var excute = _bmsMenuRepositories.SingleUpdate(menu);
            if (excute <= 0 || request.Id <= 0)
                throw new CustomException("菜单修改失败", 1);

            result.Data = menu;
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 修改排序
        /// </summary>
        public ResponseDo<string> ModifyMenuSeqNo(List<Tuple<long, int>> list, long modifierUserId)
        {
            var result = new ResponseDo<string>();
            var excute = _bmsMenuRepositories.SingleUpdateMenuSeqNo(modifierUserId, list);
            if (!excute)
                throw new CustomException("菜单排序修改失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        public ResponseDo<List<BmsMenu>> GetMenuTree(long parentId)
        {
            var result = new ResponseDo<List<BmsMenu>>();
            result.Data = _bmsMenuRepositories.GetTree(parentId);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取菜单详细信息
        /// </summary>
        public ResponseDo<BmsMenu> GetMenuDetail(long id)
        {
            var result = new ResponseDo<BmsMenu>();
            result.Data = _bmsMenuRepositories.SingleGet(id);
            if (result.Data == null || result.Data.Id <= 0)
                throw new CustomException("菜单获取失败", 1);

            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取子集
        /// </summary>
        public ResponseDo<List<BmsMenu>> GetChildren(long parentId)
        {
            var result = new ResponseDo<List<BmsMenu>>();
            result.Data = _bmsMenuRepositories.GetChildren(parentId);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 获取岗位完整的菜单树
        /// </summary>
        public ResponseDo<List<BmsMenuTreeDo>> GetWholeTree(long parentId, List<Int64> menuIds)
        {
            var result = new ResponseDo<List<BmsMenuTreeDo>>();

            result.Data = WholeTreeGet(parentId, menuIds);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDo<List<BmsMenuTreeDo>> GetStstemTree(long parentId, List<Int64> menuIds)
        {
            var result = new ResponseDo<List<BmsMenuTreeDo>>();
            result.Data = new List<BmsMenuTreeDo>();
            var MenuList = _bmsMenuRepositories.GetAll();
            foreach (BmsMenu item in MenuList.Where(p => p.ParentId == parentId && p.IsDeleted == false && menuIds.Contains(p.Id)).OrderBy(p => p.SeqNo))
            {
                BmsMenuTreeDo tree = new BmsMenuTreeDo();
                tree.Id = item.Id;
                tree.Name = item.Name;
                tree.AuthorizeCode = item.AuthorizeCode;
                tree.Path = item.Path;
                tree.SeqNo = item.SeqNo;
                tree.ParentId = item.ParentId;
                tree.IcoName = item.IcoName;
                tree.Remark = item.Remark;
                tree.Type = item.Type;
                var menu = MenuList.Where(p => p.ParentId == item.Id && !p.IsDeleted && p.Type == BmsMenuType.Menu && menuIds.Contains(p.Id)).OrderBy(p => p.SeqNo);
                foreach (var child in menu)
                {
                    var childtree = new BmsMenuTreeDo();
                    childtree.Id = child.Id;
                    childtree.Name = child.Name;
                    childtree.AuthorizeCode = child.AuthorizeCode;
                    childtree.Path = child.Path;
                    childtree.SeqNo = child.SeqNo;
                    childtree.ParentId = child.ParentId;
                    childtree.IcoName = child.IcoName;
                    childtree.Remark = child.Remark;
                    childtree.Type = child.Type;
                    tree.Children.Add(childtree);
                }
                result.Data.Add(tree);
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public List<BmsMenuTreeDo> WholeTreeGet(Int64 parentMenuId, List<Int64> menuIds)
        {
            var list = new List<BmsMenuTreeDo>();
            var MenuList = _bmsMenuRepositories.GetAll();
            foreach (BmsMenu menu in MenuList.Where(p => p.ParentId == parentMenuId && p.IsDeleted == false).OrderBy(p => p.SeqNo))
            {
                if (menu.ParentId == parentMenuId)
                {
                    var tree = new BmsMenuTreeDo();
                    tree.Id = menu.Id;
                    tree.Name = menu.Name;
                    tree.Path = menu.Path;
                    tree.SeqNo = menu.SeqNo;
                    tree.ParentId = menu.ParentId;
                    if (menuIds.Contains(menu.Id))
                    {
                        tree.IsGranted = true;
                    }
                    tree.Children = WholeTreeGet(menu.Id, menuIds);

                    list.Add(tree);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取权限名列表
        /// </summary>
        public List<string> GetPowerNameList(List<Int64> menuIds)
        {
            var list = new List<string>();
            var MenuList = _bmsMenuRepositories.GetAll();
            foreach (BmsMenu menu in MenuList.Where(p => menuIds.Contains(p.Id)))
            {
                list.Add(menu.AuthorizeCode);
            }
            return list;
        }

        /// <summary>
        /// 获取包含父Id的菜单Id列表
        /// </summary>
        public List<long> MenuIdsGetWithParentId(List<long> list)
        {
            var result = new List<long>();
            var parentIds = RecursionGetParentMenuIds(list);
            if (parentIds != null && parentIds.Count > 0)
            {
                result = list.Union(parentIds).ToList();
            }
            else
            {
                result = list;
            }
            return result;
        }

        /// <summary>
        /// 只包含父id，不包含自己
        /// </summary>
        public List<long> RecursionGetParentMenuIds(List<long> list)
        {
            var result = new List<long>();
            var MenuList = _bmsMenuRepositories.GetAll();

            if (MenuList != null && MenuList.Count > 0)
            {
                var parentIds = MenuList.Where(p => p.ParentId != 0 && list.Contains(p.Id) && p.IsDeleted == false).Select(p => p.ParentId).Distinct().ToList();
                if (parentIds != null && parentIds.Count > 0)
                {
                    var grandParentIds = RecursionGetParentMenuIds(parentIds);
                    if (grandParentIds != null && grandParentIds.Count > 0)
                    {
                        result = parentIds.Union(grandParentIds).ToList();
                    }
                    else
                    {
                        result = parentIds;
                    }
                }
            }
            return result;
        }

        public List<long> MenuIdsGetBySuperAdmin()
        {
            var result = new List<long>();
            var MenuList = _bmsMenuRepositories.GetAll();

            if (MenuList.Count > 0)
            {
                result = MenuList.Where(p => p.IsDeleted == false).Select(p => p.Id).ToList();
            }
            return result;
        }

        public List<BmsMenuTreeDo> WholeTreeGet(Int64 parentMenuId, List<Int64> menuIds, List<Int64> removedMenuIds, List<Int64> grantedMenuIds)
        {
            var list = new List<BmsMenuTreeDo>();
            var MenuList = _bmsMenuRepositories.GetAll();
            foreach (BmsMenu menu in MenuList.Where(p => p.ParentId == parentMenuId && p.IsDeleted == false).OrderBy(p => p.SeqNo))
            {
                if (menu.ParentId == parentMenuId)
                {
                    BmsMenuTreeDo tree = new BmsMenuTreeDo();
                    tree.Id = menu.Id;
                    tree.Name = menu.Name;
                    tree.Path = menu.Path;
                    tree.SeqNo = menu.SeqNo;
                    tree.ParentId = menu.ParentId;
                    if (removedMenuIds.Contains(menu.Id) && menuIds.Contains(menu.Id))
                    {
                        //标记用户移除
                        tree.IsUserRemoved = true;
                    }
                    else
                    {
                        if (grantedMenuIds.Contains(menu.Id) && !menuIds.Contains(menu.Id))
                        {
                            tree.IsUserGranted = true;
                            tree.IsGranted = true;
                        }
                        if (menuIds.Contains(menu.Id))
                        {
                            tree.IsGranted = true;
                        }
                    }

                    tree.Children = WholeTreeGet(menu.Id, menuIds, removedMenuIds, grantedMenuIds);
                    list.Add(tree);
                }
            }
            return list;
        }
    }
}
