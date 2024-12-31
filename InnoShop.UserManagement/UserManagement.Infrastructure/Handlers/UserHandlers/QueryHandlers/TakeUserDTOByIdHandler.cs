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
            var user = await _umDBContext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id.Equals(request.Id)); 
            var userDTO = UserMapper.UserToUserDTO(user);

            return userDTO;
        }
    }
}
