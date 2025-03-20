using Domain.Common.Enums;
using Domain.Entities.Orders;
using Domain.Interfaces;

namespace Domain.Entities.Payments
{
    public class Payment : BaseEntity, IAggregateRoot
    {
        public DateTime PaymentDate { get; protected set; }
        public PaymentMethod PaymentMethod { get; protected set; }
        public decimal Amount { get; protected set; }
        public long CustomerId { get; protected set; }

        private readonly List<Order> _order = new List<Order>();
        public IReadOnlyCollection<Order> Orders => _order.AsReadOnly();
    }
}
