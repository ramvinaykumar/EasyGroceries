using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Repository.Interfaces
{
    /// <summary>
    /// Defines the contract for managing orders in the e-commerce system.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Adds a new order to the repository asynchronously.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>The unique identifier of the added order.</returns>
        Task<int> AddAsync(Order order);

        /// <summary>
        /// Retrieves an order by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <returns>The order if found; otherwise, null.</returns>
        Task<Order?> GetByIdAsync(int id);

        /// <summary>
        /// Generates a shipping slip for the specified order asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns></returns>
        Task<ShippingSlipDto> GenerateShippingSlipAsync(int orderId);
    }
}
