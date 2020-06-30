using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Product;
using RGC.WMS.USA.Application.Product.Dto;
using RGC.WMS.USA.Domain.Entities.Product;
using RGC.WMS.USA.Domain.Entities.Product.Enum;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// 产品分类
    /// shane 2020/02/13
    /// </summary>
    [ApiController]
    public class CategoryController : WebApiManageBase
    {
        private readonly IProductCategoryAppService _productCategoryAppService;

        public CategoryController(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        [HttpPost("product/create"), Authorize(Policy = "Permission")]
        public JsonResult Create(ProductCategoryEditDto category)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                result = _productCategoryAppService.Create(category, loginId);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("product/update"), Authorize(Policy = "Permission")]
        public JsonResult Delete(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);

                result = _productCategoryAppService.Delete(loginId, id);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpPost("product/update"), Authorize(Policy = "Permission")]
        public JsonResult Update(ProductCategoryEditDto category)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                result = _productCategoryAppService.Update(category, loginId);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("product/list"), Authorize(Policy = "Permission")]
        public JsonResult List(CategoryType type)
        {
            return base.catchError<ProductCategoryTree>(
                   delegate (ResponsePageDto<ProductCategoryTree> result)
                   {
                       result = _productCategoryAppService.GetAllList(type);
                       return result;
                   });
        }

        /// <summary>
        /// 产品编辑页获取分类级联
        /// </summary>
        /// <returns></returns>
        [HttpGet("product/cascader"), Authorize(Policy = "Permission")]
        public JsonResult ProductCascader()
        {
            return base.catchError<ProductCategoryCascader>(
                   delegate (ResponsePageDto<ProductCategoryCascader> result)
                   {
                       result.Data = new List<ProductCategoryCascader>();
                       result = _productCategoryAppService.GetCategoryCascader(0);
                       return result;
                   });
        }

        [HttpGet("product/category"), Authorize(Policy = "Permission")]
        public JsonResult Get(long id)
        {
            ResponseDto<ProductCategoryDto> result = new ResponseDto<ProductCategoryDto>();
            result.Data = new ProductCategoryDto();
            try
            {
                result = _productCategoryAppService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }
    }
}