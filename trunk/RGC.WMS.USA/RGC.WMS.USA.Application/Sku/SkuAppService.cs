using AutoMapper;
using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Sku;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Repositories.Sku;
using RGC.WMS.USA.Domain.Services.Bms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Sku
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    public class SkuAppService : ISkuAppService
    {
        private readonly ISkuRepository _skuRepository;
        private readonly IBmsUserService _bmsUserService;
        private readonly ISkuCostRepository _skuCostRepository;
        private readonly ISkuCostBatchRepository _skuCostBatchRepository;
        private readonly ISkuStockRepository _skuStockRepository;
        private readonly IProductRepository _productRepository;
        private IMapper _mapper { get; }
        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public SkuAppService(
            ISkuRepository skuRepository,
            IBmsUserService bmsUserService,
            ISkuCostRepository skuCostRepository,
            ISkuCostBatchRepository skuCostBatchRepository,
            ISkuStockRepository skuStockRepository,
            IProductRepository productRepository,
            IMapper mapper,
            IOptions<ApplicationBaseConfig> configuration)
        {
            _skuRepository = skuRepository;
            _bmsUserService = bmsUserService;
            _skuCostRepository = skuCostRepository;
            _skuCostBatchRepository = skuCostBatchRepository;
            _skuStockRepository = skuStockRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _configuration = configuration;
        }


        public ResponseDto<string> ForceRefreshSkuDict()
        {
            throw new NotImplementedException();
        }

        public ResponsePageDto<SkuInfoDto> GetCategorySkuList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuInfoDto> result = new ResponsePageDto<SkuInfoDto>();
            result.Data = new List<SkuInfoDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _skuRepository.GetCategorySkuList(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _sku in resp)
                {
                    var product = new SkuInfoDto()
                    {

                        Id = _sku.Id,
                        FullEName = _sku.FullEName,
                        FactoryModel = _sku.FactoryModel,
                        Status = _sku.Status,
                        LastModificationTimeString = _sku.LastModificationTime.ToString(),
                        CreateUser = _bmsUserService.GetDetail(_sku.CreatorUserId).Data?.LoginName,
                        PrimaryImageSrcFull = _sku.PrimaryImageSrc
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

        public ResponsePageDto<SkuInfoDto> GetOtherCategorySkuList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuInfoDto> result = new ResponsePageDto<SkuInfoDto>();
            result.Data = new List<SkuInfoDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _skuRepository.GetOtherCategorySkuList(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _sku in resp)
                {
                    var product = new SkuInfoDto()
                    {

                        Id = _sku.Id,
                        FullEName = _sku.FullEName,
                        FactoryModel = _sku.FactoryModel,
                        Status = _sku.Status,
                        LastModificationTimeString = _sku.LastModificationTime.ToString(),
                        CreateUser = _bmsUserService.GetDetail(_sku.CreatorUserId).Data?.LoginName,
                        PrimaryImageSrcFull = _sku.PrimaryImageSrc
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

        public ResponsePageDto<SkuSearchOutput> GetPage(string key, long productId, long categoryId, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<SkuSearchOutput>();
            result.Data = new List<SkuSearchOutput>();
            var searchFilter = new SearchFilterDo()
            {
                ProductId = productId,
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };

            var lstProduct = _productRepository.GetList();
            var resp = _skuRepository.PageQuery(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _sku in resp)
                {
                    var tSKU = new SkuSearchOutput()
                    {
                        Id = _sku.Id,
                        CName = _sku.CName,
                        FullCName = _sku.FullCName,
                        EName = _sku.EName,
                        FullEName = _sku.FullEName,
                        Status = _sku.Status,
                        FactoryModel = _sku.FactoryModel,
                        BulletPoint = _sku.BulletPoint,
                        ModifyTimeString = _sku.LastModificationTime.ToDateTimeZNString(),
                        CreationTimeString = _sku.CreationTime.ToDateTimeZNString()
                    };

                    var tProduct = lstProduct.FirstOrDefault(x => x.SKU == _sku.SKU);
                    if (tProduct != null)
                        tSKU.ProductId = tProduct.Id;

                    if (_sku.FuncCategory != null)
                    {
                        tSKU.FuncCategory = new Product.Dto.ProductCategorySearchOutput
                        {
                            CName = _sku.FuncCategory.CName,
                            EName = _sku.FuncCategory.EName
                        };
                    }
                    else
                        tSKU.FuncCategory = new Product.Dto.ProductCategorySearchOutput();

                    if (!string.IsNullOrEmpty(_sku.PrimaryImageSrc))
                        tSKU.PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + _sku.PrimaryImageSrc;

                    if (_sku.CreatorUserId > 0)
                        tSKU.CreateUser = _bmsUserService.GetDetail(_sku.CreatorUserId).Data?.LoginName;

                    result.Data.Add(tSKU);
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

        public ResponsePageDto<SkuInfoDto> RecycleQuery(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuInfoDto> result = new ResponsePageDto<SkuInfoDto>();
            result.Data = new List<SkuInfoDto>();
            var searchFilter = new SearchFilterDo()
            {
                CategoryId = categoryId,
                SearchKey = key,
                PageSize = pageSize,
                CurrentPage = currentPage
            };
            var resp = _skuRepository.RecycleQuery(searchFilter, out int count);

            if (resp.Count > 0)
            {
                foreach (var _sku in resp)
                {
                    var sku = new SkuInfoDto()
                    {

                        Id = _sku.Id,
                        FullEName = _sku.FullEName,
                        FactoryModel = _sku.FactoryModel,
                        Status = _sku.Status,
                        FuncCategory = _sku.FuncCategory,
                        LastModificationTimeString = _sku.LastModificationTime.ToDateTimeZNString(),
                        PrimaryImageSrcFull = _sku.PrimaryImageSrc
                    };

                    if (_sku.CreatorUserId > 0)
                        sku.CreateUser = _bmsUserService.GetDetail(_sku.CreatorUserId).Data?.LoginName;

                    result.Data.Add(sku);
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

        public ResponseDto<SkuInfoDto> GetDetailAndParts(long skuId)
        {
            ResponseDto<SkuInfoDto> result = new ResponseDto<SkuInfoDto>();
            result.Data = new SkuInfoDto();

            var sku = _skuRepository.Get(skuId);

            result.Data = _mapper.Map<SkuInfoDto>(sku);
            result.Data.PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + result.Data.PrimaryImageSrc;

            var partIds = sku.partsDict.Values.Select(p => p.PackageId).Distinct().ToList();
            var packages = sku.packageDict.Values.ToList();
            foreach (var part in partIds)
            {
                var package = packages.FirstOrDefault(p => p.Id == part);
                var model = new SkuComponentTreeDto();
                //model.PackageId = part;
                model.EName = package.EName;
                model.SeqNo = package.SeqNo;
                model.CName = package.CName;
                var childList = sku.partsDict.Values.Where(p => p.PackageId == part).ToList();
                foreach (var child in childList)
                {
                    model.Children.Add(_mapper.Map<SkuPartsDetailDto>(child));
                }
                result.Data.PartChildren.Add(model);
            }

            var fittingIds = sku.fittingDict.Values.Select(p => p.PackageId).Distinct().ToList();
            var intList = new List<int>();
            var table1 = new List<SkuPartsDetailDto>();
            var table2 = new List<SkuPartsDetailDto>();
            foreach (var fitting in fittingIds)
            {
                int rows = 0;
                var package = packages.FirstOrDefault(p => p.Id == fitting);
                var model = new SkuComponentTreeDto();
                //model.PackageId = fitting;
                model.EName = package.EName;
                model.SeqNo = package.SeqNo;
                model.CName = package.CName;
                var childList = sku.fittingDict.Values.Where(p => p.PackageId == fitting).ToList();
                foreach (var child in childList)
                {

                    var t = _mapper.Map<SkuPartsDetailDto>(child);
                    if (child.detailList.Count > 0)
                    {
                        rows += child.detailList.Count;
                    }
                    else
                    {
                        rows++;
                    }

                    //foreach (var te in child.detailList)
                    //{
                    //    rows++;
                    //    t.detailList.Add(te);
                    //}

                    model.Children.Add(t);
                }
                intList.Add(rows);
                result.Data.FittingChildren.Add(model);
            }
            #region 左右table 按组件不分开的原则平均算法 
            //var sum = intList.Sum();
            //var half = sum / 2;
            //var start = 0;
            //result.data.FittingChildrenLeft = new List<SkuComponentTreeDto>();
            //result.data.FittingChildrenRight = new List<SkuComponentTreeDto>();
            //foreach (var item in result.data.FittingChildren)
            //{
            //    if (start <= half)
            //    {
            //        result.data.FittingChildrenLeft.Add(item);
            //    }
            //    else
            //    {
            //        result.data.FittingChildrenRight.Add(item);
            //    }
            //    foreach (var innerItem in item.Children)
            //    {
            //        if (innerItem.detailList.Count == 0)
            //        {
            //            start++;
            //        }
            //        else
            //        {
            //            start += innerItem.detailList.Count;
            //        }
            //    }
            //}
            #endregion

            #region 左右table 炉体在左 其他在右
            result.Data.FittingChildrenLeft = new List<SkuComponentTreeDto>();
            result.Data.FittingChildrenRight = new List<SkuComponentTreeDto>();
            foreach (var item in result.Data.FittingChildren)
            {
                if (item.EName.ToLower().Trim() == "main body" || item.EName.ToLower().Trim().Contains("main body"))
                {
                    result.Data.FittingChildrenLeft.Add(item);
                }
                else
                {
                    result.Data.FittingChildrenRight.Add(item);
                }
            }

            #endregion
            sku.customDict.Values.ToList().ForEach(model =>
            {
                result.Data.CustomList.Add(model);
            });

            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;

            }
            return result;
        }

        public ResponseDto<SkuInfoDto> Get(long id)
        {
            ResponseDto<SkuInfoDto> result = new ResponseDto<SkuInfoDto>();
            result.Data = new SkuInfoDto();

            var sku = _skuRepository.Get(id);

            result.Data = _mapper.Map<SkuInfoDto>(sku);
            result.Data.PrimaryImageSrcFull = _configuration.Value.ImgSiteRootAddress + result.Data.PrimaryImageSrc;
            var packageList = sku.packageDict.Values;
            var partIds = sku.partsDict.Values.Where(p => !p.IsDeleted).Select(p => p.PackageId).Distinct().ToList();
            result.Data.FuncCategoryIds.Add(result.Data.FuncCategoryId);
            foreach (var part in partIds)
            {
                var temp = new SkuPackageDetail();
                temp = packageList.FirstOrDefault(p => p.Id == part);
                if (temp != null)
                {
                    var model = new SkuComponentTreeDto();
                    model.ConfigId = temp.ConfigId;
                    model.EName = temp.EName;
                    model.Id = temp.Id;
                    model.Type = temp.Type;
                    model.CName = temp.CName;
                    model.SeqNo = temp.SeqNo;
                    sku.partsDict.Values.Where(p => !p.IsDeleted).ToList().ForEach(item =>
                    {
                        if (item.PackageId == part)
                            model.Children.Add(_mapper.Map<SkuPartsDetailDto>(item));
                    });
                    result.Data.PartChildren.Add(model);
                }
            }

            var fittingIds = sku.fittingDict.Values.Where(p => !p.IsDeleted).Select(p => p.PackageId).Distinct().ToList();
            foreach (var fitting in fittingIds)
            {
                var temp = new SkuPackageDetail();
                temp = packageList.FirstOrDefault(p => p.Id == fitting);
                if (temp != null)
                {
                    var model = new SkuComponentTreeDto();
                    model.ConfigId = temp.ConfigId;
                    model.EName = temp.EName;
                    model.Id = temp.Id;
                    model.Type = temp.Type;
                    model.CName = temp.CName;
                    model.SeqNo = temp.SeqNo;
                    sku.fittingDict.Values.Where(p => !p.IsDeleted).ToList().ForEach(item =>
                    {
                        if (item.PackageId == fitting)
                        {
                            var detail = _mapper.Map<SkuPartsDetailDto>(item);
                            /*foreach (var t in item.detailList.Values)
                            {
                                detail.detailList.Add(t);
                            }*/
                            detail.detailList = detail.detailList.Where(p => !p.IsDeleted).ToList();
                            model.Children.Add(detail);
                        }

                    });
                    result.Data.FittingChildren.Add(model);
                }
            }

            sku.customDict.Values.ToList().ForEach(model =>
            {
                result.Data.CustomList.Add(model);
            });
            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }

        #region sku_cost

        public ResponsePageDto<SkuCost> GetSkuCostList(long loginId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuCost> result = new ResponsePageDto<SkuCost>();
            result.Data = new List<SkuCost>();
            SearchFilterDo searchFilter = new SearchFilterDo();
            searchFilter.SearchKey = key;
            searchFilter.PageSize = pageSize;
            searchFilter.CurrentPage = currentPage;
            int count = 0;
            result.Data = _skuCostRepository.GetSkuCostList(searchFilter, out count);
            if (result.Data != null && result.Data.Count > 0)
            {
                result.Data.ForEach(delegate (SkuCost tSkuCost)
                {
                    if (tSkuCost.CreatorUserId > 0)
                        tSkuCost.CreatorUser = _bmsUserService.GetDetail(tSkuCost.CreatorUserId).Data?.LoginName;
                    tSkuCost.CreationTimeString = tSkuCost.CreationTime.ToDateTimeZNString();
                    tSkuCost.ModifyTimeString = tSkuCost.LastModificationTime.ToDateTimeZNString();
                });
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

        public ResponseDto<string> CreateSkuCost(long loginId, SkuCost dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查SKU

            if (dto.SkuId == 0)
            {
                result.Code = 2;
                result.Msg = "Sku不能为空";
                result.Success = false;
                return result;
            }


            if (dto.ProductId == 0)
            {
                result.Code = 2;
                result.Msg = "参数错误，ProductId不能为0";
                result.Success = false;
                return result;
            }
            #endregion

            dto.Sku = _skuRepository.Get(dto.SkuId);
            dto.Sku.PrimaryImageSrcFull = string.IsNullOrEmpty(dto.Sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + dto.Sku.PrimaryImageSrc;

            dto.CreatorUserId = loginId;
            dto.CreationTime = DateTime.Now;
            dto.FOB = ParseHelper.Yuan2Fen(dto.FOBString);
            dto.ELC = ParseHelper.Yuan2Fen(dto.ELCString);
            dto.SeaFreight = ParseHelper.Yuan2Fen(dto.SeaFreightString);
            dto.UnloadingCharge = ParseHelper.Yuan2Fen(dto.UnloadingChargeString);
            dto.DDP = ParseHelper.Yuan2Fen(dto.DDPString);
            dto.Z5Freight = ParseHelper.Yuan2Fen(dto.Z5FreightString);
            dto.Z3Freight = ParseHelper.Yuan2Fen(dto.Z3FreightString);
            dto.Map = ParseHelper.Yuan2Fen(dto.MapString);
            dto.Msrp = ParseHelper.Yuan2Fen(dto.MsrpString);

            var batchList = new List<SkuCostBatch>();
            if (dto.IsAddBatch)
            {
                foreach (var item in dto.SkuCostBatchList)
                {
                    if (string.IsNullOrEmpty(item.BatchNo.Trim()))
                    {
                        dto.SkuCostBatchList.Remove(item);
                    }
                    item.SkuId = dto.SkuId;
                    item.ProductId = dto.ProductId;
                    item.Sku = dto.Sku;

                }
                if (dto.SkuCostBatchList.Count <= 0)
                {
                    dto.IsAddBatch = false;
                }
                else
                {
                    dto.IsAddBatch = true;
                }
            }
            var res = _skuCostRepository.Add(dto);
            if (res != null && res.Id > 0)
            {
                result.Data = "";
                result.Code = 0;
                result.Success = true;
            }


            return result;
        }

        public ResponseDto<string> CreateSkuCostDirect(long loginId, SkuCost dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            return result;
        }

        public ResponseDto<string> UpdateSkuCost(long loginId, SkuCost dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查SKU

            if (dto.SkuId == 0)
            {
                result.Code = 2;
                result.Msg = "Sku不能为空";
                result.Success = false;
                return result;
            }

            if (dto.Id == 0)
            {
                result.Code = 2;
                result.Msg = "SkuCostId不能为0";
                result.Success = false;
                return result;
            }

            #endregion

            dto.Sku = _skuRepository.Get(dto.SkuId);
            dto.Sku.PrimaryImageSrcFull = string.IsNullOrEmpty(dto.Sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + dto.Sku.PrimaryImageSrc;

            dto.LastModifierUserId = loginId;
            dto.LastModificationTime = DateTime.Now;
            dto.FOB = ParseHelper.Yuan2Fen(dto.FOBString);
            dto.ELC = ParseHelper.Yuan2Fen(dto.ELCString);
            dto.SeaFreight = ParseHelper.Yuan2Fen(dto.SeaFreightString);
            dto.UnloadingCharge = ParseHelper.Yuan2Fen(dto.UnloadingChargeString);
            dto.DDP = ParseHelper.Yuan2Fen(dto.DDPString);
            dto.Z5Freight = ParseHelper.Yuan2Fen(dto.Z5FreightString);
            dto.Z3Freight = ParseHelper.Yuan2Fen(dto.Z3FreightString);
            dto.Map = ParseHelper.Yuan2Fen(dto.MapString);
            dto.Msrp = ParseHelper.Yuan2Fen(dto.MsrpString);

            var batchList = new List<SkuCostBatch>();
            if (dto.SkuCostBatchList.Count <= 0)
            {
                dto.IsAddBatch = false;
            }
            else
            {
                dto.IsAddBatch = true;
            }
            if (dto.IsAddBatch)
            {
                foreach (var item in dto.SkuCostBatchList)
                {
                    if (string.IsNullOrEmpty(item.BatchNo.Trim()))
                    {
                        dto.SkuCostBatchList.Remove(item);
                    }
                    else
                    {
                        item.SkuId = dto.SkuId;
                        item.SkuCostId = dto.Id;
                        item.ProductId = dto.ProductId;
                        item.Sku = dto.Sku;
                    }
                }
            }
            var excute = _skuCostRepository.Update(dto, loginId);
            if (excute > 0)
            {
                result.Data = "";
                result.Code = 0;
                result.Success = true;
            }


            return result;
        }

        public ResponseDto<string> DeleteSkuCost(long loginId, long Id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查
            if (Id == 0)
            {
                result.Code = 2;
                result.Msg = "Id不能为0";
                result.Success = false;
                return result;
            }
            #endregion

            var model = _skuCostRepository.Get(Id);
            model.DeleterUserId = loginId;
            model.DeletionTime = DateTime.Now;
            model.IsDeleted = true;

            var excute = _skuCostRepository.Delete(loginId, model);
            if (excute > 0)
            {
                result.Data = "";
                result.Code = 0;
                result.Success = true;
            }


            return result;
        }

        public ResponseDto<SkuCost> GetSkuCostDetail(long loginId, long skuCostId)
        {
            ResponseDto<SkuCost> result = new ResponseDto<SkuCost>();
            result.Data = new SkuCost();

            result.Data = _skuCostRepository.Get(skuCostId);

            if (result.Data != null && result.Data.Id > 0)
            {
                result.Data.FOBString = ParseHelper.Fen2YuanString(result.Data.FOB);
                result.Data.DDPString = ParseHelper.Fen2YuanString(result.Data.DDP);
                result.Data.SeaFreightString = ParseHelper.Fen2YuanString(result.Data.SeaFreight);
                result.Data.UnloadingChargeString = ParseHelper.Fen2YuanString(result.Data.UnloadingCharge);
                result.Data.ELCString = ParseHelper.Fen2YuanString(result.Data.ELC);
                result.Data.Z3FreightString = ParseHelper.Fen2YuanString(result.Data.Z3Freight);
                result.Data.Z5FreightString = ParseHelper.Fen2YuanString(result.Data.Z5Freight);
                result.Data.MsrpString = ParseHelper.Fen2YuanString(result.Data.Msrp);
                result.Data.MapString = ParseHelper.Fen2YuanString(result.Data.Map);
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }

        public ResponseDto<string> AddSkuCostBatch(long loginId, SkuCostBatch dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查SKU

            if (dto.SkuId == 0)
            {
                result.Code = 2;
                result.Msg = "Sku不能为空";
                result.Success = false;
                return result;
            }

            if (dto.ProductId == 0)
            {
                result.Code = 2;
                result.Msg = "ProductId不能为0";
                result.Success = false;
                return result;
            }

            if (dto.SkuCostId == 0)
            {
                result.Code = 2;
                result.Msg = "SkuCostId不能为0";
                result.Success = false;
                return result;
            }

            if (string.IsNullOrEmpty(dto.BatchNo))
            {
                result.Code = 2;
                result.Msg = "批次号不能为空";
                result.Success = false;
                return result;
            }
            #endregion


            dto.Sku = _skuRepository.Get(dto.SkuId);
            dto.Sku.PrimaryImageSrcFull = string.IsNullOrEmpty(dto.Sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + dto.Sku.PrimaryImageSrc;

            dto.CreatorUserId = loginId;
            dto.CreationTime = DateTime.Now;

            var excute = _skuCostRepository.AddBatch(dto);
            if (excute > 0)
            {
                result.Data = "";
                result.Code = 0;
                result.Success = true;
            }


            return result;
        }

        public ResponseDto<string> UpdateSkuCostBatchStatus(long loginId, long costId, long batchId, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            var cost = _skuCostRepository.Get(costId);

            var excute = _skuCostRepository.UpdateBatchStatus(loginId, costId, batchId, status);
            if (excute > 0)
            {
                result.Data = "";
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponsePageDto<SkuCostBatch> GetSkuCostBatchList(long loginId, string key, string batchNo, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuCostBatch> result = new ResponsePageDto<SkuCostBatch>();
            result.Data = new List<SkuCostBatch>();
            int count = 0;
            result.Data = _skuCostRepository.GetSkuCostBatchList(key, batchNo, pageSize, currentPage, out count);
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }

        public ResponsePageDto<SkuCostBatchFilterOutput> GetSkuCostBatchList(long loginId, string searchKey, int pageSize, int currentPage)
        {
            var lstResult = _skuCostBatchRepository.GetSkuCostBatchList(searchKey, pageSize, currentPage, out int count);

            var result = new ResponsePageDto<SkuCostBatchFilterOutput>();
            result.Data = new List<SkuCostBatchFilterOutput>();
            foreach (var item in lstResult)
            {
                var tOutput = new SkuCostBatchFilterOutput
                {
                    ProductId = item.ProductId,
                    SkuId = item.SkuId,
                    SkuCostId = item.SkuCostId,
                    SkuCostBatchId = item.Id,
                    SkuStockId = 0,
                    CreationTimeString = item.CreationTimeString
                };

                tOutput.BatchNo = item.BatchNo;
                tOutput.Status = item.Status;
                tOutput.Remark = item.Remark;

                if (item.Sku != null)
                {
                    tOutput.Sku = new SkuOutput
                    {
                        CName = item.Sku.CName,
                        FullCName = item.Sku.FullCName,
                        EName = item.Sku.EName,
                        FullEName = item.Sku.FullEName,
                        Status = item.Sku.Status,
                        SKU = item.Sku.SKU,
                        FactoryModel = item.Sku.FactoryModel,
                        PrimaryImageSrcFull = item.Sku.PrimaryImageSrcFull
                    };
                }
                else
                    tOutput.Sku = new SkuOutput();
                result.Data.Add(tOutput);
            }
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }

        public ResponsePageDto<SkuCostBatch> GetSkuCostBatchList(long skuId, bool isRefreshDict, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<SkuCostBatch>();
            result.Data = new List<SkuCostBatch>();
            int count = 0;
            result.Data = _skuCostRepository.GetSkuCostBatchList(skuId, isRefreshDict, pageSize, currentPage, out count);
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
            }

            return result;
        }

        #endregion

        #region sku_stock
        public ResponsePageDto<SkuStockOutput> GetSkuStockList(long loginId, long warehouseId, string key, int pageSize, int currentPage)
        {
            var result = new ResponsePageDto<SkuStockOutput>();
            var lstSkuStock = _skuStockRepository.GetSkuStockList(warehouseId, key, pageSize, currentPage, out int count);
            result.Data = _mapper.Map<List<SkuStockOutput>>(lstSkuStock);
            result.Code = 0;
            result.Success = true;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }

        public ResponseDto<SkuStockOutput> GetSkuStock(long loginId, long skuStockId)
        {
            var result = new ResponseDto<SkuStockOutput>();
            var tSkuStock = _skuStockRepository.Get(skuStockId);
            if (tSkuStock != null && tSkuStock.Id > 0)
                result.Data = _mapper.Map<SkuStockOutput>(tSkuStock);
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> AddSkuStock(long loginId, SkuStock request)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            if (request.SkuId == 0)
            {
                result.Msg = "请选择产品";
                return result;
            }

            var sku = _skuRepository.Get(request.SkuId);
            if (sku != null && sku.Id > 0)
            {
                sku.PrimaryImageSrcFull = string.IsNullOrEmpty(sku.PrimaryImageSrc) ? "" : _configuration.Value.ImgSiteRootAddress + sku.PrimaryImageSrc;

                request.Sku = sku;
            }
            else
            {
                result.Msg = "产品不存在，请检查后重新选择";
                return result;
            }

            request.CreatorUserId = loginId;

            var excute = _skuStockRepository.Add(request);

            if (excute != null && excute.Id > 0)
            {
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }

        public ResponseDto<string> UpdateSkuStock(long loginId, SkuStock request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (request.SkuId == 0)
            {
                result.Msg = "请选择产品";
                return result;
            }
            if (request.Id == 0)
            {
                result.Msg = "Id不能为0";
                return result;
            }
            var model = _skuStockRepository.Get(request.Id);
            if (model != null && model.Id == request.Id)
            {
                model.CurrentStock = request.CurrentStock;
                model.LockStock = request.LockStock;
                model.LowStock = request.LowStock;
                model.SafeStock = request.SafeStock;
                model.PreStock = request.PreStock;
                model.OrderStock = request.OrderStock;
                model.OnWayStock = request.OnWayStock;
                model.LastModificationTime = DateTime.Now;
                model.LastModifierUserId = loginId;

                var excute = _skuStockRepository.Update(loginId, model);
                if (excute > 0)
                {
                    result.Code = 0;
                    result.Success = true;
                }
            }


            return result;
        }

        public ResponseDto<string> DeleteSkuStock(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (id == 0)
            {
                result.Msg = "Id不能为0";
                return result;
            }

            var model = _skuStockRepository.Get(id);
            if (model != null && model.Id == id)
            {
                model.IsDeleted = true;
                model.DeleterUserId = loginId;
                model.DeletionTime = DateTime.Now;
            }

            var excute = _skuStockRepository.Update(loginId, model);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }
        #endregion




    }
}
