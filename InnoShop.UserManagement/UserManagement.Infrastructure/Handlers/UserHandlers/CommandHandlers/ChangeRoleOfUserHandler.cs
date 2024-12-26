using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class ChangeRoleOfUserHandler : IRequestHandler<ChangeRoleOfUserCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public ChangeRoleOfUserHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(ChangeRoleOfUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _umDBContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            var role = await _umDBContext.Roles.FirstOrDefaultAsync(u => u.Id == request.RoleId);

            if (user is null)
                return new Response(false, "Error occured while changing role! User Not Found!");
            if (role is null)
                return new Response(false, "Error occured while changing role! Role Not Found!");

            user.RoleId = request.RoleId;

            await _umDBContext.SaveChangesAsync(cancellationToken);
            return new Response(true, "Role has been changed successfully!");
        }
    }
}
