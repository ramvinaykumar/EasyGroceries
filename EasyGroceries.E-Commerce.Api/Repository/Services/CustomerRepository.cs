using Dapper;
using EasyGroceries.E_Commerce.Api.Helpers;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Repository.Services
{
    /// <summary>
    /// Provides methods for managing customer data in the database.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// The database service used for data access.
        /// </summary>
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class with the specified database service.
        /// </summary>
        /// <param name="dbService">The database service used for database operations.</param>
        public CustomerRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Adds a new customer to the database asynchronously. If a customer with the same email already exists, returns the existing customer's ID.
        /// </summary>
        /// <param name="customer">The customer entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the newly added or existing customer.</returns>
        public async Task<int> AddAsync(Customer customer)
        {
            using var connection = _dbService.CreateConnection();
            var existingCustomerId = await GetCustomerIdByEmailAsync(customer.EmailAddress);

            if (existingCustomerId > 0)
            {
                return existingCustomerId;
            }
            return await connection.ExecuteScalarAsync<int>("INSERT INTO Customers (CustomerName, EmailAddress, CustomerAddress) OUTPUT INSERTED.CustomerId VALUES (@CustomerName, @EmailAddress, @CustomerAddress)", customer);
        }

        /// <summary>
        /// Retrieves all active customers from the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of active customers.</returns>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryAsync<Customer>("SELECT CustomerId, CustomerName, EmailAddress, CustomerAddress, IsActive, CreatedDate FROM Customers WHERE IsActive = 1");
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer entity if found; otherwise, null.</returns>
        public async Task<Customer?> GetByIdAsync(int id)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Customer>("SELECT CustomerId, CustomerName, EmailAddress, CustomerAddress FROM Customers WHERE IsActive = 1 AND CustomerId = @Id", new { Id = id });
        }

        /// <summary>
        /// Retrieves the ID of a customer by their email address asynchronously.
        /// </summary>
        /// <param name="email">The email address of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the customer if found; otherwise, 0.</returns>
        private async Task<int> GetCustomerIdByEmailAsync(string? email)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<int>("SELECT CustomerId FROM Customers WHERE EmailAddress = @Email", new { Email = email });
        }
    }
}
