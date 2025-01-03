﻿using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Mappers;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Response>
    {
        private readonly ProductManagementDBContext _pmDBContext;
        public AddProductHandler(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }
        public async Task<Response> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            request.ProductDTO.Id = Guid.NewGuid();
            await _pmDBContext.Products.AddAsync(ProductMapper.ProductDTOToProduct(request.ProductDTO));
            await _pmDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
