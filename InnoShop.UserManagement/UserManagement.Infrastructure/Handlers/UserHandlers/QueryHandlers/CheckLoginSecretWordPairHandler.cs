using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class CheckLoginSecretWordPairHandler : IRequestHandler<CheckLoginSecretWordPairQuery, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public CheckLoginSecretWordPairHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(CheckLoginSecretWordPairQuery request, CancellationToken cancellationToken)
        {
            if (await _umDBContext.Users.AnyAsync(u => u.Login.Equals(request.Login) && u.SecretWordHash.Equals(request.SecretWordHash)))
                return new Response(true, "Entered data is correct!");
            return new Response(false, "Check data you have entered! Wrong Login or SecretWord!");
        }
    }
}
