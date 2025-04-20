using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EasyGroceries.E_Commerce.Api.Controllers
{
    /// <summary>
    /// Controller for managing customer-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// The service responsible for customer data operations.
        /// </summary>
        private readonly ICustomerDataServices _customerDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="customerDataServices">The customer data services dependency.</param>
        public CustomersController(ICustomerDataServices customerDataServices)
        {
            _customerDataServices = customerDataServices;
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object containing the details of the customer to add.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Invalid request.");
            }
            try
            {
                var response = await _customerDataServices.AddCustomersAsync(customerDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the list of all customers.</returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerDataServices.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>An <see cref="IActionResult"/> containing the customer details if found, or a not found result if the customer does not exist.</returns>
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerDataServices.GetByIdAsync(id);
                if (customer == null)
                {
                    return NotFound($"Customer with ID {id} not found.");
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
