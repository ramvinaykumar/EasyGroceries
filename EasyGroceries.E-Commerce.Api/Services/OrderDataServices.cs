using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Services
{
    /// <summary>
    /// Provides services for processing orders, including calculating totals, applying discounts, and managing order data.
    /// </summary>
    public class OrderDataServices : IOrderDataServices
    {
        private const decimal LoyaltyMembershipPrice = 5.00M;
        private const decimal LoyaltyDiscountRate = 0.20M;

        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDataServices"/> class.
        /// </summary>
        /// <param name="productRepository">Repository for managing product data.</param>
        /// <param name="customerRepository">Repository for managing customer data.</param>
        /// <param name="orderRepository">Repository for managing order data.</param>
        /// <param name="orderItemRepository">Repository for managing order item data.</param>
        public OrderDataServices(
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        
        /// <summary>
        /// Processes a checkout request by creating a new order, calculating totals, and saving order details.
        /// </summary>
        /// <param name="request">The checkout request containing customer, basket items, and shipping details.</param>
        /// <returns>An <see cref="CustomerOrderResponseDto"/> containing the order details and total amount.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the request is null.</exception>
        /// <exception cref="Exception">Thrown when a product in the basket is not found.</exception>
        public async Task<CustomerOrderResponseDto> ProcessOrderAsync(CheckoutRequestDto request)
        {
            var customerOrderResponse = new CustomerOrderResponseDto();
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var customerId = await _customerRepository.AddAsync(new Customer
            {
                CustomerName = request.Customer?.Name ?? string.Empty,
                EmailAddress = request.Customer?.Email,
                CustomerAddress = request.ShippingAddress
            });

            decimal totalAmount = 0;
            var orderItems = new List<OrderItem>();

            if (request.BasketItems != null && request.BasketItems.Any())
            {
                foreach (var basketItem in request.BasketItems)
                {
                    var product = await _productRepository.GetByIdAsync(basketItem.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {basketItem.ProductId} not found.");
                    }

                    decimal unitPrice = product.Price;
                    decimal discountedPrice = unitPrice;

                    if (request.IncludeLoyaltyMembership)
                    {
                        discountedPrice *= (1 - LoyaltyDiscountRate);
                    }

                    totalAmount += discountedPrice * basketItem.Quantity;
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.ProductId,
                        Quantity = basketItem.Quantity,
                        DiscountedPrice = discountedPrice,
                        Product = new Product
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName
                        }
                    });
                }
            }

            if (request.IncludeLoyaltyMembership)
            {
                totalAmount += LoyaltyMembershipPrice;
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                ShippingAddress = request.ShippingAddress == null ? string.Empty : request.ShippingAddress,
                OrderItems = orderItems
            };

            int orderId = await _orderRepository.AddAsync(order);

            foreach (var item in orderItems)
            {
                item.OrderId = orderId;
            }
            await _orderItemRepository.AddRangeAsync(orderItems);

            var shippingItems = orderItems.Select(oi => new OrderItemResponseDto
            {
                Name = oi.Product?.ProductName ?? "Unknown Product",
                Quantity = oi.Quantity
            }).ToList();

            if (request.IncludeLoyaltyMembership)
            {
                shippingItems.Add(new OrderItemResponseDto { Name = "EasyGroceries loyalty membership", Quantity = 1 });
            }

            customerOrderResponse.OrderResponse = new OrderResponseDto
            {
                CustomerId = customerId,
                OrderNumber = orderId,
                ItemLines = shippingItems,
                Total = totalAmount
            };
            // Create Shipping Slip
            customerOrderResponse.ShippingSlip = await _orderRepository.GenerateShippingSlipAsync(orderId);

            //return new OrderResponseDto
            //{
            //    CustomerId = customerId,
            //    OrderNumber = orderId,
            //    ItemLines = shippingItems,
            //    Total = totalAmount
            //};
            
            return customerOrderResponse;
        }

        public async Task<ShippingSlipDto> GetShippingSlipAsync(int orderId)
        {
            return await _orderRepository.GenerateShippingSlipAsync(orderId);
        }
    }
}
