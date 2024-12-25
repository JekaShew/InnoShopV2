using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class ProductService : IProductServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        public ProductService(IHttpContextAccessor httpContextAccessor, IMediator mediator)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }
        public async Task<Response> ChangeProductStatusOfProduct(Guid productId, Guid productStatusId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeFilteredProductDTOList(ProductFilterDTO productFilterDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeProductsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> TakeSearchedProductDTOList(string query)
        {
            throw new NotImplementedException();
        }

        public Guid? GetCurrentAccountId()
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
            var userId = GetCurrentAccountId();
            if (userId != null)
            {
                productDTO.UserId = userId.Value;
                return await _mediator.Send(new AddProductCommand() { ProductDTO = productDTO });
            }

            return new Response(false, "An Error occured while adding Product!");
        }

    }
}
