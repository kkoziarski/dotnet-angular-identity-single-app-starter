using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    public interface IMongoWriteAdapter<TSrc, TKey> where TSrc : class, IDocument<TKey>
    {
        FilterDefinitionBuilder<TSrc> Filter { get; }
        ProjectionDefinitionBuilder<TSrc> Project { get; }
        SortDefinitionBuilder<TSrc> Sort { get; }
        UpdateDefinitionBuilder<TSrc> Updater { get; }

        void AddMany(IEnumerable<TSrc> documents);

        void AddMany(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions);

        Task AddManyAsync(IEnumerable<TSrc> documents, CancellationToken cancellationToken = default);

        Task AddManyAsync(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions, CancellationToken cancellationToken = default);

        void AddOne(TSrc document);

        void AddOne(TSrc document, InsertOneOptions options);

        Task AddOneAsync(TSrc document, CancellationToken cancellationToken = default);

        Task AddOneAsync(TSrc document, InsertOneOptions options, CancellationToken cancellationToken = default);

        bool DeleteAll();

        Task<bool> DeleteAllAsync();

        long DeleteMany(Expression<Func<TSrc, bool>> filter);

        long DeleteMany(IEnumerable<TSrc> documents);

        Task<long> DeleteManyAsync(Expression<Func<TSrc, bool>> filter);

        Task<long> DeleteManyAsync(IEnumerable<TSrc> documents);

        long DeleteOne(Expression<Func<TSrc, bool>> filter);

        long DeleteOne(TSrc document);

        Task<long> DeleteOneAsync(Expression<Func<TSrc, bool>> filter);

        Task<long> DeleteOneAsync(TSrc document);

        IMongoCollection<TSrc> GetCollection();

        IMongoQueryable<TSrc> GetQuery(Expression<Func<TSrc, bool>> filter);

        bool ReplaceMany(IEnumerable<TSrc> modifiedDocuments);

        Task<bool> ReplaceManyAsync(IEnumerable<TSrc> modifiedDocuments);

        Task<bool> ReplaceManyAsync(IEnumerable<TSrc> modifiedDocuments, BulkWriteOptions bulkWriteOptions);

        bool ReplaceOne(TSrc modifiedDocument);

        Task<bool> ReplaceOneAsync(TSrc modifiedDocument);

        Task<bool> ReplaceOneAsync(TSrc modifiedDocument, ReplaceOptions replaceOptions);

        long UpdateMany(FilterDefinition<TSrc> filter, UpdateDefinition<TSrc> UpdateDefinition);

        long UpdateMany<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value);

        long UpdateMany<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value);

        Task<long> UpdateManyAsync(Expression<Func<TSrc, bool>> filter, UpdateDefinition<TSrc> update);

        Task<long> UpdateManyAsync(FilterDefinition<TSrc> filter, UpdateDefinition<TSrc> updateDefinition);

        Task<bool> UpdateManyAsync(IEnumerable<TSrc> modifiedDocuments, BulkWriteOptions bulkWriteOptions);

        Task<long> UpdateManyAsync<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value);

        Task<long> UpdateManyAsync<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value);

        bool UpdateOne(TSrc documentToModify, UpdateDefinition<TSrc> update);

        bool UpdateOne<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value);

        bool UpdateOne<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value);

        bool UpdateOne<TField>(TSrc documentToModify, Expression<Func<TSrc, TField>> field, TField value);

        Task<bool> UpdateOneAsync(TSrc documentToModify, UpdateDefinition<TSrc> update);

        Task<bool> UpdateOneAsync(TSrc modifiedDocument, UpdateOptions options);

        Task<bool> UpdateOneAsync<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value);

        Task<bool> UpdateOneAsync<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value);

        Task<bool> UpdateOneAsync<TField>(TSrc documentToModify, Expression<Func<TSrc, TField>> field, TField value);
    }
}
