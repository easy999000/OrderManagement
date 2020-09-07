using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WindwosAndLinuxServices.Tools
{
    public class SysLog
    {


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static bool AddLog(string title, string msg)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(title))
                {
                    title = "log.log";
                }
                if (!title.Contains("."))
                {
                    title += ".log";
                }

                if (msg == null)
                {
                    msg = "";
                }

                string basePath = Directory.GetCurrentDirectory();

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine(basePath+"aaaaa");
                //}

                //////////
                ///


                //Console.WriteLine("-------------------------------");
                //var v1 = Environment.CurrentDirectory;
                //var v2 = Directory.GetCurrentDirectory();

                //var v3 = Process.GetCurrentProcess().MainModule.FileName;
                //var v4 = AppDomain.CurrentDomain.BaseDirectory;
                //var v5 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

                string timeStr = $"[{DateTime.Now.ToString()}] ";

                /////////////

                string DirPath = Path.Combine(basePath, "Log", DateTime.Now.ToString("yy-MM-dd"));

                string FilePath = Path.Combine(DirPath, title);

                HQFile.Append(FilePath, timeStr + msg + "\r\n\r\n");

                return true;

            }
            catch (Exception ex)
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                return false;
            }

        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        public static bool AddLog(string title, object msg)
        {
            try
            {
                string str = "";

                if (msg == null)
                {
                    str = "null";
                }
                else
                {
                    str = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                }

                return AddLog(title, str);
            }
            catch (Exception ex)
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                return false;
            }


        }


        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        public static bool AddExceptionLog(string title, Exception ex)
        {
            return AddLog(title, ex);

        }

    }
}
