using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;
using EasyGroceries.E_Commerce.Api.Services;
using Moq;

namespace EasyGroceries.E_Commerce.Api.Tests.Services
{
    /// <summary>
    /// Unit tests for the CustomerDataServices class.
    /// </summary>
    public class CustomerDataServicesTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly CustomerDataServices _customerDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDataServicesTests"/> class.
        /// </summary>
        public CustomerDataServicesTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _customerDataServices = new CustomerDataServices(_mockCustomerRepository.Object);
        }

        /// <summary>
        /// Tests the AddCustomersAsync method for successful addition of a customer.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddCustomersAsync_SuccessfulAddition_ReturnsCustomerDto()
        {
            // Arrange
            var requestDto = MockRequest_CustomerData();
            var expectedCustomerId = 1;
            var expectedCustomer = MockResponse_CustomerData(expectedCustomerId);

            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(expectedCustomerId);
            _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(expectedCustomerId)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await _customerDataServices.AddCustomersAsync(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(requestDto.CustomerId, result.CustomerId);
            Assert.Equal(requestDto.Name, result.Name);
            Assert.Equal(requestDto.Email, result.Email);
            Assert.Equal(requestDto.Address, result.Address);
            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.Is<Customer>(c =>
                c.CustomerId == requestDto.CustomerId &&
                c.CustomerName == requestDto.Name &&
                c.EmailAddress == requestDto.Email &&
                c.CustomerAddress == requestDto.Address)), Times.Once);
            _mockCustomerRepository.Verify(repo => repo.GetByIdAsync(expectedCustomerId), Times.Once);
        }

        /// <summary>
        /// Tests the AddCustomersAsync method for failed addition of a customer.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AddCustomersAsync_FailedAddition_ReturnsNull()
        {
            // Arrange
            int? failedCustomerId = null; // Simulate failure
            var requestDto = MockRequest_CustomerData();
            requestDto.CustomerId = failedCustomerId ?? 0;

            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(failedCustomerId ?? 0);

            // Act
            var result = await _customerDataServices.AddCustomersAsync(requestDto);

            // Assert
            Assert.Null(result);
            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Once);
            _mockCustomerRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsListOfCustomerDtos()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "John Doe", EmailAddress = "john.doe@example.com", CustomerAddress = "123 Main St" },
                new Customer { CustomerId = 2, CustomerName = "Jane Smith", EmailAddress = "jane.smith@example.com", CustomerAddress = "456 Oak Ave" }
            };
            _mockCustomerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerDataServices.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(result);
            Assert.Equal(customers.Count, result.Count());
            Assert.Equal(customers.First().CustomerId, result.First().CustomerId);
            Assert.Equal(customers.First().CustomerName, result.First().Name);
      
            _mockCustomerRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the GetAllCustomersAsync method when no customers exist.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAllCustomersAsync_ReturnsEmptyList_WhenNoCustomersExist()
        {
            // Arrange
            _mockCustomerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Customer>());

            // Act
            var result = await _customerDataServices.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _mockCustomerRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the GetByIdAsync method for a customer found.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdAsync_CustomerFound_ReturnsCustomerDto()
        {
            // Arrange
            var customerId = 1;
            var expectedCustomer = MockResponse_CustomerData(customerId);
            _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await _customerDataServices.GetByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.CustomerId, result.CustomerId);
            Assert.Equal(expectedCustomer.CustomerName, result.Name);
          
            _mockCustomerRepository.Verify(repo => repo.GetByIdAsync(customerId), Times.Once);
        }

        /// <summary>
        /// Tests the GetByIdAsync method when a customer is not found.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdAsync_CustomerNotFound_ReturnsNull()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(customerId)).ReturnsAsync((Customer)null);

            // Act
            var result = await _customerDataServices.GetByIdAsync(customerId);

            // Assert
            Assert.Null(result);
            _mockCustomerRepository.Verify(repo => repo.GetByIdAsync(customerId), Times.Once);
        }

        /// <summary>
        /// Mocks the request data for customer.
        /// </summary>
        /// <returns></returns>
        private CustomerDto MockRequest_CustomerData()
        {
            return new CustomerDto { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com", Address = "123 Main St" };
        }

        /// <summary>
        /// Mocks the response data for customer.
        /// </summary>
        /// <param name="expectedCustomerId"></param>
        /// <returns></returns>
        private Customer MockResponse_CustomerData(int expectedCustomerId)
        {
            return new Customer { CustomerId = expectedCustomerId, CustomerName = "John Doe", EmailAddress = "john.doe@example.com", CustomerAddress = "123 Main St" };
        }
    }
}
