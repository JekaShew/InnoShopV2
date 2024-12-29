using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.UserStatusQueries;

namespace UserManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TakeUserStatuses()
        {
            try
            {
                var userStatusDTOs = await _mediator.Send(new TakeUserStatusDTOListQuery());

                if (!userStatusDTOs.Any())
                    return NotFound("No User Status found!");
                else
                    return Ok(userStatusDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userStatusId}")]
        public async Task<IActionResult> TakeUserStatusById(Guid userStatusId)
        {
            try
            {
                var userStatusDTO = await _mediator.Send(new TakeUserStatusDTOByIdQuery() { Id = userStatusId });
                if (userStatusDTO is null)
                    return NotFound("No User Status found!");
                else
                    return Ok(userStatusDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUserStatus([FromBody] UserStatusDTO userStatusDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new AddUserStatusCommand() { UserStatusDTO = userStatusDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{userStatusId}")]
        public async Task<IActionResult> DeleteUserStatusById(Guid userStatusId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteUserStatusByIdCommand() { Id = userStatusId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UserStatusDTO userStatusDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateUserStatusCommand() { UserStatusDTO = userStatusDTO }));
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
