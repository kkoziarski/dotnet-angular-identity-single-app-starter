using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public class MongoRepository : IRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoRepository(IMongoClient mongoClient, IOptions<MongoConfig> mongoConfig)
        {
            _client = mongoClient;
            _database = _client.GetDatabase(mongoConfig.Value.DatabaseName);
        }

        public IQueryable<T> All<T>() where T : class, new()
            => _database.GetCollection<T>(typeof(T).Name).AsQueryable();

        public IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
            => All<T>().Where(expression);

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class, new()
            => _database.GetCollection<T>(typeof(T).Name).DeleteMany(predicate);

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
            => All<T>().Where(expression).SingleOrDefault();

        public void Add<T>(T item) where T : class, new()
            => _database.GetCollection<T>(typeof(T).Name).InsertOne(item);

        public void Add<T>(IEnumerable<T> items) where T : class, new()
            => _database.GetCollection<T>(typeof(T).Name).InsertMany(items);
    }
}
