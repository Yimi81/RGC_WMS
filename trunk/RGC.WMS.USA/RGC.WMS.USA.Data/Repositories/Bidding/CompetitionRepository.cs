using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Repositories.Bidding;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Bidding
{
    /// <summary>
    /// shane 2020/03/05
    /// </summary>
    public class CompetitionRepository : RepositoryBase<Competition>,ICompetitionRepository
    {
        private static IUnitOfWork _unitOfWork;
        private static readonly object _locker = new object();
        private readonly IRepository<CompetitionDaily> _competitionDailyRepository;
        /// <summary>
        /// 在售产品字典
        /// </summary>
        public static Dictionary<long, Competition> CompetitionDict;
        public CompetitionRepository(DbContext context, 
            IUnitOfWork unitOfWork,
            IRepository<CompetitionDaily> competitionDailyRepository
            ) : base(context)
        {
            _competitionDailyRepository = competitionDailyRepository;
            _unitOfWork = unitOfWork;
        }

        public void RefreshDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (CompetitionDict == null || CompetitionDict.Count == 0)
                {
                    CompetitionDict = new Dictionary<long, Competition>();
                    CompetitionDict = GetDictFromDB();
                }
            }
        }

        /// <summary>
        /// 强制刷新
        /// </summary>
        public void ForceRefreshCompetitionDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                CompetitionDict = new Dictionary<long, Competition>();
                CompetitionDict = GetDictFromDB();
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, Competition> GetDictFromDB()
        {
            Dictionary<long, Competition> dict = new Dictionary<long, Competition>();
            try
            {
                var list = TableNoTracking.ToList();
                if (list != null && list.Any())
                {
                    dict = list.ToDictionary(p => p.Id);
                }
            }
            catch (Exception ex)
            {

            }
            return dict;
        }
        public int SingleAdd(Competition entity)
        {
            int excute = 0;
            entity.IsValid = true;
            entity.CreationTime = DateTime.Now;
            Insert(entity);
            excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (CompetitionDict == null || CompetitionDict.Count == 0)
                {
                    RefreshDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (!CompetitionDict.Keys.Contains(entity.Id))
                        {
                            CompetitionDict.Add(entity.Id, entity);
                        }
                    }
                }
            }
            return excute;
        }

        public int BatchAdd(List<CompetitionDaily> entity)
        {
            int excute = 0;
            using (var ts = BeginTransaction())
            {
                try
                {

                    foreach (var item in entity)
                    {
                        _competitionDailyRepository.Insert(item);
                    }
                    excute = _unitOfWork.SaveChanges();
                    ts.Commit();
                }
                catch (Exception ex)
                {
                    ts.Rollback();
                }
                //提交事务
            }
            return excute;
        }

        public int BatchChangeStatus(List<Competition> entity)
        {
            int excute = 0;
            using (var ts = BeginTransaction())
            {
                try
                {

                    foreach (var Competition in entity)
                    {
                        Update(Competition);
                    }
                    excute = _unitOfWork.SaveChanges();

                    if (excute > 0)
                    {
                        lock (_locker)
                        {
                            if (CompetitionDict == null || CompetitionDict.Count == 0)
                            {
                                RefreshDict();
                            }
                            else
                            {
                                foreach (var Competition in entity)
                                {
                                    if (CompetitionDict.Keys.Contains(Competition.Id))
                                    {
                                        var price = CompetitionDict[Competition.Id].RetailPrice;
                                        CompetitionDict[Competition.Id] = Competition;
                                        CompetitionDict[Competition.Id].RetailPrice = price;
                                    }
                                    else
                                    {
                                        RefreshDict();
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ts.Rollback();
                }
                //提交事务
            }


            return excute;
        }


        public int BatchUpdatePrice(List<CompetitionDaily> obj)
        {
            int excute = 0;
            using (var ts = BeginTransaction())
            {
                try
                {

                    foreach (var item in obj)
                    {
                        var competition = new Competition()
                        {
                            Id = item.CompetitionId,
                            RetailPrice = item.Price
                        };
                        Update(competition, p => p.RetailPrice);
                    }
                    excute = _unitOfWork.SaveChanges();
                    if (excute > 0)
                    {
                        if (CompetitionDict == null || CompetitionDict.Count == 0)
                        {
                            RefreshDict();
                        }
                        else
                        {
                            lock (_locker)
                            {
                                foreach (var Competition in obj)
                                {
                                    if (CompetitionDict.Keys.Contains(Competition.CompetitionId))
                                    {
                                        CompetitionDict[Competition.CompetitionId].RetailPrice = Competition.Price;
                                    }
                                }

                            }
                        }
                    }
                    ts.Commit();
                }
                catch (Exception ex)
                {
                    ts.Rollback();
                }
                //提交事务
            }


            return excute;
        }

        public List<Competition> CompetitionPageQuery(string key, long itemId, int pageSize, int currentPage, out int count)
        {
            List<Competition> result = new List<Competition>();
            count = 0;
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            if (CompetitionDict.Count > 0)
            {
                int total = 0;
                var list = new List<Competition>();

                IEnumerable<Competition> iCompetitionList;
                iCompetitionList = CompetitionDict.Values.Where(p => p.IsDeleted == false).OrderBy(p => p.ProductId).DistinctBy(p => new { p.ProductId, p.Name });
                if (itemId != 0)
                {
                    iCompetitionList = iCompetitionList.Where(p => p.ItemId == itemId);
                }
                if (!string.IsNullOrEmpty(key))
                {
                    iCompetitionList = iCompetitionList.Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                total = iCompetitionList.Count();
                list = iCompetitionList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();

                result = list;

                count = (int)total;
            }
            return result;
        }

        public List<Competition> CompetitionPlatformPageQuery(string key, long ProductId, int pageSize, int currentPage, out int count)
        {
            List<Competition> result = new List<Competition>();
            count = 0;
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            if (CompetitionDict.Count > 0)
            {
                int total = 0;
                var list = new List<Competition>();

                IEnumerable<Competition> iCompetitionList;
                iCompetitionList = CompetitionDict.Values;
                if (ProductId != 0)
                {
                    iCompetitionList = iCompetitionList.Where(p => p.ProductId == ProductId);
                }
                if (!string.IsNullOrEmpty(key))
                {
                    iCompetitionList = iCompetitionList.Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                total = iCompetitionList.Count();
                list = iCompetitionList.Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();
                result = list;
                count = (int)total;
            }
            return result;
        }


        public int SingleDelete(long loginId, long Id)
        {
            var result = new Competition()
            {
                IsDeleted = true,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            Update(result, p => p.IsDeleted, p => p.DeleterUserId, p => p.DeletionTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (CompetitionDict == null || CompetitionDict.Count == 0)
                    {
                        RefreshDict();
                    }
                    else
                    {
                        if (CompetitionDict.Keys.Contains(Id))
                        {
                            CompetitionDict[Id].IsDeleted = true;
                            CompetitionDict[Id].DeleterUserId = loginId;
                            CompetitionDict[Id].DeletionTime = DateTime.Now;
                        }
                    }
                }
            }
            return excute;
        }

        public Competition SingleGet(long Id)
        {
            Competition result = new Competition();

            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            if (CompetitionDict.Keys.Contains(Id))
            {
                result = CompetitionDict[Id];
            }
            return result;
        }

        public List<Competition> GetAll()
        {
            List<Competition> result = new List<Competition>();

            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            //var platformList = db.GetPlatformInfoDictFromDB();
            var list = CompetitionDict.Values.Where(p => p.PlatformUrl != null).ToList();
            foreach (var Competition in list)
            {

                //var model = platformList.Values.FirstOrDefault(p => p.id == Competition.PlatformId);
                //if (model != null)
                //{
                // Competition.PlatformName = model.ename;
                //Competition.SrcFull = string.IsNullOrWhiteSpace(Competition.Src) ? "" : BaseConfig.ImgSiteRootAddress + Competition.Src;
                result.Add(Competition);
                //}
            }

            return result;
        }

        public List<Competition> GetAllList()
        {
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            return CompetitionDict.Values.ToList();
        }

        public Competition GetByPlatformUniqueId(long platformId, string uniqueId)
        {
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            return CompetitionDict.Values.FirstOrDefault(p => p.PlatformId == platformId && !string.IsNullOrEmpty(p.UniqueId) && p.UniqueId == uniqueId);
        }
        public List<Competition> GetOtherPlatformCompetitionList(long PlatformId, string key, int pageSize, int currentPage, out int count)
        {
            List<Competition> result = new List<Competition>();
            count = 0;
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            if (CompetitionDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                else
                {
                    key = key.Trim().ToLower();
                }
            }
            return result;
        }

        public List<Competition> GetPlatformCompetitionList(long PlatformId, string key, long itemId, long productId, int? status, int pageSize, int currentPage, out int count)
        {
            List<Competition> result = new List<Competition>();
            count = 0;
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            if (CompetitionDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                else
                {
                    key = key.Trim();
                }

                IEnumerable<Competition> iCompetitionList;
                iCompetitionList = CompetitionDict.Values.OrderBy(p => p.ProductId).Distinct();

                if (!string.IsNullOrEmpty(key))
                {
                    iCompetitionList = iCompetitionList.Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(key.ToLower()) || !string.IsNullOrEmpty(p.FactoryModel) && p.FactoryModel.ToLower().Contains(key.ToLower()));
                }
                if (PlatformId != 0)
                {
                    iCompetitionList = iCompetitionList.Where(p => p.PlatformId == PlatformId);

                }

                else
                {
                    /*using (var db = new DbBmsUser())
                    {
                        var user = db.SingleGet(loginId);
                        if (user != null && user.userId > 0)
                        {
                            if (user.loginName.Trim().ToLower() == "admin" || user.UserRoleDict.Values.Select(p => p.roleId).Contains(1))
                            {

                            }
                            else
                            {
                                var platformIds = user.UserPlatformDict.Values.Select(p => p.platformId).ToList();
                                if (platformIds.Count == 0)
                                {
                                    return result;
                                }
                                iCompetitionList = iCompetitionList.Where(p => platformIds.Contains(p.PlatformId));
                            }

                        }
                    }*/
                }
                if (productId != 0)
                {
                    iCompetitionList = iCompetitionList.Where(p => p.ProductId == productId);

                }
                if (itemId != 0)
                {
                    iCompetitionList = iCompetitionList.Where(p => p.ItemId == itemId);

                }

                if (status.HasValue)
                {
                    var IsValid = status.Value == 0 ? true : false;
                    iCompetitionList = iCompetitionList.Where(p => p.IsValid == IsValid);

                }
                int total = iCompetitionList.Count(p => p.IsDeleted == false);

                var list = iCompetitionList.OrderByDescending(p => p.Id).Where(p => p.IsDeleted == false).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();

                result = list;
                 count = (int)total;
      
            }
            return result;
        }

        public int SingleUpdate(Competition entity)
        {
            int excute = 0;
            entity.IsValid = true;
            entity.LastModificationTime = DateTime.Now;
            Update(entity);
            excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (CompetitionDict == null || CompetitionDict.Count == 0)
                {
                    RefreshDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (CompetitionDict.Keys.Contains(entity.Id))
                        {
                            CompetitionDict[entity.Id]= entity;
                        }
                    }
                }
            }
            return excute;
        }

        public int UpdateStatus(long modifierUserId, long id, bool isValid)
        {
            int excute = 0;
            Competition entity = new Competition()
            {
                Id = id,
                IsValid = isValid,
                LastModificationTime = DateTime.Now,
                LastModifierUserId= modifierUserId

            };
            Update(entity,p=>p.IsValid, p => p.LastModificationTime, p => p.LastModifierUserId);
            excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (CompetitionDict == null || CompetitionDict.Count == 0)
                {
                    RefreshDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (CompetitionDict.Keys.Contains(entity.Id))
                        {
                            CompetitionDict[entity.Id].IsValid = entity.IsValid;
                            CompetitionDict[entity.Id].LastModifierUserId = entity.LastModifierUserId;
                            CompetitionDict[entity.Id].LastModificationTime = entity.LastModificationTime;
                        }
                    }
                }
            }
            return excute;
        }


        public List<(string value, long id)> SearchPageQuery(string key)
        {
            List<(string value, long id)> result = new List<(string value, long id)>();

            var list = new List<(string value, long id)>();
            if (CompetitionDict == null || CompetitionDict.Count == 0)
            {
                RefreshDict();
            }
            var iList = CompetitionDict.Values.DistinctBy(p => new { p.ProductId }).ToList();

            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToLower().Trim();
                iList.ForEach(model =>
                {
                    if (model.FactoryModel.ToLower().Contains(key))
                    {
                        list.Add((value: model.FactoryModel, id: model.ProductId));
                    }
                });
            }
            else
            {
                iList.ForEach(model =>
                {
                    list.Add((value: model.FactoryModel, id: model.ProductId));
                });
            }
            result = list;
            return result;
        }
    }
}
