using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoReadAdapter<TSrc> where TSrc : class
    {
        public virtual Task<TSrc> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<TSrc>.Filter.Eq("Id", id);
            return this.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual TSrc GetById<TKey>(TKey id)
        {
            var filter = Builders<TSrc>.Filter.Eq("Id", id);
            return this.GetCollection().Find(filter).FirstOrDefault();
        }

        public virtual Task<TSrc> GetOneAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default)
            => this.GetCollection().Find(filter).FirstOrDefaultAsync(cancellationToken);

        public virtual TSrc GetOne(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().Find(filter).FirstOrDefault();

        public virtual IFindFluent<TSrc, TSrc> GetCursor(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().Find(filter);

        public async virtual Task<bool> AnyAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default)
        {
            long count;
            if (filter == null)
            {
                count = await this.GetCollection().CountDocumentsAsync(this.Filter.Empty, cancellationToken: cancellationToken);
            }
            else
            {
                count = await this.GetCollection().CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            }
            return (count > 0);
        }

        public virtual bool Any(Expression<Func<TSrc, bool>> filter)
        {
            var count = this.GetCollection().CountDocuments(filter);
            return (count > 0);
        }

        public virtual Task<List<TSrc>> GetAllAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default)
            => this.GetCollection().Find(filter).ToListAsync(cancellationToken);

        public virtual List<TSrc> GetAll(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().Find(filter).ToList();

        public virtual Task<long> CountAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default)
            => filter == null
                ? this.GetCollection().CountDocumentsAsync(this.Filter.Empty, cancellationToken: cancellationToken)
                : this.GetCollection().CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        public virtual long Count(Expression<Func<TSrc, bool>> filter)
            => this.GetCollection().Find(filter).CountDocuments();

        public virtual Task<TSrc> GetByMaxAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> maxValueSelector, CancellationToken cancellationToken = default)
            => this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);

        public virtual TSrc GetByMax(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> maxValueSelector)
            => this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                .SortByDescending(maxValueSelector)
                .Limit(1)
                .FirstOrDefault();

        public virtual Task<TSrc> GetByMinAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> minValueSelector, CancellationToken cancellationToken = default)
            => this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefaultAsync(cancellationToken);

        public virtual TSrc GetByMin(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> minValueSelector)
            => this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                .SortBy(minValueSelector)
                .Limit(1)
                .FirstOrDefault();

        public virtual Task<TValue> GetMaxValueAsync<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> maxValueSelector, CancellationToken cancellationToken = default)
            => this.GetMaxMongoQuery(filter, maxValueSelector)
                .Project(maxValueSelector)
                .FirstOrDefaultAsync(cancellationToken);

        public virtual TValue GetMaxValue<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> maxValueSelector)
            => this.GetMaxMongoQuery(filter, maxValueSelector)
                .Project(maxValueSelector)
                .FirstOrDefault();

        public virtual Task<TValue> GetMinValueAsync<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> minValueSelector, CancellationToken cancellationToken = default)
            => this.GetMinMongoQuery(filter, minValueSelector)
                .Project(minValueSelector)
                .FirstOrDefaultAsync(cancellationToken);

        public virtual TValue GetMinValue<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> minValueSelector)
            => this.GetMinMongoQuery(filter, minValueSelector)
                .Project(minValueSelector)
                .FirstOrDefault();

        public virtual Task<int> SumByAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, int>> selector, CancellationToken cancellationToken = default)
            => this.GetQuery(filter).SumAsync(selector, cancellationToken);

        public virtual int SumBy(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, int>> selector)
            => this.GetQuery(filter).Sum(selector);

        public virtual Task<decimal> SumByAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, decimal>> selector, CancellationToken cancellationToken = default)
            => this.GetQuery(filter).SumAsync(selector, cancellationToken);

        public virtual decimal SumBy(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, decimal>> selector)
            => this.GetQuery(filter).Sum(selector);
    }
}
