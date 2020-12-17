using Microsoft.Extensions.Configuration;
using OrderManagementDao_Mysql;
using OrderManagementModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderManagementModel.DBModel.Authority;
using OrderManagementModel.Common;
using OrderManagementModel.DBWhere;
using Util;

namespace OrderManagementBll.Authority
{
    public class Account
    {
        public IConfiguration Configuration { get; }
        public Account(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public HQResult<Authority_Account> Sigin(string Account, string Password)
        {
            HQResult<Authority_Account> result = new HQResult<Authority_Account>();

            OrderManagementDB DBcontext = new OrderManagementDB();

            var user = DBcontext.Authority_Account.Where(w => w.Account == Account && w.Pass == Password)
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


        public HQResult<Authority_Account> GetAccount(string Account)
        {
            HQResult<Authority_Account> result = new HQResult<Authority_Account>();

            OrderManagementDB DBcontext = new OrderManagementDB();

            var user = DBcontext.Authority_Account
                .Where(w => w.Account == Account)
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

        public List<OrderManagementModel.DBModel.Authority.Authority_Account> GetAccountWhere(AccountWhere where)
        {
            OrderManagementDB DBcontext = new OrderManagementDB();

            //List<int> ids = new List<int>();
            //ids.Add(1);
            //ids.Add(2);
            //ids.Add(3);
            //ids.Add(4);
            //ids.Add(5);

            //int[] idarray =  new int[] { 1, 3, 5, 7, 9 };

            //var test1 = DBcontext.Authority_Account.AsQueryable();
            //var test2 = test1.Where(o => idarray.Contains(o.ID));

            //var v3= test2.ToList();


            var Queryable = DBcontext.Authority_Account.AsQueryable().GetWhere(new PageQueryParam<AccountWhere>(where));


            return Queryable.ToList();
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

        //public HQResult<string> DelRole(int AccountId, int RoleId)
        //{
        //    HQResult<string> result = new HQResult<string>();
        //    OrderManagementDB DBcontext = new OrderManagementDB();

        //    //DBcontext.Authority_RelatedAccountRole.FromSqlRaw
        //}








    }
}
