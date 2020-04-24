using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBModel.Authority
{
    /// <summary>
    /// 账号角色关联表
    /// </summary>
    public class Authority_RelatedAccountRole
    {

        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 账号id
        /// </summary>
        public int AccountID { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 关联账号
        /// </summary>
        public Authority_Account Account { get; set; }
        /// <summary>
        /// 关联角色
        /// </summary>
        public Authority_Role Role { get; set; }

    }
}
