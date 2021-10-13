using CleanArchWeb.Application.Common.Interfaces;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public ApplicationDbContext(IMongoRepository repository)
        {
            this.Repository = repository;
            this.Database = repository.Database;
        }

        public IMongoRepository Repository { get; }
        public IMongoDatabase Database { get; }
    }
}
