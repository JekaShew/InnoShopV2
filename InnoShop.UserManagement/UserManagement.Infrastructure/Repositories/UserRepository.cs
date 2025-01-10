using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Mappers;
using UserManagement.Domain.Data.Models;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUser
    {
        private readonly UserManagementDBContext _umDBContext;
        public UserRepository(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }

        public async Task<Response> AddUser(UserDetailedDTO userDetailedDTO)
        {
            try
            {
                var roleId = await _umDBContext.Roles
                    .AsNoTracking()
                    .Where(r => r.Title == "User")
                    .Select(r => r.Id)
                    .FirstOrDefaultAsync();

                var userStatusId = await _umDBContext.UserStatuses
                        .AsNoTracking()
                        .Where(us => us.Title == "Activated")
                        .Select(r => r.Id)
                        .FirstOrDefaultAsync();

                if (roleId == null && roleId == Guid.Empty)
                    return new Response(false, "There is no Default Role named User in DB!");

                userDetailedDTO.RoleId = roleId;

                if (userStatusId == null && userStatusId == Guid.Empty)
                    return new Response(false, "There is no Default User Status named Acitvated in DB!");
                
                userDetailedDTO.UserStatusId = userStatusId;

                var newUser = UserMapper.UserDetailedDTOToUser(userDetailedDTO);

                await _umDBContext.Users.AddAsync(newUser);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new User!");
            }
        }

        public async Task<Response> DeleteUserById(Guid userId)
        {
            try
            {
                var user = await _umDBContext.Users.FindAsync(userId);

                if (user == null)
                    return new Response(false, "User not found!");

                _umDBContext.Users.Remove(user);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting User!");
            }
        }

        public async Task<List<UserDTO>> TakeAllUsers()
        {
            try
            {
                var userDTOs = await _umDBContext.Users
                    .AsNoTracking()
                    .Select(u => UserMapper.UserToUserDTO(u))
                    .ToListAsync();

                return userDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<UserDTO> TakeUserById(Guid userId)
        {
            try
            {
                var user = await _umDBContext.Users
                        .Include(r => r.Role)
                        .Include(us => us.UserStatus)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id.Equals(userId));

                var userDTO = UserMapper.UserToUserDTO(user);

                return userDTO;

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateUser(UserDTO userDTO)
        {
            try
            {
                var user = await _umDBContext.Users.FindAsync(userDTO.Id);

                if (user == null)
                {
                    return new Response(false, "User not Found!");
                }

                ApplyPropertiesFromDTOToModel(userDTO, user);

                _umDBContext.Users.Update(user);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating User!");
            }
        }

        public async Task<UserDTO> TakeUserWithPredicate(Expression<Func<User, bool>> predicate)
        {
            try
            {
                var user = await _umDBContext.Users
                        .Where(predicate)
                        .FirstOrDefaultAsync();
                if (user != null)
                {
                    var userDTO = UserMapper.UserToUserDTO(user);

                    return userDTO;
                }
                else
                    return null;
            }
            catch(Exception ex) 
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<AuthorizationInfoDTO> TakeAuthorizationInfoDTOWithPredicate(Expression<Func<User, bool>> predicate)
        {
            try
            {
                var user = await _umDBContext.Users
                        .Where(predicate)
                        .FirstOrDefaultAsync();

                if (user == null)
                    return null;
                else
                {
                    var authorizationInfoDTO = UserMapper.UserToAuthorizationInfoDTO(user);

                    return authorizationInfoDTO;
                }
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateAuthorizationInfoOfUser(AuthorizationInfoDTO authorizationInfoDTO)
        {
            try
            {
                var user = await _umDBContext.Users
                    .Where(u => u.Id == authorizationInfoDTO.Id)
                    .FirstOrDefaultAsync();
                
                if (user == null)
                    return new Response(false, "User not found!");
                else
                {
                    user.PasswordHash = authorizationInfoDTO.PasswordHash;
                    user.SecretWordHash = authorizationInfoDTO.SecretWordHash;
                    user.SecurityStamp = authorizationInfoDTO.SecurityStamp;

                    await _umDBContext.SaveChangesAsync();
                    return new Response(true, "Successfylly Updated!");
                }
            }
            catch( Exception ex ) 
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating User's Authorization Info!");
            }
        }
        private void ApplyPropertiesFromDTOToModel(UserDTO userDTO, User user)
        {
            var dtoProperties = userDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = user.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(user, dtoProperty.GetValue(userDTO));
                }
            }
        }
    }
}
