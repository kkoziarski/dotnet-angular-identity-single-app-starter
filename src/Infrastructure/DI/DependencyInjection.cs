﻿using CleanArchWeb.Application.Common.Interfaces;
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
            services.AddSingleton<IMongoClient>(provider =>
                {
                    var mongoConfig = provider.GetRequiredService<IOptions<MongoConfig>>();
                    return new MongoClient(mongoConfig.Value.ConnectionString);
                })
                .AddRepository();

            services.ConfigureIdentity(configuration);

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.ConfigureAuth(configuration);

            ConfigureMongo();

            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositorySimple, MongoRepositorySimple>();

            services.AddScoped<IMongoRepository, MongoRepository>(provider =>
            {
                var mongoConfig = provider.GetRequiredService<IOptions<MongoConfig>>();
                var mongoClient = provider.GetRequiredService<IMongoClient>();
                return new MongoRepository(mongoClient.GetDatabase(mongoConfig.Value.DatabaseName));
            });

            return services;
        }

        private static void ConfigureMongo()
        {
            TodoListDocumentConfiguration.ConfigureMongo();
        }
    }
}