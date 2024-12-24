using InnoShop.CommonLibrary.Response;
using MediatR;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Mappers;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, Response>
    {
        private readonly UserManagementDBContext _umDBContext;
        public AddUserHandler(UserManagementDBContext umDBContext)
        {
            _umDBContext = umDBContext;
        }
        public async Task<Response> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            request.UserDTO.Id = Guid.NewGuid();
            await _umDBContext.Users.AddAsync(UserMapper.UserDTOToUser(request.UserDTO));
            await _umDBContext.SaveChangesAsync();

            return new Response(true, "Successfully Added!");
        }
    }
}
