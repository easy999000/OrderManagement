using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonWebModule;
using CommonWebModule.MVCAuthorityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderManagementModel;

namespace OrderManagement.Areas.Authority.Controllers
{
    [Area("Authority")]
    public class AuthorityController : Controller
    {

        HQAuthorizationStateManager _stateManager;
        IConfiguration Configuration { get; }
        public AuthorityController(HQAuthorizationStateManager stateManager, IConfiguration configuration)
        {
            Configuration = configuration;

            _stateManager = stateManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public HQResult<string> Sigin(string UserName, string Password)
        {
            HQResult<string> result = new HQResult<string>(0, "");

            OrderManagementBll.Authority.Account accountBll = new OrderManagementBll.Authority.Account(Configuration);

            var v1 = accountBll.Sigin(UserName, Password);

            if (v1.Status < 1)
            {
                return result.SetResult(-1, v1.Msg);
            }


            HQAuthorizationUser User = new HQAuthorizationUser() { UserName = UserName, Account = UserName, UserID = 10 };

            User.UserHQAuthorizationSign.LoadData(new List<HQAuthorizationSign>() {
                new HQAuthorizationSign { Area="", Controller="login", Action="test1",ID=10 },
               // new HQAuthorizationSign { Area="", Controller="login", Action="test3" },
                
            });

            _stateManager.Sigin(User);



            //  return RedirectToAction("test1");

            return result.SetResult(1, "登入成功", "");
        }
        [AllowAnonymous]
        public IActionResult Sigin2(string UserName, string Password)
        {
            HQResult<string> result = new HQResult<string>(0, "");

            OrderManagementBll.Authority.Account accountBll = new OrderManagementBll.Authority.Account(Configuration);

            var user = accountBll.Sigin(UserName, Password);

            if (user.Status < 1)
            {
                ViewData["msg"] = "登入失败";
                return View("index");
            }


            HQAuthorizationUser User = new HQAuthorizationUser() { UserName = user.Value.Name, Account = user.Value.Account, UserID = user.Value.ID };

            var pers = user.Value.RelatedRoles.SelectMany(s => s.Role.RelatedRoleBases.SelectMany(p => p.Role.RelatedRoleBases.Select(o => o.BasePer)));


            User.UserHQAuthorizationSign.LoadData(
                pers.Select(p => new HQAuthorizationSign { Action = p.Action, Area = p.Area, Controller = p.Control, ID = p.ID }).ToList()
                ); ;

            _stateManager.Sigin(User);



            return RedirectToAction("index", "home", new { area = "main" });

            // return result.SetResult(1, "登入成功", "");
        }

        [AllowAnonymous]
        public HQResult<string> Sigout()
        {
            HQResult<string> result = new HQResult<string>(0, "");


            _stateManager.Signout();



            return result.SetResult(1, "退出成功");
        }


        [AllowAnonymous]
        public HQResult<string> NotSigin(string ReturnUrl)
        {
            HQResult<string> result = new HQResult<string>(0, "未登入",ReturnUrl);
            return result;
        }

        [AllowAnonymous]
        public HQResult<string> NoAuthority(string ReturnUrl)
        {
            HQResult<string> result = new HQResult<string>(0, "没有访问权限" ,ReturnUrl);
            return result;
        }

    }
}