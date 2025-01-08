using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RefreshQueries;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class TakeUserIdByRTokenIdHandler : IRequestHandler<TakeUserIdByRTokenIdQuery, Guid>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public TakeUserIdByRTokenIdHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Guid> Handle(TakeUserIdByRTokenIdQuery request, CancellationToken cancellationToken)
        {
            return (await _refreshTokenRepository.TakeRefreshTokenById(request.RtokenId)).UserId;
        }
    }
}
