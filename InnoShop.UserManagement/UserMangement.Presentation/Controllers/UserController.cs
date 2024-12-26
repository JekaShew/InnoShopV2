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
        // unnecessary
        //[HttpPost]
        //public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            return Ok(await _mediator.Send(new AddProductCommand() { ProductDTO = productDTO }));
        //        }
        //        else return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPatch("/changeuserstatusofuser")]
        public async Task<IActionResult> ChangeUserStatusOfUser([FromBody] Guid userId, Guid userStatusId)
        {
            try
            {
                var changeUserStatusOfUser = await _userServices.ChangeUserStatusOfUser(userId, userStatusId);
                if (changeUserStatusOfUser.Flag == true)
                    return Ok(changeUserStatusOfUser.Message);
                else
                    return BadRequest(changeUserStatusOfUser.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPatch("/changeroleofuser")]
        public async Task<IActionResult> ChangeRoleOfUser([FromBody] Guid userId, Guid roleId)
        {
            try
            {
                var changeRoleOfUser = await _userServices.ChangeRoleOfUser(userId,roleId);
                if (changeRoleOfUser.Flag == true)
                    return Ok(changeRoleOfUser.Message);
                else
                    return BadRequest(changeRoleOfUser.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("/takeproductsdtolistbyuserid/{userId}")]
        public async Task<IActionResult> TakeProductsDTOListByUserId(Guid userId)
        {
            try
            {
                var productsDTOs = await _userServices.TakeProductsDTOListByUserId(userId);

                if (!productsDTOs.Any())
                    return NotFound("No Products found!");
                else
                    return Ok(productsDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/takeproductsofcurrentuser")]
        public async Task<IActionResult> TakeProductsOfCurrentUser()
        {
            try
            {
                var productsDTOs = await _userServices.TakeProductsOfCurrentUser();

                if (!productsDTOs.Any())
                    return NotFound("No Product found!");
                else
                    return Ok(productsDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/CheckIsLoginRegistered")]
        public async Task<IActionResult> CheckIsLoginRegistered([FromBody]string login)
        {
            try
            {
                if (ModelState.IsValid)
                    return Ok(await _userServices.CheckIsLoginRegistered(login));
                else 
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("/changeproductstatusofproduct")]
        public async Task<IActionResult> ChangePasswordByOldPassword([FromBody] string oldPassword, string newPassword)
        {
            try
            {
                var changePasswordByOldPassword = 
                    await _userServices.ChangePasswordByOldPassword(oldPassword, newPassword);
                if (changePasswordByOldPassword.Flag == true)
                    return Ok(changePasswordByOldPassword.Message);
                else
                    return BadRequest(changePasswordByOldPassword.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPatch("/changeproductstatusofproduct")]
        public async Task<IActionResult> ChangeForgottenPasswordBySecretWord([FromBody] string login, string secretWord, string newPassword)
        {
            try
            {
                var changeForgottenPasswordBySecretWord =
                    await _userServices.ChangeForgottenPasswordBySecretWord(login, secretWord, newPassword);
                if (changeForgottenPasswordBySecretWord.Flag == true)
                    return Ok(changeForgottenPasswordBySecretWord.Message);
                else
                    return BadRequest(changeForgottenPasswordBySecretWord.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
