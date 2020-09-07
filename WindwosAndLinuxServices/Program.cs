using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WindwosAndLinuxServices.Tools;

namespace WindwosAndLinuxServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var builder = Host.CreateDefaultBuilder(args)
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.AddHostedService<Worker>();

                   });

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                builder = builder.UseWindowsService();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                builder = builder.UseSystemd();
            }



            return builder;
        }


        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {


            Exception ex1 = (Exception)args.ExceptionObject;
            SysLog.AddExceptionLog("MainException", ex1);

            SysLog.AddLog("UnhandledExceptionEventArgs", args);


        }

    }
}
