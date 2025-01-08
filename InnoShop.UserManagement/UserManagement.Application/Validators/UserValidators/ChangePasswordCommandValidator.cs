using FluentValidation;
using UserManagement.Application.Commands.UserCommands;

namespace UserManagement.Application.Validators.UserValidators
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User shouldn't be Null!");

            RuleFor(c => c.NewPasswordHash)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Password hash shouldn't be Null!");
        }
    }
}
