using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers
{
    public class DeleteRoleByIdHandler : IRequestHandler<DeleteRoleByIdCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public DeleteRoleByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(DeleteRoleByIdCommand request, CancellationToken cancellationToken)
        {
            var role = await _umDBContext.Roles.FindAsync(request.Id);
            if (role == null)
                return new Response(false, "Role not found!");
            _umDBContext.Roles.Remove(role);
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Deleted!");
        }
    }
}
