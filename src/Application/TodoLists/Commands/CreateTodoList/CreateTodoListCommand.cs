using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<Guid>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid>
    {
        private readonly IMongoWriteAdapter<TodoListDocument, Guid> _writer;

        public CreateTodoListCommandHandler(IMongoWriteAdapter<TodoListDocument, Guid> writer)
        {
            _writer = writer;
        }

        public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoListDocument
            {
                Title = request.Title
            };

            await _writer.AddOneAsync(entity, cancellationToken);
            return entity.Id;
        }
    }
}