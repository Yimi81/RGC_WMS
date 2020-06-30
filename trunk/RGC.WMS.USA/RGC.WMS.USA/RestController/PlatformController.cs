using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Application.System;
using RGC.WMS.USA.Application.System.Dto;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    [ApiController]
    public class PlatformController : WebApiManageBase
    {
        private readonly IPlatformAppService _platformAppService;
        public PlatformController(
            IPlatformAppService platformAppService)
        {
            _platformAppService = platformAppService;
        }

        [Authorize]
        [HttpPost("create")]
        public JsonResult Create(PlatformCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _platformAppService.Create(request, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpPost("list/page/get")]
        public JsonResult GetPlatformList(SearchFilterDto searchFilter)
        {
            return base.catchError<PlatformListDto>(
                   delegate (ResponsePageDto<PlatformListDto> result)
                   {
                       result = _platformAppService.GetPlatformList(searchFilter);
                       return result;
                   });
        }

        [HttpGet("detail/{id}")]
        [Authorize]
        public JsonResult GetPlatformDetail(long id)
        {
            return base.catchError<PlatformCreateOrUpdateDto>(
                   delegate (ResponseDto<PlatformCreateOrUpdateDto> result)
                   {
                       result = _platformAppService.GetPlatformDetail(id);
                       return result;
                   });
        }

        [HttpPost("update")]
        [Authorize]
        public JsonResult Update(PlatformCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _platformAppService.ModifyRole(request, loginId);
                       return result;
                   });
        }

        [HttpGet("delete/{paltformId}")]
        [Authorize]
        public JsonResult Delete(long paltformId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _platformAppService.DeleteRole(paltformId, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpGet("list/simple/get")]
        public JsonResult GetRolesSimpleList()
        {
            return base.catchError<List<PlatformSimpleListDto>>(
                   delegate (ResponseDto<List<PlatformSimpleListDto>> result)
                   {
                       result = _platformAppService.GetPlatformSimpleList();
                       return result;
                   });
        }

        [HttpGet("granted/users")]
        [Authorize]
        public JsonResult GetGrantedUsers(long paltformId, int currentPage = 1)
        {
            return base.catchError<BmsUsersimpleListDto>(
                   delegate (ResponsePageDto<BmsUsersimpleListDto> result)
                   {
                       result = _platformAppService.GetGrantedUsers(paltformId, currentPage);
                       return result;
                   });
        }

        [HttpGet("dict/refresh")]
        [Authorize]
        public JsonResult ForceRefreshDict(long userId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result = _platformAppService.ForceRefreshDict();
                       return result;
                   });
        }
    }
}