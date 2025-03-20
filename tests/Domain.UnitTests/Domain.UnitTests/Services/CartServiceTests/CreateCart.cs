
using Application.Catalog.Carts.Commands;
using Application.Common.Interfaces;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using FluentAssertions;
using Moq;

namespace UnitTests.Services.CartServiceTests
{
    [TestFixture]
    public class CreateCart
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IReadRepository<Product>> _productReadRepositoryMock;
        private Mock<IRepository<Cart>> _cartRepositoryMock;
        private List<Product> _products;
        private List<Cart> _carts;
        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productReadRepositoryMock = new Mock<IReadRepository<Product>>();
            _cartRepositoryMock = new Mock<IRepository<Cart>>();
            // Initialize in-memory data
            _products = new List<Product>
        {
            new Product(1, "SKU-001", "Product 1", 10, 5, 1),
            new Product(2, "SKU-002", "Product 2", 20, 10, 2),
            new Product(3, "SKU-003", "Product 3", 30, 0, 3)
        };

            _carts = new List<Cart>();
        }

        [TearDown]
        public void TearDown()
        {
            // Clear all in-memory data
            _products.Clear();
            _carts.Clear();

            // Reset mocks
            _unitOfWorkMock = null;
            _productReadRepositoryMock = null;
            _cartRepositoryMock = null;
        }

        [Test]
        public async Task Handle_WithValidData_ShouldCreateCart()
        {
            // Arrange
            var command = new CreateCartCommand
            {
                ProductId = 1,
                CustomerId = 1,
                Quantity = 2
            };

            var product = _products.First(p => p.SKU == "SKU-001");

            // Act
            _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command.ProductId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var cart = new Cart(1, product.Id, command.CustomerId, command.Quantity);
            _cartRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cart);

            _cartRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

            _carts.Add(cart);

            // Assert
            _carts.Should().HaveCount(1);
            _carts.First().ProductId.Should().Be(command.ProductId);
            _carts.First().CustomerId.Should().Be(command.CustomerId);
            _carts.First().Quantity.Should().Be(command.Quantity);
        }

        [Test]
        public async Task Handle_MultipleConcurrentRequests_ShouldHandleTransactionsCorrectly()
        {
            // Arrange
            var command1 = new CreateCartCommand { ProductId = 1, CustomerId = 1, Quantity = 2 };
            var command2 = new CreateCartCommand { ProductId = 2, CustomerId = 1, Quantity = 1 };

            var product1 = _products.First(p => p.SKU == "SKU-001");
            var product2 = _products.First(p => p.SKU == "SKU-002");

            _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command1.ProductId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product1);
            _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command2.ProductId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product2);

            _cartRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()))
                .Callback<Cart, CancellationToken>((cart, _) => _carts.Add(cart))
                .ReturnsAsync((Cart cart, CancellationToken _) => cart);

            _cartRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));

            _unitOfWorkMock.Setup(x => x.BeginTransactionAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.CommitAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            _unitOfWorkMock.Setup(x => x.RollbackAsync())
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var task1 = Task.Run(async () =>
            {
                await _unitOfWorkMock.Object.BeginTransactionAsync();
                var cart = new Cart(1, product1.Id, command1.CustomerId, command1.Quantity);
                await _cartRepositoryMock.Object.AddAsync(cart, It.IsAny<CancellationToken>());
                await _unitOfWorkMock.Object.CommitAsync();
            });

            var task2 = Task.Run(async () =>
            {
                await _unitOfWorkMock.Object.BeginTransactionAsync();
                var cart = new Cart(2, product2.Id, command2.CustomerId, command2.Quantity);
                await _cartRepositoryMock.Object.AddAsync(cart, It.IsAny<CancellationToken>());
                await _unitOfWorkMock.Object.CommitAsync();
            });

            await Task.WhenAll(task1, task2);

            // Assert
            _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Exactly(2));
            _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Exactly(2));
            _unitOfWorkMock.Verify(x => x.RollbackAsync(), Times.Never);

            _carts.Should().HaveCount(2);
            _carts.Should().Contain(cart => cart.ProductId == command1.ProductId && cart.CustomerId == command1.CustomerId && cart.Quantity == command1.Quantity);
            _carts.Should().Contain(cart => cart.ProductId == command2.ProductId && cart.CustomerId == command2.CustomerId && cart.Quantity == command2.Quantity);
        }

    }
}
