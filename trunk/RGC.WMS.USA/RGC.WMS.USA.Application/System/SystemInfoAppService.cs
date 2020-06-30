using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Domain.Do;
using RGC.WMS.USA.Domain.Entities.System.Do;
using RGC.WMS.USA.Domain.Repositories.System;
using RGC.WMS.USA.Domain.Services.System;
using System;
using System.Collections.Generic;

namespace RGC.WMS.USA.Application.System
{
    public class SystemInfoAppService : ISystemInfoAppService, IAppService
    {
        private readonly ISystemInfoRepository _systemInfoRepository;
        private readonly ISystemInfoService _systemInfoService;


        public SystemInfoAppService(
            ISystemInfoRepository systemInfoRepository,
            ISystemInfoService systemInfoService
            )
        {
            _systemInfoRepository = systemInfoRepository;
            _systemInfoService = systemInfoService;
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        public ResponseDto<string> CreateSystem(SystemCreateOrUpdateDto createSystem, long creatorUserId)
        {
            var result = new ResponseDto<string>();

            if (string.IsNullOrEmpty(createSystem.Name))
                throw new CustomException("网站名称不能为空", 1);

            if (_systemInfoRepository.IsNameExists(createSystem.Name))
                throw new CustomException("网站名称不能重复", 1);

            var system = new SystemCreateOrUpdateDo();
            system.Name = createSystem.Name;
            system.DisplayName = createSystem.DisplayName;
            system.DomainName = createSystem.DomainName;
            system.IPAddress = createSystem.IPAddress;
            system.isStatic = createSystem.isStatic;
            system.Desc = createSystem.Desc;

            var excute = _systemInfoService.CreateSystem(system, creatorUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台分页查询系统列表
        /// </summary>
        public ResponsePageDto<SystemListDto> GetSystemList(SearchFilterDto searchFilter)
        {
            var result = new ResponsePageDto<SystemListDto>();
            result.Data = new List<SystemListDto>();
            var searchDo = new SearchFilterDo()
            {
                CurrentPage = searchFilter.CurrentPage,
                PageSize = searchFilter.PageSize,
                SearchKey = searchFilter.SearchKey,
                Sorting = searchFilter.Sorting
            };
            var excute = _systemInfoRepository.PageQuery(searchDo, out int count);

            if (excute.Count > 0)
            {
                foreach (var system in excute)
                {
                    var _system = new SystemListDto();
                    _system.Id = system.Id;
                    _system.Name = system.Name;
                    _system.DisplayName = system.DisplayName;
                    _system.DomainName = system.DomainName;
                    _system.IPAddress = system.IPAddress;
                    _system.IsStatic = system.isStatic;
                    result.Data.Add(_system);
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
        /// 获取系统详情
        /// </summary>
        public ResponseDto<SystemCreateOrUpdateDto> GetSystemDetail(long systemId)
        {
            var result = new ResponseDto<SystemCreateOrUpdateDto>();
            result.Data = new SystemCreateOrUpdateDto();
            if (systemId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _systemInfoService.GetSystemDetail(systemId);
            if (excute.Success)
            {
                var system = excute.Data;
                var _system = new SystemCreateOrUpdateDto();
                _system.Id = system.Id;
                _system.Name = system.Name;
                _system.DisplayName = system.DisplayName;
                _system.DomainName = system.DomainName;
                _system.IPAddress = system.IPAddress;
                _system.isStatic = system.isStatic;
                _system.Desc = system.Desc;
                result.Data = _system;

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
        /// 编辑系统资料
        /// </summary>
        public ResponseDto<string> ModifySystem(SystemCreateOrUpdateDto modifySystem, long modifierUserId)
        {
            var result = new ResponseDto<string>();

            if (modifySystem.Id <= 0)
                throw new CustomException("请求数据异常", 1);

            if (string.IsNullOrEmpty(modifySystem.Name))
                throw new CustomException("网站名称不能为空", 1);

            var system = new SystemCreateOrUpdateDo();
            system.Id = modifySystem.Id;
            system.Name = modifySystem.Name;
            system.DisplayName = modifySystem.DisplayName;
            system.DomainName = modifySystem.DomainName;
            system.IPAddress = modifySystem.IPAddress;
            system.isStatic = modifySystem.isStatic;
            system.Desc = modifySystem.Desc;

            var excute = _systemInfoService.ModifySystem(system, modifierUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 删除系统数据
        /// </summary>
        public ResponseDto<string> DeleteSystem(long systemId, long deleterUserId)
        {
            var result = new ResponseDto<string>();
            if (systemId <= 0)
                throw new CustomException("请求数据异常", 1);

            var excute = _systemInfoService.DeleteSystem(systemId, deleterUserId);
            result.Code = excute.Code;
            result.Msg = excute.Msg;
            result.Success = excute.Success;
            return result;
        }

        /// <summary>
        /// 后台查询全部系统列表,选择列表
        /// </summary>
        public ResponsePageDto<SystemSimpleListDto> GetAllSystemList()
        {
            var result = new ResponsePageDto<SystemSimpleListDto>();
            result.Data = new List<SystemSimpleListDto>();
            var excute = _systemInfoRepository.AllGet();

            if (excute.Count > 0)
            {
                foreach (var system in excute)
                {
                    var _system = new SystemSimpleListDto();
                    _system.Id = system.Id;
                    _system.Name = system.Name;
                    _system.DisplayName = system.DisplayName;
                    result.Data.Add(_system);
                }
            }
            result.Code = 0;
            result.Success = true;
            return result;
        }

        /// <summary>
        /// 强制刷新用户字典
        /// </summary>
        /// <returns></returns>
        public ResponseDto<string> ForceRefreshDict()
        {
            var result = new ResponseDto<string>();
            _systemInfoRepository.ForceRefreshDict();
            result.Code = 0;
            result.Success = true;
            return result;
        }
    }
}
