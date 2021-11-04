using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Application.Common.Security;
using CleanArchWeb.Domain.Entities;
using MediatR;

namespace CleanArchWeb.Application.TodoLists.Commands.PurgeTodoLists
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanPurge")]
    public class PurgeTodoListsCommand : IRequest
    {
    }

    public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
    {
        private readonly IMongoWriteAdapter<TodoListDocument, string> _writer;

        public PurgeTodoListsCommandHandler(IMongoWriteAdapter<TodoListDocument, string> writer)
        {
            _writer = writer;
        }

        public async Task<Unit> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
        {
            await _writer.DeleteManyAsync(_ => true);
            return Unit.Value;
        }
    }
}
