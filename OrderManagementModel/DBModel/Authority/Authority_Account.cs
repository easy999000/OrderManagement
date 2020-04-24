using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBModel.Authority
{
    /// <summary>
    /// 账号
    /// </summary>
    public class Authority_Account
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string  Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string  Pass { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色关联
        /// </summary>
        public List<Authority_RelatedAccountRole> RelatedRoles { get; set; }

    }
}
