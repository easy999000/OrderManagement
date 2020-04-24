using CommonWebModule.MVCAuthorityModule;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 授权模块初始化配置模块
    /// </summary>
    public static class HQAuthorizationInitializer
    {

        /// <summary>
        /// 配置授权服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHQAuthorization(this IServiceCollection services,Action<HQAuthorizationConfigOption> ConfigOption=null
            )
        {
            HQAuthorizationConfigOption Option = new HQAuthorizationConfigOption();

            ConfigOption?.Invoke(Option);

            services.AddDistributedMemoryCache();

            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<HQAuthorizationStateManager>();

            ///注册自定义授权策略
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAuthorizationHandler, HQAuthorizationRequirementHandler>();

            ///添加路由
            services.AddRouting();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.AccessDeniedPath = Option. AccessDeniedPath;
                options.LoginPath = Option.LoginPath;
                options.LogoutPath = Option.LoginPath;
                options.ExpireTimeSpan = new TimeSpan(0, 5, 0);

            });


            services.AddAuthorization(options =>
            {
                List<HQAuthorizationRequirement> Requirement = new List<HQAuthorizationRequirement>();

                Requirement.Add(new HQAuthorizationRequirement());

                string[] Schemes = new string[] { CookieAuthenticationDefaults.AuthenticationScheme };

                AuthorizationPolicy policy = new AuthorizationPolicy(Requirement, Schemes);

                options.DefaultPolicy = policy;
                options.FallbackPolicy = policy;
                 

                options.AddPolicy(Option.PolicyName, policy => policy.AddRequirements(new HQAuthorizationRequirement())

                );
            });

            return services;
        }

        /// <summary>
        /// 启用授权服务
        /// 一定要放在 UseRouting 下面.放在UseEndpoints 上面.否则会影响系统正常运行.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHQAuthorization(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }


}
