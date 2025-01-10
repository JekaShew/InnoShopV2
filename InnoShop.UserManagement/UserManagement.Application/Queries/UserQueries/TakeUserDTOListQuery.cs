using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;

namespace UserManagement.Application.Queries.UserQueries
{
    public class TakeUserDTOListQuery : IRequest<List<UserDTO>>
    {
    }
}
