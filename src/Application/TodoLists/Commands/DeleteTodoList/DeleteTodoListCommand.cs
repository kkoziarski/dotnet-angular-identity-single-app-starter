using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Exceptions;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Commands.DeleteTodoList
{
    public class DeleteTodoListCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;
        private readonly IMongoWriteAdapter<TodoListDocument, Guid> _writer;

        public DeleteTodoListCommandHandler(
            IMongoReadAdapter<TodoListDocument> reader,
            IMongoWriteAdapter<TodoListDocument, Guid> writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _reader.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.Id);
            }

            await _writer.DeleteOneAsync(entity);
            return Unit.Value;
        }
    }
}