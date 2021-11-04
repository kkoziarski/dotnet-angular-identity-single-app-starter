using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<string>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, string>
    {
        private readonly IMongoWriteAdapter<TodoListDocument, string> _writer;

        public CreateTodoListCommandHandler(IMongoWriteAdapter<TodoListDocument, string> writer)
        {
            _writer = writer;
        }

        public async Task<string> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
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