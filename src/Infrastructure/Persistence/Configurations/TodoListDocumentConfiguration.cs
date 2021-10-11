using CleanArchWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization;

namespace CleanArchWeb.Infrastructure.Persistence.Configurations
{
    internal class TodoListDocumentConfiguration : IEntityTypeConfiguration<TodoListDocument>
    {
        internal static void ConfigureMongo()
        {
            //https://mongodb.github.io/mongo-csharp-driver/1.11/serialization/
            if (!BsonClassMap.IsClassMapRegistered(typeof(TodoListDocument)))
            {
                BsonClassMap.RegisterClassMap<TodoListDocument>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    BsonClassMap.RegisterClassMap<TodoItem>(child =>
                    {
                        child.AutoMap();
                        child.UnmapProperty(x => x.DomainEvents);
                        child.SetIgnoreExtraElements(true);
                    });

                    //cm.MapProperty(c => c.SomeProperty);
                    //cm.MapProperty(c => c.AnotherProperty);
                    //cm.GetMemberMap(c => c.SomeProperty).SetElementName("sp");
                    //cm.GetMemberMap(c => c.SomeProperty).SetElementName("sp").SetOrder(1);
                    //cm.MapIdProperty(c => c.SomeProperty);
                    //cm.GetMemberMap(c => c.SomeProperty).SetIgnoreIfNull(true);
                    //cm.GetMemberMap(c => c.SomeProperty).SetDefaultValue("abc");

                });
                // register class map for MyClass
            }

        }
        public void Configure(EntityTypeBuilder<TodoListDocument> builder)
        {
            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .OwnsOne(b => b.Colour);
        }
    }
}