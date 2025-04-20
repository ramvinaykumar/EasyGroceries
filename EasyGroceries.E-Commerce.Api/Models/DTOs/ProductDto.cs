namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents a data transfer object for a product.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the product is physical.
        /// </summary>
        public bool IsPhysical { get; set; }
    }
}
