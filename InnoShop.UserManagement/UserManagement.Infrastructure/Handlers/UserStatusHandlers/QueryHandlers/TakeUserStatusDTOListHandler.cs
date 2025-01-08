using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserStatusQueries;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.QueryHandlers
{
    public class TakeUserStatusDTOListHandler : IRequestHandler<TakeUserStatusDTOListQuery, List<UserStatusDTO>>
    {
        private readonly IUserStatus _userStatusRepository;
        public TakeUserStatusDTOListHandler(IUserStatus userStatusRepository)
        {
            _userStatusRepository = userStatusRepository;
        }
        public async Task<List<UserStatusDTO>> Handle(TakeUserStatusDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _userStatusRepository.TakeAllUserStatuses();
        }
    }
}
