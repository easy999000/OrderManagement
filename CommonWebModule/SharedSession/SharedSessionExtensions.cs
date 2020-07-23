using CommonWebModule.SharedSession;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 共享session
    /// </summary>
    public static class SharedSession
    {
        /// <summary>
        /// 添加共享session组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ConfigOpeion"></param>
        public static void AddSharedSession(this IServiceCollection services, Action<SharedSessionOption> ConfigOpeion
            )
        {
            ////配置共享缓存组件
            SharedSessionOption Option = new SharedSessionOption();

            ConfigOpeion(Option);

            //services.ConfigureApplicationCookie(o=>
            //{
            //    o.CookieManager.
            //});


            //if (string.IsNullOrWhiteSpace(Option.PrefixName))
            //{
            //    throw new Exception("PrefixName 不可以为空");
            //}
            //Option.ConfigurationOptions.ChannelPrefix = Option.PrefixName;

            ////秘钥提供程序

            services.AddSingleton<IXmlRepository, ShareSessionXmlRepository>();

            services.AddDataProtection(configure =>
            {
                //configure.ApplicationDiscriminator = DateTime.Now.ToFileTime().ToString();

                //Console.WriteLine(configure.ApplicationDiscriminator);
                //  configure.
                //  configure.SetApplicationName("newP.Web");

            })
            .SetApplicationName(Option.ApplicationName)
            .AddKeyManagementOptions(o =>
            {
                o.XmlRepository = new ShareSessionXmlRepository();
            });


            services.AddStackExchangeRedisCache(o =>
            {
                //if (o.ConfigurationOptions==null)
                //{
                //    o.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions();
                //}

                //o.ConfigurationOptions.DefaultDatabase = 1;
                //o.ConfigurationOptions.Password = "Bus01#dwjwlxs";
                //o.ConfigurationOptions.ServiceName = "r-wz911aq5ebvzoo1f97pd.redis.rds.aliyuncs.com";
                // o.ConfigurationOptions.

                o.Configuration = Option.Configuration;
                //o.ConfigurationOptions=new StackExchange.Redis.ConfigurationOptions();
                ////o.ConfigurationOptions.ServiceName
                //o.ConfigurationOptions.ChannelPrefix = Option.ApplicationName;
                o.InstanceName = Option.InstanceName;
                //  o.ConfigurationOptions.DefaultDatabase = Option.DefaultDatabase;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                //options.Cookie.IsEssential = true;
                options.Cookie.Domain = Option.Domain;
                options.Cookie.Name = Option.ApplicationName + ".Session";
            });



        }


        public static string GetSecondLevelDomain(this HttpContext Context)
        {
            if (Context == null)
            {
                return "";
            }

            if (Context.Request == null)
            {
                return "";
            }
            if (Context.Request.Host == null)
            {
                return "";
            }
            if (Context.Request.Host.Host == null)
            {
                return "";
            }
            string[] Names = Context.Request.Host.Host.Split(".");
            if (Names.Length < 3)
            {
                return Context.Request.Host.Host;
            }
            return $"{Names[Names.Length - 2]}.{Names[Names.Length - 1]}";
        }
    }
}
