using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class CheckLoginSecretWordPairQuery : IRequest<Response>
    {
        public string Login { get; set; }
        public string SecretWordHash { get; set; }
    }
}
