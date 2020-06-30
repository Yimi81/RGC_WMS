using AutoMapper;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Do;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using RGC.WMS.USA.Domain.Repositories.Product;
using RGC.WMS.USA.Domain.Services.Product;
using RGC.WMS.USA.Domain.Services.System;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Product
{
    /// <summary>
    /// shane 2020/2/14
    /// </summary>
    public class ProductCategoryAppService : IProductCategoryAppService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISystemInfoService _systemInfoService;

        private IMapper _mapper { get; }

        public ProductCategoryAppService(IProductCategoryRepository productCategoryRepository,
            ISystemInfoService systemInfoService,
            IProductCategoryService productCategoryService, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _systemInfoService = systemInfoService;
            _productCategoryService = productCategoryService;
            _mapper = mapper;
        }
        /// <summary>
        /// shane 2020/02/13
        /// </summary>
        /// <param name="category"></param>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public ResponseDto<string> Create(ProductCategoryEditDto category, long loginId)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 校验
            category.ImageSrc = category.ImageSrc ?? "";

            if (string.IsNullOrWhiteSpace(category.EName))
            {
                result.Code = 2;
                result.Msg = "英文名为空";
                result.Success = false;
                return result;
            }
            else
            {
                category.EName = category.EName.Trim();
            }
            //if (DbProductCategory.ProductCategoryDict.Values.Count(p => p.IsShow == true && p.ParentId == 0) >= 8 && category.IsShow == true)
            //{
            //    result.Code = 1;
            //    result.Msg = "前台分类最多显示8个";
            //    result.Success = false;
            //    return result;
            //}
            #endregion

            var obj = new ProductCategoryCreateOrUpdateDo()
            {
                byteStr = category.byteStr,
                CName = category.CName,
                EName = category.EName,
                SeqNo = category.SeqNo,
                Code = category.Code,
                ImageSrc = category.ImageSrc,
                IsShow = category.IsShow,
                ParentId = category.ParentId,
                Type = category.Type,
            };
            var resp = _productCategoryService.Create(obj, loginId);

            if (resp.Code == 0 && resp.Success)
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
        /// <summary>
        /// shane 2020/02/13
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDto<string> Delete(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var resp = _productCategoryService.Delete(loginId, id);

            if (resp.Code == 0 && resp.Success)
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
        /// <summary>
        /// shane 2020/02/13
        /// </summary>
        /// <returns></returns>
        public ResponseDto<string> ForceRefreshCategoryDict()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// shane 2020/02/13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDto<ProductCategoryDto> Get(long id)
        {
            ResponseDto<ProductCategoryDto> result = new ResponseDto<ProductCategoryDto>();
            result.Data = new ProductCategoryDto();
            var data = _productCategoryRepository.Get(id);
            if (data.Id > 0)
            {
                result.Data = new ProductCategoryDto()
                {
                    Id = data.Id,
                    byteStr = data.byteStr,
                    CName = data.CName,
                    EName = data.EName,
                    SeqNo = data.SeqNo,
                    Code = data.Code,
                    ImageSrc = data.ImageSrc,
                    ImageSrcFull = data.ImageSrcFull,
                    IsShow = data.IsShow,
                    ParentId = data.ParentId,
                    Type = data.Type,
                };
                result.Code = 0;
                result.Success = true;
            }


            return result;
        }

        /// <summary>
        /// 获取所有分类
        /// shane 2020/02/13
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ResponsePageDto<ProductCategoryTree> GetAllList(CategoryType type)
        {
            ResponsePageDto<ProductCategoryTree> result = new ResponsePageDto<ProductCategoryTree>();
            result.Data = new List<ProductCategoryTree>();
            var data = _productCategoryRepository.GetAllTree(0, type);
            if (data.Count > 0)
            {
                result.Data = data;
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }
        /// <summary>
        /// 获取分类级联
        /// shane 2020/02/13
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponsePageDto<ProductCategoryCascader> GetCategoryCascader(long id)
        {
            ResponsePageDto<ProductCategoryCascader> result = new ResponsePageDto<ProductCategoryCascader>();
            result.Data = new List<ProductCategoryCascader>();
            var data = _productCategoryRepository.GetCategoryCascader(id);
            if (data.Count > 0)
            {
                result.Data = data;
                result.Code = 0;
                result.Success = true;
            }

            return result;
        }
        /// <summary>
        /// 分类更新
        /// shane 2020/02/13
        /// </summary>
        /// <returns></returns>
        public ResponseDto<string> Update(ProductCategoryEditDto category, long loginId)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 校验
            category.ImageSrc = category.ImageSrc ?? "";

            if (string.IsNullOrWhiteSpace(category.EName))
            {
                result.Code = 2;
                result.Msg = "英文名为空";
                result.Success = false;
                return result;
            }
            else
            {
                category.EName = category.EName.Trim();
            }
            //if (DbProductCategory.ProductCategoryDict.Values.Count(p => p.IsShow == true && p.ParentId == 0 && p.Id != category.Id) >= 8 && category.IsShow == true)
            //{
            //    result.code = 1;
            //    result.msg = "前台分类最多显示8个";
            //    result.success = false;
            //    return result;
            //}
            #endregion

            var obj = new ProductCategoryCreateOrUpdateDo()
            {
                Id = category.Id,
                byteStr = category.byteStr,
                CName = category.CName,
                EName = category.EName,
                SeqNo = category.SeqNo,
                Code = category.Code,
                ImageSrc = category.ImageSrc,
                IsShow = category.IsShow,
                ParentId = category.ParentId,
                Type = category.Type,

            };
            var resp = _productCategoryService.Update(obj, loginId);

            if (resp.Code == 0 && resp.Success)
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

        #region 同步

        /// <summary>
        /// 分类同步
        /// shane 2020/02/21
        /// </summary>
        /// <returns></returns>
        public ResponseDto<string> SyncProductCategory()
        {

            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            var resp = _productCategoryRepository.GetAllList();
            if (resp.Count > 0)
            {
                try
                {
                    var data = JsonConvert.SerializeObject(resp);
                    var systemIpAddress = _systemInfoService.GetSystemDetail(3).Data.IPAddress;
                    string url = "http://" + systemIpAddress + "/rest/manageproduct/categories/synchronize";
                    string token = HttpHelper.Get("http://" + systemIpAddress + "/rest/manageproduct/gettoken", "userName=user&&password=111");
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        var respStr = HttpHelper.PostJsonAuth(data, url, $"Bearer {token}");
                        result = JsonConvert.DeserializeObject<ResponseDto<string>>(respStr);
                    }
                }
                catch (Exception ex)
                {

                }
                

            }
            return result;
        }
        #endregion
    }
}
