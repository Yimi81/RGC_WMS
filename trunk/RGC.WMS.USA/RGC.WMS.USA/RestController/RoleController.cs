using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Bms;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestController
{
    [ApiController]
    public class RoleController : WebApiManageBase
    {
        private readonly IBmsRoleAppService _roleAppService;
        public RoleController(
            IBmsRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }

        [Authorize]
        [HttpPost("create")]
        public JsonResult Create(BmsRoleCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _roleAppService.CreateRole(request, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpGet("roles/get")]
        public JsonResult GetRolesList(string key)
        {
            return base.catchError<List<BmsRoleListDto>>(
                   delegate (ResponseDto<List<BmsRoleListDto>> result)
                   {
                       result = _roleAppService.ManageGetRoleList(key);
                       return result;
                   });
        }

        [HttpGet("detail/{id}")]
        [Authorize]
        public JsonResult GetRoleDetail(long id)
        {
            return base.catchError<BmsRoleCreateOrUpdateDto>(
                   delegate (ResponseDto<BmsRoleCreateOrUpdateDto> result)
                   {
                       result = _roleAppService.GetRoleDetail(id);
                       return result;
                   });
        }

        [HttpPost("update")]
        [Authorize]
        public JsonResult Update(BmsRoleCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _roleAppService.ModifyRole(request, loginId);
                       return result;
                   });
        }

        [HttpGet("delete/{roleId}")]
        [Authorize]
        public JsonResult Delete(long roleId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _roleAppService.DeleteRole(roleId, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpGet("list/simple/get")]
        public JsonResult GetRolesSimpleList()
        {
            return base.catchError<BmsRoleSimpleListDto>(
                   delegate (ResponsePageDto<BmsRoleSimpleListDto> result)
                   {
                       result = _roleAppService.GetRoleSimpleList();
                       return result;
                   });
        }

        [HttpGet("granted/users")]
        [Authorize]
        public JsonResult GetGrantedUsers(long roleId, int currentPage = 1)
        {
            return base.catchError<BmsUsersimpleListDto>(
                   delegate (ResponsePageDto<BmsUsersimpleListDto> result)
                   {
                       result = _roleAppService.GetGrantedUsers(roleId, currentPage);
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
                       result = _roleAppService.ForceRefreshDict();
                       return result;
                   });
        }
    }
}