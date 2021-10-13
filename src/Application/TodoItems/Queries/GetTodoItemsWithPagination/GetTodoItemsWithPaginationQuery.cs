﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Application.Common.Mappings;
using CleanArchWeb.Application.Common.Models;
using CleanArchWeb.Application.TodoLists.Queries.GetTodos;
using CleanArchWeb.Domain.Entities;
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
            //TODO: need to be changed for more optimal way - aggregate with $unwind and $project operators
            var items = await _context.Repository
                .ProjectOneAsync<TodoListDocument, IEnumerable<TodoItemDto>>(x => x.Id == request.ListId, x => x.Items.Select(i => _mapper.Map<TodoItemDto>(i)));

            return items
                .ToList()
                .OrderBy(t => t.Title)
                .ToPaginatedList(request.PageNumber, request.PageSize);

            //// return await _context.TodoItems
            ////     .Where(x => x.ListId == request.ListId)
            ////     .OrderBy(x => x.Title)
            ////     .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
            ////     .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
