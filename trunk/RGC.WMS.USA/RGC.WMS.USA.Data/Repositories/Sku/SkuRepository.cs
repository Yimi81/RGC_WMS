using HuigeTec.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Data.Repositories.Product;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Data.Repositories.Sku
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    public class SkuRepository : RepositoryBase<SkuInfo>, ISkuRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        private static IRepository<SkuPackageDetail> _skuPackageDetailRepository;
        private static IRepository<SkuPartsDetail> _skuPartsDetailRepository;
        private static IRepository<ProductCategory> _productCategoryRepository;
        private static IRepository<SkuPartsDetailEx> _skuPartsDetailExRepository;
        private static IRepository<SkuInfoCustom> _skuInfoCustomRepository;
        private static IRepository<SkuConfiguration> _skuConfigurationRepository;

        /// <summary>
        /// Product字典
        /// </summary>
        public static Dictionary<long, SkuInfo> SkuDict;

        public SkuRepository(DbContext context, IUnitOfWork unitOfWork,
            IRepository<SkuPackageDetail> skuPackageDetailRepository,
            IRepository<SkuPartsDetail> skuPartsDetailRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<SkuPartsDetailEx> skuPartsDetailExRepository,
            IRepository<SkuInfoCustom> skuInfoCustomRepository,
            IRepository<SkuConfiguration> skuConfigurationRepository,
            IOptions<DominBaseConfig> configuration
            ) : base(context)
        {
            _unitOfWork = unitOfWork;
            _skuPackageDetailRepository = skuPackageDetailRepository;
            _skuPartsDetailRepository = skuPartsDetailRepository;
            _productCategoryRepository = productCategoryRepository;
            _skuPartsDetailExRepository = skuPartsDetailExRepository;
            _skuInfoCustomRepository = skuInfoCustomRepository;
            _skuConfigurationRepository = skuConfigurationRepository;
            _configuration = configuration;
        }

        public void RefreshSkuDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                if (SkuDict == null || SkuDict.Count == 0)
                {
                    SkuDict = new Dictionary<long, SkuInfo>();
                    SkuDict = GetSkuDictFromDB();
                }
            }
        }

        /// <summary>
        /// 从数据库中获取全部
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<long, SkuInfo> GetSkuDictFromDB()
        {
            Dictionary<long, SkuInfo> dict = new Dictionary<long, SkuInfo>();

            var list = TableNoTracking.ToList();
            var packageList = _skuPackageDetailRepository.TableNoTracking.ToList();
            var partsList = _skuPartsDetailRepository.TableNoTracking.ToList();
            var customList = _skuInfoCustomRepository.TableNoTracking.ToList();
            var categoryList = _productCategoryRepository.TableNoTracking.ToList();
            var detailList = _skuPartsDetailExRepository.TableNoTracking.ToList();


            if (list != null && list.Any())
            {
                dict = list.ToDictionary(p => p.Id);

                foreach (var sku in dict.Values)
                {
                    sku.FuncCategory = categoryList.FirstOrDefault(p => p.Type == CategoryType.Func && p.Id == sku.FuncCategoryId);
                    sku.packageDict = packageList.Where(p => p.SkuId == sku.Id).ToDictionary(p => p.Id);
                    sku.partsDict = partsList.Where(p => p.SkuId == sku.Id && p.Type == ConfigurationType.Part && !p.IsDeleted).ToDictionary(p => p.Id);
                    sku.fittingDict = partsList.Where(p => p.SkuId == sku.Id && p.Type == ConfigurationType.Fitting && !p.IsDeleted).ToDictionary(p => p.Id);
                    sku.customDict = customList.Where(p => p.SkuId == sku.Id && !p.IsDeleted).ToDictionary(p => p.Id);
                    foreach (var item in sku.fittingDict.Values)
                    {
                        //item.detailList = detailList.Where(p => p.SkuId == sku.Id && p.PartDetailId==item.Id).ToDictionary(p => p.Id);
                        item.detailList = detailList.Where(p => p.SkuId == sku.Id && p.PartDetailId == item.Id && !p.IsDeleted).ToList();
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
        public SkuInfo Get(long id)
        {
            SkuInfo result = new SkuInfo();

            if (SkuDict == null || SkuDict.Count == 0)
            {
                RefreshSkuDict();
            }
            if (SkuDict.Keys.Contains(id))
            {
                result = SkuDict[id];
            }
            return result;
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
        public List<SkuInfo> PageQuery(SearchFilterDo searchFilter, out int count)
        {
            var result = new List<SkuInfo>();
            count = 0;
            if (SkuDict == null || SkuDict.Count == 0)
            {
                RefreshSkuDict();
            }
            if (SkuDict.Any())
            {
                var lstQuery = SkuDict.Select(x => x.Value).Where(x => !x.IsDeleted);
                if (searchFilter.ProductId > 0)
                {
                    var sSkuName = string.Empty;
                    var tProduct = ProductRepository.ProductDict.Values.FirstOrDefault(x => x.Id == searchFilter.ProductId);
                    if (tProduct != null)
                        sSkuName = tProduct.SKU;
                    lstQuery = lstQuery.Where(x => x.SKU == sSkuName);
                }

                if (searchFilter.CategoryId > 0)
                    lstQuery = lstQuery.Where(x => x.FuncCategoryId == searchFilter.CategoryId);

                searchFilter.SearchKey = searchFilter.SearchKey.ToEmpty().ToLower();
                if (!string.IsNullOrEmpty(searchFilter.SearchKey))
                    lstQuery = lstQuery.Where(p => (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                int total = lstQuery.Count();

                var list = lstQuery
                           .OrderByDescending(p => p.CreationTime)
                           .Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize)
                           .Take(searchFilter.PageSize).ToList();

                result = list;
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
        public List<SkuInfo> RecycleQuery(SearchFilterDo searchFilter, out int count)
        {
            List<SkuInfo> result = new List<SkuInfo>();
            if (SkuDict == null || SkuDict.Count == 0)
            {
                RefreshSkuDict();
            }
            count = 0;

            if (SkuDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                else
                {
                    searchFilter.SearchKey = searchFilter.SearchKey.ToLower().Trim();
                }

                int total = SkuDict.Values.Count(p => p.IsDeleted == true
                                             && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                var list = SkuDict.Values.Where(p => p.IsDeleted == true
                                            && (p.FullEName.ToLower().Contains(searchFilter.SearchKey) || (p.FuncCategory != null && p.FuncCategory.EName.ToLower().Contains(searchFilter.SearchKey)) || (p.FactoryModel != null && p.FactoryModel.ToLower().Contains(searchFilter.SearchKey)) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))))
                          .OrderByDescending(p => p.CreationTime).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

                result = list;
                count = (int)total;

            }
            return result;
        }

        /// <summary>
        /// 某分类下product列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="key"></param>
        /// <param name="searchFilter.PageSize"></param>
        /// <param name="searchFilter.CurrentPage"></param>
        /// <returns></returns>
        public List<SkuInfo> GetCategorySkuList(SearchFilterDo searchFilter, out int count)
        {
            List<SkuInfo> result = new List<SkuInfo>();
            count = 0;
            if (SkuDict == null || SkuDict.Count == 0)
            {
                RefreshSkuDict();
            }

            if (SkuDict.Any())
            {
                if (searchFilter.SearchKey == null)
                {
                    searchFilter.SearchKey = "";
                }
                int total = SkuDict.Values.Count(p => p.IsDeleted == false && p.FuncCategoryId == searchFilter.CategoryId && (p.FullEName.Contains(searchFilter.SearchKey) || (p.FactoryModel != null && p.FactoryModel == searchFilter.SearchKey) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey))));

                var list = SkuDict.Values.Where(p => p.IsDeleted == false && p.FuncCategoryId == searchFilter.CategoryId && (p.FullEName.Contains(searchFilter.SearchKey) || (p.FactoryModel != null && p.FactoryModel == searchFilter.SearchKey) || (p.FullCName != null && p.FullCName.Contains(searchFilter.SearchKey)))).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

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
        public List<SkuInfo> GetOtherCategorySkuList(SearchFilterDo searchFilter, out int count)
        {
            List<SkuInfo> result = new List<SkuInfo>();
            count = 0;
            if (SkuDict == null || SkuDict.Count == 0)
            {
                RefreshSkuDict();
            }
            var key = searchFilter.SearchKey;
            var categoryId = searchFilter.CategoryId;
            if (SkuDict.Any())
            {
                if (key == null)
                {
                    key = "";
                }
                int total = SkuDict.Values.Count(p => p.IsDeleted == false && p.FuncCategoryId != categoryId && (p.FullEName.Contains(key) || (p.FactoryModel != null && p.FactoryModel == key) || (p.FullCName != null && p.FullCName.Contains(key))));

                var list = SkuDict.Values.Where(p => p.IsDeleted == false && p.FuncCategoryId != categoryId && (p.FullEName.Contains(key) || (p.FactoryModel != null && p.FactoryModel == key) || (p.FullCName != null && p.FullCName.Contains(key)))).Skip(searchFilter.CurrentPage * searchFilter.PageSize - searchFilter.PageSize).Take(searchFilter.PageSize).ToList();

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

        /// <summary>
        /// 获取全部SKU
        /// </summary>
        public List<SkuInfo> GetSkuInfoDict()
        {
            RefreshSkuDict();
            return SkuDict.Select(x => x.Value).ToList();
        }
    }
}
