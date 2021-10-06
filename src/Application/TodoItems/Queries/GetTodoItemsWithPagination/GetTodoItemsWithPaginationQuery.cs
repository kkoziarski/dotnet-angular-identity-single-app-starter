using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Application.Common.Models;
using CleanArchWeb.Application.TodoLists.Queries.GetTodos;
using MediatR;

namespace CleanArchWeb.Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemDto>>
    {
        public Guid ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TodoItemDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            // return await _context.TodoItems
            //     .Where(x => x.ListId == request.ListId)
            //     .OrderBy(x => x.Title)
            //     .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
            //     .PaginatedListAsync(request.PageNumber, request.PageSize);

            var items = new List<TodoItemDto> { new TodoItemDto() };
            return await Task.FromResult(new PaginatedList<TodoItemDto>(items, 1, request.PageNumber, request.PageSize));
        }
    }
}
