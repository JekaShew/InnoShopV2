using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services
{
    public class UserExternalServices(
        HttpClient httpClient,
        ResiliencePipelineProvider<string> resiliencePipeline) : IUserExternalServices
    {
        public async Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId)
        {
            var getProducts = await httpClient.GetAsync($"/api/products/takeproductsbyuserid/{userId}");
            if (!getProducts.IsSuccessStatusCode)
                return null;
            var userProducts = await getProducts.Content.ReadFromJsonAsync<List<ProductDTO>>();
            return userProducts;
        }

        public async Task<Response> ChangeUserProductStatusesOfUserById(Guid userId)
        {
            var retryPipline = resiliencePipeline.GetPipeline("retry-pipeline");
            //var changeUserProductsStatus = await httpClient
            //                                        .GetAsync($"/api/products/changeproductstatusesofproductsbyuserid/{userId}");
            var changeUserProductsStatus = await retryPipline
                        .ExecuteAsync(
                                    async token => await httpClient
                                                    .GetAsync($"/api/products/changeproductstatusesofproductsbyuserid/{userId}"));
            if (!changeUserProductsStatus.IsSuccessStatusCode)
                return new Response(false, "Error occured while changing User's Porduct Statuses!");

            return new Response(true, "Successfully changed!");
        }

        //public async Task<List<ProductDTO>> TakeProductsOfCurrentUser()
        //{
        //    var userId = TakeCurrentUserId();
        //    if (userId is null)
        //        return null;
        //    var retryPipline = resiliencePipeline.GetPipeline("retry-pipeline");
        //    var currentUserProducts = await retryPipline
        //                .ExecuteAsync(
        //                    async token => await TakeProductsDTOListByUserId(userId.Value));

        //    //var currentUserProducts = await TakeProductsDTOListByUserId(userId.Value);
        //    return currentUserProducts;
        //}
    }
}
