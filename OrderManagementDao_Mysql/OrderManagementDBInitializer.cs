
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManagementDao_Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 授权模块初始化配置模块
    /// </summary>
    public static class OrderManagementDBInitializer
    { 
        /// <summary>
        /// 配置授权服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHQMysqlDBContext(this IServiceCollection services, Action<OrderManagementDBOptions> ConfigOption 
            )
        {
            OrderManagementDBOptions Option = new OrderManagementDBOptions();
            ConfigOption?.Invoke(Option);
            OrderManagementDB.ConnectionString = Option.ConnectionString;
            services.AddDbContext<OrderManagementDB>();
            return services;

        }

        /// <summary>
        /// 启用授权服务
        /// 一定要放在 UseRouting 下面.放在UseEndpoints 上面.否则会影响系统正常运行.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHQMysqlDBContext(this IApplicationBuilder app, OrderManagementDB context)
        {
            

            context.Database.Migrate();

            return app;
        }
    }


}
