namespace EasyGroceries.E_Commerce.Api.Models.Entities
{
    /// <summary>
    /// Represents an order placed by a customer in the e-commerce system.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of items included in the order.
        /// </summary>
        public List<OrderItem>? OrderItems { get; set; }
    }
}
