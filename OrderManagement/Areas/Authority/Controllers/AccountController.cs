using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderManagementModel;
using OrderManagementModel.Common;

namespace OrderManagement.Areas.Authority.Controllers
{
    [Area("Authority")]
    public class AccountController : Controller
    {
        public IConfiguration Configuration { get; }
        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderManagementModel.DBModel.Authority.Authority_Account> Index()
        {
            OrderManagementBll.Authority.Account AccountBll = new OrderManagementBll.Authority.Account(Configuration);
             


            return AccountBll.GetAccountList(); 



        }
        public HQResult<string> RelatedRole(int AccountId, int RoleId)
        {
            OrderManagementBll.Authority.Account AccountBll = new OrderManagementBll.Authority.Account(Configuration);

            return AccountBll.RelatedRole(AccountId, RoleId);
        }
        public HQResult<string> AddAccount(string Account, string Name, string Pass)
        {
            OrderManagementBll.Authority.Account AccountBll = new OrderManagementBll.Authority.Account(Configuration);

            return AccountBll.AddAccount(Account, Name, Pass);
        }



    }
}