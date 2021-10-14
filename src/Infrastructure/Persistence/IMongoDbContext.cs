using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence
{
    internal interface IMongoDbContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }

        IMongoCollection<TDocument> GetReadCollection<TDocument>() where TDocument : class;

        IMongoCollection<TDocument> GetReadCollection<TDocument>(string collectionName) where TDocument : class;

        IMongoCollection<TDocument> GetWriteCollection<TDocument>(string collectionName) where TDocument : class;

        IMongoCollection<TDocument> GetWriteCollection<TDocument>() where TDocument : class;
    }
}
