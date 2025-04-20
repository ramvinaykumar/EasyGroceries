using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EasyGroceries.E_Commerce.Api.Controllers
{
    /// <summary>
    /// Controller for managing product-related operations in the EasyGroceries E-Commerce API.
    /// Provides endpoints for retrieving, adding, and updating product information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Service for handling product data operations.
        /// </summary>
        private readonly IProductDataServices _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">Service for handling product data operations.</param>
        public ProductsController(IProductDataServices productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves a list of all available products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet("productslist")]
        public async Task<IActionResult> GetGroceries()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product with the specified ID, or a not found response if it does not exist.</returns>
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a new product to the system.
        /// </summary>
        /// <param name="requestDto">The product data to add.</param>
        /// <returns>The added product, or an error response if the operation fails.</returns>
        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid request.");
            }
            try
            {
                var response = await _productService.AddAsync(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing product in the system.
        /// </summary>
        /// <param name="requestDto">The updated product data.</param>
        /// <returns>The updated product, or an error response if the operation fails.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest("Invalid request.");
            }
            try
            {
                var response = await _productService.UpdateAsync(requestDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
