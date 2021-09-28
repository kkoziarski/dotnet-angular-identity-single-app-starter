using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public static class RepositoryExtensions
    {
        public static IIdentityServerBuilder AddMongoRepository(
            this IIdentityServerBuilder builder,
            string mongoConnection, 
            string mongoDatabaseName)
        {
            builder.Services.AddTransient<IRepository, MongoRepository>(
                s => new MongoRepository(mongoConnection, mongoDatabaseName));
            return builder;
        }

        public static IIdentityServerBuilder AddClients(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IClientStore, RepositoryClientStore>();
            builder.Services.AddTransient<ICorsPolicyService, InMemoryCorsPolicyService>();
            return builder;
        }

        public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IResourceStore, RepositoryResourceStore>();
            return builder;
        }

        public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IPersistedGrantStore, RepositoryPersistedGrantStore>();
            return builder;
        }        
    }
}