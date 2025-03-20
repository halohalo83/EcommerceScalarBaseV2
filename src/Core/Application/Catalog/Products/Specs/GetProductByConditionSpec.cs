using Application.Catalog.Products.Dtos;
using Application.Catalog.Products.Queries;
using Application.Common.Specification;
using Ardalis.Specification;
using Domain.Entities.Products;

namespace Application.Catalog.Products.Specs
{
    public sealed class GetProductByConditionSpec : BaseSpec<Product, ProductDto>, ISingleResultSpecification<Product, ProductDto>
    {
        public GetProductByConditionSpec(GetProductByConditionQuery request) : base(request)
        {
            Query.Where(x => x.Description!.Contains(request.Keyword!), condition: !string.IsNullOrEmpty(request.Keyword));
        }
    }
}
