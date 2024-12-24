using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public DeleteUserByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _umDBContext.Users.FindAsync(request.Id);
            if (user == null)
                return new Response(false, "User not found!");
            _umDBContext.Users.Remove(user);
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Deleted!");
        }
    }
}
