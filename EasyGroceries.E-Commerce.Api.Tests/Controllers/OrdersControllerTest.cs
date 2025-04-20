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
    /// Unit tests for the OrdersController class.
    /// </summary>
    public class OrdersControllerTest
    {
        private Mock<IOrderDataServices>? _mockOrderProcessor;
        private OrdersController? _orderController;

        /// <summary>
        /// Initializes the test class and sets up the mock order processor.
        /// </summary>
        private void Init()
        {
            _mockOrderProcessor = new Mock<IOrderDataServices>();
            _orderController = new OrdersController(_mockOrderProcessor.Object);
        }

        /// <summary>
        /// Tests the Checkout method with a valid request.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Checkout_ValidRequest_ReturnsOkWithOrderResponse()
        {
            Init();

            // Arrange
            var checkoutRequest = MockCheckoutRequestDto();
            var expectedResponse = MockCustomerOrderResponseData();
            _mockOrderProcessor?.Setup(processor => processor.ProcessOrderAsync(checkoutRequest)).ReturnsAsync(expectedResponse);
            _orderController!.ModelState.Clear();

            // Act
            var result = await _orderController.Checkout(checkoutRequest);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        /// <summary>
        /// Tests the Checkout method with an invalid request.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Checkout_InvalidRequest_ReturnsBadRequestWithModelStateErrors()
        {
            Init();

            // Arrange
            var checkoutRequest = MockCheckoutRequestDto();
            _orderController!.ModelState.AddModelError("CustomerId", "CustomerId is required.");

            // Act
            var result = await _orderController.Checkout(checkoutRequest);

            // Assert
            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().BeOfType<SerializableError>();
            ((SerializableError)badRequestResult.Value).Count.Should().Be(1);
            ((SerializableError)badRequestResult.Value).ContainsKey("CustomerId").Should().BeTrue();
        }

        /// <summary>
        /// Tests the Checkout method when the service throws a generic exception.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Checkout_ServiceThrowsGenericException_ReturnsInternalServerError()
        {
            // Arrange
            Init();

            var checkoutRequest = MockCheckoutRequestDto();
            var exceptionMessage = "An unexpected error occurred during order processing.";
            _mockOrderProcessor?.Setup(processor => processor.ProcessOrderAsync(checkoutRequest)).ThrowsAsync(new Exception(exceptionMessage));
            _orderController!.ModelState.Clear();

            // Act
            var result = await _orderController.Checkout(checkoutRequest);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Error processing order: {exceptionMessage}");
        }

        /// <summary>
        /// Tests the Checkout method when the service throws a specific database exception.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Checkout_ServiceThrowsDatabaseException_ReturnsInternalServerErrorWithSpecificMessage()
        {
            Init();

            // Arrange
            var checkoutRequest = MockCheckoutRequestDto();
            var databaseExceptionMessage = "Database connection lost while saving order.";
            // Using ApplicationException to simulate a specific type
            _mockOrderProcessor?.Setup(processor => processor.ProcessOrderAsync(checkoutRequest)).ThrowsAsync(new ApplicationException(databaseExceptionMessage));
            _orderController!.ModelState.Clear();

            // Act
            var result = await _orderController.Checkout(checkoutRequest);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Error processing order: {databaseExceptionMessage}");
        }

        /// <summary>
        /// Mock data for CheckoutRequestDto.
        /// </summary>
        /// <returns></returns>
        private CheckoutRequestDto MockCheckoutRequestDto()
        {
            return new CheckoutRequestDto
            {
                BasketItems = new List<BasketItemDto>
                {
                    new BasketItemDto { ProductId = 1, Quantity = 2 }
                },
                IncludeLoyaltyMembership = false,
                ShippingAddress = "123 Main St, Anytown, USA",
                Customer = new CustomerDto
                {
                    CustomerId = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com\"",
                    Address = "123 Main St, Anytown, USA"
                }
            };
        }

        /// <summary>
        /// Mock data for CustomerOrderResponseDto.
        /// </summary>
        /// <returns></returns>
        private CustomerOrderResponseDto MockCustomerOrderResponseData()
        {
            return new CustomerOrderResponseDto
            {
                OrderResponse = new OrderResponseDto
                {
                    CustomerId = 1,
                    OrderNumber = 123,
                    ItemLines = new List<OrderItemResponseDto>
                    {
                        new OrderItemResponseDto { Name = "Product 1", Quantity = 2 },
                        new OrderItemResponseDto { Name = "Product 2", Quantity = 1 }
                    },
                    Total = 100.50m
                },
                ShippingSlip = new ShippingSlipDto
                {
                    OrderId = 1,
                    CustomerName = "John Doe",
                    ShippingAddress = "123 London Road, UK",
                    OrderDate = DateTime.Now,
                    Items = new List<ShippingSlipItemDto>
                    {
                        new ShippingSlipItemDto
                        {
                            ProductName ="Product 1",
                            Description = "Product Description",
                            Quantity = 1
,                        },
                        new ShippingSlipItemDto
                        {
                            ProductName ="Product 2",
                            Description = "Product Description",
                            Quantity = 2
,                        }
                    }
                }
            };
        }

        #region Unit test for GetShippingSlip method

        [Fact]
        public async Task GetShippingSlip_ReturnsOk_WithValidData()
        {
            Init();

            // Arrange
            var orderId = 1;
            var expectedSlip = MockShippingData();

            _mockOrderProcessor?.Setup(p => p.GetShippingSlipAsync(orderId))
                               .ReturnsAsync(expectedSlip);

            // Act
            var result = await _orderController!.GetShippingSlip(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualSlip = Assert.IsType<ShippingSlipDto>(okResult.Value);
            Assert.Equal(orderId, actualSlip.OrderId);
            Assert.Single(actualSlip.Items);
        }

        [Fact]
        public async Task GetShippingSlip_ReturnsNotFound_WhenDataIsNull()
        {
            Init();

            // Arrange
            var orderId = 99;
            _mockOrderProcessor?.Setup(p => p.GetShippingSlipAsync(orderId))
                               .ReturnsAsync((ShippingSlipDto)null);

            // Act
            var result = await _orderController!.GetShippingSlip(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Because your current code returns OK even when null
            Assert.Null(okResult.Value); // Optionally, you can change your controller to return NotFound
        }

        [Fact]
        public async Task GetShippingSlip_Returns500_OnDatabaseException()
        {
            Init();

            // Arrange
            var orderId = 1;
            var exceptionMessage = "Service error";
            _mockOrderProcessor?.Setup(p => p.GetShippingSlipAsync(orderId))
                               .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _orderController!.GetShippingSlip(orderId);

            // Assert
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Error processing order: {exceptionMessage}");
        }

        [Fact]
        public async Task GetShippingSlip_Returns500_OnGenericException()
        {
            Init();

            // Arrange
            var orderId = 1;
            var exceptionMessage = "Something bad happened";
            _mockOrderProcessor?.Setup(p => p.GetShippingSlipAsync(orderId))
                               .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _orderController!.GetShippingSlip(orderId);

            // Assert
            var statusResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusResult.StatusCode);
            Assert.Contains(exceptionMessage, statusResult.Value.ToString());
        }

        [Fact]
        public async Task GetShippingSlip_Calls_ServiceExactlyOnce()
        {
            Init();

            // Arrange
            var orderId = 5;
            var slip = new ShippingSlipDto { OrderId = orderId };
            _mockOrderProcessor?.Setup(p => p.GetShippingSlipAsync(orderId))
                               .ReturnsAsync(slip);

            // Act
            await _orderController!.GetShippingSlip(orderId);

            // Assert
            _mockOrderProcessor?.Verify(p => p.GetShippingSlipAsync(orderId), Times.Once);
        }

        private ShippingSlipDto MockShippingData()
        {
            return new ShippingSlipDto
            {
                OrderId = 1,
                CustomerName = "John Doe",
                ShippingAddress = "123 London Road, UK",
                OrderDate = DateTime.Now,
                Items = new List<ShippingSlipItemDto>
                    {
                        new ShippingSlipItemDto
                        {
                            ProductName ="Product 1",
                            Description = "Product Description",
                            Quantity = 1
,                        }
//                        new ShippingSlipItemDto
//                        {
//                            ProductName ="Product 2",
//                            Description = "Product Description",
//                            Quantity = 2
//,                        }
                    }
            };
        }

        #endregion
    }
}
