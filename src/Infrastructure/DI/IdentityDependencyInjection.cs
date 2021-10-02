using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Identity;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchWeb.Infrastructure.DI
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoConfig>(configuration.GetSection("MongoConfig"));
            var mongoConfig = configuration.GetSection("MongoConfig").Get<MongoConfig>();

            var mongoDbIdentityConfiguration = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = mongoConfig.ConnectionString,
                    DatabaseName = mongoConfig.DatabaseName
                },
                IdentityOptionsAction = options =>
                {
                    //// Password settings
                    //options.Password.RequiredLength = 8;

                    //// Lockout settings
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                    //// ApplicationUser settings
                    options.User.RequireUniqueEmail = true;
                    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";
                }
            };

            services.AddDefaultIdentity<ApplicationUser>();
            services
                .ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, string>(mongoDbIdentityConfiguration)
                .AddDefaultTokenProviders();

            services.ConfigureIdentityServer(mongoConfig, configuration);

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        private static void ConfigureIdentityServer(this IServiceCollection services, MongoConfig mongoConfig, IConfiguration configuration)
        {
            services.TryAddScoped<SignInManager<ApplicationUser>>();
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddOperationalStore(c =>
                {
                    c.ConnectionString = mongoConfig.ConnectionString;
                    c.Database = mongoConfig.DatabaseName;
                })
                .AddConfigurationStore(c =>
                {
                    c.ConnectionString = mongoConfig.ConnectionString;
                    c.Database = mongoConfig.DatabaseName;
                })
                .AddIdentityResources()
                .AddApiResources()
                .AddClients()
                .AddSigningCredentials();
        }

        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }

        //// NOT USED
        public static IServiceCollection AddExternalIdentityProviders(this IServiceCollection services)
        {
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            services.AddOidcStateDataFormatterCache("aad", "demoidsrv");

            services.AddAuthentication()
                // .AddGoogle("Google", options =>
                // {
                //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //
                //     options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
                //     options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
                // })
                .AddOpenIdConnect("demoidsrv", "IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "implicit";
                    options.ResponseType = "id_token";
                    options.SaveTokens = true;
                    options.CallbackPath = new PathString("/signin-idsrv");
                    options.SignedOutCallbackPath = new PathString("/signout-callback-idsrv");
                    options.RemoteSignOutPath = new PathString("/signout-idsrv");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                })
                .AddOpenIdConnect("aad", "Azure AD", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.Authority = "https://login.windows.net/4ca9cb4c-5e5f-4be9-b700-c532992a3705";
                    options.ClientId = "96e3c53e-01cb-4244-b658-a42164cb67a9";
                    options.ResponseType = "id_token";
                    options.CallbackPath = new PathString("/signin-aad");
                    options.SignedOutCallbackPath = new PathString("/signout-callback-aad");
                    options.RemoteSignOutPath = new PathString("/signout-aad");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                })
                .AddOpenIdConnect("adfs", "ADFS", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.Authority = "https://adfs.leastprivilege.vm/adfs";
                    options.ClientId = "c0ea8d99-f1e7-43b0-a100-7dee3f2e5c3c";
                    options.ResponseType = "id_token";

                    options.CallbackPath = new PathString("/signin-adfs");
                    options.SignedOutCallbackPath = new PathString("/signout-callback-adfs");
                    options.RemoteSignOutPath = new PathString("/signout-adfs");
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });

            return services;
        }
    }
}