using Microsoft.Data.SqlClient;
using System.Data;

namespace EasyGroceries.E_Commerce.Api.Helpers
{
    /// <summary>
    /// Provides functionality to manage database connections.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// The connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class with the specified configuration.
        /// </summary>
        /// <param name="configuration">The application configuration containing the connection string.</param>
        /// <exception cref="InvalidOperationException">Thrown when the connection string is not configured.</exception>
        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
        }

        /// <summary>
        /// Creates and returns a new database connection.
        /// </summary>
        /// <returns>An <see cref="IDbConnection"/> instance.</returns>
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
