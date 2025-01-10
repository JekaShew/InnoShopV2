using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using Riok.Mapperly.Abstractions;

namespace ProductManagement.Application.Mappers
{
    [Mapper]
    public static partial class ProductStatusMapper
    {
        public static partial ProductStatusDTO? ProductStatusToProductStatusDTO(ProductStatus? productStatus);
        public static partial ProductStatus? ProductStatusDTOToProductStatus(ProductStatusDTO? productStatusDTO);
    }
}
