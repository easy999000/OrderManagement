using Microsoft.VisualBasic;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MQServer.Tools
{
    /// <summary>
    /// 控制台输出要用这个类,不然会卡死
    /// </summary>
    public class Log
    {

        static DataQueue<string> LogQueue = new DataQueue<string>();

        static Log()
        {
            Run();
        }
        public static void WriteLine(string Msg)
        {
            LogQueue.Enqueue(Msg + "\r\n");
        }
        public static void Write(string Msg)
        {
            if (LogQueue.Count>20000)
            {
                //var oldQue = LogQueue;
                LogQueue = new DataQueue<string>();
                
            }
            LogQueue.Enqueue(Msg);
        }

        static void Run()
        {
            Task.Run(() =>
          {
              while (true)
              {
                  try
                  {  
                       
                      string log = LogQueue.Dequeue();
                      Console.Write(log);
                  }
                  catch (Exception)
                  {

                  }

              }

          });
        }


    }
}
