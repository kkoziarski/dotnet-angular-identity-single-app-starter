﻿using System;
using CleanArchWeb.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchWeb.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            // var entity = await _context.TodoItems.FindAsync(request.Id);
            //
            // if (entity == null)
            // {
            //     throw new NotFoundException(nameof(TodoItem), request.Id);
            // }
            //
            // _context.TodoItems.Remove(entity);
            //
            // await _context.SaveChangesAsync(cancellationToken);
            //
            // return Unit.Value;
            return await Unit.Task;
        }
    }
}