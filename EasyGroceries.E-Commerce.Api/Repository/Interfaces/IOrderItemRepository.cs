using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Repository.Interfaces
{
    /// <summary>
    /// Defines the contract for repository operations related to order items.
    /// </summary>
    public interface IOrderItemRepository
    {
        /// <summary>
        /// Asynchronously adds a range of order items to the data store.
        /// </summary>
        /// <param name="orderItems">The list of order items to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddRangeAsync(List<OrderItem> orderItems);
    }
}
