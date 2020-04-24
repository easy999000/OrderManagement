using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OrderManagement.Areas.Authority.Controllers
{
    [Area("Authority")]
    public class PermissionBaseController : Controller
    {
        public IConfiguration Configuration { get; }
        public PermissionBaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderManagementModel.DBModel.Authority.Authority_PermissionBase> Index()
        {
            OrderManagementBll.Authority.PermissionBase AccountBll = new OrderManagementBll.Authority.PermissionBase(Configuration);

            return AccountBll.GetPermissionList();
        }
    }
}