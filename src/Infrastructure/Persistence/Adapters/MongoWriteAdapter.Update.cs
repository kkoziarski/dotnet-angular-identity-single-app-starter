using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoWriteAdapter<TSrc, TKey> where TSrc : class, IDocument<TKey>
    {
        public virtual async Task<bool> ReplaceOneAsync(TSrc modifiedDocument)
        {
            SetAuditable(modifiedDocument);
            var filter = Builders<TSrc>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = await this.GetCollection().ReplaceOneAsync(filter, modifiedDocument);
            return updateRes.ModifiedCount == 1;
        }

        public virtual async Task<bool> ReplaceOneAsync(TSrc modifiedDocument, ReplaceOptions replaceOptions)
        {
            SetAuditable(modifiedDocument);
            var filter = Builders<TSrc>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = await this.GetCollection().ReplaceOneAsync(filter, modifiedDocument, replaceOptions);
            return updateRes.ModifiedCount == 1;
        }

        public virtual bool ReplaceOne(TSrc modifiedDocument)
        {
            SetAuditable(modifiedDocument);
            var filter = Builders<TSrc>.Filter.Eq("Id", modifiedDocument.Id);
            var updateRes = this.GetCollection().ReplaceOne(filter, modifiedDocument);
            return updateRes.ModifiedCount == 1;
        }

        public virtual async Task<bool> ReplaceManyAsync(IEnumerable<TSrc> modifiedDocuments)
        {
            var updates = new List<WriteModel<TSrc>>();

            foreach (var doc in modifiedDocuments)
            {
                SetAuditable(doc);
                var filter = Builders<TSrc>.Filter.Eq("Id", doc.Id);
                updates.Add(new ReplaceOneModel<TSrc>(filter, doc));
            }

            var updateRes = await this.GetCollection().BulkWriteAsync(updates);
            return updateRes.ModifiedCount > 0;
        }

        public virtual async Task<bool> ReplaceManyAsync(IEnumerable<TSrc> modifiedDocuments, BulkWriteOptions bulkWriteOptions)
        {
            var updates = new List<WriteModel<TSrc>>();
            foreach (var doc in modifiedDocuments)
            {
                SetAuditable(doc);
                var filter = Builders<TSrc>.Filter.Eq("Id", doc.Id);
                var replaceOneModel = new ReplaceOneModel<TSrc>(filter, doc) { IsUpsert = true };
                updates.Add(replaceOneModel);
            }

            var updateRes = await this.GetCollection().BulkWriteAsync(updates, bulkWriteOptions);
            return updateRes.ModifiedCount > 0;
        }

        public virtual bool ReplaceMany(IEnumerable<TSrc> modifiedDocuments)
        {
            var updates = new List<WriteModel<TSrc>>();

            foreach (var doc in modifiedDocuments)
            {
                SetAuditable(doc);
                var filter = Builders<TSrc>.Filter.Eq("Id", doc.Id);
                updates.Add(new ReplaceOneModel<TSrc>(filter, doc));
            }

            var updateRes = this.GetCollection().BulkWrite(updates);
            return updateRes.ModifiedCount > 0;
        }

        public virtual async Task<bool> UpdateOneAsync(TSrc documentToModify, UpdateDefinition<TSrc> update)
        {
            SetAuditable(documentToModify);
            var filter = Builders<TSrc>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await this.GetCollection().UpdateOneAsync(filter, update);
            return updateRes.ModifiedCount == 1;
        }

        public virtual bool UpdateOne(TSrc documentToModify, UpdateDefinition<TSrc> update)
        {
            SetAuditable(documentToModify);
            var filter = Builders<TSrc>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = this.GetCollection().UpdateOne(filter, update);
            return updateRes.ModifiedCount == 1;
        }

        public virtual async Task<bool> UpdateOneAsync<TField>(TSrc documentToModify, Expression<Func<TSrc, TField>> field, TField value)
        {
            SetAuditable(documentToModify);
            var filter = Builders<TSrc>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = await this.GetCollection().UpdateOneAsync(filter, Builders<TSrc>.Update.Set(field, value));
            return updateRes.ModifiedCount == 1;
        }

        public virtual bool UpdateOne<TField>(TSrc documentToModify, Expression<Func<TSrc, TField>> field, TField value)
        {
            SetAuditable(documentToModify);
            var filter = Builders<TSrc>.Filter.Eq("Id", documentToModify.Id);
            var updateRes = this.GetCollection().UpdateOne(filter, Builders<TSrc>.Update.Set(field, value));
            return updateRes.ModifiedCount == 1;
        }

        public virtual async Task<bool> UpdateOneAsync<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value)
        {
            var updateRes = await this.GetCollection().UpdateOneAsync(filter, SetAuditable(field, value));
            return updateRes.ModifiedCount == 1;
        }

        public virtual Task<bool> UpdateOneAsync<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value)
            => this.UpdateOneAsync(Builders<TSrc>.Filter.Where(filter), field, value);

        public virtual bool UpdateOne<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value)
        {
            var updateRes = this.GetCollection().UpdateOne(filter, SetAuditable(field, value));
            return updateRes.ModifiedCount == 1;
        }

        public virtual bool UpdateOne<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value)
            => this.UpdateOne(Builders<TSrc>.Filter.Where(filter), field, value);

        public virtual Task<long> UpdateManyAsync<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value)
            => this.UpdateManyAsync(Builders<TSrc>.Filter.Where(filter), field, value);

        public virtual async Task<long> UpdateManyAsync<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value)
        {
            var updateRes = await this.GetCollection().UpdateManyAsync(filter, SetAuditable(field, value));
            return updateRes.ModifiedCount;
        }

        public virtual Task<long> UpdateManyAsync(Expression<Func<TSrc, bool>> filter, UpdateDefinition<TSrc> update)
            => this.UpdateManyAsync(Builders<TSrc>.Filter.Where(filter), update);

        public virtual async Task<long> UpdateManyAsync(FilterDefinition<TSrc> filter, UpdateDefinition<TSrc> updateDefinition)
        {
            var updateRes = await this.GetCollection().UpdateManyAsync(filter, SetAuditable(updateDefinition));
            return updateRes.ModifiedCount;
        }

        public virtual long UpdateMany<TField>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TField>> field, TField value)
            => this.UpdateMany(Builders<TSrc>.Filter.Where(filter), field, value);

        public virtual long UpdateMany<TField>(FilterDefinition<TSrc> filter, Expression<Func<TSrc, TField>> field, TField value)
        {
            var updateRes = this.GetCollection().UpdateMany(filter, SetAuditable(field, value));
            return updateRes.ModifiedCount;
        }

        public virtual long UpdateMany(FilterDefinition<TSrc> filter, UpdateDefinition<TSrc> updateDefinition)
        {
            var updateRes = this.GetCollection().UpdateMany(filter, SetAuditable(updateDefinition));
            return updateRes.ModifiedCount;
        }

        public virtual async Task<bool> UpdateOneAsync(TSrc modifiedDocument, UpdateOptions options)
        {
            this.SetAuditable(modifiedDocument);
            var filter = Builders<TSrc>.Filter.Eq("Id", modifiedDocument.Id);
            var update = Builders<TSrc>.Update;

            var updates = (from prop in typeof(TSrc).GetProperties()
                           select update.Set(prop.Name, prop.GetValue(modifiedDocument, null))).ToList();

            var updateRes = await this.GetCollection().UpdateOneAsync(filter, update.Combine(updates), options);
            return updateRes.ModifiedCount > 0;
        }

        public virtual async Task<bool> UpdateManyAsync(IEnumerable<TSrc> modifiedDocuments, BulkWriteOptions bulkWriteOptions)
        {
            var updateRes = default(BulkWriteResult);
            if (modifiedDocuments.Any())
            {
                var writeUpdates = new List<WriteModel<TSrc>>();

                foreach (var doc in modifiedDocuments)
                {
                    this.SetAuditable(doc);
                    var filter = Builders<TSrc>.Filter.Eq("Id", doc.Id);
                    var update = Builders<TSrc>.Update;

                    var updates = (from prop in typeof(TSrc).GetProperties()
                                   select update.Set(prop.Name, prop.GetValue(doc, null))).ToList();

                    var updateOneModel = new UpdateOneModel<TSrc>(filter, update.Combine(updates));

                    writeUpdates.Add(updateOneModel);
                }

                updateRes = await this.GetCollection().BulkWriteAsync(writeUpdates, bulkWriteOptions);
            }
            return updateRes.ModifiedCount > 0;
        }
    }
}
