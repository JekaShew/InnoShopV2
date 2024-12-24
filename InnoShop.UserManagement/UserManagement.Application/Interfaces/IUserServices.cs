using Azure;
using InnoShop.CommonLibrary.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Interfaces
{
    public interface IUserServices
    {
        public Task<Response> ChangeUserStatusOfUser(Guid userId, Guid userStatusId);
        public Task<List<ProductDTO>> TakeProductsOfCurrentUser();
        public Task<Response> ChangeUserPasswordBySecretWord(string secretWord);
        public Task<Response> ChangeUserPasswordByEmail(string email);
    }
}
