using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Interfaces
{
    /// <summary>
    /// Interface for managing product data services.
    /// Provides methods for retrieving, adding, and updating product information.
    /// </summary>
    public interface IProductDataServices
    {
        /// <summary>
        /// Retrieves all products asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of ProductDto objects.</returns>
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        /// <summary>
        /// Retrieves a product by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ProductDto object if found, otherwise null.</returns>
        Task<ProductDto?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product asynchronously.
        /// </summary>
        /// <param name="newProduct">The ProductDto object representing the new product to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added ProductDto object.</returns>
        Task<ProductDto?> AddAsync(ProductDto newProduct);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="updateProduct">The ProductDto object containing updated product information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated ProductDto object.</returns>
        Task<ProductDto?> UpdateAsync(ProductDto updateProduct);
    }
}
