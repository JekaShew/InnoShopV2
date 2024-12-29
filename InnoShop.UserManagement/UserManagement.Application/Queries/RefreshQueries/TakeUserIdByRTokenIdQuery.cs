using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Queries.RefreshQueries
{
    public class TakeUserIdByRTokenIdQuery : IRequest<Guid>
    {
        public Guid RtokenId { get; set; }
    }
}
