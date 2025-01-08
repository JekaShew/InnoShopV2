using FluentValidation;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Validators.UserValidators
{
    public class IsLoginRegisteredQueryValidator : AbstractValidator<IsLoginRegisteredQuery>
    {
        public IsLoginRegisteredQueryValidator()
        {
            RuleFor(c => c.EnteredLogin)
                 .NotEmpty()
                 .NotNull()
                 .WithMessage("The Login should be entered!");
        }
    }
}
