using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonWebModule.MVCAuthenticationModule
{
    internal static class HQLoggingExtensions
    {
        private static Action<ILogger, string, Exception> _authSchemeSignedIn;
        private static Action<ILogger, string, Exception> _authSchemeSignedOut;

        static HQLoggingExtensions()
        {
            _authSchemeSignedIn = LoggerMessage.Define<string>(
                eventId: 10,
                logLevel: LogLevel.Information,
                formatString: "AuthenticationScheme: {AuthenticationScheme} signed in.");
            _authSchemeSignedOut = LoggerMessage.Define<string>(
                eventId: 11,
                logLevel: LogLevel.Information,
                formatString: "AuthenticationScheme: {AuthenticationScheme} signed out.");
        }

        public static void SignedIn(this ILogger logger, string authenticationScheme)
        {
            _authSchemeSignedIn(logger, authenticationScheme, null);
        }

        public static void SignedOut(this ILogger logger, string authenticationScheme)
        {
            _authSchemeSignedOut(logger, authenticationScheme, null);
        }
    }
}
