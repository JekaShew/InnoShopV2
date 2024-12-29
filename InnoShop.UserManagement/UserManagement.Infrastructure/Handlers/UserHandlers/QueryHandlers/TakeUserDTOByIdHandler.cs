using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Mappers;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers
{
    public class TakeUserDTOByIdHandler : IRequestHandler<TakeUserDTOByIdQuery, UserDTO>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeUserDTOByIdHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<UserDTO> Handle(TakeUserDTOByIdQuery request, CancellationToken cancellationToken)
        {
            var userDTO = UserMapper.UserToUserDTO(await _umDBContext.Users
                        .Include(us => us.UserStatus)
                        .Include(r => r.Role)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == request.Id));

            return userDTO;
        }
    }
}
