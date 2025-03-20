using Domain.Entities.Products;

namespace Domain.Entities.Orders
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; protected set; }
        public decimal Price { get; protected set; }
        public long ProductId { get; protected set; }
        public long OrderId { get; protected set; }

        public virtual Product? Product { get; set; }

        public virtual Order? Order { get; set; }

        public static OrderItem Create (long orderId, long productId, decimal price, int quantity)
        {
            if (price <= 0)
                throw new InvalidOperationException("Invalid price.");

            if (quantity <= 0)
                throw new InvalidOperationException("Invalid quantity.");

            return new OrderItem
            {
                Quantity = quantity,
                Price = price,
                ProductId = productId,
                OrderId = orderId
            };
        }

        public void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Invalid quantity.");

            Quantity += quantity;
        }
    }
}
