using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.RefreshQueries;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class IsRefreshTokenCorrectByRTokenIdHandler : IRequestHandler<IsRefreshTokenCorrectByRTokenIdQuery, Response>
    {
        private readonly IRefreshToken _refreshTokenRepository;
        public IsRefreshTokenCorrectByRTokenIdHandler(IRefreshToken refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<Response> Handle(IsRefreshTokenCorrectByRTokenIdQuery request, CancellationToken cancellationToken)
        {
            var refreshTokenDTO = await _refreshTokenRepository.TakeRefreshTokenById(request.RTokenId);
                
            if (refreshTokenDTO == null)
                return new Response(false, "Refresh Token Not Found!");

            if (refreshTokenDTO.IsRevoked == true)
                return new Response(false, "Refresh Token is Revoked!");

            if (refreshTokenDTO.ExpireDate <= DateTime.UtcNow || refreshTokenDTO.ExpireDate == null)
                return new Response(false, "Refresh Token Expired!");

            return new Response(true, "Refresh Token is Correct!");
        }
    }
}
