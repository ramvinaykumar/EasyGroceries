namespace EasyGroceries.E_Commerce.Api.Models.Entities
{
    /// <summary>
    /// Represents a product in the e-commerce system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string? ProductDesc { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is physical.
        /// </summary>
        public bool IsPhysical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the date when the product was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
