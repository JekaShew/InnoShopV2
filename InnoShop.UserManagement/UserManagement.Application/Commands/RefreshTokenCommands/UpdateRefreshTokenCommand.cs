using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.RefreshTokenCommands
{
    public class UpdateRefreshTokenCommand : IRequest<Response>
    {
        public RefreshTokenDTO RefreshTokenDTO { get; set; }
    }
}
