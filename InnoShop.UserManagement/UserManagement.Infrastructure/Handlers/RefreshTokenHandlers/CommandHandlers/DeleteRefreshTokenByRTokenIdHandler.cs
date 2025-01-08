using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class DeleteRefreshTokenByRTokenIdHandler : IRequestHandler<DeleteRefreshTokenByRTokenIdCommand, Response>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public DeleteRefreshTokenByRTokenIdHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Response> Handle(DeleteRefreshTokenByRTokenIdCommand request, CancellationToken cancellationToken)
        {
            return await _refreshTokenRepository.DeleteRefreshTokenById(request.RTokenId);
        }
    }
}
