using System;
using System.Collections.Generic;
using CleanArchWeb.Application.Common.Mappings;
using CleanArchWeb.Domain.Entities;

namespace CleanArchWeb.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoListDocument>
    {
        public TodoListDto()
        {
            Items = new List<TodoItemDto>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }
    }
}
