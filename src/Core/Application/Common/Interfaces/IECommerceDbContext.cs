using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IECommerceDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
    }
}
