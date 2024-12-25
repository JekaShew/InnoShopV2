using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.UserCommands
{
    public class ChangePasswordCommand : IRequest<Response>
    {
        public Guid UserId { get; set; }
        public string NewPasswordHash { get; set; }
    }
}
