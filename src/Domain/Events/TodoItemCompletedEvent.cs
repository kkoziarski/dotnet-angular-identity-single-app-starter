using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.Entities;

namespace CleanArchWeb.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
