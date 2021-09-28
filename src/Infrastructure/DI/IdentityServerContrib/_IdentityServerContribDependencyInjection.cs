using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Identity;
using CleanArchWeb.Infrastructure.Persistence.IdentityServer;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchWeb.Infrastructure.DI.IdentityServerContrib
{
    public static class IdentityServerContribDependencyInjection
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoConfig>(configuration.GetSection("MongoConfig"));
            var mongoConfig = configuration.GetSection("MongoConfig").Get<MongoConfig>();

            //var mongoDbContext = new MongoDbContext(mongoConfig.ConnectionString, mongoConfig.DatabaseName);
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddMongoDbStores<ApplicationUser, ApplicationRole, string>(mongoDbContext)
            //    .AddDefaultTokenProviders();

            var mongoDbIdentityConfiguration = new MongoDbIdentityConfiguration
            {
                MongoDbSettings = new MongoDbSettings
                {
                    ConnectionString = mongoConfig.ConnectionString,
                    DatabaseName = mongoConfig.DatabaseName
                },
                IdentityOptionsAction = options =>
                {
                    //options.Password.RequireDigit = false;
                    //options.Password.RequiredLength = 8;
                    //options.Password.RequireNonAlphanumeric = false;
                    //options.Password.RequireUppercase = false;
                    //options.Password.RequireLowercase = false;

                    //// Lockout settings
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    //options.Lockout.MaxFailedAccessAttempts = 10;

                    //// ApplicationUser settings
                    options.User.RequireUniqueEmail = true;
                    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.-_";
                }
            };
            services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, string>(mongoDbIdentityConfiguration)
                 .AddDefaultTokenProviders();

            services.ConfigureIdentityServer(mongoConfig);

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }

        private static void ConfigureIdentityServer(this IServiceCollection services, MongoConfig mongoConfig)
        {
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            })
            .AddConfigurationStore(c =>
            {
                c.ConnectionString = mongoConfig.ConnectionString;
                c.Database = mongoConfig.DatabaseName;
            })
            .AddOperationalStore(c =>
            {
                c.ConnectionString = mongoConfig.ConnectionString;
                c.Database = mongoConfig.DatabaseName;
            })
            // THIS IS BAAAAD!!!!!
            //},
            //(tco) =>
            //{
            //    tco.Enable = true;
            //    tco.Interval = 3600;
            //})
            .AddDeveloperSigningCredential()
            .AddExtensionGrantValidator<Identity.Extensions.ExtensionGrantValidator>()
            .AddExtensionGrantValidator<Identity.Extensions.NoSubjectExtensionGrantValidator>()
            .AddJwtBearerClientAuthentication()
            .AddAppAuthRedirectUriValidator()
            .AddTestUsers(TestUsers.Users);
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