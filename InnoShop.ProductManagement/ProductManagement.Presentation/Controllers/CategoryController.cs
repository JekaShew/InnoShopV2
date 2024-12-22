using MediatR;
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
        public async Task<IActionResult> TakeCatigories()
        {
            var categoryDTOs = await _mediator.Send(new TakeCategoryDTOListQuery());
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TakeCatigoryById(Guid categoryId)
        {
            var categoryDTO = await _mediator.Send(new TakeCategoryDTOByIdQuery() { Id = categoryId });
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddCatigory([FromBody] CategoryDTO categoryDTO)
        {
            return Ok(await _mediator.Send(new AddCategoryCommand() { CategoryDTO = categoryDTO }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatigoryById(Guid categoryId)
        {
            return Ok(await _mediator.Send(new DeleteCategoryByIdCommand() { Id = categoryId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatigory([FromBody] CategoryDTO categoryDTO)
        {
            return Ok(await _mediator.Send(new UpdateCategoryCommand() { CategoryDTO = categoryDTO }));
        }
    }
}
