using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<Response>
    {
        public RegistrationInfoDTO RegistrationInfoDTO { get; set; }
        public string PasswordHash { get; set; }
        public string SecretWordHash { get; set; }
        public string SecurityStamp { get; set; }

    }
}
