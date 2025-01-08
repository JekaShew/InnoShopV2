using FluentValidation;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Validators.UserValidators
{
    public class CheckLoginPasswordPairQueryValidator : AbstractValidator<CheckLoginPasswordPairQuery>
    {
        public CheckLoginPasswordPairQueryValidator()
        {
            RuleFor(c => c.Login)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Login shouldn't be Null!");

            RuleFor(c => c.PasswordHash)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Password hash shouldn't be Null!");
        }
    }
}
