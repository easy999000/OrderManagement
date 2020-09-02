using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpHelper.Tests
{
    [TestClass()]
    public class HttpHelperTests
    {
        [TestMethod()]
        public void Post_JsonToStringTest()
        {
            posttest4();

            posttest2();

            posttest3();


        }

        void posttest2()
        {
            string host = "http://commonmanage.hq.cc/api/Log/AddLog";

            HttpHelper helper = new HttpHelper();

            Common_Log log = new Common_Log();
            log.Function = "Post_JsonToStringTest";
            log.Msg = "测试消息";
            log.Title = "测试httphelper";
            log.Type = 1;

            var res = helper.Post_JsonToStringAsync(host, log).Result;

        }
        void posttest3()
        {
            string host = "https://fanyi.baidu.com/langdetect";

            HttpHelper helper = new HttpHelper();

            List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("query", "fuck you"));



            var res = helper.Post_FormToStringAsync(host, data).Result;

        }
        void posttest4()
        {
            string host = "https://docs.microsoft.com/zh-cn/dotnet/csharp/language-reference/builtin-types/integral-numeric-types?f1url=%3FappId%3DDev16IDEF1%26l%3DZH-CN%26k%3Dk(long_CSharpKeyword);k(DevLang-csharp)%26rd%3Dtrue";

            HttpHelper helper = new HttpHelper();
             

            var res = helper.Get_StringAsync(host).Result;

        }

        


    }
    public class Common_Log
    {
        /// <summary>
        /// 日志类型,1 普通消息 ,2 系统异常
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 日志方法名字
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Msg { get; set; }
    }
}