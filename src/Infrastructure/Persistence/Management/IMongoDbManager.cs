using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.Persistence.Management.Models;
using MongoDB.Bson;

namespace CleanArchWeb.Infrastructure.Persistence.Management
{
    public interface IMongoDbManager
    {
        IAsyncEnumerable<string> GetDatabaseNamesAsync(CancellationToken cancellationToken = default);
        IEnumerable<string> GetDatabaseNames();
        void DropDatabase(string databaseName);
        Task CreateCollectionAsync(string databaseName, string collectionName, CancellationToken cancellationToken = default);
        void CreateCollection(string databaseName, string collectionName);
        IAsyncEnumerable<string> GetCollectionNamesAsync(string databaseName, CancellationToken cancellationToken = default);
        IEnumerable<string> GetCollectionNames(string databaseName);
        IEnumerable<string> GetCollectionInfo(string databaseName);
        CollectionStatsResult GetCollectionStats(string databaseName, string collectionName);
        Task DropCollectionAsync(string databaseName, string collectionName, CancellationToken cancellationToken = default);
        void DropCollection(string databaseName, string collectionName);
        IAsyncEnumerable<IndexSizesResult> GetIndexesAsync(string databaseName, string collectionName, CancellationToken cancellationToken = default);
        IEnumerable<IndexSizesResult> GetIndexes(string databaseName, string collectionName);
        IAsyncEnumerable<IndexSizesResult> GetIndexesAsync(string collectionName, CancellationToken cancellationToken = default);
        IEnumerable<IndexSizesResult> GetIndexes(string collectionName);
        void DropIndex(string databaseName, string collectionName, string indexName);
        Task DropIndexAsync(string databaseName, string collectionName, string indexName);
        Task DropIndexesAsync(string databaseName, string collectionName, CancellationToken cancellationToken);
        void DropIndexes(string databaseName, string collectionName);
        List<TProjection> Distinct<TProjection>(string databaseName, string collectionName, Expression<Func<BsonDocument, bool>> predicate, Expression<Func<BsonDocument, TProjection>> projection);
        Task ExportAsync(string databaseName, string collectionName, Expression<Func<BsonDocument, bool>> predicate, string outputFileName);
    }
}
