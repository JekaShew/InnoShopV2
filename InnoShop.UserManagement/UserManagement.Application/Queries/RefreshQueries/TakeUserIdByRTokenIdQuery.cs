using MediatR;

namespace UserManagement.Application.Queries.RefreshQueries
{
    public class TakeUserIdByRTokenIdQuery : IRequest<Guid>
    {
        public Guid RtokenId { get; set; }
    }
}
