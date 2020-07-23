using OrderManagementModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBWhere
{
    public class Authority_PermissionBaseWhere: PagingWhere
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string Control { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
