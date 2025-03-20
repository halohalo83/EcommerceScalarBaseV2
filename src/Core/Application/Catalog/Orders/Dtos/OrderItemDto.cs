using Application.Common.Interfaces;

namespace Application.Catalog.Orders.Dtos
{
    public class OrderItemDto : IDto
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
