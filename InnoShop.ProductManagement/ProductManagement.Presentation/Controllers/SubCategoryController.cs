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
            //var subCategories =  await subCategory.TakeAllSubCategories();
            var subCategoryDTOs = await _mediator.Send(new TakeSubCategoryDTOListQuery());
            return Ok(subCategoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TakeSubCatigoryById(Guid subCategoryId)
        {
            var subCategoryDTO = await _mediator.Send(new TakeSubCategoryDTOByIdQuery() { Id = subCategoryId });
            return Ok(subCategoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCatigory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            return Ok(await _mediator.Send(new AddSubCategoryCommand() { SubCategoryDTO = subCategoryDTO }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCatigoryById(Guid subCategoryId)
        {
            return Ok(await _mediator.Send(new DeleteSubCategoryByIdCommand() { Id = subCategoryId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubCatigory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            return Ok(await _mediator.Send(new UpdateSubCategoryCommand() { SubCategoryDTO = subCategoryDTO }));
        }
    }
}
