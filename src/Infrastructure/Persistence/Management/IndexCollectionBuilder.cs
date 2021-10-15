using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.Persistence.Management.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence.Management
{
    public class IndexCollectionBuilder
    {
        private enum IndexType
        {
            Ascending,
            Descending,
            Text,
            Hashed
        }

        private readonly IMongoCollection<BsonDocument> _mongoCollection;

        public IndexCollectionBuilder(IMongoCollection<BsonDocument> mongoCollection) => this._mongoCollection = mongoCollection;

        public IndexCollectionBuilder CreateAscendingIndex(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateIndexModel(field, IndexType.Ascending, options));
            return this;
        }

        public async Task<IndexCollectionBuilder> CreateAscendingIndexAsync(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            await _mongoCollection.Indexes.CreateOneAsync(CreateIndexModel(field, IndexType.Ascending, options));
            return this;
        }

        public IndexCollectionBuilder CreateDescendingIndex(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateIndexModel(field, IndexType.Descending, options));
            return this;
        }

        public async Task<IndexCollectionBuilder> CreateDescendingIndexAsync(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            await _mongoCollection.Indexes.CreateOneAsync(CreateIndexModel(field, IndexType.Descending, options));
            return this;
        }

        public async Task<IndexCollectionBuilder> CreateHashedIndexAsync(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            await _mongoCollection.Indexes.CreateOneAsync(CreateIndexModel(field, IndexType.Hashed, options));
            return this;
        }

        public IndexCollectionBuilder CreateHashedIndex(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateIndexModel(field, IndexType.Hashed, options));
            return this;
        }

        public async Task<IndexCollectionBuilder> CreateTextIndexAsync(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            await _mongoCollection.Indexes.CreateOneAsync(CreateIndexModel(field, IndexType.Text, options));
            return this;
        }

        public IndexCollectionBuilder CreateTextIndex(Expression<Func<BsonDocument, object>> field, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateIndexModel(field, IndexType.Text, options));
            return this;
        }

        public IndexCollectionBuilder CreateCombinedAscendingIndex(params Expression<Func<BsonDocument, object>>[] fields)
        {
            return this.CreateCombinedAscendingIndex(fields, options: null);
        }

        public IndexCollectionBuilder CreateCombinedAscendingIndex(IEnumerable<Expression<Func<BsonDocument, object>>> fields, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateCombinedIndex(fields, IndexType.Ascending, options));
            return this;
        }

        public IndexCollectionBuilder CreateCombinedDescendingIndex(params Expression<Func<BsonDocument, object>>[] fields)
        {
            return this.CreateCombinedDescendingIndex(fields, options: null);
        }

        public IndexCollectionBuilder CreateCombinedDescendingIndex(IEnumerable<Expression<Func<BsonDocument, object>>> fields, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateCombinedIndex(fields, IndexType.Descending, options));
            return this;
        }

        public IndexCollectionBuilder CreateCombinedTextIndex(IEnumerable<Expression<Func<BsonDocument, object>>> fields, IndexCreationOptions options = null)
        {
            _mongoCollection.Indexes.CreateOne(CreateCombinedIndex(fields, IndexType.Text, options));
            return this;
        }

        public async Task<IndexCollectionBuilder> CreateCombinedTextIndexAsync(IEnumerable<Expression<Func<BsonDocument, object>>> fields, IndexCreationOptions options = null)
        {
            await _mongoCollection.Indexes.CreateOneAsync(CreateCombinedIndex(fields, IndexType.Text, options));
            return this;
        }

        private static CreateIndexModel<BsonDocument> CreateCombinedIndex(IEnumerable<Expression<Func<BsonDocument, object>>> fields, IndexType indexType, IndexCreationOptions options)
        {
            var createOptions = options == null ? null : MapIndexOptions(options);
            var listOfDefs = new List<IndexKeysDefinition<BsonDocument>>();
            foreach (var field in fields)
            {
                listOfDefs.Add(GetIndexKeyDefinition(field, indexType));
            }
            return new CreateIndexModel<BsonDocument>(Builders<BsonDocument>.IndexKeys.Combine(listOfDefs), createOptions);
        }

        private static CreateIndexModel<BsonDocument> CreateIndexModel(Expression<Func<BsonDocument, object>> field, IndexType indexType, IndexCreationOptions options)
        {
            var createOptions = options == null ? null : MapIndexOptions(options);
            var indexKeysDefinition = GetIndexKeyDefinition(field, indexType);
            return new CreateIndexModel<BsonDocument>(indexKeysDefinition, createOptions);
        }

        private static IndexKeysDefinition<BsonDocument> GetIndexKeyDefinition(Expression<Func<BsonDocument, object>> field, IndexType indexType)
        {
            var indexKey = Builders<BsonDocument>.IndexKeys;

            return indexType switch
            {
                IndexType.Ascending => indexKey.Ascending(field),
                IndexType.Descending => indexKey.Descending(field),
                IndexType.Text => indexKey.Text(field),
                IndexType.Hashed => indexKey.Hashed(field),
                _ => throw new ArgumentOutOfRangeException(nameof(indexType)),
            };
        }

        private static CreateIndexOptions MapIndexOptions(IndexCreationOptions options)
            => new()
            {
                Unique = options.Unique,
                TextIndexVersion = options.TextIndexVersion,
                SphereIndexVersion = options.SphereIndexVersion,
                Sparse = options.Sparse,
                Name = options.Name,
                Min = options.Min,
                Max = options.Max,
                LanguageOverride = options.LanguageOverride,
                ExpireAfter = options.ExpireAfter,
                DefaultLanguage = options.DefaultLanguage,
                Bits = options.Bits,
                Background = options.Background,
                Version = options.Version
            };
    }
}