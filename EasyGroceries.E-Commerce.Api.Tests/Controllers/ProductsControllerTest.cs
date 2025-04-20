using EasyGroceries.E_Commerce.Api.Controllers;
using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Models.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EasyGroceries.E_Commerce.Api.Tests.Controllers
{
    /// <summary>
    /// Controller test class for testing the ProductsController.
    /// </summary>
    public class ProductsControllerTest
    {
        private Mock<IProductDataServices>? _mockProductDataServices;
        private ProductsController? _productController;

        /// <summary>
        /// Initializes the mock services and controller for testing.
        /// </summary>
        private void Init()
        {
            _mockProductDataServices = new Mock<IProductDataServices>();
            _productController = new ProductsController(_mockProductDataServices.Object);
        }

        /// <summary>
        /// Tests that GetGroceries returns an Ok result with a list of products when the service provides valid data.
        /// </summary>
        [Fact]
        public async Task GetGroceries_ValidRequest_ReturnsOkWithProducts()
        {
            Init();
            var products = MockProducts();
            _mockProductDataServices?.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);
            var result = await _productController!.GetGroceries();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(products);
        }

        /// <summary>
        /// Tests that GetGroceries returns an Ok result with an empty list when no products are available.
        /// </summary>
        [Fact]
        public async Task GetGroceries_NoProducts_ReturnsOkWithEmptyList()
        {
            Init();
            var products = new List<ProductDto>();
            _mockProductDataServices?.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);
            var result = await _productController!.GetGroceries();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(products);
        }

        /// <summary>
        /// Tests that GetGroceries returns an Internal Server Error when the service throws an exception.
        /// </summary>
        [Fact]
        public async Task GetGroceries_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();
            var exceptionMessage = "Service error";
            _mockProductDataServices?.Setup(service => service.GetAllProductsAsync()).ThrowsAsync(new Exception(exceptionMessage));
            var result = await _productController!.GetGroceries();
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Tests that GetGroceriesById returns an Ok result with an empty list when no products are available.
        /// </summary>
        [Fact]
        public async Task GetGroceriesById_NoProducts_ReturnsOkWithEmptyList()
        {
            Init();
            var products = new List<ProductDto>();
            _mockProductDataServices?.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);
            var result = await _productController!.GetGroceries();
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(products);
        }

        /// <summary>
        /// Tests that GetGroceriesById returns an Ok result with the product when a valid ID is provided.
        /// </summary>
        [Fact]
        public async Task GetGroceriesById_ValidId_ReturnsOkWithProduct()
        {
            Init();
            int productId = 1;
            var product = MockProduct();
            _mockProductDataServices?.Setup(service => service.GetByIdAsync(productId)).ReturnsAsync(product);
            var result = await _productController!.GetProductById(productId);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(product);
        }

        /// <summary>
        /// Tests that GetGroceriesById returns a NotFound result when an invalid ID is provided.
        /// </summary>
        [Fact]
        public async Task GetGroceriesById_InvalidId_ReturnsNotFound()
        {
            Init();
            int productId = 999;
            _mockProductDataServices?.Setup(service => service.GetByIdAsync(productId)).ReturnsAsync((ProductDto)null);
            var result = await _productController!.GetProductById(productId);
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        /// <summary>
        /// Tests that GetGroceriesById returns an Internal Server Error when the service throws an exception.
        /// </summary>
        [Fact]
        public async Task GetGroceriesById_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();
            int productId = 1;
            var exceptionMessage = "Service error";
            _mockProductDataServices?.Setup(service => service.GetByIdAsync(productId)).ThrowsAsync(new Exception(exceptionMessage));
            var result = await _productController!.GetProductById(productId);
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Tests that AddNewProduct returns an Ok result with the added product when valid input is provided.
        /// </summary>
        [Fact]
        public async Task AddNewProduct_ValidInput_ReturnsOkWithResponse()
        {
            Init();
            var productDto = MockProduct();
            var expectedResponse = MockProduct();
            _mockProductDataServices?.Setup(service => service.AddAsync(productDto)).ReturnsAsync(expectedResponse);
            var result = await _productController!.AddNewProduct(productDto);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        /// <summary>
        /// Tests that AddNewProduct returns a BadRequest result when invalid input is provided.
        /// </summary>
        [Fact]
        public async Task AddNewProduct_InvalidInput_ReturnsBadRequest()
        {
            Init();
            ProductDto productDto = null;
            var result = await _productController!.AddNewProduct(productDto);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        /// <summary>
        /// Tests that AddNewProduct returns an Internal Server Error when the service throws an exception.
        /// </summary>
        [Fact]
        public async Task AddNewProduct_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();
            var productDto = MockProduct();
            var exceptionMessage = "Service error";
            _mockProductDataServices?.Setup(service => service.AddAsync(productDto)).ThrowsAsync(new Exception(exceptionMessage));
            var result = await _productController!.AddNewProduct(productDto);
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Tests that UpdateProduct returns an Ok result with the updated product when valid input is provided.
        /// </summary>
        [Fact]
        public async Task UpdateProduct_ValidInput_ReturnsOkWithResponse()
        {
            Init();
            var productDto = MockProduct();
            var expectedResponse = MockProduct();
            _mockProductDataServices?.Setup(service => service.UpdateAsync(productDto)).ReturnsAsync(expectedResponse);
            var result = await _productController!.UpdateProduct(productDto);
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        /// <summary>
        /// Tests that UpdateProduct returns a BadRequest result when invalid input is provided.
        /// </summary>
        [Fact]
        public async Task UpdateProduct_InvalidInput_ReturnsBadRequest()
        {
            Init();
            ProductDto productDto = null;
            var result = await _productController!.UpdateProduct(productDto);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        /// <summary>
        /// Tests that UpdateProduct returns an Internal Server Error when the service throws an exception.
        /// </summary>
        [Fact]
        public async Task UpdateProduct_ServiceThrowsException_ReturnsInternalServerError()
        {
            Init();
            var productDto = MockProduct();
            var exceptionMessage = "Service error";
            _mockProductDataServices?.Setup(service => service.UpdateAsync(productDto)).ThrowsAsync(new Exception(exceptionMessage));
            var result = await _productController!.UpdateProduct(productDto);
            var statusCodeResult = result.Should().BeOfType<ObjectResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            statusCodeResult.Value.Should().Be($"Internal server error: {exceptionMessage}");
        }

        /// <summary>
        /// Creates a mock list of products for testing purposes.
        /// </summary>
        /// <returns>A list of mock ProductDto objects.</returns>
        private List<ProductDto> MockProducts()
        {
            return new List<ProductDto>
                {
                    new ProductDto { ProductId = 1, Name = "Apple", Price = 0.5m },
                    new ProductDto { ProductId = 2, Name = "Banana", Price = 0.3m }
                };
        }

        /// <summary>
        /// Creates a mock product for testing purposes.
        /// </summary>
        /// <returns>A mock ProductDto object.</returns>
        private ProductDto MockProduct()
        {
            return new ProductDto { ProductId = 1, Name = "Apple", Price = 0.5m };
        }
    }
}
