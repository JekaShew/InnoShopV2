using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class DeleteRefreshTokenByRTokenIdHandler : IRequestHandler<DeleteRefreshTokenByRTokenIdCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public DeleteRefreshTokenByRTokenIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(DeleteRefreshTokenByRTokenIdCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _umDBContext.RefreshTokens
                                    .AsNoTracking()
                                    .Where(rt => rt.Id == request.RTokenId)
                                    .FirstOrDefaultAsync(cancellationToken);
            if (refreshToken == null)
                return new Response(false, "Refresh Token Not Found!");

            _umDBContext.RefreshTokens.Remove(refreshToken);
            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Refresh Token has been successfully Removed!");
        }
    }
}
