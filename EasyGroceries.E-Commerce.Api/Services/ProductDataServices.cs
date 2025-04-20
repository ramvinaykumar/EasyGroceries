using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;

namespace EasyGroceries.E_Commerce.Api.Services
{
    /// <summary>
    /// Service class for managing product data.
    /// </summary>
    public class ProductDataServices : IProductDataServices
    {
        /// <summary>
        /// The product repository used for data access.
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDataServices"/> class.
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductDataServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Adds a new product to the repository and returns the created product as a DTO.
        /// </summary>
        /// <param name="addProductDto">The product data to add.</param>
        /// <returns>The created product as a DTO, or null if the operation fails.</returns>
        public async Task<ProductDto?> AddAsync(ProductDto addProductDto)
        {
            var newProcuct = new Product
            {
                ProductName = addProductDto.Name,
                ProductDesc = addProductDto.Description,
                Price = addProductDto.Price,
                IsPhysical = addProductDto.IsPhysical,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            var newId = await _productRepository.AddAsync(newProcuct);
            newProcuct.ProductId = newId;

            return await MapEntityToDto(newProcuct);
        }

        /// <summary>
        /// Retrieves all products from the repository and maps them to DTOs.
        /// </summary>
        /// <returns>A collection of product DTOs.</returns>
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.ProductName,
                Description = p.ProductDesc,
                Price = p.Price,
                IsPhysical = p.IsPhysical
            });
        }

        /// <summary>
        /// Retrieves a product by its ID and maps it to a DTO.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product as a DTO, or null if not found.</returns>
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            return await MapEntityToDto(result);
        }

        /// <summary>
        /// Updates an existing product in the repository and returns the updated product as a DTO.
        /// </summary>
        /// <param name="updateProductDto">The product data to update.</param>
        /// <returns>The updated product as a DTO, or null if the operation fails.</returns>
        public async Task<ProductDto?> UpdateAsync(ProductDto updateProductDto)
        {
            var updateProcuct = new Product
            {
                ProductName = updateProductDto.Name,
                ProductDesc = updateProductDto.Description,
                Price = updateProductDto.Price,
                IsPhysical = updateProductDto.IsPhysical,
                ProductId = updateProductDto.ProductId
            };
            var updateId = await _productRepository.UpdateAsync(updateProcuct);

            var result = updateId > 0 ? await MapEntityToDto(updateProcuct) : null;

            return result;
        }

        /// <summary>
        /// Maps a product entity to a product DTO.
        /// </summary>
        /// <param name="product">The product entity to map.</param>
        /// <returns>The mapped product DTO, or null if the product is null.</returns>
        private Task<ProductDto?> MapEntityToDto(Product? product)
        {
            var result = product != null
                ? new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.ProductName,
                    Price = product.Price,
                    Description = product.ProductDesc,
                    IsPhysical = product.IsPhysical
                }
                : null;

            return Task.FromResult(result);
        }
    }
}
