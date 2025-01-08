using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeUserDTOByIdHandler : IRequestHandler<TakeUserDTOByIdQuery, UserDTO>
    {
        private readonly IUser _userRepository;
        public TakeUserDTOByIdHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> Handle(TakeUserDTOByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.TakeUserById(request.Id);
        }
    }
}
