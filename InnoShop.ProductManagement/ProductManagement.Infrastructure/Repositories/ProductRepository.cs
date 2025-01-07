using Azure.Core;
using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Logs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Domain.Data.Models;
using ProductManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly ProductManagementDBContext _pmDBContext;

        public ProductRepository(ProductManagementDBContext pmDBContext)
        {
            _pmDBContext = pmDBContext;
        }

        public async Task<Response> AddProduct(ProductDTO productDTO)
        {
            try
            {
                var product = ProductMapper.ProductDTOToProduct(productDTO);

                await _pmDBContext.Products.AddAsync(product);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Added!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while adding new Product!");
            }
        }

        public async Task<Response> DeleteProductById(Guid productId)
        {
            try
            {
                var product = await _pmDBContext.Products.FindAsync(productId);
                
                if (product == null)
                    return new Response(false, "Product not found!");

                _pmDBContext.Products.Remove(product);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Deleted!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while deleting Product!");
            }
        }

        public async Task<List<ProductDTO>> TakeAllProducts()
        {
            try
            {
                var productDTOs = await _pmDBContext.Products
                    .Include(ps => ps.ProductStatus)
                    .Include(sc => sc.SubCategory)
                        .ThenInclude(c => c.Category)
                    .AsNoTracking()
                    .Select(p => ProductMapper.ProductToProductDTO(p))
                    .ToListAsync();

                return productDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<List<ProductDTO>> TakeProductsWithPredicate(
                Expression<Func<Product, bool>> predicate)
        {
            try
            {
                var productDTOs = await _pmDBContext.Products
                    .Include(ps => ps.ProductStatus)
                    .Include(sc => sc.SubCategory)
                        .ThenInclude(c => c.Category)
                    .AsNoTracking()
                    .Where(predicate)
                    .Select(p => ProductMapper.ProductToProductDTO(p))
                    .ToListAsync();

                return productDTOs;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<ProductDTO> TakeProductById(Guid productId)
        {
            try
            {
                var productDTO = ProductMapper.ProductToProductDTO(
                    await _pmDBContext.Products
                       .Include(ps => ps.ProductStatus)
                       .Include(sc => sc.SubCategory)
                           .ThenInclude(c => c.Category)
                       .AsNoTracking()
                       .FirstOrDefaultAsync(p => p.Id == productId));

                return productDTO;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return null;
            }
        }

        public async Task<Response> UpdateProduct(ProductDTO productDTO)
        {
            try
            {
                var product = ProductMapper.ProductDTOToProduct(productDTO);

                _pmDBContext.Products.Update(product);
                await _pmDBContext.SaveChangesAsync();

                return new Response(true, "Successfully Updated!");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error while updating Product!");
            }
        }
    }
}
