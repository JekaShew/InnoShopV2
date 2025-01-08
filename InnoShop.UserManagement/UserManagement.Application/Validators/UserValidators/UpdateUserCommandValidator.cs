using FluentValidation;
using UserManagement.Application.Commands.UserCommands;

namespace UserManagement.Application.Validators.UserValidators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.UserDTO.FIO)
               .NotEmpty()
               .NotNull()
               .WithMessage("The User should have FIO!");

            RuleFor(c => c.UserDTO.FIO)
                .MinimumLength(2)
                .WithMessage("The User FIO should be at least 2 symbols long!");

            RuleFor(c => c.UserDTO.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is required!");

            RuleFor(c => c.UserDTO.Email)
               .EmailAddress()
               .WithMessage("Email Address should be valid!");

            RuleFor(c => c.UserDTO.Login)
                .NotEmpty()
                .NotNull()
                .WithMessage("User's Login shouldn't be Null!");

            RuleFor(c => c.UserDTO.Login)
                .MinimumLength(3)
                .WithMessage("The User's Login should be at least 3 symbols long!");

            RuleFor(c => c.UserDTO.UserStatusId)
                .NotEmpty()
                .NotNull()
                .WithMessage("User's Status shouldn't be Null!");

            RuleFor(c => c.UserDTO.RoleId)
                .NotEmpty()
                .NotNull()
                .WithMessage("User's Role shouldn't be Null!");
        }
    }
}
