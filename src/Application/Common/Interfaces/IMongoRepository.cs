using MongoDbGenericRepository;
using MongoDB.Driver;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IMongoRepository : IBaseMongoRepository
    {
        IMongoDatabase Database { get; }
    }
}