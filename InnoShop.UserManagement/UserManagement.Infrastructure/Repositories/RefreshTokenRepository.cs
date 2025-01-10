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
    public class RefreshTokenRepository : IRefreshToken
    {
        private readonly UserManagementDBContext _umDBContext;
        public RefreshTokenRepository(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }

        public async Task<Response> AddRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var checkUser = await _umDBContext.Users.AsNoTracking().AnyAsync(u => u.Id == refreshTokenDTO.UserId);

                if (!checkUser)
                    return new Response(false, "Can't add Refresh Token! User not Found!");

                await _umDBContext.RefreshTokens.AddAsync(RefreshTokenMapper.RefreshTokenDTOToRefreshToken(refreshTokenDTO));
                await _umDBContext.SaveChangesAsync();
  
                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Refresh Token!");
            }
        }

        public async Task<Response> DeleteRefreshTokenById(Guid rTokenId)
        {
            try
            {
                var refreshToken = await _umDBContext.RefreshTokens.FindAsync(rTokenId);

                if (refreshToken == null)
                    return new Response(false, "Refresh Token Not Found!");

                _umDBContext.RefreshTokens.Remove(refreshToken);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Refresh Token!");
            }
        }

        public async Task<List<RefreshTokenDTO>> TakeAllRefreshTokens()
        {
            try
            {
                var refreshTokenDTOs = await _umDBContext.RefreshTokens
                        .AsNoTracking()
                        .Select(rt => RefreshTokenMapper.RefreshTokenToRefreshTokenDTO(rt))
                        .ToListAsync();
             
                return refreshTokenDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<RefreshTokenDTO> TakeRefreshTokenById(Guid refreshTokenId)
        {
            try
            {
                var refreshTokenDTO = RefreshTokenMapper.RefreshTokenToRefreshTokenDTO(
                await _umDBContext.RefreshTokens
                       .Include(u => u.User)
                       .AsNoTracking()
                       .FirstOrDefaultAsync(r => r.Id == refreshTokenId));

                return refreshTokenDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateRefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var refreshToken = await _umDBContext.RefreshTokens.FindAsync(refreshTokenDTO.Id);

                if (refreshToken == null)
                {
                    return new Response(false, "Refresh Token not Found!");
                }

                ApplyPropertiesFromDTOToModel(refreshTokenDTO, refreshToken);

                _umDBContext.RefreshTokens.Update(refreshToken);
                await _umDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Refresh Token!");
            }
        }

        private void ApplyPropertiesFromDTOToModel(RefreshTokenDTO refreshTokenDTO, RefreshToken refreshToken)
        {
            var dtoProperties = refreshTokenDTO.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var modelProperties = refreshToken.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var dtoProperty in dtoProperties)
            {
                var modelProperty = modelProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (modelProperty != null)
                {
                    modelProperty.SetValue(refreshToken, dtoProperty.GetValue(refreshTokenDTO));
                }
            }
        }
    }
}
