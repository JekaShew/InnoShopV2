﻿using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Interfaces
{
    public interface IUserServices
    {
        public Task<Response> Register(RegistrationInfoDTO registrationInfoDTO);
        public Task<Response> ChangeUserStatusOfUser(Guid userId, Guid userStatusId);
        public Task<Response> ChangeRoleOfUser(Guid userId, Guid roleId);
        //public Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId);
        //public Task<List<ProductDTO>> TakeProductsOfCurrentUser();
        public Task<Response> CheckIsLoginRegistered(string login);
        public Task<Response> ChangePasswordByOldPassword(string oldPassword, string newPassword);
        public Task<Response> ChangeForgottenPasswordBySecretWord(string login, string secretWord, string newPassword);
        public Task<Response> ChangeForgottenPasswordByEmail(string email);
        public Task<Response> CheckLoginPasswordPair(string login, string password);
        public Task<Response> CheckLoginSecretWordPair(string login, string secretWord);

    }
}
