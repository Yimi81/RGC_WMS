using HuigeTec.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Repositories.Bms;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bms
{
    public class BmsMenuRepository : RepositoryBase<BmsMenu>, IBmsMenuRepository
    {
        private static IUnitOfWork _unitOfWork;

        private static readonly object _locker = new object();

        private static Dictionary<long, BmsMenu> BmsMenuDict;

        public BmsMenuRepository(DbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }
        public void RefreshBmsMenuDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (BmsMenuDict == null || BmsMenuDict.Count == 0)
                {
                    BmsMenuDict = new Dictionary<long, BmsMenu>();
                    BmsMenuDict = GetBmsMenuDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部菜单
        /// </summary>
        public Dictionary<long, BmsMenu> GetBmsMenuDictFromDB()
        {
            var dict = new Dictionary<long, BmsMenu>();

            var list = TableNoTracking.ToList();
            if (list != null && list.Count() > 0)
            {
                dict = list.ToDictionary(p => p.Id);
            }

            return dict;
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshDict()
        {
            ///加锁，保证同时只有一个线程访问 
            lock (_locker)
            {
                BmsMenuDict = new Dictionary<Int64, BmsMenu>();
                BmsMenuDict = GetBmsMenuDictFromDB();
            }
        }

        /// <summary>
        /// 单个获取
        /// </summary>
        public BmsMenu SingleGet(long Id)
        {
            var obj = new BmsMenu();

            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            if (BmsMenuDict.Keys.Contains(Id))
            {
                obj = BmsMenuDict[Id];
            }
            obj = DeepCopyByReflect<BmsMenu>(obj);
            return obj;
        }

        /// <summary>
        /// 单个新增
        /// </summary>
        public int SingleInsert(BmsMenu request)
        {
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            if (BmsMenuDict.Values.Where(p => p.AuthorizeCode.ToLower().Trim() == request.AuthorizeCode.ToLower().Trim() && !p.IsDeleted).Any())
                throw new CustomException("权限名称重复", 301);

            if (BmsMenuDict.Values.Where(p => p.ParentId == request.ParentId && p.Name.ToLower().Trim() == request.Name.ToLower().Trim() && !p.IsDeleted).Any())
                throw new CustomException("该菜单节点下已存在此名称", 302);

            Insert(request);
            var excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                //新增成功
                if (BmsMenuDict == null || BmsMenuDict.Count == 0)
                {
                    RefreshBmsMenuDict();
                }
                lock (_locker)
                {
                    if (BmsMenuDict.Keys.Contains(request.Id) == false)
                    {
                        BmsMenuDict.Add(request.Id, request);
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 单个更新数据库
        /// </summary>
        public int SingleUpdate(BmsMenu obj)
        {
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            if (BmsMenuDict.Values.Where(p => p.Id != obj.Id && p.AuthorizeCode.ToLower().Trim() == obj.AuthorizeCode.ToLower().Trim() && !p.IsDeleted).Any())
                throw new CustomException("权限名称重复", 301);

            if (BmsMenuDict.Values.Where(p => p.Id != obj.Id && p.ParentId == obj.ParentId && p.Name.ToLower().Trim() == obj.Name.ToLower().Trim() && !p.IsDeleted).Any())
                throw new CustomException("该菜单节点下已存在此名称", 302);

            Update(obj);
            var excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (BmsMenuDict == null || BmsMenuDict.Count == 0)
                {
                    RefreshBmsMenuDict();
                }
                lock (_locker)
                {
                    if (BmsMenuDict.Keys.Contains(obj.Id))
                    {
                        BmsMenuDict[obj.Id] = obj;
                    }
                    else
                    {
                        RefreshBmsMenuDict();
                    }
                }
            }
            return excute;

        }

        /// <summary>
        /// 更新排序
        /// </summary>
        public bool SingleUpdateMenuSeqNo(long modifierUserId, List<Tuple<long, int>> seqNoList)
        {
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            var modifyTime = DateTime.Now;
            foreach (var item in seqNoList)
            {
                var menu = new BmsMenu()
                {
                    Id = item.Item1,
                    SeqNo = item.Item2,
                    LastModificationTime = modifyTime,
                    LastModifierUserId = modifierUserId
                };
                Update(menu, x => x.SeqNo, x => x.LastModifierUserId, x => x.LastModificationTime);

            }
            var excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (BmsMenuDict == null || BmsMenuDict.Count == 0)
                {
                    RefreshBmsMenuDict();
                }
                lock (_locker)
                {
                    foreach (var item in seqNoList)
                    {
                        if (BmsMenuDict.Keys.Contains(item.Item1))
                        {
                            BmsMenuDict[item.Item1].SeqNo = item.Item2;
                        }
                        else
                        {
                            RefreshBmsMenuDict();
                        }

                    }

                }
                return true;
            }
            return false;

        }

        /// <summary>
        /// 单个删除，伪删除
        /// </summary>
        public bool SingleDelete(long menuId, long loginId)
        {
            var obj = DeepCopyByReflect(BmsMenuDict[menuId]);
            obj.IsDeleted = true;
            obj.DeleterUserId = loginId;
            obj.DeletionTime = DateTime.Now;
            Update(obj);
            var excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (BmsMenuDict.Keys.Contains(menuId))
                    {
                        BmsMenuDict[menuId].IsDeleted = true;
                        BmsMenuDict[menuId].DeleterUserId = obj.DeleterUserId;
                        BmsMenuDict[menuId].DeletionTime = obj.DeletionTime;
                    }
                    else
                    {
                        RefreshBmsMenuDict();
                    }
                }
                return true;
            }

            return false;
        }

        public List<BmsMenu> GetAll()
        {
            var list = new List<BmsMenu>();
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            list = BmsMenuDict.Values.ToList();
            return list;
        }

        /// <summary>
        /// 获取完整的菜单树
        /// </summary>
        public List<BmsMenu> GetTree(long parentId)
        {
            var list = new List<BmsMenu>();
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            foreach (BmsMenu menu in BmsMenuDict.Values.Where(p => p.ParentId == parentId && !p.IsDeleted).OrderBy(p => p.SeqNo))
            {
                if (menu.ParentId == parentId)
                {
                    menu.Children = GetTree(menu.Id);
                    list.Add(menu);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取下一级
        /// </summary>
        public List<BmsMenu> GetChildren(long parentId)
        {
            var list = new List<BmsMenu>();
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            foreach (BmsMenu menu in BmsMenuDict.Values.Where(p => p.ParentId == parentId && !p.IsDeleted).OrderBy(p => p.SeqNo))
            {
                if (menu.ParentId == parentId)
                {
                    list.Add(menu);
                }
            }
            return list;
        }

        public List<BmsMenu> GetAuthorizationMenu(long systemId, List<long> menuIds)
        {
            var list = new List<BmsMenu>();
            if (BmsMenuDict == null || BmsMenuDict.Count == 0)
            {
                RefreshBmsMenuDict();
            }
            var menulist = BmsMenuDict.Values.Where(p => p.ParentId == systemId && !p.IsDeleted).OrderBy(p => p.SeqNo);

            foreach (BmsMenu menu in menulist)
            {
                if (menuIds.Contains(menu.Id))
                {
                    list.Add(menu);
                }
                list = list.Union(GetAuthorizationMenu(menu.Id, menuIds)).ToList();
            }
            return list;
        }
    }
}
