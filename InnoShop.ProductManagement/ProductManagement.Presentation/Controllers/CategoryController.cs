using InnoShop.CommonLibrary.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.CategoryQueries;
using ProductManagement.Application.Queries.SubCategoryQueries;
using ProductManagement.Domain.Data.Models;


namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TakeCatigories()
        {
            try
            {
                var categoryDTOs = await _mediator.Send(new TakeCategoryDTOListQuery());

                if (!categoryDTOs.Any())
                    return NotFound("No Category found!");
                else
                    return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> TakeCatigoryById(Guid categoryId)
        {
            try
            {
                var categoryDTO = await _mediator.Send(new TakeCategoryDTOByIdQuery() { Id = categoryId });
                if (categoryDTO is null)
                    return NotFound("No Category found!");
                else
                    return Ok(categoryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }     
        }

        [HttpPost]
        public async Task<IActionResult> AddCatigory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _mediator.Send(new AddCategoryCommand() { CategoryDTO = categoryDTO });
                    return Ok(result);
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCatigoryById(Guid categoryId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCategoryByIdCommand() { Id = categoryId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatigory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateCategoryCommand() { CategoryDTO = categoryDTO }));
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
