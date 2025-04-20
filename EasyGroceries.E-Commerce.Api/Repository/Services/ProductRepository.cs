using Dapper;
using EasyGroceries.E_Commerce.Api.Helpers;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Repository.Services
{
    /// <summary>
    /// Repository class for managing product data.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The database service used for data access.
        /// </summary>
        private readonly DatabaseService _dbService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="dbService"></param>
        public ProductRepository(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Adds a new product to the database and returns the generated ID.
        /// </summary>
        /// <param name="newProduct">The product data to add.</param>
        /// <returns></returns>
        public async Task<int> AddAsync(Product newProduct)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.ExecuteScalarAsync<int>("INSERT INTO Products (ProductName, ProductDesc, Price, IsPhysical) OUTPUT INSERTED.ProductId VALUES (@ProductName, @ProductDesc, @Price, @IsPhysical)", newProduct);
        }

        /// <summary>
        /// Retrieves all active products from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryAsync<Product>("SELECT ProductId, ProductName, ProductDesc, Price, IsPhysical, IsActive, CreatedDate FROM Products WHERE IsActive = 1");
        }

        /// <summary>
        /// Retrieves a product by its ID from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = _dbService.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(
                "SELECT ProductId, ProductName, ProductDesc, Price, IsPhysical FROM Products WHERE IsActive = 1 AND ProductId = @Id",
                new { Id = id }
            );
        }

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="updateProduct">The product data to update.</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Product updateProduct)
        {
            const string sql = @"
                    UPDATE Products
                    SET
                        ProductName = @ProductName,
                        ProductDesc = @ProductDesc,
                        Price = @Price,
                        IsPhysical = @IsPhysical
                    WHERE
                        ProductId = @ProductId;";

            using var connection = _dbService.CreateConnection();
            return await connection.ExecuteAsync(sql, updateProduct);
        }
    }
}
