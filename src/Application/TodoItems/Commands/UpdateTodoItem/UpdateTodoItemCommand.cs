using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
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
            entity.Title = request.Title;
            entity.Done = request.Done;

            await _context.Repository.UpdateOneAsync(listDocument);
            return Unit.Value;
        }
    }
}