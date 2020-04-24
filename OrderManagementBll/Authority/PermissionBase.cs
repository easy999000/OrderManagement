using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text; 
using System.Linq;
using OrderManagementDao_Mysql;

namespace OrderManagementBll.Authority
{
    public class PermissionBase
    {
        public IConfiguration Configuration { get; }
        public PermissionBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public List<OrderManagementModel.DBModel.Authority.Authority_PermissionBase> GetPermissionList()
        {
            OrderManagementDB DBcontext = new OrderManagementDB();

            var vl = DBcontext.Authority_PermissionBase.OrderByDescending(o => o.ID);

            return vl.ToList();
        }
    }
}
