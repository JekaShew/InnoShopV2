using InnoShop.CommonLibrary.Response;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Interfaces
{
    public interface IRefreshToken
    {
        public Task<Response> AddRefreshToken(RefreshTokenDTO refreshTokenDTO);
        public Task<List<RefreshTokenDTO>> TakeAllRefreshTokens();
        public Task<RefreshTokenDTO> TakeRefreshTokenById(Guid refreshTokenId);
        public Task<Response> UpdateRefreshToken(RefreshTokenDTO refreshTokenDTO);
        public Task<Response> DeleteRefreshTokenById(Guid refreshTokenId);
    }
}
