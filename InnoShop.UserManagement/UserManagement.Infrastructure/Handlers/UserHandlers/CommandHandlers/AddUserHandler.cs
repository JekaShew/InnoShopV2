using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public AddUserHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            var userDetailedDTO = UserMapper.RegistrationInfoDTOToUserDetailedDTO(request.RegistrationInfoDTO);
            userDetailedDTO.Id = Guid.NewGuid();
            userDetailedDTO.SecurityStamp = request.SecurityStamp;
            userDetailedDTO.PasswordHash = request.PasswordHash;
            userDetailedDTO.SecretWordHash = request.SecretWordHash;
           
            var roleId = await _umDBContext.Roles
                    .AsNoTracking()
                    .Where(r => r.Title == "User")
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();
            if (roleId == null)
                return new Response(false, "There is no Default Role named User in DB!");
            userDetailedDTO.RoleId = roleId;

            var userStatusId = await _umDBContext.UserStatuses
                    .AsNoTracking()
                    .Where(us => us.Title == "InActive")
                    .Select(us => us.Id)
                    .FirstOrDefaultAsync();
            if (userStatusId == null)
                return new Response(false, "There is no Default Role named InActive in DB!");
            userDetailedDTO.UserStatusId = userStatusId;

            await _umDBContext.Users.AddAsync(UserMapper.UserDetailedDTOToUser(userDetailedDTO));
            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Successfully Added!");
        }
    }
}
