using InnoShop.CommonLibrary.CommonDTOs;
using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Polly.Registry;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Application.Services
{

    // SOLUTION NOT FOUND!!!!
    public class UserServices(/*HttpClient httpClient,*/
    //        ResiliencePipelineProvider<string> resiliencePipeline
            /*IHttpContextAccessor _httpContextAccessor, IMediator mediator*/) : IUserServices
    {
        private readonly HttpClient _httpClient;
        private readonly ResiliencePipelineProvider<string> _resiliencePipeline;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly IAuthorizationServices _authenticationServices;

        public UserServices(
            HttpClient httpClient,
            ResiliencePipelineProvider<string> resiliencePipeline,
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator,
            IAuthorizationServices authenticationServices)
        {
            _httpClient = httpClient;
            _resiliencePipeline = resiliencePipeline;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _authenticationServices = authenticationServices;
        }

        public async Task<Response> Register(RegistrationInfoDTO registrationInfoDTO)
        {
            //var checkIsLoginRegistered = await _mediator.Send(new IsUserLoginRegisteredQuery()
            //{
            //    EnteredLogin = registrationInfoDTO.Login,
            //});

            //if(checkIsLoginRegistered.Flag == true)
            //    return checkIsLoginRegistered;
            //else
            //{
                var securityStamp = await GetHash(registrationInfoDTO.SecretWord);
                var secretWordHash = await GetHash($"{registrationInfoDTO.SecretWord}{securityStamp}");
                var passwordHash = await GetHash($"{registrationInfoDTO.Password}{securityStamp}");

                return await _mediator.Send(new AddUserCommand()
                {
                    RegistrationInfoDTO = registrationInfoDTO,
                    SecurityStamp = securityStamp,
                    PasswordHash = passwordHash,
                    SecretWordHash = secretWordHash
                });
            //}
        }

        //public async Task<Response> TryLogin(LoginInfoDTO loginInfoDTO)
        //{
        //    var checkIsLoginRegistered = await CheckIsLoginRegistered(loginInfoDTO.Login);

        //    if (checkIsLoginRegistered.Flag == false)
        //        return checkIsLoginRegistered;
        //    else
        //        return await CheckLoginPasswordPair(loginInfoDTO.Login, loginInfoDTO.Password);
        //}

        //To Controller ???????"
        public Guid? TakeCurrentUserId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }

        // to Controller
        public async Task<List<ProductDTO>> TakeProductsDTOListByUserId(Guid userId)
        {
            var getProducts = await _httpClient.GetAsync($"/api/products/takeproductsbyuserid/{userId}");
            if (!getProducts.IsSuccessStatusCode)
                return null;
            var userProducts = await getProducts.Content.ReadFromJsonAsync<List<ProductDTO>>();
                return userProducts;
        }

        // to Controller 
        public async Task<Response> ChangeUserStatusOfUser(Guid userId, Guid userStatusId)
        {
            return await _mediator.Send(new ChangeUserStatusOfUserCommand() { UserId = userId, UserStatusId = userStatusId });
        }

        // to Controller
        public async Task<List<ProductDTO>> TakeProductsOfCurrentUser()
        {
            var userId = TakeCurrentUserId();
            if(userId is null)
                return null;
            var retryPipline = _resiliencePipeline.GetPipeline("retry-pipeline");
            var currentUserProducts = await retryPipline
                        .ExecuteAsync(
                            async token => await TakeProductsDTOListByUserId(userId.Value));

            return currentUserProducts;
        }        

        private async Task<string> GetHash(string stringToHash)
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

        // To Controller
        public async Task<Response> CheckIsLoginRegistered(string login)
        {
            //Mediator
            return await _mediator.Send(new IsUserLoginRegisteredQuery() { EnteredLogin = login });
        }

        public async Task<Response> CheckLoginPasswordPair(string login, string password)
        {
                //Mediator
                var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });
                var enteredPasswordHash = await GetHash($"{password}{authorizationInfoDTO.SecurityStamp}");
                //Mediator
                return await _mediator.Send(new CheckLoginPasswordPairQuery() { Login = login, PasswordHash = enteredPasswordHash });
        }

        public async Task<Response> CheckLoginSecretWordPair(string login, string secretWord)
        {
            //Mediator
            var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });
            var enteredSecretWordHash = await GetHash($"{secretWord}{authorizationInfoDTO.SecurityStamp}");

            //Mediator
            return await _mediator.Send(new CheckLoginSecretWordPairQuery() { Login = login, SecretWordHash = enteredSecretWordHash });
        }

        //to Controller
        public async Task<Response> ChangeForgottenPasswordBySecretWord(string login, string secretWord, string newPassword)
        {
            var checkIsLoginRegistered = await CheckIsLoginRegistered(login);
            if (checkIsLoginRegistered.Flag == true)
            {
                var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByLoginQuery() { EnteredLogin = login });

                var checkLoginSecretWordPair = await CheckLoginSecretWordPair(login, secretWord);

                if (checkLoginSecretWordPair.Flag == true)
                {
                    var newPasswordHash = await GetHash($"{newPassword}{authorizationInfoDTO.SecurityStamp}");
                    return await ChangePassword(authorizationInfoDTO.Id, newPasswordHash);
                }
                else
                    return checkLoginSecretWordPair;
            }
            else
                return checkIsLoginRegistered;
        }

        //to Controller
        public Task<Response> ChangeForgottenPasswordByEmail(string email)
        {
            throw new NotImplementedException();
        }

        //to Controller
        public async Task<Response> ChangePasswordByOldPassword(string oldPassword, string newPassword)
        {
            var userId = TakeCurrentUserId();
            var authorizationInfoDTO = await _mediator.Send(new TakeAuthorizationInfoDTOByUserIdQuery() { UserId = userId.Value });

            var checkLoginPasswordPair = await CheckLoginPasswordPair(authorizationInfoDTO.Login, oldPassword);

            if (checkLoginPasswordPair.Flag == true)
            {
                var newPasswordHash = await GetHash($"{newPassword}{authorizationInfoDTO.SecurityStamp}");
                return await ChangePassword(authorizationInfoDTO.Id, newPasswordHash);
            }
            else
                return checkLoginPasswordPair;
        }

        private async Task<Response> ChangePassword(Guid userId, string newPasswordHash)
        {
            return await _mediator.Send(new ChangePasswordCommand() { UserId = userId, NewPasswordHash = newPasswordHash });
        }

        
    }
}
