using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Infrastructure.Persistence.Adapters
{
    internal partial class MongoWriteAdapter<TSrc, TKey> : IMongoWriteAdapter<TSrc, TKey> where TSrc : class, IDocument<TKey>
    {
        private readonly ICurrentUserService _currentUserService;

        public IMongoDbContext MongoContext { get; }

        public MongoWriteAdapter(IMongoDbContext mongoContext, ICurrentUserService currentUserService)
        {
            MongoContext = mongoContext;
            _currentUserService = currentUserService;
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

        public virtual TSrc SetAuditable(TSrc document)
        {
            if (document != null && document is AuditableEntity auditableEntity)
            {
                auditableEntity.Created = auditableEntity.Created == default ? DateTime.UtcNow : auditableEntity.Created;
                auditableEntity.CreatedBy ??= _currentUserService.UserId.ToString();
                auditableEntity.LastModifiedBy = _currentUserService.UserId.ToString();
                auditableEntity.LastModified = DateTime.UtcNow;
            }
            return document;
        }

        public virtual IEnumerable<TSrc> SetAuditable(IEnumerable<TSrc> documents)
        {
            foreach (var doc in documents)
            {
                SetAuditable(doc);
            }
            return documents;
        }

        protected virtual UpdateDefinition<TSrc> SetAuditable<TField>(Expression<Func<TSrc, TField>> field, TField value)
        {
            var builder = Builders<TSrc>.Update;
            if (typeof(TSrc).IsAssignableTo(typeof(AuditableEntity)))
            {
                Expression<Func<AuditableEntity, DateTime?>> auditExpressionModified = x => x.LastModified;
                Expression<Func<AuditableEntity, string>> auditExpressionModifiedBy = x => x.LastModifiedBy;
                return builder
                    .Set(ConvertExpression(auditExpressionModified), DateTime.UtcNow)
                    .Set(ConvertExpression(auditExpressionModifiedBy), _currentUserService.UserId)
                    .Set(field, value);
            }

            return builder.Set(field, value);
        }

        protected virtual UpdateDefinition<TSrc> SetAuditable(UpdateDefinition<TSrc> updateDefinition)
        {
            if (typeof(TSrc).IsAssignableTo(typeof(AuditableEntity)))
            {
                Expression<Func<AuditableEntity, DateTime?>> auditExpressionModified = x => x.LastModified;
                Expression<Func<AuditableEntity, string>> auditExpressionModifiedBy = x => x.LastModifiedBy;
                var auditableUpdateDefinition = Builders<TSrc>.Update
                    .Set(ConvertExpression(auditExpressionModified), DateTime.UtcNow)
                    .Set(ConvertExpression(auditExpressionModifiedBy), _currentUserService.UserId);

                return Builders<TSrc>.Update.Combine(auditableUpdateDefinition, updateDefinition);
            }

            return updateDefinition;
        }

        protected virtual Expression<Func<TSrc, object>> ConvertExpression<TValue>(Expression<Func<TSrc, TValue>> expression)
        {
            var param = expression.Parameters[0];
            var body = expression.Body;
            var convert = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<TSrc, object>>(convert, param);
        }

        protected virtual Expression<Func<TSrc, object>> ConvertExpression<TValue>(Expression<Func<AuditableEntity, TValue>> expression)
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
    }
}