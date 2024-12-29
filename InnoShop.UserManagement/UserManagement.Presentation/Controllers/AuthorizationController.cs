using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;
        private readonly IAuthorizationServices _authorizationServices;
        public AuthorizationonController(IMediator mediator, IUserServices userServices, IAuthorizationServices authorizationServices)
        {
            _mediator = mediator;
            _userServices = userServices;
            _authorizationServices = authorizationServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInfoDTO loginInfoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // rework as simple Mediator calls?????
                    var checkIsLoginRegistered = await _userServices.CheckIsLoginRegistered(loginInfoDTO.Login);

                    if (checkIsLoginRegistered.Flag == false)
                        return BadRequest(checkIsLoginRegistered);
                    else
                    {
                        var checkLoginPasswordPair = await _userServices.CheckLoginPasswordPair(loginInfoDTO.Login, loginInfoDTO.Password);
                        if (checkLoginPasswordPair.Flag == true)
                        {
                            var userId = await _mediator.Send(new TakeUserIdByLoginQuery() { Login = loginInfoDTO.Login });
                            if (userId == null)
                                return BadRequest("The Error occured while Signing In!");

                            var tokens = await GenerateTokenPair(userId);
                            if (tokens.Item1 == null || tokens.Item2 == null)
                                return BadRequest("Generating tokens Failed!");
                            return Ok(new { AccessToken = tokens.Item1, RefreshToken = tokens.Item2 });
                        }
                        else
                            return Unauthorized(checkLoginPasswordPair);
                    }

                }
                else return BadRequest("Invalid Data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationInfoDTO registrationInfoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkIsLoginRegistered = await _mediator.Send(new IsUserLoginRegisteredQuery()
                    {
                        EnteredLogin = registrationInfoDTO.Login,
                    });

                    if (checkIsLoginRegistered.Flag == true)
                        return BadRequest(checkIsLoginRegistered);
                    else
                    {
                        var tryRegister = await _userServices.Register(registrationInfoDTO);
                        if (tryRegister.Flag == false)
                            return BadRequest(tryRegister.Message);
                        else
                            return Ok(tryRegister.Message);
                    }
                }
                else return BadRequest("Invalid Data.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] Guid rTokenId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deleteRefreshToken = await _authorizationServices.DeleteRefreshTokenByRTokenId(rTokenId);
                    if (deleteRefreshToken.Flag == true)
                        return Ok(deleteRefreshToken.Message);
                    else
                        return BadRequest(deleteRefreshToken.Message);
                }
                else return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("/refresh")]
        public async Task<IActionResult> Refresh([FromBody] Guid rTokenId)
        {
            var isRefreshTokenCorrect = await _authorizationServices.IsRefreshTokenCorrectByRTokenId(rTokenId);
            if (isRefreshTokenCorrect.Flag == true)
            {
                var userId = await _authorizationServices.TakeUserIdByRTokenId(rTokenId);
                if (userId != null)
                {
                    var tokens = await GenerateTokenPair(userId);

                    var deleteRefreshToken = await _authorizationServices.DeleteRefreshTokenByRTokenId(rTokenId);

                    if (deleteRefreshToken.Flag == true)
                        return Ok(new { AccessToken = tokens.Item1, RefreshToken = tokens.Item2 });
                    else
                        return BadRequest(deleteRefreshToken.Message);
                }
                else
                    return BadRequest("User not found!");
            }
            return BadRequest(isRefreshTokenCorrect.Message);
        }

        [HttpPatch("/revoke/{id}")]
        public async Task<IActionResult> RevokeTokenById(Guid rTokenId)
        {
            var isRefreshTokenCorrect = await _authorizationServices.IsRefreshTokenCorrectByRTokenId(rTokenId);
            if (isRefreshTokenCorrect.Flag == true)
            {
                var revokeToken = await _authorizationServices.RevokeTokenByRTokenId(rTokenId);
                if (revokeToken.Flag == true)
                    return Ok(revokeToken.Message);
                else
                    return BadRequest(revokeToken.Message);
            }
            else
                return BadRequest(isRefreshTokenCorrect.Message);
        }

        private async Task<(string, string)> GenerateTokenPair(Guid userId)
        {
            var jwt = await _authorizationServices.GenerateJwtTokenStringByUserId(userId);
            var rt = await _authorizationServices.GenerateRefreshTokenByUserId(userId);
            return (jwt, rt);
        }
    }
}
