﻿using MediatR;
using ProductManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.ProductQueries
{
    public class TakeProductDTOByIdQuery : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
}