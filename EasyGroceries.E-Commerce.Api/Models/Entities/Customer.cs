namespace EasyGroceries.E_Commerce.Api.Models.Entities
{
    /// <summary>
    /// Represents a customer entity in the e-commerce system.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the address of the customer.
        /// </summary>
        public string? CustomerAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the date when the customer was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
