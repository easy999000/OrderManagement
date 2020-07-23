using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.sysConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class SysConfigHelper
    {
        public string GetConfig(string SectionKey)
        {

            //添加 json 文件路径
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"config/sysConfig.json");
            //创建配置根对象
            var configurationRoot = builder.Build();

            //取配置根下的 name 部分
            var Section = configurationRoot.GetSection(SectionKey);
            if (Section!=null)
            {
                if (Section.Value!=null)
                {
                    return Section.Value;
                }
            }
            return "";
            ////取配置根下的 name 部分
            //var nameSection = configurationRoot.GetSection("edition");
            ////取配置根下的 family 部分
            //var test = configurationRoot.GetSection("appsetting:name");

            ////取配置根下的 family 部分
            //var familySection = configurationRoot.GetSection("family");
            ////取配置根下的 family 部分
            //var appsetting = configurationRoot.GetSection("appsetting");
            //////取 family 部分下的 mother 部分下的 name 部分
            //var name = appsetting.GetSection("name");
            //////取 family 部分下的 mother 部分下的 name 部分
            //var age = appsetting.GetSection("age");
            //////取 family 部分下的 father 部分下的 age 部分
            ////var fatherAgeSection = familySection.GetSection("father").GetSection("age");


        }
    }
}
