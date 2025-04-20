using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Repository.Interfaces
{
    /// <summary>
    /// Defines the contract for product repository operations.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves all products asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of products.</returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the product if found; otherwise, null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new product asynchronously.
        /// </summary>
        /// <param name="newProduct">The product to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of rows affected.</returns>
        Task<int> AddAsync(Product newProduct);

        /// <summary>
        /// Updates an existing product asynchronously.
        /// </summary>
        /// <param name="updateProduct">The product with updated information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of rows affected.</returns>
        Task<int> UpdateAsync(Product updateProduct);
    }
}
