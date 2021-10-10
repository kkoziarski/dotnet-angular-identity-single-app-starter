using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using CleanArchWeb.Domain.Enums;
using MediatR;

namespace CleanArchWeb.Application.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommand : IRequest
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
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

            throw new NotImplementedException();

            //entity.ListId = request.ListId; //TODO: implement change of list
            entity.Priority = request.Priority;
            entity.Note = request.Note;

            await _context.Repository.UpdateOneAsync(listDocument);
            return Unit.Value;
        }
    }
}