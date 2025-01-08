using FluentValidation;
using UserManagement.Application.Commands.UserStatusCommands;

namespace UserManagement.Application.Validators.UserStatusValidators
{
    public class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
    {
        public UpdateUserStatusCommandValidator()
        {
            RuleFor(c => c.UserStatusDTO.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User Status must have Title!");

            RuleFor(c => c.UserStatusDTO.Title)
                .MinimumLength(2)
                .WithMessage("The User Status's Title should have at least 2 symbols long!");
        }
    }
}
