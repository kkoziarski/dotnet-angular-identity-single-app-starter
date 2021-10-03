using MongoDB.Driver;
using MongoDbGenericRepository;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public interface IMongoRepository : IBaseMongoRepository
    {

    }

    public class MongoRepository : BaseMongoRepository, IMongoRepository
    {
        public MongoRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}