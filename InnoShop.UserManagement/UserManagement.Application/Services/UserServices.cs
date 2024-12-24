using Azure;
using InnoShop.CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.Http;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services
{
    public class UserServices(HttpClient httpClient,
            ResiliencePipelineProvider<string> resiliencePipeline,
            IHttpContextAccessor _httpContextAccessor) : IUserServices
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
       
        //public UserServices(
        //    IHttpContextAccessor httpContextAccessor) 
        //{
        //    _httpContextAccessor= httpContextAccessor;
      
        //}
        public Guid? GetCurrentAccountId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }
        public Guid? TakeCurrentUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }
        public async Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId)
        {
            var getProducts = await httpClient.GetAsync($"/api/products/getproductsbyuserid/{userId}");
            if (!getProducts.IsSuccessStatusCode)
                return null;
            var userProducts = await getProducts.Content.ReadFromJsonAsync<List<ProductDTO>>();
                return userProducts;
        }

        public Task<Response> ChangeUserStatusOfUser(Guid userId, Guid userStatusId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> TakeProductsOfCurrentUser()
        {
            var userId = TakeCurrentUserId();
            if(userId is null)
                return null;
            var retryPipline = resiliencePipeline.GetPipeline("retry-pipeline");
            var currentUserProducts = await retryPipline
                        .ExecuteAsync(
                            async token => await TakeProductsDTOListByUserId(userId.Value));

            return currentUserProducts;
        }

        public Task<Response> ChangeUserPasswordBySecretWord(string secretWord)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ChangeUserPasswordByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
