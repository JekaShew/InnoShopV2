using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class CheckLoginPasswordPairQuery : IRequest<Response>
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
