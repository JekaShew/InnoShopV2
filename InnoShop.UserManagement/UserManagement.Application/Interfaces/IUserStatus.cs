using InnoShop.CommonLibrary.Response;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Interfaces
{
    public interface IUserStatus
    {
        public Task<Response> AddUserStatus(UserStatusDTO userStatusDTO);
        public Task<List<UserStatusDTO>> TakeAllUserStatuses();
        public Task<UserStatusDTO> TakeUserStatusById(Guid userStatusId);
        public Task<Response> UpdateUserStatus(UserStatusDTO userStatusDTO);
        public Task<Response> DeleteUserStatusById(Guid userStatusId);
    }
}
