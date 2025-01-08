using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserStatusQueries;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.QueryHandlers
{
    public class TakeUserStatusDTOByIdHandler : IRequestHandler<TakeUserStatusDTOByIdQuery, UserStatusDTO>
    {
        private readonly IUserStatus _userStatusRepository;
        public TakeUserStatusDTOByIdHandler(IUserStatus userStatusRepository)
        {
            _userStatusRepository = userStatusRepository;
        }
        public async Task<UserStatusDTO> Handle(TakeUserStatusDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userStatusRepository.TakeUserStatusById(request.Id);
        }
    }
}
