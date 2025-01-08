using FluentValidation;
using UserManagement.Application.Commands.UserCommands;

namespace UserManagement.Application.Validators.UserValidators
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(c => c.RegistrationInfoDTO.FIO)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User should have FIO!");

            RuleFor(c => c.RegistrationInfoDTO.FIO)
                .MinimumLength(2)
                .WithMessage("The User FIO should be at least 2 symbols long!");
            
            RuleFor(c => c.RegistrationInfoDTO.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Email is required!");

            RuleFor(c => c.RegistrationInfoDTO.Email)
                .EmailAddress()
                .WithMessage("Email Address should be valid!");

            RuleFor(c => c.RegistrationInfoDTO.Login)
                .NotEmpty()
                .NotNull()
                .WithMessage("User's Login shouldn't be Null!");

            RuleFor(c => c.RegistrationInfoDTO.Login)
                .MinimumLength(3)
                .WithMessage("The User's Login should be at least 3 symbols long!");

            RuleFor(c => c.RegistrationInfoDTO.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("User's Password shouldn't be Null!");


            RuleFor(c => c.RegistrationInfoDTO.Password)
                .MinimumLength(5)
                .WithMessage("The User's Password should be at least 5 symbols long!");

            RuleFor(c => c.RegistrationInfoDTO.SecretWord)
               .NotEmpty()
               .NotNull()
               .WithMessage("User's Secret Word shouldn't be Null!");


            RuleFor(c => c.RegistrationInfoDTO.SecretWord)
                .MinimumLength(5)
                .WithMessage("The User's Secret Word should be at least 5 symbols long!");

            RuleFor(c => c.SecretWordHash)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User's Secret Word hash shouldn't be Null!");

            RuleFor(c => c.SecurityStamp)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User's Secruty Stamp shouldn't be Null!");

            RuleFor(c => c.PasswordHash)
                .NotEmpty()
                .NotNull()
                .WithMessage("The User's Password hash shouldn't be Null!");
        }
    }
}
