using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Tests.ClassValidator
{
    /// <summary>
    /// Provides validation methods for the Product entity.
    /// </summary>
    public static class ProducsgfsdtValidator
    {
        /// <summary>
        /// Validates the specified product to ensure it meets required conditions.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the product's CreatedDate is null or default.</exception>
        public static void ValidateProduct(Product product)
        {
            if (product.CreatedDate == default)
            {
                throw new ArgumentException("CreatedDate cannot be null or default.");
            }
        }
    }
}
