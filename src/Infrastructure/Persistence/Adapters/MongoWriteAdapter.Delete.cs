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
        public virtual long DeleteOne(TSrc document)
        {
            var filter = Builders<TSrc>.Filter.Eq("Id", document.Id);
            return this.GetCollection().DeleteOne(filter).DeletedCount;
        }

        public virtual async Task<long> DeleteOneAsync(TSrc document)
        {
            var filter = Builders<TSrc>.Filter.Eq("Id", document.Id);
            return (await this.GetCollection().DeleteOneAsync(filter)).DeletedCount;
        }

        public virtual long DeleteOne(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().DeleteOne(filter).DeletedCount;

        public virtual async Task<long> DeleteOneAsync(Expression<Func<TSrc, bool>> filter)
            => (await this.GetCollection().DeleteOneAsync(filter)).DeletedCount;

        public virtual async Task<long> DeleteManyAsync(Expression<Func<TSrc, bool>> filter)
            => (await this.GetCollection().DeleteManyAsync(filter)).DeletedCount;

        public virtual async Task<long> DeleteManyAsync(IEnumerable<TSrc> documents)
        {
            if (!documents.Any())
            {
                return 0;
            }
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return (await this.GetCollection().DeleteManyAsync(x => idsTodelete.Contains(x.Id))).DeletedCount;
        }

        public virtual long DeleteMany(IEnumerable<TSrc> documents)
        {
            if (!documents.Any())
            {
                return 0;
            }
            var idsTodelete = documents.Select(e => e.Id).ToArray();
            return this.GetCollection().DeleteMany(x => idsTodelete.Contains(x.Id)).DeletedCount;
        }

        public virtual long DeleteMany(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().DeleteMany(filter).DeletedCount;

        public virtual bool DeleteAll()
            => this.GetCollection().DeleteMany(this.Filter.Empty).IsAcknowledged;

        public virtual async Task<bool> DeleteAllAsync()
        {
            var result = await this.GetCollection().DeleteManyAsync(this.Filter.Empty);
            return result.IsAcknowledged;
        }
    }
}
