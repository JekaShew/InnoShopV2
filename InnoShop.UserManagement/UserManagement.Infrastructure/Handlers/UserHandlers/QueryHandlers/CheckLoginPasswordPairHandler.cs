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
    public class CheckLoginPasswordPairHandler : IRequestHandler<CheckLoginPasswordPairQuery, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public CheckLoginPasswordPairHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(CheckLoginPasswordPairQuery request, CancellationToken cancellationToken)
        {
            if (await _umDBContext.Users.AnyAsync(u => u.Login.Equals(request.Login) && u.PasswordHash.Equals(request.PasswordHash)))
                return new Response(true, "Entered Credentials are correct!");
            return new Response(false, "Check credetials you have entered! Wrong Login or Password!");
        }
    }
}
