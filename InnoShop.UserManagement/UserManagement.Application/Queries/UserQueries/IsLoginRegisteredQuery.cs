using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class IsLoginRegisteredQuery : IRequest<Response>
    {
        public string EnteredLogin { get; set; }
    }
}
