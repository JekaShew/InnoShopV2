using FluentValidation;
using ProductManagement.Application.Commands.ProductStatusCommands;

namespace ProductManagement.Application.Validators.ProductStatusValidators
{
    public class AddProductStatusCommandValidator : AbstractValidator<AddProductStatusCommand>
    {
        public AddProductStatusCommandValidator()
        {
            RuleFor(c => c.ProductStatusDTO.Title)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Product Status must have Title!");

            RuleFor(c => c.ProductStatusDTO.Title)
                .MinimumLength(2)
                .WithMessage("The Product Status's Title should have at least 2 symbols long!");
        }
    }
}
