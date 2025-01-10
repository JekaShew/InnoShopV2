using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeUserDTOByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}
