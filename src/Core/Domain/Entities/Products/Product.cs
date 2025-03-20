using Domain.Common;
using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.Wishlists;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Products
{
    public class Product : BaseEntity, IAggregateRoot
    {
        [MaxLength(100)]
        public string? SKU { get; protected set; }
        [MaxLength(500)]
        public string? Description { get; protected set; }
        public decimal Price { get; protected set; }
        public int Stock { get; protected set; }
        public long? CategoryId { get; protected set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private readonly List<Wishlist> _wishlist = new List<Wishlist>();
        public IReadOnlyCollection<Wishlist> Wishlists => _wishlist.AsReadOnly();

        private readonly List<Cart> _cart = new List<Cart>();
        public IReadOnlyCollection<Cart> Carts => _cart.AsReadOnly();

        public Product()
        {

        }

        public Product(long id, string sku, string description, decimal price, int stock, long? categoryId)
        {
            Id = id;
            SKU = sku;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public static Product Create(string sku, string description, decimal price, int stock, long? categoryId)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new InvalidOperationException("SKU is required.");
            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidOperationException("Description is required.");
            if (price <= 0)
                throw new InvalidOperationException("Price must be greater than zero.");
            if (stock < 0)
                throw new InvalidOperationException("Stock must be greater than or equal to zero.");
            return new Product { SKU = sku, Description = description, Price = price, Stock = stock, CategoryId = categoryId };
        }

        public void SetStock(int stock) {
            Stock = stock;
        }

        public bool CheckStock(int requestedQuantity) {
            return Stock >= requestedQuantity;
        }

        public void UpdateStock(int requestedQuantity)
        {
            if (!CheckStock(requestedQuantity))
                throw new InvalidOperationException($"Insufficient stock. Available: {Stock}, Requested: {requestedQuantity}");
            Stock -= requestedQuantity;
        }
    }
}
