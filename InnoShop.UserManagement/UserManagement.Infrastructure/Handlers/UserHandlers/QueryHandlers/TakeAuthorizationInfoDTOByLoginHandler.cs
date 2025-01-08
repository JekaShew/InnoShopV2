using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeAuthorizationInfoDTOByLoginHandler : IRequestHandler<TakeAuthorizationInfoDTOByLoginQuery, AuthorizationInfoDTO>
    {
        private readonly IUser _userRepository;
        public TakeAuthorizationInfoDTOByLoginHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AuthorizationInfoDTO> Handle(TakeAuthorizationInfoDTOByLoginQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.TakeAuthorizationInfoDTOWithPredicate(u => u.Login
                                                                .Equals(request.EnteredLogin)); 
        }
    }
}
