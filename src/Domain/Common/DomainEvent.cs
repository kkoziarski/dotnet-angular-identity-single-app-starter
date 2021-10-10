using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace CleanArchWeb.Domain.Common
{
    public interface IHasDomainEvent
    {
        [BsonIgnore]
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }
}
