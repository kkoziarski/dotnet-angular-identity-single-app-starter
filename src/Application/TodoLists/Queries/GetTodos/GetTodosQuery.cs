using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using CleanArchWeb.Domain.Enums;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            return new TodosVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = await _context.Repository.ProjectManyAsync<TodoListDocument, TodoListDto>(_ => true, x => new TodoListDto
                {
                    Id = x.Id,
                    Colour = x.Colour,
                    Title = x.Title,
                    Items = x.Items.Select(i => new CleanArchWeb.Application.TodoLists.Queries.GetTodos.TodoItemDto
                    {
                        Id = i.Id,
                        ListId = x.Id,
                        Title = i.Title,
                        Done = i.Done,
                        Note = i.Note,
                        Priority = (int)i.Priority,
                    }).ToList()
                })
            };

            //return new TodosVm
            //{
            //    PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
            //        .Cast<PriorityLevel>()
            //        .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
            //        .ToList(),

            //    Lists = await _context.Repository<TodoListDocument>(_ => true)
            //        .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
            //        .OrderBy(t => t.Title)
            //        .ToListAsync(cancellationToken)
            //};
        }
    }
}
