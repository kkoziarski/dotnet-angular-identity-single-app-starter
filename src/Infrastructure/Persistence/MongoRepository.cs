using CleanArchWeb.Application.Common.Interfaces;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public class MongoRepository : BaseMongoRepository, IMongoRepository
    {
        public MongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
            this.Database = mongoDatabase;
        }

        public IMongoDatabase Database { get; }
    }
}