using Microsoft.Extensions.Configuration;
using OrderManagementDao_Mysql;
using OrderManagementModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderManagementModel.DBModel.Authority;

namespace OrderManagementBll.Authority
{
    public class Account
    {
        public IConfiguration Configuration { get; }
        public Account(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HQResult<Authority_Account> Sigin(string UserName, string Password)
        {
            HQResult<Authority_Account> result = new HQResult<Authority_Account>();

            OrderManagementDB DBcontext = new OrderManagementDB();

            var user = DBcontext.Authority_Account.Where(w => w.Name == UserName && w.Pass == Password)
                   .Include(p => p.RelatedRoles)
                   .ThenInclude(p => p.Role)
                   .ThenInclude(p => p.RelatedRoleBases)
                   .ThenInclude(p => p.BasePer).FirstOrDefault();

            if (user == null)
            {

                return result.SetResult(-1, null);
            }

            return result.SetResult(1, "ok", user);
        }

        public List<OrderManagementModel.DBModel.Authority.Authority_Account> GetAccountList()
        {
            OrderManagementDB DBcontext = new OrderManagementDB();

            var vl = DBcontext.Authority_Account.OrderByDescending(o => o.ID);

            return vl.ToList();
        }

        public HQResult<string> AddAccount(string Account, string Name, string Pass)
        {
            HQResult<string> result = new HQResult<string>();
            OrderManagementDB DBcontext = new OrderManagementDB();
            DBcontext.Authority_Account.Add(new Authority_Account { Account = Account, Name = Name, Pass = Pass, });
            DBcontext.SaveChanges();

            return result.SetResult(1, "ok");
        }
        public HQResult<string> RelatedRole(int AccountId, int RoleId)
        {
            HQResult<string> result = new HQResult<string>();
            OrderManagementDB DBcontext = new OrderManagementDB();
            DBcontext.Authority_RelatedAccountRole.Add(new OrderManagementModel.DBModel.Authority.Authority_RelatedAccountRole() { AccountID = AccountId, RoleID = RoleId });
            DBcontext.SaveChanges();

            return result.SetResult(1, "ok");
        }

    }
}
