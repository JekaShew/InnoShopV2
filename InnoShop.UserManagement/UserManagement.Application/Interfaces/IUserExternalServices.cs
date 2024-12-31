using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Interfaces
{
    public interface IUserExternalServices
    {
        public Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId);
        public Task<Response> ChangeUserProductStatusesOfUserById(Guid userId);
        //public Task<List<ProductDTO>> TakeProductsOfCurrentUser();
    }
}
