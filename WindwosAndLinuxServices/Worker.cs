using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WindwosAndLinuxServices.Tools;

namespace WindwosAndLinuxServices
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

 

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SysLog.AddLog("������־", "ExecuteAsync");
            //path();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            SysLog.AddLog("������־", "StartAsync");
            return base.StartAsync(cancellationToken);  
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            SysLog.AddLog("������־", "StartAsync");
            return base.StopAsync(cancellationToken);       
        }
    }
}
