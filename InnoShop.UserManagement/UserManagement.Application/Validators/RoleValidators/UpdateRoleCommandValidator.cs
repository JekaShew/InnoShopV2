using FluentValidation;
using UserManagement.Application.Commands.RoleCommands;

namespace UserManagement.Application.Validators.RoleValidators
{
    internal class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(c => c.RoleDTO.Title)
               .NotEmpty()
               .NotNull()
               .WithMessage("The Role must have Title!");

            RuleFor(c => c.RoleDTO.Title)
                .MinimumLength(2)
                .WithMessage("The Role's Title should have at least 2 symbols long!");
        }
    }
}
