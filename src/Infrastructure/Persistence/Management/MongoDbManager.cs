using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Persistence.Management.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence.Management
{
    internal class MongoDbManager : IMongoDbManager
    {
        private readonly IMongoDbContext _mongoContext;
        private readonly IEnumerable<string> _systemDatabases = new[] { "admin", "config", "local" }.Select(v => v.ToLower());

        public MongoDbManager(IMongoDbContext mongoContext) => _mongoContext = mongoContext;

        public async IAsyncEnumerable<string> GetDatabaseNamesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var dbsCursor = await _mongoContext.Client.ListDatabaseNamesAsync(cancellationToken);
            while (await dbsCursor.MoveNextAsync(cancellationToken))
            {
                foreach (var current in dbsCursor.Current)
                {
                    if (_systemDatabases.Contains(current))
                    {
                        continue;
                    }
                    yield return current;
                }
            }
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            var dbsCursor = _mongoContext.Client.ListDatabaseNames();
            while (dbsCursor.MoveNext())
            {
                foreach (var current in dbsCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public Task CreateCollectionAsync(string databaseName, string collectionName, CancellationToken cancellationToken = default)
            => _mongoContext.Client.GetDatabase(databaseName).CreateCollectionAsync(collectionName, null, cancellationToken);

        public void CreateCollection(string databaseName, string collectionName)
            => _mongoContext.Client.GetDatabase(databaseName).CreateCollection(collectionName);

        public async IAsyncEnumerable<string> GetCollectionNamesAsync(string databaseName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var options = new ListCollectionNamesOptions { Filter = Builders<BsonDocument>.Filter.Empty };
            var collectionsCursor = await _mongoContext.Client
                                        .GetDatabase(databaseName)
                                        .ListCollectionNamesAsync(options, cancellationToken);
            while (await collectionsCursor.MoveNextAsync(cancellationToken))
            {
                foreach (var current in collectionsCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public IEnumerable<string> GetCollectionNames(string databaseName)
        {
            var options = new ListCollectionNamesOptions { Filter = Builders<BsonDocument>.Filter.Empty };
            var collectionsCursor = _mongoContext.Client
                                        .GetDatabase(databaseName)
                                        .ListCollectionNames(options);
            while (collectionsCursor.MoveNext())
            {
                foreach (var current in collectionsCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public IEnumerable<string> GetCollectionInfo(string databaseName)
        {
            var collectionsCursor = _mongoContext.Client
                                        .GetDatabase(databaseName)
                                        .ListCollections();
            while (collectionsCursor.MoveNext())
            {
                foreach (var current in collectionsCursor.Current)
                {
                    yield return current.ToJson();
                }
            }
        }

        public async IAsyncEnumerable<IndexSizesResult> GetIndexesAsync(string databaseName, string collectionName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var mongoCollectionIndexes = _mongoContext.Client
                                            .GetDatabase(databaseName)
                                            .GetCollection<BsonDocument>(collectionName).Indexes;
            var indexesCusrsor = await mongoCollectionIndexes.ListAsync(cancellationToken);
            while (await indexesCusrsor.MoveNextAsync(cancellationToken))
            {
                foreach (var current in indexesCusrsor.Current)
                {
                    yield return new IndexSizesResult(current.AsBsonDocument);
                }
            }
        }

        public IEnumerable<IndexSizesResult> GetIndexes(string databaseName, string collectionName)
        {
            var mongoCollectionIndexes = _mongoContext.Client
                                            .GetDatabase(databaseName)
                                            .GetCollection<BsonDocument>(collectionName).Indexes;
            var indexesCusrsor = mongoCollectionIndexes.List();
            while (indexesCusrsor.MoveNext())
            {
                foreach (var current in indexesCusrsor.Current)
                {
                    yield return new IndexSizesResult(current.AsBsonDocument);
                }
            }
        }

        public async IAsyncEnumerable<IndexSizesResult> GetIndexesAsync(string collectionName, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var mongoCollectionIndexes = _mongoContext.Database.GetCollection<BsonDocument>(collectionName).Indexes;
            var indexesCusrsor = await mongoCollectionIndexes.ListAsync(cancellationToken);
            while (await indexesCusrsor.MoveNextAsync(cancellationToken))
            {
                foreach (var current in indexesCusrsor.Current)
                {
                    yield return new IndexSizesResult(current.AsBsonDocument);
                }
            }
        }

        public IEnumerable<IndexSizesResult> GetIndexes(string collectionName)
        {
            var mongoCollectionIndexes = _mongoContext.Database.GetCollection<BsonDocument>(collectionName).Indexes;
            var indexesCusrsor = mongoCollectionIndexes.List();
            while (indexesCusrsor.MoveNext())
            {
                foreach (var current in indexesCusrsor.Current)
                {
                    yield return new IndexSizesResult(current.AsBsonDocument);
                }
            }
        }

        public void DropDatabase(string databaseName)
            => _mongoContext.Client.DropDatabase(databaseName);

        public async void DropDatabaseAsync(string databaseName)
            => await _mongoContext.Client.DropDatabaseAsync(databaseName);

        public Task DropCollectionAsync(string databaseName, string collectionName, CancellationToken cancellationToken = default)
            => _mongoContext.Client.GetDatabase(databaseName).DropCollectionAsync(collectionName, cancellationToken);

        public void DropCollection(string databaseName, string collectionName)
            => _mongoContext.Client.GetDatabase(databaseName).DropCollection(collectionName);

        public void DropIndexes(string databaseName, string collectionName)
            => _mongoContext.Client
                .GetDatabase(databaseName)
                .GetCollection<BsonDocument>(collectionName)
                .Indexes
                .DropAll();

        public Task DropIndexesAsync(string databaseName, string collectionName, CancellationToken cancellationToken)
            => _mongoContext.Client
                .GetDatabase(databaseName)
                .GetCollection<BsonDocument>(collectionName)
                .Indexes
                .DropAllAsync();

        public void DropIndex(string databaseName, string collectionName, string indexName)
            => _mongoContext.Client
                .GetDatabase(databaseName)
                .GetCollection<BsonDocument>(collectionName)
                .Indexes
                .DropOne(indexName);

        public Task DropIndexAsync(string databaseName, string collectionName, string indexName)
            => _mongoContext.Client
                .GetDatabase(databaseName)
                .GetCollection<BsonDocument>(collectionName)
                .Indexes
                .DropOneAsync(indexName);

        public List<TProjection> Distinct<TProjection>(string databaseName, string collectionName, Expression<Func<BsonDocument, bool>> predicate, Expression<Func<BsonDocument, TProjection>> projection)
        {
            var filter = predicate ?? (x => true);
            return _mongoContext.Client
                    .GetDatabase(databaseName)
                    .GetCollection<BsonDocument>(collectionName)
                    .AsQueryable()
                    .Where(filter)
                    .Select(projection)
                    .Distinct()
                    .ToList();
        }

        public async Task ExportAsync(string databaseName, string collectionName, Expression<Func<BsonDocument, bool>> predicate, string outputFileName)
        {
            var filter = predicate ?? (x => true);
            var collection = _mongoContext.Client
                                .GetDatabase(databaseName)
                                .GetCollection<BsonDocument>(collectionName);

            using var streamWriter = new StreamWriter(outputFileName);
            await collection.Find(filter).ForEachAsync(async (document) =>
            {
                using (var stringWriter = new StringWriter())
                using (var jsonWriter = new JsonWriter(stringWriter))
                {
                    var context = BsonSerializationContext.CreateRoot(jsonWriter);
                    collection.DocumentSerializer.Serialize(context, document);
                    var line = stringWriter.ToString();
                    await streamWriter.WriteLineAsync(line);
                }
            });
        }

        public CollectionStatsResult GetCollectionStats(string databaseName, string collectionName)
        {
            var collection = _mongoContext.Client
                                .GetDatabase(databaseName)
                                .GetCollection<BsonDocument>(collectionName);
            return new(collection.Database.RunCommand(new BsonDocumentCommand<BsonDocument>(new BsonDocument { { "collstats", collection.CollectionNamespace.CollectionName } })));
        }
    }
}
