using System;
using CleanArchWeb.Application.Common.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchWeb.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        private Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // return await _context.TodoLists
            //     .Where(l => l.Id != model.Id)
            //     .AllAsync(l => l.Title != title);
        }
    }
}
