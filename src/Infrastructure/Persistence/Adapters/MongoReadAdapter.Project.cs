using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoReadAdapter<TSrc, TDst> where TSrc : class
    {
        public virtual List<TProjection> GroupBy<TGroupKey, TProjection>(Expression<Func<TSrc, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> groupProjection)
            where TProjection : class, new()
            => this.GetCollection()
                .Aggregate()
                .Group(groupingCriteria, groupProjection)
                .ToList();

        public virtual List<TProjection> GroupBy<TGroupKey, TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TGroupKey>> selector, Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> projection)
            where TProjection : class, new()
            => this.GetCollection()
                .Aggregate()
                .Match(Builders<TSrc>.Filter.Where(filter))
                .Group(selector, projection)
                .ToList();

        public virtual Task<List<TProjection>> GroupByAsync<TGroupKey, TProjection>(
            Expression<Func<TSrc, bool>> filter,
            Expression<Func<TSrc, TGroupKey>> selector,
            Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> projection,
            CancellationToken cancellationToken = default)
            where TProjection : class, new()
            => this.GetCollection()
                .Aggregate()
                .Match(Builders<TSrc>.Filter.Where(filter))
                .Group(selector, projection)
                .ToListAsync(cancellationToken);

        public virtual Task<List<TSrc>> GetSortedPaginatedAsync(
            Expression<Func<TSrc, bool>> filter,
            Expression<Func<TSrc, object>> sortSelector,
            bool ascending = true,
            int skipNumber = 0,
            int takeNumber = 50,
            CancellationToken cancellationToken = default)
        {
            var sorting = ascending
                ? Builders<TSrc>.Sort.Ascending(sortSelector)
                : Builders<TSrc>.Sort.Descending(sortSelector);
            return this.GetCollection()
                                    .Find(filter)
                                    .Sort(sorting)
                                    .Skip(skipNumber)
                                    .Limit(takeNumber)
                                    .ToListAsync(cancellationToken);
        }

        public virtual Task<List<TSrc>> GetSortedPaginatedAsync(
            Expression<Func<TSrc, bool>> filter,
            SortDefinition<TSrc> sortDefinition,
            int skipNumber = 0,
            int takeNumber = 50,
            CancellationToken cancellationToken = default)
            => this.GetCollection()
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(skipNumber)
                .Limit(takeNumber)
                .ToListAsync(cancellationToken);

        public virtual Task<TProjection> ProjectOneAsync<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection, CancellationToken cancellationToken = default)
            where TProjection : class
            => this.GetCollection().Find(filter)
                .Project(projection)
                .FirstOrDefaultAsync(cancellationToken);

        public virtual TProjection ProjectOne<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection)
            where TProjection : class
            => this.GetCollection().Find(filter)
                .Project(projection)
                .FirstOrDefault();

        public virtual Task<List<TProjection>> ProjectManyAsync<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection, CancellationToken cancellationToken = default)
            where TProjection : class
            => this.GetCollection()
                .Find(filter)
                .Project(projection)
                .ToListAsync(cancellationToken);

        public virtual List<TProjection> ProjectMany<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection)
            where TProjection : class
            => this.GetCollection().Find(filter)
                .Project(projection)
                .ToList();
    }
}
