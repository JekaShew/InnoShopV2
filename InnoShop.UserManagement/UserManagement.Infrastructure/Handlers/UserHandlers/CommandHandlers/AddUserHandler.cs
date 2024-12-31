using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            //var userStatuses = await _umDBContext.UserStatuses.ToListAsync();
            var userStatusId = (await _umDBContext.UserStatuses
                    .AsNoTracking()
                    .Where(us => us.Title == "Activated")
                    .FirstOrDefaultAsync()).Id;
            if (userStatusId == null)
                return new Response(false, "There is no Default User Status named Acitvated in DB!");
            userDetailedDTO.UserStatusId = userStatusId;

            var newUser = UserMapper.UserDetailedDTOToUser(userDetailedDTO);
            await _umDBContext.Users.AddAsync(newUser);
            await _umDBContext.SaveChangesAsync(cancellationToken);

            return new Response(true, "Successfully Added!");
        }
    }
}
