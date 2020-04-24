using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthorityModule
{
    /// <summary>
    /// 权限用户模型
    /// </summary>
    public class HQAuthorizationUser
    {
        public int UserID { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }

        public HQAuthorizationSignManager UserHQAuthorizationSign = new HQAuthorizationSignManager();
    }
}
