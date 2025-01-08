using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using System.Linq.Expressions;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Data.Models;

namespace UserManagement.Application.Interfaces
{
    public interface IUser
    {
        public Task<Response> AddUser(UserDetailedDTO userDTO);
        public Task<List<UserDTO>> TakeAllUsers();
        public Task<UserDTO> TakeUserById(Guid userId);
        //public Task<UserDetailedDTO> TakeUserDetailedById(Guid userId);
        public Task<Response> UpdateUser(UserDTO userDTO);
        public Task<Response> UpdateAuthorizationInfoOfUser(AuthorizationInfoDTO authorizationInfoDTO);
        public Task<Response> DeleteUserById(Guid userId);
        public Task<UserDTO> TakeUserWithPredicate(Expression<Func<User, bool>> predicate);
        public Task<AuthorizationInfoDTO> TakeAuthorizationInfoDTOWithPredicate(Expression<Func<User, bool>> predicate);
    }
}
