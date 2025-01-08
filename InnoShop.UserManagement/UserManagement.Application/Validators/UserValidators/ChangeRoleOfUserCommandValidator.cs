using FluentValidation;
using UserManagement.Application.Commands.UserCommands;

namespace UserManagement.Application.Validators.UserValidators
{
    public class ChangeRoleOfUserCommandValidator : AbstractValidator<ChangeRoleOfUserCommand>
    {
        public ChangeRoleOfUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User shouldn't be Null!");

            RuleFor(c => c.RoleId)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Role shouldn't be Null!");

        }
    }
}
