using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeProductDTOListByUserIdQuery : IRequest<List<ProductDTO>>
    {
        public Guid UserId { get; set; }
    }
}
