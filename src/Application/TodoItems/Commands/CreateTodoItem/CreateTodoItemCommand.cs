using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using CleanArchWeb.Domain.Events;
using MediatR;

namespace CleanArchWeb.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<Guid>
    {
        public Guid ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                Title = request.Title,
                Done = false
            };

            entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

            var listDocument = await _context.Repository.GetByIdAsync<TodoListDocument>(request.ListId);

            if (listDocument == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.ListId);
            }

            listDocument.Items.Add(entity);
            await _context.Repository.UpdateOneAsync(listDocument);
            return entity.Id;
        }
    }
}