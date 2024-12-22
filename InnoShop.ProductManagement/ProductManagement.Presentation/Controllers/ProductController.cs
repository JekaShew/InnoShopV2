using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.ProductQueries;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> TakeProducts()
        {
            var productDTOs = await _mediator.Send(new TakeProductDTOListQuery());
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TakeProductById(Guid productId)
        {
            var productDTO = await _mediator.Send(new TakeProductDTOByIdQuery() { Id = productId });
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            return Ok(await _mediator.Send(new AddProductCommand() { ProductDTO = productDTO }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(Guid productId)
        {
            return Ok(await _mediator.Send(new DeleteProductByIdCommand() { Id = productId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            return Ok(await _mediator.Send(new UpdateProductCommand() { ProductDTO = productDTO }));
        }
    }
}
