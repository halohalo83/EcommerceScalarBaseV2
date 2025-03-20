using Application.Common.Specification;
using Ardalis.Specification;

namespace Application.Common.Models;

public interface ISpecPayload
{
    /// <summary>
    /// Apply these operations on <see cref="ISpecificationBuilder{T}"/>: SearchBy, PaginateBy, OrderBy.
    /// </summary>
    /// <param name="specBuilder"></param>
    /// <param name="usePagination"></param>
    /// <typeparam name="T">Type of entity.</typeparam>
    public void ApplyOperations<T>(ISpecificationBuilder<T> specBuilder, bool? usePagination)
    {

        if (this is IPagination pagination)
        {
            if (usePagination is null or true && !pagination.IgnorePagination)
            {
                specBuilder.PaginateBy(pagination);
            }
        }

        ApplyOrderBy(specBuilder);
    }

    /// <summary>
    /// Apply these operations on <see cref="ISpecificationBuilder{T}"/>: SearchBy, PaginateBy, OrderBy.
    /// </summary>
    /// <param name="specBuilder"></param>
    /// <param name="usePagination"></param>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <typeparam name="TResult">Type of mapping result.</typeparam>
    public void ApplyOperations<T, TResult>(ISpecificationBuilder<T, TResult> specBuilder, bool? usePagination)
    {
        if (this is IPagination pagination)
        {
            if (usePagination is null or true && !pagination.IgnorePagination)
            {
                specBuilder.PaginateBy(pagination);
            }
        }

        ApplyOrderBy(specBuilder);
    }

    public void ApplyOrderBy<T>(ISpecificationBuilder<T> specBuilder)
    {
        if (this is IOrderBy orderBy)
        {
            var defaultOrderBy = GetDefaultOrderBy<T>();
            specBuilder.OrderBy(orderBy, defaultOrderBy);
        }
    }

    public void ApplyOrderBy<T, TResult>(ISpecificationBuilder<T, TResult> specBuilder)
    {
        if (this is IOrderBy orderBy)
        {
            var defaultOrderBy = orderBy.ForceUseCustomOrderBy
                ? new List<string>()
                : GetDefaultOrderBy<T>();
            specBuilder.OrderBy(orderBy, defaultOrderBy);
        }
    }

    private static List<string> GetDefaultOrderBy<T>()
    {
        List<string> defaultOrderBy = [];
        if (typeof(T).GetProperty("CreatedOn") != null)
        {
            defaultOrderBy.Add("CreatedOn desc");
        }

        return defaultOrderBy;
    }
}