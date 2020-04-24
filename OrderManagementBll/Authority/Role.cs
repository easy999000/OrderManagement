using Microsoft.Extensions.Configuration;
using OrderManagementDao_Mysql;
using OrderManagementModel;
using OrderManagementModel.DBModel.Authority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderManagementBll.Authority
{
    public class Role
    {
        public IConfiguration Configuration { get; }
        public Role(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public List<OrderManagementModel.DBModel.Authority.Authority_Role> GetRoleList()
        {
            OrderManagementDB DBcontext = new OrderManagementDB();

            var vl = DBcontext.Authority_Role.OrderByDescending(o => o.ID);

            return vl.ToList();
        }
        public HQResult<string> RelatedPerBase(int RoleId, int BaseId)
        {
            HQResult<string> result = new HQResult<string>();
            OrderManagementDB DBcontext = new OrderManagementDB();
            DBcontext.Authority_RelatedRoleBasePer.Add(new  OrderManagementModel.DBModel.Authority.Authority_RelatedRoleBasePer() {  BasePerID = BaseId, RoleID = RoleId });
            DBcontext.SaveChanges();

            return result.SetResult(1, "ok");
        }
        public HQResult<string> AddRole(string RoleName, string Description )
        {
            HQResult<string> result = new HQResult<string>();
            OrderManagementDB DBcontext = new OrderManagementDB();
            DBcontext.Authority_Role.Add(new  Authority_Role { RoleName= RoleName, Description = Description });
            DBcontext.SaveChanges();

            return result.SetResult(1, "ok");
        }
    }
}
