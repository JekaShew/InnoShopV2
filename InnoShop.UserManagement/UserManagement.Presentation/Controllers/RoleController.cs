using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.RoleQueries;

namespace UserMangement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> TakeRoles()
        {
            try
            {
                var roleDTOs = await _mediator.Send(new TakeRoleDTOListQuery());

                if (!roleDTOs.Any())
                    return NotFound("No Role found!");
                else
                    return Ok(roleDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{roleId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> TakeRoleById(Guid roleId)
        {
            try
            {
                var roleDTO = await _mediator.Send(new TakeRoleDTOByIdQuery() { Id = roleId });
                if (roleDTO is null)
                    return NotFound("No Role found!");
                else
                    return Ok(roleDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO roleDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new AddRoleCommand() { RoleDTO = roleDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{roleId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRoleById(Guid roleId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteRoleByIdCommand() { Id = roleId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDTO roleDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateRoleCommand() { RoleDTO = roleDTO }));
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
