﻿using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.UserCommands
{
    public class ChangeUserPasswordCommand : IRequest<Response>
    {
        public string NewPassword { get; set; }
    }
}
