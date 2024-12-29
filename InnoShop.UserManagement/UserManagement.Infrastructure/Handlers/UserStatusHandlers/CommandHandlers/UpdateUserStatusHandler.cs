using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class UpdateUserStatusHandler : IRequestHandler<UpdateUserStatusCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public UpdateUserStatusHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            _umDBContext.UserStatuses.Update(UserStatusMapper.UserStatusDTOToUserStatus(request.UserStatusDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
