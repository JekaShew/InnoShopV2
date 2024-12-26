using InnoShop.CommonLibrary.Response;

namespace UserManagement.Application.Interfaces
{
    public interface IAuthorizationServices
    {
        public Task<string> GenerateJwtTokenStringByUserId(Guid userId);
        public Task<string> GenerateRefreshTokenByUserId(Guid userId);
        public Task<Response> RevokeTokenByRTokenId(Guid rTokenId);
        public Task<Response> IsRefreshTokenCorrectByRTokenId(Guid rTokenId);
        public Task<Response> DeleteRefreshTokenByRTokenId(Guid rTokenId);
        public Task<Guid> TakeUserIdByRTokenId(Guid rTokenId);

    }
}
