namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents the response data for an order.
    /// </summary>
    public class OrderResponseDto
    {
        /// <summary>
        /// Gets or sets the ID of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique order number.
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the order.
        /// </summary>
        public List<OrderItemResponseDto>? ItemLines { get; set; }

        /// <summary>
        /// Gets or sets the total cost of the order.
        /// </summary>
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Represents the response data for a shipping slip.
    /// </summary>
    public class CustomerOrderResponseDto
    {
        /// <summary>
        /// Gets or sets the customer details.
        /// </summary>
        public OrderResponseDto OrderResponse { get; set; } = new OrderResponseDto();

        /// <summary>
        /// Gets or sets the shipping slip details.
        /// </summary>
        public ShippingSlipDto ShippingSlip { get; set; } = new ShippingSlipDto();
    }
    
}
