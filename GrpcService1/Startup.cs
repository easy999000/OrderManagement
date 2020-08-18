using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GrpcService1.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQServer.Tools;

namespace GrpcService1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMQService();


            services.AddGrpc(o =>
            {
                

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => builder.Run(async context => await ErrorEvent(context)));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MQWebApiMsgServer>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }



        public Task ErrorEvent(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;

            Log.WriteLine("!!!!!!!!!!!!!!!!!");
            Log.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(error));
            Log.WriteLine("!!!!!!!!!!!!!!!!!");


            //LogHelper.Write("Global\\Error", error.Message, error.StackTrace);
            //return context.Response.WriteAsync(JsonHelper.ToJson(new RequestResult(444, "系统未知异常，请联系管理员")), Encoding.GetEncoding("GBK"));
            return Task.CompletedTask;
        }


    }
}
