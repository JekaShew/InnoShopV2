using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeAuthorizationInfoDTOByUserIdHandler : IRequestHandler<TakeAuthorizationInfoDTOByUserIdQuery, AuthorizationInfoDTO>
    {
        private readonly IUser _userRepository;
        public TakeAuthorizationInfoDTOByUserIdHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AuthorizationInfoDTO> Handle(TakeAuthorizationInfoDTOByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.TakeAuthorizationInfoDTOWithPredicate(u => u.Id == request.UserId);
        }
    }
}
