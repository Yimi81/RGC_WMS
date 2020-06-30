using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System;
using RGC.WMS.USA.Application.System.Dto;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    [ApiController]
    public class SystemController : WebApiManageBase
    {
        private readonly ISystemInfoAppService _systemAppService;
        public SystemController(
            ISystemInfoAppService systemAppService)
        {
            _systemAppService = systemAppService;
        }

        [Authorize]
        [HttpPost("create")]
        public JsonResult Create(SystemCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _systemAppService.CreateSystem(request, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpPost("systems/page/get")]
        public JsonResult GetSystemList(SearchFilterDto searchFilter)
        {
            return base.catchError<SystemListDto>(
                   delegate (ResponsePageDto<SystemListDto> result)
                   {
                       result = _systemAppService.GetSystemList(searchFilter);
                       return result;
                   });
        }

        [HttpGet("detail/{id}")]
        [Authorize]
        public JsonResult GetSystemDetail(long id)
        {
            return base.catchError<SystemCreateOrUpdateDto>(
                   delegate (ResponseDto<SystemCreateOrUpdateDto> result)
                   {
                       result = _systemAppService.GetSystemDetail(id);
                       return result;
                   });
        }

        [HttpPost("update")]
        [Authorize]
        public JsonResult Update(SystemCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _systemAppService.ModifySystem(request, loginId);
                       return result;
                   });
        }

        [HttpGet("delete/{systemId}")]
        [Authorize]
        public JsonResult Delete(long systemId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _systemAppService.DeleteSystem(systemId, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpGet("systems/all/get")]
        public JsonResult GetAllSystemList()
        {
            return base.catchError<SystemSimpleListDto>(
                   delegate (ResponsePageDto<SystemSimpleListDto> result)
                   {
                       result = _systemAppService.GetAllSystemList();
                       return result;
                   });
        }

        [HttpGet("dict/refresh")]
        [Authorize]
        public JsonResult ForceRefreshDict()
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result = _systemAppService.ForceRefreshDict();
                       return result;
                   });
        }

        [HttpGet("systems/select/list"), Authorize]
        public JsonResult GetSystemSelectList()
        {
            return base.catchError<SystemListDto>(
                   delegate (ResponsePageDto<SystemListDto> result)
                   {
                       var searchFilter = new SearchFilterDto()
                       {
                           PageSize = 999,
                           CurrentPage = 1
                       };
                       result = _systemAppService.GetSystemList(searchFilter);
                       return result;
                   });
        }
    }
}