using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Bms;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RGC.WMS.USA.RestControllers
{
    [ApiController]
    public class OrganizationController : WebApiManageBase
    {
        private readonly IBmsOrganizationAppService _bmsOrganizationAppService;
        public OrganizationController(
            IBmsOrganizationAppService bmsOrganizationAppService)
        {
            _bmsOrganizationAppService = bmsOrganizationAppService;
        }

        [HttpPost("create"), Authorize]
        public JsonResult Create(BmsOrganizationDto request)
        {
            return base.catchError<BmsOrganizationDto>(
                   delegate (ResponseDto<BmsOrganizationDto> result)
                   {
                       result.Data = new BmsOrganizationDto();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.ManageCreateOrganization(request, loginId);
                       return result;
                   });
        }

        [HttpPost("modify"), Authorize]
        public JsonResult Modify(BmsOrganizationDto request)
        {
            return base.catchError<BmsOrganizationDto>(
                   delegate (ResponseDto<BmsOrganizationDto> result)
                   {
                       result.Data = new BmsOrganizationDto();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.ManageModifyOrganization(request, loginId);
                       return result;
                   });
        }

        [HttpGet("delete"), Authorize]
        public JsonResult Delete(Int64 organizationId)
        {
            return base.catchError<BmsOrganizationDto>(
                   delegate (ResponseDto<BmsOrganizationDto> result)
                   {
                       result.Data = new BmsOrganizationDto();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.ManageDeleteOrganization(organizationId, loginId);
                       return result;
                   });
        }

        [HttpGet("tree"), Authorize]
        public JsonResult GetTree()
        {
            return base.catchError<List<BmsOrganizationTreeDto>>(
                   delegate (ResponseDto<List<BmsOrganizationTreeDto>> result)
                   {

                       result.Data = new List<BmsOrganizationTreeDto>();
                       result = _bmsOrganizationAppService.ManageGetOrganizationTree();
                       return result;
                   });
        }

        [HttpGet("detail"), Authorize]
        public JsonResult GetDetail(long orgId)
        {
            return base.catchError<BmsOrganizationDto>(
                   delegate (ResponseDto<BmsOrganizationDto> result)
                   {
                       result.Data = new BmsOrganizationDto();
                       result = _bmsOrganizationAppService.ManageGetOrganizationDetail(orgId);
                       return result;
                   });
        }

        [HttpGet("children"), Authorize]
        public JsonResult GetChildren(long orgId)
        {
            return base.catchError<List<BmsOrganizationDto>>(
                   delegate (ResponseDto<List<BmsOrganizationDto>> result)
                   {
                       result.Data = new List<BmsOrganizationDto>();
                       result = _bmsOrganizationAppService.ManageGetChildrenOrganization(orgId);
                       return result;
                   });
        }

        /// <summary>
        /// 某组织架构下user列表
        /// </summary>
        [HttpGet("userList"), Authorize]
        public JsonResult GetOrganizationUserList(long orgId)//, string key, int pageSize, int currentPage)
        {
            return base.catchError<BmsUserDto>(
                   delegate (ResponsePageDto<BmsUserDto> result)
                   {
                       result = _bmsOrganizationAppService.GetOrganizationUserList(orgId, null, 999, 1);
                       return result;
                   });
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        [HttpGet("otherUserList"), Authorize]
        public JsonResult GetOtherOrganizationUserList(long orgId)
        {
            return base.catchError<BmsUserDto>(
                   delegate (ResponsePageDto<BmsUserDto> result)
                   {
                       result = _bmsOrganizationAppService.GetOtherOrganizationUserList(orgId, null, 999, 1);
                       return result;
                   });
        }

        /// <summary>
        /// 组织架构添加用户
        /// </summary>
        [HttpPost("add/user"), Authorize]
        public JsonResult AddUser(BmsOrganizationUserCreateDto request)//long userId, long orgId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.AddUser(request, loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 组织架构移除用户
        /// </summary>
        [HttpGet("remove/user"), Authorize]
        public JsonResult RemoveUser(long userId, long orgId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.RemoveUser(userId, orgId, loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 获取组织架构权限菜单
        /// </summary>
        [HttpGet("menuTree/query"), Authorize]
        public JsonResult GetRoleWholeMenuTree(Int64 organizationId)
        {
            return base.catchError<BmsMenuTreeDto>(
                   delegate (ResponseDto<BmsMenuTreeDto> result)
                   {
                       result.Data = new BmsMenuTreeDto();
                       result = _bmsOrganizationAppService.GetWholeMenuTree(organizationId);
                       return result;
                   });
        }

        /// <summary>
        /// 编辑组织架构菜单树
        /// </summary>
        [HttpPost("menuTree/modify"), Authorize]
        public JsonResult ModifyRoleMenuTree(BmsOrganizationMenuTreeModifyDto request)
        {
            return base.catchError<int>(
                   delegate (ResponseDto<int> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsOrganizationAppService.ModifyMenuTree(loginId, request);
                       return result;
                   });
        }

        [HttpGet("dict/refresh"), Authorize]
        public JsonResult ForceRefreshDict(long userId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result = _bmsOrganizationAppService.ForceRefreshDict();
                       return result;
                   });
        }
    }
}