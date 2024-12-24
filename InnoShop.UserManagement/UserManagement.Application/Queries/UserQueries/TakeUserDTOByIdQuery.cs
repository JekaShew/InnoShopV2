﻿using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeUserDTOByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}