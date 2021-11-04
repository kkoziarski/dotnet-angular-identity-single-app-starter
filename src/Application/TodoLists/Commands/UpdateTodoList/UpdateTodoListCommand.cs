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
        public string Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;
        private readonly IMongoWriteAdapter<TodoListDocument, string> _writer;

        public UpdateTodoListCommandHandler(IMongoReadAdapter<TodoListDocument> reader, IMongoWriteAdapter<TodoListDocument, string> writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _reader.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoListDocument), request.Id);
            }

            var updateDefinition = _writer.Updater.Set(f => f.Title, request.Title);
            await _writer.UpdateOneAsync(entity, updateDefinition);
            return Unit.Value;
        }
    }
}