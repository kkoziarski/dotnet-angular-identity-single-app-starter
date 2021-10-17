using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Attributes;
using CleanArchWeb.Infrastructure.Persistence.Configurations;
using CleanArchWeb.Infrastructure.Utils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CleanArchWeb.Infrastructure.Persistence
{
    internal class MongoDbContext : IMongoDbContext
    {
        private readonly IDictionary<string, bool> cachedCollections;

        public MongoDbContext(
            IMongoClient client,
            MongoConfig mongoConfig)
        {
            this.Client = client;
            this.Database = client.GetDatabase(mongoConfig.DatabaseName);
            this.cachedCollections = new ConcurrentDictionary<string, bool>();
        }

        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public IMongoCollection<TDocument> GetReadCollection<TDocument>() where TDocument : class
            => this.GetReadCollection<TDocument>(this.GetCollectionName<TDocument>());

        public IMongoCollection<TDocument> GetReadCollection<TDocument>(string collectionName) where TDocument : class
            => this.Get<TDocument>(collectionName)
                .WithReadConcern(ReadConcern.Default)
                .WithReadPreference(ReadPreference.Primary);

        public IMongoCollection<TDocument> GetWriteCollection<TDocument>() where TDocument : class
            => this.GetWriteCollection<TDocument>(this.GetCollectionName<TDocument>());

        public IMongoCollection<TDocument> GetWriteCollection<TDocument>(string collectionName) where TDocument : class
            => this.Get<TDocument>(collectionName)
                .WithWriteConcern(WriteConcern.WMajority);

        private IMongoCollection<TDocument> Get<TDocument>(string collectionName)
        {
            if (!this.cachedCollections.TryGetValue(collectionName, out var _))
            {
                this.CreateCollectionIfNotExist<TDocument>(collectionName);
                this.cachedCollections.Add(collectionName, true);
            }
            return this.Database.GetCollection<TDocument>(collectionName);
        }

        private void CreateCollectionIfNotExist<TDocument>(string collectionName)
        {
            if (!this.CollectionExists(collectionName))
            {
                this.Database.CreateCollection(collectionName);
                this.ConfigureCollection<TDocument>(collectionName);
            }
        }

        private bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };
            return this.Database.ListCollectionNames(options).Any();
        }

        private string GetCollectionName<TDocument>() => GetAttributeCollectionName<TDocument>() ?? Pluralize<TDocument>();

        protected virtual string GetAttributeCollectionName<TDocument>()
        {
            return (typeof(TDocument).GetTypeInfo()
                    .GetCustomAttributes(typeof(CollectionNameAttribute))
                    .FirstOrDefault() as CollectionNameAttribute)?.Name;
        }

        private string Pluralize<TDocument>() => typeof(TDocument).Name.Pluralize().Camelize();

        private void ConfigureCollection<TDocument>(string collectionName)
        {
            var mongoCollection = this.Database.GetCollection<BsonDocument>(collectionName);
            //TODO: create and configure indexes
        }
    }
}
