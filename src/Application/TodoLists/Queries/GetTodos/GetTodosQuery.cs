using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using CleanArchWeb.Domain.Enums;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CleanArchWeb.Application.TodoLists.Queries.GetTodos
{
    public class GetTodosQuery : IRequest<TodosVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IMongoReadAdapter<TodoListDocument> reader, IMapper mapper)
        {
            _reader = reader;
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
                Lists = await _reader.ProjectManyAsync(_ => true, x => _mapper.Map<TodoListDocument, TodoListDto>(x), cancellationToken)
            };
        }
    }
}
