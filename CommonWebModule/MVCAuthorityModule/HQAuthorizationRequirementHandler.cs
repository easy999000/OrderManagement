using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthorityModule
{
    /// <summary>
    /// 授权处理程序
    /// </summary>
    public class HQAuthorizationRequirementHandler : AuthorizationHandler<HQAuthorizationRequirement>
    {
        public IHttpContextAccessor _Context { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public HQAuthorizationSignManager _HQAuthorizationSignManager = new HQAuthorizationSignManager();

        public HQAuthorizationStateManager _StateManager;

        public HQAuthorizationRequirementHandler(IHttpContextAccessor Context, HQAuthorizationStateManager StateManager)
        {
            _Context = Context;
            _StateManager = StateManager;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HQAuthorizationRequirement requirement)
        {
            //  context.

            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            var sessionUser = _StateManager.GetLoginUser();

            if (sessionUser == null)
            {

                return Task.CompletedTask;
            }

            //     return Task.CompletedTask;
            //  var RouteData = _Context.HttpContext.GetRouteData();
            //     return Task.CompletedTask;
            // var RouteValue = _Context.HttpContext.GetRouteValue();

            object area = _Context.HttpContext.GetRouteValue("area");
            object controller = _Context.HttpContext.GetRouteValue("controller");
            object action = _Context.HttpContext.GetRouteValue("action");

            if (controller == null)
            {
                ////无法映射的路径会找不到路由信息
                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            ///全匹配
            HQAuthorizationSign sign = new HQAuthorizationSign()
            {
                Area = area != null ? area.ToString() : ""
                ,
                Controller = controller != null ? controller.ToString() : ""
                ,
                Action = action != null ? action.ToString() : ""
            };

            int id = 1;


            id = sessionUser.UserHQAuthorizationSign.GetHQAuthorizationSignID(sign);

            if (id > 0)
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            ///控制器匹配
            ///

            sign = new HQAuthorizationSign()
            {
                Area = area != null ? area.ToString() : ""
               ,
                Controller = controller != null ? controller.ToString() : ""
               ,
                Action = "*"
            };

            id = sessionUser.UserHQAuthorizationSign.GetHQAuthorizationSignID(sign);

            if (id > 0)
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }

            ///控制器区域
            ///

            sign = new HQAuthorizationSign()
            {
                Area = area != null ? area.ToString() : ""
               ,
                Controller = "*"
               ,
                Action = "*"
            };

            id = sessionUser.UserHQAuthorizationSign.GetHQAuthorizationSignID(sign);

            if (id > 0)
            {
                context.Succeed(requirement);

                return Task.CompletedTask;
            }


            Console.WriteLine("授权失败");

            //context.Fail()

            return Task.CompletedTask;

        }


    }


}
