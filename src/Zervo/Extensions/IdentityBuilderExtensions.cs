using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OpenIddict;
using Zervo.Data.Models;
using Zervo.Data.Repositories.Database;
using Zervo.Domain.Services;
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
            //Token implementation here
            //https://github.com/openiddict/openiddict-core/issues/249
            var tokenSecretKey = Encoding.ASCII.GetBytes("");

            //TODO: Symmetric Security Key
            var tokenValidationParameters = new TokenValidationParameters
            {
                //ValidateIssuerSigningKey = true,
                //IssuerSigningKey = new SymmetricSecurityKey(tokenSecretKey),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            app.UseIdentity()
                .UseOpenIddict()
                .UseJwtBearerAuthentication(new JwtBearerOptions
                {
                    Authority = "http://localhost:5000", // Issuer
                    Audience = "http://localhost:5000", // Audience
                    AutomaticChallenge = true,
                    AutomaticAuthenticate = true,
                    TokenValidationParameters = tokenValidationParameters,
                    RequireHttpsMetadata = false
                });
        }
    }
}