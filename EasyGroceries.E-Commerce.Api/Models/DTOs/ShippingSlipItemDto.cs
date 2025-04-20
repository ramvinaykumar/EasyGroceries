namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents an item in a shipping slip.
    /// </summary>
    public class ShippingSlipItemDto
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }
    }
}
