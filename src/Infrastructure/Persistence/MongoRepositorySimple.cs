using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public class MongoRepositorySimple : IRepositorySimple
    {
        private readonly IMongoDatabase _database;

        public MongoRepositorySimple(IMongoClient mongoClient, MongoConfig mongoConfig)
        {
            _database = mongoClient.GetDatabase(mongoConfig.DatabaseName);
        }

        public IQueryable<T> All<T>() where T : class, new()
            => _database.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

        public IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
            => All<T>().Where(expression);

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class, new()
            => _database.GetCollection<T>(GetCollectionName<T>()).DeleteMany(predicate);

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
            => All<T>().Where(expression).SingleOrDefault();

        public void Add<T>(T item) where T : class, new()
            => _database.GetCollection<T>(GetCollectionName<T>()).InsertOne(item);

        public void Add<T>(IEnumerable<T> items) where T : class, new()
            => _database.GetCollection<T>(GetCollectionName<T>()).InsertMany(items);

        private static string GetAttributeCollectionName<T>()
        {
            return (typeof(T).GetTypeInfo()
                            .GetCustomAttributes(typeof(CollectionNameAttribute))
                            .FirstOrDefault() as CollectionNameAttribute)?.Name;
        }

        private static string GetCollectionName<T>()
        {
            return GetAttributeCollectionName<T>() ?? typeof(T).Name;
        }
    }
}
