using FluentValidation;
using ProductManagement.Application.Commands.ProductCommands;

namespace ProductManagement.Application.Validators.ProductValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(c => c.ProductDTO.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product should have Title!");

            RuleFor(c => c.ProductDTO.Title)
                .MinimumLength(2)
                .WithMessage("The Product's Title should have at least 2 symbols long!");

            RuleFor(c => c.ProductDTO.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Product should have Price!");

            RuleFor(c => c.ProductDTO.Price)
               .LessThan(Decimal.MaxValue)
               .GreaterThan(0.01M)
               .WithMessage($"The Product's Price should be more than 0.01 and less than {Decimal.MaxValue}!");            

            RuleFor(c => c.ProductDTO.ProductStatusId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product should have any Product Status!");

            RuleFor(c => c.ProductDTO.SubCategoryId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product should belong to any SubCategory!");
        }
    }
}
