using Domain.Entities.Orders;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Shipments
{
    public class Shipment : BaseEntity, IAggregateRoot
    {
        public DateTime ShipmentDate { get; protected set; }
        [MaxLength(2000)]
        public string? Address { get; protected set; }
        [MaxLength(500)]
        public string? City { get; protected set; }
        [MaxLength(500)]
        public string? State { get; protected set; }
        [MaxLength(500)]
        public string? Country { get; protected set; }
        [MaxLength(500)]
        public string? ZipCode { get; protected set; }
        public long CustomerId { get; protected set; }

        private readonly List<Order> _order = new List<Order>();
        public IReadOnlyCollection<Order> Orders => _order.AsReadOnly();
    }
}
