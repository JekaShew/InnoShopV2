using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Queries.RefreshQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers
{
    public class TakeUserIdByRTokenIdHandler : IRequestHandler<TakeUserIdByRTokenIdQuery, Guid>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeUserIdByRTokenIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Guid> Handle(TakeUserIdByRTokenIdQuery request, CancellationToken cancellationToken)
        {
            return await _umDBContext.RefreshTokens.AsNoTracking().Where(rt => rt.Id == request.RtokenId).Select(rt => rt.UserId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
