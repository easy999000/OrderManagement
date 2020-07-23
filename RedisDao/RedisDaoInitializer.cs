
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace RedisDao
{
    /// <summary>
    /// 
    /// </summary>
    public static class RedisDaoInitializer
    {


        /// <summary>
        /// 配置授权服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisDao(this IServiceCollection services, Action<RedisDaoInitializerOptions> ConfigOption    
            )
        {
            RedisDaoInitializerOptions Options = new RedisDaoInitializerOptions();
            ConfigOption(Options);
            //redis缓存
            var section = Options.ConfigurationSection;
            string _connectionString = section.GetSection("Connection").Value;//连接字符串
            string _instanceName = section.GetSection("InstanceName").Value; //实例名称
            int _defaultDB = int.Parse(section.GetSection("DefaultDB").Value ?? "0"); //默认数据库           
            services.AddSingleton(new RedisHelper(_connectionString, _instanceName, _defaultDB));


            return services;

        }

        /// <summary>
        /// 启用授权服务
        /// 一定要放在 UseRouting 下面.放在UseEndpoints 上面.否则会影响系统正常运行.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRedisDao(this IApplicationBuilder app
            )
        {



            return app;
        }

    }
}
