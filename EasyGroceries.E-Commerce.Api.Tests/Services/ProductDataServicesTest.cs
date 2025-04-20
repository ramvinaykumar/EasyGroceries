using EasyGroceries.E_Commerce.Api.Models.DTOs;
using EasyGroceries.E_Commerce.Api.Models.Entities;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;
using EasyGroceries.E_Commerce.Api.Services;
using Moq;

namespace EasyGroceries.E_Commerce.Api.Tests.Services
{
    /// <summary>
    /// Unit tests for the ProductDataServices class.
    /// </summary>
    public class ProductDataServicesTest
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductDataServices _productDataServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDataServicesTest"/> class.
        /// </summary>
        public ProductDataServicesTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _productDataServices = new ProductDataServices(_mockProductRepository.Object);
        }

        /// <summary>
        /// Tests the AddAsync method for successful addition of a product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task AddAsync_SuccessfulAddition_ReturnsProductDto()
        {
            // Arrange
            var addProductDto = MockRequest_ProductData();
            var expectedProductId = 1;
            var expectedProduct = MockResponse_ProductData(expectedProductId);
            _mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ReturnsAsync(expectedProductId);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(expectedProductId)).ReturnsAsync(expectedProduct);
            // Act
            var result = await _productDataServices.AddAsync(addProductDto);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(addProductDto.ProductId, result.ProductId);
            Assert.Equal(addProductDto.Name, result.Name);
            Assert.Equal(addProductDto.Description, result.Description);
            Assert.Equal(addProductDto.Price, result.Price);
            Assert.Equal(addProductDto.IsPhysical, result.IsPhysical);
            _mockProductRepository.Verify(repo => repo.AddAsync(It.Is<Product>(p =>
                p.ProductName == addProductDto.Name &&
                p.ProductDesc == addProductDto.Description &&
                p.Price == addProductDto.Price &&
                p.IsPhysical == addProductDto.IsPhysical)), Times.Once);
        }

        /// <summary>
        /// Tests the AddAsync method for failed addition of a product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task AddAsync_FailedAddition_ThrowsException()
        {
            // Arrange
            var addProductDto = MockRequest_ProductData();
            _mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Failed to add product"));
            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _productDataServices.AddAsync(addProductDto));
            _mockProductRepository.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        /// <summary>
        /// Tests the GetByIdAsync method for retrieving an existing product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdAsync_ExistingProduct_ReturnsProductDto()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = MockResponse_ProductData(productId);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);
            // Act
            var result = await _productDataServices.GetByIdAsync(productId);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct?.ProductId, result.ProductId);
            Assert.Equal(expectedProduct?.ProductName, result.Name);
            Assert.Equal(expectedProduct?.ProductDesc, result.Description);
            Assert.Equal(expectedProduct?.Price, result.Price);
            Assert.Equal(expectedProduct?.IsPhysical, result.IsPhysical);
        }

        /// <summary>
        /// Tests the GetByIdAsync method for retrieving a non-existing product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdAsync_NonExistingProduct_ReturnsNull()
        {
            // Arrange
            var productId = 1;
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product?)null);
            // Act
            var result = await _productDataServices.GetByIdAsync(productId);
            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Tests the UpdateAsync method for updating an existing product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task UpdateAsync_ExistingProduct_ReturnsProductDto()
        {
            // Arrange
            var productId = 1;
            var updateProductDto = MockRequest_ProductData();
            var existingProduct = MockResponse_ProductData(productId);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockProductRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(1);
            // Act
            var result = await _productDataServices.UpdateAsync(updateProductDto);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateProductDto.ProductId, result.ProductId);
            Assert.Equal(updateProductDto.Name, result.Name);
            Assert.Equal(updateProductDto.Description, result.Description);
            Assert.Equal(updateProductDto.Price, result.Price);
            Assert.Equal(updateProductDto.IsPhysical, result.IsPhysical);
        }

        /// <summary>
        /// Tests the UpdateAsync method for updating a non-existing product.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task UpdateAsync_NonExistingProduct_ReturnsNull()
        {
            // Arrange
            int? productId = null;
            var updateProductDto = MockRequest_ProductData();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId ?? 0)).ReturnsAsync((Product?)null);
            // Act
            var result = await _productDataServices.UpdateAsync(updateProductDto);
            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Mocks the request data for a product.
        /// </summary>
        /// <returns></returns>
        private ProductDto MockRequest_ProductData()
        {
            return new ProductDto
            {
                ProductId = 1,
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.99m,
                IsPhysical = true
            };
        }

        /// <summary>
        /// Mocks the response data for a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private Product? MockResponse_ProductData(int productId)
        {
            return new Product
            {
                ProductId = productId,
                ProductName = "Test Product",
                ProductDesc = "Test Description",
                Price = 10.99m,
                IsPhysical = true
            };
        }
    }
}
