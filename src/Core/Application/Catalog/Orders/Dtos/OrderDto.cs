using Application.Common.Interfaces;

namespace Application.Catalog.Orders.Dtos
{
    public class OrderDto : IDto
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
