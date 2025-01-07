using FluentValidation;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Commands.ProductStatusCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Validators.ProductStatusValidators
{
    public class UpdateProductStatusCommandValidator : AbstractValidator<UpdateProductStatusCommand>
    {
        public UpdateProductStatusCommandValidator()
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
