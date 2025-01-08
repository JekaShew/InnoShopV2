using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class CheckLoginSecretWordPairHandler : IRequestHandler<CheckLoginSecretWordPairQuery, Response>
    {
        private readonly IUser _userRepository;
        public CheckLoginSecretWordPairHandler(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Response> Handle(CheckLoginSecretWordPairQuery request, CancellationToken cancellationToken)
        {
            if (await _userRepository.TakeUserWithPredicate(u => 
                                u.Login.Equals(request.Login) && 
                                u.SecretWordHash.Equals(request.SecretWordHash)) != null)
                return new Response(true, "Entered data is correct!");
            else
                return new Response(false, "Check data you have entered! Wrong Login or SecretWord!");
        }
    }
}
