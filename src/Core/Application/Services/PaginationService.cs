using Application.Common.Interfaces;
using Application.Common.Models;
using Ardalis.Specification;

namespace Application.Services;

public class PaginationService : IPaginationService
{
    public async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        IReadRepositoryBase<T> repository,
        ISpecification<T, TDestination> spec,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto
    {
        return await repository.PaginatedListAsync(spec, pageNumber, pageSize, cancellationToken);
    }

    public async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        IReadRepositoryBase<T> repository,
        ISpecification<T, TDestination> spec,
        int pageNumber,
        int pageSize,
        bool ignorePagination,
        CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IDto
    {
        return await repository.PaginatedListAsync(spec, pageNumber, ignorePagination ? 99999 : pageSize, cancellationToken);
    }
}