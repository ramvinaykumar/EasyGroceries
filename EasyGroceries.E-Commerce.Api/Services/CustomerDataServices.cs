using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Services
{
    /// <summary>
    /// Provides services for managing customer data.
    /// </summary>
    public class CustomerDataServices : ICustomerDataServices
    {
        /// <summary>
        /// The customer repository to interact with customer data.
        /// </summary>
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDataServices"/> class.
        /// </summary>
        /// <param name="customerRepository">The customer repository to interact with customer data.</param>
        public CustomerDataServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Adds a new customer asynchronously.
        /// </summary>
        /// <param name="requestDto">The customer data transfer object containing the details of the customer to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added CustomerDto object, or null if the operation fails.</returns>
        public async Task<CustomerDto?> AddCustomersAsync(CustomerDto requestDto)
        {
            var customer = new Customer
            {
                CustomerId = requestDto.CustomerId,
                CustomerName = requestDto.Name,
                EmailAddress = requestDto.Email,
                CustomerAddress = requestDto.Address
            };
            var customerId = await _customerRepository.AddAsync(customer);

            return await MapCustomerDtoWithEntity(customerId);
        }

        /// <summary>
        /// Retrieves all customers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of CustomerDto objects.</returns>
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(p => new CustomerDto
            {
                CustomerId = p.CustomerId,
                Name = p.CustomerName,
                Email = p.EmailAddress,
                Address = p.CustomerAddress
            });
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the CustomerDto object if found, or null if not found.</returns>
        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            return await MapCustomerDtoWithEntity(id);
        }

        /// <summary>
        /// Maps a customer entity to a CustomerDto object asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the mapped CustomerDto object, or null if the customer is not found.</returns>
        private async Task<CustomerDto?> MapCustomerDtoWithEntity(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            var customerResponse = customer != null
                ? new CustomerDto
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.CustomerName,
                    Email = customer.EmailAddress,
                    Address = customer.CustomerAddress
                }
                : null;

            return customerResponse;
        }
    }
}
