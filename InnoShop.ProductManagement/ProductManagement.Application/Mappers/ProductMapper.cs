using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Mappers
{
    [Mapper]
    public static partial class ProductMapper
    {
        [MapProperty([nameof(Product.ProductStatus), nameof(Product.ProductStatus.Id)],
       [nameof(ProductDTO.ProductStatus), nameof(ProductDTO.ProductStatus.Id)])]
        [MapProperty([nameof(Product.ProductStatus), nameof(Product.ProductStatus.Title)],
       [nameof(ProductDTO.ProductStatus), nameof(ProductDTO.ProductStatus.Text)])]

        [MapProperty([nameof(Product.SubCategory), nameof(Product.SubCategory.Id)],
       [nameof(ProductDTO.SubCategory), nameof(ProductDTO.SubCategory.Id)])]
        [MapProperty([nameof(Product.SubCategory), nameof(Product.SubCategory.Title)],
       [nameof(ProductDTO.SubCategory), nameof(ProductDTO.SubCategory.Text)])]
        public static partial ProductDTO? ProductToProductDTO(Product? product);

        public static partial Product? ProductDTOToProduct(ProductDTO? productDTO);
    }
}
