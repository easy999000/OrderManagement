using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthorityModule
{
    public class HQAuthorizationConfigOption
    {
        /// <summary>
        /// 安全策略的名字
        /// </summary>
        public string PolicyName { get; set; } = "HQPolicy";

        /// <summary>
        /// 无权访问的 调整页面地址
        /// </summary>
        public string AccessDeniedPath { get; set; } = "/login/test2";

        /// <summary>
        /// 未登入的跳转页面地址
        /// </summary>
        public string LoginPath { get; set; } = "/login/index";
    }
}
