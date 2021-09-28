using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.DI.identityserver.Storage
{
    public class MongoRepository : IRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public MongoRepository(string mongoConnection, string mongoDatabaseName)
        {
            _client = new MongoClient(mongoConnection);
            _database = _client.GetDatabase(mongoDatabaseName);
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