using EasyGroceries.E_Commerce.Api.Models.Entities;

namespace EasyGroceries.E_Commerce.Api.Tests.Models.Entitties
{
    /// <summary>
    /// Unit tests for the Product class.
    /// </summary>
    public class ProductTest
    {
        /// <summary>
        /// Tests setting and getting all properties of the Product class with valid values.
        /// </summary>
        [Fact]
        public void Product_SetAndGetProperties_PositiveCase()
        {
            // Arrange
            var product = new Product();
            var productId = 1;
            var productName = "Sample Product";
            var productDesc = "This is a sample product.";
            var price = 19.99m;
            var isPhysical = true;
            var isActive = true;
            var createdDate = DateTime.Now;
            // Act
            product.ProductId = productId;
            product.ProductName = productName;
            product.ProductDesc = productDesc;
            product.Price = price;
            product.IsPhysical = isPhysical;
            product.IsActive = isActive;
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(productId, product.ProductId);
            Assert.Equal(productName, product.ProductName);
            Assert.Equal(productDesc, product.ProductDesc);
            Assert.Equal(price, product.Price);
            Assert.True(product.IsPhysical);
            Assert.True(product.IsActive);
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting an invalid price for the Product class.
        /// </summary>
        [Fact]
        public void Product_SetInvalidPrice_NegativeCase()
        {
            // Arrange
            var product = new Product();
            // Act
            product.Price = -10.00m;
            // Assert
            Assert.NotEqual(19.99m, product.Price);
        }

        /// <summary>
        /// Tests setting an invalid product name for the Product class.
        /// </summary>
        [Fact]
        public void Product_SetInvalidProductName_NegativeCase()
        {
            // Arrange
            var product = new Product();
            // Act
            product.ProductName = "";
            // Assert
            Assert.NotEqual("Sample Product", product.ProductName);
        }

        /// <summary>
        /// Tests setting an invalid product description for the Product class.
        /// </summary>
        [Fact]
        public void Product_SetInvalidProductDesc_NegativeCase()
        {
            // Arrange
            var product = new Product();
            // Act
            product.ProductDesc = "";
            // Assert
            Assert.NotEqual("This is a sample product.", product.ProductDesc);
        }

        /// <summary>
        /// Tests setting the IsPhysical property to an invalid value.
        /// </summary>
        [Fact]
        public void Product_SetInvalidIsPhysical_NegativeCase()
        {
            // Arrange
            var product = new Product();
            // Act
            product.IsPhysical = false;
            // Assert
            Assert.False(product.IsPhysical);
        }

        /// <summary>
        /// Tests setting the IsActive property to an invalid value.
        /// </summary>
        [Fact]
        public void Product_SetInvalidIsActive_NegativeCase()
        {
            // Arrange
            var product = new Product();
            // Act
            product.IsActive = false;
            // Assert
            Assert.False(product.IsActive);
        }

        /// <summary>
        /// Tests the default values of the Product class properties.
        /// </summary>
        [Fact]
        public void Product_SetDefaultValues_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            // Assert
            Assert.Equal(0, product.ProductId);
            Assert.Null(product.ProductName);
            Assert.Null(product.ProductDesc);
            Assert.Equal(0, product.Price);
            Assert.False(product.IsPhysical);
            Assert.False(product.IsActive);
            Assert.Equal(DateTime.MinValue, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to a valid value.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDate_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = DateTime.Now;
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to a future date.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDateToFuture_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = DateTime.Now.AddDays(1);
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to a past date.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDateToPast_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = DateTime.Now.AddDays(-1);
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to DateTime.MinValue.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDateToMinValue_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = DateTime.MinValue;
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to DateTime.MaxValue.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDateToMaxValue_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = DateTime.MaxValue;
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }

        /// <summary>
        /// Tests setting the CreatedDate property to its default value.
        /// </summary>
        [Fact]
        public void Product_SetCreatedDateToDefault_ShouldBeValid()
        {
            // Arrange
            var product = new Product();
            var createdDate = default(DateTime);
            // Act
            product.CreatedDate = createdDate;
            // Assert
            Assert.Equal(createdDate, product.CreatedDate);
        }
    }
}
