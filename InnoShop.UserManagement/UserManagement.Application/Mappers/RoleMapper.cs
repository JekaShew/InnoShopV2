using Riok.Mapperly.Abstractions;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Data.Models;

namespace UserManagement.Application.Mappers
{
    [Mapper]
    public static partial class RoleMapper
    {
        public static partial RoleDTO? RoleToRoleDTO(Role? role);
        public static partial Role? RoleDTOToRole(RoleDTO? roleDTO);
    }
}
