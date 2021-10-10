using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Repository.GetByIdAsync<TodoListDocument>(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.Id);
            }

            entity.Title = request.Title;

            await _context.Repository.UpdateOneAsync(entity);
            return Unit.Value;
        }
    }
}