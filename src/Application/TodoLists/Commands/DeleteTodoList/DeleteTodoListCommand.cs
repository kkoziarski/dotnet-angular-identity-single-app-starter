﻿using CleanArchWeb.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchWeb.Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            // var entity = await _context.TodoLists
            //     .Where(l => l.Id == request.Id)
            //     .SingleOrDefaultAsync(cancellationToken);
            //
            // if (entity == null)
            // {
            //     throw new NotFoundException(nameof(TodoList), request.Id);
            // }
            //
            // _context.TodoLists.Remove(entity);
            //
            // await _context.SaveChangesAsync(cancellationToken);
            //
            // return Unit.Value;
            return await Unit.Task;
        }
    }
}
