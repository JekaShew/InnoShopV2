using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly IUserExternalServices _userExternalServices;
 
        public UserServices(IHttpContextAccessor httpContextAccessor,
                IMediator mediator,
                IUserExternalServices userExternalServices)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _userExternalServices = userExternalServices;
        }

        public async Task<Response> Register(RegistrationInfoDTO registrationInfoDTO)
        {
            var securityStamp = await GetHashString(registrationInfoDTO.SecretWord);
            var secretWordHash = await GetHashString($"{registrationInfoDTO.SecretWord}{securityStamp}");
            var passwordHash = await GetHashString($"{registrationInfoDTO.Password}{securityStamp}");

            return await _mediator.Send(new AddUserCommand()
            {
                RegistrationInfoDTO = registrationInfoDTO,
                SecurityStamp = securityStamp,
                PasswordHash = passwordHash,
                SecretWordHash = secretWordHash
            });
        }

        public Guid? TakeCurrentUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }  

        private async Task<string> GetHashString(string stringToHash)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{stringToHash}");
                var ms = new MemoryStream(inputBytes);
                var hashBytes = await md5.ComputeHashAsync(ms);
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return passwordHash;
            }
        }

        public async Task<Response> CheckIsLoginRegistered(string login)
        {
            return await _mediator.Send(new IsLoginRegisteredQuery() { EnteredLogin = login });
        }

        public async Task<Response> CheckLoginPasswordPair(string login, string password)
        {
            var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });
            var enteredPasswordHash = await GetHashString($"{password}{authorizationInfoDTO.SecurityStamp}");

            return await _mediator.Send(new CheckLoginPasswordPairQuery() { Login = login, PasswordHash = enteredPasswordHash });
        }

        public async Task<Response> CheckLoginSecretWordPair(string login, string secretWord)
        {
            var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });
            var enteredSecretWordHash = await GetHashString($"{secretWord}{authorizationInfoDTO.SecurityStamp}");

            return await _mediator.Send(new CheckLoginSecretWordPairQuery() { Login = login, SecretWordHash = enteredSecretWordHash });
        }

        public async Task<Response> ChangeForgottenPasswordBySecretWord(string login, string secretWord, string newPassword)
        {
            var checkIsLoginRegistered = await CheckIsLoginRegistered(login);
            if (checkIsLoginRegistered.Flag == true)
            {
                var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });

                var checkLoginSecretWordPair = await CheckLoginSecretWordPair(login, secretWord);

                if (checkLoginSecretWordPair.Flag == true)
                {
                    var newPasswordHash = await GetHashString($"{newPassword}{authorizationInfoDTO.SecurityStamp}");
                    return await ChangePassword(authorizationInfoDTO.Id, newPasswordHash);
                }
                else
                    return checkLoginSecretWordPair;
            }
            else
                return checkIsLoginRegistered;
        }


        public Task<Response> ChangeForgottenPasswordByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> ChangePasswordByOldPassword(string oldPassword, string newPassword)
        {
            var userId = TakeCurrentUserId();
            var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByUserIdQuery() { UserId = userId.Value });

            var checkLoginPasswordPair = await CheckLoginPasswordPair(authorizationInfoDTO.Login, oldPassword);

            if (checkLoginPasswordPair.Flag == true)
            {
                var newPasswordHash = await GetHashString($"{newPassword}{authorizationInfoDTO.SecurityStamp}");
                return await ChangePassword(authorizationInfoDTO.Id, newPasswordHash);
            }
            else
                return checkLoginPasswordPair;
        }

        private async Task<Response> ChangePassword(Guid userId, string newPasswordHash)
        {
            return await _mediator.Send(new ChangePasswordCommand() { UserId = userId, NewPasswordHash = newPasswordHash });
        }

        public async Task<Response> ChangeRoleOfUser(Guid userId, Guid roleId)
        {
            return await _mediator.Send(new ChangeRoleOfUserCommand() { UserId = userId, RoleId = roleId });
        }

        public async Task<Response> ChangeUserStatusOfUser(Guid userId, Guid userStatusId)
        {
            var changeProducts = await _userExternalServices.ChangeUserProductStatusesOfUserById(userId);
            if (changeProducts.Flag == false)
                return new Response(false, "Somthing goes Wrong while changing User products statuses!");

            return await _mediator.Send(new ChangeUserStatusOfUserCommand() { UserId = userId, UserStatusId = userStatusId });
        }
    }
}
