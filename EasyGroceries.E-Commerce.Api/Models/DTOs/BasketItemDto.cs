namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents an item in the shopping basket.
    /// </summary>
    public class BasketItemDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the basket.
        /// </summary>
        public int Quantity { get; set; }
    }
}
