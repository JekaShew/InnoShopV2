using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeUserIdByLoginHandler : IRequestHandler<TakeUserIdByLoginQuery, Guid>
    {
        private readonly IUser _userRepository;
        public TakeUserIdByLoginHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> Handle(TakeUserIdByLoginQuery request, CancellationToken cancellationToken)
        {
            return (await _userRepository.TakeUserWithPredicate(u => u.Login == request.Login)).Id;
        }
    }
}
