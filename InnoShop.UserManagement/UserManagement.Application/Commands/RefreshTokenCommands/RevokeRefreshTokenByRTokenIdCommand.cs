using InnoShop.CommonLibrary.Response;
using MediatR;

namespace UserManagement.Application.Commands.RefreshTokenCommands
{
    public class RevokeRefreshTokenByRTokenIdCommand : IRequest<Response>
    {
        public Guid RTokenId { get; set; }
    }
}
