using InnoShop.CommonLibrary.Response;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Interfaces
{
    public interface IRole
    {
        public Task<Response> AddRole(RoleDTO roleDTO);
        public Task<List<RoleDTO>> TakeAllRoles();
        public Task<RoleDTO> TakeRoleById(Guid roleId);
        public Task<Response> UpdateRole(RoleDTO roleDTO);
        public Task<Response> DeleteRoleById(Guid roleId);
    }
}
