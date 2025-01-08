using FluentValidation;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Validators.UserValidators
{
    public class CheckLoginSecretWordPairQueryValidator : AbstractValidator<CheckLoginSecretWordPairQuery>
    {
        public CheckLoginSecretWordPairQueryValidator()
        {
            RuleFor(c => c.Login)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Login shouldn't be Null!");

            RuleFor(c => c.SecretWordHash)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Secret Word hash shouldn't be Null!");
        }
    }
}
