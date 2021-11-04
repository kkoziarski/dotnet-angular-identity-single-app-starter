using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;
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

        public string Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TodoItemDto> Items { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoListDocument, TodoListDto>()
                .AfterMap((src, dest) => dest.Items.ForAll(x => x.ListId = src.Id));
        }
    }
}
