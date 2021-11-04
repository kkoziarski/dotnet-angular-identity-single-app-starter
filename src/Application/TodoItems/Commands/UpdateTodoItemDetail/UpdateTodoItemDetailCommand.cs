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
        public string Id { get; set; }

        public string ListId { get; set; }

        public string NewListId { get; set; }

        public PriorityLevel Priority { get; set; }

        public string Note { get; set; }
    }

    public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;
        private readonly IMongoWriteAdapter<TodoListDocument, string> _writer;
        private readonly IAuditableService _auditableService;

        public UpdateTodoItemDetailCommandHandler(IMongoReadAdapter<TodoListDocument> reader, IMongoWriteAdapter<TodoListDocument, string> writer, IAuditableService auditableService)
        {
            _reader = reader;
            _writer = writer;
            _auditableService = auditableService;
        }

        public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
        {
            var listDocument = await _reader.GetByIdAsync(request.ListId, cancellationToken);

            if (listDocument == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.ListId);
            }

            var entity = listDocument.Items.SingleOrDefault(x => x.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            TodoListDocument newListDocument = null;

            if (request.ListId != request.NewListId)
            {
                newListDocument = await _reader.GetByIdAsync(request.NewListId);
                if (newListDocument == null)
                {
                    throw new NotFoundException(nameof(TodoListDocument), request.NewListId);
                }

                listDocument.Items.Remove(entity);
                newListDocument.Items.Add(entity);
            }

            entity.Priority = request.Priority;
            entity.Note = request.Note;
            _auditableService.SetAuditable(entity);

            if (newListDocument != null)
            {
                await _writer.ReplaceOneAsync(newListDocument);
            }
            await _writer.ReplaceOneAsync(listDocument);

            return Unit.Value;
        }
    }
}