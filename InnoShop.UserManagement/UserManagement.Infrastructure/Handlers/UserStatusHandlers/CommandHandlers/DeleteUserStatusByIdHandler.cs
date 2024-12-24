using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class DeleteUserStatusByIdHandler : IRequestHandler<DeleteUserStatusByIdCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public DeleteUserStatusByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(DeleteUserStatusByIdCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _umDBContext.UserStatuses.FindAsync(request.Id);
            if (userStatus == null)
                return new Response(false, "User Status not found!");
            _umDBContext.UserStatuses.Remove(userStatus);
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Deleted!");
        }
    }
}
