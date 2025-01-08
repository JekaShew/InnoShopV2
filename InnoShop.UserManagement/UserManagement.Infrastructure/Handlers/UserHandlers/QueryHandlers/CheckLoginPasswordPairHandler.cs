using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class CheckLoginPasswordPairHandler : IRequestHandler<CheckLoginPasswordPairQuery, Response>
    {
        private readonly IUser _userRepository;
        public CheckLoginPasswordPairHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(CheckLoginPasswordPairQuery request, CancellationToken cancellationToken)
        {
            if (await _userRepository.TakeUserWithPredicate(u => 
                            u.Login.Equals(request.Login) && 
                            u.PasswordHash.Equals(request.PasswordHash)) != null)
                return new Response(true, "Entered Credentials are correct!");
            else
                return new Response(false, "Check credetials you have entered! Wrong Login or Password!");
        }
    }
}
