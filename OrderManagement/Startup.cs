using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonWebModule.MVCAuthorityModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            ///启动mysql服务
            services.AddHQMysqlDBContext(options => { options.ConnectionString = Configuration.GetConnectionString("HqOrder"); });
            ///启动权限服务
            services.AddHQAuthorization(options =>
            {
                options.AccessDeniedPath = "/Authority/NoAuthority";
                options.LoginPath = "/Authority/NotSigin";
            });
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

            ////启用授权服务
            app.UseHQAuthorization();


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
