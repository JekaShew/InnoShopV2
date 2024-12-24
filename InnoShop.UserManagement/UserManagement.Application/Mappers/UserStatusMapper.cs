using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Data.Models;

namespace UserManagement.Application.Mappers
{
    [Mapper]
    public static partial class UserStatusMapper
    {
        public static partial UserStatusDTO? UserStatusToUserStatusDTO(UserStatus? userStatus);
        public static partial UserStatus? UserStatusDTOToUserStatus(UserStatusDTO? userStatusDTO);
    }
}
