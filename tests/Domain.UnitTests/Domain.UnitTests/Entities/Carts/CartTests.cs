using Domain.Entities.Carts;
using Domain.Entities.Products;
using FluentAssertions;

namespace UnitTests.Entities.Carts
{
    [TestFixture]
    public class CartTests
    {
        private readonly string _sku = "SKU";
        private readonly string _description = "Description";
        private readonly long _categoryId = 1;
        private readonly long _productId = 1;
        private readonly long _customerId = 1;

        [Test]
        public void Create_WithValidData_ShouldCreateCart()
        {
            // Arrange
            decimal price = 10;
            int stock = 100;
            int quantity = 10;

            // Act
            var product = Product.Create(_sku, _description, price, stock, _categoryId);
            var cart = Cart.Create(_productId, _customerId, quantity, product);

            // Assert
            cart.Should().NotBeNull();
            cart.ProductId.Should().Be(_productId);
            cart.CustomerId.Should().Be(_customerId);
            cart.Quantity.Should().Be(quantity);
        }

        [Test]
        public void Create_WithZeroQuantity_ShouldThrowException()
        {
            // Arrange
            var quantity = 0;
            var price = 10;
            var stock = 5;
            var product = Product.Create(_sku, _description, price, stock, _categoryId);

            // Act & Assert
            var action = () => Cart.Create(_productId, _customerId, quantity, product);
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be greater than zero.");
        }

        [Test]
        public void Create_WithNegativeQuantity_ShouldThrowException()
        {
            // Arrange
            var quantity = -1;
            var price = 10;
            var stock = 5;
            var product = Product.Create(_sku, _description, price, stock, _categoryId);

            // Act & Assert
            var action = () => Cart.Create(_productId, _customerId, quantity, product);
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Quantity must be greater than zero.");
        }

        [Test]
        public void Create_WithNullProduct_ShouldThrowException()
        {
            // Arrange
            var quantity = 1;
            Product? product = null;

            // Act & Assert
            var action = () => Cart.Create(_productId, _customerId, quantity, product);
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("Product not found.");
        }

        [Test]
        public void Create_WithInsufficientStock_ShouldThrowException()
        {
            // Arrange
            var quantity = 10;
            var stock = 5;
            var price = 10;
            var product = Product.Create(_sku, _description, price, stock, _categoryId);

            // Act & Assert
            var action = () => Cart.Create(_productId, _customerId, quantity, product);
            action.Should().Throw<InvalidOperationException>()
                .WithMessage($"Insufficient stock. Available: {stock}, Requested: {quantity}");
        }
    }
}
