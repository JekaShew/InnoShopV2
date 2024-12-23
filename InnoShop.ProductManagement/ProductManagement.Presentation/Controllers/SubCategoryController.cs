using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.SubCategoryQueries;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TakeSubCatigories()
        {
            try
            {
                var subCategoryDTOs = await _mediator.Send(new TakeSubCategoryDTOListQuery());
                    
                if (!subCategoryDTOs.Any())
                    return NotFound("No SubCategory found!"); 
                else
                    return Ok(subCategoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{subCategoryId}")]
        public async Task<IActionResult> TakeSubCatigoryById(Guid subCategoryId)
        {
            try
            {
                var subCategoryDTO = await _mediator.Send(new TakeSubCategoryDTOByIdQuery() { Id = subCategoryId });
                if (subCategoryDTO is null)
                    return NotFound("No SubCategory found!"); 
                else
                    return Ok(subCategoryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCatigory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new AddSubCategoryCommand() { SubCategoryDTO = subCategoryDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }     
        }

        [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCatigoryById(Guid subCategoryId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteSubCategoryByIdCommand() { Id = subCategoryId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubCatigory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateSubCategoryCommand() { SubCategoryDTO = subCategoryDTO }));
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
