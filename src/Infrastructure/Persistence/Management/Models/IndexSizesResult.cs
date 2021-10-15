using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace CleanArchWeb.Infrastructure.Persistence.Management.Models
{
    public class IndexSizesResult
    {
        private readonly BsonDocument indexSizes;

        public int Count => this.indexSizes.ElementCount;

        public IEnumerable<string> Keys => this.indexSizes.Names;

        public IEnumerable<long> Values => this.indexSizes.Values.Select(v => v.ToInt64());

        public IndexSizesResult(BsonDocument indexSizes) => this.indexSizes = indexSizes;

        public long this[string indexName] => this.indexSizes[indexName].ToInt64();

        public bool ContainsKey(string indexName) => this.indexSizes.Contains(indexName);
    }
}
