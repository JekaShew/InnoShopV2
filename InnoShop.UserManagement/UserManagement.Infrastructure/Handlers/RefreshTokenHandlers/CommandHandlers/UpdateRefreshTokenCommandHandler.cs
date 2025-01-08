using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, Response>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public UpdateRefreshTokenCommandHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Response> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _refreshTokenRepository.UpdateRefreshToken(request.RefreshTokenDTO);
        }
    }
}
