using Domain.Common.Enums;
using Domain.Interfaces;

namespace Domain.Entities.Orders
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; protected set; }
        public OrderStatus OrderStatus { get; protected set; }
        public decimal TotalPrice { get; protected set; }
        public long? CustomerId { get; protected set; }
        public long? PaymentId { get; protected set; }
        public long? ShipmentId { get; protected set; }

        private IList<OrderItem> _orderItems = new List<OrderItem>();
        public virtual IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public static Order Create(long? customerId)
        {
            return new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                OrderStatus = OrderStatus.Processing,
            };
        }

        public void AddOrderItems(IEnumerable<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                _orderItems.Add(item);
            }
        }

        public void GetTotalPrice()
        {
            TotalPrice = _orderItems.Sum(l => l.Quantity * l.Price);
        }
    }
}
