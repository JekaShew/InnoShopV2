using MediatR;

namespace UserManagement.Application.Queries.RefreshTokenQueries
{
    public class TakeUserIdByRTokenIdQuery : IRequest<Guid>
    {
        public Guid RtokenId { get; set; }
    }
}
