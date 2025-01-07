using FluentValidation;
using ProductManagement.Application.Commands.SubCategoryCommands;

namespace ProductManagement.Application.Validators.SubCategoryValidators
{
    public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
    {
        public UpdateSubCategoryCommandValidator()
        {
            RuleFor(c => c.SubCategoryDTO.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("The SubCategory must have Title!");

            RuleFor(c => c.SubCategoryDTO.Title)
                .MinimumLength(2)
                .WithMessage("The SubCategory's Title should have at least 2 symbols long!");

            RuleFor(c => c.SubCategoryDTO.CategoryId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The SubCategory should belong to any Category!");
        }
    }
}
