using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RGC.WMS.USA.Domain.Entities.System.Enum;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RGC.WMS.USA.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        // private readonly IBmsUserAppService _bmsUserAppService;
        private IHttpContextAccessor _httpContextAccessor;
        public PermissionHandler(
            //IBmsUserAppService bmsUserAppService,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            // _bmsUserAppService = bmsUserAppService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   PermissionRequirement requirement)
        {
            //用户未登录
            if (!context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }
            //if (!context.User.HasClaim(c => c.Type == JwtClaimTypes.Subject))
            //{
            //    return Task.CompletedTask;
            //}




            RouteEndpoint filterContext = context.Resource as RouteEndpoint;

            //当前访问的Controller
            string controllerName = filterContext.RoutePattern.Defaults["Controller"].ToString();//通过ActionContext类的RouteData属性获取Controller的名称：Home
                                                                                                 //当前访问的Action
            string actionName = filterContext.RoutePattern.Defaults["Action"].ToString();//通过ActionContext类的RouteData属性获取Action的名称：Index

            var ControllerPermission = ManagePermissions.GetPermissions().FirstOrDefault(p => p.ControllerName == controllerName && p.ActionName == actionName);

            if (ControllerPermission == null)
            {
                context.Succeed(requirement);
            }
            else
            {
                //接口权限
                var Permissions = ControllerPermission.AuthorizeCodes.ToList();
                if (Permissions == null || Permissions.Count <= 0)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                //var userId = Convert.ToInt64(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
                //var organizationId = Convert.ToInt64(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.PrimaryGroupSid).Value);
                //var UserPermissionsList = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value.Split(',').ToList();

                //用户权限
                // var excute = _bmsUserAppService.HasPower(userId, Permissions.ToList(), organizationId);
                //if (excute.Code!=0)
                //{
                //    context.Fail();
                //    return Task.CompletedTask;
                //}
                var authList = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value.ToLower().Split(',').ToList();
                if (authList.Contains(((int)StaticRoleIds.SuperAdmin).ToString()))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                var UserPermissionsList = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.AuthorizationDecision).Value.ToLower().Split(',').ToList();

                Permissions = Permissions.Select(p => p.ToLower()).ToList();
                //if (excute.Code==0)
                if (UserPermissionsList.Contains(Permissions.First()))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //string requestType = filterContext.HttpContext.Request.Headers["X-Requested-With"];
                    //if (!string.IsNullOrEmpty(requestType) && requestType.Equals("XMLHttpRequest", StringComparison.CurrentCultureIgnoreCase))
                    //{
                    //    //ajax 的错误返回
                    //    filterContext.Result = new StatusCodeResult(499); //自定义错误号 ajax请求错误 可以用来错没有权限判断 也可以不写 用默认的

                    //    context.Fail();
                    //}
                    //else
                    //{
                    //    context.Fail();
                    //}

                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
