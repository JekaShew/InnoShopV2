using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public AddRoleHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            request.RoleDTO.Id = Guid.NewGuid();
            await _umDBContext.Roles.AddAsync(RoleMapper.RoleDTOToRole(request.RoleDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
