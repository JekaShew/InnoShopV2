using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.ProductStatusQueries;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TakeProductStatuses()
        {
            var productStatusDTOs = await _mediator.Send(new TakeProductStatusDTOListQuery());
            return Ok(productStatusDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TakeProductStatusById(Guid productStatusId)
        {
            var productStatusDTO = await _mediator.Send(new TakeProductStatusDTOByIdQuery() { Id = productStatusId });
            return Ok(productStatusDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductStatus([FromBody] ProductStatusDTO productStatusDTO)
        {
            return Ok(await _mediator.Send(new AddProductStatusCommand() { ProductStatusDTO = productStatusDTO }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductStatusById(Guid productStatusId)
        {
            return Ok(await _mediator.Send(new DeleteProductStatusByIdCommand() { Id = productStatusId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductStatus([FromBody] ProductStatusDTO productStatusDTO)
        {
            return Ok(await _mediator.Send(new UpdateProductStatusCommand() { ProductStatusDTO = productStatusDTO }));
        }
    }
}
