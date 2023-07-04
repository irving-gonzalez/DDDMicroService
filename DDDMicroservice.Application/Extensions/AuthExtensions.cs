using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDDMicroservice.Application.Authorization.Extesions
{
    public static class AuthenticationExtensions
    {
        private static BasicAuthSchemeOptions _options = new BasicAuthSchemeOptions();

        public static AuthenticationBuilder AddBasic(this AuthenticationBuilder builder, string authenticationScheme, Action<BasicAuthSchemeOptions> configureOptions)
        {
            configureOptions(_options);
            builder.Services.AddSingleton<IUserAuthService, InMemoryUserAuthService>(resolver => new InMemoryUserAuthService(_options));
            return builder.AddScheme<BasicAuthSchemeOptions, BasicAuthHandler>(authenticationScheme, configureOptions);
        }
    }
}