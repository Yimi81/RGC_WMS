using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Application.Sku;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    [ApiController]
    public class ProductController : WebApiManageBase
    {
        public readonly IProductAppService _productAppService;
        public readonly IProductConfigAppService _productConfigAppService;
        public readonly ISkuAppService _skuAppService;
        public ProductController(IProductAppService productAppService,
            IProductConfigAppService productConfigService,
            ISkuAppService skuAppService)
        {
            _productAppService = productAppService;
            _productConfigAppService = productConfigService;
            _skuAppService = skuAppService;
        }
        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> Create()
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var request = await getPostData<ProductEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.CreateProduct(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delete"), Authorize(Policy = "Permission")]
        public JsonResult Delete(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.Delete(_loginId, id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 恢复产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("recovery"), Authorize(Policy = "Permission")]
        public JsonResult Recovery(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.Recovery(_loginId, id);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 更新产品主表
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        [HttpPost("update"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> Update()
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var request = await getPostData<ProductEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.UpdateProduct(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("get"), Authorize(Policy = "Permission")]
        public JsonResult EditGetModel(long id)
        {
            ResponseDto<ProductEditDto> result = new ResponseDto<ProductEditDto>();
            result.Data = new ProductEditDto();

            try
            {
                result = _productAppService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("list"), Authorize(Policy = "Permission")]
        public JsonResult GetProductList(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();

            try
            {
                result = _productAppService.GetPage(key, categoryId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 产品回收站列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("recycle"), Authorize(Policy = "Permission")]
        public JsonResult RecycleQuery(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();

            try
            {
                result = _productAppService.RecycleQuery(key, categoryId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStatus(long id, int status, bool isSync)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.UpdateStatus(id, status, _loginId, isSync);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        #region 产品分类相关
        /// <summary>
        /// 产品分类
        /// </summary>
        /// <param name="skuId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("add_category"), Authorize(Policy = "Permission")]
        public JsonResult AddCategory(long productId, long categoryId)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productAppService.AddCategory(productId, categoryId);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 某分类下产品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("category/list"), Authorize(Policy = "Permission")]
        public JsonResult GetCategoryProductList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();

            try
            {
                result = _productAppService.GetCategoryProductList(categoryId, key, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 非分类下sku列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("category/otherList"), Authorize(Policy = "Permission")]
        public JsonResult GetOtherCategoryProductList(long categoryId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductListDto> result = new ResponsePageDto<ProductListDto>();
            result.Data = new List<ProductListDto>();

            try
            {
                result = _productAppService.GetOtherCategoryProductList(categoryId, key, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }
        #endregion


        #region 产品配置
        [HttpGet("package"), Authorize(Policy = "Permission")]
        public JsonResult GetPackage()
        {
            ResponsePageDto<ProductConfigTree> result = new ResponsePageDto<ProductConfigTree>();
            result.Data = new List<ProductConfigTree>();
            try
            {
                result = _productConfigAppService.GetAllList(0);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("parts"), Authorize(Policy = "Permission")]
        public JsonResult GetParts(long packageId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductConfigDto> result = new ResponsePageDto<ProductConfigDto>();
            result.Data = new List<ProductConfigDto>();
            try
            {
                result = _productConfigAppService.GetChild(packageId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 获取部件列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("parts/list"), Authorize(Policy = "Permission")]
        public JsonResult GetPartsList()
        {
            ResponsePageDto<ProductComponentTreeDto> result = new ResponsePageDto<ProductComponentTreeDto>();
            result.Data = new List<ProductComponentTreeDto>();
            try
            {
                result = _productConfigAppService.GetChildList(ConfigurationType.Part);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 获取部件编辑对象
        /// </summary>
        /// <returns></returns>
        [HttpPost("parts/edit"), Authorize(Policy = "Permission")]
        public JsonResult GetPartsEdit(GetProductConfigInput request)
        {
            ResponseDto<GetProductConfigOutput> result = new ResponseDto<GetProductConfigOutput>();
            result.Data = new GetProductConfigOutput();
            try
            {
                result = _productConfigAppService.GetChildEdit(request);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }


        /// <summary>
        /// 获取配件列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("fitting/list"), Authorize(Policy = "Permission")]
        public JsonResult GetFittingList()
        {
            ResponsePageDto<ProductComponentTreeDto> result = new ResponsePageDto<ProductComponentTreeDto>();

            result.Data = new List<ProductComponentTreeDto>();
            try
            {
                result = _productConfigAppService.GetChildList(ConfigurationType.Fitting);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 获取配件详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("fitting/detail"), Authorize(Policy = "Permission")]
        public JsonResult GetFittingDetail(long id)
        {
            ResponsePageDto<ProductPartsDetailEx> result = new ResponsePageDto<ProductPartsDetailEx>();
            result.Data = new List<ProductPartsDetailEx>();
            try
            {
                result = _productConfigAppService.GetFittingDetail(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }
        #endregion


        [HttpGet("sku/list"), Authorize(Policy = "Permission")]
        public JsonResult GetSkuList(string key, long productId, int pageSize, int currentPage)
        {
            return base.catchError<SkuSearchOutput>(
                   delegate (ResponsePageDto<SkuSearchOutput> result)
                   {
                       result = _skuAppService.GetPage(key, productId,0, pageSize, currentPage);
                       return result;
                   });
        }
    }
}