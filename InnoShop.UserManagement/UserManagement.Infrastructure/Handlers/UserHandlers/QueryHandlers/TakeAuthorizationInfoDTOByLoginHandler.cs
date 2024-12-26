using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeAuthorizationInfoDTOByLoginHandler : IRequestHandler<TakeAuthorizationInfoDTOByLoginQuery, AuthorizationInfoDTO>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeAuthorizationInfoDTOByLoginHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<AuthorizationInfoDTO> Handle(TakeAuthorizationInfoDTOByLoginQuery request, CancellationToken cancellationToken)
        {
            return UserMapper.UserToAuthorizationInfoDTO(await _umDBContext.Users
                                                    .AsNoTracking()
                                                    .FirstOrDefaultAsync(u => u.Login
                                                            .Equals(request.EnteredLogin)));
        }
    }
}
