using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Tests.ClassValidator
{
    /// <summary>
    /// Provides validation methods for the Customer entity.
    /// </summary>
    public static class CustomerValidator
    {
        /// <summary>
        /// Validates the properties of a given customer object.
        /// Ensures that required fields such as CustomerName and CustomerAddress are not null.
        /// </summary>
        /// <param name="customer">The customer object to validate.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when CustomerName or CustomerAddress is null.
        /// </exception>
        public static void ValidateCustomer(Customer customer)
        {
            if (customer.CustomerName == null)
            {
                throw new ArgumentNullException(nameof(customer.CustomerName), "CustomerName cannot be null.");
            }

            if (customer.CustomerAddress == null)
            {
                throw new ArgumentNullException(nameof(customer.CustomerAddress), "CustomerAddress cannot be null.");
            }
        }
    }

}
