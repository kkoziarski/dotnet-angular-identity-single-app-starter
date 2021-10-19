using System.Threading;
using System.Threading.Tasks;
using CleanArchWeb.Application.Common.Interfaces;
using CleanArchWeb.Domain.Entities;
using FluentValidation;

namespace CleanArchWeb.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly IMongoReadAdapter<TodoListDocument> _reader;

        public UpdateTodoListCommandValidator(IMongoReadAdapter<TodoListDocument> reader)
        {
            _reader = reader;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        private async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
        {
            return !await _reader.AnyAsync(c => c.Id != model.Id && c.Title == title, cancellationToken);
        }
    }
}
