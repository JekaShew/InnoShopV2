using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers
{
    public class AddUserStatusHandler : IRequestHandler<AddUserStatusCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public AddUserStatusHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext; 
        }

        public async Task<Response> Handle(AddUserStatusCommand request, CancellationToken cancellationToken)
        {
            request.UserStatusDTO.Id = Guid.NewGuid();
            await _umDBContext.UserStatuses.AddAsync(UserStatusMapper.UserStatusDTOToUserStatus(request.UserStatusDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
