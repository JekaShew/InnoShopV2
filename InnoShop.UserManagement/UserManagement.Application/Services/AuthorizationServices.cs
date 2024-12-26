using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RefreshTokenQueries;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Services
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AuthorizationServices(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        public async Task<string> GenerateJwtTokenStringByUserId(Guid userId)
        {
            var userDTO = await _mediator.Send(new TakeUserDTOByIdQuery() { Id = userId });

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userDTO.Email),
                new Claim(ClaimTypes.Name, userDTO.FIO),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, userDTO.Role.Text),
            };

            var jwtHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Authentication:Issuer"],
                Audience = _configuration["Authentication:Audience"],
                //Expires = DateTime.UtcNow.AddHours(4),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials =
                    new SigningCredentials(key,
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = jwtHandler.CreateToken(tokenDescriptor);
            var tokenString = jwtHandler.WriteToken(jwtToken);
            return tokenString;
        }

        public async Task<string> GenerateRefreshTokenByUserId(Guid userId)
        {
            var refreshToken = new RefreshTokenDTO()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                IsRevoked = false,
                ExpireDate = DateTime.UtcNow.AddMinutes(120),
            };

            var addRefreshToken = await _mediator.Send(new AddRefreshTokenCommand() { RefreshTokenDTO = refreshToken });
            if(addRefreshToken.Flag == true)
                return refreshToken.Id.ToString();
            else return null;
        }

        public async Task<Response> IsRefreshTokenCorrectByRTokenId(Guid rTokenId)
        {
            return await _mediator.Send(new IsRefreshTokenCorrectByRTokenIdQuery() { RTokenId = rTokenId });
        }

        public async Task<Response> RevokeTokenByRTokenId(Guid rTokenId)
        {
            return await _mediator.Send(new RevokeRefreshTokenByRTokenIdCommand() { RTokenId = rTokenId });
        }

        public async Task<Guid> TakeUserIdByRTokenId(Guid rTokenId)
        {
            return await _mediator.Send(new TakeUserIdByRTokenIdQuery() { RtokenId = rTokenId });
        }

        public async Task<Response> DeleteRefreshTokenByRTokenId(Guid rTokenId)
        {
            return await _mediator.Send(new DeleteRefreshTokenByRTokenIdCommand() { RTokenId = rTokenId });
        }
    }
}
