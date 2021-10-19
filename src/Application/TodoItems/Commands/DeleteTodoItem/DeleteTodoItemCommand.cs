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
        private readonly IMongoReadAdapter<TodoListDocument> _reader;
        private readonly IMongoWriteAdapter<TodoListDocument, Guid> _writer;

        public DeleteTodoItemCommandHandler(IMongoReadAdapter<TodoListDocument> reader, IMongoWriteAdapter<TodoListDocument, Guid> writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
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

            listDocument.Items.Remove(entity);
            await _writer.ReplaceOneAsync(listDocument);

            return Unit.Value;
        }
    }
}