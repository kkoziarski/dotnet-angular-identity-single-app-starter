using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Common;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Services
{
    public class AuditableService : IAuditableService
    {
        private readonly ICurrentUserService _currentUserService;

        public AuditableService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public TSrc SetAuditable<TSrc>(TSrc document)
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

        public IEnumerable<TSrc> SetAuditable<TSrc>(IEnumerable<TSrc> documents)
        {
            foreach (var doc in documents)
            {
                SetAuditable(doc);
            }
            return documents;
        }

        public UpdateDefinition<TSrc> SetAuditable<TSrc, TField>(Expression<Func<TSrc, TField>> field, TField value)
        {
            var builder = Builders<TSrc>.Update;
            if (typeof(TSrc).IsAssignableTo(typeof(AuditableEntity)))
            {
                Expression<Func<AuditableEntity, DateTime?>> auditExpressionModified = x => x.LastModified;
                Expression<Func<AuditableEntity, string>> auditExpressionModifiedBy = x => x.LastModifiedBy;
                return builder
                    .Set(ConvertExpression<TSrc, DateTime?>(auditExpressionModified), DateTime.UtcNow)
                    .Set(ConvertExpression<TSrc, string>(auditExpressionModifiedBy), _currentUserService.UserId)
                    .Set(field, value);
            }

            return builder.Set(field, value);
        }

        public UpdateDefinition<TSrc> SetAuditable<TSrc>(UpdateDefinition<TSrc> updateDefinition)
        {
            if (typeof(TSrc).IsAssignableTo(typeof(AuditableEntity)))
            {
                Expression<Func<AuditableEntity, DateTime?>> auditExpressionModified = x => x.LastModified;
                Expression<Func<AuditableEntity, string>> auditExpressionModifiedBy = x => x.LastModifiedBy;
                var auditableUpdateDefinition = Builders<TSrc>.Update
                    .Set(ConvertExpression<TSrc, DateTime?>(auditExpressionModified), DateTime.UtcNow)
                    .Set(ConvertExpression<TSrc, string>(auditExpressionModifiedBy), _currentUserService.UserId);

                return Builders<TSrc>.Update.Combine(auditableUpdateDefinition, updateDefinition);
            }

            return updateDefinition;
        }

        private static Expression<Func<TSrc, object>> ConvertExpression<TSrc, TValue>(Expression<Func<AuditableEntity, TValue>> expression)
        {
            var param = expression.Parameters[0];
            var body = expression.Body;
            var convert = Expression.Convert(body, typeof(object));
            return Expression.Lambda<Func<TSrc, object>>(convert, param);
        }
    }
}
