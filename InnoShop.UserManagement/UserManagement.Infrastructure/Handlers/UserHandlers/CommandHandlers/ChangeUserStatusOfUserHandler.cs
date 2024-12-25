using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class ChangeUserStatusOfUserHandler : IRequestHandler<ChangeUserStatusOfUserCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public ChangeUserStatusOfUserHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(ChangeUserStatusOfUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _umDBContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            var userStatus = await _umDBContext.UserStatuses.FirstOrDefaultAsync(u => u.Id == request.UserStatusId);
            
            if (user is null)
                return new Response(false, "Error occured while changing status! User Not Found!");
            if (userStatus is null)
                return new Response(false, "Error occured while changing status! User Status Not Found!");
            
            user.UserStatusId = request.UserStatusId;

            await _umDBContext.SaveChangesAsync(cancellationToken);
            return new Response(true, "User Status changed successfully!");
        }
    }
}
