using System;
using System.Collections.Generic;
using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.Enums;
using CleanArchWeb.Domain.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CleanArchWeb.Domain.Entities
{
    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Title { get; set; }

        public string Note { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    DomainEvents.Add(new TodoItemCompletedEvent(this));
                }

                _done = value;
            }
        }

        [BsonIgnore]
        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}