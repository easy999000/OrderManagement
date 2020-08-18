using Microsoft.Extensions.DependencyInjection;
using MQServer;
using MQServer.MQService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    static class MQServiceExtensions
    {
        /// <summary>
        /// 添加共享session组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ConfigOpeion"></param>
        public static void AddMQService(this IServiceCollection Services
            )
        {

            var MQservice = new MainService();

            MQservice.Start();

            Services.AddSingleton(typeof(MainService), MQservice);



        }
    }
}
