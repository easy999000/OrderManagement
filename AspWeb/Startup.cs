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

            // ʹӦ�ó������ʹ�� Cookie ���洢�ѵ�¼�û�����Ϣ
            // ��ʹ�� Cookie ����ʱ�洢�й�ʹ�õ�������¼�ṩ�����¼���û�����Ϣ
            // ���õ�¼ Cookie
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

            //        // ���û���¼ʱʹӦ�ó��������֤��ȫ����
            //        // ����һ�ȫ���ܣ������������������ʻ�����ⲿ��¼��ʱ����ʹ�ô˹��ܡ�
            //        //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //        //    validateInterval: TimeSpan.FromMinutes(30),
            //        //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //    }
            //}); ; ; ;

            ////AuthStartup.ConfigureAuth(app);
            ////����Middleware �M��
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
