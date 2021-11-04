using System.Collections.Generic;
using CleanArchWeb.Domain.Common;
using CleanArchWeb.Domain.ValueObjects;

namespace CleanArchWeb.Domain.Entities
{
    public class TodoListDocument : AuditableDocument
    {
        public string Title { get; set; }

        public Colour Colour { get; set; } = Colour.White;

        public IList<TodoItem> Items { get; init; } = new List<TodoItem>();
    }
}
