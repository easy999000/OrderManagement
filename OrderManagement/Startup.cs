using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommonWebModule.MVCAuthorityModule;
using CommonWebModule.sysConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderManagementDao_Mysql;

namespace OrderManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SysConfigHelper sysConfig = new SysConfigHelper();
            string edition = sysConfig.GetConfig("edition");
            string redisconn = sysConfig.GetConfig("redisconn");
            
            //dev ����
            //////////,cc ����,servertest ������,  ��ʽ
            //////////,Test ������,  ��ʽ
            //////////,Run  ��ʽ

            services.AddSharedSession(o =>
            {
                //o.Configuration = "192.168.18.115:6379,password=12345678";
                o.Configuration = redisconn;
                // o.Configuration = "120.79.20.121:8199,defaultDatabase=15,password=bwerUGF21b";
                o.ApplicationName = "OrderManagement";
                switch (edition.ToLower())
                {
                    case "dev":
                        o.Domain = "zhao.cc";
                        break;
                    case "cc":
                        o.Domain = "zhao.cc";
                        break;
                    case "test":
                        o.Domain = "zhao.cc";
                        break;
                    case "run":
                        o.Domain = "zhao.cc";
                        break;
                    default:   
                        break;
                }
            });

            

            services.AddControllersWithViews().AddRazorRuntimeCompilation();


            ///����mysql����
            services.AddHQMysqlDBContext(options => { options.ConnectionString = Configuration.GetConnectionString("HqOrder"); });


            //���ÿ���Ȩ��
            //services.AddCors(options=>options.AddDefaultPolicy(configure=> configure
            //.AllowAnyOrigin()
            //.AllowAnyMethod()
            //.AllowAnyHeader() 
            //.AllowCredentials()

            //));

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        // .AllowAnyOrigin()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials();
                }));




            ///����Ȩ�޷���
            services.AddHQAuthorization(options =>
            {
                options.AccessDeniedPath = "/Authority/NoAuthority";
                options.LoginPath = "/Authority/NotSigin";
            });


            //services.AddDataProtection()
            //    .PersistKeysToFileSystem(new DirectoryInfo(@"d:\key"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, OrderManagementDB DBContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHQMysqlDBContext(DBContext);

            ////������Ȩ����
            app.UseHQAuthorization();


            //���ÿ���
            //app.UseCors(); 
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authority}/{action=Index}/{upar1?}/{upar2?}/{upar3?}", new { area = "Authority" });
                endpoints.MapControllerRoute(
                    name: "areaDefault",
                    pattern: "{area=Main}/{controller=Authority}/{action=Index}/{upar1?}/{upar2?}/{upar3?}");
            });
        }
    }
}
