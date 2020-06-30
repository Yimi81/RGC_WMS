using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
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
    /// shane 2020/2/12
    /// </summary>
    public class ProductRepository : RepositoryBase<ProductInfo>, IProductRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();
        private static IRepository<ProductPackageDetail> _productPackageDetailRepository;
        private static IRepository<ProductPartsDetail> _productPartsDetailRepository;
        private static IRepository<ProductCategory> _productCategoryRepository;
        private static IRepository<ProductPartsDetailEx> _productPartsDetailExRepository;
        private static IRepository<ProductModifyFlow> _productModifyFlowRepository;

        /// <summary>
        /// Product字典
        /// </summary>
        public static Dictionary<long, ProductInfo> ProductDict;

        public ProductRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<ProductPackageDetail> productPackageDetailRepository,
            IRepository<ProductPartsDetail> productPartsDetailRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<ProductPartsDetailEx> productPartsDetailExRepository,
            IRepository<ProductModifyFlow> productModifyFlowRepository,
            IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _productPackageDetailRepository = productPackageDetailRepository;
            _productPartsDetailRepository = productPartsDetailRepository;
            _productCategoryRepository = productCategoryRepository;
            _productPartsDetailExRepository = productPartsDetailExRepository;
            _productModifyFlowRepository = productModifyFlowRepository;
            _configuration = configuration;
        }


        public void RefreshProductDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (ProductDict == null || ProductDict.Count == 0)
                {
                    ProductDict = new Dictionary<long, ProductInfo>();
                    ProductDict = GetProductDictFromDB();
                }
            }
        }

        /// <summary>
        /// shane 2020/2/14 强制刷新
        /// </summary>
        public void ForceRefreshProductDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                ProductDict = new Dictionary<long, ProductInfo>();
                ProductDict = GetProductDictFromDB();
            }
        }

        /// <summary>
        /// 强制刷新后，获取产品列表
        /// </summary>
        public List<ProductInfo> GetList()
        {
            RefreshProductDict();
            return ProductDict.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// 从数据库中获取全部
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, ProductInfo> GetProductDictFromDB()
        {
            Dictionary<long, ProductInfo> dict = new Dictionary<long, ProductInfo>();

            var list = TableNoTracking.ToList();
            var packageList = _productPackageDetailRepository.TableNoTracking.ToList();
            var partsList = _productPartsDetailRepository.TableNoTracking.ToList();
            // var customList = _productInfoCustomRepository.TableNoTracking.ToList();
            var categoryList = _productCategoryRepository.TableNoTracking.ToList();
            var detailList = _productPartsDetailExRepository.TableNoTracking.ToList();
            var flowList = _productModifyFlowRepository.TableNoTracking.ToList();

            if (list != null && list.Any())
            {
                dict = list.ToDictionary(p => p.Id);

                foreach (var product in dict.Values)
                {
                    product.FuncCategory = categoryList.FirstOrDefault(p => p.Type == CategoryType.Func && p.Id == product.FuncCategoryId);
                    product.PackageDict = packageList.Where(p => p.ProductId == product.Id).ToDictionary(p => p.Id);
                    product.PartsDict = partsList.Where(p => p.ProductId == product.Id && p.Type == Domain.Entities.Product.Enum.ConfigurationType.Part && !p.IsDeleted).ToDictionary(p => p.Id);
                    product.FittingDict = partsList.Where(p => p.ProductId == product.Id && p.Type == Domain.Entities.Product.Enum.ConfigurationType.Fitting && !p.IsDeleted).ToDictionary(p => p.Id);
                    product.FlowDict = flowList.Where(p => p.ProductId == product.Id).ToDictionary(p => p.Id);
                    //product.customDict = customList.Where(p => p.SkuId == product.Id && !p.IsDeleted).ToDictionary(p => p.Id);
                    foreach (var item in product.FittingDict.Values)
                    {
                        //item.detailList = detailList.Where(p => p.SkuId == sku.Id && p.PartDetailId==item.Id).ToDictionary(p => p.Id);
                        item.detailList = detailList.Where(p => p.ProductId == product.Id && p.PartDetailId == item.Id && !p.IsDeleted).ToList();
                    }
                }

            }

            return dict;
        }

        /// <summary>
        /// 获取单个实例
        /// shane 2020/2/13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductInfo Get(long id)
        {
            ProductInfo result = new ProductInfo();

            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            if (ProductDict.Keys.Contains(id))
            {
                result = ProductDict[id];
            }
            return result;
        }

        /// <summary>
        /// 获取单个实例
        /// shane 2020/2/13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModifyFlow GetFlow(long productId, long flowId)
        {
            ProductModifyFlow result = new ProductModifyFlow();

            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            if (ProductDict.Keys.Contains(productId))
            {
                if (ProductDict[productId].FlowDict.Keys.Contains(flowId))
                {
                    result = ProductDict[productId].FlowDict[flowId];
                }
                else
                {
                    result = ProductDict[productId].FlowDict.Values.Where(p => p.SyncStatus == 0 || p.SyncStatus == 2).OrderByDescending(p => p.CreationTime).FirstOrDefault();
                }
            }
            if (result != null)
                return DeepCopyByReflect(result);
            else
                return result;
        }
        /// <summary>
        /// 新增产品
        /// shane 2020/2/14
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductInfo Add(ProductEditDo model)
        {
            var categoryId = model.FuncCategoryIds[model.FuncCategoryIds.Count - 1];
            var category = _productCategoryRepository.TableNoTracking.ToList().FirstOrDefault(p => p.Id == categoryId);
            model.FuncCategoryId = categoryId;
            model.FuncCategory = category;
            model.CreationTime = DateTime.Now;
            var entity = new ProductInfo();
            using (var transaction = BeginTransaction())
            {

                try
                {
                    entity = model.Product;
                    Insert(entity);
                    int excute = _unitOfWork.SaveChanges();

                    if (excute > 0)
                    {
                        var partPackage = new List<ProductPackageDetail>();
                        foreach (var child in model.PartChildren)
                        {
                            var packageModel = new ProductPackageDetail();
                            packageModel.CName = child.CName;
                            packageModel.EName = child.EName;
                            packageModel.SeqNo = child.SeqNo;

                            packageModel.Type = child.Type;
                            packageModel.Id = child.Id;
                            packageModel.ConfigId = child.ConfigId;
                            packageModel.Index = child.Index;
                            var partList = new List<ProductPartsDetail>();
                            foreach (var item in child.Children)
                            {
                                partList.Add(item);
                            }
                            packageModel.child = partList;
                            partPackage.Add(packageModel);

                        }
                        var fittingPackage = new List<ProductPackageDetail>();
                        foreach (var child in model.FittingChildren)
                        {
                            var packageModel = new ProductPackageDetail();
                            packageModel.CName = child.CName;
                            packageModel.EName = child.EName;
                            packageModel.Type = child.Type;
                            packageModel.SeqNo = child.SeqNo;

                            packageModel.Id = child.Id;
                            packageModel.ConfigId = child.ConfigId;
                            packageModel.Index = child.Index;
                            var fittingList = new List<ProductPartsDetail>();

                            foreach (var item in child.Children)
                            {
                                fittingList.Add(item);
                            }
                            packageModel.child = fittingList;

                            fittingPackage.Add(packageModel);

                        }

                        foreach (var child in partPackage)
                        {
                            var childlList = new List<ProductPartsDetail>();
                            foreach (var de in child.child)
                            {
                                if (!string.IsNullOrWhiteSpace(de.Remarks) || !string.IsNullOrWhiteSpace(de.Surface) || !string.IsNullOrWhiteSpace(de.Material) || !string.IsNullOrWhiteSpace(de.ERemarks) || !string.IsNullOrWhiteSpace(de.EMaterial) || !string.IsNullOrWhiteSpace(de.ESurface))
                                    childlList.Add(de);
                            }
                            child.child = childlList;
                            if (child.child.Any())
                            {
                                _productPackageDetailRepository.Insert(child);
                            }
                            else
                            {
                                child.IsDeleted = true;
                            }

                        }
                        partPackage = partPackage.Where(p => !p.IsDeleted).ToList();
                        foreach (var child in fittingPackage)
                        {
                            var childlList = new List<ProductPartsDetail>();
                            foreach (var de in child.child)
                            {
                                var detailList = new List<ProductPartsDetailEx>();

                                foreach (var item in de.detailList)
                                {
                                    if (!string.IsNullOrWhiteSpace(item.Remarks) || !string.IsNullOrWhiteSpace(item.ERemarks))
                                        detailList.Add(item);
                                }
                                if (detailList.Any())
                                    childlList.Add(de);
                            }
                            child.child = childlList;
                            if (child.child.Any())
                            {
                                _productPackageDetailRepository.Insert(child);

                            }
                            else
                            {
                                child.IsDeleted = true;
                            }

                        }
                        fittingPackage = fittingPackage.Where(p => !p.IsDeleted).ToList();
                        _unitOfWork.SaveChanges();

                        foreach (var item in partPackage)
                        {
                            foreach (var child in item.child)
                            {
                                child.Id = 0;
                                child.ProductId = entity.Id;
                                child.CreationTime = DateTime.Now;
                                child.CreatorUserId = entity.CreatorUserId;
                                child.PackageId = item.Id;
                                _productPartsDetailRepository.Insert(child);

                            }

                        }

                        foreach (var item in fittingPackage)
                        {
                            foreach (var child in item.child)
                            {
                                child.Id = 0;

                                child.ProductId = entity.Id;
                                child.CreationTime = DateTime.Now;
                                child.CreatorUserId = entity.CreatorUserId;
                                child.PackageId = item.Id;
                                _productPartsDetailRepository.Insert(child);

                            }

                        }
                        _unitOfWork.SaveChanges();


                        foreach (var item in fittingPackage)
                        {
                            foreach (var fit in item.child)
                            {
                                foreach (var child in fit.detailList)
                                {
                                    child.ProductId = entity.Id;
                                    child.CreationTime = DateTime.Now;
                                    child.CreatorUserId = entity.CreatorUserId;
                                    child.PartDetailId = fit.Id;
                                    _productPartsDetailExRepository.Insert(child);

                                }

                            }
                        }

                        _unitOfWork.SaveChanges();

                        if (ProductDict == null || ProductDict.Count == 0)
                        {
                            RefreshProductDict();
                        }
                        else
                        {
                            lock (_locker)
                            {
                                if (!ProductDict.Keys.Contains(entity.Id))
                                {
                                    ProductDict.Add(entity.Id, entity);

                                    var dic = ProductDict[entity.Id];
                                    foreach (var child in partPackage)
                                    {
                                        child.ProductId = dic.Id;
                                        dic.PackageDict.Add(child.Id, child);
                                    }
                                    foreach (var child in fittingPackage)
                                    {
                                        child.ProductId = dic.Id;
                                        dic.PackageDict.Add(child.Id, child);
                                    }
                                    foreach (var item in partPackage)
                                    {
                                        foreach (var child in item.child)
                                        {
                                            dic.PartsDict.Add(child.Id, child);
                                        }


                                    }
                                    foreach (var item in fittingPackage)
                                    {
                                        foreach (var child in item.child)
                                        {
                                            dic.FittingDict.Add(child.Id, child);
                                            //foreach (var detail in child.detailList)
                                            //{
                                            //    //dic.fittingDict[child.Id].detailList.Add(detail.Id, detail);
                                            //    dic.fittingDict[child.Id].detailList.Add(detail);

                                            //}
                                        }

                                    }
                                }
                            }
                        }


                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }


            return entity;
        }

        /// <summary>
        /// 增加修改同步流水
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductModifyFlow AddFlow(ProductModifyFlow model)
        {
            model.CreationTime = DateTime.Now;
            try
            {
                _productModifyFlowRepository.Insert(model);
                int excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (ProductDict.Keys.Contains(model.ProductId))
                            {
                                ProductDict[model.ProductId].FlowDict.Add(model.Id, model);
                            }
                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }


            return model;
        }

        public ProductModifyFlow UpdateFlow(ProductModifyFlow model)
        {
            var categoryId = model.FuncCategoryIds[model.FuncCategoryIds.Count - 1];
            var category = _productCategoryRepository.TableNoTracking.ToList().FirstOrDefault(p => p.Id == categoryId);
            model.FuncCategoryId = categoryId;
            model.FuncCategory = category;
            model.LastModificationTime = DateTime.Now;

            try
            {
                _productModifyFlowRepository.Update(model, p => p.SyncStatus, p => p.LastModificationTime, p => p.LastModifierUserId, p => p.RequestSyncTime);
                int excute = _unitOfWork.SaveChanges();

                if (excute > 0)
                {

                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    else
                    {
                        lock (_locker)
                        {
                            if (ProductDict.Keys.Contains(model.ProductId))
                            {
                                if (!ProductDict[model.ProductId].FlowDict.Keys.Contains(model.Id))
                                    ProductDict[model.ProductId].FlowDict.Add(model.Id, model);
                                else
                                {
                                    ProductDict[model.ProductId].FlowDict[model.Id].SyncStatus = model.SyncStatus;
                                    ProductDict[model.ProductId].FlowDict[model.Id].RequestSyncTime = model.RequestSyncTime;
                                    ProductDict[model.ProductId].FlowDict[model.Id].LastModificationTime = model.LastModificationTime;
                                    ProductDict[model.ProductId].FlowDict[model.Id].LastModifierUserId = model.LastModifierUserId;
                                }
                            }

                        }
                    }


                }
            }
            catch (Exception ex)
            {
            }


            return model;
        }

        /// <summary>
        /// 产品更新
        /// shane 2020/2/14
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SingleUpdate(ProductEditDo entity)
        {

            int excute = 0;
            var model = new ProductInfo();
            using (var transaction = BeginTransaction())
            {
                try
                {
                    model = entity.Product;
                    Update(model);
                    var funcCategory = _productCategoryRepository.TableNoTracking.ToList().FirstOrDefault(p => p.Id == entity.FuncCategoryId);
                    entity.FuncCategory = funcCategory;

                    var partList = new List<ProductPartsDetail>();
                    var partPackage = new List<ProductPackageDetail>();
                    foreach (var child in entity.PartChildren)
                    {
                        var packageModel = new ProductPackageDetail();
                        packageModel.CName = child.CName;
                        packageModel.EName = child.EName;
                        packageModel.SeqNo = child.SeqNo;
                        packageModel.Type = child.Type;
                        packageModel.Id = child.Id;
                        packageModel.ConfigId = child.ConfigId;
                        packageModel.Index = child.Index;
                        partPackage.Add(packageModel);
                        foreach (var item in child.Children)
                        {
                            partList.Add(item);
                        }
                    }
                    var fittingList = new List<ProductPartsDetail>();
                    var fittingPackage = new List<ProductPackageDetail>();

                    foreach (var child in entity.FittingChildren)
                    {
                        var packageModel = new ProductPackageDetail();
                        packageModel.CName = child.CName;
                        packageModel.EName = child.EName;
                        packageModel.Type = child.Type;
                        packageModel.SeqNo = child.SeqNo;

                        packageModel.Id = child.Id;
                        packageModel.ConfigId = child.ConfigId;
                        packageModel.Index = child.Index;

                        fittingPackage.Add(packageModel);
                        foreach (var item in child.Children)
                        {
                            fittingList.Add(item);
                        }
                    }
                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    if (ProductDict.Keys.Contains(entity.Id))
                    {
                        var product = ProductDict[entity.Id];
                        var partsPackageIds = product.PackageDict.Values.Where(p => p.Type == Domain.Entities.Product.Enum.ConfigurationType.Part).Select(p => p.Id).ToList();
                        var modifyPartsPackageIds = entity.PartChildren.Select(p => p.Id).ToList();
                        var delPartsPackageIds = partsPackageIds.Except(modifyPartsPackageIds).ToList();
                        foreach (var item in product.PackageDict.Values)
                        {
                            if (delPartsPackageIds.Contains(item.Id))
                            {
                                item.IsDeleted = true;
                                _productPackageDetailRepository.Delete(item);
                            }
                        }
                        var fittingPackageIds = product.PackageDict.Values.Where(p => p.Type == Domain.Entities.Product.Enum.ConfigurationType.Fitting).Select(p => p.Id).ToList();
                        var modifyFittingPackageIds = entity.FittingChildren.Select(p => p.Id).ToList();
                        var delFittingPackageIds = fittingPackageIds.Except(modifyFittingPackageIds).ToList();
                        foreach (var item in product.PackageDict.Values)
                        {
                            if (delFittingPackageIds.Contains(item.Id))
                            {
                                item.IsDeleted = true;
                                _productPackageDetailRepository.Delete(item);

                            }
                        }
                        foreach (var item in fittingPackage)
                        {
                            item.ProductId = entity.Id;
                            if (item.Id == 0)
                            {
                                item.Type = Domain.Entities.Product.Enum.ConfigurationType.Fitting;
                                item.CreationTime = DateTime.Now;
                                item.CreatorUserId = entity.CreatorUserId;
                                _productPackageDetailRepository.Insert(item);

                            }
                            else
                            {
                                item.Type = Domain.Entities.Product.Enum.ConfigurationType.Fitting;
                                item.LastModificationTime = DateTime.Now;
                                item.LastModifierUserId = entity.LastModifierUserId;
                                _productPackageDetailRepository.Update(item);


                            }
                        }
                        foreach (var item in partPackage)
                        {
                            item.ProductId = entity.Id;
                            if (item.Id == 0)
                            {
                                item.Type = Domain.Entities.Product.Enum.ConfigurationType.Part;
                                item.CreationTime = DateTime.Now;
                                item.CreatorUserId = entity.CreatorUserId;
                                _productPackageDetailRepository.Insert(item);

                            }
                            else
                            {
                                item.Type = Domain.Entities.Product.Enum.ConfigurationType.Part;
                                item.LastModificationTime = DateTime.Now;
                                item.LastModifierUserId = entity.LastModifierUserId;
                                _productPackageDetailRepository.Update(item);


                            }
                        }
                        #region 自定义
                        //var customIds = product.customDict.Values.Select(p => p.Id).ToList();
                        //var modifCustomIds = entity.CustomList.Select(p => p.Id).ToList();
                        //var delCustomIds = customIds.Except(modifCustomIds).ToList();
                        //foreach (var item in product.customDict.Values)
                        //{
                        //    if (delCustomIds.Contains(item.Id))
                        //    {
                        //        item.IsDeleted = true;

                        //        Entry<ProductInfoCustom>(item).State = EntityState.Deleted;
                        //    }
                        //}

                        //foreach (var item in entity.CustomList)
                        //{
                        //    item.ProductId = entity.Id;
                        //    if (item.Id == 0)
                        //    {
                        //        item.CreationTime = DateTime.Now;
                        //        item.CreatorUserId = entity.CreatorUserId;
                        //        item.CreatorUserIP = entity.CreatorUserIP;
                        //        Entry<ProductInfoCustom>(item).State = EntityState.Added;
                        //    }
                        //    else
                        //    {
                        //        item.LastModificationTime = DateTime.Now;
                        //        item.LastModifierUserId = entity.LastModifierUserId;
                        //        item.ModifierUserIP = entity.ModifierUserIP;
                        //        Entry<ProductInfoCustom>(item).State = EntityState.Modified;

                        //    }
                        //}
                        #endregion
                        excute = _unitOfWork.SaveChanges();
                        if (excute > 0)
                        {
                            var partsIds = product.PartsDict.Values.Select(p => p.Id).ToList();
                            var modifyPartsIds = new List<long>();
                            foreach (var item in partList)
                            {
                                modifyPartsIds.Add(item.Id);
                            }
                            //var modifyPartsIds = entity.PartChildren.Select(p => p.Id).ToList();
                            var delPartsIds = partsIds.Except(modifyPartsIds).ToList();
                            foreach (var item in product.PartsDict.Values)
                            {
                                if (delPartsIds.Contains(item.Id) || delPartsPackageIds.Contains(item.PackageId))//大类删除同时移除小类
                                                                                                                 //if (delPartsIds.Contains(item.Id))
                                {
                                    item.IsDeleted = true;
                                    _productPartsDetailRepository.Delete(item);

                                }
                            }

                            foreach (var item in partList)
                            {
                                item.ProductId = entity.Id;
                                if (item.Id == 0)
                                {
                                    item.CreationTime = DateTime.Now;
                                    item.CreatorUserId = entity.CreatorUserId;
                                    item.PackageId = partPackage.Where(p => p.Index == item.Index).FirstOrDefault().Id;
                                    if (string.IsNullOrWhiteSpace(item.Remarks) && string.IsNullOrWhiteSpace(item.Material) && string.IsNullOrWhiteSpace(item.Surface) && string.IsNullOrWhiteSpace(item.ERemarks) && string.IsNullOrWhiteSpace(item.EMaterial) && string.IsNullOrWhiteSpace(item.ESurface))
                                    {
                                        item.IsDeleted = true;
                                    }
                                    else
                                    {
                                        _productPartsDetailRepository.Insert(item);


                                    }

                                }
                                else
                                {
                                    item.LastModificationTime = DateTime.Now;
                                    item.LastModifierUserId = entity.LastModifierUserId;
                                    if (string.IsNullOrWhiteSpace(item.Remarks) && string.IsNullOrWhiteSpace(item.Material) && string.IsNullOrWhiteSpace(item.Surface) && string.IsNullOrWhiteSpace(item.ERemarks) && string.IsNullOrWhiteSpace(item.EMaterial) && string.IsNullOrWhiteSpace(item.ESurface))
                                    {
                                        item.IsDeleted = true;
                                        _productPartsDetailRepository.Delete(item);


                                    }
                                    else
                                    {
                                        _productPartsDetailRepository.Update(item);


                                    }

                                }
                            }



                            var fittingIds = product.FittingDict.Values.Select(p => p.Id).ToList();
                            var modifyFittingIds = new List<long>();
                            foreach (var item in fittingList)
                            {
                                modifyFittingIds.Add(item.Id);
                            }
                            var delFittingIds = partsIds.Except(modifyFittingIds).ToList();
                            foreach (var item in product.FittingDict.Values)
                            {
                                if (delPartsIds.Contains(item.Id) || delFittingPackageIds.Contains(item.PackageId))//大类删除同时移除小类
                                                                                                                   // if (delFittingIds.Contains(item.Id))
                                {
                                    item.IsDeleted = true;
                                    _productPartsDetailRepository.Delete(item);

                                }
                            }


                            foreach (var item in fittingList)
                            {
                                item.ProductId = entity.Id;
                                if (item.Id == 0)
                                {
                                    item.CreationTime = DateTime.Now;
                                    item.CreatorUserId = entity.CreatorUserId;
                                    item.PackageId = fittingPackage.Where(p => p.Index == item.Index).FirstOrDefault().Id;
                                    if (string.IsNullOrWhiteSpace(item.Remarks) && string.IsNullOrWhiteSpace(item.ERemarks) && !item.detailList.Any())
                                    {
                                        item.IsDeleted = true;
                                    }
                                    else
                                    {
                                        var detailList = new List<ProductPartsDetailEx>();
                                        foreach (var de in item.detailList)
                                        {
                                            if (!string.IsNullOrWhiteSpace(de.Remarks) || !string.IsNullOrWhiteSpace(de.ERemarks))
                                                detailList.Add(de);
                                        }
                                        item.detailList = detailList;
                                        if (item.detailList.Any() || !string.IsNullOrWhiteSpace(item.Remarks) || !string.IsNullOrWhiteSpace(item.ERemarks))
                                        {
                                            _productPartsDetailRepository.Insert(item);

                                        }
                                        else
                                        {
                                            item.IsDeleted = true;
                                        }

                                    }

                                }
                                else
                                {
                                    item.LastModificationTime = DateTime.Now;
                                    item.LastModifierUserId = entity.LastModifierUserId;
                                    if (string.IsNullOrWhiteSpace(item.Remarks) && string.IsNullOrWhiteSpace(item.ERemarks) && !item.detailList.Any())
                                    {
                                        item.IsDeleted = true;
                                        _productPartsDetailRepository.Delete(item);


                                    }
                                    else
                                    {
                                        _productPartsDetailRepository.Update(item);


                                    }

                                    var detailIds = product.FittingDict[item.Id].detailList.Select(p => p.Id).ToList();
                                    var modifyDetailIds = item.detailList.Select(p => p.Id).ToList();
                                    var delDetailIds = detailIds.Except(modifyDetailIds).ToList();
                                    foreach (var detail in product.FittingDict[item.Id].detailList)
                                    {
                                        if (item.IsDeleted == true || delDetailIds.Contains(detail.Id) || delFittingIds.Contains(item.Id) || delFittingPackageIds.Contains(item.PackageId))
                                        {
                                            detail.IsDeleted = true;
                                            _productPartsDetailExRepository.Delete(detail);

                                        }
                                    }
                                }
                            }
                            _unitOfWork.SaveChanges();

                            foreach (var item in fittingList)
                            {
                                foreach (var detail in item.detailList)
                                {
                                    detail.PartDetailId = item.Id;
                                    if (item.Id > 0)
                                    {
                                        detail.ProductId = entity.Id;
                                        if (detail.Id == 0)
                                        {
                                            detail.CreationTime = DateTime.Now;
                                            detail.CreatorUserId = entity.CreatorUserId;
                                            if (!string.IsNullOrWhiteSpace(detail.Remarks) || !string.IsNullOrWhiteSpace(detail.ERemarks))
                                                _productPartsDetailRepository.Insert(item);

                                            else
                                            {
                                                detail.IsDeleted = true;
                                            }
                                        }
                                        else
                                        {
                                            detail.LastModificationTime = DateTime.Now;
                                            detail.LastModifierUserId = entity.LastModifierUserId;
                                            if (string.IsNullOrWhiteSpace(detail.Remarks) && string.IsNullOrWhiteSpace(detail.ERemarks))
                                            {
                                                detail.IsDeleted = true;
                                                _productPartsDetailExRepository.Delete(detail);

                                            }
                                            else
                                            {
                                                _productPartsDetailExRepository.Update(detail);

                                            }
                                        }
                                    }
                                }

                            }
                            fittingList = fittingList.Where(p => !p.IsDeleted).ToList();
                            partList = partList.Where(p => !p.IsDeleted).ToList();
                            _unitOfWork.SaveChanges();

                            if (ProductDict == null || ProductDict.Count == 0)
                            {
                                RefreshProductDict();
                            }
                            else
                            {
                                lock (_locker)
                                {
                                    if (ProductDict.Keys.Contains(entity.Id))
                                    {
                                        var flowDict = ProductDict[entity.Id].FlowDict;
                                        var category = _productCategoryRepository.TableNoTracking.ToList().FirstOrDefault(p => p.Id == entity.FuncCategoryId);
                                        entity.FuncCategory = category;
                                        foreach (var child in partPackage)
                                        {
                                            child.ProductId = model.Id;
                                            model.PackageDict.Add(child.Id, child);
                                        }
                                        foreach (var child in fittingPackage)
                                        {
                                            child.ProductId = model.Id;
                                            model.PackageDict.Add(child.Id, child);
                                        }
                                        foreach (var child in partList)
                                        {
                                            child.ProductId = model.Id;
                                            model.PartsDict.Add(child.Id, child);
                                        }
                                        foreach (var child in fittingList)
                                        {
                                            child.ProductId = model.Id;
                                            child.detailList = child.detailList.Where(p => !p.IsDeleted).ToList();
                                            model.FittingDict.Add(child.Id, child);
                                            /*foreach (var detail in child.detailDict.Values)
                                            {
                                                model.fittingDict[child.Id].detailDict.Add(detail.Id, detail);
                                            }*/
                                        }

                                        //foreach (var item in entity.CustomList)
                                        //{
                                        //    item.ProductId = model.Id;
                                        //    model.customDict.Add(item.Id, item);
                                        //}
                                        ProductDict[entity.Id] = model;
                                        ProductDict[entity.Id].FlowDict = flowDict;

                                        //var dic = ProductDict[entity.id];
                                    }
                                }
                            }

                            transaction.Commit();

                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

            return excute;
        }

        /// <summary>
        /// 删除
        /// shane 2020/2/14
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(long loginId, long id)
        {
            var product = new ProductInfo
            {
                Id = id,
                IsDeleted = true,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            Update(product, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    else
                    {
                        if (ProductDict.Keys.Contains(id))
                        {
                            ProductDict[id].IsDeleted = true;
                            ProductDict[id].DeleterUserId = loginId;
                            ProductDict[id].DeletionTime = DateTime.Now;
                        }
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 恢复
        /// shane 2020/2/14
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Recovery(long loginId, long id)
        {
            var product = new ProductInfo
            {
                Id = id,
                IsDeleted = false,
                DeleterUserId = loginId,
                DeletionTime = DateTime.Now
            };
            Update(product, x => x.IsDeleted, x => x.DeleterUserId, x => x.DeletionTime);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    else
                    {
                        if (ProductDict.Keys.Contains(id))
                        {
                            ProductDict[id].IsDeleted = false;
                            ProductDict[id].LastModifierUserId = loginId;
                            ProductDict[id].LastModificationTime = DateTime.Now;
                        }
                    }
                }
            }

            return excute;
        }


        /// <summary>
        /// 分页查询
        /// shane 2020/2/14
        /// </summary>
        /// <param name="key"></param>
        /// <param name="categoryId"></param>
        /// <param name="searchFilter.PageSize"></param>
        /// <param name="searchFilter.CurrentPage"></param>
        /// <returns></returns>
        public List<ProductInfo> PageQuery(SearchFilterDo searchFilter, out int count)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            count = 0;
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            if (ProductDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                else
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.ToLower().Trim();
                }

                int total = ProductDict.Values.Count(p => p.IsDeleted == false
                                             && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                var list = ProductDict.Values.Where(p => p.IsDeleted == false
                                            && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))))
                           .OrderByDescending(p => p.CreationTime).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;
                //Dictionary<long, SkuCost> skuCostDict;
                //Dictionary<long, SkuInfo> skuDict;
                //using (var db = new DbSku())
                //{
                //    skuDict = db.GetSkuDictFromDB();
                //}
                //using (var db = new DbSkuCost())
                //{
                //    skuCostDict = db.GetSkuCostDictFromDB();
                //}
                //using (var db = new DbBmsUser())
                //{
                //    foreach (var item in result.list)
                //    {
                //        item.PrimaryImageSrcFull = BaseConfig.ImgSiteRootAddress + item.PrimaryImageSrc;
                //        item.CreationTimeString = item.CreationTime.ToString();
                //        item.ModifyTimeString = item.LastModificationTime.ToString();
                //        var skuIds = skuDict.Values.Where(p => p.ProductId == item.Id).Select(p => p.Id).ToList();
                //        //var skuModel = skuCostDict.Values.Where(p => skuIds.Contains(p.SkuId) && p.SkuCostBatchDict.Any()).FirstOrDefault();//有批次的第一个成本价
                //        var skuModel = skuCostDict.Values.Where(p => skuIds.Contains(p.SkuId)).FirstOrDefault();//暂时默认选一个
                //        if (skuModel != null)
                //        {
                //            item.Map = skuModel.Map;
                //            item.Msrp = skuModel.Msrp;
                //            item.MapString = ParseHelper.Fen2YuanString(skuModel.Map);
                //            item.MsrpString = ParseHelper.Fen2YuanString(skuModel.Msrp);
                //        }
                //        if (item.CreatorUserId > 0)
                //        {
                //            item.CreateUser = db.Get(item.CreatorUserId).loginName;
                //        }
                //    }
                //}
                count = (int)total;
            }
            return result;
        }

        /// <summary>
        /// shane 2020/2/14
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ProductInfo> RecycleQuery(SearchFilterDo searchFilter, out int count)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            count = 0;

            if (ProductDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                else
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.ToLower().Trim();
                }

                int total = ProductDict.Values.Count(p => p.IsDeleted == true
                                             && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                var list = ProductDict.Values.Where(p => p.IsDeleted == true
                                            && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))))
                          .OrderByDescending(p => p.CreationTime).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;
                count = (int)total;

            }
            return result;
        }

        /// <summary>
        /// 根据id获取产品
        /// shane 2020/2/21
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public List<ProductInfo> GetProductList(List<long> productIds)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }

            if (ProductDict.Any())
            {
                if (productIds.Contains(-1))
                {
                    var list = ProductDict.Values.OrderByDescending(p => p.CreationTime).ToList();
                    result = list;
                }
                else
                {
                    var list = ProductDict.Values.Where(p => productIds.Contains(p.Id))
                                             .OrderByDescending(p => p.CreationTime).ToList();
                    result = list;
                }
            }
            return result;
        }

        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="LastModifierUserId"></param>
        /// <param name="modifierUserIP"></param>
        /// <returns></returns>
        public int UpdateStatus(long id, ProductStatus status, long loginId)
        {


            var product = new ProductInfo
            {
                Id = id,
                Status = status,
                LastModifierUserId = loginId,
                LastModificationTime = DateTime.Now
            };
            Update(product, x => x.Status, x => x.LastModifierUserId, x => x.LastModificationTime);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductDict == null || ProductDict.Count == 0)
                    {
                        RefreshProductDict();
                    }
                    else
                    {
                        if (ProductDict.Keys.Contains(id))
                        {
                            ProductDict[id].Status = status;
                            ProductDict[id].LastModifierUserId = loginId;
                            ProductDict[id].LastModificationTime = DateTime.Now;
                        }
                    }


                }
            }

            return excute;
        }


        #region 产品分类维护

        /// <summary>
        /// 产品分类
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int AddCategory(long productId, long categoryId)
        {
            var category = _productCategoryRepository.TableNoTracking.FirstOrDefault(p => p.Id == categoryId);
            var parentId = category.ParentId;
            List<int> categoryIds = new List<int>() { (int)categoryId };

            while (parentId > 0)
            {
                categoryIds.Add((int)parentId);
                parentId = _productCategoryRepository.TableNoTracking.FirstOrDefault(p => p.Id == parentId).ParentId;
            }
            categoryIds.Reverse();

            var product = new ProductInfo
            {
                Id = productId,
                FuncCategoryId = categoryId,
                FuncCategory = category,
            };
            Update(product, x => x.Status, x => x.LastModifierUserId, x => x.LastModificationTime);
            int excute = _unitOfWork.SaveChanges();

            if (excute > 0)
            {
                if (ProductDict == null || ProductDict.Count == 0)
                {
                    RefreshProductDict();
                }
                else
                {
                    lock (_locker)
                    {
                        if (ProductDict.Keys.Contains(productId))
                        {
                            ProductDict[productId].FuncCategoryId = categoryId;
                            ProductDict[productId].FuncCategory = _productCategoryRepository.TableNoTracking.ToList().FirstOrDefault(p => p.Id == categoryId);
                        }
                        else
                        {
                            RefreshProductDict();
                        }
                    }
                }
            }

            return excute;
        }

        /// <summary>
        /// 某分类下product列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="searchFilter.PageSize"></param>
        /// <param name="searchFilter.CurrentPage"></param>
        /// <returns></returns>
        public List<ProductInfo> GetCategoryProductList(SearchFilterDo searchFilter, out int count)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            count = 0;
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }

            if (ProductDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                int total = ProductDict.Values.Count(p => p.IsDeleted == false && p.FuncCategoryId == searchFilter.CategoryId && (p.FullEName.Contains(searchFilter.SearchKey) || (p.FactoryModel != null && p.FactoryModel == searchFilter.SearchKey) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                var list = ProductDict.Values.Where(p => p.IsDeleted == false && p.FuncCategoryId == searchFilter.CategoryId && (p.FullEName.Contains(searchFilter.SearchKey) || (p.FactoryModel != null && p.FactoryModel == searchFilter.SearchKey) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey)))).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;

                //foreach (var item in result.list)
                //{
                //    item.ProductImgList = list.Where(p => p.id == item.id).FirstOrDefault().ProductImgDict.Values.ToList();
                //    item.ProductPriceList = list.Where(p => p.id == item.id).FirstOrDefault().ProductPriceDict.Values.ToList();
                //}
                foreach (var item in result)//shane 2019/01/16
                {
                    //item.PrimaryImageSrcFull = string.IsNullOrEmpty(item.PrimaryImageSrc) ? "" : BaseConfig.ImgSiteRootAddress + item.PrimaryImageSrc;
                }

                count = (int)total;
            }
            return result;
        }

        /// <summary>
        /// 非该分类下product列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="searchFilter.PageSize"></param>
        /// <param name="searchFilter.CurrentPage"></param>
        /// <returns></returns>
        public List<ProductInfo> GetOtherCategoryProductList(SearchFilterDo searchFilter, out int count)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            count = 0;
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            var key = searchFilter.SearchKey;
            var categoryId = searchFilter.CategoryId;
            if (ProductDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                int total = ProductDict.Values.Count(p => p.IsDeleted == false && p.FuncCategoryId != categoryId && (p.FullEName.Contains(key) || (p.FactoryModel != null && p.FactoryModel == key) || (p.FullCName != null && p.FullCName.Contains(key))));

                var list = ProductDict.Values.Where(p => p.IsDeleted == false && p.FuncCategoryId != categoryId && (p.FullEName.Contains(key) || (p.FactoryModel != null && p.FactoryModel == key) || (p.FullCName != null && p.FullCName.Contains(key)))).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;

                //foreach (var item in result.list)
                //{
                //    item.ProductImgList = list.Where(p => p.id == item.id).FirstOrDefault().ProductImgDict.Values.ToList();
                //    item.ProductPriceList = list.Where(p => p.id == item.id).FirstOrDefault().ProductPriceDict.Values.ToList();
                //}
                //foreach (var item in result)//shane 2019/01/16
                //{
                //    item.PrimaryImageSrcFull = string.IsNullOrEmpty(item.PrimaryImageSrc) ? "" : BaseConfig.ImgSiteRootAddress + item.PrimaryImageSrc;
                //}

                count = (int)total;
                //result.page.totalPages = (int)Math.Ceiling((Decimal)total / searchFilter.PageSize);
                //result.page.searchFilter.PageSize = searchFilter.PageSize;
                //result.page.searchFilter.CurrentPage = searchFilter.CurrentPage;
                //result.page.currentCount = list.Count;
            }
            return result;
        }



        #endregion


        public bool IfExistProduct(string factoryModel)
        {
            if (ProductDict == null || ProductDict.Count == 0)
            {
                RefreshProductDict();
            }
            return ProductDict.Values.Where(p => p.FactoryModel == factoryModel).Any() ? true : false;
        }
    }
}
