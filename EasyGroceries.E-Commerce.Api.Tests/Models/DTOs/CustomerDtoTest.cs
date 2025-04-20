using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.DTOs
{
    /// <summary>
    /// Unit tests for the CustomerDto class.
    /// </summary>
    public class CustomerDtoTests
    {
        /// <summary>
        /// Tests that the properties of the CustomerDto can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void CustomerDto_SetAndGetProperties_ShouldBeValid()
        {
            // Arrange
            var customerDto = new CustomerDto();
            var customerId = 101;
            var name = "John Doe";
            var email = "john.doe@example.com";
            var address = "123 Main St";

            // Act
            customerDto.CustomerId = customerId;
            customerDto.Name = name;
            customerDto.Email = email;
            customerDto.Address = address;

            // Assert
            Assert.Equal(customerId, customerDto.CustomerId);
            Assert.Equal(name, customerDto.Name);
            Assert.Equal(email, customerDto.Email);
            Assert.Equal(address, customerDto.Address);
        }

        /// <summary>
        /// Tests that the default values of the CustomerDto properties are as expected.
        /// </summary>
        [Fact]
        public void CustomerDto_DefaultValues_ShouldBeValid()
        {
            // Arrange
            var customerDto = new CustomerDto();

            // Assert
            Assert.Equal(0, customerDto.CustomerId); // Default value for int is 0
            Assert.Null(customerDto.Name);
            Assert.Null(customerDto.Email);
            Assert.Null(customerDto.Address);
        }

        /// <summary>
        /// Tests that setting null values to the CustomerDto properties is handled correctly.
        /// </summary>
        [Fact]
        public void CustomerDto_SetNullValues_ShouldBeHandled()
        {
            // Arrange
            var customerDto = new CustomerDto();

            // Act
            customerDto.Name = null;
            customerDto.Email = null;
            customerDto.Address = null;

            // Assert
            Assert.Null(customerDto.Name);
            Assert.Null(customerDto.Email);
            Assert.Null(customerDto.Address);
        }

        /// <summary>
        /// Tests that setting empty string values to the CustomerDto properties is handled correctly.
        /// </summary>
        [Fact]
        public void CustomerDto_SetEmptyStringValues_ShouldBeHandled()
        {
            // Arrange
            var customerDto = new CustomerDto();
            var emptyString = string.Empty;

            // Act
            customerDto.Name = emptyString;
            customerDto.Email = emptyString;
            customerDto.Address = emptyString;

            // Assert
            Assert.Equal(emptyString, customerDto.Name);
            Assert.Equal(emptyString, customerDto.Email);
            Assert.Equal(emptyString, customerDto.Address);
        }
    }
}
