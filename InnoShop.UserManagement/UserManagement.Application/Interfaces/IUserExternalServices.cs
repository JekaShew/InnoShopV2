using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;

namespace UserManagement.Application.Interfaces
{
    public interface IUserExternalServices
    {
        public Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId);
        public Task<Response> ChangeUserProductStatusesOfUserById(Guid userId);
    }
}
