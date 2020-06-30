using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Entities.Product;
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
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        private static IUnitOfWork _unitOfWork;
        private readonly IOptions<DominBaseConfig> _configuration;
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// Product字典
        /// </summary>
        public static Dictionary<long, ProductCategory> ProductCategoryDict;

        public ProductCategoryRepository(DbContext context, IUnitOfWork unitOfWork,
            IOptions<DominBaseConfig> configuration) : base(context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public bool RefreshDict()
        {
            bool result = false;

            if (ProductCategoryDict == null || ProductCategoryDict.Count == 0)
            {
                ///加锁，保证同时只有一个线程访问
                lock (_locker)
                {
                    if (ProductCategoryDict == null || ProductCategoryDict.Count == 0)
                    {
                        ProductCategoryDict = new Dictionary<long, ProductCategory>();
                        ProductCategoryDict = GetProductCategoryDictFromDB();
                        result = true;
                    }
                }
            }

            return result;
        }

        public void ForceRefreshDict()
        {
            ///加锁，保证同时只有一个线程访问
            lock (_locker)
            {
                ProductCategoryDict = new Dictionary<long, ProductCategory>();
                ProductCategoryDict = GetProductCategoryDictFromDB();
            }
        }

        public Dictionary<long, ProductCategory> GetProductCategoryDictFromDB()
        {
            Dictionary<long, ProductCategory> dict = new Dictionary<long, ProductCategory>();

            var list = TableNoTracking.ToList();

            if (list != null && list.Any())
            {
                dict = list.ToDictionary(p => p.Id);
            }

            return dict;
        }


        public int Add(ProductCategory request)
        {
            RefreshDict();
            Insert(request);
            int excute = _unitOfWork.SaveChanges();
            if (excute > 0)
            {
                lock (_locker)
                {
                    if (ProductCategoryDict.Keys.Contains(request.Id) == false)
                    {
                        request.ImageSrcFull = _configuration.Value.ImgSiteRootAddress+ request.ImageSrc;
                        ProductCategoryDict.Add(request.Id, request);
                    }
                }
            }

            return excute;
        }

        public int Delete(long loginId, long id)
        {
            var product = new ProductCategory
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
                    if (ProductCategoryDict == null || ProductCategoryDict.Count == 0)
                    {
                        RefreshDict();
                    }
                    else
                    {
                        if (ProductCategoryDict.Keys.Contains(id))
                        {
                            ProductCategoryDict[id].IsDeleted = true;
                            ProductCategoryDict[id].DeleterUserId = loginId;
                            ProductCategoryDict[id].DeletionTime = DateTime.Now;
                        }
                    }
                }
            }
            return excute;
        }

        public ProductCategory Get(long id)
        {
            ProductCategory result = new ProductCategory();

            if (ProductCategoryDict == null || ProductCategoryDict.Count == 0)
            {
                RefreshDict();
            }
            if (ProductCategoryDict.Keys.Contains(id))
            {
                result = ProductCategoryDict[id];
            }
            return result;
        }
        public List<ProductCategory> GetAllList()
        {
            List<ProductCategory> result = new List<ProductCategory>();
            RefreshDict();
            result=ProductCategoryDict.Values.ToList();
            return result;
        }
        public List<ProductCategoryTree> GetAllTree(long id, CategoryType type)
        {
            List<ProductCategoryTree> result = new List<ProductCategoryTree>();

            RefreshDict();

            foreach (ProductCategory category in ProductCategoryDict.Values.Where(p => p.ParentId == id && p.IsDeleted == false).OrderBy(p => p.SeqNo))
            {
                if (category.ParentId == id && category.Type == type)
                {
                    ProductCategoryTree tree = new ProductCategoryTree();
                    tree.Id = category.Id;
                    tree.IsShow = category.IsShow;
                    tree.Type = category.Type;
                    tree.Code = category.Code;
                    tree.EName = category.EName;
                    tree.CName = category.CName;
                    tree.SeqNo = category.SeqNo;
                    tree.ParentId = category.ParentId;
                    tree.ImageSrc = category.ImageSrc;
                    tree.ImageSrcFull = string.IsNullOrWhiteSpace(category.ImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + category.ImageSrc;
                    tree.Children = GetAllTree(category.Id, type);

                    result.Add(tree);
                }
            }
            return result;
        }

        public List<ProductCategoryCascader> GetCategoryCascader(long id)
        {
            RefreshDict();

            var result = ProductCategoryDict.Values.Where(c => c.ParentId == id && c.IsDeleted == false).OrderBy(c => c.SeqNo)
               .Select(item => new ProductCategoryCascader
               {
                   label = item.EName,
                   value = item.Id,
                   imageSrc = string.IsNullOrEmpty(item.ImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + item.ImageSrc
               }).ToList();

            if (result.Any())
            {
                foreach (var model in result)
                {
                    var children = GetCategoryCascader(model.value);
                    if (children.Any())
                    {
                        model.children = children;
                    }
                    else
                    {
                        model.children = new List<ProductCategoryCascader>();
                    }
                }
                return result;
            }
            else
            {
                return new List<ProductCategoryCascader>();
            }
        }

       public int SingleUpdate(ProductCategory request)
        {
            Update(request);
            int excute =_unitOfWork.SaveChanges();
            if (excute > 0)
            {
                if (!RefreshDict())
                {
                    lock (_locker)
                    {
                        if (ProductCategoryDict.Keys.Contains(request.Id))
                        {
                            request.ImageSrcFull = _configuration.Value.ImgSiteRootAddress + request.ImageSrc;
                            ProductCategoryDict[request.Id] = request;
                        }
                    }
                }
            }
            return excute;
        }
    }
}
