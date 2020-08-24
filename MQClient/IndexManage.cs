using System;
using System.Collections.Generic;
using System.Text;

namespace MQClient
{
    /// <summary>
    /// 管理索引的
    /// </summary>
    internal class IndexManage
    {
        List<int> IndexList = new List<int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Count">总数量</param>
        /// <param name="StartIndex">起始索引</param>
        public IndexManage(int Count, int StartIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                int Index = (StartIndex + i) % Count;

                IndexList.Add(Index);
            }
        }

        /// <summary>
        /// 获取下一个索引
        /// </summary>
        /// <returns></returns>
        public int GetIndexNext()
        {
            if (IndexList.Count == 0)
            {
                return -1;
            }
            int index = IndexList[0];
            IndexList.RemoveAt(0);
            return index;
        }

    }
}
