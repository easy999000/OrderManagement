using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MQClient.Tools
{
    /// <summary>
    /// 数据缓存
    /// </summary>
    /// <typeparam name="tData"></typeparam>
    public class DataQueue<tData> where tData : class
    {
        /// <summary>
        /// 数据集合,线程安全形,先进先出集合
        /// </summary>
        ConcurrentQueue<tData> Queue = new ConcurrentQueue<tData>();

        /// <summary>
        /// 线程同步锁
        /// </summary>
        ManualResetEventSlim dataLock = new ManualResetEventSlim(false, 100);

        /// <summary>
        /// 向缓存中添加数据.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Enqueue(tData data)
        {
            Queue.Enqueue(data);
            dataLock.Set();
            return true;
        }

        /// <summary>
        /// 包含元素的数量
        /// </summary>
        public int Count
        {
            get
            {

                return Queue.Count;

            }
        }
        /// <summary>
        /// 获取数据,如果当前没有数据,会照成线程阻塞,直到有数据为止.
        /// </summary>
        /// <returns></returns>
        public tData Dequeue()
        {

            tData data;
            bool returnValue = Queue.TryDequeue(out data);
            if (!returnValue)
            {
                while (true)
                {
                    try
                    {
                        dataLock.Wait(50);
                        returnValue = Queue.TryDequeue(out data);
                        if (returnValue)
                        {
                            dataLock.Set();
                            return data;
                        }
                        else
                        {
                            dataLock.Reset();
                        }
                    }
                    catch (Exception ex1)
                    {

                    }
                }
            }
            return data;
        }


    }
}
