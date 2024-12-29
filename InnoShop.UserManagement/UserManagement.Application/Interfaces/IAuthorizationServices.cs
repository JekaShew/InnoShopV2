using InnoShop.CommonLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
