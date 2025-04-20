using EasyGroceries.E_Commerce.Api.Models.DTOs;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.DTOs
{
    /// <summary>
    /// Unit tests for the ProductDto class.
    /// </summary>
    public class ProductDtoTest
    {
        /// <summary>
        /// Tests if the properties of the ProductDto class can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void ProductDto_SetAndGetProperties_ShouldBeValid()
        {
            // Arrange
            var description = "Procut Description";
            var isPhysical = true;
            var name = "Procut 1";
            var price = 1.25M;
            var productId = 101;

            // Act
            var productDto = new ProductDto()
            {
                Description = "Procut Description",
                IsPhysical = true,
                Name = "Procut 1",
                Price = 1.25M,
                ProductId = 101
            };

            // Assert
            Assert.Equal(productId, productDto.ProductId);
            Assert.Equal(description, productDto.Description);
            Assert.Equal(isPhysical, productDto.IsPhysical);
            Assert.Equal(name, productDto.Name);
            Assert.Equal(price, productDto.Price);
            Assert.Equal(productId, productDto.ProductId);
        }

        /// <summary>
        /// Verifies that the default values of the ProductDto properties are valid.
        /// </summary>
        [Fact]
        public void ProductDto_DefaultValues_ShouldBeValid()
        {
            // Arrange
            var ProductDto = new ProductDto();

            // Assert
            Assert.Equal(0, ProductDto.ProductId); // Default value for int is 0
        }

        /// <summary>
        /// Tests if the ProductDto class can handle null values for its properties.
        /// </summary>
        [Fact]
        public void ProductDto_SetNullValues_ShouldBeHandled()
        {
            // Arrange
            var ProductDto = new ProductDto();

            // Act
            ProductDto.Name = null;
            ProductDto.Description = null;
            ProductDto.Price = Decimal.Zero;

            // Assert
            Assert.Null(ProductDto.Name);
            Assert.Null(ProductDto.Description);
            Assert.Equal(Decimal.Zero, ProductDto.Price);
        }

        /// <summary>
        /// Tests if the ProductDto class can handle empty string values for its properties.
        /// </summary>
        [Fact]
        public void ProductDto_SetEmptyStringValues_ShouldBeHandled()
        {
            // Arrange
            var ProductDto = new ProductDto();
            var emptyString = string.Empty;

            // Act
            ProductDto.Name = emptyString;
            ProductDto.Description = emptyString;
            ProductDto.Price = 0;

            // Assert
            Assert.Equal(emptyString, ProductDto.Name);
            Assert.Equal(emptyString, ProductDto.Description);
            Assert.Equal(0, ProductDto.Price);
        }
    }
}
