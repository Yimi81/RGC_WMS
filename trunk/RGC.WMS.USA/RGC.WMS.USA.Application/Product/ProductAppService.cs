using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Services.Bms;
using RGC.WMS.USA.Domain.Services.Product;
using RGC.WMS.USA.Domain.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Product
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductModifyFlowRepository _productModifyFlowRepositories;
        private readonly IProductService _productService;
        private readonly IBmsUserService _bmsUserService;
        private readonly ISystemInfoService _systemInfoService;
        private IMapper _mapper { get; }
        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public ProductAppService(
            IProductRepository productRepository,
            IProductService productService,
            IBmsUserService bmsUserService,
            ISystemInfoService systemInfoService,
            IProductModifyFlowRepository productModifyFlowRepositories,
            IMapper mapper,
            IOptions<ApplicationBaseConfig> configuration)
        {
            _productRepository = productRepository;
            _productService = productService;
            _bmsUserService = bmsUserService;
            _systemInfoService = systemInfoService;
            _productModifyFlowRepositories = productModifyFlowRepositories;
            _mapper = mapper;
            _configuration = configuration;
        }
        public ResponseDto<string> AddCategory(long productId, long categoryId)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var data = _productService.AddCategory(productId, categoryId);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> CreateProduct(long loginId, ProductEditDto entity)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查产品重复

            if (string.IsNullOrWhiteSpace(entity.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                entity.FactoryModel = entity.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(entity.FullEName))
            {
                result.Code = 2;
                result.Msg = "英文名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                entity.FullEName = entity.FullEName.Trim();
            }

            if (string.IsNullOrWhiteSpace(entity.ImageMain))
            {
                result.Code = 2;
                result.Msg = "展示图不能为空";
                result.Success = false;
                return result;
            }
            #endregion
            var model = _mapper.Map<ProductEditDo>(entity);
            model.Product = _mapper.Map<ProductInfo>(entity);
            var resp = _productService.CreateProduct(loginId, model);

            if (resp.Code == 0 && resp.Success)
            {

                result.Code = 0;
                result.Success = true;
                if (!entity.IsSync)
                {
                    var flow = _mapper.Map<ProductModifyFlow>(model.Product);
                    flow.Id = 0;
                    flow.ProductId = model.Id;
                    flow.SyncStatus = 0;
                    flow.RequestSyncTime = DateTime.Now;
                    var data = _productService.AddProductFlow(loginId, flow);
                }


            }
            return result;
        }

        public ResponseDto<string> Delete(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            if (id <= 0)
            {
                result.Msg = "请求数据异常";
                return result;
            }

            var data = _productService.Delete(loginId, id);
            if (data.Code == 0 && data.Success)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDto<string> ForceRefreshProductDict()
        {
            throw new NotImplementedException();
        }

        public ResponseDto<ProductEditDto> Get(long id)
        {
            ResponseDto<ProductEditDto> result = new ResponseDto<ProductEditDto>();
            result.Data = new ProductEditDto();
            //var data = _productRepository.GetById(id);
            var resp = _productService.Get(id);

            if (resp.Code == 0 && resp.Success)
            {
                result.Data = _mapper.Map<ProductEditDto>(resp.Data.product);
                result.Data.PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + result.Data.PrimaryImageSrc;
                if (!result.Data.FuncCategoryIds.Any())
                    result.Data.FuncCategoryIds.Add(result.Data.FuncCategoryId);
                //result.Data.FuncCategoryIds.Add(result.Data.FuncCategoryId);
                result.Data.FittingChildren = _mapper.Map<List<ProductComponentTreeDto>>(resp.Data.FittingChildren);
                result.Data.PartChildren = _mapper.Map<List<ProductComponentTreeDto>>(resp.Data.PartChildren);
                // result.Data.CustomList = _mapper.Map<List<ProductComponentTreeDto>>(resp.Data.CustomList);
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }

        public ResponsePageDto<ProductListDto> GetCategoryProductList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _productRepository.GetCategoryProductList(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _product in resp)
                {
                    var product = new ProductListDto()
                    {

                        Id = _product.Id,
                        FullEName = _product.FullEName,
                        FactoryModel = _product.FactoryModel,
                        Status = _product.Status,
                        LastModificationTimeString = _product.LastModificationTime.ToString(),
                        CreateUser = _bmsUserService.GetDetail(_product.CreatorUserId).Data?.LoginName,
                        PrimaryImageSrcFull = _product.PrimaryImageSrc
                    };

                    result.Data.Add(product);
                }
            }
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;

            }
            return result;
        }

        public ResponsePageDto<ProductListDto> GetOtherCategoryProductList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _productRepository.GetOtherCategoryProductList(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _product in resp)
                {
                    var product = new ProductListDto()
                    {

                        Id = _product.Id,
                        FullEName = _product.FullEName,
                        FactoryModel = _product.FactoryModel,
                        Status = _product.Status,
                        LastModificationTimeString = _product.LastModificationTime.ToString(),
                        CreateUser = _bmsUserService.GetDetail(_product.CreatorUserId).Data?.LoginName,
                        PrimaryImageSrcFull = _product.PrimaryImageSrc
                    };

                    result.Data.Add(product);
                }
            }
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;

            }
            return result;
        }


        public ResponseDto<ProductListSynchronizeDto> GetProductList(List<long> productIds)
        {
            ResponseDto<ProductListSynchronizeDto> result = new ResponseDto<ProductListSynchronizeDto>();
            result.Data = new ProductListSynchronizeDto();
            var resp = _productRepository.GetProductList(productIds);
            if (resp.Count > 0)
            {
                result.Data.ProductList = resp;
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponsePageDto<ProductListDto> GetPage(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _productRepository.PageQuery(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _product in resp)
                {
                    var product = new ProductListDto()
                    {

                        Id = _product.Id,
                        FullEName = _product.FullEName,
                        FactoryModel = _product.FactoryModel,
                        Status = _product.Status,
                        FuncCategory = _product.FuncCategory,
                        LastModificationTimeString = _product.LastModificationTime.ToString(),
                        PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + _product.PrimaryImageSrc,
                        CreateUser = _bmsUserService.GetDetail(_product.CreatorUserId).Data?.LoginName
                    };

                    result.Data.Add(product);
                }
            }
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;

            }
            return result;
        }

        public ResponseDto<string> Recovery(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            var data = _productService.Recovery(loginId, id);
            if (data.Code == 0 && data.Success)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponsePageDto<ProductListDto> RecycleQuery(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _productRepository.RecycleQuery(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _product in resp)
                {
                    var product = new ProductListDto()
                    {

                        Id = _product.Id,
                        FullEName = _product.FullEName,
                        FactoryModel = _product.FactoryModel,
                        Status = _product.Status,
                        FuncCategory = _product.FuncCategory,
                        LastModificationTimeString = _product.LastModificationTime.ToDateTimeZNString(),
                        PrimaryImageSrcFull = _product.PrimaryImageSrc
                    };

                    if (_product.CreatorUserId > 0)
                        product.CreateUser = _bmsUserService.GetDetail(_product.CreatorUserId).Data?.LoginName;

                    result.Data.Add(product);
                }
            }
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / searchFilter.PageSize);
                result.Page.PageSize = searchFilter.PageSize;
                result.Page.CurrentPage = searchFilter.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;

            }
            return result;
        }

        public ResponseDto<string> UpdateProduct(long loginId, ProductEditDto entity)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            if (string.IsNullOrWhiteSpace(entity.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                entity.FactoryModel = entity.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(entity.FullEName))
            {
                result.Code = 2;
                result.Msg = "英文名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                entity.FullEName = entity.FullEName.Trim();
            }
            var model = _mapper.Map<ProductEditDo>(entity);
            model.Product = _mapper.Map<ProductInfo>(entity);
            var data = _productService.UpdateProduct(loginId, model);
            if (data.Code == 0 && data.Success)
            {
                result.Code = 0;
                result.Success = true;
                if (!entity.IsSync)
                {
                    var flow = _mapper.Map<ProductModifyFlow>(model.Product);
                    flow.Id = 0;
                    flow.ProductId = model.Id;
                    flow.SyncStatus = 0;
                    flow.RequestSyncTime = DateTime.Now;
                    _productService.AddProductFlow(loginId, flow);
                }
            }
            return result;
        }

        public ResponseDto<string> UpdateStatus(long id, int status, long modifierUserId, bool isSync)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            var data = _productService.UpdateStatus(id, (ProductStatus)status, modifierUserId);
            if (data.Code == 0 && data.Success)
            {
                result.Code = 0;
                result.Success = true;
                if (!isSync)
                {

                    var flow = new ProductModifyFlow();
                    flow.Id = 0;
                    flow.ProductId = id;
                    flow.Status = (ProductStatus)status;
                    flow.SyncStatus = 0;
                    flow.RequestSyncTime = DateTime.Now;
                    _productService.AddProductFlow(modifierUserId, flow);
                }


            }
            return result;
        }
    }
}
