using AutoMapper;
using HuigeTec.Core.Helpers;
using Microsoft.Extensions.Options;
using RGC.WMS.USA.Application.Bidding.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain.Entities.Bidding;
using RGC.WMS.USA.Domain.Entities.Bidding.Do;
using RGC.WMS.USA.Domain.Repositories.Bidding;
using RGC.WMS.USA.Domain.Services.Bidding;
using RGC.WMS.USA.Domain.Services.Item;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Bidding
{
    /// <summary>
    /// shane 2020/03/05
    /// </summary>
    public class CompetitionAppService : ICompetitionAppService
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly ICompetitionDailyRepository _competitionDailyRepository;
        private readonly ICompetitionService _competitionService;
        private readonly IItemService _itemService;

        private readonly IOptions<ApplicationBaseConfig> _configuration;

        private IMapper _mapper { get; }


        public CompetitionAppService(ICompetitionRepository competitionRepository,
            ICompetitionService competitionService,
             ICompetitionDailyRepository competitionDailyRepository,
            IItemService itemService,
            IMapper mapper, IOptions<ApplicationBaseConfig> configuration)
        {
            _competitionRepository = competitionRepository;
            _competitionService = competitionService;
            _competitionDailyRepository = competitionDailyRepository;
            _mapper = mapper;
            _configuration = configuration;
            _itemService = itemService;
        }
        public ResponseDto<string> Create(long loginId, CompetitionEditDto dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {

            }
            else
            {
                dto.Name = dto.Name.Trim();
            }
            if (dto.PlatformId == 0)
            {
                result.Code = 2;
                result.Msg = "平台不能为空";
                result.Success = false;
                return result;
            }
            //
            var ifExist = _competitionService.IfExist(dto.PlatformId, dto.Id, dto.FactoryModel);
            if (!ifExist.Success)
            {
                result.Msg = ifExist.Msg;
                result.Code = 2;
                result.Success = false;
                return result;
            }

            var resp = _competitionService.Create(loginId, _mapper.Map<CompetitionEditDo>(dto));
            if (resp.Success)
            {
                result.Success = true;
                result.Code = 0;
            }

            return result;
        }


        /// <summary>
        /// 删除item
        /// shane 2019/5/27
        /// <param name="loginId"></param>
        /// <param name="loginIP"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDto<string> Delete(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            var productIdList = new List<long>();
            int execute = _competitionRepository.SingleDelete(loginId, id);

            if (execute > 0)
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
        /// 修改Item主表
        /// <param name="item"></param>
        /// <returns></returns>
        public ResponseDto<string> Update(long loginId, CompetitionEditDto dto)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            if (string.IsNullOrWhiteSpace(dto.FactoryModel))
            {
                result.Code = 2;
                result.Msg = "工厂型号不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                dto.FactoryModel = dto.FactoryModel.Trim();
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {

            }
            else
            {
                dto.Name = dto.Name.Trim();
            }
            if (dto.PlatformId == 0)
            {
                result.Code = 2;
                result.Msg = "平台不能为空";
                result.Success = false;
                return result;
            }
            //
            var ifExist = _competitionService.IfExist(dto.PlatformId, dto.Id, dto.FactoryModel);
            if (!ifExist.Success)
            {
                result.Msg = ifExist.Msg;
                result.Code = 2;
                result.Success = false;
                return result;
            }

            var resp = _competitionService.Update(loginId, _mapper.Map<CompetitionEditDo>(dto));
            if (resp.Success)
            {
                result.Success = true;
                result.Code = 0;
            }

            return result;
        }


        /// <summary>
        /// 修改状态
        /// </summary>
        public ResponseDto<string> UpdateStatus(long id, long modifierUserId, bool isValid)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            int excute = _competitionRepository.UpdateStatus(id, modifierUserId, isValid);
            if (excute > 0)
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
        /// 单独获取Item
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseDto<CompetitionDto> Get(long id)
        {
            ResponseDto<CompetitionDto> result = new ResponseDto<CompetitionDto>();
            result.Data = new CompetitionDto();
            var item = _competitionRepository.SingleGet(id);
            result.Data = _mapper.Map<CompetitionDto>(item);
            result.Data.SrcFull = _configuration.Value.ImgSiteRootAddress + result.Data.Src;
            if (result.Data != null)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }


        /// <summary>
        /// 分页获取列表
        /// <param name="key"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public ResponsePageDto<CompetitionDto> GetPage(string key, long categoryId, int pageSize, int currentPage)
        {
            ResponsePageDto<CompetitionDto> result = new ResponsePageDto<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            int count = 0;
            var data = _competitionRepository.CompetitionPageQuery(key, categoryId, pageSize, currentPage, out count);
            if (data != null && data.Count >= 0)
            {
                result.Data = _mapper.Map<List<CompetitionDto>>(data);
                result.Code = 0;
                result.Success = true;
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / pageSize);
                result.Page.PageSize = pageSize;
                result.Page.CurrentPage = currentPage;
                result.Page.CurrentCount = result.Data.Count;

            }
            return result;
        }

        /// <summary>
        /// 某平台下列表
        /// </summary>
        /// <param name="PlatformId"></param>
        /// <param name="key"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <returns></returns>
        public ResponsePageDto<CompetitionDto> GetPlatformCompetitionList(CompetitionSearchDto dto)
        {
            ResponsePageDto<CompetitionDto> result = new ResponsePageDto<CompetitionDto>();
            result.Data = new List<CompetitionDto>();
            int count = 0;
            var data = _competitionRepository.GetPlatformCompetitionList(dto.PlatformId, dto.SearchKey, dto.ItemId, dto.ProductId, dto.Status, dto.PageSize, dto.CurrentPage, out count);
            if (data != null && data.Count >= 0)
            {
                result.Data = _mapper.Map<List<CompetitionDto>>(data);
                result.Code = 0;
                result.Success = true;
                result.Page.TotalCount = (int)count;
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / dto.PageSize);
                result.Page.PageSize = dto.PageSize;
                result.Page.CurrentPage = dto.CurrentPage;
                result.Page.CurrentCount = result.Data.Count;
            }
            return result;
        }


        /// <summary>
        /// 加入平台
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="PlatformId"></param>
        /// <returns></returns>
        public ResponseDto<string> AddPlatform(CompetitionEditDto entity)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                result.Code = 2;
                result.Msg = "产品名称不能为空";
                result.Success = false;
                return result;
            }
            else
            {
                entity.Name = entity.Name.Trim();

            }
            entity.PlatformUrl = string.IsNullOrWhiteSpace(entity.PlatformUrl) ? "" : entity.PlatformUrl.Trim();
            entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? "" : entity.Name.Trim();
            entity.UniqueId = string.IsNullOrWhiteSpace(entity.UniqueId) ? "" : entity.UniqueId.Trim();
            entity.RetailPrice = string.IsNullOrWhiteSpace(entity.RetailPriceString) ? 0 : ParseHelper.Yuan2Fen(entity.RetailPriceString);

            entity.PlatformUrl = string.IsNullOrWhiteSpace(entity.PlatformUrl) ? "" : entity.PlatformUrl.Trim();
            int execute = _competitionRepository.SingleAdd(_mapper.Map<Competition>(entity));

            if (execute > 0)
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
        /// 更新平台当日零售价
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="PlatformId"></param>
        /// <returns></returns>
        public int BatchUpdatePrice(List<CompetitionDaily> entity)
        {
            int execute = 0;
            execute = _competitionRepository.BatchUpdatePrice(entity);
            return execute;
        }

        /// <summary>
        /// 更新平台在售产品信息
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="PlatformId"></param>
        /// <returns></returns>
        public ResponseDto<string> UpdatePlatform(CompetitionEditDto entity)
        {
            ResponseDto<string> result = new ResponseDto<string>();

            entity.PlatformUrl = string.IsNullOrWhiteSpace(entity.PlatformUrl) ? "" : entity.PlatformUrl.Trim();
            entity.Name = string.IsNullOrWhiteSpace(entity.Name) ? "" : entity.Name.Trim();
            entity.UniqueId = string.IsNullOrWhiteSpace(entity.UniqueId) ? "" : entity.UniqueId.Trim();
            //if (!string.IsNullOrWhiteSpace(entity.wholeSalePriceString))
            //    entity.wholeSalePrice = ParseHelper.Yuan2Fen(entity.wholeSalePriceString);
            //using (var dbproduct = new DbProduct())
            //{
            //    entity.Status = (int)dbproduct.Get(entity.ProductId).Status;
            //}
            int execute = _competitionRepository.SingleUpdate(_mapper.Map<Competition>(entity));

            if (execute > 0)
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


        public ResponseDto<string> UpdatePlatformStatus(long loginId, long id, int status)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            var data = _competitionRepository.SingleGet(id);
            data.IsValid = status == 1 ? true : false;
            data.LastModifierUserId = loginId;
            data.LastModificationTime = DateTime.Now;
            int excute = _competitionRepository.UpdateStatus(loginId, id, data.IsValid);
            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }

        public ResponseDto<string> DeletePlatform(long loginId, long id)
        {
            ResponseDto<string> result = new ResponseDto<string>();
            int excute = _competitionRepository.SingleDelete(loginId, id);

            if (excute > 0)
            {
                result.Code = 0;
                result.Success = true;
            }
            return result;
        }


        #region 每日价格报表相关

        public ResponseDto<string> UpdateStatus(long id, long modifierUserId)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> UpdateCompetitionPlatformPrice(long adminId, long CompetitionId, string retailPriceString)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> ForceRefreshCompetitionDict()
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> ForceRefreshCompetitionDailyDict()
        {
            throw new NotImplementedException();
        }

        public List<CompetitionDto> GetCompetitionPlatFormList()
        {
            throw new NotImplementedException();
        }

        public List<PlatformCompetitionObj> GetPlatFormCompetitionList()
        {
            throw new NotImplementedException();
        }

        public ResponsePageDto<CompetitionDto> GetItemCompetitionList(string key, long ItemId, int pageSize, int currentPage)
        {
            throw new NotImplementedException();
        }

        public ResponsePageDto<CompetitionDto> GetCompetitionPlatformList(CompetitionSearchDto dto)
        {
            throw new NotImplementedException();
        }

        public ResponsePageDto<CompetitionDto> GetCompetitionPlatformDetailList(CompetitionSearchDto dto)
        {
            throw new NotImplementedException();
        }

        public ResponsePageDto<SearchOutput> GetCompetitionDailySearchList(string key)
        {
            ResponsePageDto<SearchOutput> result = new ResponsePageDto<SearchOutput>();
            result.Data = new List<SearchOutput>();
            var dataResp = _competitionService.GetSearchList(key);
            if (dataResp.Success)
            {
                var data = dataResp.Data;
                foreach (var item in data)
                {
                  
                    result.Data.Add(new SearchOutput {Id=item.id,Value=item.value });
                }

            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        public ResponsePageDto<CompetitionDaily> GetCompetitionDailyList(CompSearchDto dto)
        {
            ResponsePageDto<CompetitionDaily> result = new ResponsePageDto<CompetitionDaily>();
            result.Data = new List<CompetitionDaily>();
            if (dto.startTime > DateTime.MinValue && dto.endTime > DateTime.MinValue && dto.startTime > dto.endTime)
            {
                result.Msg = "开始或结束时间有误";
                return result;
            }
            int count = 0;
            var data = _competitionDailyRepository.PageQuery(dto.platformIds, dto.date, dto.dateRange, dto.key, dto.itemId, dto.sources, dto.isOnly, dto.pageSize, dto.currentPage, dto.startTime, dto.endTime, out count);
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
                result.Page.TotalPages = (int)Math.Ceiling((Decimal)count / dto.pageSize);
                result.Page.PageSize = dto.pageSize;
                result.Page.CurrentPage = dto.currentPage;
                result.Page.CurrentCount = count;
            }
            return result;
        }

        public ResponsePageDto<CompetitionDaily> GetCompetitionDailyChartList(CompSearchDto dto)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> UpdateCompetitionDaily(CompetitionDaily entity)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> AddCompetitionDaily(CompetitionDaily entity)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<string> DeleteCompetitionDaily(long loginId, long id)
        {
            throw new NotImplementedException();
        }

        public int BatchAddCompetitionDaily(List<CompetitionDaily> dto)
        {
            throw new NotImplementedException();
        }

        public int BatchChangeCompetitionPlatformStatus(List<CompetitionDto> dto)
        {
            throw new NotImplementedException();
        }

        #endregion




    }
}
