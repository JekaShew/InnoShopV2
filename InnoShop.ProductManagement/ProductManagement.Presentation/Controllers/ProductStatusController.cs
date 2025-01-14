﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> TakeProductStatuses()
        {
            try
            {
                var productStatusDTOs = await _mediator.Send(new TakeProductStatusDTOListQuery());

                if (!productStatusDTOs.Any())
                    return NotFound("No Product Status found!");
                else
                    return Ok(productStatusDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpGet("{productStatusId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> TakeProductStatusById(Guid productStatusId)
        {
            try
            {
                var productStatusDTO = await _mediator.Send(new TakeProductStatusDTOByIdQuery() { Id = productStatusId });
                if (productStatusDTO is null)
                    return NotFound("No Product Status found!");
                else
                    return Ok(productStatusDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }               
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddProductStatus([FromBody] ProductStatusDTO productStatusDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new AddProductStatusCommand() { ProductStatusDTO = productStatusDTO }));
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }      
        }

        [HttpDelete("{productStatusId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteProductStatusById(Guid productStatusId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteProductStatusByIdCommand() { Id = productStatusId }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateProductStatus([FromBody] ProductStatusDTO productStatusDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _mediator.Send(new UpdateProductStatusCommand() { ProductStatusDTO = productStatusDTO }));
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
