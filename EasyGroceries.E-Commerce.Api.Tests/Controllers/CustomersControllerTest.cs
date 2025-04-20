using EasyGroceries.E_Commerce.Api.Controllers;
using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EasyGroceries.E_Commerce.Api.Tests.Controllers
{
    /// <summary>
    /// Unit tests for the CustomersController class.
    /// </summary>
    public class CustomersControllerTest
    {
        private Mock<ICustomerDataServices>? _mockCustomerDataServices;
        private CustomersController? _customerController;

        /// <summary>
        /// Initializes the mock services and controller instance.
        /// </summary>
        private void Init()
        {
            _mockCustomerDataServices = new Mock<ICustomerDataServices>();
            _customerController = new CustomersController(_mockCustomerDataServices.Object);
        }

        /// <summary>
        /// Tests the AddNewCustomer method with valid input, expecting an Ok result with the response.
        /// </summary>
        [Fact]
        public async Task AddNewCustomer_ValidInput_ReturnsOkWithResponse()
        {
            Init();

            // Arrange
            var customerDto = new CustomerDto { CustomerId = 0, Name = "John Doe", Email = "john.doe@example.com", Address = "Test Address." };
            var expectedResponse = MockCustomerData();
            _mockCustomerDataServices?.Setup(service => service.AddCustomersAsync(customerDto)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _customerController!.AddNewCustomer(customerDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        /// <summary>
        /// Tests the AddNewCustomer method with invalid input, expecting a BadRequest result.
        /// </summary>
        [Fact]
        public async Task AddNewCustomer_InvalidInput_ReturnsBadRequest()
        {
            Init();

            // Arrange
            CustomerDto customerDto = null;

            // Act
            var result = await _customerController!.AddNewCustomer(customerDto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        /// <summary>
        /// Tests the AddNewCustomer method when the service throws an exception, expecting an InternalServerError result.
        /// </summary>
        [Fact]
        public async Task AddNewCustomer_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();

            // Arrange
            var customerDto = new CustomerDto { Name = "John Doe", Email = "john.doe@example.com" };
            var exceptionMessage = "Database connection failed";
            _mockCustomerDataServices?.Setup(service => service.AddCustomersAsync(customerDto)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _customerController!.AddNewCustomer(customerDto);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Tests the GetAllCustomers method when data is found, expecting an Ok result with the customer list.
        /// </summary>
        [Fact]
        public async Task GetAllCustomers_DataFound_ReturnsOkWithCustomerList()
        {
            Init();

            // Arrange
            var customers = new List<CustomerDto>
                {
                    new CustomerDto { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com", Address="Test address" },
                    new CustomerDto { CustomerId = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Address="Test address 2" }
                };
            _mockCustomerDataServices?.Setup(service => service.GetAllCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerController!.GetAllCustomers();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(customers);
        }

        /// <summary>
        /// Tests the GetAllCustomers method when no data is found, expecting an Ok result with an empty list.
        /// </summary>
        [Fact]
        public async Task GetAllCustomers_NoDataFound_ReturnsOkWithEmptyList()
        {
            Init();

            // Arrange
            var customers = new List<CustomerDto>();
            _mockCustomerDataServices?.Setup(service => service.GetAllCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerController!.GetAllCustomers();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            ((IEnumerable<CustomerDto>)okResult.Value).Should().BeEmpty();
        }

        /// <summary>
        /// Tests the GetAllCustomers method when the service throws an exception, expecting an InternalServerError result.
        /// </summary>
        [Fact]
        public async Task GetAllCustomers_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();

            // Arrange
            var exceptionMessage = "Failed to retrieve customers";
            _mockCustomerDataServices?.Setup(service => service.GetAllCustomersAsync()).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _customerController!.GetAllCustomers();

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Tests the GetCustomerById method when data is found, expecting an Ok result with the customer details.
        /// </summary>
        [Fact]
        public async Task GetCustomerById_DataFound_ReturnsOkWithCustomer()
        {
            Init();

            // Arrange
            int customerId = 1;
            var customer = new CustomerDto { CustomerId = customerId, Name = "John Doe", Email = "john.doe@example.com" };
            _mockCustomerDataServices?.Setup(service => service.GetByIdAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerController!.GetCustomerById(customerId);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(customer);
        }

        /// <summary>
        /// Tests the GetCustomerById method when no data is found, expecting a NotFound result.
        /// </summary>
        [Fact]
        public async Task GetCustomerById_DataNotFound_ReturnsNotFound()
        {
            Init();

            // Arrange
            int customerId = 1;
            _mockCustomerDataServices?.Setup(service => service.GetByIdAsync(customerId)).ReturnsAsync((CustomerDto)null);

            // Act
            var result = await _customerController!.GetCustomerById(customerId);

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be($"Customer with ID {customerId} not found.");
        }

        /// <summary>
        /// Tests the GetCustomerById method when the service throws an exception, expecting an InternalServerError result.
        /// </summary>
        [Fact]
        public async Task GetCustomerById_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();

            // Arrange
            int customerId = 1;
            var exceptionMessage = "Error fetching customer";
            _mockCustomerDataServices?.Setup(service => service.GetByIdAsync(customerId)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _customerController!.GetCustomerById(customerId);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Mocks customer data for testing purposes.
        /// </summary>
        /// <returns>A mock CustomerDto object.</returns>
        private CustomerDto MockCustomerData()
        {
            return new CustomerDto
            {
                CustomerId = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                Address = "Test address"
            };
        }
    }
}
