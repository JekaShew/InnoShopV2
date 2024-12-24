using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public UpdateRoleHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _umDBContext.Roles.Update(RoleMapper.RoleDTOToRole(request.RoleDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Updated!");
        }
    }
}
