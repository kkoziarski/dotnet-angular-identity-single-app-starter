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
            => this.GetCollection().InsertOneAsync(SetAuditable(document), null, cancellationToken);

        public virtual Task AddOneAsync(TSrc document, InsertOneOptions options, CancellationToken cancellationToken = default)
            => this.GetCollection().InsertOneAsync(SetAuditable(document), options, cancellationToken);

        public virtual void AddOne(TSrc document) => this.GetCollection().InsertOne(SetAuditable(document));

        public virtual void AddOne(TSrc document, InsertOneOptions options) => this.GetCollection().InsertOne(SetAuditable(document), options);

        public virtual async Task AddManyAsync(IEnumerable<TSrc> documents, CancellationToken cancellationToken = default)
        {
            if (!documents.Any())
            {
                return;
            }

            await this.GetCollection().InsertManyAsync(SetAuditable(documents).ToList(), null, cancellationToken);
        }

        public virtual async Task AddManyAsync(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions, CancellationToken cancellationToken = default)
        {
            if (!documents.Any())
            {
                return;
            }

            await this.GetCollection().InsertManyAsync(SetAuditable(documents).ToList(), insertManyOptions, cancellationToken);
        }

        public virtual void AddMany(IEnumerable<TSrc> documents)
        {
            if (!documents.Any())
            {
                return;
            }

            this.GetCollection().InsertMany(SetAuditable(documents).ToList());
        }

        public virtual void AddMany(IEnumerable<TSrc> documents, InsertManyOptions insertManyOptions)
        {
            if (!documents.Any())
            {
                return;
            }

            this.GetCollection().InsertMany(SetAuditable(documents).ToList(), insertManyOptions);
        }
    }
}
