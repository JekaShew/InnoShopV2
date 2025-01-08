using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeUserDTOListHandler : IRequestHandler<TakeUserDTOListQuery, List<UserDTO>>
    {
        private readonly IUser _userRepository;
        public TakeUserDTOListHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<UserDTO>> Handle(TakeUserDTOListQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.TakeAllUsers();
        }
    }
}
