using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.DI.IdentityServerSample.Repository
{
    public interface IRepository
    {
        IMongoDatabase GetDatabase();
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new();
        T Single<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> expression) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Add<T>(IEnumerable<T> items) where T : class, new();
        bool CollectionExists<T>() where T : class, new();
    }
}
