namespace UserManagement.Application.Interfaces
{
    public interface IAuthorizationServices
    {
        public Task<string> GenerateJwtTokenStringByUserId(Guid userId);
        //public Task<string> GenerateRefreshTokenByUserId(Guid userId);
        //public Task RevokeTokenByRTokenId(Guid rTokenId);
        //public Task<bool> IsRefreshTokenCorrectByRTokenId(Guid rTokenId);
        //public Task DeleteRefreshTokenByRTokenId(Guid rTokenId);

    }
}
