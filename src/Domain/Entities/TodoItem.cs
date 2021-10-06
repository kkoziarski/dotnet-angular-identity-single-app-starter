using System;
using System.Collections.Generic;
using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.Enums;
using CleanArchWeb.Domain.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace CleanArchWeb.Domain.Entities
{
    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonIgnore]
        public Guid ListId { get; set; }

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

        public List<DomainEvent> DomainEvents { get; set; } = new();
    }
}