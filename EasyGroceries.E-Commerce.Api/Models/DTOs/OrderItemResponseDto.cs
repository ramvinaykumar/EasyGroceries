namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents the response data for an order item.
    /// </summary>
    public class OrderItemResponseDto
    {
        /// <summary>
        /// Gets or sets the name of the order item.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the order item.
        /// </summary>
        public int Quantity { get; set; }
    }
}
