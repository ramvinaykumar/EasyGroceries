namespace EasyGroceries.E_Commerce.Api.Models.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) for adding an item to the shopping basket.
    /// </summary>
    public class AddItemToBasketDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product to be added to the basket.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
    }
}
