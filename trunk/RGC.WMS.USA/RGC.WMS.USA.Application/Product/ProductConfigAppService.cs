using AutoMapper;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Product
{
    /// <summary>
    /// shane 2020/2/14
    /// </summary>
    public class ProductConfigAppService : IProductConfigAppService
    {
        public readonly IProductConfigService _productConfigService;
        public readonly IProductConfigRepository _productConfigRepository;
        private IMapper _mapper { get; }

        public ProductConfigAppService(IProductConfigService productConfigService,
            IProductConfigRepository productConfigRepository, IMapper mapper)
        {
            _productConfigService = productConfigService;
            _productConfigRepository = productConfigRepository;
            _mapper = mapper;

        }
        public ResponseDto<string> Create(ProductConfigEditDto config)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var model = _mapper.Map<ProductConfig>(config);
            var existResp = _productConfigService.IfExistConfig(model);
            if (existResp.Success && existResp.Data)
            {
                result.Code = 1;
                result.Msg = "已存在该配置";
                result.Success = false;
                return result;
            }
            var excuteResp = _productConfigService.Create(model);
            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;

        }

        public ResponseDto<string> CreateDetail(ProductConfigDetail config)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (string.IsNullOrWhiteSpace(config.EName))
            {
                result.Code = 1;
                result.Msg = "请填写英文名";
                result.Success = false;
                return result;
            }
            if (string.IsNullOrWhiteSpace(config.CName))
            {
                config.CName = "";
            }

            var excuteResp = _productConfigService.CreateDetail(config);
            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }

            return result;
        }

        public ResponseDto<string> Delete(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _productConfigService.Delete(loginId, id);

            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }

            return result;
        }

        public ResponseDto<string> DeleteDetail(long loginId, long id, long configId)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _productConfigService.DeleteDetail(loginId, id, configId);

            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        public ResponseDto<string> ForceRefreshSkuConfigDict()
        {
            throw new NotImplementedException();
        }

        public ResponseDto<ProductConfig> Get(long id)
        {
            ResponseDto<ProductConfig> result = new ResponseDto<ProductConfig>();
            result.Data = new ProductConfig();
            result.Data = _productConfigRepository.Get(id);

            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponsePageDto<ProductConfigTree> GetAllList(long id)
        {
            ResponsePageDto<ProductConfigTree> result = new ResponsePageDto<ProductConfigTree>();
            result.Data = new List<ProductConfigTree>();
            result.Data = _productConfigRepository.GetAllList(id);
            if (result.Data != null)
            {

            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponsePageDto<ProductConfigDto> GetChild(long id, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductConfigDto> result = new ResponsePageDto<ProductConfigDto>();
            result.Data = new List<ProductConfigDto>();
            var data = _productConfigRepository.GetChild(id, pageSize, currentPage, out int total);
            result.Data = _mapper.Map<List<ProductConfigDto>>(data);

            result.Page.TotalCount = (int)total;
            result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / pageSize);
            result.Page.PageSize = pageSize;
            result.Page.CurrentPage = currentPage;
            result.Page.CurrentCount = data.Count;
            result.Data.ForEach(p =>
                {
                    var list = _productConfigRepository.GetDetailList(p.Id).Where(m => m.IsDeleted == false).OrderBy(m => m.SeqNo).ToList();
                    p.children = list;
                });
            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDto<GetProductConfigOutput> GetChildEdit(GetProductConfigInput input)
        {
            ResponseDto<GetProductConfigOutput> result = new ResponseDto<GetProductConfigOutput>();
            result.Data = new GetProductConfigOutput();


            var ParentComponent = _productConfigRepository.GetChildList(input.Type);
            var ComponentList = new List<ProductPartsDetail>();
            foreach (var item in ParentComponent)
            {
                foreach (var k in item.Children)
                {
                    ComponentList.Add(k);
                }
            }
            ComponentList = ComponentList.Where(p => input.ComponentIdList.Contains(p.Id) && p.Type != ConfigurationType.Package).ToList();
            var packageIdList = ComponentList.Select(p => p.PackageId).Distinct().ToList();
            if (packageIdList != null && packageIdList.Any())
            {

                int i = 0;
                int j = 0;
                foreach (var packageId in packageIdList)
                {
                    var package = _productConfigRepository.Get(packageId);
                    var PartTree = new ProductComponentTreeDto();
                    var FittingTree = new ProductComponentTreeDto();

                    if (package != null && package.Id > 0)
                    {
                        var PartList = ComponentList.Where(p => p.PackageId == package.Id && p.Type == ConfigurationType.Part && p.IsDeleted == false).OrderBy(p => p.SeqNo).ToList();
                        var FittingList = ComponentList.Where(p => p.PackageId == package.Id && p.Type == ConfigurationType.Fitting && p.IsDeleted == false).OrderBy(p => p.SeqNo).ToList();

                        if (PartList != null && PartList.Any())
                        {
                            var model = input.PartChildren.SingleOrDefault(p => p.ConfigId == package.Id);//现在默认一个组件
                            PartTree.ConfigId = package.Id;
                            PartTree.Type = input.Type;
                            PartTree.CName = package.CName;
                            PartTree.SeqNo = package.SeqNo;
                            PartTree.EName = package.EName;
                            PartTree.Id = model == null ? 0 : model.Id;
                            PartTree.Index = i;
                            foreach (var part in PartList)
                            {
                                var partComponent = new ProductPartsDetailDto();

                                var partComponentTree = input.PartChildren.FirstOrDefault(p => p.ConfigId == packageId);

                                if (partComponentTree != null && partComponentTree.Children.Any())
                                {
                                    var partComponentTreeChildren = partComponentTree.Children.FirstOrDefault(p => p.ConfigId == part.Id);
                                    if (partComponentTreeChildren != null && partComponentTreeChildren.ConfigId > 0)
                                    {
                                        partComponent = partComponentTreeChildren;
                                        partComponent.Index = i;
                                    }
                                    else
                                    {
                                        var child = _productConfigRepository.Get(part.Id);
                                        partComponent.PackageId = package.Id;
                                        partComponent.CName = child.CName;
                                        partComponent.Type = child.Type;
                                        partComponent.EName = child.EName;
                                        partComponent.SeqNo = child.SeqNo;
                                        partComponent.Id = 0;
                                        partComponent.ConfigId = part.Id;
                                        partComponent.Index = i;

                                    }
                                }
                                else
                                {
                                    var child = _productConfigRepository.Get(part.Id);
                                    partComponent.PackageId = package.Id;
                                    partComponent.CName = child.CName;
                                    partComponent.Type = child.Type;
                                    partComponent.EName = child.EName;
                                    partComponent.SeqNo = child.SeqNo;
                                    partComponent.Id = 0;
                                    partComponent.ConfigId = part.Id;
                                    partComponent.Index = i;


                                }
                                PartTree.Children.Add(partComponent);
                            }
                            result.Data.PartChildren.Add(PartTree);
                            i++;

                        }
                        if (FittingList != null && FittingList.Any())
                        {
                            var model = input.FittingChildren.SingleOrDefault(p => p.ConfigId == package.Id);//现在默认一个组件
                            FittingTree.ConfigId = package.Id;
                            FittingTree.Type = input.Type;
                            FittingTree.CName = package.CName;
                            FittingTree.SeqNo = package.SeqNo;
                            FittingTree.EName = package.EName;
                            FittingTree.Id = model == null ? 0 : model.Id;
                            FittingTree.Index = j;
                            foreach (var Fitting in FittingList)
                            {
                                var FittingComponent = new ProductPartsDetailDto();

                                var FittingComponentTree = input.FittingChildren.FirstOrDefault(p => p.ConfigId == packageId);

                                if (FittingComponentTree != null && FittingComponentTree.Children.Any())
                                {
                                    var FittingComponentTreeChildren = FittingComponentTree.Children.FirstOrDefault(p => p.ConfigId == Fitting.Id);
                                    if (FittingComponentTreeChildren != null && FittingComponentTreeChildren.ConfigId > 0)
                                    {

                                        FittingComponent = FittingComponentTreeChildren;
                                        FittingComponent.Index = j;
                                    }
                                    else
                                    {
                                        var child = _productConfigRepository.Get(Fitting.Id);
                                        FittingComponent.PackageId = package.Id;
                                        FittingComponent.CName = child.CName;
                                        FittingComponent.Type = child.Type;
                                        FittingComponent.EName = child.EName;
                                        FittingComponent.SeqNo = child.SeqNo;
                                        FittingComponent.Id = 0;
                                        FittingComponent.ConfigId = Fitting.Id;
                                        FittingComponent.Index = j;

                                        foreach (var item in child.detailList.Values)
                                        {
                                            var ex = new ProductPartsDetailEx()
                                            {
                                                EName = item.EName,
                                                CName = item.CName,
                                                SeqNo = item.SeqNo,
                                                ConfigDetailId = item.Id,
                                                PartDetailId = item.Id,
                                                Id = 0,
                                            };
                                            FittingComponent.detailList.Add(ex);
                                        }
                                    }
                                }
                                else
                                {
                                    var child = _productConfigRepository.Get(Fitting.Id);
                                    FittingComponent.PackageId = package.Id;
                                    FittingComponent.CName = child.CName;
                                    FittingComponent.Type = child.Type;
                                    FittingComponent.EName = child.EName;
                                    FittingComponent.SeqNo = child.SeqNo;
                                    FittingComponent.Id = 0;
                                    FittingComponent.ConfigId = Fitting.Id;
                                    FittingComponent.Index = j;

                                    foreach (var item in child.detailList.Values)
                                    {
                                        var ex = new ProductPartsDetailEx()
                                        {
                                            EName = item.EName,
                                            CName = item.CName,
                                            SeqNo = item.SeqNo,
                                            ConfigDetailId = item.Id,
                                            PartDetailId = item.Id,
                                            Id = 0,
                                        };
                                        FittingComponent.detailList.Add(ex);

                                    }
                                }

                                FittingTree.Children.Add(FittingComponent);
                            }
                            result.Data.FittingChildren.Add(FittingTree);
                            j++;

                        }
                    }

                }
            }



            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponsePageDto<ProductComponentTreeDto> GetChildList(ConfigurationType type)
        {
            ResponsePageDto<ProductComponentTreeDto> result = new ResponsePageDto<ProductComponentTreeDto>();
            result.Data = new List<ProductComponentTreeDto>();
            var query = _productConfigRepository.GetChildList(type);
            result.Data = _mapper.Map<List<ProductComponentTreeDto>>(query);
            if (result.Data != null)
            {

            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponsePageDto<ProductPartsDetailEx> GetFittingDetail(long id)
        {
            ResponsePageDto<ProductPartsDetailEx> result = new ResponsePageDto<ProductPartsDetailEx>();
            result.Data = new List<ProductPartsDetailEx>();
            result.Data = _productConfigRepository.GetAllDetail(id);
            if (result.Data != null)
            {

            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> Update(ProductConfigEditDto config)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var model = _mapper.Map<ProductConfig>(config);
            var existResp = _productConfigService.IfExistConfig(model);
            if (existResp.Success && existResp.Data)
            {
                result.Code = 1;
                result.Msg = "已存在该配置";
                result.Success = false;
                return result;
            }
            var excuteResp = _productConfigService.Update(model);
            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }

        public ResponseDto<string> UpdateDetail(ProductConfigDetail detail)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (string.IsNullOrWhiteSpace(detail.EName))
            {
                result.Code = 1;
                result.Msg = "请填写英文名";
                result.Success = false;
                return result;
            }
            if (string.IsNullOrWhiteSpace(detail.CName))
            {
                detail.CName = "";
            }
            var excuteResp = _productConfigService.UpdateDetail(detail);
            if (excuteResp.Success && excuteResp.Code == 0)
            {
                //_productConfigService.SyncUpdateDetail(detail);

                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Msg = "操作失败";
            }
            return result;
        }
    }
}
