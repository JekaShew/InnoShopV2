using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeFilteredProductDTOListQuery : IRequest<List<ProductDTO>>
    {
        public ProductFilterDTO ProductFilterDTO { get; set; }
    }
}
