using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EasyGroceries.E_Commerce.Api.Controllers
{
    /// <summary>
    /// Controller for managing orders in the EasyGroceries E-Commerce API.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The service responsible for processing orders.
        /// </summary>
        private readonly IOrderDataServices _orderProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderProcessor">The service responsible for processing orders.</param>
        public OrdersController(IOrderDataServices orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        /// <summary>
        /// Processes a checkout request and creates an order.
        /// </summary>
        /// <param name="checkoutRequest">The checkout request containing order details.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order response or an error message.</returns>
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto checkoutRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderResponse = await _orderProcessor.ProcessOrderAsync(checkoutRequest);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing order: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a shipping slip for a specific order.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order</param>
        /// <returns></returns>
        [HttpGet("getshippingslip/{orderid}")]
        public async Task<IActionResult> GetShippingSlip(int orderId)
        {
            try
            {
                var orderResponse = await _orderProcessor.GetShippingSlipAsync(orderId);
                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error processing order: {ex.Message}");
            }
        }
    }
}
