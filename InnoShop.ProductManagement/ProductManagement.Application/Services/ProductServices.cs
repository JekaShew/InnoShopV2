using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;
using System.Security.Claims;

namespace ProductManagement.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        public ProductServices(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        public async Task<Response> ChangeProductStatusOfProduct(Guid productId, Guid productStatusId)
        {
            return await _mediator.Send(new ChangeProductStatusOfProductCommand()
            {
                ProductId = productId,
                ProductStatusId = productStatusId
            });
        }

        public async Task<List<ProductDTO>> TakeFilteredProductDTOList(ProductFilterDTO productFilterDTO)
        {
            return await _mediator.Send(new TakeFilteredProductDTOListQuery() { ProductFilterDTO = productFilterDTO });
        }

        public async Task<List<ProductDTO>> TakeProductsByUserId(Guid userId)
        {
            return await _mediator.Send(new TakeProductDTOListByUserIdQuery() { UserId = userId });
        }

        public async Task<List<ProductDTO>> TakeSearchedProductDTOList(string query)
        {
            return await _mediator.Send(new TakeSearchedProductDTOListQuery() { QueryString = query });
        }

        public Guid? GetCurrentUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }

        public async Task<Response> CreateProduct(ProductDTO productDTO)
        {
            var userId = GetCurrentUserId();
            if (userId != null)
            {
                productDTO.UserId = userId.Value;
                return await _mediator.Send(new AddProductCommand() { ProductDTO = productDTO });
            }

            return new Response(false, "An Error occured while adding Product!");
        }

        public async Task<Response> ChangeProductStatusesOfProductsByUserId(Guid userId)
        {
            return await _mediator.Send(new ChangeProductStatusesOfProductsByUserIdCommand() { UserId = userId });
        }
    }
}
