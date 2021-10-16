using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoWriteAdapter<TSrc, TKey> where TSrc : class, IDocument<TKey>
    {
        public virtual Task AddOneAsync(TSrc document, CancellationToken cancellationToken = default)
            => this.GetCollection().InsertOneAsync(document, null, cancellationToken);

        public virtual Task AddOneAsync(TSrc document, InsertOneOptions options, CancellationToken cancellationToken = default)
            => this.GetCollection().InsertOneAsync(document, options, cancellationToken);

        public virtual void AddOne(TSrc document) => this.GetCollection().InsertOne(document);

        public virtual void AddOne(TSrc document, InsertOneOptions options) => this.GetCollection().InsertOne(document, options);

        public virtual async Task AddManyAsync(IEnumerable<TSrc> documents, CancellationToken cancellationToken = default)
        {
            if (!documents.Any())
            {
                return;
            }

            await this.GetCollection().InsertManyAsync(documents.ToList(), null, cancellationToken);
        }

        public virtual async Task AddManyAsync(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions, CancellationToken cancellationToken = default)
        {
            if (!documents.Any())
            {
                return;
            }

            await this.GetCollection().InsertManyAsync(documents.ToList(), insertManyOptions, cancellationToken);
        }

        public virtual void AddMany(IEnumerable<TSrc> documents)
        {
            if (!documents.Any())
            {
                return;
            }

            this.GetCollection().InsertMany(documents.ToList());
        }

        public virtual void AddMany(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions)
        {
            if (!documents.Any())
            {
                return;
            }

            this.GetCollection().InsertMany(documents.ToList(), insertManyOptions);
        }
    }
}
