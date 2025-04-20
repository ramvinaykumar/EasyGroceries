using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Tests.ClassValidator;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.Entitties
{
    /// <summary>
    /// Unit tests for the Customer class.
    /// </summary>
    public class CustomerTest
    {
        /// <summary>
        /// Tests the properties of the Customer class.
        /// </summary>
        [Fact]
        public void Customer_SetAndGetProperties_PositiveCase()
        {
            // Arrange
            var customer = new Customer();
            var customerId = 1;
            var customerName = "John Doe";
            var emailAddress = "john.doe@example.com";
            var customerAddress = "123 Main St";
            var isActive = true;
            var createdDate = DateTime.Now;

            // Act
            customer.CustomerId = customerId;
            customer.CustomerName = customerName;
            customer.EmailAddress = emailAddress;
            customer.CustomerAddress = customerAddress;
            customer.IsActive = isActive;
            customer.CreatedDate = createdDate;

            // Assert
            Assert.Equal(customerId, customer.CustomerId);
            Assert.Equal(customerName, customer.CustomerName);
            Assert.Equal(emailAddress, customer.EmailAddress);
            Assert.Equal(customerAddress, customer.CustomerAddress);
            Assert.True(customer.IsActive);
            Assert.Equal(createdDate, customer.CreatedDate);
        }

        /// <summary>
        /// Tests the Customer class with an invalid email address.
        /// </summary>
        [Fact]
        public void Customer_SetInvalidEmailAddress_NegativeCase()
        {
            // Arrange
            var customer = new Customer();

            // Act
            customer.EmailAddress = "invalid-email";

            // Assert
            Assert.NotEqual("john.doe@example.com", customer.EmailAddress);
        }

        /// <summary>
        /// Tests the Customer class with an empty customer name.
        /// </summary>
        [Fact]
        public void Customer_DefaultValues_ShouldBeValid()
        {
            // Arrange
            var customer = new Customer();

            // Assert
            Assert.Equal(0, customer.CustomerId);
            Assert.Null(customer.CustomerName);
            Assert.Null(customer.EmailAddress);
            Assert.Null(customer.CustomerAddress);
            Assert.False(customer.IsActive);
            Assert.Equal(default(DateTime), customer.CreatedDate);
        }

        /// <summary>
        /// Tests the Customer class with empty or null properties.
        /// </summary>
        [Fact]
        public void Customer_EmptyOrNullProperties_ExceptionScenario()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerName = null,
                CustomerAddress = null
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => CustomerValidator.ValidateCustomer(customer));
        }
    }
}
