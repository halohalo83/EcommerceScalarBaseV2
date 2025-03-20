using Application.Catalog.Products.Dtos;

namespace Application.Services.Catalog
{
    public interface IProductService
    {
        Task<IDictionary<long, ProductDto>> GetAllProducts();
    }
}
