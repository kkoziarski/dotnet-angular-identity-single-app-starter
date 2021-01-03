using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.Entities;

namespace CleanArchWeb.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
