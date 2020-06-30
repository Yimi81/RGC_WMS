using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Product
{
    /// <summary>
    /// shane 2020/2/14
    /// </summary>
    public class ProductConfigRepository : RepositoryBase<ProductConfig>, IProductConfigRepository
    {
        private static IUnitOfWork _unitOfWork;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        private static IRepository<ProductConfigDetail> _productConfigDetail;
        /// <summary>
        /// Product字典
        /// </summary>
        public static Dictionary<long, ProductConfig> ProductConfigDict;
        public ProductConfigRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<ProductConfigDetail> productConfigDetail):base(context)
        {
             _unitOfWork = unitOfWork;
            _productConfigDetail = productConfigDetail;
        }
        public bool RefreshDict()
        {
            bool result = false;

            if (ProductConfigDict == null || ProductConfigDict.Count == 0)
            {
                ///加锁，保证同时只有一个线程访问
                lock (_locker)
                {
                    if (ProductConfigDict == null || ProductConfigDict.Count == 0)
                    {
                        ProductConfigDict = new Dictionary<long, ProductConfig>();
                        ProductConfigDict = GetSkuConfigDictFromDB();
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从数据库中获取全部
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ProductConfig> GetSkuConfigDictFromDB()
        {
            Dictionary<long, ProductConfig> dict = new Dictionary<long, ProductConfig>();

            var detailList = _productConfigDetail.TableNoTracking.ToList();
            var list = TableNoTracking.ToList();
            if (list != null && list.Any())
            {
                dict = list.Where(p => !p.IsDeleted).ToDictionary(p => p.Id);
            }
            foreach (var item in dict.Values)
            {
                var tempList = detailList.Where(p => p.ConfigId == item.Id && !p.IsDeleted).ToList();
                tempList.ForEach(p =>
                {
                    item.detailList.Add(p.Id, p);
                });
            }

            return dict;
        }
        public int Add(ProductConfig request)
        {
            RefreshDict();
            if (request.PackageId == 0)
            {
                request.Type = ConfigurationType.Package;
            }
            request.CreationTime = DateTime.Now;
            Insert(request);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {

                lock (_locker)
                {
                    if (ProductConfigDict.Keys.Contains(request.Id) == false)
                    {
                        ProductConfigDict.Add(request.Id, request);
                    }
                }
            }

            return excute;
        }

        public int AddDetail(ProductConfigDetail request)
        {
            RefreshDict();
            request.CreationTime = DateTime.Now;
            _productConfigDetail.Insert(request);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductConfigDict.Keys.Contains(request.ConfigId))
                    {
                        ProductConfigDict[request.ConfigId].detailList.Add(request.Id, request);
                    }
                }
            }

            return excute;
        }

        public int Delete(long loginId, long id)
        {
            var config = new ProductConfig
            {
                Id = id,
                IsDeleted = true,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            Update(config, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductConfigDict == null || ProductConfigDict.Count == 0)
                    {
                        RefreshDict();
                    }
                    else
                    {
                        if (ProductConfigDict.Keys.Contains(id))
                        {
                            ProductConfigDict[id].IsDeleted = true;
                            ProductConfigDict[id].DeleterUserId = loginId;
                            ProductConfigDict[id].DeletionTime = DateTime.Now;
                        }
                    }
                }
            }
            return excute;
        }

        public int DeleteDetail(long loginId,  long id, long configId)
        {
            var configDetail = new ProductConfigDetail
            {
                Id = id,
                IsDeleted = true,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            _productConfigDetail.Update(configDetail, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (!RefreshDict())
                {
                    lock (_locker)
                    {
                        if (ProductConfigDict.Keys.Contains(configId))
                        {
                            if (ProductConfigDict[configId].detailList.Keys.Contains(id))
                            {
                                ProductConfigDict[configId].detailList[id].IsDeleted = true;
                                ProductConfigDict[configId].detailList[id].DeleterUserId = loginId;
                                ProductConfigDict[configId].detailList[id].DeletionTime = DateTime.Now;
                            }
                            

                        }
                    }
                }
            }
            return excute;
        }

        public ProductConfig Get(long id)
        {
            ProductConfig obj = new ProductConfig();

            RefreshDict();

            if (ProductConfigDict.Keys.Contains(id))
            {
                obj = ProductConfigDict[id];
            }
            return obj;
        }

        public List<ProductPartsDetailEx> GetAllDetail(long id)
        {
            List<ProductPartsDetailEx> list = new List<ProductPartsDetailEx>();

            RefreshDict();
            var model = _productConfigDetail.TableNoTracking.Where(p => p.ConfigId == id && !p.IsDeleted).OrderBy(p => p.SeqNo).ToList();
            model.ForEach(p =>
            {
                var item = new ProductPartsDetailEx();
                item.EName = p.EName;
                item.CName = p.CName;
                item.ConfigDetailId = p.Id;
                item.PartDetailId = id;
                item.Id = 0;
                list.Add(item);
            });

            return list;
        }

        public List<ProductConfigTree> GetAllList(long id)
        {
            List<ProductConfigTree> list = new List<ProductConfigTree>();

            RefreshDict();

            foreach (ProductConfig SkuConfig in ProductConfigDict.Values.Where(p => p.PackageId == id && p.IsDeleted == false).OrderBy(p => p.SeqNo))
            {
                if (SkuConfig.PackageId == id)
                {
                    ProductConfigTree tree = new ProductConfigTree();
                    tree.Id = SkuConfig.Id;
                    tree.SeqNo = SkuConfig.SeqNo;
                    tree.EName = SkuConfig.EName;
                    tree.CName = SkuConfig.CName;
                    tree.Type = SkuConfig.Type;
                    //tree.child = GetAllList(SkuConfig.id);
                    list.Add(tree);
                }
            }
            return list;
        }

        public List<ProductConfig> GetChild(long id, int pageSize, int currentPage,out int count)
        {
            List<ProductConfig> result = new List<ProductConfig>();

            count = 0;
            if (ProductConfigDict == null || ProductConfigDict.Count == 0)
            {
                RefreshDict();

            }
            if (ProductConfigDict.Any())
            {

                int total = ProductConfigDict.Values.Count(p => p.IsDeleted == false && p.PackageId == id);


                var list = ProductConfigDict.Values.Where(p => p.IsDeleted == false && p.PackageId == id).OrderBy(p => p.SeqNo)
                           .ThenBy(p => p.Id).Skip(currentPage * pageSize - pageSize).Take(pageSize).ToList();

                result = list;
                count = (int)total;
            }
            return result;
        }

        public List<ProductComponentTreeDo> GetChildList(ConfigurationType type)
        {
            List<ProductComponentTreeDo> result = new List<ProductComponentTreeDo>();


            if (ProductConfigDict == null || ProductConfigDict.Count == 0)
            {
                RefreshDict();

            }
            if (ProductConfigDict.Any())
            {

                var packageIds = ProductConfigDict.Values.Where(p => p.IsDeleted == false && p.PackageId == 0).OrderBy(p => p.SeqNo).Select(p => p.Id).ToList();

                foreach (var item in packageIds)
                {
                    var childList = ProductConfigDict.Values.Where(p => p.IsDeleted == false && p.PackageId == item).OrderBy(p => p.SeqNo).ToList();
                    var temp = ProductConfigDict.Values.FirstOrDefault(p => p.IsDeleted == false && p.Id == item);
                    ProductComponentTreeDo model = new ProductComponentTreeDo();
                    model.ConfigId = item;
                    model.EName = temp.EName;
                    model.SeqNo = temp.SeqNo;
                    model.Type = type;
                    model.Id = temp.Id;
                    model.CName = temp.CName;
                    foreach (var child in childList)
                    {
                        if (child.Type == type)
                        {
                            //var cmodel = Mapper.Map<SkuPartsDetailDto>(child);
                            var cmodel = new ProductPartsDetail();
                            cmodel.EName = child.EName;
                            cmodel.CName = child.CName;
                            cmodel.SeqNo = child.SeqNo;
                            cmodel.Type = child.Type;
                            cmodel.Id = child.Id;
                            cmodel.PackageId = child.PackageId;
                            if (type == ConfigurationType.Fitting)//获取细节
                            {
                                foreach (var de in child.detailList.Values)
                                {
                                    if (!de.IsDeleted)
                                    {
                                        var detail = new ProductPartsDetailEx();
                                        detail.Id = de.Id;
                                        detail.EName = de.EName;
                                        detail.CName = de.CName;
                                        detail.PartDetailId = de.ConfigId;
                                        cmodel.detailList.Add(detail);
                                    }
                                }
                            }
                            /*if(type == ConfigurationType.Fitting&&cmodel.detailList.Any())
                                model.Children.Add(cmodel);
                            else if(type == ConfigurationType.Part)
                                model.Children.Add(cmodel);*/
                            model.Children.Add(cmodel);


                        }
                    }
                    if (model.Children.Any())
                    {
                        result.Add(model);

                    }

                }


            }
            return result;
        }
        
        public List<ProductConfigDetail> GetDetailList(long configId)
        {
            List<ProductConfigDetail> obj = new List<ProductConfigDetail>();

            RefreshDict();

            if (ProductConfigDict.Keys.Contains(configId))
            {
                foreach (var item in ProductConfigDict[configId].detailList.Values.OrderBy(p => p.SeqNo).ThenBy(p => p.Id))
                {

                    obj.Add(item);
                }
            }
            return obj;
        }

        public int SyncUpdateDetail(ProductConfigDetail request)
        {
            throw new NotImplementedException();
        }

        public int UpdateDetail(ProductConfigDetail request)
        {
            request.CreationTime = DateTime.Now;
            _productConfigDetail.Update(request);
            int excute =_unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (!RefreshDict())
                {
                    lock (_locker)
                    {
                        if (ProductConfigDict.Keys.Contains(request.ConfigId))
                        {
                            if (ProductConfigDict[request.ConfigId].detailList.Keys.Contains(request.Id))
                            {
                                ProductConfigDict[request.ConfigId].detailList[request.Id] = request;
                            }
                            else
                            {
                                ProductConfigDict[request.ConfigId].detailList.Add(request.Id, request);
                            }

                        }
                    }
                }
            }
            return excute;
        }

        public int SingleUpdate(ProductConfig request)
        {
            try
            {
                Update(request);
                int excute = _unitOfWork.SaveChanges();
                if (excute > 0)
                {
                    if (!RefreshDict())
                    {
                        lock (_locker)
                        {
                            if (ProductConfigDict.Keys.Contains(request.Id))
                            {
                                ProductConfigDict[request.Id] = request;
                            }
                        }
                    }
                }
                return excute;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public bool IfExist(ProductConfig config)
        {
            RefreshDict();
            if (ProductConfigDict.Values.Where(p => p.PackageId == config.PackageId && p.CName.ToLower() == config.CName.ToLower().Trim() && p.EName.ToLower() == config.EName.ToLower().Trim() && p.Type == config.Type).Any())
            {
                //已存在该配置
                return true;
            }
            return false;

        }
    }
}
