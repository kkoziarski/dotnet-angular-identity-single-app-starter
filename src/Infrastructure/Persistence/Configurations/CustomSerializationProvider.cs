using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace CleanArchWeb.Infrastructure.Persistence.Configurations
{
    internal class CustomSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            return type switch
            {
                var _type when _type == typeof(DateTime) => new DateTimeUtcSerializer(),
                _ => null
            };
        }
    }

    internal class DateTimeUtcSerializer : DateTimeSerializer
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var utcValue = new DateTime(value.Ticks, DateTimeKind.Utc);
            base.Serialize(context, args, utcValue);
        }
    }
}
