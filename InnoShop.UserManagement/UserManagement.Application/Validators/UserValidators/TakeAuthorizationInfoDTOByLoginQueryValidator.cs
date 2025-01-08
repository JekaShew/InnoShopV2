using FluentValidation;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Validators.UserValidators
{
    public class TakeAuthorizationInfoDTOByLoginQueryValidator : AbstractValidator<TakeAuthorizationInfoDTOByLoginQuery>
    {
        public TakeAuthorizationInfoDTOByLoginQueryValidator()
        {
            RuleFor(c => c.EnteredLogin)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Login should be entered!");
        }
    }
}
