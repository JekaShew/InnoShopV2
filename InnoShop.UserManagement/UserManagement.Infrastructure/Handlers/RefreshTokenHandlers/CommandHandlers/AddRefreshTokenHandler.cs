using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers
{
    public class AddRefreshTokenHandler : IRequestHandler<AddRefreshTokenCommand, Response>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public AddRefreshTokenHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Response> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _refreshTokenRepository.AddRefreshToken(request.RefreshTokenDTO);
        }
    }
}
