using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBModel.Authority
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Authority_Role
    {
        public int ID { get; set; }

        /// <summary>
        /// 角色名字
        /// </summary>
        public string  RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 权限关联
        /// </summary>
        public List<Authority_RelatedRoleBasePer> RelatedRoleBases { get; set; }
        /// <summary>
        /// 账号关联
        /// </summary>
        public List<Authority_RelatedAccountRole> RelatedAccounts { get; set; }



    }
}
