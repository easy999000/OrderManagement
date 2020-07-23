using AspWeb.code;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(AspWeb.StartupOwin))]

namespace AspWeb
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            HQAuthenticationManager manager = new HQAuthenticationManager();

            manager.Configuration(app);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            // 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            // 配置登录 Cookie
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //    CookieDomain = "zhao.cc"
            //    ,
            //    CookieName = "CookieAuthentication.hq2"
            //    ,
            //    TicketDataFormat = new HQSecureDataFormat()
            //    //,
            //    ,Provider = new CookieAuthenticationProvider
            //    {

            //        // 当用户登录时使应用程序可以验证安全戳。
            //        // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
            //        //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //        //    validateInterval: TimeSpan.FromMinutes(30),
            //        //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //    }
            //}); ; ; ;

            ////AuthStartup.ConfigureAuth(app);
            ////配置Middleware 組件
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account2/Login"),
            //    CookieSecure = CookieSecureOption.Never,
            //    CookieDomain = "hqbuy.com",
            //    // TicketDataFormat = null
            //})
                //.UseExternalSignInCookie();
                ; ;
        }
    }
}
