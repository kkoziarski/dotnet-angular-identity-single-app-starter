using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

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
            var listDocument = await _context.Repository.GetByIdAsync<TodoListDocument>(request.ListId);

            if (listDocument == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.ListId);
            }

            var entity = listDocument.Items.SingleOrDefault(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            listDocument.Items.Remove(entity);
            await _context.Repository.UpdateOneAsync(listDocument);

            return Unit.Value;
        }
    }
}