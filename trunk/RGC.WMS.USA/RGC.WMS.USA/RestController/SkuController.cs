using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Sku;
using RGC.WMS.USA.Application.Sku.Dto;
using RGC.WMS.USA.Domain.Entities.Sku;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// shane 2020/2/12
    /// </summary>
    [ApiController]
    public class SkuController : WebApiManageBase
    {
        public readonly ISkuAppService _skuAppService;

        public SkuController(ISkuAppService skuAppService)
        {
            _skuAppService = skuAppService;

        }

        #region Sku

        [HttpGet("get")]
        public JsonResult GetDetail(long id)
        {
            ResponseDto<SkuInfoDto> result = new ResponseDto<SkuInfoDto>();
            result.Data = new SkuInfoDto();

            try
            {
                result = _skuAppService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("detail/parts")]
        public JsonResult GetDetailAndParts(long id)
        {
            ResponseDto<SkuInfoDto> result = new ResponseDto<SkuInfoDto>();
            result.Data = new SkuInfoDto();

            try
            {
                result = _skuAppService.GetDetailAndParts(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }

            return new JsonResult(result);
        }

        [HttpGet("list"), Authorize(Policy = "Permission")]
        public JsonResult SkuList(string key, long categoryId, int pageSize, int currentPage)
        {
            return base.catchError<SkuSearchOutput>(
                   delegate (ResponsePageDto<SkuSearchOutput> result)
                   {
                       result = _skuAppService.GetPage(key, 0, categoryId, pageSize, currentPage);
                       return result;
                   });
        }

        [HttpGet("recycle"), Authorize(Policy = "Permission")]
        public JsonResult RecycleQuery(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuInfoDto> result = new ResponsePageDto<SkuInfoDto>();
            result.Data = new List<SkuInfoDto>();

            try
            {
                result = _skuAppService.RecycleQuery(key, categoryId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        #endregion

        #region Sku cost

        [HttpGet("costlist/get"), Authorize(Policy = "Permission")]
        public JsonResult GetSkuCostBatchList(long id, bool isRefreshDict)
        {
            ResponsePageDto<SkuCostBatch> result = new ResponsePageDto<SkuCostBatch>();
            result.Data = new List<SkuCostBatch>();

            try
            {
                result = _skuAppService.GetSkuCostBatchList(id, isRefreshDict, 9999, 1);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }

            return new JsonResult(result);
        }

        [HttpGet("cost/list"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostList(string key, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuCost> result = new ResponsePageDto<SkuCost>();
            result.Data = new List<SkuCost>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.GetSkuCostList(_loginId, key, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }
            return new JsonResult(result);
        }

        [HttpGet("cost/detail"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostDetail(long skuCostId)
        {
            ResponseDto<SkuCost> result = new ResponseDto<SkuCost>();
            result.Data = new SkuCost();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.GetSkuCostDetail(_loginId, skuCostId);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }
            return new JsonResult(result);
        }

        [HttpPost("cost/create"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostCreate(SkuCost request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.CreateSkuCost(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpPost("cost/update"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostUpdate(SkuCost request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.UpdateSkuCost(_loginId, request);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpGet("cost/delete"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostDelete(long skuCostId)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.DeleteSkuCost(_loginId, skuCostId);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpPost("cost/batch/add"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostBatchAdd(SkuCostBatch request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.AddSkuCostBatch(_loginId, request);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpGet("cost/batch/status/update"), Authorize(Policy = "Permission")]
        public JsonResult SkuCostBatchStatusUpdate(long costId, long batchId, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.UpdateSkuCostBatchStatus(_loginId, costId, batchId, status);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        [HttpGet("cost/batch/list"), Authorize(Policy = "Permission")]
        public JsonResult GetSkuCostBatchPageList(string key, string batchNo, int pageSize, int currentPage)
        {
            ResponsePageDto<SkuCostBatch> result = new ResponsePageDto<SkuCostBatch>();
            result.Data = new List<SkuCostBatch>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.GetSkuCostBatchList(_loginId, key, batchNo, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        #endregion

        #region sku stock

        [HttpGet("stock/list"), Authorize(Policy = "Permission")]
        public JsonResult SkuStockList(string key, int pageSize, int currentPage, long warehouseId = 0)
        {
            return base.catchError<SkuStockOutput>(
                   delegate (ResponsePageDto<SkuStockOutput> result)
                   {
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out int _loginId);
                       result = _skuAppService.GetSkuStockList(_loginId, warehouseId, key, pageSize, currentPage);
                       return result;
                   });
        }

        [Route("stock/detail"), Authorize(Policy = "Permission")]
        [HttpGet]
        public JsonResult GetSkuStock(long skuStockId)
        {
            return base.catchError<SkuStockOutput>(
                   delegate (ResponseDto<SkuStockOutput> result)
                   {
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out int _loginId);
                       result = _skuAppService.GetSkuStock(_loginId, skuStockId);
                       return result;
                   });
        }

        [Route("stock/create"), Authorize(Policy = "Permission")]
        [HttpPost]
        public JsonResult CreateStock(SkuStock request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.AddSkuStock(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("stock/delete"), Authorize(Policy = "Permission")]
        [HttpGet]
        public JsonResult DeleteStock(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.DeleteSkuStock(_loginId, id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("stock/update"), Authorize(Policy = "Permission")]
        [HttpPost]
        public JsonResult UpdateStock(SkuStock request)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _skuAppService.UpdateSkuStock(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Msg = ex.Message;
            }

            return new JsonResult(result);
        }


        [HttpGet("cost/batch/list/simple/get"), Authorize(Policy = "Permission")]
        public JsonResult GetSkuCostBatchSimplePageList(string searchKey, int pageSize, int currentPage)
        {
            return base.catchError<SkuCostBatchFilterOutput>(
                   delegate (ResponsePageDto<SkuCostBatchFilterOutput> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _skuAppService.GetSkuCostBatchList(_loginId, searchKey, pageSize, currentPage);
                       return result;
                   });
        }
        #endregion
    }
}