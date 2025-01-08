using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeUserIdByLoginQuery : IRequest<Guid>
    {
        public string Login { get; set; }
    }
}
