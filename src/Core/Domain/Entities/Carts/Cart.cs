using Domain.Entities.Products;

namespace Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
        public int Quantity { get; protected set; }
        public long ProductId { get; protected set; }
        public long CustomerId { get; protected set; }

        public Cart()
        {
        }

        public Cart(long id, long productId, long customerId, int quantity)
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
            Quantity = quantity;
        }

        public static Cart Create(long productId, long customerId, int quantity, Product? product)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero.");

            if (product == null)
                throw new InvalidOperationException("Product not found.");

            if (!product.CheckStock(quantity))
                throw new InvalidOperationException($"Insufficient stock. Available: {product.Stock}, Requested: {quantity}");

            return new Cart { ProductId = productId, CustomerId = customerId, Quantity = quantity };
        }
    }
}
