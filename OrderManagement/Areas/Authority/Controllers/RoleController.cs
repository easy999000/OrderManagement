using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderManagementModel;

namespace OrderManagement.Areas.Authority.Controllers
{
    [Area("Authority")]
    public class RoleController : Controller
    {
        public IConfiguration Configuration { get; }
        public RoleController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderManagementModel.DBModel.Authority.Authority_Role> Index()
        {
            OrderManagementBll.Authority.Role AccountBll = new OrderManagementBll.Authority.Role(Configuration);

            return AccountBll.GetRoleList();
        }
        public HQResult<string> RelatedPerBase(int BaseId, int RoleId)
        {
            OrderManagementBll.Authority.Role Bll = new OrderManagementBll.Authority.Role(Configuration);

            return Bll.RelatedPerBase(RoleId, BaseId);
        }
        public HQResult<string> AddRole(string RoleName, string Description)
        {
            OrderManagementBll.Authority.Role Bll = new OrderManagementBll.Authority.Role(Configuration);

            return Bll.AddRole(RoleName, Description);
        }
    }
}