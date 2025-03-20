using Domain.Entities.Carts;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Entities.Shipments;
using Domain.Entities.Wishlists;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Customers
{
    public class Customer : BaseEntity, IAggregateRoot
    {
        [MaxLength(200)]
        public string? FirstName { get; protected set; }
        [MaxLength(200)]
        public string? LastName { get; protected set; }
        [MaxLength(500)]
        public string? Email { get; protected set; }
        [MaxLength(50)]
        public string? PhoneNumber { get; protected set; }

        private readonly List<Order> _order= new List<Order>();
        public IReadOnlyCollection<Order> Orders => _order.AsReadOnly();

        private readonly List<Payment> _payment = new List<Payment>();
        public IReadOnlyCollection<Payment> Payments => _payment.AsReadOnly();

        private readonly List<Shipment> _shipment = new List<Shipment>();
        public IReadOnlyCollection<Shipment> Shipments => _shipment.AsReadOnly();

        private readonly List<Wishlist> _wishlist = new List<Wishlist>();
        public IReadOnlyCollection<Wishlist> Wishlists => _wishlist.AsReadOnly();

        private readonly List<Cart> _cart = new List<Cart>();
        public IReadOnlyCollection<Cart> Carts => _cart.AsReadOnly();

        public static Customer Create(string? firstName, string? lastName, string? email, string? phoneNumber)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            return customer;
        }
    }
}
