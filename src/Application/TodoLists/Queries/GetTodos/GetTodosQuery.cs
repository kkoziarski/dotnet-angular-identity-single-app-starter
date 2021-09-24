﻿using AutoMapper;
using CleanArchWeb.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
            return await Task.FromResult(new TodosVm());
            // return new TodosVm
            // {
            //     PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
            //         .Cast<PriorityLevel>()
            //         .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
            //         .ToList(),
            //
            //     Lists = await _context.TodoLists
            //         .AsNoTracking()
            //         .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
            //         .OrderBy(t => t.Title)
            //         .ToListAsync(cancellationToken)
            // };
        }
    }
}
