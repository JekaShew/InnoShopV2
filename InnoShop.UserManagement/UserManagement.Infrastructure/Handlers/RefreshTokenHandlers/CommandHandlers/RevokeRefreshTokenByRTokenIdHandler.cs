using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class RevokeRefreshTokenByRTokenIdHandler : IRequestHandler<RevokeRefreshTokenByRTokenIdCommand, Response>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public RevokeRefreshTokenByRTokenIdHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Response> Handle(RevokeRefreshTokenByRTokenIdCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenDTO = await _refreshTokenRepository.TakeRefreshTokenById(request.RTokenId);
                
            if (refreshTokenDTO == null)
                return new Response(false, "Refresh Token Not Found!");

            refreshTokenDTO.IsRevoked = true;

            return await _refreshTokenRepository.UpdateRefreshToken(refreshTokenDTO);
        }
    }
}
