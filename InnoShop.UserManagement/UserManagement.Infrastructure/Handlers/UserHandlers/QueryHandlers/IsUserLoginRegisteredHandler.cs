using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class IsUserLoginRegisteredHandler : IRequestHandler<IsLoginRegisteredQuery, Response>
    {
        private readonly IUser _userRepository;
        public IsUserLoginRegisteredHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(IsLoginRegisteredQuery request, CancellationToken cancellationToken)
        {
            if (await _userRepository.TakeUserWithPredicate(u => u.Login == request.EnteredLogin) != null)
                return new Response(true, "This Login is Registered!");
            else
                return new Response(false, "This Login is available for registration!");
        }
    }
}
