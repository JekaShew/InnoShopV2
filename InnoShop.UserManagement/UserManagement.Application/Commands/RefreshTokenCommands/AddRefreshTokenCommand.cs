using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.RefreshTokenCommands
{
    public class AddRefreshTokenCommand : IRequest<Response>
    {
        public RefreshTokenDTO? RefreshTokenDTO { get; set; }
    }
}
