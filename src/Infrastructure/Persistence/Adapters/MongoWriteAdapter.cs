using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoWriteAdapter<TSrc, TKey> : IMongoWriteAdapter<TSrc, TKey> where TSrc : class, IDocument<TKey>
    {
        private readonly IAuditableService _auditableService;

        public IMongoDbContext MongoContext { get; }

        public MongoWriteAdapter(IMongoDbContext mongoContext, IAuditableService auditableService)
        {
            MongoContext = mongoContext;
            _auditableService = auditableService;
        }

        public virtual IMongoCollection<TSrc> GetCollection()
            => this.MongoContext.GetWriteCollection<TSrc>();

        public FilterDefinitionBuilder<TSrc> Filter => Builders<TSrc>.Filter;

        public ProjectionDefinitionBuilder<TSrc> Project => Builders<TSrc>.Projection;

        public UpdateDefinitionBuilder<TSrc> Updater => Builders<TSrc>.Update;

        public SortDefinitionBuilder<TSrc> Sort => Builders<TSrc>.Sort;

        public virtual IMongoQueryable<TSrc> GetQuery(Expression<Func<TSrc, bool>> filter)
            => filter != null
                ? this.GetCollection().AsQueryable().Where(filter)
                : this.GetCollection().AsQueryable();

        protected virtual Expression<Func<TSrc, object>> ConvertExpression<TValue>(Expression<Func<TSrc, TValue>> expression)
        {
            var param = expression.Parameters[0];
            var body = expression.Body;
            var convert = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<TSrc, object>>(convert, param);
        }

        protected virtual IFindFluent<TSrc, TSrc> GetMinMongoQuery<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> minValueSelector)
            => filter == null
                ? this.GetCollection().Find(this.Filter.Empty)
                    .SortBy(this.ConvertExpression(minValueSelector))
                    .Limit(1)
                : this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                    .SortBy(this.ConvertExpression(minValueSelector))
                    .Limit(1);

        protected virtual IFindFluent<TSrc, TSrc> GetMaxMongoQuery<TValue>(Expression<Func<TSrc, bool>> filter, Expression<Func<TSrc, TValue>> maxValueSelector)
            => filter == null
                ? this.GetCollection().Find(this.Filter.Empty)
                    .SortByDescending(this.ConvertExpression(maxValueSelector))
                    .Limit(1)
                : this.GetCollection().Find(Builders<TSrc>.Filter.Where(filter))
                    .SortByDescending(this.ConvertExpression(maxValueSelector))
                    .Limit(1);

        protected virtual TSrc SetAuditable(TSrc document)
            => _auditableService.SetAuditable(document);

        protected virtual IEnumerable<TSrc> SetAuditable(IEnumerable<TSrc> documents)
            => _auditableService.SetAuditable(documents);

        protected virtual UpdateDefinition<TSrc> SetAuditable<TField>(Expression<Func<TSrc, TField>> field, TField value)
            => _auditableService.SetAuditable(field, value);

        protected virtual UpdateDefinition<TSrc> SetAuditable(UpdateDefinition<TSrc> updateDefinition)
            => _auditableService.SetAuditable(updateDefinition);
    }
}