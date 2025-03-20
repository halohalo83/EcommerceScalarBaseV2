using Application.Catalog.Products.Dtos;
using Application.Common.Interfaces;
using Domain.Entities.Products;
using MediatR;
using Mapster;
using Application.Common.Caching;
using Domain.Common.Constants;
using Application.Services.Catalog;

namespace Application.Catalog.Products.Queries
{
    public class GetAllProductQuery : IRequest<IList<ProductDto>>
    {
        public class GetAllProductQueryHandler(
            IReadRepository<Product> productReadRepository,
            ICacheService cacheService,
            IProductService productService) : IRequestHandler<GetAllProductQuery, IList<ProductDto>>
        {
            public async Task<IList<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                //return await GetByMemoryCache();

                var productDict = await productService.GetAllProducts();

                return [.. productDict.Values];
            }

            private async Task<IList<ProductDto>> GetByMemoryCache()
            {
                var productCaches = await cacheService.GetAsync<IList<ProductDto>>(CacheConstants.ProductCacheKey);
                if (productCaches != null)
                {
                    return productCaches;
                }
                var products = await productReadRepository.ListAsync();
                await cacheService.SetAsync(CacheConstants.ProductCacheKey, products.Adapt<IList<ProductDto>>());
                return products.Adapt<IList<ProductDto>>();
            }
        }
    }
}
