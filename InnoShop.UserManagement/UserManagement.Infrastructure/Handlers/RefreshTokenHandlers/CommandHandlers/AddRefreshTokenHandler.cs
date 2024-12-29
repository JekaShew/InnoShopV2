using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class AddRefreshTokenHandler : IRequestHandler<AddRefreshTokenCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public AddRefreshTokenHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _umDBContext.Users.AsNoTracking().AnyAsync(u => u.Id == request.RefreshTokenDTO.UserId);

            if (!checkUser)
                return new Response(false, "Can't add Refresh Token! User not Found!");

            await _umDBContext.RefreshTokens.AddAsync(RefreshTokenMapper.RefreshTokenDTOToRefreshToken(request.RefreshTokenDTO));
            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Refresh Token has been successfully Added!");
        }
    }
}
