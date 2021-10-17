using MongoDB.Driver;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoClient Client { get; }

        IMongoDatabase Database { get; }

        IMongoCollection<TDocument> GetReadCollection<TDocument>() where TDocument : class;

        IMongoCollection<TDocument> GetReadCollection<TDocument>(string collectionName) where TDocument : class;

        IMongoCollection<TDocument> GetWriteCollection<TDocument>(string collectionName) where TDocument : class;

        IMongoCollection<TDocument> GetWriteCollection<TDocument>() where TDocument : class;
    }
}
