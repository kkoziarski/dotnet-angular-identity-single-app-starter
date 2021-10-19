using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using FluentValidation;

namespace CleanArchWeb.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;

        public CreateTodoListCommandValidator(IMongoReadAdapter<TodoListDocument> reader)
        {
            _reader = reader;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !await _reader.AnyAsync(c => c.Title == title, cancellationToken);
        }
    }
}
