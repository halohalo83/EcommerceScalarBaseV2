using Application.Catalog.Products.Dtos;
using Application.Common.Caching;
using Application.Common.Interfaces;
using Domain.Common.Constants;
using Domain.Entities.Products;
using Mapster;

namespace Application.Services.Catalog
{
    public class ProductService(
        IRedisCacheService redisCacheService,
        IReadRepository<Product> productReadRepository) : IProductService
    {
        public async Task<IDictionary<long, ProductDto>> GetAllProducts()
        {
            var cacheKey = RedisCacheConstants.AllProducts;

            // Fetch all products from cache
            var productCaches = await redisCacheService.GetCacheValueAsync<IDictionary<long, ProductDto>>(cacheKey);

            if (productCaches != null)
            {
                return productCaches;
            }

            // Fetch from DB and cache the result
            var products = (await productReadRepository.ListAsync())
                .Select(x => x.Adapt<ProductDto>())
                .ToDictionary(x => x.Id, x => x);

            await redisCacheService.SetCacheValueAsync(cacheKey, products);

            return products;
        }
    }
}
