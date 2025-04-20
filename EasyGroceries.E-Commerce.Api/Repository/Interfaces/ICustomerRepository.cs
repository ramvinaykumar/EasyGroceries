using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Repository.Interfaces
{
    /// <summary>
    /// Defines the contract for customer repository operations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of customers.</returns>
        Task<IEnumerable<Customer>> GetAllAsync();

        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customer">The customer entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the newly added customer.</returns>
        Task<int> AddAsync(Customer customer);

        /// <summary>
        /// Retrieves a customer by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer entity if found; otherwise, null.</returns>
        Task<Customer?> GetByIdAsync(int id);
    }
}
