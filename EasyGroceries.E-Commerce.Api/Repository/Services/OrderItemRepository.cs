using Dapper;
using EasyGroceries.E_Commerce.Api.Helpers;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Repository.Services
{
    /// <summary>
    /// Provides repository operations for managing order items in the database.
    /// </summary>
    public class OrderItemRepository : IOrderItemRepository
    {
        /// <summary>
        /// The database service used for data access.
        /// </summary>
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemRepository"/> class with the specified database service.
        /// </summary>
        /// <param name="dbService">The database service used to manage database connections.</param>
        public OrderItemRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Asynchronously adds a range of order items to the database.
        /// </summary>
        /// <param name="orderItems">The list of order items to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddRangeAsync(List<OrderItem> orderItems)
        {
            using var connection = _dbService.CreateConnection();
            foreach (var item in orderItems)
            {
                await connection.ExecuteAsync("INSERT INTO OrderItems (OrderId, ProductId, Quantity, DiscountedPrice) VALUES (@OrderId, @ProductId, @Quantity, @DiscountedPrice)", item);
            }
        }
    }
}
