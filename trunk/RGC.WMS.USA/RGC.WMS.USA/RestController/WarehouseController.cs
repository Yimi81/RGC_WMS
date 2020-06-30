using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Warehouse;
using RGC.WMS.USA.Application.Warehouse.Dto;
using RGC.WMS.USA.Domain.Entities.Warehouse;
using RGC.WMS.USA.Domain.Entities.Warehouse.Enum;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// MeridianGo 2020/06/11
    /// </summary>
    [ApiController]
    public class WarehouseController : WebApiManageBase
    {
        public readonly IWarehouseAppService _warehouseAppService;
        public readonly IStockInAppService _stockInAppService;
        public readonly IStockOutAppService _stockOutAppService;
        public WarehouseController(
            IWarehouseAppService warehouseAppService,
            IStockInAppService stockInAppService,
            IStockOutAppService stockOutAppService)
        {
            _warehouseAppService = warehouseAppService;
            _stockInAppService = stockInAppService;
            _stockOutAppService = stockOutAppService;
        }

        #region 仓库操作
        /// <summary>
        /// 创建仓库
        /// </summary>
        [HttpPost("create"), Authorize(Policy = "Permission")]
        public JsonResult Create(WarehouseEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _warehouseAppService.Create(_loginId, request);
                       return result;
                   });
        }

        /// <summary>
        /// 更新仓库
        /// </summary>
        [HttpPost("update"), Authorize(Policy = "Permission")]
        public JsonResult Update(WarehouseEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _warehouseAppService.Update(_loginId, request);
                       return result;
                   });
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        [HttpGet("delete/{id}"), Authorize(Policy = "Permission")]
        public JsonResult Delete(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _warehouseAppService.Delete(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 恢复仓库
        /// </summary>
        [HttpGet("recovery/{id}"), Authorize(Policy = "Permission")]
        public JsonResult Recovery(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _warehouseAppService.Recovery(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 更新仓库的状态
        /// </summary>
        [HttpGet("status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStatus(long id, WarehouseStatus status)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _warehouseAppService.UpdateStatus(id, status, _loginId);
                       return result;
                   });
        }
        #endregion

        #region 仓库查看
        /// <summary>
        /// 获取仓库
        /// </summary>
        [HttpGet("get/{id}"), Authorize(Policy = "Permission")]
        public JsonResult Get(long id)
        {
            return base.catchError<WarehouseDto>(
                   delegate (ResponseDto<WarehouseDto> result)
                   {
                       result = _warehouseAppService.Get(id);
                       return result;
                   });
        }

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        [HttpGet("list"), Authorize(Policy = "Permission")]
        public JsonResult GetPage(string key, int status, int isDeleted, int pageSize, int currentPage)
        {
            return base.catchError<WarehouseDto>(
                   delegate (ResponsePageDto<WarehouseDto> result)
                   {
                       result = _warehouseAppService.GetPage(new WarehouseFilterInput
                       {
                           SearchKey = key,
                           Status = status,
                           IsDeleted = isDeleted,
                           PageSize = pageSize,
                           CurrentPage = currentPage
                       });
                       return result;
                   });
        }

        /// <summary>
        /// 获取仓库列表，用于下拉框选择
        /// </summary>
        [HttpGet("list/simple/get"), Authorize(Policy = "Permission")]
        public JsonResult GetWarehouseSimpleList()
        {
            return base.catchError<List<WarehouseFilterSimpleOutput>>(
                   delegate (ResponseDto<List<WarehouseFilterSimpleOutput>> result)
                   {
                       result = _warehouseAppService.GetWarehouseSimpleList();
                       return result;
                   });
        }

        /// <summary>
        /// 重置缓存数据到静态变量中
        /// </summary>
        [HttpGet("dict/refresh"), Authorize(Policy = "Permission")]
        public JsonResult ForceRefreshWarehouseDict()
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result = _warehouseAppService.ForceRefreshWarehouseDict();
                       return result;
                   });
        }
        #endregion

        #region 入库操作
        /// <summary>
        /// 创建入库单
        /// </summary>
        [HttpPost("stock/in/create"), Authorize(Policy = "Permission")]
        public JsonResult StockInCreate(StockInEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockInAppService.Create(_loginId, request);
                       return result;
                   });
        }


        /// <summary>
        /// 更新入库单
        /// </summary>
        [HttpPost("stock/in/update"), Authorize(Policy = "Permission")]
        public JsonResult StockInUpdate(StockInEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockInAppService.Update(_loginId, request);
                       return result;
                   });
        }

        /// <summary>
        /// 删除入库单
        /// </summary>
        [HttpGet("stock/in/delete/{id}"), Authorize(Policy = "Permission")]
        public JsonResult StockInDelete(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockInAppService.Delete(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 恢复入库单
        /// </summary>
        [HttpGet("stock/in/recovery/{id}"), Authorize(Policy = "Permission")]
        public JsonResult StockInRecovery(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockInAppService.Recovery(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 更新入库单的状态
        /// </summary>
        [HttpGet("stock/in/status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStockInStatus(long id, StockInStatus status)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockInAppService.UpdateStatus(id, status, _loginId);
                       return result;
                   });
        }
        #endregion

        #region 入库查看
        /// <summary>
        /// 获取入库单
        /// </summary>
        [HttpGet("stock/in/get/{id}"), Authorize(Policy = "Permission")]
        public JsonResult GetStockIn(long id)
        {
            return base.catchError<StockInOutput>(
                   delegate (ResponseDto<StockInOutput> result)
                   {
                       result = _stockInAppService.Get(id);
                       return result;
                   });
        }

        /// <summary>
        /// 获取入库单列表
        /// </summary>
        [HttpGet("stock/in/list"), Authorize(Policy = "Permission")]
        public JsonResult GetStockInPage(string searchType, string searchKey, int warehouseId, int status, int isDeleted, int pageSize, int currentPage)
        {
            return base.catchError<StockInOutput>(
                   delegate (ResponsePageDto<StockInOutput> result)
                   {
                       result = _stockInAppService.GetPage(new StockInFilterInput
                       {
                           SearchType = searchType,
                           SearchKey = searchKey,
                           WarehouseId = warehouseId,
                           Status = status,
                           IsDeleted = isDeleted,
                           PageSize = pageSize,
                           CurrentPage = currentPage
                       });
                       return result;
                   });
        }
        #endregion

        #region 出库操作
        /// <summary>
        /// 创建出库单
        /// </summary>
        [HttpPost("stock/out/create"), Authorize(Policy = "Permission")]
        public JsonResult StockOutCreate(StockOutEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockOutAppService.Create(_loginId, request);
                       return result;
                   });
        }


        /// <summary>
        /// 更新出库单
        /// </summary>
        [HttpPost("stock/out/update"), Authorize(Policy = "Permission")]
        public JsonResult StockOutUpdate(StockOutEditInput request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockOutAppService.Update(_loginId, request);
                       return result;
                   });
        }

        /// <summary>
        /// 删除出库单
        /// </summary>
        [HttpGet("stock/out/delete/{id}"), Authorize(Policy = "Permission")]
        public JsonResult StockOutDelete(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockOutAppService.Delete(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 恢复出库单
        /// </summary>
        [HttpGet("stock/out/recovery/{id}"), Authorize(Policy = "Permission")]
        public JsonResult StockOutRecovery(long id)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockOutAppService.Recovery(_loginId, id);
                       return result;
                   });
        }

        /// <summary>
        /// 更新出库单状态
        /// </summary>
        [HttpGet("stock/out/status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStockOutStatus(long id, StockOutStatus status)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _stockOutAppService.UpdateStatus(id, status, _loginId);
                       return result;
                   });
        }
        #endregion

        #region 出库查看
        /// <summary>
        /// 获取出库单
        /// </summary>
        [HttpGet("stock/out/get/{id}"), Authorize(Policy = "Permission")]
        public JsonResult GetStockOut(long id)
        {
            return base.catchError<StockOutOutput>(
                   delegate (ResponseDto<StockOutOutput> result)
                   {
                       result = _stockOutAppService.Get(id);
                       return result;
                   });
        }

        /// <summary>
        /// 获取出库单列表
        /// </summary>
        [HttpGet("stock/out/list"), Authorize(Policy = "Permission")]
        public JsonResult GetStockOutPage(string searchType, string searchKey, int warehouseId, int status, int isDeleted, int pageSize, int currentPage)
        {
            return base.catchError<StockOutOutput>(
                   delegate (ResponsePageDto<StockOutOutput> result)
                   {
                       result = _stockOutAppService.GetPage(new StockOutFilterInput
                       {
                           SearchType = searchType,
                           SearchKey = searchKey,
                           WarehouseId = warehouseId,
                           Status = status,
                           IsDeleted = isDeleted,
                           PageSize = pageSize,
                           CurrentPage = currentPage
                       });
                       return result;
                   });
        }
        #endregion
    }
}