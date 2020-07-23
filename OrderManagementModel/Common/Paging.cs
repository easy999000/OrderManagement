using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.Common
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// 页面序号
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面数据数量
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总条数 用于返回结果
        /// </summary>
        public int TotalNumber { get; set; }




    }
}
