using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Interfaces
{
    /// <summary>
    /// Interface for customer data services.
    /// </summary>
    public interface ICustomerDataServices
    {
        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CustomerDto objects.</returns>
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();

        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="customerDto">The customer data transfer object containing the details of the customer to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added CustomerDto object, or null if the operation fails.</returns>
        Task<CustomerDto?> AddCustomersAsync(CustomerDto customerDto);

        /// <summary>
        /// Retrieves a customer by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CustomerDto object if found, or null if not found.</returns>
        Task<CustomerDto?> GetByIdAsync(int id);
    }
}
