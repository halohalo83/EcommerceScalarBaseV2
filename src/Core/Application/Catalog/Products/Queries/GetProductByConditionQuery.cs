using Application.Catalog.Products.Dtos;
using Application.Catalog.Products.Specs;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Services;
using Domain.Entities.Products;
using MediatR;

namespace Application.Catalog.Products.Queries
{
    public class GetProductByConditionQuery : PaginationFilter, IRequest<PaginationResponse<ProductDto>>;
    
    public class GetProductByConditionQueryHandler(
         IPaginationService paginationService,
         IReadRepository<Product> productReadRepository) : IRequestHandler<GetProductByConditionQuery, PaginationResponse<ProductDto>>
    {
        public async Task<PaginationResponse<ProductDto>> Handle(GetProductByConditionQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetProductByConditionSpec(request);

            return await paginationService.PaginatedListAsync(
                productReadRepository,
                spec,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
    }
}
