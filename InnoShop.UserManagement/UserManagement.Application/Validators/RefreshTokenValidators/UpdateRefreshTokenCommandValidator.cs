﻿using FluentValidation;
using UserManagement.Application.Commands.RefreshTokenCommands;

namespace UserManagement.Application.Validators.RefreshTokenValidators
{
    public class UpdateRefreshTokenCommandValidator : AbstractValidator<UpdateRefreshTokenCommand>
    {
        public UpdateRefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshTokenDTO.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("The Refresh Token Id shouldn't be Null!");

            RuleFor(c => c.RefreshTokenDTO.IsRevoked)
                .NotNull()
                .WithMessage("The Refresh Token's Revoke status shouldn't be Null!");

            RuleFor(c => c.RefreshTokenDTO.ExpireDate)
                .NotNull()
                .WithMessage("The Refresh Token's Expire Date shouldn't be Null!");

            RuleFor(c => c.RefreshTokenDTO.UserId)
                .NotNull()
                .WithMessage("The Refresh Token must have User!");
        }
    }
}
