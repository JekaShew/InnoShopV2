using InnoShop.CommonLibrary.Response;
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
    public class IsUserLoginRegisteredHandler : IRequestHandler<IsUserLoginRegisteredQuery, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public IsUserLoginRegisteredHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(IsUserLoginRegisteredQuery request, CancellationToken cancellationToken)
        {
            if (await _umDBContext.Users.AnyAsync(u => u.Login == request.EnteredLogin))
                return new Response(true, "This Login is Registered!");
            else
                return new Response(false, "This Login is available for registration!");
        }
    }
}
