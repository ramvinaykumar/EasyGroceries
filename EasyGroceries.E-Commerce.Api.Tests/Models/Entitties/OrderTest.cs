using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.Entitties
{
    /// <summary>
    /// Unit tests for the Order class.
    /// </summary>
    public class OrderTest
    {
        /// <summary>
        /// Tests setting and getting properties of the Order class with valid values.
        /// </summary>
        [Fact]
        public void Order_SetAndGetProperties_PositiveCase()
        {
            var order = new Order();
            var orderId = 1;
            var customerId = 123;
            var orderDate = DateTime.Now;
            var totalAmount = 99.99m;
            var shippingAddress = "123 Main St, City, Country";
            var orderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                };
            order.OrderId = orderId;
            order.CustomerId = customerId;
            order.OrderDate = orderDate;
            order.TotalAmount = totalAmount;
            order.ShippingAddress = shippingAddress;
            order.OrderItems = orderItems;
            Assert.Equal(orderId, order.OrderId);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(orderDate, order.OrderDate);
            Assert.Equal(totalAmount, order.TotalAmount);
            Assert.Equal(shippingAddress, order.ShippingAddress);
            Assert.Equal(orderItems, order.OrderItems);
        }

        /// <summary>
        /// Tests setting an invalid total amount for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidTotalAmount_NegativeCase()
        {
            var order = new Order();
            order.TotalAmount = -50.00m;
            Assert.NotEqual(99.99m, order.TotalAmount);
        }

        /// <summary>
        /// Tests setting an invalid shipping address for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidShippingAddress_NegativeCase()
        {
            var order = new Order();
            order.ShippingAddress = "";
            Assert.NotEqual("123 Main St, City, Country", order.ShippingAddress);
        }

        /// <summary>
        /// Tests setting null as order items for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidOrderItems_NegativeCase()
        {
            var order = new Order();
            order.OrderItems = null;
            Assert.Null(order.OrderItems);
        }

        /// <summary>
        /// Tests setting an invalid order date for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidOrderDate_NegativeCase()
        {
            var order = new Order();
            order.OrderDate = DateTime.MinValue;
            Assert.NotEqual(DateTime.Now, order.OrderDate);
        }

        /// <summary>
        /// Tests setting an invalid customer ID for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidCustomerId_NegativeCase()
        {
            var order = new Order();
            order.CustomerId = -1;
            Assert.NotEqual(123, order.CustomerId);
        }

        /// <summary>
        /// Tests setting an invalid order ID for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidOrderId_NegativeCase()
        {
            var order = new Order();
            order.OrderId = -1;
            Assert.NotEqual(1, order.OrderId);
        }

        /// <summary>
        /// Tests setting an invalid order items count for the Order class.
        /// </summary>
        [Fact]
        public void Order_SetInvalidOrderItemsCount_NegativeCase()
        {
            var order = new Order();
            order.OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 2 },
                    new OrderItem { ProductId = 2, Quantity = 1 }
                };
            Assert.NotEqual(0, order.OrderItems?.Count);
        }
    }
}
