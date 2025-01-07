using FluentValidation;
using ProductManagement.Application.Commands.ProductCommands;

namespace ProductManagement.Application.Validators.ProductValidators
{
    public class ChangeProductStatusOfProductCommandValidator : AbstractValidator<ChangeProductStatusOfProductCommand>
    {
        public ChangeProductStatusOfProductCommandValidator()
        {
            RuleFor(c => c.ProductId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product shouldn't be Null!");

            RuleFor(c => c.ProductStatusId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product shouldn't be Null!");
        }
    }
}
