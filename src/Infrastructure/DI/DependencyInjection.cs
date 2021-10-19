using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Files;
using CleanArchWeb.Infrastructure.Persistence;
using CleanArchWeb.Infrastructure.Persistence.Adapters;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using CleanArchWeb.Infrastructure.Persistence.Management;
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
            //services.AddScoped<ApplicationDbContext>();
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            var mongoConfig = services.ConfigureMongo(configuration);

            services.ConfigureIdentity(mongoConfig);
            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.ConfigureAuth();

            return services;
        }

        private static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //services.AddScoped<IRepositorySimple, MongoRepositorySimple>();

            //services.AddScoped<IMongoRepository, Mongorepository>(provider =>
            //{
            //    var mongoconfig = provider.getrequiredService<MongoConfig>();
            //    var mongoClient = provider.GetRequiredService<IMongoClient>();
            //    return new MongoRepository(mongoClient.GetDatabase(mongoConfig.DatabaseName));
            //});

            return services;
        }

        private static MongoConfig ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoConfig>(configuration.GetSection("MongoConfig"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MongoConfig>>().Value);
            var mongoConfig = configuration.GetSection("MongoConfig").Get<MongoConfig>();

            services.AddSingleton<IMongoClient>(provider =>
            {
                var mongoConfig = provider.GetRequiredService<MongoConfig>();
                return new MongoClient(mongoConfig.ConnectionString);
            })
            .AddScoped<IMongoDbContext, MongoDbContext>()
            .AddScoped<IMongoDbManager, MongoDbManager>()
            .AddScoped(typeof(IMongoReadAdapter<>), typeof(MongoReadAdapter<>))
            .AddScoped(typeof(IMongoWriteAdapter<,>), typeof(MongoWriteAdapter<,>))
            .AddRepository();

            TodoListDocumentConfiguration.ConfigureMongo();

            return mongoConfig;
        }
    }
}