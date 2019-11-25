using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace PingOne.Core.Configuration.Extensions
{
    public static class AddAuthenticationExtensions
    {
        public static IServiceCollection AddPingOneAuthentication(
            this IServiceCollection services,
            string authenticationScheme,
            PingOneConfigurationAuthentication configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            PingOneConfigurationValidator.ValidateAuthenticationConfiguration(configuration);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(authenticationScheme, options =>
                {
                    options.ClaimsIssuer = authenticationScheme;
                    options.Authority = $"{configuration.AuthBaseUrl}/{configuration.EnvironmentId}/as";
                    options.ClientId = configuration.ClientId;
                    options.ClientSecret = configuration.Secret;
                    options.CallbackPath = new PathString(configuration.RedirectPath);
                    options.SaveTokens = true;
                    options.ResponseType = configuration.ResponseType;

                    options.Scope.Clear();
                    foreach (var scope in configuration.Scopes)
                    {
                        options.Scope.Add(scope);
                    }

                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProviderForSignOut = context =>
                        {
                            context.ProtocolMessage.PostLogoutRedirectUri = configuration.PostSignOffRedirectUrl;
                            return Task.FromResult(0);
                        },
                    };
                });

            return services;
        }
    }
}
