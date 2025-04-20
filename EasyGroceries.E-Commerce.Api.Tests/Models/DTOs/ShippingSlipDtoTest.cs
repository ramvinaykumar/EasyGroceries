using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.DTOs
{
    /// <summary>
    /// Unit tests for the ShippingSlipDto class.
    /// </summary>
    public class ShippingSlipDtoTest
    {
        /// <summary>
        /// Tests if the properties of ShippingSlipDto can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_SetAndGetProperties_ShouldBeValid()
        {
            // Arrange
            var orderId = 123;
            var customerName = "Test customer";
            var shippingAddress = "Test address";
            var orderDate = DateTime.Now;
            var items = new List<ShippingSlipItemDto>()
                {
                    new ShippingSlipItemDto { ProductName = "", Description= "", Quantity = 2 }
                };

            // Act
            var shippingSlipDto = new ShippingSlipDto()
            {
                CustomerName = customerName,
                OrderId = orderId,
                Items = items,
                ShippingAddress = shippingAddress,
            };

            // Assert
            Assert.Equal(customerName, shippingSlipDto.CustomerName);
            Assert.Equal(orderId, shippingSlipDto.OrderId);
            Assert.Equal(shippingAddress, shippingSlipDto.ShippingAddress);
            Assert.Equal(items, shippingSlipDto.Items);
        }

        /// <summary>
        /// Tests if the default values of ShippingSlipDto properties are valid.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_DefaultValues_ShouldBeValid()
        {
            // Arrange
            var shippingSlipDto = new ShippingSlipDto();
            int zero = 0;

            // Assert
            Assert.Equal(0, shippingSlipDto.OrderId); // Default value for int is 0
            Assert.Empty(shippingSlipDto.CustomerName);
            Assert.Empty(shippingSlipDto.ShippingAddress);
            Assert.Equal(zero, shippingSlipDto.Items.Count);
        }

        /// <summary>
        /// Tests if ShippingSlipDto handles null values correctly.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_SetNullValues_ShouldBeHandled()
        {
            // Arrange
            var shippingSlipDto = new ShippingSlipDto();
            // Act
            shippingSlipDto.CustomerName = string.Empty;
            shippingSlipDto.ShippingAddress = string.Empty;
            shippingSlipDto.Items = null;
            // Assert
            Assert.Empty(shippingSlipDto.CustomerName);
            Assert.Empty(shippingSlipDto.ShippingAddress);
            Assert.Null(shippingSlipDto.Items);
        }

        /// <summary>
        /// Tests if ShippingSlipDto handles empty values correctly.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_SetEmptyValues_ShouldBeHandled()
        {
            // Arrange
            var shippingSlipDto = new ShippingSlipDto();
            // Act
            shippingSlipDto.CustomerName = string.Empty;
            shippingSlipDto.ShippingAddress = string.Empty;
            shippingSlipDto.Items = new List<ShippingSlipItemDto>();
            // Assert
            Assert.Equal(string.Empty, shippingSlipDto.CustomerName);
            Assert.Equal(string.Empty, shippingSlipDto.ShippingAddress);
            Assert.NotNull(shippingSlipDto.Items);
        }

        /// <summary>
        /// Tests if ShippingSlipDto handles invalid property values correctly.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_SetInvalidValues_ShouldBeHandled()
        {
            // Arrange
            var shippingSlipDto = new ShippingSlipDto();
            // Act
            shippingSlipDto.CustomerName = "Test customer";
            shippingSlipDto.ShippingAddress = "Test address";
            shippingSlipDto.Items = new List<ShippingSlipItemDto>()
                {
                    new ShippingSlipItemDto { ProductName = "", Description= "", Quantity = 2 }
                };
            // Assert
            Assert.NotEqual("Invalid customer", shippingSlipDto.CustomerName);
            Assert.NotEqual("Invalid address", shippingSlipDto.ShippingAddress);
        }

        /// <summary>
        /// Tests if ShippingSlipDto handles invalid items correctly.
        /// </summary>
        [Fact]
        public void ShippingSlipDto_SetInvalidItems_ShouldBeHandled()
        {
            // Arrange
            var shippingSlipDto = new ShippingSlipDto();
            // Act
            shippingSlipDto.Items = new List<ShippingSlipItemDto>()
                {
                    new ShippingSlipItemDto { ProductName = "", Description= "", Quantity = 2 }
                };
            // Assert
            Assert.NotEmpty(shippingSlipDto.Items);
        }
    }
}
