using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    [ApiController]
    public class ProductConfigController :  WebApiManageBase
    {
        public readonly IProductConfigAppService _productConfigService;
        public ProductConfigController(
            IProductConfigAppService productConfigService)
        {
            _productConfigService = productConfigService;
        }
        #region 产品配置
        [HttpPost("create"), Authorize(Policy = "Permission")]
        public JsonResult Create(ProductConfigEditDto config)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                result = _productConfigService.Create(config);
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpPost("create/detail"), Authorize(Policy = "Permission")]
        public JsonResult CreateDetail(ProductConfigDetail detail)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                result = _productConfigService.CreateDetail(detail);
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpGet("delete"), Authorize(Policy = "Permission")]
        public JsonResult Delete(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productConfigService.Delete(_loginId,id);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpGet("delete/detail"), Authorize(Policy = "Permission")]
        public JsonResult DeleteDetail(long id, long skuConfigId)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _productConfigService.DeleteDetail(_loginId, id, skuConfigId);

                
               
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }


        [HttpPost("update"), Authorize(Policy = "Permission")]
        public JsonResult Update(ProductConfigEditDto config)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);

                result = _productConfigService.Update(config);

                
               
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpPost("update/detail"), Authorize(Policy = "Permission")]
        public JsonResult UpdateDetail(ProductConfigDetail detail)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                 result = _productConfigService.UpdateDetail(detail);

                
               
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        //[Route("list")]
        //[HttpGet]
        //
        //public JsonResult List()
        //{
        //    ResponseDto<List<ProductConfigTree>> result = new ResponseDto<List<ProductConfigTree>>();
        //    result.Data = new List<ProductConfigTree>();
        //    try
        //    {
        //        result = _productConfigService.GetAllList(0);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Code = 2;//系统异常
        //        
        //    }

        //    return new JsonResult(result);
        //}
        [HttpGet("list"), Authorize(Policy = "Permission")]
        public JsonResult List()
        {
            ResponsePageDto<ProductConfigTree> result = new ResponsePageDto<ProductConfigTree>();
            result.Data = new List<ProductConfigTree>();
            try
            {
                result = _productConfigService.GetAllList(0);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }


        [HttpGet("config"), Authorize(Policy = "Permission")]      
        public JsonResult Get(long id)
        {
            ResponseDto<ProductConfig> result = new ResponseDto<ProductConfig>();
            result.Data = new ProductConfig();
            try
            {
                result = _productConfigService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpGet("child/list"), Authorize(Policy = "Permission")]       
        public JsonResult GetChild(long packageId, int pageSize, int currentPage)
        {
            ResponsePageDto<ProductConfigDto> result = new ResponsePageDto<ProductConfigDto>();
            result.Data = new List<ProductConfigDto>();
            try
            {
                result = _productConfigService.GetChild(packageId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }
        #endregion


    }
}