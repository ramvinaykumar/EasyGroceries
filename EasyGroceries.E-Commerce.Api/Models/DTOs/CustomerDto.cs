namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a customer.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the customer.
        /// </summary>
        public string? Address { get; set; }
    }
}
