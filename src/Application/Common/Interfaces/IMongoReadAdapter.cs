using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IMongoReadAdapter<TSrc, TDst> where TSrc : class
    {
        IMongoDbContext MongoContext { get; }

        FilterDefinitionBuilder<TSrc> Filter { get; }
        ProjectionDefinitionBuilder<TSrc> Project { get; }
        SortDefinitionBuilder<TSrc> Sort { get; }
        UpdateDefinitionBuilder<TSrc> Updater { get; }

        bool Any(Expression<Func<TSrc, bool>> filter);

        Task<bool> AnyAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default);

        long Count(Expression<Func<TSrc, bool>> filter);

        Task<long> CountAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default);

        List<TSrc> GetAll(Expression<Func<TSrc, bool>> filter);

        Task<List<TSrc>> GetAllAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default);

        TSrc GetById<TKey>(TKey id);

        Task<TSrc> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default);

        TSrc GetByMax(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> maxValueSelector);

        Task<TSrc> GetByMaxAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> maxValueSelector, CancellationToken cancellationToken = default);

        TSrc GetByMin(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> minValueSelector);

        Task<TSrc> GetByMinAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> minValueSelector, CancellationToken cancellationToken = default);

        IMongoCollection<TSrc> GetCollection();

        IFindFluent<TSrc, TSrc> GetCursor(Expression<Func<TSrc, bool>> filter);

        TValue GetMaxValue<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> maxValueSelector);

        Task<TValue> GetMaxValueAsync<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> maxValueSelector, CancellationToken cancellationToken = default);

        TValue GetMinValue<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> minValueSelector);

        Task<TValue> GetMinValueAsync<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> minValueSelector, CancellationToken cancellationToken = default);

        TSrc GetOne(Expression<Func<TSrc, bool>> filter);

        Task<TSrc> GetOneAsync(Expression<Func<TSrc, bool>> filter, CancellationToken cancellationToken = default);

        IMongoQueryable<TSrc> GetQuery(Expression<Func<TSrc, bool>> filter);

        Task<List<TSrc>> GetSortedPaginatedAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, object>> sortSelector, bool ascending = true, int skipNumber = 0, int takeNumber = 50, CancellationToken cancellationToken = default);

        Task<List<TSrc>> GetSortedPaginatedAsync(Expression<Func<TSrc, bool>> filter, SortDefinition<TSrc> sortDefinition, int skipNumber = 0, int takeNumber = 50, CancellationToken cancellationToken = default);

        List<TProjection> GroupBy<TGroupKey, TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TGroupKey>> selector, Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> projection) where TProjection : class, new();

        List<TProjection> GroupBy<TGroupKey, TProjection>(Expression<Func<TSrc, TGroupKey>> groupingCriteria, Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> groupProjection) where TProjection : class, new();

        Task<List<TProjection>> GroupByAsync<TGroupKey, TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TGroupKey>> selector, Expression<Func<IGrouping<TGroupKey, TSrc>, TProjection>> projection, CancellationToken cancellationToken = default) where TProjection : class, new();

        List<TProjection> ProjectMany<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection) where TProjection : class;

        Task<List<TProjection>> ProjectManyAsync<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection, CancellationToken cancellationToken = default) where TProjection : class;

        TProjection ProjectOne<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection) where TProjection : class;

        Task<TProjection> ProjectOneAsync<TProjection>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TProjection>> projection, CancellationToken cancellationToken = default) where TProjection : class;

        decimal SumBy(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, decimal>> selector);

        int SumBy(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, int>> selector);

        Task<decimal> SumByAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, decimal>> selector, CancellationToken cancellationToken = default);

        Task<int> SumByAsync(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, int>> selector, CancellationToken cancellationToken = default);
    }
}
