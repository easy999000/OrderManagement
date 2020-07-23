using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthenticationModule
{
    public static class HQAuthenticationExtensions
    {
        public static AuthenticationBuilder AddHQAuthentication(this AuthenticationBuilder builder)
            => builder.AddHQAuthentication(HQAuthenticationDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddHQAuthentication(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddHQAuthentication(authenticationScheme, configureOptions: null);

        public static AuthenticationBuilder AddHQAuthentication(this AuthenticationBuilder builder, Action<HQAuthenticationHandlerOptions> configureOptions)
            => builder.AddHQAuthentication(HQAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddHQAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<HQAuthenticationHandlerOptions> configureOptions)
            => builder.AddHQAuthentication(authenticationScheme, displayName: null, configureOptions: configureOptions);

        public static AuthenticationBuilder AddHQAuthentication(this AuthenticationBuilder builder, string authenticationScheme
            , string displayName, Action<HQAuthenticationHandlerOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<HQAuthenticationHandlerOptions>
                , PostConfigureCookieAuthenticationOptions>());

            return builder.AddScheme<HQAuthenticationHandlerOptions, HQAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
