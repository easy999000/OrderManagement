using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementModel.DBModel.Authority
{
    /// <summary>
    /// 基础权限
    /// </summary>
    public class Authority_PermissionBase
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


        /// <summary>
        /// 角色关联
        /// </summary>
        public List<Authority_RelatedRoleBasePer> RelatedRoles { get; set; }
    }
}
