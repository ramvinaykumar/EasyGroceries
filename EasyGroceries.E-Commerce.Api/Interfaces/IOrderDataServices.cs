using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Interfaces
{
    /// <summary>
    /// Interface for order data services, providing methods to process orders.
    /// </summary>
    public interface IOrderDataServices
    {
        /// <summary>
        /// Processes an order based on the provided checkout request.
        /// </summary>
        /// <param name="checkoutRequest">The checkout request containing order details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the order response details.</returns>
        Task<CustomerOrderResponseDto> ProcessOrderAsync(CheckoutRequestDto checkoutRequest);

        /// <summary>
        /// Get a shipping slip for the specified order asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>Return Shipping details based on orderId</returns>
        Task<ShippingSlipDto> GetShippingSlipAsync(int orderId);
    }
}
