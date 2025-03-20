using Application.Common.Interfaces;

namespace Application.Catalog.Products.Dtos
{
    public class ProductDto : IDto
    {
        public long Id { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
