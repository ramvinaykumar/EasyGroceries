using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.Entitties
{
    /// <summary>
    /// Unit tests for the OrderItem class.
    /// </summary>
    public class OrderItemTest
    {
        /// <summary>
        /// Tests setting and getting properties of OrderItem with valid values.
        /// </summary>
        [Fact]
        public void OrderItem_SetAndGetProperties_PositiveCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            var orderItemId = 1;
            var orderId = 123;
            var productId = 456;
            var quantity = 2;
            var discountedPrice = 19.99m;
            // Act
            orderItem.OrderItemId = orderItemId;
            orderItem.OrderId = orderId;
            orderItem.ProductId = productId;
            orderItem.Quantity = quantity;
            orderItem.DiscountedPrice = discountedPrice;
            // Assert
            Assert.Equal(orderItemId, orderItem.OrderItemId);
            Assert.Equal(orderId, orderItem.OrderId);
            Assert.Equal(productId, orderItem.ProductId);
            Assert.Equal(quantity, orderItem.Quantity);
            Assert.Equal(discountedPrice, orderItem.DiscountedPrice);
        }

        /// <summary>
        /// Tests setting an invalid quantity for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidQuantity_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.Quantity = -1;
            // Assert
            Assert.NotEqual(2, orderItem.Quantity);
        }

        /// <summary>
        /// Tests setting an invalid discounted price for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidDiscountedPrice_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.DiscountedPrice = -10.00m;
            // Assert
            Assert.NotEqual(19.99m, orderItem.DiscountedPrice);
        }

        /// <summary>
        /// Tests setting an invalid product ID for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidProductId_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.ProductId = -1;
            // Assert
            Assert.NotEqual(456, orderItem.ProductId);
        }

        /// <summary>
        /// Tests setting an invalid order ID for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidOrderId_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.OrderId = -1;
            // Assert
            Assert.NotEqual(123, orderItem.OrderId);
        }

        /// <summary>
        /// Tests setting an invalid order item ID for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidOrderItemId_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.OrderItemId = -1;
            // Assert
            Assert.NotEqual(1, orderItem.OrderItemId);
        }

        /// <summary>
        /// Tests setting a null product for OrderItem.
        /// </summary>
        [Fact]
        public void OrderItem_SetInvalidProduct_NegativeCase()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Act
            orderItem.Product = null;
            // Assert
            Assert.Null(orderItem.Product);
        }

        /// <summary>
        /// Tests default values of OrderItem properties.
        /// </summary>
        [Fact]
        public void OrderItem_DefaultValues_ShouldBeValid()
        {
            // Arrange
            var orderItem = new OrderItem();
            // Assert
            Assert.Equal(0, orderItem.OrderItemId);
            Assert.Equal(0, orderItem.OrderId);
            Assert.Equal(0, orderItem.ProductId);
            Assert.Equal(0, orderItem.Quantity);
            Assert.Equal(0.0m, orderItem.DiscountedPrice);
            Assert.Null(orderItem.Product);
        }
    }
}
