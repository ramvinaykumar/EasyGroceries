namespace EasyGroceries.E_Commerce.Api.Models.Entities
{
    /// <summary>
    /// Represents an item in an order, including product details, quantity, and pricing.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the discounted price of the product in the order.
        /// </summary>
        public decimal DiscountedPrice { get; set; }

        /// <summary>
        /// Gets or sets the product details associated with this order item.
        /// </summary>
        public Product? Product { get; set; }
    }
}
