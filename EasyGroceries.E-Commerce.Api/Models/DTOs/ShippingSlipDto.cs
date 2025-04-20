namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents a shipping slip for an order, including customer details, items, and shipping information.
    /// </summary>
    public class ShippingSlipDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer who placed the order.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items included in the shipping slip.
        /// </summary>
        public List<ShippingSlipItemDto> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }
    }
}
