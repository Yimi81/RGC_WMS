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
    public class BmsMenuController : WebApiManageBase
    {
        private readonly IBmsMenuAppService _bmsMenuAppService;
        public BmsMenuController(
            IBmsMenuAppService bmsMenuAppService)
        {
            _bmsMenuAppService = bmsMenuAppService;
        }

        [HttpPost("create"), Authorize(Policy = "Permission")]
        public JsonResult Create(BmsMenuDto request)
        {
            return base.catchError<BmsMenuDto>(
                   delegate (ResponseDto<BmsMenuDto> result)
                   {
                       result.Data = new BmsMenuDto();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsMenuAppService.CreateMenu(request, loginId);
                       return result;
                   });
        }

        [HttpPost("modify"), Authorize(Policy = "Permission")]
        public JsonResult Modify(BmsMenuDto request)
        {
            return base.catchError<BmsMenuDto>(
                   delegate (ResponseDto<BmsMenuDto> result)
                   {
                       result.Data = new BmsMenuDto();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsMenuAppService.ModifyMenu(request, loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 保存排序
        /// </summary>
        [HttpPost("save/seqno"), Authorize(Policy = "Permission")]
        public JsonResult SaveSeqNo(List<BmsMenuSeqNoDto> request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result.Data = "";
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsMenuAppService.ModifyMenuSeqNo(request, loginId);
                       return result;
                   });
        }

        [HttpGet("delete"), Authorize(Policy = "Permission")]
        public JsonResult Delete(Int64 menuId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result.Data = "";
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsMenuAppService.DeleteMenu(menuId, loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 获取菜单管理左侧树
        /// </summary>
        [HttpGet("tree"), Authorize]
        public JsonResult TreeGet()
        {
            return base.catchError<List<BmsMenuTreeDto>>(
                   delegate (ResponseDto<List<BmsMenuTreeDto>> result)
                   {
                       result.Data = new List<BmsMenuTreeDto>();
                       result = _bmsMenuAppService.GetMenuTree(0);
                       return result;
                   });
        }

        [HttpGet("detail"), Authorize(Policy = "Permission")]
        public JsonResult DetailGet(int id)
        {
            return base.catchError<BmsMenuTreeDto>(
                   delegate (ResponseDto<BmsMenuTreeDto> result)
                   {
                       result.Data = new BmsMenuTreeDto();
                       result = _bmsMenuAppService.GetMenuDetail(id);
                       return result;
                   });
        }

        /// <summary>
        /// 后台左侧菜单栏
        /// </summary>
        [HttpGet("query"), Authorize]
        public JsonResult GetSystemMenuTree()
        {
            return base.catchError<List<BmsMenuTreeDto>>(
                   delegate (ResponseDto<List<BmsMenuTreeDto>> result)
                   {
                       result.Data = new List<BmsMenuTreeDto>();
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       var orgId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.PrimaryGroupSid).Value, out orgId);
                       result = _bmsMenuAppService.GetPermissionSystemMenuTree(1, loginId, orgId);
                       return result;
                   });
        }

        [HttpGet("dict/refresh"), Authorize(Policy = "Permission")]
        public JsonResult ForceRefreshDict(long userId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       result = _bmsMenuAppService.ForceRefreshDict();
                       return result;
                   });
        }
    }
}