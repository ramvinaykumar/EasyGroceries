namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Represents the data required to process a checkout request.
    /// </summary>
    public class CheckoutRequestDto
    {
        /// <summary>
        /// Gets or sets the list of items in the basket for checkout.
        /// </summary>
        public List<BasketItemDto>? BasketItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include loyalty membership benefits in the checkout.
        /// </summary>
        public bool IncludeLoyaltyMembership { get; set; }

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string? ShippingAddress { get; set; }

        /// <summary>
        /// Gets or sets the customer details associated with the checkout.
        /// </summary>
        public CustomerDto? Customer { get; set; }
    }
}
