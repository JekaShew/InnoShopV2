using FluentValidation;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Application.Validators.ProductValidators
{
    public class TakeFilteredProductDTOListQueryValidator : AbstractValidator<TakeFilteredProductDTOListQuery>
    {
        public TakeFilteredProductDTOListQueryValidator()
        {
            RuleFor(c => c.ProductFilterDTO)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Product's Filter shouldn't be Null!");
        }
    }
}
