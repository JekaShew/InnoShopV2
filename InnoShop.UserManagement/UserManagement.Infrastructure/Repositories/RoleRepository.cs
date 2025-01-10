using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Mappers;
using UserManagement.Domain.Data.Models;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class RoleRepository : IRole
    {
        private readonly UserManagementDBContext _umDBContext;
        public RoleRepository(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }

        public async Task<Response> AddRole(RoleDTO roleDTO)
        {
            try
            {
                var role = RoleMapper.RoleDTOToRole(roleDTO);

                await _umDBContext.Roles.AddAsync(role!);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Role!");
            }
        }

        public async Task<Response> DeleteRoleById(Guid roleId)
        {
            try
            {
                var role = await _umDBContext.Roles.FindAsync(roleId);
                if (role == null)
                    return new Response(false, "Role not found!");

                _umDBContext.Roles.Remove(role);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Role!");
            }
        }

        public async Task<List<RoleDTO>> TakeAllRoles()
        {
            try
            {
                var roleDTOs = await _umDBContext.Roles
                                .AsNoTracking()
                                .Select(r => RoleMapper.RoleToRoleDTO(r))
                                .ToListAsync();
                return roleDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<RoleDTO> TakeRoleById(Guid roleId)
        {
            try
            {
                var roleDTO = RoleMapper.RoleToRoleDTO(
                await _umDBContext.Roles
                   .AsNoTracking()
                   .FirstOrDefaultAsync(r => r.Id == roleId));
                return roleDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateRole(RoleDTO roleDTO)
        {
            try
            {
                var role = await _umDBContext.Roles.FindAsync(roleDTO.Id);

                if (role == null)
                {
                    return new Response(false, "Role not Found!");
                }

                ApplyPropertiesFromDTOToModel(roleDTO, role);

                _umDBContext.Roles.Update(role);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Role!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(RoleDTO roleDTO, Role role)
        {
            var dtoProperties = roleDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = role.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(role, dtoProperty.GetValue(roleDTO));
                }
            }
        }
    }
}
