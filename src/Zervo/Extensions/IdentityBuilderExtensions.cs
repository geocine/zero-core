using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using OpenIddict;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Database;
using Zervo.Identity;

namespace Zervo.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static void AddZervoIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict<OpenIddictApplication<int, Token>, OpenIddictAuthorization<int, Token>, OpenIddictScope<int>, Token>()
                .AddEntityFramework<ZervoContext, int>()
                .AddMvcBinders()
                .EnableTokenEndpoint("/api/auth/token")
                .EnableRevocationEndpoint("/api/auth/logout")
                .EnableUserinfoEndpoint("/api/me")
                .AllowPasswordFlow()
                .AllowRefreshTokenFlow()
                .UseJsonWebTokens()
                .SetAccessTokenLifetime(TimeSpan.FromMinutes(1))
                .SetRefreshTokenLifetime(TimeSpan.FromMinutes(5))
                .DisableHttpsRequirement()
                .AddEphemeralSigningKey(); //Development

            services.AddScoped<IRoleStore<Role>, RoleStore>();
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IOpenIddictTokenStore<Token>, TokenStore>();
        }

        public static void UseZervoIdentity(this IApplicationBuilder app)
        {
            app.UseIdentity()
                .UseOpenIddict()
                .UseJwtBearerAuthentication(new JwtBearerOptions
                {
                    Authority = "http://localhost:5000",
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    Audience = "http://localhost:5000", // resource
                    RequireHttpsMetadata = false
                });
        }
    }
}