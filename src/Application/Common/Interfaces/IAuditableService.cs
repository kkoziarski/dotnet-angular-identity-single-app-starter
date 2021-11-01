using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CleanArchWeb.Application.Common.Interfaces
{
    public interface IAuditableService
    {
        TSrc SetAuditable<TSrc>(TSrc document);
        IEnumerable<TSrc> SetAuditable<TSrc>(IEnumerable<TSrc> documents);

        UpdateDefinition<TSrc> SetAuditable<TSrc, TField>(Expression<Func<TSrc, TField>> field, TField value);

        UpdateDefinition<TSrc> SetAuditable<TSrc>(UpdateDefinition<TSrc> updateDefinition);
    }
}
