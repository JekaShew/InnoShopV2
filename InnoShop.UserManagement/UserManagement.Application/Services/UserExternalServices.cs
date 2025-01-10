using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using Microsoft.AspNetCore.Http;
using Polly.Registry;
using System.Net.Http.Json;
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

            var changeUserProductsStatus = await retryPipline
                        .ExecuteAsync(
                                    async token => await httpClient
                                                    .GetAsync($"/api/products/changeproductstatusesofproductsbyuserid/{userId}"));
            if (!changeUserProductsStatus.IsSuccessStatusCode)
                return new Response(false, "Error occured while changing User's Porduct Statuses!");

            return new Response(true, "Successfully changed!");
        }
    }
}
