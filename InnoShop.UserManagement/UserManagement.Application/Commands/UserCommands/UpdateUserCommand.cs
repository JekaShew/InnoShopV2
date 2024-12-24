﻿using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<Response>
    {
        public UserDTO UserDTO { get; set; }
    }
}