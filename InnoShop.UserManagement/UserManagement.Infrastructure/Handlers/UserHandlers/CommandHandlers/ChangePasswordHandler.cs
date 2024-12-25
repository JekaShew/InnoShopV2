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
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public ChangePasswordHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _umDBContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user is null)
                return new Response(false, "Error occured while changing password! User not found!");
            user.PasswordHash = request.NewPasswordHash;

            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Password changed successfully!");
        }
    }
}
