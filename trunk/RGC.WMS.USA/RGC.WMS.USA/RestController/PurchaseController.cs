using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Purchase;
using RGC.WMS.USA.Application.Purchase.Dto;
using RGC.WMS.USA.Application.Sku;
using RGC.WMS.USA.Domain.Entities.Purchase.Enum;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// MeridianGo 2020/06/17
    /// </summary>
    [ApiController]
    public class PurchaseController : WebApiManageBase
    {
        public readonly IPackingListAppService _packingListAppService;
        public PurchaseController(
            IPackingListAppService packingListAppService)
        {
            _packingListAppService = packingListAppService;
        }

        /// <summary>
        /// 发货单状态更新
        /// </summary>
        [HttpGet("packinglist/status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStatus(long id, CargoStatus status)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var _loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                       result = _packingListAppService.UpdateStatus(id, status, _loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 获取指定发货单
        /// </summary>
        [HttpGet("packinglist/get/{id}"), Authorize(Policy = "Permission")]
        public JsonResult GetPackingList(long id)
        {
            return base.catchError<PackingListOutput>(
                   delegate (ResponseDto<PackingListOutput> result)
                   {
                       result = _packingListAppService.Get(id);
                       return result;
                   });
        }

        /// <summary>
        /// 获取发货单清单
        /// </summary>
        [HttpGet("packinglist/list"), Authorize(Policy = "Permission")]
        public JsonResult GetPackingListPage(string searchType, string searchKey, long warehouseId, int isDeleted, int pageSize, int currentPage)
        {
            return base.catchError<PackingListOutput>(
                   delegate (ResponsePageDto<PackingListOutput> result)
                   {
                       result = _packingListAppService.GetPage(
                           new PackingListFilterInput
                           {
                               SearchType = searchType,
                               SearchKey = searchKey,
                               WarehouseId = warehouseId,
                               IsDeleted = isDeleted,
                               PageSize = pageSize,
                               CurrentPage = currentPage
                           });
                       return result;
                   });
        }
    }
}