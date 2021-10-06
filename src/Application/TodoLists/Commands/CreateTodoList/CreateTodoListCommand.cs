using System;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchWeb.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<Guid>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoListDocument();

            entity.Title = request.Title;

            // _context.TodoLists.Add(entity);
            //
            // await _context.SaveChangesAsync(cancellationToken);
            //
            return await Task.FromResult(entity.Id);
        }
    }
}