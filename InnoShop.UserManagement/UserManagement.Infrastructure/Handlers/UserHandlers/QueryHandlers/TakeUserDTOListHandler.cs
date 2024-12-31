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
    public class TakeUserDTOListHandler : IRequestHandler<TakeUserDTOListQuery, List<UserDTO>>
    {
        private readonly UserManagementDBContext _umDBContext;
        public TakeUserDTOListHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<List<UserDTO>> Handle(TakeUserDTOListQuery request, CancellationToken cancellationToken)
        {
            var userDTOs = await _umDBContext.Users
                    .AsNoTracking()
                    .Select(u => UserMapper.UserToUserDTO(u))
                    .ToListAsync(cancellationToken);

            return userDTOs;
        }
    }
}
