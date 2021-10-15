using System;
using MongoDB.Bson;

namespace CleanArchWeb.Infrastructure.Persistence.Management.Models
{
    [Flags]
    public enum CollectionSystemFlags
    {
        None = 0,
        HaveIdIndex = 1
    }

    [Flags]
    public enum CollectionUserFlags
    {
        None = 0,
        UsePowerOf2Sizes = 1
    }

    [Serializable]
    public class CollectionStatsResult
    {
        private readonly BsonDocument response;

        private IndexSizesResult indexSizes;

        public double AverageObjectSize => this.response["avgObjSize"].ToDouble();

        public long DataSize => this.response["size"].ToInt64();

        public int IndexCount => this.response["nindexes"].ToInt32();

        public IndexSizesResult IndexSizes
        {
            get
            {
                if (this.indexSizes == null)
                {
                    this.indexSizes = new IndexSizesResult(this.response["indexSizes"].AsBsonDocument);
                }
                return this.indexSizes;
            }
        }

        public bool IsCapped => this.response.GetValue("capped", false).ToBoolean();

        public long MaxDocuments => this.response.GetValue("max", 0).ToInt32();

        public string Namespace => this.response["ns"].AsString;

        public long ObjectCount => this.response["count"].ToInt64();

        public long StorageSize => this.response["storageSize"].ToInt64();

        public CollectionSystemFlags SystemFlags
             => this.response.TryGetValue("systemFlags", out var systemFlags) || this.response.TryGetValue("flags", out systemFlags)
                ? (CollectionSystemFlags)systemFlags.ToInt32()
                : CollectionSystemFlags.HaveIdIndex;

        public long TotalIndexSize => this.response["totalIndexSize"].ToInt64();

        public CollectionUserFlags UserFlags
            => this.response.TryGetValue("userFlags", out var userFlags)
                ? (CollectionUserFlags)userFlags.ToInt32()
                : CollectionUserFlags.None;

        public CollectionStatsResult(BsonDocument response) => this.response = response;
    }
}
