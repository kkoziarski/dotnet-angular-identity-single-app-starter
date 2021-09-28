using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchWeb.Infrastructure.DI.identityserver
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            
            // services
            //     .AddDefaultIdentity<ApplicationUser>()
            //     .AddRoles<IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer();
            // .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
        
        public static IServiceCollection AddAuthz(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            return services;
        }
    }
}