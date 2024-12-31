using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Domain.Data.Models;


namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductServices _productServices;
        public ProductController(IMediator mediator, IProductServices productServices)
        {
            _mediator = mediator;
            _productServices = productServices;
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
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createProduct = await _productServices.CreateProduct(productDTO);
                    if (createProduct.Flag == true)
                        return Ok(createProduct.Message);
                    else
                        return BadRequest(createProduct.Message);
                }
                else return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }

        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductById(Guid productId)
        {
            try
            {
                var userId = _productServices.GetCurrentUserId();

                var product = await _mediator.Send(new TakeProductDTOByIdQuery() { Id = productId });
                if (product != null)
                    if (product.UserId != userId.Value)
                        return Forbid("Forbidden Action Detected! You can't delete this Product!");
                    else
                        return Ok(await _mediator.Send(new DeleteProductByIdCommand() { Id = productId }));
                else
                    return BadRequest("Product not Found!");
            }
            catch(Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productDTO)
        {
            try
            {
                var userId = _productServices.GetCurrentUserId();

                var product = await _mediator.Send(new TakeProductDTOByIdQuery() { Id = productDTO.Id.Value });
                if (product != null)
                    if (product.UserId != userId.Value)
                        return Forbid("Forbidden Action Detected! You can't update this Product!");
                    else
                        return Ok(await _mediator.Send(new UpdateProductCommand() { ProductDTO = productDTO }));
                else
                    return BadRequest("Product not Found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }            
        }

        [HttpGet("/takefilteredproducts")]
        [Authorize]
        public async Task<IActionResult> TakeFilteredProducts([FromBody]ProductFilterDTO productFilterDTO)
        {
            try
            {
                var productDTOs = await _productServices.TakeFilteredProductDTOList(productFilterDTO);
                if (productDTOs is null)
                    return NotFound("No Product found!");
                else
                    return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/takesearchedproducts")]
        public async Task<IActionResult> TakeSearchedProducts([FromBody] string queryString)
        {
            try
            {
                var productDTOs = await _productServices.TakeSearchedProductDTOList(queryString);
                if (productDTOs is null)
                    return NotFound("No Product found!");
                else
                    return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/takeproductsbyuserid/{userId}")]
        [Authorize]
        public async Task<IActionResult> TakeProductsByUserId(Guid userId)
        {
            try
            {
                var productDTOs = await _productServices.TakeProductsByUserId(userId);
                if (productDTOs is null)
                    return NotFound("No Product found!");
                else
                    return Ok(productDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("/changeproductstatusofproduct")]
        [Authorize]
        public async Task<IActionResult> ChangeProductStatusOfProduct([FromBody] Guid productId, Guid productStatusId)
        {
            try
            {
                var userId = _productServices.GetCurrentUserId();

                var product = await _mediator.Send(new TakeProductDTOByIdQuery() { Id = productId });
                if (product != null)
                    if (product.UserId != userId.Value)
                        return Forbid("Forbidden Action Detected! You can't update this Product!");
                    else
                    {
                        var changeProductsStatusOfProduct = await _productServices.ChangeProductStatusOfProduct(productId, productStatusId);

                        if (changeProductsStatusOfProduct.Flag == true)
                            return Ok(changeProductsStatusOfProduct.Message);
                        else
                            return BadRequest(changeProductsStatusOfProduct.Message);
                    }
                else
                    return BadRequest("Product not Found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("/changeproductstatusofbadproduct")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ChangeProductStatusOfBadProduct([FromBody] Guid productId, Guid productStatusId)
        {
            try
            {
                var changeProductsStatusOfProduct = await _productServices.ChangeProductStatusOfProduct(productId, productStatusId);

                if (changeProductsStatusOfProduct.Flag == true)
                    return Ok(changeProductsStatusOfProduct.Message);
                else
                    return BadRequest(changeProductsStatusOfProduct.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("/changeproductstatusesofproductsbyuserid/{userId}")]
        [Authorize]
        public async Task<IActionResult> ChangeProductStatusesOfProductsByUserId(Guid userId)
        {
            try
            {
                var changeProductsStatusesOfProducts = await _productServices.ChangeProductStatusesOfProductsByUserId(userId);

                if (changeProductsStatusesOfProducts.Flag == true)
                    return Ok(changeProductsStatusesOfProducts.Message);
                else
                    return BadRequest(changeProductsStatusesOfProducts.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
