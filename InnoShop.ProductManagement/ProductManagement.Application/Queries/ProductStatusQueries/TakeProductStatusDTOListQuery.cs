using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.ProductStatusQueries
{
    public class TakeProductStatusDTOListQuery : IRequest<List<ProductStatusDTO>>
    {
    }
}
