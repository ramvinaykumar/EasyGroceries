using Dapper;
using EasyGroceries.E_Commerce.Api.Helpers;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Repository.Services
{
    /// <summary>
    /// Provides methods for managing orders in the e-commerce system.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        /// <summary>
        /// The database service used for data access.
        /// </summary>
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class with the specified database service.
        /// </summary>
        /// <param name="dbService">The database service used for database operations.</param>
        public OrderRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Adds a new order to the database asynchronously.
        /// </summary>
        /// <param name="order">The order to add.</param>
        /// <returns>The unique identifier of the added order.</returns>
        public async Task<int> AddAsync(Order order)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(
                "INSERT INTO Orders (CustomerId, OrderDate, TotalAmount, ShippingAddress) OUTPUT INSERTED.OrderId VALUES (@CustomerId, @OrderDate, @TotalAmount, @ShippingAddress)",
                order);
        }

        /// <summary>
        /// Retrieves an order by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <returns>The order if found; otherwise, null.</returns>
        public async Task<Order?> GetByIdAsync(int id)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Order>(
                "SELECT OrderId, CustomerId, OrderDate, TotalAmount, ShippingAddress FROM Orders WHERE OrderId = @Id",
                new { Id = id });
        }

        /// <summary>
        /// Generates a shipping slip for the specified order asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns></returns>
        public async Task<ShippingSlipDto> GenerateShippingSlipAsync(int orderId)
        {
            using var connection = _dbService.CreateConnection();
            var order = await connection.QuerySingleAsync<Order>("SELECT * FROM Orders WHERE OrderId = @Id", new { Id = orderId });
            var customer = await connection.QuerySingleAsync<Customer>("SELECT * FROM Customers WHERE CustomerId = @Id", new { Id = order.CustomerId });

            var items = await connection.QueryAsync<ShippingSlipItemDto>(@"
                SELECT P.ProductName AS ProductName, P.ProductDesc AS Description, OI.Quantity
                FROM OrderItems OI
                INNER JOIN Products P ON P.ProductId = OI.ProductId
                WHERE OI.OrderId = @OrderId AND P.IsPhysical = 1", new { OrderId = orderId });

            return new ShippingSlipDto
            {
                OrderId = orderId,
                CustomerName = customer.CustomerName == null ? "N/A" : customer.CustomerName,
                ShippingAddress = customer.CustomerAddress == null ? "N/A" : customer.CustomerAddress,
                OrderDate = order.OrderDate,
                Items = items.ToList()
            };
        }
    }
}
