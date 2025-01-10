using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.CategoryQueries;

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
        [Authorize]
        public async Task<IActionResult> TakeCatigories()
        {     
            var categoryDTOs = await _mediator.Send(new TakeCategoryDTOListQuery());
            if (!categoryDTOs.Any())
                return NotFound("No Category found!");
            else
                return Ok(categoryDTOs);      
        }

        [HttpGet("{categoryId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> TakeCatigoryById(Guid categoryId)
        {
            var categoryDTO = await _mediator.Send(new TakeCategoryDTOByIdQuery() { Id = categoryId });
            if (categoryDTO is null)
                return NotFound("The Category Not Found!");
            return Ok(categoryDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddCatigory([FromBody] CategoryDTO categoryDTO)
        {
            var result = await _mediator.Send(new AddCategoryCommand() { CategoryDTO = categoryDTO });
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCatigoryById(Guid categoryId)
        {
            return Ok(await _mediator.Send(new DeleteCategoryByIdCommand() { Id = categoryId }));             
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateCatigory([FromBody] CategoryDTO categoryDTO)
        {
            return Ok(await _mediator.Send(new UpdateCategoryCommand() { CategoryDTO = categoryDTO }));
        }
    }
}
