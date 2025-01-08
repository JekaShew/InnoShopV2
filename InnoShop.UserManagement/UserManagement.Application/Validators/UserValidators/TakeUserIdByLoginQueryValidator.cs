using FluentValidation;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Validators.UserValidators
{
    public class TakeUserIdByLoginQueryValidator : AbstractValidator<TakeUserIdByLoginQuery>
    {
        public TakeUserIdByLoginQueryValidator()
        {
            RuleFor(c => c.Login)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Login should be entered!");
        }
    }
}
