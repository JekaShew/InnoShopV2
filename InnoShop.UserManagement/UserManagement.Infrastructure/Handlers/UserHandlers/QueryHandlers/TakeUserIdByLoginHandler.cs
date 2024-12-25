using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeUserIdByLoginHandler : IRequestHandler<TakeUserIdByLoginQuery, Guid>
    {
        private  readonly UserManagementDBContext _umDBContext;
        public TakeUserIdByLoginHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Guid> Handle(TakeUserIdByLoginQuery request, CancellationToken cancellationToken)
        {
            var userId = await _umDBContext.Users.AsNoTracking().Where(u => u.Login == request.Login).Select(u => u.Id).FirstOrDefaultAsync();

            return userId;
        }
    }
}
