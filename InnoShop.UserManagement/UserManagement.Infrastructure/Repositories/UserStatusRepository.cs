﻿using InnoShop.CommonLibrary.Logs;
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
    public class UserStatusRepository : IUserStatus
    {
        private readonly UserManagementDBContext _umDBContext;
        public UserStatusRepository(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }

        public async Task<Response> AddUserStatus(UserStatusDTO userStatusDTO)
        {
            try
            {
                var userStatus = UserStatusMapper.UserStatusDTOToUserStatus(userStatusDTO);

                await _umDBContext.UserStatuses.AddAsync(userStatus!);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new User Status!");
            }
        }

        public async Task<Response> DeleteUserStatusById(Guid userStatusId)
        {
            try
            {
                var userStatus = await _umDBContext.UserStatuses.FindAsync(userStatusId);
                if (userStatus == null)
                    return new Response(false, "User Status not found!");

                _umDBContext.UserStatuses.Remove(userStatus);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting User Status!");
            }
        }

        public async Task<List<UserStatusDTO>> TakeAllUserStatuses()
        {
            try
            {
                var userStatusDTOs = await _umDBContext.UserStatuses
                                .AsNoTracking()
                                .Select(us => UserStatusMapper.UserStatusToUserStatusDTO(us))
                                .ToListAsync();
                return userStatusDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<UserStatusDTO> TakeUserStatusById(Guid userStatusId)
        {
            try
            {
                var userStatusDTO = UserStatusMapper.UserStatusToUserStatusDTO(
                await _umDBContext.UserStatuses
                   .AsNoTracking()
                   .FirstOrDefaultAsync(us => us.Id == userStatusId));
                return userStatusDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateUserStatus(UserStatusDTO userStatusDTO)
        {
            try
            {
                var userStatus = await _umDBContext.UserStatuses.FindAsync(userStatusDTO.Id);

                if (userStatus == null)
                {
                    return new Response(false, "User Status not Found!");
                }

                ApplyPropertiesFromDTOToModel(userStatusDTO, userStatus);

                _umDBContext.UserStatuses.Update(userStatus);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating User Status!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(UserStatusDTO userStatusDTO, UserStatus userStatus)
        {
            var dtoProperties = userStatusDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = userStatus.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(userStatus, dtoProperty.GetValue(userStatusDTO));
                }
            }
        }
    }
}
