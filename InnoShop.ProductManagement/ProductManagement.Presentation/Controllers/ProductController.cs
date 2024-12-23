﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Application.Queries.SubCategoryQueries;
using ProductManagement.Domain.Data.Models;

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
            try
            {
                var productDTOs = await _mediator.Send(new TakeProductDTOListQuery());

                if (!productDTOs.Any())
                    return NotFound("No Product found!");
                else
                    return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }      
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> TakeProductById(Guid productId)
        {
            try
            {
                var productDTO = await _mediator.Send(new TakeProductDTOByIdQuery() { Id = productId });
                if (productDTO is null)
                    return NotFound("No Product found!");
                else
                    return Ok(productDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }       
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new AddProductCommand() { ProductDTO = productDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(Guid productId)
        {
            return Ok(await _mediator.Send(new DeleteProductByIdCommand() { Id = productId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                return Ok(await _mediator.Send(new UpdateProductCommand() { ProductDTO = productDTO }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}