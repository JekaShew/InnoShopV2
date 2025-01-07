using FluentValidation;
using ProductManagement.Application.Commands.CategoryCommands;

namespace ProductManagement.Infrastructure.Validators.CategoryValidators
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryDTO.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Category must have Title!");

            RuleFor(c => c.CategoryDTO.Title)
                .MinimumLength(2)
                .WithMessage("The Category's Title should have at least 2 symbols long!");
        }
    }
}
