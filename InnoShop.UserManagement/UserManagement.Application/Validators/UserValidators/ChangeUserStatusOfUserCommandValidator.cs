using FluentValidation;
using UserManagement.Application.Commands.UserCommands;

namespace UserManagement.Application.Validators.UserValidators
{
    public class ChangeUserStatusOfUserCommandValidator : AbstractValidator<ChangeUserStatusOfUserCommand>
    {
        public ChangeUserStatusOfUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User shouldn't be Null!");

            RuleFor(c => c.UserStatusId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User Status shouldn't be Null!");
        }
    }
}
