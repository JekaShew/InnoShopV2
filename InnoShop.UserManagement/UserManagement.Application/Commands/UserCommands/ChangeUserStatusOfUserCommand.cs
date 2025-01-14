﻿using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.UserCommands
{
    public class ChangeUserStatusOfUserCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public Guid UserStatusId { get; set; }
    }
}
