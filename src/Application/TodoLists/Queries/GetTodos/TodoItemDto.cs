using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;
using CleanArchWeb.Application.Common.Mappings;
using CleanArchWeb.Domain.Entities;

namespace CleanArchWeb.Application.TodoLists.Queries.GetTodos
{
    public class TodoItemDto : IMapFrom<TodoItem>
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));

            profile.CreateMap<TodoListDocument, IEnumerable<TodoItemDto>>()
                .ConstructUsing((src, ctx) => ctx.Mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemDto>>(src.Items))
                .AfterMap((src, dest) => dest.ForAll(x => x.ListId = src.Id));
        }
    }
}
