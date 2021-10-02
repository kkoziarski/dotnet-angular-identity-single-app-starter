using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Files;
using CleanArchWeb.Infrastructure.Persistence;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using CleanArchWeb.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IRepository, MongoRepository>();
            services.AddSingleton<IMongoClient>(provider =>
            {
                var mongoConfig = provider.GetRequiredService<IOptions<MongoConfig>>();
                return new MongoClient(mongoConfig.Value.ConnectionString);
            });

            services.ConfigureIdentity(configuration);

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.ConfigureAuth(configuration);

            return services;
        }
    }
}