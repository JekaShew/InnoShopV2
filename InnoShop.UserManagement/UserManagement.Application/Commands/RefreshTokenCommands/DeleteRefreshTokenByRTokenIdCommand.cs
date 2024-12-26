using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.RefreshTokenCommands
{
    public class DeleteRefreshTokenByRTokenIdCommand : IRequest<Response>
    {
        public Guid RTokenId { get; set; }
    }
}
