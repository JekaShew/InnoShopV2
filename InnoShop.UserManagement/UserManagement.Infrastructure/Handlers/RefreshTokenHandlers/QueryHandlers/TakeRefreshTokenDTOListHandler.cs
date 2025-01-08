using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RefreshQueries;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class TakeRefreshTokenDTOListHandler : IRequestHandler<TakeRefreshTokenDTOListQuery, List<RefreshTokenDTO>>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public TakeRefreshTokenDTOListHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<List<RefreshTokenDTO>> Handle(TakeRefreshTokenDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _refreshTokenRepository.TakeAllRefreshTokens();
        }
    }
}
