using Application.Catalog.Carts.Commands;
using Application.Common.Interfaces;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Domain.UnitTests.Application.Catalog.Carts.Commands;

[TestFixture]
public class CreateCartCommandTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IReadRepository<Product>> _productReadRepositoryMock;
    private Mock<IRepository<Cart>> _cartRepositoryMock;
    private CreateCartCommandHandler _handler;
    private List<Product> _products;
    private List<Cart> _carts;

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _productReadRepositoryMock = new Mock<IReadRepository<Product>>();
        _cartRepositoryMock = new Mock<IRepository<Cart>>();
        _handler = new CreateCartCommandHandler(
            _unitOfWorkMock.Object,
            _productReadRepositoryMock.Object,
            _cartRepositoryMock.Object);

        // Initialize in-memory data
        _products = new List<Product>();
        _carts = new List<Cart>();

        _unitOfWorkMock.Setup(x => x.BeginTransactionAsync())
            .Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(x => x.CommitAsync())
            .Returns(Task.CompletedTask);
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
        _handler = null;
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

        var product = Product.Create("SKU-001", "Test Product 1", 10.0m, 5, 1);
        _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        Cart addedCart = null;
        _cartRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()))
            .Callback<Cart, CancellationToken>((cart, _) => 
            {
                addedCart = cart;
                _carts.Add(cart);
            })
            .Returns(Task.CompletedTask);

        _cartRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        addedCart.Should().NotBeNull();
        _carts.Should().HaveCount(1);
        var cart = _carts.First();
        cart.ProductId.Should().Be(command.ProductId);
        cart.CustomerId.Should().Be(command.CustomerId);
        cart.Quantity.Should().Be(command.Quantity);
        
        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
        _cartRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cart>(), It.IsAny<CancellationToken>()), Times.Once);
        _cartRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithNonExistentProduct_ShouldThrowException()
    {
        // Arrange
        var command = new CreateCartCommand
        {
            ProductId = 999,
            CustomerId = 1,
            Quantity = 2
        };

        _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command.ProductId))
            .ReturnsAsync((Product?)null);

        // Act & Assert
        var action = () => _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Product not found.");
        _carts.Should().BeEmpty();
    }

    [Test]
    public async Task Handle_WithInsufficientStock_ShouldThrowException()
    {
        // Arrange
        var command = new CreateCartCommand
        {
            ProductId = 1,
            CustomerId = 1,
            Quantity = 10
        };

        var product = Product.Create("SKU-001", "Test Product 1", 10.0m, 5, 1);
        _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        // Act & Assert
        var action = () => _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Insufficient stock. Available: 5, Requested: 10");
        _carts.Should().BeEmpty();
    }

    [Test]
    public async Task Handle_WithZeroQuantity_ShouldThrowException()
    {
        // Arrange
        var command = new CreateCartCommand
        {
            ProductId = 1,
            CustomerId = 1,
            Quantity = 0
        };

        var product = Product.Create("SKU-001", "Test Product 1", 10.0m, 5, 1);
        _productReadRepositoryMock.Setup(x => x.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        // Act & Assert
        var action = () => _handler.Handle(command, CancellationToken.None);
        await action.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Quantity must be greater than zero.");
        _carts.Should().BeEmpty();
    }
} 