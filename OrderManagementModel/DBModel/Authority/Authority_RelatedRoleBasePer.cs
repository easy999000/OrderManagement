using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBModel.Authority
{
    /// <summary>
    /// 角色和权限关联
    /// </summary>
    public  class Authority_RelatedRoleBasePer
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int BasePerID { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 关联权限
        /// </summary>
        public Authority_PermissionBase BasePer { get; set; }
        /// <summary>
        /// 关联角色
        /// </summary>
        public Authority_Role Role { get; set; }

    }
}
