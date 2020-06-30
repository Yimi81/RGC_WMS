using AutoMapper;
using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.Item.Dto;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Item;
using RGC.WMS.USA.Domain.Entities.Item.Do;
using RGC.WMS.USA.Domain.Entities.Item.Enum;
using RGC.WMS.USA.Domain.Repositories.Item;
using RGC.WMS.USA.Domain.Services.Bms;
using RGC.WMS.USA.Domain.Services.Item;
using RGC.WMS.USA.Domain.Services.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGC.WMS.USA.Application.Item
{
    public class ItemAppService : IItemAppService
    {

        private readonly IItemRepository _itemRepository;
        private readonly IItemModifyFlowRepository _itemModifyFlowRepository;
        private readonly IItemPriceRecordRepository _itemPriceRecordRepository;
        private readonly IItemDailyPriceRepository _itemDailyPriceRepository;
        private readonly IItemService _itemService;
        private readonly IBmsUserService _bmsUserService;
        private readonly IPlatformService _platformService;
        private readonly ISystemInfoService _systemInfoService;
        private IMapper _mapper { get; }
        private readonly IOptions<ApplicationBaseConfig> _configuration;
        public ItemAppService(
            IItemDailyPriceRepository itemDailyPriceRepository,
            IItemRepository itemRepository,
            IItemService itemService,
             IBmsUserService bmsUserService,
             IPlatformService platformService,
             IItemPriceRecordRepository itemPriceRecordRepository,
              IItemModifyFlowRepository itemModifyFlowRepository,
            IOptions<ApplicationBaseConfig> configuration,
             ISystemInfoService systemInfoService,
            IMapper mapper)
        {
            _itemDailyPriceRepository = itemDailyPriceRepository;
            _itemRepository = itemRepository;
            _configuration = configuration;
            _mapper = mapper;
            _itemService = itemService;
            _bmsUserService = bmsUserService;
            _platformService = platformService;
            _itemPriceRecordRepository = itemPriceRecordRepository;
            _itemModifyFlowRepository = itemModifyFlowRepository;
            _systemInfoService = systemInfoService;


        }

        #region item平台产品
        public ResponseDto<string> CreateItem(long loginId, ItemEditDto dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            #region 检查参数

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.ItemName))
            {
                result.Code = 2;
                result.Msg = "产品名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.ItemName = dto.ItemName.Trim();
            }

            if (string.IsNullOrWhiteSpace(dto.ImageMain) && string.IsNullOrWhiteSpace(dto.Src))
            {
                result.Code = 2;
                result.Msg = "展示图不能为空";
                result.Success = false;
                return result;
            }
            if (dto.PlatformId == 0)
            {
                result.Code = 2;
                result.Msg = "平台不能为空";
                result.Success = false;
                return result;
            }
            if (dto.IsSpecial && !dto.ProductList.Any())
            {
                result.Code = 2;
                result.Msg = "组合产品的关联产品不能为空";
                result.Success = false;
                return result;
            }
            #endregion
            dto.CreatorUserId = loginId;
            dto.CreationTime = DateTime.Now;
            dto.IsValid = true;
            dto.WholeSalePrice = ParseHelper.Yuan2Fen(dto.WholeSalePriceString);
            dto.Msrp = ParseHelper.Yuan2Fen(dto.MsrpString);
            dto.Map = ParseHelper.Yuan2Fen(dto.MapString);
            if (dto.IsSpecial)
            {
                dto.ProductId = 0;
            }
            else
            {
                dto.ProductList = new List<ItemProduct>();
            }

            var model = _mapper.Map<ItemEditDo>(dto);
            var resp = _itemService.CreateItem(model);
            if (resp.Code == 0 && resp.Success)
            {
                result.Data = dto.Src;
                if (dto.PlatformId == 10)
                {
                    if (!dto.IsSync)
                    {
                        var flow = _mapper.Map<ItemModifyFlow>(model);
                        flow.Id = 0;
                        flow.ProductId = model.Id;
                        flow.SyncStatus = 0;
                        flow.RequestSyncTime = DateTime.Now;
                        flow.CreatorUserId = loginId;
                        _itemService.AddItemFlow(flow);
                    }
                }

            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> Delete(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            if (id <= 0)
            {
                result.Msg = "请求数据异常";
                return result;
            }

            var data = _itemService.Delete(loginId, id);
            if (data.Code == 0 && data.Success)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }
        public ResponseDto<string> ForceRefreshItemDict()
        {
            ResponseDto<string> result = new ResponseDto<string>();
            result.Data = "";
            _itemRepository.ForceRefreshItemDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
        public ResponseDto<ItemDto> Get(long id)
        {
            ResponseDto<ItemDto> result = new ResponseDto<ItemDto>();
            result.Data = new ItemDto();
            //var data = _productRepository.GetById(id);
            var query = _itemRepository.Get(id);
            query.SrcFull = string.IsNullOrEmpty(query.Src) ? "" : _configuration.Value.ImgSiteRootAddress + query.Src;
            if (query.Id > 0)
            {
                result.Data = _mapper.Map<ItemDto>(query);

                result.Code = 0;
                result.Success = true;
            }

            return result;
        }
        /// <summary>
        /// 获取平台在售产品列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ItemId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public ResponsePageDto<ItemDto> GetItemList(string key, long platformId, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemDto> result = new ResponsePageDto<ItemDto>();
            result.Data = new List<ItemDto>();
            var query = _itemRepository.ItemPageQuery(key, platformId, pageSize, currentPage, out int total);
            foreach (var item in query)
            {
                if (item.ValidTime <= DateTime.Now)
                    item.RetailPrice = item.PreRetailPrice;
            }
            result.Data = _mapper.Map<List<ItemDto>>(query);
            if (result.Data != null)
            {
                result.Page.TotalCount = (int)total;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = query.Count;
            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> DeleteItem(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _itemService.Delete(loginId, id);

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

        /// <summary>
        /// 获取某平台下列表
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="platformId"></param>
        /// <param name="key"></param>
        /// <param name="uniqueId"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public ResponsePageDto<ItemDto> GetPlatformItemList(long platformId, string key, string uniqueId, int? status, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemDto> result = new ResponsePageDto<ItemDto>();
            result.Data = new List<ItemDto>();
            var query = _itemRepository.GetPlatformItemList(platformId, key, uniqueId, status, pageSize, currentPage, out int total);
            result.Data = _mapper.Map<List<ItemDto>>(query);
            if (result.Data != null)
            {
                foreach (var item in result.Data)
                {
                    if (item.ValidTime <= DateTime.Now)
                        item.RetailPrice = item.PreRetailPrice;
                    item.SrcFull = string.IsNullOrEmpty(item.Src) ? "" : _configuration.Value.ImgSiteRootAddress + item.Src;
                    item.PlatformName = _platformService.ManageGetPlatformDetail(item.PlatformId).Data.EName;
                }
                result.Page.TotalCount = (int)total;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = query.Count;
            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponseDto<string> UpdateItemStatus(long id, long modifierUserId)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _itemService.UpdateStatus(id, modifierUserId);
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

        public ResponseDto<string> UpdateItem(long loginId, ItemEditDto dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            #region 检查参数

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.ItemName))
            {
                result.Code = 2;
                result.Msg = "产品名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.ItemName = dto.ItemName.Trim();
            }

            if (string.IsNullOrWhiteSpace(dto.ImageMain) && string.IsNullOrWhiteSpace(dto.Src))
            {
                result.Code = 2;
                result.Msg = "展示图不能为空";
                result.Success = false;
                return result;
            }
            if (dto.PlatformId == 0)
            {
                result.Code = 2;
                result.Msg = "平台不能为空";
                result.Success = false;
                return result;
            }
            if (dto.IsSpecial && !dto.ProductList.Any())
            {
                result.Code = 2;
                result.Msg = "组合产品的关联产品不能为空";
                result.Success = false;
                return result;
            }
            #endregion
            dto.CreatorUserId = loginId;
            dto.CreationTime = DateTime.Now;
            dto.IsValid = true;
            dto.WholeSalePrice = ParseHelper.Yuan2Fen(dto.WholeSalePriceString);
            dto.Msrp = ParseHelper.Yuan2Fen(dto.MsrpString);
            dto.Map = ParseHelper.Yuan2Fen(dto.MapString);
            if (dto.IsSpecial)
            {
                dto.ProductId = 0;
            }
            else
            {
                dto.ProductList = new List<ItemProduct>();
            }

            var model = _mapper.Map<ItemEditDo>(dto);
            var excuteResp = _itemService.UpdateItem(model);
            if (excuteResp.Success && excuteResp.Code == 0)
            {
                result.Data = dto.Src;
                if (dto.PlatformId == 10)
                {
                    if (!dto.IsSync)
                    {
                        var flow = _mapper.Map<ItemModifyFlow>(model);
                        flow.Id = 0;
                        flow.ProductId = model.Id;
                        flow.SyncStatus = 0;
                        flow.RequestSyncTime = DateTime.Now;
                        flow.CreatorUserId = loginId;
                        _itemService.AddItemFlow(flow);
                    }
                }


            }
            else
            {
                result.Msg = "操作失败";
            }
            result.Success = true;
            result.Code = 0;
            return result;
        }

        /// <summary>
        /// 更新产品平台状态
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ResponseDto<string> UpdatePlatformStatus(long loginId, long id, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _itemService.UpdatePlatformStatus(loginId, id, status);
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
        #endregion

        #region 平台产品价格相关
        public ResponsePageDto<ItemPriceRecordDto> GetItemPriceRecordList(long platformId, string key, string itemId, int pageSize, int currentPage)
        {
            ResponsePageDto<ItemPriceRecordDto> result = new ResponsePageDto<ItemPriceRecordDto>();
            result.Data = new List<ItemPriceRecordDto>();
            var query = _itemPriceRecordRepository.GetItemPriceRecordList(platformId, key, itemId, pageSize, currentPage, out int total);
            result.Data = _mapper.Map<List<ItemPriceRecordDto>>(query);

            if (result.Data != null)
            {

                foreach (var item in result.Data)
                {
                    item.SrcFull = string.IsNullOrEmpty(item.Src) ? "" : _configuration.Value.ImgSiteRootAddress + item.Src;
                    item.CreationUserName = _bmsUserService.GetDetail(item.CreatorUserId).Data?.LoginName;
                }
                result.Page.TotalCount = (int)total;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)total / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = query.Count;
            }
            else
            {
                result.Msg = "暂无数据";
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }
        /// <summary>
        /// 更新平台在售产品价格
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="ItemId"></param>
        /// <param name="wholeSalePriceString"></param>
        /// <param name="retailPriceString"></param>
        /// <returns></returns>
        public ResponseDto<string> UpdateItemPlatformPrice(long adminId, long ItemId, string wholeSalePriceString, string retailPriceString, DateTime validTime, int level)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            try
            {
                if (adminId < 0)
                {
                    result.Msg = "请先登入";
                    result.Code = 3;
                    return result;
                }
                var resp = new ResponseDo<string>();
                if (ParseHelper.Yuan2Fen(wholeSalePriceString) > 0 && validTime == DateTime.MinValue)
                {
                    resp = _itemService.UpdateAndAddRecord(adminId, ItemId, wholeSalePriceString, retailPriceString);
                }
                else if (ParseHelper.Yuan2Fen(retailPriceString) > 0 && validTime != null)
                {
                    resp = _itemService.PriceApplyAndAddRecord(adminId, ItemId, retailPriceString, validTime, level);
                }

                if (resp.Code == 0 && resp.Success)
                {
                    result.Code = 0;
                    result.Success = true;
                }
                else
                {
                    result.Msg = "操作失败";
                }
            }
            catch (Exception ex)
            {


            }

            return result;
        }

        /// <summary>
        /// shane 2020/02/24
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifierUserId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ResponseDto<string> UpdateItemPriceStatus(long id, long modifierUserId, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var excuteResp = _itemService.UpdateItemPriceVertifyStatus(id, modifierUserId, status);
            var price = _itemPriceRecordRepository.Get(id);
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
        #endregion

        public ResponseDto<string> SyncUpdateItem(long loginId, ItemEditDto dto)
        {
            throw new NotImplementedException();
        }

        #region 每日价格报表相关

        public ResponsePageDto<SearchOutput> GetItemDailyPriceSearchList(string key)
        {
            ResponsePageDto<SearchOutput> result = new ResponsePageDto<SearchOutput>();
            result.Data = new List<SearchOutput>();
            var dataResp = _itemService.GetItemSearchList(key);
            if (dataResp.Success)
            {
                var data = dataResp.Data;
                foreach (var item in data)
                {

                    result.Data.Add(new SearchOutput { Id = item.id, Value = item.value });
                }

            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponsePageDto<ItemDailyPrice> GetItemDailyPriceList(List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long itemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime)
        {
            ResponsePageDto<ItemDailyPrice> result = new ResponsePageDto<ItemDailyPrice>();
            result.Data = new List<ItemDailyPrice>();
            if (startTime > DateTime.MinValue && endTime > DateTime.MinValue && startTime > endTime)
            {
                result.Msg = "开始或结束时间有误";
                return result;
            }
            int count = 0;
            var data = _itemDailyPriceRepository.PageQuery(platformIds, date, dateRange, key, itemId, sources, isOnly, pageSize, currentPage, startTime, endTime, out count);
            foreach (var item in data)
            {
                item.PriceString = ParseHelper.Fen2YuanString(item.Price);
            }
            result.Data = data;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Code = 0;
                result.Success = true;
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = data.Count;
            }
            return result;
        }

        public ResponsePageDto<ItemDailyReview> GetItemDailyReviewList( List<long> platformIds, DateTime date, List<DateTime> dateRange, string key, long itemId, List<DailyPriceSource> sources, bool isOnly, int pageSize, int currentPage, DateTime startTime, DateTime endTime)
        {
            ResponsePageDto<ItemDailyReview> result = new ResponsePageDto<ItemDailyReview>();
            result.Data = new List<ItemDailyReview>();
            if (startTime > DateTime.MinValue && endTime > DateTime.MinValue && startTime > endTime)
            {
                result.Msg = "开始或结束时间有误";
                return result;
            }
            int count = 0;
            var data = _itemDailyPriceRepository.PageQuery(platformIds, date, dateRange, key, itemId, sources, isOnly, pageSize, currentPage, startTime, endTime, out count);
            foreach (var item in data)
            {
                item.PriceString = ParseHelper.Fen2YuanString(item.Price);
            }
            result.Data = null;// data;
            if (result.Data != null && result.Data.Count >= 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDto<string> UpdateItemDailyPrice(ItemDailyPrice entity)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> AddItemDailyPrice(ItemDailyPrice entity)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> DeleteItemDailyPrice(long loginId, long id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
