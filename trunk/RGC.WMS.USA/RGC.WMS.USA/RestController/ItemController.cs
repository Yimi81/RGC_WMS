using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Bidding;
using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Item;
using RGC.WMS.USA.Application.Item.Dto;
using RGC.WMS.USA.Application.System;
using RGC.WMS.USA.Application.System.Dto;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Item;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RGC.WMS.USA.RestController
{
    /// <summary>
    /// shane 2020/2/17
    /// </summary>
    [ApiController]
    public class ItemController : WebApiManageBase
    {
        private readonly IItemAppService _itemAppService;
        private readonly ICompetitionAppService _competitionAppService;
        private readonly IPlatformAppService _platformAppService;
        public ItemController(IItemAppService itemAppService,
            ICompetitionAppService competitionAppService,
            IPlatformAppService platformAppService)
        {
            _itemAppService = itemAppService;
            _platformAppService = platformAppService;
            _competitionAppService = competitionAppService;
        }
        /// <summary>
        /// 创建Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("create"), Authorize(Policy = "Permission")]
        //public JsonResult Create(ItemEditDto request)
        public async Task<JsonResult> Create()
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var request = await getPostData<ItemEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.CreateItem(_loginId, request);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 删除Item
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
                result = _itemAppService.Delete(_loginId, id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 更新Item主表
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        [HttpPost("update"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> Update()
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {

                var request = await getPostData<ItemEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.UpdateItem(_loginId, request);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 同步更新Item主表
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        [HttpPost("sync/update"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> SyncUpdate()
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var request = await getPostData<ItemEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.SyncUpdateItem(_loginId, request);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateStatus(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.UpdateItemStatus(id, _loginId);

                if (result.Code == 0 && result.Success == true)
                {

                }

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }


        [HttpGet("price/status/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateItemPriceStatus(long id, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.UpdateItemPriceStatus(id, _loginId, status);
                if (result.Code == 0 && result.Success == true)
                {

                }

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
            ResponseDto<ItemDto> result = new ResponseDto<ItemDto>();
            result.Data = new ItemDto();

            try
            {
                result = _itemAppService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }



        #region 平台产品相关

        /// <summary>
        /// 获取平台列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("platform/list"), Authorize(Policy = "Permission")]
        public JsonResult GetPlatformList()
        {
            ResponsePageDto<PlatformListDto> result = new ResponsePageDto<PlatformListDto>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                if (_loginId > 0)
                {
                    var searchDto = new SearchFilterDto()
                    {
                        CurrentPage = 1,
                        PageSize = 999,
                    };
                    result = _platformAppService.GetPlatformList(searchDto);
                }
                else
                {
                    result.Msg = "please login";
                }

                if (result.Success)
                {
                    result.Code = 0;
                }

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Success = false;

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 某平台下列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("list"), Authorize(Policy = "Permission")]
        public JsonResult GetPlatformItemList(long platformId, string key, string uniqueId, int? status, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemDto> result = new ResponsePageDto<ItemDto>();
            result.Data = new List<ItemDto>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.GetPlatformItemList(platformId, key, uniqueId, status, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("price/record/list"), Authorize(Policy = "Permission")]
        public JsonResult GetPriceRecordList(long platformId, string key, string itemId, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemPriceRecordDto> result = new ResponsePageDto<ItemPriceRecordDto>();
            result.Data = new List<ItemPriceRecordDto>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.GetItemPriceRecordList(platformId, key, itemId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 非平台下item列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        [HttpGet("otherList"), Authorize(Policy = "Permission")]
        public JsonResult GetOtherPlatformItemList(long platformId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemDto> result = new ResponsePageDto<ItemDto>();
            result.Data = new List<ItemDto>();

            try
            {
                // result = _itemAppService.GetOtherPlatformItemList(platformId, key, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }



        /// <summary>
        /// 改价流水接口
        /// </summary>
        /// <param name="skuPlatformId"></param>
        /// <param name="wholeSalePriceString"></param>
        /// <param name="retailPriceString"></param>
        /// <returns></returns>
        [HttpPost("update/platform/price"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> UpdatePlatformPrice()
        {

            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                var dto = await getPostData<ItemPriceApplyDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                var validTime = DateTime.Now;
                DateTime.TryParse(dto.ValidTimeString, out validTime);
                result = _itemAppService.UpdateItemPlatformPrice(_loginId, dto.ItemId, dto.WholeSalePriceString, dto.RetailPriceString, validTime, dto.Level);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }


        [HttpGet("update/platform/status"), Authorize(Policy = "Permission")]
        public JsonResult UpdatePlatformStatus(long id, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.UpdatePlatformStatus(_loginId, id, status);

                if (result.Code == 0 && result.Success == true)
                {

                }

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("delete/platform"), Authorize(Policy = "Permission")]
        public JsonResult DeletePlatform(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.DeleteItem(_loginId, id);

                if (result.Code == 0 && result.Success == true)
                {

                }

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 获取所有平台在售产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet("all/platform/list"), Authorize(Policy = "Permission")]
        public JsonResult GetItemPlatformList(string key, long platformId, int currentPage, int pageSize, int status)
        {
            ResponsePageDto<ItemDto> result = new ResponsePageDto<ItemDto>();
            result.Data = new List<ItemDto>();

            try
            {
                result = _itemAppService.GetItemList(key, platformId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }



        /// <summary>
        /// 刷新关联平台
        /// </summary>
        /// <returns></returns>
        [HttpGet("refresh"), Authorize(Policy = "Permission")]
        public JsonResult RefreshItemDictionary()
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                result = _itemAppService.ForceRefreshItemDict();

                if (result.Code == 0 && result.Success == true)
                {

                }

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                result.Success = false;

            }

            return new JsonResult(result);
        }
        #endregion

        #region 竞品

        [HttpGet("competition/get"), Authorize(Policy = "Permission")]
        public JsonResult CompetitionGet(long id)
        {
            ResponseDto<CompetitionDto> result = new ResponseDto<CompetitionDto>();
            result.Data = new CompetitionDto();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.Get(id);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }

        [HttpPost("competition/add"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> CompetitionCreate()
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            try
            {
                var dto = await getPostData<CompetitionEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.Create(_loginId, dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }

        [HttpPost("competition/update"), Authorize(Policy = "Permission")]
        public async Task<JsonResult> CompetitionUpdate()
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            try
            {
                var dto = await getPostData<CompetitionEditDto>(HttpContext.Request.Body);
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.Update(_loginId, dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }

        [HttpPost("competition/page/query"), Authorize(Policy = "Permission")]
        public JsonResult CompetitionPageQuery(CompetitionSearchDto dto)
        {
            ResponsePageDto<CompetitionDto> result = new ResponsePageDto<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.GetPlatformCompetitionList(dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }

        [HttpGet("competition/list"), Authorize(Policy = "Permission")]
        public JsonResult ItemCompetitionList(long itemId, string key, int pageSize, int currentPage)
        {
            ResponsePageDto<CompetitionDto> result = new ResponsePageDto<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.GetItemCompetitionList(null, itemId, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }


       
        #endregion

        #region 每日平台产品价格
        [HttpGet("dailyprice/search/list"), Authorize(Policy = "Permission")]
        public JsonResult GetItemDailyPriceSearchList(string key)
        {
            ResponsePageDto<SearchOutput> result = new ResponsePageDto<SearchOutput>();
            result.Data = new List<SearchOutput>();

            try
            {
                result = _itemAppService.GetItemDailyPriceSearchList(key);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpPost("dailyprice/list"), Authorize(Policy = "Permission")]
        public JsonResult GetItemDailyPriceList(ItemSearchDto dto)
        {
            ResponsePageDto<ItemDailyPrice> result = new ResponsePageDto<ItemDailyPrice>();
            result.Data = new List<ItemDailyPrice>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.GetItemDailyPriceList(dto.platformIds, dto.date, dto.dateRange, dto.key, dto.itemId, dto.sources, dto.isOnly, dto.pageSize, dto.currentPage, dto.startTime, dto.endTime);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }


        [HttpPost("dailyreview/list"), Authorize(Policy = "Permission")]
        public JsonResult GetItemDailyReviewList(ItemSearchDto dto)
        {
            ResponsePageDto<ItemDailyReview> result = new ResponsePageDto<ItemDailyReview>();
            result.Data = new List<ItemDailyReview>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.GetItemDailyReviewList(dto.platformIds, dto.date, dto.dateRange, dto.key, dto.itemId, dto.sources, dto.isOnly, dto.pageSize, dto.currentPage, dto.startTime, dto.endTime);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }


        [HttpPost("dailyprice/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateItemDailyPrice(ItemDailyPrice dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                result = _itemAppService.UpdateItemDailyPrice(dto);
                
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpPost("dailyprice/add"), Authorize(Policy = "Permission")]
        public JsonResult AddItemDailyPrice(ItemDailyPrice dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                
                result = _itemAppService.AddItemDailyPrice(dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }

            return new JsonResult(result);
        }

        [HttpGet("dailyprice/delete"), Authorize(Policy = "Permission")]
        public JsonResult DeleteItemDailyPrice(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _itemAppService.DeleteItemDailyPrice(_loginId, id);
                if (result.Code == 0 && result.Success == true)
                {

                }
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
                
            }
            return new JsonResult(result);
        }

        #endregion

        #region 每日竞品价格
        [HttpGet("competition/search/list"), Authorize(Policy = "Permission")]
        public JsonResult CompetitionSearchList(string key)
        {
            ResponsePageDto<SearchOutput> result = new ResponsePageDto<SearchOutput>();
            result.Data = new List<SearchOutput>();
            try
            {
                result = _competitionAppService.GetCompetitionDailySearchList(key);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常
            }
            return new JsonResult(result);
        }

        [HttpPost("competition/daily/list"), Authorize(Policy = "Permission")]
        public JsonResult GetCompetitionDailyPriceList(CompSearchDto dto)
        {
            ResponsePageDto<CompetitionDaily> result = new ResponsePageDto<CompetitionDaily>();
            result.Data = new List<CompetitionDaily>();

            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.GetCompetitionDailyList(dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }


        [HttpPost("Competition/dailyprice/update"), Authorize(Policy = "Permission")]
        public JsonResult UpdateCompetitionDailyPrice(CompetitionDaily dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                result = _competitionAppService.UpdateCompetitionDaily(dto);

            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpPost("competition/dailyprice/add"), Authorize(Policy = "Permission")]
        public JsonResult AddCompetitionDailyPrice(CompetitionDaily dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {

                result = _competitionAppService.AddCompetitionDaily(dto);
            }
            catch (Exception ex)
            {
                result.Code = 2;//系统异常

            }

            return new JsonResult(result);
        }

        [HttpGet("competition/dailyprice/delete"), Authorize(Policy = "Permission")]
        public JsonResult DeleteCompetitionDailyPrice(long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            try
            {
                var _loginId = 0;
                int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out _loginId);
                result = _competitionAppService.DeleteCompetitionDaily(_loginId, id);
                if (result.Code == 0 && result.Success == true)
                {

                }
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