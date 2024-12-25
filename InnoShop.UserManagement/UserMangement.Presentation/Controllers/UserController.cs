using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Domain.Data.Models;

namespace UserMangement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;
        public UserController(IMediator mediator, IUserServices userServices)
        {
            _mediator = mediator;   
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> TakeUsers()
        {
            try
            {
                var userDTOs = await _mediator.Send(new TakeUserDTOListQuery());

                if (!userDTOs.Any())
                    return NotFound("No User found!");
                else
                    return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> TakeUserById(Guid userId)
        {
            try
            {
                var userDTO = await _mediator.Send(new TakeUserDTOByIdQuery() { Id = userId });
                if (userDTO is null)
                    return NotFound("No User found!");
                else
                    return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginInfoDTO loginInfoDTO)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            await _userServices.Login(loginInfoDTO);
        //            await _authenticationServices.GenerateJwtTokenStringByUserId(userId);
        //            return Ok(await _mediator.Send(new AddUserCommand() { UserDTO = userDTO }));
        //        }
        //        else return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddUser([FromBody] UserDTO userDTO)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            return Ok(await _mediator.Send(new AddUserCommand() { UserDTO = userDTO }));
        //        }
        //        else return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(Guid userId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteUserByIdCommand() { Id = userId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateUserCommand() { UserDTO = userDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
