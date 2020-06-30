using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RGC.WMS.USA.Application.Bms;
using RGC.WMS.USA.Application.Bms.Dto;
using RGC.WMS.USA.Application.Dto;
using RGC.WMS.USA.Domain;
using RGC.WMS.USA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RGC.WMS.USA.RestControllers
{
    [ApiController]
    public class BmsUserController : WebApiManageBase
    {
        private readonly IBmsUserAppService _bmsUserAppService;
        private readonly IBmsMenuAppService _bmsMenuAppService;
        public BmsUserController(
            IBmsUserAppService bmsUserAppService,
            IBmsMenuAppService bmsMenuAppService)
        {
            _bmsUserAppService = bmsUserAppService;
            _bmsMenuAppService = bmsMenuAppService;
        }

        [Authorize]
        [HttpPost("create")]
        public JsonResult CreateUser(BmsUserCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.CreateBmsUser(request, loginId);
                       return result;
                   });
        }

        [Authorize]
        [HttpPost("users/get")]
        public JsonResult GetUserList(SearchFilterDto searchFilter)
        {
            return base.catchError<BmsUserListDto>(
                   delegate (ResponsePageDto<BmsUserListDto> result)
                   {
                       result = _bmsUserAppService.GetBmsUserList(searchFilter);
                       return result;
                   });
        }

        [HttpGet("detail/{id}")]
        [Authorize]
        public JsonResult GetUserDetail(long id)
        {
            return base.catchError<BmsUserCreateOrUpdateDto>(
                   delegate (ResponseDto<BmsUserCreateOrUpdateDto> result)
                   {
                       result = _bmsUserAppService.GetBmsUserDetail(id);
                       return result;
                   });
        }

        [HttpPost("user/update")]
        [Authorize]
        public JsonResult UpdateUser(BmsUserCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.UpdateBmsUser(request, loginId);
                       return result;
                   });
        }

        [HttpPost("status/update")]
        [Authorize]
        public JsonResult UpdateUserStatus(BmsUserCreateOrUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.UpdateBmsUserStatus(request, loginId);
                       return result;
                   });
        }

        [HttpPost("password/update")]
        [Authorize]
        public JsonResult UpdateUserPassword(BmsUserChangePwdDto input)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.ChangeBmsUserPassword(input, loginId);
                       return result;
                   });
        }

        [HttpGet("delete/{userId}")]
        [Authorize]
        public JsonResult DeleteUser(long userId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.DeleteBmsUser(userId, loginId);
                       return result;
                   });
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail/current")]
        [Authorize]
        public JsonResult GetCurrentUserDetail()
        {
            return base.catchError<BmsUserDto>(
                   delegate (ResponseDto<BmsUserDto> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.GetCurrentUserDetail(loginId);
                       return result;

                   });
        }

        [HttpGet("system/grant/{userId}")]
        public JsonResult GetGrantedSystemIds(long userId)
        {
            return base.catchError<List<long>>(
                   delegate (ResponseDto<List<long>> result)
                   {
                       result = _bmsUserAppService.GetUserSystemIds(userId);
                       return result;
                   });
        }

        [HttpPost("system/grant/update")]
        [Authorize]
        public JsonResult UpdateSystemGrant(BmsUserGrantedUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.UpdateGrantedSystem(request.checkIds, request.UserId, loginId);
                       return result;
                   });
        }

        [HttpGet("role/grant/{userId}")]
        public JsonResult GetGrantedRoleIds(long userId)
        {
            return base.catchError<List<long>>(
                   delegate (ResponseDto<List<long>> result)
                   {
                       result = _bmsUserAppService.GetUserRoleIds(userId);
                       return result;
                   });
        }

        [HttpPost("role/grant/update")]
        [Authorize]
        public JsonResult UpdateRoleGrant(BmsUserGrantedUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.UpdateGrantedRole(request.checkIds, request.UserId, loginId);
                       return result;
                   });
        }

        [HttpGet("platformids/grant/{userId}")]
        public JsonResult GetGrantedPlatformIds(long userId)
        {
            return base.catchError<List<long>>(
                   delegate (ResponseDto<List<long>> result)
                   {
                       result = _bmsUserAppService.GetUserPlatformIds(userId);
                       return result;
                   });
        }

        [HttpPost("platform/grant/update")]
        [Authorize]
        public JsonResult UpdatePlatformGrant(BmsUserGrantedUpdateDto request)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result = _bmsUserAppService.UpdateGrantedPlatform(request.checkIds, request.UserId, loginId);
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
                       result = _bmsUserAppService.ForceRefreshDict();
                       return result;
                   });
        }

        #region Login / Logout

        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public JsonResult Login(LoginInputModel model)
        {
            return base.catchError<BmsUserLoginAttemptDto>(
                   delegate (ResponseDto<BmsUserLoginAttemptDto> result)
                   {
                       result.Data = new BmsUserLoginAttemptDto();
                       if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                       {
                           throw new CustomException("账号密码不能为空", 1);
                       }
                       if (string.IsNullOrWhiteSpace(model.ValidateCode))
                       {
                           throw new CustomException("请填写验证码", 1);
                       }
                       else
                           model.ValidateCode = model.ValidateCode.Trim().ToLower();

                       if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(WebApiManageBase.SessionValidateCode)))
                       {
                           throw new CustomException("验证码失效，请刷新", 1);
                       }
                       if (HttpContext.Session.GetString(WebApiManageBase.SessionValidateCode) != model.ValidateCode)
                       {
                           throw new CustomException("验证码错误", 1);
                       }
                       var excute = _bmsUserAppService.CanLogin(model.UserName, model.Password);
                       if (!excute.Success)
                       {
                           throw new CustomException(excute.Msg, 1);
                       }
                       if (excute.Data != null)
                       {
                           result.Data.LoginId = excute.Data.Id;
                           result.Data.LoginName = excute.Data.FullName;
                           result.Data.StaffId = excute.Data.StaffId;
                           result.Data.AttemptResult = excute.Success;
                       }

                       AuthenticationProperties props = null;
                       if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                       {
                           props = new AuthenticationProperties
                           {
                               IsPersistent = true,
                               ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                           };
                       }
                       else
                       {
                           props = new AuthenticationProperties
                           {
                               ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                           };

                       }
                       var powerList = new List<string>();
                       var powerResp = _bmsMenuAppService.GetPowerNameList(excute.Data.UserMenuExtendDict.Values.Select(p => p.MenuId).ToList());
                       if (powerResp.Code == 0)
                           powerList = powerResp.Data;
                       if (excute.Data.PrimaryOrganizationId <= 0)
                       {
                           var organization = excute.Data.UserOrganizationDict.Values.FirstOrDefault();
                           if (organization != null)
                           {
                               excute.Data.PrimaryOrganizationId = organization.OrganizationId;
                           }
                       }
                       //登录成功,
                       var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                           new Claim(ClaimTypes.Name, excute.Data.LoginName),
                           new Claim(ClaimTypes.Sid, excute.Data.Id.ToString()),
                           new Claim(ClaimTypes.PrimaryGroupSid, excute.Data.PrimaryOrganizationId.ToString()),
                           new Claim(ClaimTypes.PrimaryGroupSid, excute.Data.PrimaryOrganizationId.ToString()),
                           new Claim(ClaimTypes.Role,string.Join(",", excute.Data.UserRoleDict.Values.Select(p=>p.RoleId).ToList())),
                           new Claim(ClaimTypes.AuthorizationDecision,string.Join(",", powerList)),

                       }, CookieAuthenticationDefaults.AuthenticationScheme));

                       Task.Run(async () =>
                       {
                           await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, props);
                       }).Wait();

                       result.Success = true;
                       result.Code = 0;
                       return result;
                   });
        }

        [HttpGet("logout")]
        public JsonResult logout()
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       Task.Run(async () =>
                       {
                           //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
                           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                       }).Wait();

                       result.Success = true;
                       result.Code = 0;
                       return result;
                   });
        }

        #endregion

        /// <summary>
        /// 获取管理员的菜单树
        /// 系统管理->用户管理->权限编辑
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("menuTree/query")]
        public JsonResult GetUserWholeMenuTree(Int64 userId, long organizationId = 0)
        {
            return base.catchError<BmsUserMenuTreeDto>(
                   delegate (ResponseDto<BmsUserMenuTreeDto> result)
                   {
                       result.Data = new BmsUserMenuTreeDto();
                       result = _bmsUserAppService.GetUserWholeMenuTree(userId, organizationId);
                       return result;
                   });
        }


        [HttpPost("menuTree/modify")]
        public JsonResult ModifyUserMenuTree(BmsUserMenuTreeModifyDto request)
        {
            return base.catchError<int>(
                   delegate (ResponseDto<int> result)
                   {
                       var loginId = 0;
                       int.TryParse(User.FindFirst(ClaimTypes.Sid).Value, out loginId);
                       result.Data = 0;
                       result = _bmsUserAppService.ManageModifyUserMenuTree(request, loginId);
                       return result;
                   });
        }
        /// <summary>
        /// 后台设置用户当前职位
        /// </summary>
        /// <returns></returns>
        [Route("current/organization/set")]
        [HttpGet]
        public JsonResult SetCurrentOrganization(string organizationId)
        {
            return base.catchError<string>(
                   delegate (ResponseDto<string> result)
                   {
                       var props = new AuthenticationProperties
                       {
                           IsPersistent = true,
                           ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                       };
                       var user = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, User.FindFirst(ClaimTypes.Name).Value),
                            new Claim(ClaimTypes.Sid, User.FindFirst(ClaimTypes.Sid).Value),
                            new Claim(ClaimTypes.PrimaryGroupSid, organizationId),
                            new Claim(ClaimTypes.Role,string.Join(",", User.FindFirst(ClaimTypes.Role).Value)),
                            new Claim(ClaimTypes.AuthorizationDecision,string.Join(",", User.FindFirst(ClaimTypes.AuthorizationDecision).Value)),

                        }, CookieAuthenticationDefaults.AuthenticationScheme));
                       Task.Run(async () =>
                       {
                           //注销登录的用户，相当于ASP.NET中的FormsAuthentication.SignOut  
                           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                       }).Wait();
                       Task.Run(async () =>
                       {
                           await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user, props);
                       }).Wait();
                       result.Code = 0;
                       result.Success = true;
                       return result;
                   });
        }
    }
    public class AccountOptions
    {
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
    }
}