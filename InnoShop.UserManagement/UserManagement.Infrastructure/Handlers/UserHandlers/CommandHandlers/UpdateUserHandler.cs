using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public UpdateUserHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }

        public async Task<Response> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _umDBContext.Users.Update(UserMapper.UserDTOToUser(request.UserDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
