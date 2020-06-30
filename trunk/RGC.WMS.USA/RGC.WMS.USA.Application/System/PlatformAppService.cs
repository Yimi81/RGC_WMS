using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System.Do;
using RGC.WMS.USA.Domain.Repositories.System;
using RGC.WMS.USA.Domain.Services.Bms;
using RGC.WMS.USA.Domain.Services.System;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.System
{
    public class PlatformAppService : IPlatformAppService, IAppService
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IPlatformService _platformService;
        private readonly IBmsUserService _bmsUserService;

        public PlatformAppService(
            IPlatformRepository platformRepository,
            IPlatformService platformService,
            IBmsUserService bmsUserService
            )
        {
            _platformRepository = platformRepository;
            _platformService = platformService;
            _bmsUserService = bmsUserService;
        }

        /// <summary>
        /// 后台分页查询平台列表
        /// </summary>
        public ResponsePageDto<PlatformListDto> GetPlatformList(SearchFilterDto searchFilter)
        {
            var result = new ResponsePageDto<PlatformListDto>();
            result.Data = new List<PlatformListDto>();
            var searchDo = new SearchFilterDo()
            {
                CurrentPage = searchFilter.CurrentPage,
                PageSize = searchFilter.PageSize,
                SearchKey = searchFilter.SearchKey,
                Sorting = searchFilter.Sorting
            };
            var excute = _platformRepository.PageQuery(searchDo, out int count);
            if (excute.Count > 0)
            {
                foreach (var platform in excute)
                {
                    var _platform = new PlatformListDto();
                    _platform.Id = platform.Id;
                    _platform.CName = platform.CName;
                    _platform.EName = platform.EName;
                    result.Data.Add(_platform);
                }
            }
            result.Code = 0;
            result.Success = true;
            result.Page.TotalCount = count;
            result.Page.TotalPages = (int)Math.Ceiling((Decimal)result.Page.TotalCount / searchFilter.PageSize);
            result.Page.PageSize = searchFilter.PageSize;
            result.Page.CurrentPage = searchFilter.CurrentPage;
            return result;
        }

        /// <summary>
        /// 后台查询全部平台列表,选择列表
        /// </summary>
        public ResponseDto<List<PlatformSimpleListDto>> GetPlatformSimpleList()
        {
            var result = new ResponseDto<List<PlatformSimpleListDto>>();
            result.Data = new List<PlatformSimpleListDto>();
            var excute = _platformRepository.AllListGet();

            if (excute.Count > 0)
            {
                foreach (var platform in excute)
                {
                    var _platform = new PlatformSimpleListDto();
                    _platform.Id = platform.Id;
                    _platform.EName = platform.EName;
                    _platform.CName = platform.CName;
                    result.Data.Add(_platform);
                }
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 新增平台
        /// </summary>
        public ResponseDto<string> Create(PlatformCreateOrUpdateDto createPlatform, long creatorUserId)
        {
            var result = new ResponseDto<string>();

            if (string.IsNullOrEmpty(createPlatform.EName))
                throw new CustomException("平台英文名称不能为空", 1);

            if (_platformRepository.IsENameExists(createPlatform.EName))
                throw new CustomException("平台英文名称不能重复", 1);

            var platform = new PlatformCreateOrUpdateDo();
            platform.EName = createPlatform.EName;
            platform.CName = createPlatform.CName;

            var excute = _platformService.ManageCreate(platform, creatorUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取平台详情
        /// </summary>
        public ResponseDto<PlatformCreateOrUpdateDto> GetPlatformDetail(long id)
        {
            var result = new ResponseDto<PlatformCreateOrUpdateDto>();
            result.Data = new PlatformCreateOrUpdateDto();
            if (id <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _platformService.ManageGetPlatformDetail(id);
            if (excute.Success)
            {
                var platform = excute.Data;
                var _platform = new PlatformCreateOrUpdateDto();
                _platform.Id = platform.Id;
                _platform.EName = platform.EName;
                _platform.CName = platform.CName;
                result.Data = _platform;

                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Code = excute.Code;
                result.Msg = excute.Msg;
                result.Success = excute.Success;
            }
            return result;
        }

        /// <summary>
        /// 编辑平台资料
        /// </summary>
        public ResponseDto<string> ModifyRole(PlatformCreateOrUpdateDto modifyPlatform, long modifierUserId)
        {
            var result = new ResponseDto<string>();

            if (modifyPlatform.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (string.IsNullOrEmpty(modifyPlatform.EName))
                throw new CustomException("平台英文名称不能为空", 1);

            var platform = new PlatformCreateOrUpdateDo();
            platform.Id = modifyPlatform.Id;
            platform.EName = modifyPlatform.EName;
            platform.CName = modifyPlatform.CName;

            var excute = _platformService.ManageModify(platform, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 删除平台数据
        /// </summary>
        public ResponseDto<string> DeleteRole(long id, long deleterUserId)
        {
            var result = new ResponseDto<string>();
            if (id <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _platformService.ManageDelete(id, deleterUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 获取拥有平台授权的所有用户
        /// </summary>
        public ResponsePageDto<BmsUsersimpleListDto> GetGrantedUsers(long platformId,int currentPage)
        {
            var result = new ResponsePageDto<BmsUsersimpleListDto>();
            result.Data = new List<BmsUsersimpleListDto>();
            var excute = _bmsUserService.GetPlatformGrantedUsers(platformId, currentPage, out int count);
            if (excute.Success)
            {
                foreach (var user in excute.Data)
                {
                    var _user = new BmsUsersimpleListDto();
                    _user.Id = user.Id;
                    _user.FullName = user.FirstName + " " + user.LastName;
                    result.Data.Add(_user);
                }
                result.Page.TotalCount = count;
                result.Code = 0;
                result.Success = true;
            }
            else
            {
                result.Code = excute.Code;
                result.Msg = excute.Msg;
                result.Success = excute.Success;
            }
            return result;
        }

        /// <summary>
        /// 强制刷新角色字典
        /// </summary>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _platformRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
