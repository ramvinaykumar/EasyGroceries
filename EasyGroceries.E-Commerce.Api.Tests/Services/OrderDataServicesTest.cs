using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;
using EasyGroceries.E_Commerce.Api.Services;
using Moq;

namespace EasyGroceries.E_Commerce.Api.Tests.Services
{
    /// <summary>
    /// Unit tests for the OrderDataServices class.
    /// </summary>
    public class OrderDataServicesTest
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IOrderItemRepository> _mockOrderItemRepository;
        private readonly OrderDataServices _orderDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDataServicesTest"/> class.
        /// </summary>
        public OrderDataServicesTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockOrderItemRepository = new Mock<IOrderItemRepository>();
            _orderDataServices = new OrderDataServices(
                _mockProductRepository.Object,
                _mockCustomerRepository.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object);
        }

        /// <summary>
        /// Tests that ProcessOrderAsync throws an ArgumentNullException when the request is null.
        /// Verifies that no repository methods are called in this scenario.
        /// </summary>
        [Fact]
        public async Task ProcessOrderAsync_NullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            CheckoutRequestDto request = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _orderDataServices.ProcessOrderAsync(request));

            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Never);
            _mockProductRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
            _mockOrderItemRepository.Verify(repo => repo.AddRangeAsync(It.IsAny<List<OrderItem>>()), Times.Never);
            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Tests that ProcessOrderAsync successfully processes an order without loyalty membership.
        /// Verifies that the correct repositories are called with the expected data and the response contains accurate order details.
        /// </summary>
        [Fact]
        public async Task ProcessOrderAsync_SuccessfulOrder_WithoutLoyalty()
        {
            // Arrange
            var request = new CheckoutRequestDto
            {
                Customer = new CustomerDto { Name = "John Doe", Email = "john.doe@example.com" },
                ShippingAddress = "123 Main St",
                BasketItems = new List<BasketItemDto>
                {
                    new BasketItemDto { ProductId = 1, Quantity = 2 },
                    new BasketItemDto { ProductId = 2, Quantity = 1 }
                },
                IncludeLoyaltyMembership = false
            };

            var customerId = 101;
            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customerId);

            var product1 = new Product { ProductId = 1, ProductName = "Product A", Price = 10.00M };
            var product2 = new Product { ProductId = 2, ProductName = "Product B", Price = 20.00M };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product1);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(product2);

            var orderId = 201;
            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(orderId);
            _mockOrderRepository.Setup(repo => repo.GenerateShippingSlipAsync(orderId)).ReturnsAsync(new ShippingSlipDto { OrderId = orderId });

            // Act
            var response = await _orderDataServices.ProcessOrderAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.OrderResponse);
            Assert.Equal(customerId, response.OrderResponse.CustomerId);
            Assert.Equal(orderId, response.OrderResponse.OrderNumber);
            Assert.Equal(2, response.OrderResponse.ItemLines.Count);
            Assert.Equal("Product A", response.OrderResponse.ItemLines[0].Name);
            Assert.Equal(2, response.OrderResponse.ItemLines[0].Quantity);
            Assert.Equal("Product B", response.OrderResponse.ItemLines[1].Name);
            Assert.Equal(1, response.OrderResponse.ItemLines[1].Quantity);
            Assert.Equal(10.00M * 2 + 20.00M * 1, response.OrderResponse.Total);
            Assert.NotNull(response.ShippingSlip);

            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.Is<Customer>(c =>
                c.CustomerName == "John Doe" && c.EmailAddress == "john.doe@example.com" && c.CustomerAddress == "123 Main St")), Times.Once);

            _mockProductRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _mockProductRepository.Verify(repo => repo.GetByIdAsync(2), Times.Once);

            _mockOrderItemRepository.Verify(repo => repo.AddRangeAsync(It.Is<List<OrderItem>>(items =>
                items.Count == 2 &&
                items.Any(i => i.ProductId == 1 && i.Quantity == 2 && i.DiscountedPrice == 10.00M) &&
                items.Any(i => i.ProductId == 2 && i.Quantity == 1 && i.DiscountedPrice == 20.00M))), Times.Once);

            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(orderId), Times.Once);
        }

        /// <summary>
        /// Tests that ProcessOrderAsync successfully processes an order with loyalty membership.
        /// Verifies that the correct repositories are called with the expected data and the response includes loyalty membership details.
        /// </summary>
        [Fact]
        public async Task ProcessOrderAsync_SuccessfulOrder_WithLoyalty()
        {
            // Arrange
            var request = new CheckoutRequestDto
            {
                Customer = new CustomerDto { Name = "Jane Doe", Email = "jane.doe@example.com" },
                ShippingAddress = "456 Oak Ave",
                BasketItems = new List<BasketItemDto>
                {
                    new BasketItemDto { ProductId = 3, Quantity = 1 },
                    new BasketItemDto { ProductId = 4, Quantity = 3 }
                },
                IncludeLoyaltyMembership = true
            };

            var customerId = 102;
            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customerId);

            var product3 = new Product { ProductId = 3, ProductName = "Product C", Price = 50.00M };
            var product4 = new Product { ProductId = 4, ProductName = "Product D", Price = 5.00M };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(3)).ReturnsAsync(product3);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(4)).ReturnsAsync(product4);

            var orderId = 202;
            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(orderId);
            _mockOrderRepository.Setup(repo => repo.GenerateShippingSlipAsync(orderId)).ReturnsAsync(new ShippingSlipDto { OrderId = 2 });

            // Act
            var response = await _orderDataServices.ProcessOrderAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.OrderResponse);
            Assert.Equal(customerId, response.OrderResponse.CustomerId);
            Assert.Equal(orderId, response.OrderResponse.OrderNumber);
            Assert.Equal(3, response.OrderResponse.ItemLines.Count); // Includes loyalty membership
            Assert.Contains(response.OrderResponse.ItemLines, i => i.Name == "EasyGroceries loyalty membership" && i.Quantity == 1);
            Assert.Contains(response.OrderResponse.ItemLines, i => i.Name == "Product C" && i.Quantity == 1);
            Assert.Contains(response.OrderResponse.ItemLines, i => i.Name == "Product D" && i.Quantity == 3);
            Assert.Equal((50.00M * (1 - 0.20M)) * 1 + (5.00M * (1 - 0.20M)) * 3 + 5.00M, response.OrderResponse.Total);
            Assert.NotNull(response.ShippingSlip);

            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.Is<Customer>(c =>
                c.CustomerName == "Jane Doe" && c.EmailAddress == "jane.doe@example.com" && c.CustomerAddress == "456 Oak Ave")), Times.Once);

            _mockProductRepository.Verify(repo => repo.GetByIdAsync(3), Times.Once);
            _mockProductRepository.Verify(repo => repo.GetByIdAsync(4), Times.Once);

            _mockOrderItemRepository.Verify(repo => repo.AddRangeAsync(It.Is<List<OrderItem>>(items =>
                items.Count == 2 &&
                items.Any(i => i.ProductId == 3 && i.Quantity == 1 && i.DiscountedPrice == 50.00M * 0.8M) &&
                items.Any(i => i.ProductId == 4 && i.Quantity == 3 && i.DiscountedPrice == 5.00M * 0.8M))), Times.Once);

            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(orderId), Times.Once);
        }

        /// <summary>
        /// Tests that ProcessOrderAsync throws an exception when a product in the basket is not found.
        /// Verifies that customer creation still occurs but no order or order items are created.
        /// </summary>
        [Fact]
        public async Task ProcessOrderAsync_ProductNotFound_ThrowsException()
        {
            // Arrange
            var request = new CheckoutRequestDto
            {
                BasketItems = new List<BasketItemDto>
                {
                    new BasketItemDto { ProductId = 99, Quantity = 1 }
                }
            };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(99)).ReturnsAsync((Product)null);
            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(1); // Customer creation should still happen

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _orderDataServices.ProcessOrderAsync(request));

            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.IsAny<Customer>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.GetByIdAsync(99), Times.Once);
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
            _mockOrderItemRepository.Verify(repo => repo.AddRangeAsync(It.IsAny<List<OrderItem>>()), Times.Never);
            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Tests that ProcessOrderAsync creates an order without items when the basket is empty.
        /// Verifies that the response contains an empty item list and a total of zero.
        /// </summary>
        [Fact]
        public async Task ProcessOrderAsync_EmptyBasket_CreatesOrderWithoutItems()
        {
            // Arrange
            var request = new CheckoutRequestDto
            {
                Customer = new CustomerDto { Name = "No Items", Email = "no.items@example.com" },
                ShippingAddress = "789 Pine Ln",
                BasketItems = new List<BasketItemDto>(),
                IncludeLoyaltyMembership = false
            };

            var customerId = 103;
            _mockCustomerRepository.Setup(repo => repo.AddAsync(It.IsAny<Customer>())).ReturnsAsync(customerId);

            var orderId = 203;
            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).ReturnsAsync(orderId);
            _mockOrderRepository.Setup(repo => repo.GenerateShippingSlipAsync(orderId)).ReturnsAsync(new ShippingSlipDto { OrderId = 3 });

            // Act
            var response = await _orderDataServices.ProcessOrderAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.OrderResponse);
            Assert.Equal(customerId, response.OrderResponse.CustomerId);
            Assert.Equal(orderId, response.OrderResponse.OrderNumber);
            Assert.Empty(response.OrderResponse.ItemLines);
            Assert.Equal(0, response.OrderResponse.Total); // No products, no loyalty
            Assert.NotNull(response.ShippingSlip);

            _mockCustomerRepository.Verify(repo => repo.AddAsync(It.Is<Customer>(c =>
                c.CustomerName == "No Items" && c.EmailAddress == "no.items@example.com" && c.CustomerAddress == "789 Pine Ln")), Times.Once);

            _mockProductRepository.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.Is<Order>(o =>
                o.CustomerId == customerId &&
                o.TotalAmount == 0 &&
                o.ShippingAddress == "789 Pine Ln" &&
                o.OrderItems.Count == 0)), Times.Once);

            _mockOrderItemRepository.Verify(repo => repo.AddRangeAsync(It.IsAny<List<OrderItem>>()), Times.Once);
            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(orderId), Times.Once);
        }

        /// <summary>
        /// Tests that GetShippingSlipAsync retrieves the correct shipping slip for a given order ID.
        /// Verifies that the repository method is called once and the returned shipping slip matches the expected data.
        /// </summary>
        [Fact]
        public async Task GetShippingSlipAsync_ReturnsShippingSlip()
        {
            // Arrange
            int orderId = 501;
            var expectedShippingSlip = new ShippingSlipDto { OrderId = orderId };
            _mockOrderRepository.Setup(repo => repo.GenerateShippingSlipAsync(orderId)).ReturnsAsync(expectedShippingSlip);

            // Act
            var result = await _orderDataServices.GetShippingSlipAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedShippingSlip.OrderId, result.OrderId);
            _mockOrderRepository.Verify(repo => repo.GenerateShippingSlipAsync(orderId), Times.Once);
        }
    }
}
